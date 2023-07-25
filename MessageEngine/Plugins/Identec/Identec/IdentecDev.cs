using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BeverageMetrics.BeaconParser;
using BeverageMetrics.BeaconParser.BeaconMessages;
using IDENTEC;
using IDENTEC.ILR350;
using IDENTEC.ILR350.Readers;
using IDENTEC.ILR350.Tags;
using Jaxis.Engine.Base.Device;
using Jaxis.Interfaces;
using Jaxis.Interfaces.Tags;
using Jaxis.MessageLibrary;
using Jaxis.Util.Log4Net;

//using ClientBevMetData;

namespace Jaxis.Readers.Identec
{

    /*
    <DeviceConfig>
      <AssemblyName>Identec.dll</AssemblyName>
      <AssemblyType>Jaxis.Readers.Identec.IdentecDev</AssemblyType>
       <AssemblyVersion>1.0</AssemblyVersion>
       <ID>321</ID>
         <Name>Identec Device Prod</Name>
         <Type>DataProducer</Type>
         <State>Started</State>
         <ConsumerMessageType>None</ConsumerMessageType>
       <Options>
         <string>192.168.1.101</string> <!-- IP Address or COM Port -->
         <string></string> <!-- Timeout -->
         <string></string> <!-- Frequency -->
         <string></string> <!-- RFBaudRate -->
         <string></string> <!-- AllowHeartbeat -->
         <string></string> <!-- AllowPours -->
       </Options>
     </DeviceConfig>
    */

    public class IdentecDev : BaseProducerDevice, IProducer
    {
        [Obfuscation(Exclude = true)]
        public static DeviceConfig GetDefaultDeviceConfig()
        {
            DeviceConfig rc = new DeviceConfig();
            System.Reflection.Assembly Asm = System.Reflection.Assembly.GetExecutingAssembly();
            rc.AssemblyType = MethodBase.GetCurrentMethod().DeclaringType.FullName;
            rc.AssemblyName = Asm.ManifestModule.Name;
            rc.AssemblyVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(BaseDevice.GetPath() + "\\" + rc.AssemblyName).FileVersion;
            rc.ID = Guid.NewGuid().ToString();
            rc.Name = "Identec Device Prod";
            rc.Type = DeviceType.DataProducerConsumer;
            rc.State = DeviceState.Stopped;
            rc.ProducerMessageType = 0;
            rc.ConsumerMessageType = 1;

            rc.Options.Add(new DeviceConfigOption { Name="IP Address/COM Port", Value = "192.168.1.101"});
            rc.Options.Add(new DeviceConfigOption { Name = "Timeout", Value = "" });
            rc.Options.Add(new DeviceConfigOption { Name = "Frequency", Value = "" });
            rc.Options.Add(new DeviceConfigOption { Name = "RFBaudRate", Value = "" });
            rc.Options.Add(new DeviceConfigOption { Name = "Allow Heartbeat", Value = "true" });
            rc.Options.Add(new DeviceConfigOption { Name = "Allow Pours", Value = "true" });
            rc.Options.Add(new DeviceConfigOption { Name = "Event ID", Value = "0000" });

            return rc;
        }

        private Dictionary<uint, Int16> m_OldMsgs = new Dictionary<uint, Int16>( );
        private Dictionary<uint, BeverageMetrics.BeaconParser.BeaconMessages.PourMessage> m_OldPours = new Dictionary<uint, BeverageMetrics.BeaconParser.BeaconMessages.PourMessage>( );
        private ILR350Reader m_ILR350Reader = null;
        private Gen3Manager m_Gen3Manager = new Gen3Manager( );
        private BeaconMessageParser m_BeaconMessageParser = new BeaconMessageParser( );
        //private Jaxis.MessageLibrary.IdentecPour m_IdentecPour = null;
        private int m_Timeout = 5 * 60 * 60; // 5 minutes default, in Sec

        protected bool AllowHeartbeat = true;
        protected bool AllowPours = true;

        private System.Threading.Thread m_Worker;

        public IdentecDev( )
            : this(GetDefaultDeviceConfig())
        {
        }

