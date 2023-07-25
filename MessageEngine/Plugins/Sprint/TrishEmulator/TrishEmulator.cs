using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Jaxis.Engine.Base.Device;
using Jaxis.Interfaces;
using Jaxis.MessageLibrary;
using Jaxis.Util.Log4Net;
using JaxisExtensions;
using System.Net;

namespace Jaxis.Readers.Sprint
{
    public class TrishEmulator : BaseDevice, IConsumer
    {
        enum Commands
        {
            RPOLL,
            POLL,
            RTRV,
            RTR,

        }


        /*
        <DeviceConfig>
            <AssemblyName>TrishEmulator.dll</AssemblyName>
            <AssemblyType>Jaxis.Readers.Sprint.TrishEmulator</AssemblyType>
            <AssemblyVersion>1.0</AssemblyVersion>
            <ID>TrishEmulator</ID>
            <Name>TrishEmulator</Name>
            <Type>DataConsumer</Type>
            <State>Started</State>
            <ProducerMessageType>0</ProducerMessageType>
            <ConsumerMessageType>32</ConsumerMessageType>
            <Options>
                <string>127.0.0.1</string> <!-- IP address -->
                <string>555</string> <!-- Port -->
                <string>Trish,Identec</string> <!-- What original message type to support -->
            </Options>
        </DeviceConfig>
        */

        [Obfuscation(Exclude = true)]
        public static DeviceConfig GetDefaultDeviceConfig()
        {
            DeviceConfig rc = new DeviceConfig();
            System.Reflection.Assembly Asm = System.Reflection.Assembly.GetExecutingAssembly();
            rc.AssemblyName = Asm.ManifestModule.Name;
            rc.AssemblyType = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName;
            rc.AssemblyVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(BaseDevice.GetPath() + "\\" + rc.AssemblyName).FileVersion;
            rc.ID = Guid.NewGuid().ToString();
            rc.Name = "TrishEmulator";
            rc.Type = DeviceType.DataConsumer;
            rc.State = DeviceState.Stopped;
            rc.ProducerMessageType = 0;
            rc.ConsumerMessageType = 32;
            DeviceConfigOption Option1 = new DeviceConfigOption();
            Option1.Name = "IP address";
            Option1.Value = "127.0.0.1";
            rc.Options.Add(Option1);
            DeviceConfigOption Option2 = new DeviceConfigOption();
            Option2.Name = "Port";
            Option2.Value = "555";
            rc.Options.Add(Option2);
            DeviceConfigOption Option3 = new DeviceConfigOption();
            Option3.Name = "MessageType";
            Option3.Value = "Trish,Identec";
            rc.Options.Add(Option3);
            return rc;
        }

        private TcpListener m_TcpClient;
        private List<CalcPour> m_Pours;
        private List<string> m_RPollData;

        protected char[] CRLFChars = new char[] { (char)0x0d, (char)0x0a };
        protected string CRLFString = new string( new char[] { (char)0x0d, (char)0x0a });
        private Dictionary<OriginalMessageType, bool> m_SupportedMessages = null;

                
        public TrishEmulator( )
            : this(GetDefaultDeviceConfig())
        {
        }


        public TrishEmulator( IDeviceConfig _Config )
            : base( _Config )
        {
            try
            {
                Log.Debug( _Config.AssemblyType );

                Log.Debug("Create Trish Emulator");
                Config.Type = DeviceType.DataConsumer;

                m_Pours = new List<CalcPour>();
                m_RPollData = new List<string>( );
            }
            catch( Exception exp )
            {
                Log.WriteException("TrishEmulator::TrishEmulator", exp);
            }
        }

        override public void Start( )
        {
            try
            {
                Log.Debug( string.Format( "Start TrishEmulator" ) );

                if( null != m_DeviceConfig )
                {
                    Log.Debug(string.Format("Start TrishEmulator on {0}", m_DeviceConfig.GetPort()));

                    m_SupportedMessages = m_DeviceConfig.GetSupportedMessages();
                    IPEndPoint endPoint = new IPEndPoint( IPAddress.Any, ( m_DeviceConfig.GetPort() ) );

                    String hostName = Dns.GetHostName( );
                    IPHostEntry ipEntry = Dns.GetHostEntry( hostName );
                    IPAddress[] addr = ipEntry.AddressList;

                    if( 0 < addr.Count( ) )
                    {
                        m_TcpClient = new TcpListener( endPoint );
                        m_TcpClient.Start( );
                        m_TcpClient.BeginAcceptSocket( this.ReadSocket, m_TcpClient );

                        Log.Debug( "TrishEmulator::m_TcpClient.BeginAcceptSocket" );
                    }
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "TrishEmulator:: Start", exp );
            }
        }
        
