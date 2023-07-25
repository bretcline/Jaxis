using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using IDENTEC.ILR350.Readers;
using IDENTEC.ILR350.Tags;
using Jaxis.Engine.Base.Device;
using Jaxis.Interfaces;
using Jaxis.MessageLibrary;
using Jaxis.Readers.Identec.BeaconMessages;
using Jaxis.Util.Log4Net;

namespace Jaxis.Readers.Identec
{
    public class PourReader : AlertableProducerDevice
    {
        enum MessagePriority
        {
            Alert = 0,
            Pour,
            Connection,
            Heartbeat,
        }

        [Obfuscation(Exclude = true)]
        public static DeviceConfig GetDefaultDeviceConfig()
        {
            var rc = new DeviceConfig();
            Assembly asm = Assembly.GetExecutingAssembly();
            rc.AssemblyType = MethodBase.GetCurrentMethod().DeclaringType.FullName;
            rc.AssemblyName = asm.ManifestModule.Name;
            rc.AssemblyVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(BaseDevice.GetPath() + "\\" + rc.AssemblyName).FileVersion;
            rc.ID = Guid.NewGuid().ToString();
            rc.Name = "Identec Pour Device";
            rc.Type = DeviceType.DataProducer;
            rc.State = DeviceState.Stopped;
            rc.ProducerMessageType = 8;
            rc.ConsumerMessageType = 0;

            rc.Options.Add(new DeviceConfigOption { Name = "IP Address/COM Port", Value = "192.168.1.101" });
            rc.Options.Add(new DeviceConfigOption { Name = "Timeout", Value = "" });
            rc.Options.Add(new DeviceConfigOption { Name = "Frequency", Value = "" });
            rc.Options.Add(new DeviceConfigOption { Name = "RFBaudRate", Value = "" });
            rc.Options.Add(new DeviceConfigOption { Name = "Allow Heartbeat", Value = "true" });
            rc.Options.Add(new DeviceConfigOption { Name = "Allow Pours", Value = "true" });
            
            return rc;
        }

        private readonly Dictionary<uint, Int16> m_OldMsgs = new Dictionary<uint, Int16>( );
        private readonly Dictionary<uint, Jaxis.Readers.Identec.BeaconMessages.PourMessage> m_OldPours = new Dictionary<uint, Jaxis.Readers.Identec.BeaconMessages.PourMessage>();
        //private ILR350Reader m_ILR350Reader = null;
        private Dictionary<string, ILR350Reader> m_ILR350Readers = null;
        private readonly Gen3Manager m_Gen3Manager = new Gen3Manager();
        private readonly BeaconMessageParser m_BeaconMessageParser = new BeaconMessageParser( );
//        private readonly IdentecPour m_IdentecPour = null;
        private int m_Timeout = 5 * 60 * 60; // 5 minutes default, in Sec

        private Dictionary<MessagePriority, Queue<IMessage>> m_QueueList = new Dictionary<MessagePriority, Queue<IMessage>>();


        protected bool m_AllowHeartbeat = true;
        protected bool m_AllowPours = true;

        private Thread m_TagReader;
        private Thread m_MessageProcessor;
        private object m_QueueLocker = new object();

        private static string ATTACH_LOGGER = "IdentecAttach";
                
        public PourReader( )
            : this(GetDefaultDeviceConfig())
        {
        }


        public PourReader(IDeviceConfig _config)
            : base( _config )
        {
            try
            {
                Config.Type = DeviceType.DataProducer;
                State = Config.State;

//                m_IdentecPour = new IdentecPour { Driver = Config.Name };

                m_ILR350Readers.Clear();

                m_QueueList.Add( MessagePriority.Alert, new Queue<IMessage>() ); // Alert
                m_QueueList.Add( MessagePriority.Pour, new Queue<IMessage>() ); // Pour
                m_QueueList.Add( MessagePriority.Connection, new Queue<IMessage>() ); // Connection
                m_QueueList.Add( MessagePriority.Heartbeat, new Queue<IMessage>() ); // Heartbeat
            }
            catch( Exception exp )
            {
                Log.WriteException( "IdentecDev:: IdentecDev", exp );
            }
        }

