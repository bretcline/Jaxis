using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using Jaxis.AlienRFID.MessageLibrary;
using Jaxis.Engine.Base.Device;
using Jaxis.Interfaces;
using Jaxis.Util.Log4Net;

namespace Jaxis.Readers.AlienRFID
{
    public class AlienReader : BaseProducerDevice, IProducer
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
            rc.Name = "AlienReader";
            rc.Type = DeviceType.DataProducer;
            rc.State = DeviceState.Stopped;
            rc.ProducerMessageType = 4;
            rc.ConsumerMessageType = 0;
            DeviceConfigOption Option1 = new DeviceConfigOption();
            Option1.Name = "TcpPort";
            Option1.Value = "127.0.0.1";
            rc.Options.Add(Option1);
            DeviceConfigOption Option2 = new DeviceConfigOption();
            Option2.Name = "UdpPort";
            Option2.Value = "555";
            rc.Options.Add(Option2);
            DeviceConfigOption Option3 = new DeviceConfigOption();
            Option3.Name = "TimeoutInterval";
            Option3.Value = "1000";
            rc.Options.Add(Option3);
            return rc;
        }

        protected TcpListener m_TcpClient = null;
        protected UdpClient m_UdpClient = null;
        protected Socket m_Socket = null;
        protected Thread m_ActivityThread = null;

        public AlienReader( )
            : this(GetDefaultDeviceConfig())
        {
        }

        public AlienReader( IDeviceConfig _Config )
            : base( _Config )
        {
        }

        public override void Stop( )
        {
            Log.Wrap<int>( "AlienRFID::Stop", LogType.Debug, true, ( ) =>
            {
                m_Stop = true;
                m_TcpClient.Stop( );
                m_UdpClient.Close( );
                return 1;
            } );
        }

        public override void Start( )
        {
            Log.Wrap<int>( "AlienRFID::Start", LogType.Debug, true, ( ) =>
            {
                m_Stop = false;
                // Startup thread to check for activity.
                m_ActivityThread = new Thread( ActivityMonitor );
                m_ActivityThread.Start( );

                // Receive a message and write it to the console.
                IPEndPoint e = new IPEndPoint( IPAddress.Any, ( m_DeviceConfig.GetUdpPort( ) ) );
                m_UdpClient = new UdpClient( e );

                UdpState s = new UdpState { e = e, u = m_UdpClient };

                Console.WriteLine( "listening for messages" );
                m_UdpClient.BeginReceive( Heartbeat, s );

                string m_Results = string.Empty;

                String strHostName = Dns.GetHostName( );
                IPHostEntry ipEntry = Dns.GetHostEntry( strHostName );
                IPAddress[] addr = ipEntry.AddressList;

                if( 0 < addr.Count( ) )
                {
                    //m_TcpClient = new TcpListener( addr[0], ( (AlienConfig)m_ConfigData ).TcpPort );
                    m_TcpClient = new TcpListener( m_DeviceConfig.GetTcpPort( ) );
                    m_TcpClient.Start( );
                    m_TcpClient.BeginAcceptSocket( this.ReadSocket, m_TcpClient );
                }
                return 1;
            } );
        }

        protected void ActivityMonitor( )
        {
            //KDC -- Didn't wrap this method because it doesn't do anything.
            while( !m_Stop )
            {
                Thread.Sleep( m_DeviceConfig.GetTimeoutInterval( ) * 2000 );
                //TimeSpan ReadWindow = new TimeSpan( 0, 0, ( (AlienConfig)m_ConfigData ).TimeoutInterval * 2 );
                //if( ( DateTime.Now - LastKey.TimeStamp ) > ReadWindow )
                //{
                //    ValidationKey Key = new ValidationKey { Key = string.Empty, TimeStamp = DateTime.Now };
                //    SubmitForProcessing( Key );
                //}
            }
        }

        private void Heartbeat( IAsyncResult ar )
        {
            Log.Wrap<int>( "AlienRFID::Heartbeat", LogType.Debug, true, ( ) =>
            {
                UdpClient u = (UdpClient)( (UdpState)( ar.AsyncState ) ).u;
                IPEndPoint e = (IPEndPoint)( (UdpState)( ar.AsyncState ) ).e;

                Byte[] receiveBytes = u.EndReceive( ar, ref e );
                string receiveString = Encoding.ASCII.GetString( receiveBytes );

                Console.WriteLine( "Received: {0}", receiveString );

                u.BeginReceive( Heartbeat, ar.AsyncState );
                return 1;
            } );
        }

        private void ReadSocket( IAsyncResult ar )
        {
            Log.Wrap<int>( "AlienRFID::ReadSocket", LogType.Debug, true, ( ) =>
            {
                Queue<string> KeysToRemove = new Queue<string>( );

                // Get the listener that handles the client request.
                TcpListener listener = (TcpListener)ar.AsyncState;

                // End the operation and display the received data on the console.
                Socket clientSocket = listener.EndAcceptSocket( ar );
                using( NetworkStream m_NetworkStream = new NetworkStream( clientSocket ) )
                {
                    using( StreamReader m_StreamReader = new StreamReader( m_NetworkStream ) )
                    {
                        while( !m_StreamReader.EndOfStream && !m_Stop )
                        {
                            StringBuilder Builder = BuildMessage( m_StreamReader );

                            using( StringReader Reader = new StringReader( Builder.ToString( ) ) )
                            {
                                try
                                {
                                    XDocument Doc = XDocument.Load( Reader );
                                    Log.Write( Doc.ToString( ), LogType.Debug );
                                    IEnumerable<XElement> E = Doc.Descendants( "TagID" );
                                    if( 0 < E.Count( ) )
                                    {
                                        IEnumerable<XElement> LastSeen = Doc.Descendants( "LastSeenTime" );
                                        string TagID = E.First( ).Value;

                                        AlienMessages Message = new AlienMessages( );

                                        Message.TagID = TagID;
                                        Message.DeviceID = this.m_DeviceConfig.ID;
                                        Message.ReadTime = Convert.ToDateTime( LastSeen.First( ).Value );
                                        //Message.Type = MessageType.RawData;

                                        ProduceMessage( Message );
                                    }
                                }
                                catch( Exception err )
                                {
                                    Log.WriteException( string.Format( "On Read: {0}", Builder.ToString( ) ), err );
                                }
                            }
                        }
                    }
                }
                clientSocket.Disconnect( true );
                listener.BeginAcceptSocket( this.ReadSocket, listener );
                return 1;
            } );
        }

        private StringBuilder BuildMessage( StreamReader m_StreamReader )
        {
            return Log.Wrap<StringBuilder>( "AlienRFID::BuildMessage", LogType.Debug, true, ( ) =>
            {
                StringBuilder Builder = new StringBuilder( );

                bool IncompleteMessage = true;
                bool MessageStarted = false;
                Builder.Append( "<Root>" );
                while( true == IncompleteMessage && !m_StreamReader.EndOfStream )
                {
                    string Line = m_StreamReader.ReadLine( ).Trim( );

                    if( false == MessageStarted && Line.Contains( "<Alien-RFID-Tag>" ) && Line.Contains( "</Alien-RFID-Tag>" ) )
                    {
                        Builder.Append( Line );
                        IncompleteMessage = false;
                        MessageStarted = false;
                    }
                    else if( false == MessageStarted && Line.Contains( "<Alien-RFID-Tag>" ) )
                    {
                        MessageStarted = true;
                        Builder.Append( "<Alien-RFID-Tag>" );
                    }
                    else if( MessageStarted )
                    {
                        Builder.Append( Line );
                        if( Line.Contains( "</Alien-RFID-Tag>" ) )
                        {
                            IncompleteMessage = false;
                            MessageStarted = false;
                        }
                    }
                }
                Builder.Append( "</Root>" );

                return Builder;
            } );
        }

        public class UdpState
        {
            public UdpClient u;
            public IPEndPoint e;
        }
    }
}