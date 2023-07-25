using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Jaxis.Engine.Base.Device;
using Jaxis.Interfaces;
using Jaxis.Interfaces.Tags;
using Jaxis.MessageLibrary;
using Jaxis.MessageLibrary.Generic;
using Jaxis.Util.Log4Net;
using PureRF;

namespace Jaxis.Readers.PureRFID
{
    /*
    <DeviceConfig>
        <AssemblyName>PureRFID.dll</AssemblyName>
        <AssemblyType>Jaxis.Readers.PureRFID.PureRFReader</AssemblyType>
        <AssemblyVersion>1.0</AssemblyVersion>
        <ID>123</ID>
        <Name>PureRFID Device Prod</Name>
        <Type>DataProducer</Type>
        <State>Started</State>
        <ConsumerMessageType>None</ConsumerMessageType>
        <ProducerMessageType>RawData</ProducerMessageType>
        <Options>
            <string>1</string> <!-- Sleep time -->
            <string>COM15</string> <!-- Com or IP -->
            <string>1</string> <!-- Device ID's (comma separated list of int's) -->
        </Options>
    </DeviceConfig>
    */


    public class PureRFReader : BaseProducerDevice, IProducer
    {
        [Obfuscation(Exclude = true)]
        public static DeviceConfig GetDefaultDeviceConfig()
        {
            DeviceConfig rc = new DeviceConfig();
            System.Reflection.Assembly Asm = System.Reflection.Assembly.GetExecutingAssembly();
            rc.AssemblyName = Asm.ManifestModule.Name;
            rc.AssemblyType = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName;
            rc.AssemblyVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(BaseDevice.GetPath() + "\\" + rc.AssemblyName).FileVersion;
            rc.ID = Guid.NewGuid().ToString();
            rc.Name = "PureRFID Device Prod";
            rc.Type = DeviceType.DataProducer;
            rc.State = DeviceState.Stopped;
            rc.ProducerMessageType = 8;
            rc.ConsumerMessageType = 0;
            DeviceConfigOption Option1 = new DeviceConfigOption();
            Option1.Name = "SleepTime";
            Option1.Value = "1";
            rc.Options.Add(Option1);
            DeviceConfigOption Option2 = new DeviceConfigOption();
            Option2.Name = "Com or IP";
            Option2.Value = "COM15";
            rc.Options.Add(Option2);
            DeviceConfigOption Option3 = new DeviceConfigOption();
            Option3.Name = "Device ID's";
            Option3.Value = "1";
            rc.Options.Add(Option3);
            return rc;
        }

        protected System.Threading.Thread m_Worker = null;
        protected PureRF.ReceiversManager m_Manager = null;
        List<string> m_Receivers = new List<string>( );

        
        public PureRFReader( )
            : this(GetDefaultDeviceConfig())
        {
        }

        public PureRFReader( IDeviceConfig _Config )
            : base( _Config )
        {
        }

        public override void Stop( )
        {
            Log.Wrap<int>( "PureRF::Stop", LogType.Debug, true, ( ) =>
            {
                m_Stop = true;

                m_Manager.ClearReceivers( );
                return 1;
            } );
        }

        public override void Start( )
        {
            Log.Wrap<int>( "PureRF::Start", LogType.Debug, true, ( ) =>
            {
                m_Manager = new PureRF.ReceiversManager( );

                string LoopName = string.Empty;
                if( string.IsNullOrWhiteSpace( m_DeviceConfig.GetIPAddress( ) ) )
                {
                    LoopName = m_DeviceConfig.GetPort( );
                    m_Manager.AddSerialLoop( LoopName, LoopName, 0xe100 );
                }
                else
                {
                    m_Manager.AddIPLoop( m_DeviceConfig.GetIPAddress( ), m_DeviceConfig.GetIPAddress( ), Convert.ToInt32( m_DeviceConfig.GetPort( ) ) );
                }

                m_Manager.SetEventCallback( new ReceiversManager.EventCallback( this.ReceiversManagerEvent ) );

                foreach( int UnitID in m_DeviceConfig.GetDeviceList( ) )
                {
                    string Receiver = string.Format( "{0}-{1}", LoopName, UnitID );
                    m_Manager.AddReceiver( Receiver, LoopName, (byte)UnitID );
                    m_Receivers.Add( Receiver );
                }

                m_Worker = new System.Threading.Thread( StartThread );
                m_Worker.Start( );

                m_Stop = false;
                State = DeviceState.Started;
                Config.State = DeviceState.Started;

                return 1;
            } );
        }

        protected void StartThread( )
        {
            ReceiversManager.RetVal allResults = m_Manager.GetAllTags( m_Receivers.ToArray( ), true );
        }

        private void ReceiversManagerEvent( ReceiversManager m, ReceiversManager.ProgressEvent e )
        {
            switch( e.EventID )
            {
                case ReceiversManager.ProgressEvent.ID.COMPLETED:
                {
                    this.RequestCompleted( e );
                    if( false == m_Stop )
                    {
                        System.Threading.Thread.Sleep( m_DeviceConfig.GetPollTime( ) );
                        m_Manager.GetAllTags( m_Receivers.ToArray( ), true );
                    }
                    break;
                }
                default:
                {
                    break;
                }
            }
        }

        private void RequestCompleted( ReceiversManager.ProgressEvent e )
        {
            ReceiversManager.ResultSet set;
            ReceiversManager.RetVal allResults = m_Manager.GetAllResults( out set );
            if( allResults == ReceiversManager.RetVal.SUCCESS )
            {
                foreach( ReceiversManager.ManagedReceiver receiver in set.Keys )
                {
                    ReceiversManager.ReceiverResult result = set[receiver];
                    Receiver.Tag[] tagArray = (Receiver.Tag[])result.Result;
                    if( result.RetVal == ReceiverRetVal.SUCCESS )
                    {
                        foreach( PureRF.Receiver.Tag tag in tagArray )
                        {
                            TagRead Message = null;
                            if( Receiver.TagMsg.Tamper == ( tag.tagMsg & Receiver.TagMsg.Tamper ) )
                            {
                                TagButtonPress Tag = new TagButtonPress( );
                                Tag.ButtonType = ButtonTypes.Primary;
                                Message = Tag;
                            }
                            else if( Receiver.TagMsg.LowBattery == ( tag.tagMsg & Receiver.TagMsg.LowBattery ) )
                            {
                                TagAlertMessage Tag = new TagAlertMessage( );
                                Tag.AlertType = AlertTypes.LowBattery;
                                ProduceMessage( Tag );
                            }
                            else
                            {
                                Message = new Jaxis.MessageLibrary.TagRead( );
                            }

                            Message.Driver = this.m_DeviceConfig.AssemblyName;
                            Message.DeviceID = receiver.Name;
                            Message.TagID = tag.tagID.GetPureRFTagID( ).ToString( );
                            Message.SignalStrength = tag.RSSI;
                            Message.ReadTime = DateTime.Today.AddHours(tag.ts.hour).AddMinutes(tag.ts.min).AddSeconds(tag.ts.sec).
                                AddMilliseconds(tag.ts.msec);

                            ProduceMessage( Message );
                        }
                    }
                }
            }
        }
    }
}