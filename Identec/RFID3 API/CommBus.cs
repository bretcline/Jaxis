using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.Collections;
using System.Net;
using System.IO;
using System.Net.Sockets;


namespace IDENTEC
{
 	namespace Readers
	{

		/// <summary>
		/// Provides common method definitions for use with custom streams (serial port, file, etc).
		/// </summary>
		public interface ICompatibleIOStream
		{
			/// <summary>
			/// Writes data to the stream
			/// </summary>
			/// <param name="buffer"></param>
			/// <param name="nBytesToWrite"></param>
			/// <returns></returns>
			int WriteData(byte [] buffer, int nBytesToWrite);
			
			/// <summary>
			/// Reads data from the stream. Preferrably a blocking call.
			/// </summary>
			/// <param name="buffer"></param>
			/// <param name="offset"></param>
			/// <param name="nBytesToRead"></param>
			/// <returns></returns>
			int ReadData(byte [] buffer, int offset, int nBytesToRead);

			bool IsOpen();
		}

		/// <summary>
		/// Internal
		/// </summary>
	
		internal class ISolProtocolFramer: IDisposable	
		{

			public delegate void PacketReceivedEventHandler(object sender); 
			/// <summary>
			/// The event that is fired each time a full message is received.			
			/// </summary>
			public event PacketReceivedEventHandler PacketReceived;


			public delegate void PacketSentEventHandler(object sender); 
			/// <summary>
			/// The event that is fired each time a full message is sent.			
			/// </summary>
			public event PacketSentEventHandler PacketSent;

			internal enum Transport: int
			{
				/// <summary>
				/// A serial/PCMCIA connection
				/// </summary>
				Serial,
				/// <summary>
				/// A TCP socket based (client) connection
				/// </summary>
				TCP,
				/// <summary>
				/// Compatible stream (Serial port, file?)
				/// </summary>
				Stream
			}

			/// <summary>
			/// Start Of Header
			/// </summary> 
			const byte SOH = 0x01;
			/// <summary>
			/// End Of Transmission
			/// </summary>
			const byte EOT = 0x04;
			/// <summary>
			/// Data Link Escape
			/// </summary>
			const byte DLE = 0x10;

			//const int INTERCHAR_TIMEOUT = 150;

			private Port m_Port;

			private bool m_bTCPConnected = false;			
			
			private Socket m_socket;

			private ICompatibleIOStream m_stream;

			public ICompatibleIOStream DataStream
			{
				get {return m_stream ;}
				set 
				{
					m_transport = Transport.Stream;
					m_stream = value;
				}
			}
			
			public Socket Socket
			{
				get { return m_socket;}
			}

			/// <summary>
			/// The address if multiple readers are used on a bus; that is modular readers such as the T2
			/// </summary>
			internal byte m_byBusAddress = 0;

			/// <summary>
			/// Helps with multiple connections between applications (CF does not support the constructor we need to use
			/// </summary>
			//TODO: Create mutex wrapper compatible with CF
			private Mutex m_mutexConnection;

			internal byte [] m_bySendBuffer;		
	
			internal Transport m_transport = Transport.Serial;

			private uint m_nSendPacketCount;
			private uint m_nReceivePacketCount;
			public uint SentPackets
			{
				get { return m_nSendPacketCount;}
			}

			public uint ReceivedPackets
			{
				get { return m_nReceivePacketCount;}
			}


			public void ClearReadBuffer()
			{
				Array.Clear(m_byArrayReadHelper, 0, m_byArrayReadHelper.Length);
				m_nBytesAlreadyRead = 0;
			}

#if DEBUG
            //internal StreamWriter m_swDebug;
            //internal StreamWriter m_swDebugAtCard;
#endif
			public ISolProtocolFramer()
			{			
				m_bySendBuffer = new byte[1024]; //should be big enough for all messages
				m_byArrayReadHelper = new byte[512];			
	
#if (DEBUG && WRITE_DEBUG_TO_FILE)
				m_swDebug = new StreamWriter("debug.txt", true);
				m_swDebug.WriteLine("Testing started at " + DateTime.Now.ToString());
				m_swDebug.Flush();

				m_swDebugAtCard = new StreamWriter("debug2.txt", true);
				m_swDebugAtCard.WriteLine("Testing started at " + DateTime.Now.ToString());
				m_swDebugAtCard.Flush();
#endif
			}