        override public void Start( )
        {
            try
            {
                ConfigureReader( );

                State = DeviceState.Started;
                m_Stop = false;

                m_MessageProcessor = new Thread(ProcessMessages);
                m_MessageProcessor.Start();

                foreach (var reader in m_ILR350Readers.Values)
                {
                    ILR350Reader reader1 = reader;
                    Task.Factory.StartNew(() => ProcessTags(reader1));
                }
                //m_TagReader = new System.Threading.Thread( ProcessTags );
                //m_TagReader.Start( );
            }
            catch( Exception exp )
            {
                SendDeviceIssueMessage( AlertTypes.CannotConnect );
                Log.WriteException( "IdentecDev:: Start", exp );
            }
        }

        protected void MonitorStatus( )
        {
            while( !m_Stop )
            {
                System.Threading.Thread.Sleep( 1000 );
                State = 6 < ( DateTime.Now - m_LastMessage ).Minutes ? DeviceState.NotReporting : DeviceState.Running;
            }
        }

        private void ConfigureReader( )
        {
            try
            {
                if( Config.Options.Count != 0 )
                {
                    // Option 0 == IP Address or Com Port
                    string address = Config.GetAddress();
                    if( address.Contains( '.' ) )
                    {
                        m_Gen3Manager.DiscoverDevicesEth( address );

                        foreach (var device in m_Gen3Manager.Devices)
                        {
                            Log.Debug( string.Format( "{0}", device.SerialNumber ) );
                            m_ILR350Readers[device.SerialNumber] = device;
                        }
                    }
                    else if( !string.IsNullOrWhiteSpace( address ))
                    {
                        m_Gen3Manager.DiscoverDevices( address );
                        foreach (var device in m_Gen3Manager.Devices)
                        {
                            Log.Debug(string.Format("{0}", device.SerialNumber));
                            m_ILR350Readers[device.SerialNumber] = device;
                        }
                    }

                    foreach (var reader in m_ILR350Readers.Values)
                    {
                        if (null != reader)
                        {
                            m_Timeout = Config.GetTimeout();

                            reader.ResetToFactoryDefault();
                            reader.SetRFBeaconBaudrate(Config.GetBaudRate());
                            reader.SetFrequency(Config.GetFrequency());
                            reader.TXPower = 10;

                            Log.Debug(string.Format("Transmit power: {0} - Filter Level {1}", reader.TXPower,
                                                    reader.GetTagSignalFilterLevel()));

                            m_AllowPours = Config.GetPublishPours();
                            m_AllowHeartbeat = Config.GetPublishHeartbeats();

                            //                        reader.SetTagSignalFilterLevel()

                            HardwareID = reader.SerialNumber;

                            if (ClearAlert(AlertTypes.CannotConnect))
                            {
                                SendDeviceIssueMessage(AlertTypes.Reconnect);
                            }
                        }
                        else
                        {
                            Log.Debug("reader is null");
                            SendDeviceIssueMessage(AlertTypes.CannotConnect);
                        }
                    }
                }
                else
                {
                    Log.Debug( "No Options" );
                    SendDeviceIssueMessage(AlertTypes.CannotConnect);
                }
            }
            catch( Exception err )
            {
                Log.WriteException( "IdentecDev::ConfigureReader", err );
                SendDeviceIssueMessage(AlertTypes.CannotConnect);
            }
        }

        override public void Stop( )
        {
            try
            {
                State = DeviceState.Stopped;
//                Config.State = DeviceState.Stopped;
                m_Gen3Manager.CloseStream( );
            }
            catch( Exception exp )
            {
                Log.WriteException( "IdentecDev:: Stop", exp );
            }
            finally
            {
                m_Stop = true;
            }
        }

