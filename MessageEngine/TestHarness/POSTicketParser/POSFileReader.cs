using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Jaxis.MessageLibrary.POS;
using Jaxis.Readers.POS;
using Jaxis.Readers.POS.Parsers;

namespace POSTicketParser
{
    public class POSFileReader
    {
        protected static int RawDataLineCount = 0;
        private bool m_Stop = false;

        private string ByteToHex(byte[] comByte)
        {
            var builder = new StringBuilder(comByte.Length * 3);
            foreach (byte data in comByte)
            {
                builder.Append(Convert.ToString(data, 16).PadLeft(2, '0').PadRight(3, ' '));
            }
            return builder.ToString().ToUpper();
        }


        private const int BUFFER_SIZE = 256;

        protected IParser m_Parser = null;

        public POSFileReader()
        {
        }

        public void Start()
        {
            m_Parser = new Generic("ParserConfig.xml", true);

            ProcessLogFile("POSReaderHeartbeat.txt");

            ReadSocket();
        }

        private void ProcessLogFile(string _empty)
        {
            using (var reader = new StreamReader(_empty))
            {
                using (var writer = new StreamWriter("RawTicketData.txt"))
                {
                    var line = reader.ReadLine();
                    do
                    {
                        if (line.Length > 500)
                        {
                            writer.WriteLine(line.Substring(line.IndexOf("DEBUG - ") + 8));
                        }
                        line = reader.ReadLine();
                    } while (null != line);

                }
            }
        }

        private void ReadSocket( )
        {
            Queue<string> KeysToRemove = new Queue<string>();
            using (var output = new StreamWriter("Output.txt", true))
            {
                using (var stream = new StreamReader("RawTicketData.txt"))
                {
                    //using (StreamReader Reader = new StreamReader(Stream))
                    var builder = new StringBuilder();
                    bool ticketComplete = false;
                    while (!m_Stop && !stream.EndOfStream )
                    {
                        builder.Append(BuildMessage(stream, ref ticketComplete));
                        if (0 < builder.Length)
                        {
                            builder.Append(BuildMessage(stream, ref ticketComplete));
                        }
                        if (null != m_Parser && ticketComplete)
                        {
                            try
                            {
                                output.Write( String.Format( "{0}{1}{2}{1}{0}", "-----------------------------------------------------", System.Environment.NewLine, builder.ToString( ) ));

                                Console.WriteLine("--------------------------------------------------------------------");
                                Console.WriteLine(builder.ToString());
                                Console.WriteLine("--------------------------------------------------------------------");
                                ITicket T = m_Parser.ParseData(builder.ToString());

                                Console.WriteLine(T.ToString());
                                output.Write(String.Format("{0}{1}{2}{1}{0}", "-----------------------------------------------------", System.Environment.NewLine, T.ToString()));
                                builder.Clear();
                                ticketComplete = false;
                            }
                            catch (Exception err)
                            {
                                Console.WriteLine(err.Message);
                            }
                        }
                        else if (null == m_Parser)
                        {
                            Console.WriteLine("Parser is Invalid!");
                        }
                    }
                }
            }
        }

        private StringBuilder BuildMessage(StreamReader _stream, ref bool _ticketComplete)
        {
            bool Reading = true;
            StringBuilder Buffer = new StringBuilder();
            byte[] InputBuffer = new byte[BUFFER_SIZE];

            int bufferPosition = 0;
            int ByteCount = 0;

            try
            {
                var rawData = _stream.ReadLine();
                RawDataLineCount++;
                if (null != rawData)
                {
                    int i = 0;
                    var bits = rawData.Split(' ');
                    for (int j = 0; j < bits.Length; ++j)
                    { 
                        int value = Convert.ToInt32(bits[j], 16);
                        byte b = (byte)value;
                        if (0 != b)
                        {
                            InputBuffer[i++] = b;
                        }
                        else
                        {
                            value = Convert.ToInt32(bits[++j], 16);
                            b = (byte)value;
                            if (0 != b)
                            {
                                InputBuffer[i++] = 0;
                                InputBuffer[i++] = b;
                            }
                            else
                            {
                                value = Convert.ToInt32(bits[++j], 16);
                                b = (byte)value;
                                if (0 != b)
                                {
                                    InputBuffer[i++] = 0;
                                    InputBuffer[i++] = 0;
                                    InputBuffer[i++] = b;
                                }
                                else
                                    break;
                            }
                        }
                    }
                    ByteCount = i;
                }
                if (ByteCount > 0)
                {
                    byte currentChar = (byte)InputBuffer[ByteCount - 1];
                    if (currentChar == PosEscTable.ESC || currentChar == PosEscTable.GS || currentChar == PosEscTable.DLE)
                    {
//                        InputBuffer[ByteCount] = (byte)_stream.ReadByte();
                        ByteCount++;
                    }
                }
            }
            catch( Exception err )
            {
                Console.WriteLine( err.Message );
            }
            using (FileStream output = new FileStream("response.txt", FileMode.OpenOrCreate))
            {
                while (bufferPosition < ByteCount && true == Reading)
                {
                    try
                    {
                        byte currentChar = (byte) InputBuffer[bufferPosition];
                        if (currentChar == PosEscTable.ESC || currentChar == PosEscTable.GS ||
                            currentChar == PosEscTable.DLE)
                        {
                            Reading = ProcessEscapeCharacter(output, ref _ticketComplete, ref Reading, Buffer,
                                                             InputBuffer, ref bufferPosition, ByteCount);
                        }
                        else
                        {
                            bufferPosition = ProcessPrintableCharacter(Buffer, InputBuffer, bufferPosition);
                        }
                    }
                    catch (Exception err)
                    {
                        Console.WriteLine("POSReader::BuildMessage", err);
                        //Log.Write(LOG_TYPE, InputBuffer, ByteCount, LogType.Debug);
                        Console.WriteLine(ByteToHex(InputBuffer));
                        Reading = false;
                    }
                }
            }
            using (var results = new StreamWriter("rawresults.txt", true))
            {
                results.WriteLine( Buffer.ToString( ));
            }
            //Console.WriteLine( Buffer.ToString( ) );
            return Buffer;
        }