			~ISolProtocolFramer()
			{
				if (null != m_Port)
					m_Port.Dispose();
			}



			private void InitializeTCPClient()
			{
				m_transport = Transport.TCP;
				m_socket = new Socket(AddressFamily.InterNetwork,
					SocketType.Stream, ProtocolType.Tcp);				
			}

			/// <summary>
			/// 
			/// </summary>
			/// <param name="nIndex"></param>
			/// <param name="bySendBuffer"></param>
			/// <param name="byChar"></param>
			internal static void StuffiCard3Message(ref int nIndex, byte [] bySendBuffer, byte byChar)
			{
				switch (byChar)
				{
					case SOH:
						bySendBuffer[nIndex++] = DLE;
						byChar ^= 0x80;
						bySendBuffer[nIndex++] = byChar;
						break;

					case EOT:
						bySendBuffer[nIndex++] = DLE;
						byChar ^= 0x80;
						bySendBuffer[nIndex++] = byChar;
						break;
	
					case DLE:
						bySendBuffer[nIndex++] = DLE;
						byChar ^= 0x80;
						bySendBuffer[nIndex++] = byChar;
						break;

					default:
						bySendBuffer[nIndex++] = byChar;
						break;
				}
			}

			internal static int UnstuffiCard3Message( byte [] byOut, byte [] byIn, int len )
			{
				int  nlen = 0;
				byte dleflag = 0;

				int n_in_Index = 0;
				int n_out_Index = 0;

				for ( ; len-- > 0; n_in_Index++ )
				{
					switch ( byIn[n_in_Index] )
					{
						case DLE:
							dleflag = 0x80;
							break;

						default:
							byOut[n_out_Index++] = (byte) ( byIn[n_in_Index] ^ dleflag);//*out++ = *in ^ dleflag;		
							nlen++;
							dleflag = 0;
							break;
					}
				}
				//return dleflag ? 0 : nlen; //can't do this in C# :(
				if (dleflag != 0) //only because C# does not allow if's on integers...
					return 0;
				else
					return nlen;
			}



			/// <summary>
			/// Resets the buffer count so that the instance doesn't think it still has data in the buffer.
			/// </summary>
			public void ClearReceiveBuffer()
			{
				m_nBytesAlreadyRead = 0;
			}

			internal bool m_bTraceOutSendMessage = true;


