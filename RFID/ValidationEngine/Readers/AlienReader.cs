using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Xml.Linq;
using Jaxis.Util.Log4Net;
using System.Threading;

namespace ValidationEngine.Readers
{
    public class AlienReader : BaseReader, IReader
    {
        protected TcpListener m_TcpClient = null;
        protected UdpClient m_UdpClient = null;
        protected Socket m_Socket = null;
        protected Thread m_ActivityThread = null;

        protected IValidationKey LastKey { get; set; }
        
        public Action<IValidationKey> SubmitForProcessing { get; set; }

        public AlienReader( IConfigData _Config )
            : base( _Config )
        {

        }

        public override void Stop( )
        {
            m_Running = false;
            m_TcpClient.Stop( );
            m_UdpClient.Close( );
        }

        public override void Start( )
        {
            m_Running = true;
            // Startup thread to check for activity.
            m_ActivityThread = new Thread( ActivityMonitor );
            m_ActivityThread.Start( );


            // Receive a message and write it to the console.
            IPEndPoint e = new IPEndPoint( IPAddress.Any, ((AlienConfig)m_ConfigData).UdpPort );
            m_UdpClient = new UdpClient( e );

            UdpState s = new UdpState { e = e, u = m_UdpClient };

            Console.WriteLine( "listening for messages" );
            m_UdpClient.BeginReceive( Heartbeat, s );

            string m_Results = string.Empty;

            String strHostName = Dns.GetHostName( );
            IPHostEntry ipEntry = Dns.GetHostEntry( "localhost" );
            IPAddress[] addr = ipEntry.AddressList;

            if( 0 < addr.Count( ) )
            {
                m_TcpClient = new TcpListener( addr[0], ( (AlienConfig)m_ConfigData ).TcpPort );
                m_TcpClient.Start( );
                m_TcpClient.BeginAcceptSocket( this.ReadSocket, m_TcpClient );
            }
        }

        protected override void ActivityMonitor( )
        {
            while( m_Running )
            {
                Thread.Sleep( ( (AlienConfig)m_ConfigData ).TimeoutInterval * 2000 );
                TimeSpan ReadWindow = new TimeSpan( 0, 0, ( (AlienConfig)m_ConfigData ).TimeoutInterval * 2 );
                if( ( DateTime.Now - LastKey.TimeStamp ) > ReadWindow )
                {
                    ValidationKey Key = new ValidationKey { Key = string.Empty, TimeStamp = DateTime.Now };
                    SubmitForProcessing( Key );
                }
            }

        }

        private void Heartbeat( IAsyncResult ar )
        {
            UdpClient u = (UdpClient)( (UdpState)( ar.AsyncState ) ).u;
            IPEndPoint e = (IPEndPoint)( (UdpState)( ar.AsyncState ) ).e;

            Byte[] receiveBytes = u.EndReceive( ar, ref e );
            string receiveString = Encoding.ASCII.GetString( receiveBytes );

            Console.WriteLine( "Received: {0}", receiveString );

            u.BeginReceive( Heartbeat, ar.AsyncState );

        }

        private void ReadSocket( IAsyncResult ar )
        {
            Dictionary<string, IValidationKey> CurrentKeys = new Dictionary<string, IValidationKey>( );
            Queue<string> KeysToRemove = new Queue<string>( );

            // Get the listener that handles the client request.
            TcpListener listener = (TcpListener)ar.AsyncState;

            // End the operation and display the received data on the
            //console.
            Socket clientSocket = listener.EndAcceptSocket( ar );
            TimeSpan ReadWindow = new TimeSpan( 0, 0, ( (AlienConfig)m_ConfigData ).TimeoutInterval );
            using( NetworkStream m_NetworkStream = new NetworkStream( clientSocket ) )
            {
                using( StreamReader m_StreamReader = new StreamReader( m_NetworkStream ) )
                {
                    while( !m_StreamReader.EndOfStream && m_Running )
                    {
                        StringBuilder Builder = BuildMessage( m_StreamReader );

                        using( StringReader Reader = new StringReader( Builder.ToString( ) ) )
                        {
                            try
                            {
                                XDocument Doc = XDocument.Load( Reader );
                                IEnumerable<XElement> E = Doc.Descendants( "TagID" );
                                if( 0 < E.Count( ) )
                                {
                                    IEnumerable<XElement> LastSeen = Doc.Descendants( "LastSeenTime" );
                                    string TagID = E.First( ).Value;

                                    if( !CurrentKeys.ContainsKey( TagID ) )
                                    {
                                        ValidationKey Key = new ValidationKey { Key = TagID, TimeStamp = Convert.ToDateTime( LastSeen.First( ).Value ) };
                                        CurrentKeys.Add( TagID, Key );
                                        KeysToRemove.Enqueue( TagID );
                                        SubmitForProcessing( Key );
                                        if( 100 < KeysToRemove.Count )
                                        {
                                            string Remove = KeysToRemove.Dequeue( );
                                            CurrentKeys.Remove( Remove );
                                        }
                                    }
                                    else if( ( DateTime.Now - CurrentKeys[TagID].TimeStamp ) > ReadWindow )
                                    {
                                        ValidationKey Key = CurrentKeys[TagID] as ValidationKey;
                                        Key.TimeStamp = Convert.ToDateTime( LastSeen.First( ).Value );
                                        CurrentKeys[TagID] = Key;
                                        SubmitForProcessing( Key );
                                    }

                                    LastKey = CurrentKeys[TagID];
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
        }

        private StringBuilder BuildMessage( StreamReader m_StreamReader )
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
        }

        public class UdpState
        {
            public UdpClient u;
            public IPEndPoint e;
        }
    }
}