        protected void ProcessMessages( )
        {
            try
            {
                while( !m_Stop )
                {
                    try
                    {
                        bool emptyQueues = true;
                        lock (m_QueueLocker)
                        {
                            foreach (var queue in m_QueueList.Values)
                            {
                                if( 0 < queue.Count )
                                {
                                    emptyQueues = false;
                                    for( int i = 0; i < 10; ++i )
                                    {
                                        if( 0 < queue.Count )
                                        {
                                            IMessage msg = queue.Dequeue();
                                            if (null != msg)
                                            {
                                                ProduceMessage(msg);
                                            }
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                        if( emptyQueues )
                        {
                            Thread.Sleep( 1000 );
                        }
                    }
                    catch (Exception exp)
                    {
                        Log.WriteException("IdentecDev::ProcessMessages", exp);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException("IdentecDev::ProcessMessages", exp);
            }
        }

        public void ProcessTags( ILR350Reader _device )
        {
            int timeFromLastMsg = 0;
            try
            {
                while (!m_Stop && null != _device)
                {
                    try
                    {
                        ILR350TagCollection beaconTags = _device.GetBeaconTags( );

                        if( beaconTags.Count > 100 )
                        {
                            Log.Write( string.Format( "Pulled {0} tags", beaconTags.Count ), LogType.Debug );
                        }

                        //m_ILR350Reader.Status
                        foreach( ILR350Tag tag in beaconTags )
                        {
                            timeFromLastMsg = 0;
                            if( false == FilterMsg( tag ) )
                            {
                                SendMessage( tag );
                            }
                        }

                        if( 0 == beaconTags.Count )
                        {
                            timeFromLastMsg += 1;
                            if( timeFromLastMsg >= m_Timeout )
                            {
                                SendDeviceIssueMessage( AlertTypes.NotReadingTags );
                                timeFromLastMsg = 0;
                            }
                        }
                        else
                        {
                            m_LastMessage = DateTime.Now;
                        }
                        System.Threading.Thread.Sleep( 1000 );
                    }
                    catch( Exception exp )
                    {
                        m_Gen3Manager.CloseStream( );
                        ConfigureReader( );
                        Log.WriteException( "IdentecDev::ProcessTags", exp );
                    }
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "IdentecDev::ProcessTags", exp );
            }
        }

        private void SendDeviceIssueMessage( AlertTypes _type, bool _forceMessage = false )
        {
            try
            {
                var message = new DeviceAlertMessage { Driver = Config.AssemblyType, DeviceID = Config.Name, AlertClass = AlertClasses.Devices, ReadTime = DateTime.Now};
                var messageText = "Unable to connect";
                if (_type == AlertTypes.Reconnect )
                {
                    messageText = "Reconnect";
                }
                messageText = string.Format("{2} to reader {0} at {1}", message.DeviceID, message.ReadTime, messageText);
                SendAlert(message, _type, messageText, _forceMessage);
            }
            catch( Exception exp )
            {
                Log.WriteException( "IdentecDev:: SendDetachMessage", exp );
            }
        }

        private void QueueMessage(MessagePriority _priority, IMessage _message)
        {
            lock (m_QueueLocker)
            {
                m_QueueList[_priority].Enqueue(_message);
            }
        }

        private void SendAlertMessage( AlertTypes _type, ILR350Tag _tag, Dictionary<string, object> _parameters )
        {
            try
            {
                Log.Debug( string.Format( "{0} {1}", _type, _tag.SerialLabel ) );

                var beaconMessage = m_BeaconMessageParser.ParseBeaconMessage( _tag.BeaconMessage );
                if (null != beaconMessage)
                {
                    var message = new TagAlertMessage
                    {
                        AlertClass = AlertClasses.Tags,
                        Driver = Config.Name,
                        DeviceID = m_ILR350Reader.SerialNumber,
                        ReadTime = _tag.TimeLastSeen,
                        TagID = _tag.SerialLabel.Replace(".", ""),
                        BatteryVoltage = beaconMessage.BatteryVoltage
                    };
                    if (null != _parameters)
                    {
                        message.Parameters = _parameters;
                    }

                    SendAlert(message, _type, string.Format("", message.DeviceID, message.ReadTime), true);
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "IdentecDev:: SendDetachMessage", exp );
            }
        }

        private static bool AnglesEqual(Jaxis.Readers.Identec.BeaconMessages.PourMessage _oldPour, Jaxis.Readers.Identec.BeaconMessages.PourMessage _beaconMessage)
        {
            bool rc = false;

            if( _oldPour.PourDurationZoneA == _beaconMessage.PourDurationZoneA &&
                _oldPour.PourDurationZoneB == _beaconMessage.PourDurationZoneB &&
                _oldPour.PourDurationZoneC == _beaconMessage.PourDurationZoneC &&
                _oldPour.PourDurationZoneD == _beaconMessage.PourDurationZoneD &&
                _oldPour.PourDurationZoneE == _beaconMessage.PourDurationZoneE &&
                _oldPour.PourDurationZoneF == _beaconMessage.PourDurationZoneF &&
                _oldPour.PourDurationZoneG == _beaconMessage.PourDurationZoneG &&
                _oldPour.PourDurationZoneH == _beaconMessage.PourDurationZoneH &&
                _oldPour.PourDurationZoneI == _beaconMessage.PourDurationZoneI &&
                _oldPour.PourDurationZoneJ == _beaconMessage.PourDurationZoneJ &&
                _oldPour.PourDurationZoneK == _beaconMessage.PourDurationZoneK &&
                _oldPour.PourDurationZoneL == _beaconMessage.PourDurationZoneL )
            {
                rc = true;
            }

            return rc;
        }

        private bool FilterPour(ILR350Tag _tag, Jaxis.Readers.Identec.BeaconMessages.PourMessage _beaconMessage)
        {
            bool rc = false;
            try
            {
                if( null != _tag && null != _beaconMessage )
                {
                    Jaxis.Readers.Identec.BeaconMessages.PourMessage oldPour;
                    m_OldPours.TryGetValue( _tag.SerialNumber, out oldPour );
                    if( null != oldPour )
                    {
                        if( 1 == _beaconMessage.PourCount && 1 == oldPour.PourCount && AnglesEqual( oldPour, _beaconMessage ) )
                        {
                            rc = true;
                                // if the tag is removed and reattached after each pour all pours because pour will always == 1
                        }
                        else if( _beaconMessage.PourCount == oldPour.PourCount )
                        {
                            rc = true;
                        }
                        else if( _beaconMessage.PourCount != oldPour.PourCount + 1 )
                        {
                            Log.Warn( string.Format( "Pour Missed  Old {0} New {1}", _beaconMessage.PourCount, oldPour.PourCount ) );
                            int delta = _beaconMessage.PourCount - oldPour.PourCount;

                            var parameters = new Dictionary<string, object>
                            {
                                {"OldPour", oldPour.PourCount},
                                {"CurrentPour", _beaconMessage.PourCount},
                                {"Delta", delta}
                            };
                            SendAlertMessage( AlertTypes.MissedPour, _tag, parameters );
                        }
                    }

                    m_OldPours[_tag.SerialNumber] = _beaconMessage;
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "IdentecDev:: FilterMsg", exp );
            }
            return rc;
        }

        private bool FilterMsg( ILR350Tag _tag )
        {
            bool rc = false;
            try
            {
                if( null != _tag )
                {
                    Int16 oldCount;
                    m_OldMsgs.TryGetValue( _tag.SerialNumber, out oldCount );
                    var newCount = (Int16)_tag.BeaconCounterHighByte;
                    newCount <<= 8;
                    newCount += (Int16)_tag.BeaconCounterLowByte;
                    if( 0 != oldCount )
                    {
                        if( newCount == oldCount )
                        {
                            rc = true;
                        }
#warning not sending alerts for all msg, only pours
                        //else if (NewCount != OldCount + 1)
                        //    SendAlertMessage(AlertTypes.MissedMsg, _Tag, NewCount - OldCount);
                    }
                    m_OldMsgs[_tag.SerialNumber] = newCount;
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "IdentecDev:: FilterMsg", exp );
            }
            return rc;
        }

        private void SendMessage( ILR350Tag _tag )
        {
            try
            {
                if( _tag != null )
                {
                    //Log.Debug(string.Format("TagID {0} Signal Strength {1}", _tag.SerialNumber, _tag.LastSignal));
                    BeaconMessage beaconMessage = m_BeaconMessageParser.ParseBeaconMessage(_tag.BeaconMessage);
                    if (null != beaconMessage)
                    { 
                        switch( beaconMessage.EventType )
                        {
                            case 1:
                            case 2:
                            {
                                if( m_AllowHeartbeat )
                                {
                                    SendInventoryMessage( _tag, beaconMessage as InventoryMessage );
                                }
                                break;
                            }
                            case 3:
                            {
                                SendAttachMessage( _tag, beaconMessage as AttachMessage );
                                Log.Debug( ATTACH_LOGGER, string.Format( "TagID {0} Signal Strength {1}", _tag.SerialNumber, _tag.LastSignal ) );

                                break;
                            }
                            case 4:
                            {
                                SendDetachMessage( _tag, beaconMessage as AttachMessage );
                                break;
                            }
                            case 5:
                            {
                                if( m_AllowPours )
                                {
                                    SendPourMessage(_tag, beaconMessage as Jaxis.Readers.Identec.BeaconMessages.PourMessage);
                                }
                                break;
                            }
                            case 6:
                            {
                                SendDormantMessage( _tag, beaconMessage as DormantMessage );
                                break;
                            }
                        }
                    }
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( string.Format( "IdentecDev:: SendMessage( ) TagID: {0}", ( null !=_tag ) ? _tag.SerialLabel : "Unknown" ), exp );
            }
        }

        protected PhaseMessage BuildPhaseMessage( ILR350Tag _tag, BeaconMessage _beaconMessage, TagPhaseType _type )
        {
            var message = new PhaseMessage
            {
                Driver = Config.Name,
                BatteryVoltage = _beaconMessage.BatteryVoltage,
                EventType = _type,
                DeviceID = ,
                SignalStrength = _tag.LastSignal,
                ReadTime = _tag.TimeLastSeen,
                TagID = _tag.SerialLabel.Replace(".", ""),
                Temperature = _beaconMessage.Temperature
            };

            message.BatteryVoltage = _beaconMessage.BatteryVoltage;
            message.RawData = _tag.BeaconMessage;

            return message;
        }

        private void SendInventoryMessage( ILR350Tag _tag, InventoryMessage _beaconMessage )
        {
            try
            {
                PhaseMessage message = BuildPhaseMessage( _tag, _beaconMessage, TagPhaseType.Heartbeat );
                if( 2 == _beaconMessage.EventType )
                {
                    message.EventType = TagPhaseType.HeartbeatDetached;
                }
                QueueMessage(MessagePriority.Heartbeat, message);
                //ProduceMessage(message);
            }
            catch( Exception exp )
            {
                Log.WriteException( "IdentecDev:: SendInventoryMessage", exp );
            }
        }

        private void SendAttachMessage( ILR350Tag _tag, AttachMessage _beaconMessage )
        {
            try
            {
                PhaseMessage message = BuildPhaseMessage( _tag, _beaconMessage, TagPhaseType.Connect );
                QueueMessage(MessagePriority.Connection, message);
                //ProduceMessage(message);

                if( 0.00 > _beaconMessage.XGforce || -0.10 > _beaconMessage.YGforce )
                {
                    SendAlertMessage(AlertTypes.BadBottleAttach, _tag, null);
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "IdentecDev:: SendAttachMessage", exp );
            }
        }

        private void SendDetachMessage( ILR350Tag _tag, AttachMessage _beaconMessage )
        {
            try
            {
                PhaseMessage message = BuildPhaseMessage( _tag, _beaconMessage, TagPhaseType.Disconnect );
                QueueMessage(MessagePriority.Connection, message);
                //ProduceMessage(message);
            }
            catch( Exception exp )
            {
                Log.WriteException( "IdentecDev:: SendDetachMessage", exp );
            }
        }

        private void SendDormantMessage( ILR350Tag _tag, DormantMessage _beaconMessage )
        {
            try
            {
                PhaseMessage message = BuildPhaseMessage( _tag, _beaconMessage, TagPhaseType.Dormant );
                QueueMessage(MessagePriority.Connection, message);
                //ProduceMessage(message);
            }
            catch( Exception exp )
            {
                Log.WriteException( "IdentecDev:: SendDormantMessage", exp );
            }
        }

        private void SendPourMessage(ILR350Tag _tag, Jaxis.Readers.Identec.BeaconMessages.PourMessage _beaconMessage)
        {
            try
            {
                var identecPour = new IdentecPour { Driver = Config.Name };

                Log.Debug( string.Format( "SendPourMessage:: DeviceID {2}  TagID {1}  -  Pour Count: {0}", _beaconMessage.PourCount, _tag.SerialLabel, m_ILR350Reader.SerialNumber ) );
                if( false == FilterPour( _tag, _beaconMessage ) )
                {
                    identecPour.Angles[0].Duration = TimeSpan.FromSeconds( _beaconMessage.PourDurationZoneA );
                    identecPour.Angles[1].Duration = TimeSpan.FromSeconds( _beaconMessage.PourDurationZoneB );
                    identecPour.Angles[2].Duration = TimeSpan.FromSeconds( _beaconMessage.PourDurationZoneC );
                    identecPour.Angles[3].Duration = TimeSpan.FromSeconds( _beaconMessage.PourDurationZoneD );
                    identecPour.Angles[4].Duration = TimeSpan.FromSeconds( _beaconMessage.PourDurationZoneE );
                    identecPour.Angles[5].Duration = TimeSpan.FromSeconds( _beaconMessage.PourDurationZoneF );
                    identecPour.Angles[6].Duration = TimeSpan.FromSeconds( _beaconMessage.PourDurationZoneG );
                    identecPour.Angles[7].Duration = TimeSpan.FromSeconds( _beaconMessage.PourDurationZoneH );
                    identecPour.Angles[8].Duration = TimeSpan.FromSeconds( _beaconMessage.PourDurationZoneI );
                    identecPour.Angles[9].Duration = TimeSpan.FromSeconds( _beaconMessage.PourDurationZoneJ );
                    identecPour.Angles[10].Duration = TimeSpan.FromSeconds( _beaconMessage.PourDurationZoneK );
                    identecPour.Angles[11].Duration = TimeSpan.FromSeconds( _beaconMessage.PourDurationZoneL );

                    identecPour.TagID = _tag.SerialLabel.Replace( ".", "" );
                    identecPour.DeviceID = m_ILR350Reader.SerialNumber;
                    identecPour.SignalStrength = _tag.LastSignal;
                    identecPour.Temperature = _beaconMessage.Temperature;
                    identecPour.BatteryVoltage = _beaconMessage.BatteryVoltage;
                    identecPour.ReadTime = _tag.TimeLastSeen;
                    identecPour.RawData = _tag.BeaconMessage;
                    identecPour.PourCount = _beaconMessage.PourCount;

                    Log.Debug( identecPour.ToString( ) );

                    QueueMessage(MessagePriority.Pour, identecPour);
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "IdentecDev:: SendPourMessage", exp );
            }
        }
    }
}