			/// <summary>
			/// Frames the message with SOH, EOT CRC's etc.
			/// </summary>
			/// <param name="msg"></param>
			/// <param name="len"></param>
			internal void SendMessage( byte [] msg, int len )
			{			
				Debug.Assert(msg.Length <= m_bySendBuffer.Length);
				Debug.Assert(len <= m_bySendBuffer.Length);

				//TODO: Throw an exception here if the variables below are invalid
				//if ( m_byAddress < 0 || m_byAddress > 0xfe )			

				ushort crc = CRC.CRC16( msg, len );		
				m_bySendBuffer[0] = SOH;
				int i = 1, j = 0;
				while ( len-- > 0 )
				{
					StuffiCard3Message(ref i, m_bySendBuffer, msg[j++]);
					//iCard3_stuff( &ptr, *msg++ );
				}

				StuffiCard3Message(ref i, m_bySendBuffer, (byte)( crc & 0xff )); // iCard3_stuff( &ptr, (byte)( crc & 0xff ) );
				StuffiCard3Message(ref i, m_bySendBuffer, (byte)( crc >> 8   ));// iCard3_stuff( &ptr, (byte)( crc >> 8   ) );
				m_bySendBuffer[i++] = EOT; //*ptr++ = EOT;			

				byte [] byDataToSend = new byte[i];	//create an array that is exactly the right length...	
				//TODO: write the data out the port without copying...
				Array.Copy(m_bySendBuffer, 0, byDataToSend, 0, i);	
	
#if DEBUG					
				if (m_bTraceOutSendMessage)
				{	
					Debug.Write("Writing " + i + " bytes: ");
					Debug.WriteLine(BitConverter.ToString(byDataToSend, 0, i));					
				}
#endif

				switch (m_transport)
				{
					case Transport.Serial:
					{
						if (null == m_Port)						
                            throw new Exception("The device is not connected");
										
						if (!m_Port.IsOpen)
                            throw new Exception("The device is not connected");
						
						ClearReceiveBuffer();
						m_Port.Output = byDataToSend;

					}
						break;

					case Transport.TCP:
					{
						m_bTCPConnected = false;						
						m_socket.Send(byDataToSend, 0, byDataToSend.Length, SocketFlags.None);
						m_bTCPConnected = true;
					}
						break;

					case Transport.Stream:
					{
						if (m_stream == null)
							throw new InvalidOperationException("Data Stream has not been set");
						if (!m_stream.IsOpen())
							throw new InvalidOperationException("Data Stream is not connected");
						m_stream.WriteData(byDataToSend, byDataToSend.Length);
					}
						break;
				}	
				
				m_nSendPacketCount++;
				if (null != PacketSent)
					PacketSent(this);
			}

			/// <summary>
			/// The PCMCIA port number to connect to.
			/// </summary>
			/// <param name="nPort">The port number; the first iCard in the system is 1</param>
			/// <returns></returns>

			public bool ConnectSerialPort(int port)
			{
				return ConnectSerialPort(port, 115200);
			}

			public bool ConnectSerialPort(int port, int baudRate)
			{
				CreateConnectionMutex(port, false);
				return Connect("COM" + port.ToString() + ":", baudRate);
			}

			private void CreateConnectionMutex(int nPort, bool bPCMCIA)		
			{		
				//TODO: figure out a way to create the mutex under compact framework!!!
				if (NativeMethods.FullFramework)
				{
					if (null == m_mutexConnection)
					{					
						bool bCreatedNew = true;				
						if (bPCMCIA)
						{
						}
						else
						{
						}
						//m_mutexConnection = new Mutex(true, "ILRPCMCIA" + nPort.ToString(), out bCreatedNew);
						if (!bCreatedNew)
						{
							CloseConnectionMutex();
                            throw new Exception("Another application or process is already connected to the commbus specified");
						}
					}
				}
			
			}

			private void CloseConnectionMutex()
			{
				if (null != m_mutexConnection)
				{
					m_mutexConnection.Close();
					m_mutexConnection = null;
				}
			}
		