        public IdentecDev( IDeviceConfig _Config )
            : base( _Config )
        {
            try
            {
                //Config.ConsumerMessageType = MessageType.None;
                Config.Type = DeviceType.DataProducer;
                State = Config.State;

                //m_IdentecPour = new Jaxis.MessageLibrary.IdentecPour { Driver = Config.Name };

                m_ILR350Reader = null;
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
                Config.State = DeviceState.Started;
                m_Stop = false;
                m_Worker = new System.Threading.Thread( ProcessTags );
                m_Worker.Start( );

               // StatusMonitor = MonitorStatus;
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
                if( 6 < ( DateTime.Now - m_LastMessage ).Minutes )
                {
                    State = DeviceState.NotReporting;
                }
                else
                {
                    State = DeviceState.Running;
                }
            }
        }

        private void ConfigureReader( )
        {
            try
            {
                if( Config.Options.Count != 0 )
                {
                    // Option 0 == IP Address or Com Port
                    string Address = Config.GetAddress();
                    if( Address.Contains( '.' ) )
                    {
                        m_Gen3Manager.DiscoverDevicesEth( Address );
                        m_ILR350Reader = m_Gen3Manager.SelectedDevice;
                    }
                    else
                    {
                        m_Gen3Manager.DiscoverDevices( Address );
                        m_ILR350Reader = m_Gen3Manager.SelectedDevice;
                    }

                    if( null != m_ILR350Reader )
                    {
                        m_Timeout = Config.GetTimeout( );
                        m_ILR350Reader.SetFrequency( Config.GetFrequency( ) );
                        m_ILR350Reader.SetRFBeaconBaudrate( Config.GetBaudRate( ) );

                        AllowPours = Config.GetPublishPours( );
                        AllowHeartbeat = Config.GetPublishHeartbeats( );

                        HardwareID = m_ILR350Reader.SerialNumber;
                    }
                    else
                    {
                        Log.Debug( "m_ILR350Reader is null" );
                        SendDeviceIssueMessage( AlertTypes.CannotConnect );
                    }
                }
                else
                {
                    Log.Debug( "No Options" );
                }
            }
            catch( Exception err )
            {
                Log.WriteException( "IdentecDev::ConfigureReader", err );
            }
        }

