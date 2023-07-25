using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Jaxis.Engine.Base.Device;
using Jaxis.Interfaces;
using Jaxis.MessageLibrary.POS;
using Jaxis.Readers.POS.Parsers;
using Jaxis.Util.Log4Net;

namespace Jaxis.Readers.POS
{
    public enum POSType
    {
        Unknown,
        InfoGenesis,
        Micros3700,
        Micros9300,
        Aloha,
    }

    public static class Extensions
    {
        static public uint ShiftRight( uint z_value, int z_shift )
        {
            return ( ( z_value >> z_shift ) | ( z_value << ( 16 - z_shift ) ) ) & 0x0000FFFF;
        }

        static public uint ShiftLeft( uint z_value, int z_shift )
        {
            return ( ( z_value << z_shift ) | ( z_value >> ( 16 - z_shift ) ) ) & 0x0000FFFF;
        }
    }

    /*

    <DeviceConfig>
        <AssemblyName>Jaxis.Readers.POS.dll</AssemblyName>
        <AssemblyType>Jaxis.Readers.POS.POSReader</AssemblyType>
        <AssemblyVersion>1.0</AssemblyVersion>
        <ID>111</ID>
        <Name>POS Reader</Name>
        <Type>DataProducer</Type>
        <State>Started</State>
        <ConsumerMessageType>None</ConsumerMessageType>
        <Options>
            <string>4550</string> <!-- TCP Port -->
            <string>10</string> <!-- Timeout -->
            <string>C:\Program Files\Beverage Metrics\EdgeBase\ParserConfig.xml</string> <!-- POSType -->
            <string>3125</string> <!-- Bar ID -->
        </Options>
    </DeviceConfig>

    */