			public bool Connect(string strConnection)
			{
                if (strConnection.Length == 5)
                    if (!strConnection.EndsWith(":"))
                    {
                        return Connect(@"\\.\" + strConnection, 115200);
                    }
                return Connect(strConnection, 115200);
			}

			private bool Connect(string strConnection, int baudRate)
			{
				if (null == m_Port)
				{				
					HandshakeNone handshake = new HandshakeNone();
					handshake.BasicSettings.BaudRate = /*BaudRates.CBR_115200;*/ (BaudRates) baudRate;
					handshake.DTRControl = DTRControlFlows.disable;
					handshake.RTSControl = RTSControlFlows.disable;				
					m_Port = new Port(strConnection, handshake);								
					m_Port.OverlappedEnabled = false;
                    m_Port.OnError +=new Port.CommErrorEvent(m_Port_OnError);
                    return m_Port.Open();
				}
				if (m_Port.IsOpen)
					return false;
				else
				{
					m_Port = null;
					return Connect(strConnection, baudRate);
				}
			}

			
			public void ConnectTCP(string hostname, int port)
			{	
				try
				{
					IPAddress address = IPAddress.Parse(hostname);
					if (null != address)
					{
						ConnectTCP(address, port);
					}
				}
				catch (Exception ex)
				{
					//not an IP Address... maybe a network name:
					InitializeTCPClient();
					m_bTCPConnected = false;					
					m_socket.Connect(new IPEndPoint(IPAddress.Parse(hostname), port));
					m_bTCPConnected = true;
				}
			}

			public void ConnectTCP(IPAddress address, int port)
			{			
				//TODO: Check to see if we are already open
				//TODO: document exceptions!
				InitializeTCPClient();						
				m_bTCPConnected = false;								
				m_socket.Connect(new IPEndPoint(address, port));
				m_bTCPConnected = true;
			}

			internal bool IsOpen
			{
				get
				{
					switch (m_transport)
					{
						case Transport.Serial:
							if (null == m_Port)
								return false;
							return m_Port.IsOpen;
							//break;

						case Transport.TCP:
							if (null == m_socket)
								return false;
							Debug.Assert(m_socket.Connected == m_bTCPConnected, "The socket connection state doesn't match our state");
							return m_bTCPConnected;
							//break;

						case Transport.Stream:
							return m_stream.IsOpen();
							//break;
					}
					return false;
				}
			}


			internal bool bPollTCP4AvailableDataFirst = false;
			internal TimeSpan tsTimeOutPoll4AvailableTCPData;
			private int m_nBytesAlreadyRead = 0;
			private byte [] m_byArrayReadHelper;
#if DEBUG
			public bool m_bDebugOutputPortData = false;
#endif			
			
			private bool ReadPortCached(byte [] byArray, int nBytesToRead, ref int nRead)
			{
				//Sanity checks
				Debug.Assert(nBytesToRead > 0);
				Debug.Assert(m_nBytesAlreadyRead >= 0, "The number of bytes read was negative", m_nBytesAlreadyRead.ToString());
				Debug.Assert(m_byArrayReadHelper.Length >= nBytesToRead);
				
				bool bReturn = true;
				if (0 == m_nBytesAlreadyRead)
				{
					switch (m_transport)
					{
						case Transport.Serial:
							bReturn =  m_Port.ReadPort(m_byArrayReadHelper, 256, ref m_nBytesAlreadyRead);
							break;
						case Transport.TCP:
						{                            
							if (bPollTCP4AvailableDataFirst)
							{
								DateTime dtToQuit = DateTime.Now.Add(tsTimeOutPoll4AvailableTCPData);
								while (true)
								{
									//We are polling to avoid a dropped connection in this case
									if (m_socket.Available > 0)
										break;

									if (DateTime.Now > dtToQuit)
									{
										return false;
									}
									//Because this polling could potentially cause a lot of CPU usage...
									Thread.Sleep(1);
								}
							}

							//TODO: handle error codes!!!
							m_bTCPConnected = false;
							//This call blocks to read the data according to the timeout; it throw an IO exception if the timeout occurs!							
							m_nBytesAlreadyRead = m_socket.Receive(m_byArrayReadHelper, 0, m_byArrayReadHelper.Length, SocketFlags.None);
							m_bTCPConnected = true;
							if (m_nBytesAlreadyRead <=0)
								bReturn = false;
							else
								bReturn = true;
						}
							break;

						case Transport.Stream:
						{
							if (m_stream == null)
								throw new InvalidOperationException("Data Stream has not been set");
							if (!m_stream.IsOpen())
								throw new InvalidOperationException("Data Stream is not connected");
							m_nBytesAlreadyRead = m_stream.ReadData(m_byArrayReadHelper, 0, m_byArrayReadHelper.Length);
							if (m_nBytesAlreadyRead <=0)
								bReturn = false;
							else
								bReturn = true;
						}

							break;

							//#if DEBUG

							//#endif
					}
					

#if (DEBUG && WRITE_DEBUG_TO_FILE)
					if (m_nBytesAlreadyRead > 0)
					{
						m_swDebug.WriteLine(BitConverter.ToString(m_byArrayReadHelper, 0, m_nBytesAlreadyRead));
						m_swDebug.Flush();
					}
#endif

#if DEBUG
					
//					if (/*m_bDebugOutputPortData &&*/ m_nBytesAlreadyRead > 0)
//					{					
//					Debug.WriteLine(BitConverter.ToString(m_byArrayReadHelper, 0, m_nBytesAlreadyRead));
//					Debug.WriteLine("");
//					}
					
#endif
				}

				if (m_nBytesAlreadyRead > 0)
				{
					Debug.Assert(m_nBytesAlreadyRead >= nBytesToRead, "The number of bytes to read is greater than the bytes available");
					//get data into calling buffer
					Array.Copy(m_byArrayReadHelper, 0, byArray, 0, nBytesToRead);
					nRead = nBytesToRead;

					m_nBytesAlreadyRead -= nRead;
					//shift array over...					
					if (m_nBytesAlreadyRead > 0)
						Array.Copy(m_byArrayReadHelper, nRead, m_byArrayReadHelper, 0, m_nBytesAlreadyRead);
								
				}	
				else
					nRead = 0;
				return bReturn;
			}

						
			internal int iCard3_RecvMsg(byte [] msg, long timeout, int nPauseBeforeRead)
			{
#if DEBUG
				if (timeout < 300)
				{
					throw new ArgumentOutOfRangeException("The communications timeout may be too short for the real world");
				}
#endif
				long start, ltimeout, now;
				int   nTotalBytes = 0;			
				int nRead = 0;			
				//int nTempRx = 0; //helps us append to msg

				byte [] byArrayTemp = new byte[1];		
#if DEBUG
				byte [] byMessageBeforeUnstuff = new byte[256];
				byte [] bySanityBuff = new byte[256];
				int nLenBeforeUnstuff = 0;
				int nBytesReceivedSanityCheck = 0;
#endif
				Thread.Sleep(nPauseBeforeRead);			

				start = now = Environment.TickCount;
				ltimeout = timeout;
				while(true)
				{
					try
					{
						if (!ReadPortCached(byArrayTemp, 1, ref nRead))
							return -1;// ILR_LLIOERR;
						Debug.Assert(nRead <= 1, "More than 1 byte read");

#if DEBUG
						if (nRead == 1)
						{
							bySanityBuff[nBytesReceivedSanityCheck] = byArrayTemp[0];						
							nBytesReceivedSanityCheck += nRead;
							Debug.Assert(bySanityBuff[0] == SOH, "No SOH in the sanity check");
						}
#endif
					}
						//TODO: make this exception into a helper procedure
					catch (CommPortException ex)
					{
						#region CommPortException handler
						switch (ex.Win32ErrorCode)
						{
								//TODO: resource strings!!!
							case 2:
								throw new CommPortException(
									"The device is unavailable or could not be detected", 
									ex.Win32ErrorCode);
							case 5:
								throw new CommPortException("The device cannot be accessed anymore.", 
									ex.Win32ErrorCode);											
								
							case 32: //extremely unlikely but here for completeness
								throw new CommPortException("The device is being used by another process.", 
									ex.Win32ErrorCode);							
						}
						#endregion
						throw; //rethrow
					}

					if (nRead > 0)
					{							
						//msg[nTempRx] = byArrayTemp[0];
						msg[nTotalBytes] = byArrayTemp[0];
						//nTempRx += nRead;
						nTotalBytes +=nRead;



#if (DEBUG && WRITE_DEBUG_TO_FILE)		
						
							m_swDebugAtCard.WriteLine(BitConverter.ToString(byArrayTemp, 0, 1));
							m_swDebugAtCard.Flush();						
#endif
						
					
						//If for some reason we've read data to the maximum length of the buffer without findind SOH/EOT...
						//if (msg.Length <= nTempRx)
						if (msg.Length <= nTotalBytes)
                            throw new Exception("Error parsing i-CARD 3 message: " + 
								BitConverter.ToString(msg, 0, msg.Length));	
					}
					if ( nRead == 0 )
					{	
						if ( (Environment.TickCount - now) > ltimeout )
						{
							Debug.WriteLine("Timeout is: " + ltimeout + "ms");
							Debug.Assert(nTotalBytes ==0, "Aborting read when bytes are in the queue!");
                            throw new Exception("Low level communications timeout. Timeout setting was " + ltimeout + "ms"); 
						}
						//return - 2;//return nTotalBytes == 0 ? ILR_LLTIMEOUT :  ILR_LLCTIMEOUT;   // timeout
						continue;
					}

					now = Environment.TickCount;
					//					ltimeout = INTERCHAR_TIMEOUT;

					if ( nTotalBytes == 0 &&  byArrayTemp[0] != SOH )
					{
						if ( (Environment.TickCount - start) > timeout )
                            throw new Exception("Low level communications timeout. Timeout setting was " + ltimeout + "ms"); 		

						Thread.Sleep(0);
						continue;
					}
					if (byArrayTemp[0] == SOH )
					{
						if ( (Environment.TickCount - start) > timeout )
						{							
							Debug.WriteLine("Timeout is: "+(Environment.TickCount - start).ToString() + "ms");
							Debug.Assert(nTotalBytes ==0, "Aborting read when bytes are in the queue!");
                            throw new Exception("Low level communications timeout. Timeout setting was " + ltimeout + "ms"); 
							//return -2;//ILR_LLTIMEOUT;   // bail out if the i-Card won't stop sending
						}				
						// re-synchronize start of message
						//NOTE:::!!!! this used to be set to 0 with the nTempRx
						nTotalBytes = 1;
						msg[0] = SOH; 												
					}					
					//nTotalBytes = nTempRx;
					//Check to see if we have the end of transmission character:
					if (byArrayTemp[0] == EOT )
					{				
						Debug.Assert(msg[0] == SOH, "SOH not at the start of the message");
#if DEBUG
						for (int i = 0; i < nTotalBytes - 1; i++)
						{
							Debug.Assert(msg[i] != EOT, "EOT in the middle of the message");
						}
#endif
						//Got the end character so it is time to get out of the while loop :)
						break;
					}
				}//end while loop

#if DEBUG
                if (m_bTraceOutSendMessage)
                {
                    Debug.Write("Reading " + nTotalBytes + " bytes: ");
                    Debug.WriteLine(BitConverter.ToString(msg, 0, nTotalBytes));
                }
#endif

				if ( nTotalBytes > 2 )
				{
					Debug.Assert(msg[0] == SOH, "SOH missing from message");
					Debug.Assert(msg[nTotalBytes -1] == EOT, "EOT missing from message");

					//byte [] byIn = new byte[nTempRx];					
					byte [] byIn = new byte[nTotalBytes];					
					//Array.Copy(msg, 1, byIn, 0, nTempRx);					
					Array.Copy(msg, 1, byIn, 0, nTotalBytes);					
					//Debug.Assert(nTempRx == nTotalBytes);


					
					//Gets the message without the SOH and EOT:
					nTotalBytes = UnstuffiCard3Message(msg, byIn, nTotalBytes - 2);
					
				}
				else
                    throw new Exception("Low level message format error");//return ILR_LLFORMAT;
				if ( nTotalBytes <= 2 )
                    throw new Exception("Low level message format error");//return ILR_LLFORMAT;   // format error			
				



				//This bad boy is returning false when it should return true ;)
				//if ( !CRC.CRCok(msg, (int)nTotalBytes ) )
				if ( !CRC.CRCok(msg, (int)nTotalBytes ) )
				{			
#if DEBUG
					CRC.CRCok(byMessageBeforeUnstuff, nLenBeforeUnstuff);
#endif 
                    throw new Exception("The message received from the reader has an invalid CRC");					
				}
				else				
				{	
					m_nReceivePacketCount++;
					if (null != PacketReceived)
						PacketReceived(this);
					//m_ArrayBytesReadDEBUG.Add(-1);
					return nTotalBytes - 2;
				}
			}


			internal bool Disconnect()
			{
				CloseConnectionMutex();

				switch (m_transport)
				{
					case Transport.Serial:
						if (null != m_Port)
						{
							return m_Port.Close();
						}
						break;

					case Transport.TCP:
					{
						if (m_socket != null)
						{
							m_socket.Close();
							m_socket = null;
							m_bTCPConnected = false;
							return true;
						}
					}
						break;
					case Transport.Stream:
						//DO nothing: this is up to the stream to connect and disconnect!
						break;
				}


				return false;
			}
			#region IDisposable Members

			public void Dispose()
			{
				m_Port.Dispose();			
			}

			#endregion

			private void m_Port_OnError(string Description)
			{
				Debug.WriteLine("Error on Port: " + Description);
			}
		}
	}
    /// <summary>
    /// The CRC class that helps us calculate a CRC for communications with the iCard
    /// </summary>
    internal class CRC
    {
        // Add a private constructor because only static methods are used in this class
        // This prevents the compiler from creating a default constructor that will never be used
        private CRC()
        {

        }