        override public void Stop( )
        {
            try
            {
                State = DeviceState.Stopped;
                Config.State = DeviceState.Stopped;
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

        public void ProcessTags( )
        {
            int TimeFromLastMsg = 0;
            try
            {
                while( !m_Stop )
                {
                    try
                    {
                        if( m_ILR350Reader != null )
                        {
                            ILR350TagCollection beaconTags = m_ILR350Reader.GetBeaconTags( );

                            //m_ILR350Reader.Status
                            foreach( ILR350Tag Tag in beaconTags )
                            {
                                TimeFromLastMsg = 0;
                                if( false == FilterMsg( Tag ) )
                                {
                                    SendMessage( Tag );
                                }
                            }

                            if( 0 == beaconTags.Count )
                            {
                                TimeFromLastMsg += 1;
                                if( TimeFromLastMsg >= m_Timeout )
                                {
                                    SendDeviceIssueMessage( AlertTypes.NotReadingTags );
                                    TimeFromLastMsg = 0;
                                }
                            }
                            else
                            {
                                m_LastMessage = DateTime.Now;
                            }
                        }
                        else
                        {
                            ConfigureReader( );
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

        private void SendDeviceIssueMessage( AlertTypes _Type )
        {
            try
            {
                DeviceAlertMessage Message = new DeviceAlertMessage( ) { Driver = Config.Name, DeviceID = Config.ID};
                if( null != m_ILR350Reader )
                    Message.DeviceID = m_ILR350Reader.SerialNumber;
                Message.AlertType = _Type;
                Message.ReadTime = DateTime.Now;

                Log.Warn(string.Format("IdentecDev::SendDeviceIssueMessage({0})", _Type.ToString()));
                ProduceMessage( Message );
            }
            catch( Exception exp )
            {
                Log.WriteException( "IdentecDev:: SendDetachMessage", exp );
            }
        }


        private void SendAlertMessage( AlertTypes _Type, ILR350Tag _Tag, Dictionary<string, object> _Parameters )
        {
            try
            {
                Log.Debug( string.Format( "{0} {1}", _Type, _Tag.SerialLabel ) );

                BeaconMessage BeaconMessage = m_BeaconMessageParser.ParseBeaconMessage( _Tag.BeaconMessage );
                TagAlertMessage Message = new TagAlertMessage( ) { Driver = Config.Name };
                Message.DeviceID = m_ILR350Reader.SerialNumber;
                Message.ReadTime = _Tag.TimeLastSeen;
                Message.TagID = _Tag.SerialLabel.Replace( ".", "" );
                Message.BatteryVoltage = BeaconMessage.BatteryVoltage;
                if( null != _Parameters )
                {
                    Message.Parameters = _Parameters;
                }
                //Message.Type = MessageType.RawData;
                Message.AlertType = _Type;

                ProduceMessage( Message );
            }
            catch( Exception exp )
            {
                Log.WriteException( "IdentecDev:: SendDetachMessage", exp );
            }
        }

        private bool AnglesEqual( BeverageMetrics.BeaconParser.BeaconMessages.PourMessage _OldPour, BeverageMetrics.BeaconParser.BeaconMessages.PourMessage _BeaconMessage )
        {
            bool rc = false;

            if( _OldPour.PourDurationZoneA == _BeaconMessage.PourDurationZoneA &&
                _OldPour.PourDurationZoneB == _BeaconMessage.PourDurationZoneB &&
                _OldPour.PourDurationZoneC == _BeaconMessage.PourDurationZoneC &&
                _OldPour.PourDurationZoneD == _BeaconMessage.PourDurationZoneD &&
                _OldPour.PourDurationZoneE == _BeaconMessage.PourDurationZoneE &&
                _OldPour.PourDurationZoneF == _BeaconMessage.PourDurationZoneF &&
                _OldPour.PourDurationZoneG == _BeaconMessage.PourDurationZoneG &&
                _OldPour.PourDurationZoneH == _BeaconMessage.PourDurationZoneH &&
                _OldPour.PourDurationZoneI == _BeaconMessage.PourDurationZoneI &&
                _OldPour.PourDurationZoneJ == _BeaconMessage.PourDurationZoneJ &&
                _OldPour.PourDurationZoneK == _BeaconMessage.PourDurationZoneK &&
                _OldPour.PourDurationZoneL == _BeaconMessage.PourDurationZoneL )
            {
                rc = true;
            }

            return rc;
        }

        private bool FilterPour( ILR350Tag _Tag, BeverageMetrics.BeaconParser.BeaconMessages.PourMessage _BeaconMessage )
        {
            bool rc = false;
            try
            {
                if( null != _Tag && null != _BeaconMessage )
                {
                    BeverageMetrics.BeaconParser.BeaconMessages.PourMessage OldPour = null;
                    m_OldPours.TryGetValue( _Tag.SerialNumber, out OldPour );
                    if( null != OldPour )
                    {
                        if( 1 == _BeaconMessage.PourCount && 1 == OldPour.PourCount && true == AnglesEqual( OldPour, _BeaconMessage ) )
                        {
                            rc = true;
                                // if the tag is removed and reattached after each pour all pours because pour will always == 1
                        }
                        else if( _BeaconMessage.PourCount == OldPour.PourCount )
                        {
                            rc = true;
                        }
                        else if( _BeaconMessage.PourCount != OldPour.PourCount + 1 )
                        {
                            Log.Warn( string.Format( "Pour Missed  Old {0} New {1}", _BeaconMessage.PourCount, OldPour.PourCount ) );
                            int delta = (int)_BeaconMessage.PourCount - (int)OldPour.PourCount;

                            Dictionary<string, object> Parameters = new Dictionary<string, object>( );
                            Parameters.Add( "OldPour", OldPour.PourCount );
                            Parameters.Add( "CurrentPour", _BeaconMessage.PourCount );
                            Parameters.Add( "Delta", delta );
                            SendAlertMessage( AlertTypes.MissedPour, _Tag, Parameters );
                        }
                    }

                    m_OldPours[_Tag.SerialNumber] = _BeaconMessage;
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "IdentecDev:: FilterMsg", exp );
            }
            return rc;
        }

        private bool FilterMsg( ILR350Tag _Tag )
        {
            bool rc = false;
            try
            {
                if( null != _Tag )
                {
                    Int16 OldCount = 0;
                    m_OldMsgs.TryGetValue( _Tag.SerialNumber, out OldCount );
                    Int16 NewCount = (Int16)_Tag.BeaconCounterHighByte;
                    NewCount <<= 8;
                    NewCount += (Int16)_Tag.BeaconCounterLowByte;
                    if( 0 != OldCount )
                    {
                        if( NewCount == OldCount )
                        {
                            rc = true;
                        }
#warning not sending alerts for all msg, only pours
                        //else if (NewCount != OldCount + 1)
                        //    SendAlertMessage(AlertTypes.MissedMsg, _Tag, NewCount - OldCount);
                    }
                    m_OldMsgs[_Tag.SerialNumber] = NewCount;
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "IdentecDev:: FilterMsg", exp );
            }
            return rc;
        }

        private void SendMessage( ILR350Tag _Tag )
        {
            try
            {
                if( _Tag != null )
                {
                    //Log.Debug( string.Format( "TagID: {0}", _Tag.SerialLabel ) );
                    BeaconMessage BeaconMessage = m_BeaconMessageParser.ParseBeaconMessage( _Tag.BeaconMessage );
                    //Log.Debug( string.Format( "IdentecDev::SendMessage {0}, {1} {2}", _Tag.SerialLabel.Replace( ".", "" ), BeaconMessage.GetType( ).ToString( ), _Tag.TimeLastSeen ) );

                    switch( BeaconMessage.EventType )
                    {
                        case 1:
                        case 2:
                        {
                            if( AllowHeartbeat )
                            {
                                SendInventoryMessage( _Tag, BeaconMessage as InventoryMessage );
                            }
                            break;
                        }
                        case 3:
                        {
                            SendAttachMessage( _Tag, BeaconMessage as AttachMessage );
                            break;
                        }
                        case 4:
                        {
                            SendDetachMessage( _Tag, BeaconMessage as AttachMessage );
                            break;
                        }
                        case 5:
                        {
                            if( AllowPours )
                            {
                                SendPourMessage( _Tag, BeaconMessage as BeverageMetrics.BeaconParser.BeaconMessages.PourMessage );
                            }
                            break;
                        }
                        case 6:
                        {
                            SendDormantMessage( _Tag, BeaconMessage as DormantMessage );
                            break;
                        }
                    }
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( string.Format( "IdentecDev:: SendMessage( ) TagID: {0}", _Tag.SerialLabel ), exp );
            }
        }

        protected PhaseMessage BuildPhaseMessage( ILR350Tag _Tag, BeaconMessage _BeaconMessage, TagPhaseType _Type )
        {
            //Log.Debug( string.Format( "{2} - DeviceID {0} TagID {1} - Last Seen {3}", m_ILR350Reader.SerialNumber, _Tag.SerialLabel, _Type.ToString( ), _Tag.TimeLastSeen ) );

            PhaseMessage Message = new PhaseMessage( ) { Driver = Config.Name };

            Message.BatteryVoltage = _BeaconMessage.BatteryVoltage;
            Message.EventType = _Type;

            Message.DeviceID = m_ILR350Reader.SerialNumber;
            Message.SignalStrength = _Tag.LastSignal;
            Message.ReadTime = _Tag.TimeLastSeen;
            Message.TagID = _Tag.SerialLabel.Replace( ".", "" );
            Message.Temperature = _BeaconMessage.Temperature;
            Message.BatteryVoltage = _BeaconMessage.BatteryVoltage;
            Message.RawData = _Tag.BeaconMessage;

            return Message;
        }

        private void SendInventoryMessage( ILR350Tag _Tag, InventoryMessage _BeaconMessage )
        {
            try
            {
                PhaseMessage Message = BuildPhaseMessage( _Tag, _BeaconMessage, TagPhaseType.Heartbeat );
                if( 2 == _BeaconMessage.EventType )
                {
                    Message.EventType = TagPhaseType.HeartbeatDetached;
                }
                ProduceMessage( Message );
            }
            catch( Exception exp )
            {
                Log.WriteException( "IdentecDev:: SendInventoryMessage", exp );
            }
        }

        private void SendAttachMessage( ILR350Tag _Tag, AttachMessage _BeaconMessage )
        {
            try
            {
                PhaseMessage Message = BuildPhaseMessage( _Tag, _BeaconMessage, TagPhaseType.Connect );
                ProduceMessage( Message );

                if( 0.00 > _BeaconMessage.XGforce || -0.10 > _BeaconMessage.YGforce )
                {
                    SendAlertMessage( AlertTypes.BadBottleAttach, _Tag, null );
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "IdentecDev:: SendAttachMessage", exp );
            }
        }

        private void SendDetachMessage( ILR350Tag _Tag, AttachMessage _BeaconMessage )
        {
            try
            {
                PhaseMessage Message = BuildPhaseMessage( _Tag, _BeaconMessage, TagPhaseType.Disconnect );
                ProduceMessage( Message );
            }
            catch( Exception exp )
            {
                Log.WriteException( "IdentecDev:: SendDetachMessage", exp );
            }
        }

        private void SendDormantMessage( ILR350Tag _Tag, DormantMessage _BeaconMessage )
        {
            try
            {
                PhaseMessage Message = BuildPhaseMessage( _Tag, _BeaconMessage, TagPhaseType.Dormant );
                ProduceMessage( Message );
            }
            catch( Exception exp )
            {
                Log.WriteException( "IdentecDev:: SendDormantMessage", exp );
            }
        }

        private void SendPourMessage( ILR350Tag _Tag, BeverageMetrics.BeaconParser.BeaconMessages.PourMessage _BeaconMessage )
        {
            try
            {
                var message = new Jaxis.MessageLibrary.IdentecPour( ) { Driver = Config.Name };

                Log.Debug( string.Format( "SendPourMessage:: DeviceID {2}  TagID {1}  -  Pour Count: {0}", _BeaconMessage.PourCount, _Tag.SerialLabel, m_ILR350Reader.SerialNumber ) );
                if( null != message && false == FilterPour( _Tag, _BeaconMessage ) )
                {
                    message.Angles[0].Duration = TimeSpan.FromSeconds( _BeaconMessage.PourDurationZoneA );
                    message.Angles[1].Duration = TimeSpan.FromSeconds( _BeaconMessage.PourDurationZoneB );
                    message.Angles[2].Duration = TimeSpan.FromSeconds( _BeaconMessage.PourDurationZoneC );
                    message.Angles[3].Duration = TimeSpan.FromSeconds( _BeaconMessage.PourDurationZoneD );
                    message.Angles[4].Duration = TimeSpan.FromSeconds( _BeaconMessage.PourDurationZoneE );
                    message.Angles[5].Duration = TimeSpan.FromSeconds( _BeaconMessage.PourDurationZoneF );
                    message.Angles[6].Duration = TimeSpan.FromSeconds( _BeaconMessage.PourDurationZoneG );
                    message.Angles[7].Duration = TimeSpan.FromSeconds( _BeaconMessage.PourDurationZoneH );
                    message.Angles[8].Duration = TimeSpan.FromSeconds( _BeaconMessage.PourDurationZoneI );
                    message.Angles[9].Duration = TimeSpan.FromSeconds( _BeaconMessage.PourDurationZoneJ );
                    message.Angles[10].Duration = TimeSpan.FromSeconds( _BeaconMessage.PourDurationZoneK );
                    message.Angles[11].Duration = TimeSpan.FromSeconds( _BeaconMessage.PourDurationZoneL );

                    message.TagID = _Tag.SerialLabel.Replace( ".", "" );
                    message.DeviceID = m_ILR350Reader.SerialNumber;
                    message.SignalStrength = _Tag.LastSignal;
                    message.Temperature = _BeaconMessage.Temperature;
                    message.BatteryVoltage = _BeaconMessage.BatteryVoltage;
                    message.ReadTime = _Tag.TimeLastSeen;
                    message.RawData = _Tag.BeaconMessage;
                    message.PourCount = _BeaconMessage.PourCount;

                    Log.Debug( message.ToString( ) );

                    ProduceMessage( message );
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "IdentecDev:: SendPourMessage", exp );
            }
        }
    }
}