        private static int ProcessPrintableCharacter(StringBuilder _buffer, byte[] _inputBuffer, int _bufferPosition)
        {
            // If it is a printable character, copy it to the print buffer
            byte currentByte = _inputBuffer[_bufferPosition];
            if (currentByte >= PosEscTable.START_ASCII && currentByte <= PosEscTable.END_ASCII)
            {
                _buffer.Append(Convert.ToChar(currentByte));
            }
            else
            {
                // Handle any of the additional codes that affect the formatting.
                if (currentByte == 0x0A)
                {
                    _buffer.Append(System.Environment.NewLine);
                }
            }
            ++_bufferPosition;
            return _bufferPosition;
        }

        private bool ProcessEscapeCharacter(Stream _stream, ref bool _ticketComplete, ref bool _reading, StringBuilder _buffer, byte[] _inputBuffer, ref int _bufferPosition, int _byteCount)
        {
            bool rc = true;
            if (_bufferPosition + 1 >= _byteCount)
            {
                rc = false;
                Console.WriteLine(string.Format("Buffer has an invalid escape sequence in it {0} - {1}", _bufferPosition, _byteCount));
                //throw new Exception("Buffer has an invalid escape sequence in it");
            }
            else
            {
                short command = (short)(_inputBuffer[_bufferPosition] << 8 | _inputBuffer[_bufferPosition + 1]);
                int commandLength = PosEscTable.GetCommandLength(command);

                switch (command)
                {
                    case PosEscTable.PARTIAL_CUT:
                    case PosEscTable.PARTIAL_CUT_2:
                        //{
                        //    break;
                        //}
                    case PosEscTable.FORM_FEED:
                        {
                            Console.WriteLine(string.Format(
                                          "POSDriver::ProcessEscapeCharacter() End of ticket - {0:X2} {1:X2}", command,
                                          commandLength));
                            _ticketComplete = true;
                            _reading = false;
                            break;
                        }
                    // Writes 0x10 to the port. Bit 4 is ignored but meant to be on.
                    case PosEscTable.REAL_TIME_STATUS:
                        {
                            _stream.WriteByte(0x10); //.Write( new byte[] { 0x10 }, 0, 1 );
                            break;
                        }
                    // Write a zero byte back to printer to tell it that the paper is good.
                    case PosEscTable.TRANSMIT_PERPH_STATUS:
                    case PosEscTable.TRANSMIT_PAPER_STATUS:
                    case PosEscTable.TRANSMIT_REAL_TIME_STATUS:
                    case PosEscTable.TRANSMIT_STATUS:
                        {
                            _stream.WriteByte(0x00); //.Write( new byte[] { 0x00 }, 0, 1 );
                            break;
                        }
                    default:
                        {
                            _stream.WriteByte(0x00); //.Write( new byte[] { 0x00 }, 0, 1 );
                            commandLength = 2;
                            break;
                        }
                }
                //            Log.Write(LOG_HEARTBEAT, string.Format("POSDriver::ProcessEscapeCharacter() {0:X2} {1:X2}", command, commandLength), LogType.Debug);

                _bufferPosition += commandLength;
            }
            return rc;
        }
    }
}