    [System.Runtime.InteropServices.GuidAttribute("165D4A2B-4DA1-452E-A994-B3211C115D52")]
    public class POSReader : BaseProducerDevice, IProducer
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
            rc.Name = "POS Reader";
            rc.Type = DeviceType.DataProducer;
            rc.State = DeviceState.Stopped;
            rc.ProducerMessageType = 16;
            rc.ConsumerMessageType = 0;
            DeviceConfigOption Option1 = new DeviceConfigOption();
            Option1.Name = "TCP Port";
            Option1.Value = "4550";
            rc.Options.Add(Option1);
            DeviceConfigOption Option2 = new DeviceConfigOption();
            Option2.Name = "Timeout";
            Option2.Value = "10";
            rc.Options.Add(Option2);
            DeviceConfigOption Option3 = new DeviceConfigOption();
            Option3.Name = "POSType";
            Option3.Value = "C:\\Program Files\\Beverage Metrics\\EdgeBase\\ParserConfig.xml";
            rc.Options.Add(Option3);
            DeviceConfigOption Option4 = new DeviceConfigOption();
            Option4.Name = "Bar ID";
            Option4.Value = "3125";
            rc.Options.Add(Option4);
            DeviceConfigOption Option5 = new DeviceConfigOption();
            Option5.Name = "Append Date Time to Ticket";
            Option5.Value = "true";
            rc.Options.Add(Option5);
            return rc;
        }

        private string ByteToHex(byte[] comByte)
        {
            StringBuilder builder = new StringBuilder(comByte.Length * 3);
            foreach (byte data in comByte)
            {
                builder.Append(Convert.ToString(data, 16).PadLeft(2, '0').PadRight(3, ' '));
            }
            return builder.ToString().ToUpper();
        }


        private const int BUFFER_SIZE = 256;
        private readonly string LOG_TYPE = "POSReader";
        private readonly string LOG_HEARTBEAT = "POSReaderHeartbeat";

        protected TcpListener m_TcpClient = null;
        protected Socket m_Socket = null;
        protected Thread m_ActivityThread = null;
        protected IParser m_Parser = null;
        private DateTime m_ActivityTime;


        public POSReader( )
            : this(GetDefaultDeviceConfig())
        {
        }

        public POSReader( IDeviceConfig _Config )
            : base( _Config )
        {
            LOG_TYPE = this.GetType( ).Name;
            Log.Info( LOG_TYPE, string.Format("Create {0}", LOG_TYPE ) );
        }

        public override void Stop( )
        {
            Log.Wrap<int>( LOG_TYPE, "POSReader::Stop", LogType.Debug, true, ( ) =>
            {
                m_Stop = true;
                if( null != m_TcpClient )
                {
                    m_TcpClient.Stop( );
                }
                return 1;
            } );
        }

        public override void Start( )
        {
            Log.Wrap<int>( LOG_TYPE, "POSReader::Start", LogType.Debug, true, ( ) =>
            {
                m_Stop = false;

                var ParseConfig = m_DeviceConfig.GetParserConfig( );

                bool appendToTicket = m_DeviceConfig.GetAppendToTicket();
                Log.Debug(LOG_TYPE, string.Format("{1} Parser: {0}", ParseConfig, appendToTicket));

                m_Parser = new Generic(ParseConfig, appendToTicket);

                // Startup thread to check for activity.
                m_ActivityThread = new Thread( ActivityMonitor );
                m_ActivityThread.Start( );
                // Receive a message and write it to the console.
                var e = new IPEndPoint( IPAddress.Any, ( m_DeviceConfig.GetTcpPort( ) ) );

                var strHostName = Dns.GetHostName( );
                var ipEntry = Dns.GetHostEntry( strHostName );
                var addr = ipEntry.AddressList;

                if( addr.Any() )
                {
                    m_TcpClient = new TcpListener( e );
                    m_TcpClient.Start( );
                    m_TcpClient.BeginAcceptSocket( this.ReadSocket, m_TcpClient );

                    Log.Debug( LOG_TYPE, "m_TcpClient.BeginAcceptSocket" );
                }
                return 1;
            } );
        }

        protected void ActivityMonitor( )
        {
            while( !m_Stop )
            {
                if( m_ActivityTime > DateTime.Now.AddHours( 1 ) )
                {
                    //TODO: Add logic for alerting on POS non activity.
                }
                Thread.Sleep( 250 );
            }
        }

        private void ReadSocket( IAsyncResult ar )
        {
            Log.Wrap<int>( LOG_TYPE, "POSReader::ReadSocket", LogType.Debug, false, ( ) =>
            {
                if( false == m_Stop )
                {
                    Queue<string> KeysToRemove = new Queue<string>();

                    // Get the listener that handles the client request.
                    TcpListener listener = (TcpListener) ar.AsyncState;

                    // End the operation and display the received data on the console.
                    Socket clientSocket = listener.EndAcceptSocket(ar);
                    Task.Factory.StartNew(() =>
                    {
                        Log.Debug( LOG_TYPE, "Socket clientSocket = listener.EndAcceptSocket( ar )" );
                        using (NetworkStream Stream = new NetworkStream(clientSocket))
                        {
                            //using (StreamReader Reader = new StreamReader(Stream))
                            {
                                StringBuilder Builder = new StringBuilder();
                                bool TicketComplete = false;
                                while (!m_Stop)
                                {
                                    if (Stream.DataAvailable)
                                    {
                                        lock( m_Locker )
                                        {
                                            m_ActivityTime = DateTime.Now;
                                        }
                                        Builder.Append( BuildMessage(Stream, ref TicketComplete) );
                                        if (0 < Builder.Length)
                                        {
                                            Builder.Append(BuildMessage(Stream, ref TicketComplete));
                                        }
                                        if (null != m_Parser && TicketComplete)
                                        {
                                            try
                                            {
                                                Log.Debug( LOG_TYPE, Builder.ToString( ) );
                                                var T = m_Parser.ParseData(Builder.ToString());
                                                if( string.IsNullOrWhiteSpace( T.Establishment ) )
                                                {
                                                    T.Establishment = m_DeviceConfig.GetBarID();
                                                }
                                                Log.Debug( LOG_TYPE, string.Format( "POSDriver::ProduceMessage() {0}{1}", System.Environment.NewLine, T.ToString( ) ) );
                                                ProduceMessage(T);
                                                Builder.Clear();
                                                TicketComplete = false;
                                            }
                                            catch (Exception err)
                                            {
                                                Log.Exception( LOG_TYPE, err );
                                            }
                                        }
                                        else if (null == m_Parser)
                                        {
                                            Log.Error( "Parser is Invalid!" );
                                        }
                                    }
                                    else
                                    {
                                        Thread.Sleep( 5 );
                                    }
                                }
                            }
                        }
                        clientSocket.Disconnect(true);
                    });
                    listener.BeginAcceptSocket(this.ReadSocket, listener);
                }
                return 1;
            } );
        }

        private StringBuilder BuildMessage( NetworkStream _Stream, ref bool _TicketComplete )
        {
            //return Log.Wrap<StringBuilder>( "POSDriver::BuildMessage", LogType.Debug, true, ( ) =>
            {
                bool Reading = true;
                StringBuilder Buffer = new StringBuilder( );
                //_Stream.ReadTimeout = 500;
                byte[] InputBuffer = new byte[BUFFER_SIZE];
                try
                {
                    int bufferPosition = 0;
                    int ByteCount = 0;

                    try
                    {
                        if (_Stream.DataAvailable)
                        {
                            ByteCount = _Stream.Read(InputBuffer, 0, BUFFER_SIZE);
                            //Thread.Sleep(350);
                            if (ByteCount > 0)
                            {
                                byte currentChar = (byte) InputBuffer[ByteCount - 1];
                                if (currentChar == PosEscTable.ESC || currentChar == PosEscTable.GS ||
                                    currentChar == PosEscTable.DLE)
                                {
                                    InputBuffer[ByteCount] = (byte) _Stream.ReadByte();
                                    ByteCount++;
                                }

                                Log.Debug(LOG_HEARTBEAT,
                                          string.Format("{1} - POS Read Received - {0} bytes", ByteCount,
                                                        m_DeviceConfig.GetBarID()));
                                Log.Write(LOG_HEARTBEAT, InputBuffer, ByteCount, LogType.Debug);
                                Log.Debug(LOG_HEARTBEAT, ByteToHex(InputBuffer));

                            }
                        }
                    }
                    catch
                    {
                    }
                    while( bufferPosition < ByteCount && true == Reading )
                    {
                        try
                        {
                            byte currentChar = (byte)InputBuffer[bufferPosition];
                            if( currentChar == PosEscTable.ESC || currentChar == PosEscTable.GS || currentChar == PosEscTable.DLE )
                            {
                                Reading = ProcessEscapeCharacter(_Stream, ref _TicketComplete, ref Reading, Buffer, InputBuffer, ref bufferPosition, ByteCount);
                            }
                            else
                            {
                                bufferPosition = ProcessPrintableCharacter( Buffer, InputBuffer, bufferPosition );
                            }
                        }
                        catch( Exception err )
                        {
                            Log.WriteException( LOG_TYPE, "POSReader::BuildMessage", err );
                            //Log.Write(LOG_TYPE, InputBuffer, ByteCount, LogType.Debug);
                            Log.Debug(LOG_TYPE, ByteToHex(InputBuffer));
                            Reading = false;
                        }
                    }
                }
                finally
                {
                    _Stream.ReadTimeout = Timeout.Infinite;
                }
                return Buffer;
            } //);
        }

        private static int ProcessPrintableCharacter( StringBuilder Buffer, byte[] InputBuffer, int bufferPosition )
        {
            // If it is a printable character, copy it to the print buffer
            byte currentByte = InputBuffer[bufferPosition];
            if( currentByte >= PosEscTable.START_ASCII && currentByte <= PosEscTable.END_ASCII )
            {
                Buffer.Append( Convert.ToChar( currentByte ) );
            }
            else
            {
                // Handle any of the additional codes that affect the formatting.
                if( currentByte == 0x0A )
                {
                    Buffer.Append( System.Environment.NewLine );
                }
            }
            ++bufferPosition;
            return bufferPosition;
        }

        private bool ProcessEscapeCharacter( NetworkStream _Stream, ref bool _TicketComplete, ref bool Reading, StringBuilder Buffer, byte[] InputBuffer, ref int bufferPosition, int ByteCount )
        {
            bool rc = true;
            if (bufferPosition + 1 >= ByteCount)
            {
                rc = false;
                Log.Debug(LOG_TYPE, string.Format("Buffer has an invalid escape sequence in it {0} - {1}", bufferPosition, ByteCount));
                //throw new Exception("Buffer has an invalid escape sequence in it");
            }
            else
            {
                short command = (short) (InputBuffer[bufferPosition] << 8 | InputBuffer[bufferPosition + 1]);
                int commandLength = PosEscTable.GetCommandLength(command);

                switch (command)
                {
                    case PosEscTable.PARTIAL_CUT:
                    case PosEscTable.PARTIAL_CUT_2:
                    case PosEscTable.FORM_FEED:
                        {
                            Log.Write(LOG_TYPE,
                                      string.Format(
                                          "POSDriver::ProcessEscapeCharacter() End of ticket - {0:X2} {1:X2}", command,
                                          commandLength), LogType.Debug);
                            _TicketComplete = true;
                            Reading = false;
                            break;
                        }
                        // Writes 0x10 to the port. Bit 4 is ignored but meant to be on.
                    case PosEscTable.REAL_TIME_STATUS:
                        {
                            _Stream.WriteByte(0x10); //.Write( new byte[] { 0x10 }, 0, 1 );
                            break;
                        }
                        // Write a zero byte back to printer to tell it that the paper is good.
                    case PosEscTable.TRANSMIT_PERPH_STATUS:
                    case PosEscTable.TRANSMIT_PAPER_STATUS:
                    case PosEscTable.TRANSMIT_REAL_TIME_STATUS:
                    case PosEscTable.TRANSMIT_STATUS:
                        {
                            _Stream.WriteByte(0x00); //.Write( new byte[] { 0x00 }, 0, 1 );
                            break;
                        }
                    default:
                        {
                            _Stream.WriteByte(0x00); //.Write( new byte[] { 0x00 }, 0, 1 );
                            commandLength = 2;
                            break;
                        }
                }
                //            Log.Write(LOG_HEARTBEAT, string.Format("POSDriver::ProcessEscapeCharacter() {0:X2} {1:X2}", command, commandLength), LogType.Debug);

                bufferPosition += commandLength;
            }
            return rc;
        }
    }
}