        override public void Stop( )
        {
            try
            {
                m_TcpClient.Stop();
                State = DeviceState.Stopped;
                Config.State = DeviceState.Stopped;
            }
            catch( Exception exp )
            {
                Log.WriteException( "TrishEmulator:: Stop", exp );
            }
            finally
            {
                m_Stop = true;
            }
        }

        private void ReadSocket( IAsyncResult ar )
        {
            Log.Wrap<int>( "TrishEmulator::ReadSocket", LogType.Debug, false, ( ) =>
            {
                if( false == m_Stop )
                {
                    // Get the listener that handles the client request.
                    TcpListener listener = (TcpListener)ar.AsyncState;

                    // End the operation and display the received data on the console.
                    Socket clientSocket = listener.EndAcceptSocket( ar );
                    Task.Factory.StartNew( ( ) =>
                    {
                        using( NetworkStream stream = new NetworkStream( clientSocket ) )
                        {
                            StringBuilder builder = new StringBuilder( );
                            while( !m_Stop )
                            {
                                try
                                {
                                    bool readCommands = true;
                                    while( true == readCommands )
                                    {
                                        char b = (char)stream.ReadByte( );
                                        if( b == 0x0d )
                                        {
                                            b = (char)stream.ReadByte( );
                                            readCommands = false;
                                        }
                                        else
                                        {
                                            if( 32 <= (int)b && 127 > (int)b )
                                            {
                                                builder.Append( b );
                                            }
                                        }
                                    }
                                    string data = ProcessData( builder.ToString( ).ToUpper( ) );

                                    byte[] buffer = System.Text.Encoding.ASCII.GetBytes( data );
                                    stream.Write( buffer, 0, buffer.Count( ) );

                                    builder.Clear( );
                                }
                                catch( Exception err )
                                {
                                    Log.Exception( err );
                                    break;
                                }
                                finally
                                {
                                    stream.ReadTimeout = Timeout.Infinite;
                                }
                            }
                        }
                        clientSocket.Disconnect( true );
                    } );
                    listener.BeginAcceptSocket( this.ReadSocket, listener );
                }
                return 1;
            } );
        }

        protected string ProcessData( string _data )
        {
            string rc = string.Empty;
            try
            {
                string command = CleanCommands(_data);
                if( !string.IsNullOrWhiteSpace( command ) )
                {
                    if( command.StartsWith( Commands.RPOLL.ToString() ) )
                    {
                        rc = ProcessRPoll( command );
                    }
                    else if( command.StartsWith( Commands.POLL.ToString( ) ) )
                    {
                        rc = ProcessPoll( command );
                    }
                    else if( command.StartsWith( Commands.RTRV.ToString( ) ) )
                    {
                        rc = ProcessRTRV( command );
                    }
                }
            }
            catch( Exception err )
            {
                Log.Debug( err.Message );
            }
            return rc;
        }

        private string ProcessRTRV(string _command)
        {
            StringBuilder builder = new StringBuilder( );
            try
            {
                // Command format is RTRVXXX_<CR><LF> where XXX is UPC number reqused 
                string requested = ( _command.Substring( 4 ).TrimEnd( CRLFChars ) ).TrimEnd( '_' );
            }
            catch( Exception err )
            {
                Log.Debug( err.Message );
            }
            return builder.ToString( );
        }

        private string ProcessRPoll( string command )
        {
            string appender = string.Empty;
            StringBuilder builder = new StringBuilder( );
            int max = 0;
            lock( m_RPollData )
            {
                if( 0 == m_RPollData.Count( ) )
                {
                    // List is now empty, need to send "Empty"
                    builder.Append( string.Format( "{0}{1}", "Empty", CRLFString ) );
                }
                else
                {
                    // Command format is PollXXX_<CR><LF> where XXX is number reqused or 
                    // PollAll_<CR><LF> to request all in list
                    string requested = ( command.Substring( 5 ).TrimEnd( CRLFChars ) ).TrimEnd( '_' );
                    if( "ALL" == requested )
                    {
                        max = m_RPollData.Count( );
                    }
                    else
                    {
                        if( string.IsNullOrWhiteSpace( requested ) )
                        {
                            max = 1;
                        }
                        else
                        {
                            max = Convert.ToInt32( requested );
                        }
                    }

                    if( max > m_RPollData.Count( ) )
                    {
                        max = m_RPollData.Count( );
                        appender = "Empty";
                    }

                    for( int i = 0; i < max; i++ )
                    {
                        string pour = m_RPollData[i];
                        builder.Append( pour );
                    }
                    if( !string.IsNullOrEmpty( appender ) )
                    {
                        builder.Append( string.Format( "{0}{1}", appender, CRLFString ) );
                    }
                }
            }
            return builder.ToString( );
        }