        #region >>>>> CRC Tables <<<<<
        private static uint[] aCRCHi = 
		{
			0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0,
			0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
			0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
			0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
			0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1,
			0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
			0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1,
			0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
			0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0,
			0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40,
			0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1,
			0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
			0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0,
			0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40,
			0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
			0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
			0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0,
			0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
			0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
			0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
			0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
			0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40,
			0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1,
			0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
			0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0,
			0x80, 0x41, 0x00, 0xC1, 0x81, 0x40
		};

        private static uint[] aCRCLo = 
{
	0x00, 0xC0, 0xC1, 0x01, 0xC3, 0x03, 0x02, 0xC2, 0xC6, 0x06,
	0x07, 0xC7, 0x05, 0xC5, 0xC4, 0x04, 0xCC, 0x0C, 0x0D, 0xCD,
	0x0F, 0xCF, 0xCE, 0x0E, 0x0A, 0xCA, 0xCB, 0x0B, 0xC9, 0x09,
	0x08, 0xC8, 0xD8, 0x18, 0x19, 0xD9, 0x1B, 0xDB, 0xDA, 0x1A,
	0x1E, 0xDE, 0xDF, 0x1F, 0xDD, 0x1D, 0x1C, 0xDC, 0x14, 0xD4,
	0xD5, 0x15, 0xD7, 0x17, 0x16, 0xD6, 0xD2, 0x12, 0x13, 0xD3,
	0x11, 0xD1, 0xD0, 0x10, 0xF0, 0x30, 0x31, 0xF1, 0x33, 0xF3,
	0xF2, 0x32, 0x36, 0xF6, 0xF7, 0x37, 0xF5, 0x35, 0x34, 0xF4,
	0x3C, 0xFC, 0xFD, 0x3D, 0xFF, 0x3F, 0x3E, 0xFE, 0xFA, 0x3A,
	0x3B, 0xFB, 0x39, 0xF9, 0xF8, 0x38, 0x28, 0xE8, 0xE9, 0x29,
	0xEB, 0x2B, 0x2A, 0xEA, 0xEE, 0x2E, 0x2F, 0xEF, 0x2D, 0xED,
	0xEC, 0x2C, 0xE4, 0x24, 0x25, 0xE5, 0x27, 0xE7, 0xE6, 0x26,
	0x22, 0xE2, 0xE3, 0x23, 0xE1, 0x21, 0x20, 0xE0, 0xA0, 0x60,
	0x61, 0xA1, 0x63, 0xA3, 0xA2, 0x62, 0x66, 0xA6, 0xA7, 0x67,
	0xA5, 0x65, 0x64, 0xA4, 0x6C, 0xAC, 0xAD, 0x6D, 0xAF, 0x6F,
	0x6E, 0xAE, 0xAA, 0x6A, 0x6B, 0xAB, 0x69, 0xA9, 0xA8, 0x68,
	0x78, 0xB8, 0xB9, 0x79, 0xBB, 0x7B, 0x7A, 0xBA, 0xBE, 0x7E,
	0x7F, 0xBF, 0x7D, 0xBD, 0xBC, 0x7C, 0xB4, 0x74, 0x75, 0xB5,
	0x77, 0xB7, 0xB6, 0x76, 0x72, 0xB2, 0xB3, 0x73, 0xB1, 0x71,
	0x70, 0xB0, 0x50, 0x90, 0x91, 0x51, 0x93, 0x53, 0x52, 0x92,
	0x96, 0x56, 0x57, 0x97, 0x55, 0x95, 0x94, 0x54, 0x9C, 0x5C,
	0x5D, 0x9D, 0x5F, 0x9F, 0x9E, 0x5E, 0x5A, 0x9A, 0x9B, 0x5B,
	0x99, 0x59, 0x58, 0x98, 0x88, 0x48, 0x49, 0x89, 0x4B, 0x8B,
	0x8A, 0x4A, 0x4E, 0x8E, 0x8F, 0x4F, 0x8D, 0x4D, 0x4C, 0x8C,
	0x44, 0x84, 0x85, 0x45, 0x87, 0x47, 0x46, 0x86, 0x82, 0x42,
	0x43, 0x83, 0x41, 0x81, 0x80, 0x40
};

