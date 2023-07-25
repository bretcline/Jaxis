using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JaxisInterfaces;
using JaxisEngine;
using System.Threading;
using IDENTEC;
using IDENTEC.Readers.BeaconReaders;
using IDENTEC.Readers;
using IDENTEC.Tags;
using IDENTEC.Tags.BeaconTags;
using JaxisEngine.Base;
using Jaxis.Util.Log4Net;

namespace Jaxis.Plugin.Identec
{
    public class iPortMBPlugin : BaseDevice, IDevice
    {
        private System.Threading.Thread m_Worker;
        public event ProduceHandler Produce;

        public iPortMBPlugin( IDeviceConfig _Config )
            : base( _Config )
        {
            Config.ConsumerMessageType = MessageType.RawData;
            Config.Type = DeviceType.DataProducer;
            State = DeviceState.Stopped;
        }

        override public void Start( )
        {
            m_Stop = false;
            m_Worker = new System.Threading.Thread( StartThread );
            m_Worker.Start( );
        }

        override public void Stop( )
        {
            m_Stop = true;
        }

        public void StartThread( )
        {
            List<iPortMB> readers = new List<iPortMB>( );
            while( false == m_Stop )
            {
                try
                {
                    if( null != Produce )
                    {

                        string IPAddress = m_DeviceConfig.Options[0];
                        int Port = int.Parse( m_DeviceConfig.Options[1] );
                        int SleepTime = int.Parse( m_DeviceConfig.Options[2] );
                        using( TCPSocketStream myStream = new TCPSocketStream( IPAddress, Port ) )
                        {
                            myStream.Open( );
                            myStream.ReadTimeout = new TimeSpan( 0, 0, 10 );
                            myStream.WriteTimeout = new TimeSpan( 0, 0, 10 );
                            iBusAdapter myBus = new iBusAdapter( myStream );
                            IBusDevice[] devs = myBus.EnumerateBusModules( );
                            if( 0 != devs.Length )
                            {
                                for( int i = 0; i < devs.Length; i++ )
                                {
                                    iPortMB mb = devs[i] as iPortMB;
                                    if( mb != null )
                                    {
                                        readers.Add( mb );
                                        mb.SetTagListBehavior( TagListBehavior.RemoveTagsWhenReported );
                                        mb.EnableHighRfSensitivity( true );
                                    }
                                }
                                while( false == m_Stop )
                                {
                                    readers.ForEach( mb =>
                                    {
                                        TagCollection tags = mb.GetTags( true );
                                        if( 0 < tags.Count )
                                        {
                                            foreach( iB2Tag t in tags )
                                            {
                                                BevMessage Message = new BevMessage
                                                {
                                                    DeviceName = Config.Name,
                                                    DeviceID = Config.ID,
                                                    Pour = new BevClasses.Pour { Duration = TimeSpan.FromSeconds(Math.Abs(t.Signal)) },
                                                    Tag = t.Label,
                                                    ReadTime = t.ContactTime,
                                                    Type = MessageType.RawData
                                                };
                                                Produce( Message );
                                            }
                                        }
                                    } );
                                    Thread.Sleep( SleepTime );
                                }
                            }
                        }
                    }
                }
                catch( Exception ex )
                {
                    Log.WriteException( "iPortMBPlugin", ex );
                }
            }
        }
    }
}