        private string ProcessPoll(string command)
        {
            string appender = string.Empty;
            StringBuilder builder = new StringBuilder();
            int max = 0;
            lock( m_Pours )
            {
                m_RPollData.Clear();
                if( 0 == m_Pours.Count( ) )
                {
                    // List is now empty, need to send "Empty"
                    builder.Append( string.Format( "{0}{1}", "Empty", CRLFString ) );
                }
                else
                {
                    // Command format is PollXXX_<CR><LF> where XXX is number reqused or 
                    // PollAll_<CR><LF> to request all in list
                    string requested = ( command.Substring( 4 ).TrimEnd( CRLFChars ) ).TrimEnd( '_' );
                    if( "ALL" == requested )
                    {
                        max = m_Pours.Count( );
                    }
                    else
                    {
                        if( string.IsNullOrWhiteSpace( requested ) )
                        {
                            max = 1;
                        }
                        else
                        {
                            max = Convert.ToInt32( requested );
                        }
                    }

                    if( max > m_Pours.Count( ) )
                    {
                        max = m_Pours.Count( );
                        appender = "Empty";
                    }

                    for( int i = 0; i < max; i++ )
                    {
                        CalcPour pour = m_Pours[i];
                        string strip = string.Format("{0},{1},{2},{3:0.000###},{4:0.0000##},{5},{6},{7}{8}",
                                                     pour.ReadTime.ToString("yyyy-MM-dd,HH:mm:ss"), 
                                                     pour.TagNumber, 
                                                     pour.UPCName,
                                                     (pour.TotalPoured/1000.0), 
                                                     (pour.PourAmount/1000.0), 
                                                     pour.PourCount,
                                                     pour.DeviceID,
                                                     pour.Location, CRLFString);
                        // This should be YYYY-MM-DD,HH:MM:SS,TagNumber,UPC,TotalAmt,PourAmt,Sequence,DeviceId
                        m_RPollData.Add(strip);
                        builder.Append( strip );
                    }
                    if( !string.IsNullOrEmpty( appender ) )
                    {
                        builder.Append(string.Format("{0}{1}", appender, CRLFString));
                    }
                    m_Pours.RemoveRange( 0, max );
                }
            }
            return builder.ToString();
        }

        private string CleanCommands(string _data)
        {
            string rc = _data.Trim().ToUpper( );
            if( !rc.StartsWith( Commands.RPOLL.ToString( ) ) || !rc.StartsWith( Commands.POLL.ToString( ) ) || !rc.EndsWith( "_" ) )
            {
                int index = 0;
                if( rc.Contains( Commands.RPOLL.ToString( ) ) )
                {
                    index = rc.IndexOf( Commands.RPOLL.ToString( ) );
                }
                else if( rc.Contains( Commands.POLL.ToString( ) ) )
                {
                    index = rc.IndexOf( Commands.POLL.ToString( ) );
                }
                if( 0 < index )
                {
                    rc = rc.Substring(index);
                }
                index = rc.IndexOf("_");
                if( 0 < index && index < rc.Length - 1 )
                {
                    rc = rc.Remove(index + 1);
                }
            }
            return rc;
        }

        public override string Consume( IMessage _message )
        {
            string rc = string.Empty;

            try
            {
                if (_message is CalcPour)
                {
                    if( m_SupportedMessages.ContainsKey( ( _message as CalcPour ).OriginalType ) )
                    {
                        if( m_SupportedMessages[( _message as CalcPour ).OriginalType] )
                        {
                            lock( m_Pours )
                            {
                                m_Pours.Add( _message.Clone( ) as CalcPour );
                            }
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException("TrishEmulator::Consume", exp);
            }
            return rc;
        }
    }
}