        #endregion

        #region >>>>> CRC Calculations <<<<<
        internal static ushort Crc16TagData(byte[] byBuffer, int count)
        {
            int crc, i;

            int c = 0;
            crc = 0;
            while (--count >= 0)
            {
                //crc = crc ^ (int)*ptr++ << 8;
                crc = crc ^ (int)byBuffer[c++] << 8;
                for (i = 0; i < 8; ++i)
                    if ((crc & 0x8000) != 0)
                        crc = crc << 1 ^ 0x1021;
                    else
                        crc = crc << 1;
            }
            return (ushort)(crc & 0xFFFF);
        }

        static internal ushort CRC16(byte[] msg, int len)
        {
            byte CRCHi = 0xFF;               // high CRC byte initialized
            byte CRCLo = 0xFF;               // low CRC byte initialized
            ushort index;                      // will index into CRC lookup table

            int i = 0;

            while (len-- > 0)                  // pass through message buffer
            {
                index = (ushort)(CRCHi ^ msg[i++]);            // calculate the CRC
                CRCHi = (byte)(CRCLo ^ aCRCHi[index]);
                CRCLo = (byte)(aCRCLo[index]);
            }
            return (ushort)(CRCHi << 8 | CRCLo);
        }


        //TODO: verify that this translation is correct
        static internal bool CRCok(byte[] msg, int len)
        {
            if (len <= 2)
                return /*1*/ true;

            ushort usCalc = CRC16(msg, len - 2);
            ushort usMess = (ushort)(msg[len - 1] << 8 | msg[len - 2]);
            return usCalc == usMess;
            //return Convert.ToBoolean(/*iCard3_*/CRC16( msg, len - 2 ) != ( msg[len-1] << 8 | msg[len-2] ));
        }
        #endregion
    }
}




