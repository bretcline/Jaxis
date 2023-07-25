//==========================================================================================
//		MODIFIED BY IDENTEC SOLUTIONS INC. APRIL 26, 2004
//		namespace OpenNETCF.IO.Serial.Port
//		Copyright (c) 2003, OpenNETCF.org
//
//		This library is free software; you can redistribute it and/or modify it under 
//		the terms of the OpenNETCF.org Shared Source License.
//
//		This library is distributed in the hope that it will be useful, but 
//		WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or 
//		FITNESS FOR A PARTICULAR PURPOSE. See the OpenNETCF.org Shared Source License 
//		for more details.
//
//		You should have received a copy of the OpenNETCF.org Shared Source License 
//		along with this library; if not, email licensing@opennetcf.org to request a copy.
//
//		If you wish to contact the OpenNETCF Advisory Board to discuss licensing, please 
//		email licensing@opennetcf.org.
//
//		For general enquiries, email enquiries@opennetcf.org or visit our website at:
//		http://www.opennetcf.org
//
//==========================================================================================
// $Log$
// Revision 1.1  2006/12/06 16:47:43  acastrignano
// first check-in
//
// Revision 1.13  2006-10-26 23:51:29  cadamson
// Added some more meaningful error messages instead of throwing the error code from Win32.
//
// Revision 1.12  2006/02/21 00:31:01  cadamson
// FxCop suggestions implemented.
//
// Revision 1.11  2006/01/28 00:32:57  cadamson
// Now sets the error code number in the CompPortException when one is thrown.
//
// Revision 1.10  2005/11/01 22:14:13  cadamson
// Changed:
// ct.WriteTotalTimeoutConstant = 5;
// ---
// to:
// ct.WriteTotalTimeoutConstant = 0;
//
// Because when testing i-PORT R2 units over the Ethernet with TCP/IP I was having timeout problems.
//
// Revision 1.9  2005/10/24 18:26:11  cadamson
// CommPortException now has a Win32 error code member.
//
// Revision 1.8  2005/06/14 00:28:00  cadamson
// Fx Cop Fixes.
//
// Revision 1.7  2005/06/01 16:01:32  cadamson
// Added some debug code to check the data read from the Port.
//
// Revision 1.6  2005/05/12 17:34:27  cadamson
// FX Cop fixes.
//
// Revision 1.5  2005/05/11 19:51:33  cadamson
// Added the "Readers" namespace
//
// Revision 1.4  2004/11/22 17:04:40  cadamson
// Includes some options for opening an IR port for communications.
// This may be taken out in the future.
//
// Revision 1.3  2004/07/08 21:52:55  cadamson
// More debug assertions made.
//
// Revision 1.2  2004/06/10 23:56:07  cadamson
// Enabling CVS comments
//
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;


namespace IDENTEC
{
	namespace Readers
	{
		/// <summary>
		/// Exceptions thrown by the communications port
		/// </summary>
		public class CommPortException : Exception
		{
			/// <summary>
			/// Default CommPortException
			/// </summary>
			/// <param name="desc"></param>
			public CommPortException(string desc) : base(desc) {}

			public CommPortException(string desc, int Win32ErrorCode) : base(desc) 
			{
				m_nErrorCode = Win32ErrorCode;
			}

			private int m_nErrorCode;

			public int Win32ErrorCode
			{
				get { return m_nErrorCode;}
			}

		}

		/// <summary>
		/// A class wrapper for serial port communications
		/// </summary>
		internal class Port : IDisposable
		{
			#region >>>>> DLLImport <<<<<
			[DllImport("kernel32", EntryPoint="LocalAlloc", SetLastError=true)]
			internal static extern IntPtr LocalAlloc(int uFlags, int uBytes);

			[DllImport("kernel32", EntryPoint="LocalFree", SetLastError=true)]
			internal static extern IntPtr LocalFree(IntPtr hMem);
			#endregion

			#region delegates and events
			/// <summary>
			/// Raised on all enables communication events
			/// </summary>
			public delegate void CommEvent();
			/// <summary>
			/// Raised when the communication state changes
			/// </summary>
			public delegate void CommChangeEvent(bool NewState);
			/// <summary>
			/// Rasied during any communication error
			/// </summary>
			public delegate void CommErrorEvent(string Description);

			/// <summary>
			///  A communication error has occurred
			/// </summary>
			public event CommErrorEvent OnError;
			/// <summary>
			/// Serial data has been received
			/// </summary>
			public event CommEvent DataReceived;
			/// <summary>
			/// Overrun of the transmit buffer
			/// </summary>
			public event CommEvent RxOverrun;
			/// <summary>
			/// Transmit complete
			/// </summary>
			public event CommEvent TxDone;
			/// <summary>
			/// Set flag character was in the receive stream
			/// </summary>
			public event CommEvent FlagCharReceived;
			/// <summary>
			/// Power change event has occurred
			/// </summary>
			public event CommEvent PowerEvent;
			/// <summary>
			/// Serial buffer's high-water level has been exceeded
			/// </summary>
			public event CommEvent HighWater;
			/// <summary>
			/// DSR state has changed
			/// </summary>
			public event CommChangeEvent DSRChange;
			/// <summary>
			/// Ring signal has been detected
			/// </summary>
			public event CommChangeEvent RingChange;
			/// <summary>
			/// CTS state has changed
			/// </summary>
			public event CommChangeEvent CTSChange;
			/// <summary>
			/// RLSD state has changed
			/// </summary>
			public event CommChangeEvent RLSDChange;
			#endregion

			#region variable declarations
			private string portName;
			private IntPtr hPort = (IntPtr)NativeMethods.INVALID_HANDLE_VALUE;
		
			// default Rx buffer is 1024 bytes
			private int rxBufferSize = 1024;
			private byte[] rxBuffer;
			private int prxBuffer	;//= 0;
			private int rthreshold = 1;

			// default Tx buffer is 1024 bytes
			private int txBufferSize = 1024;
			private byte[] txBuffer;
			private int ptxBuffer	;//= 0;
			private int sthreshold = 1;

			private Mutex rxBufferBusy = new Mutex();
//			private int inputLength;

			private DCB dcb = new DCB();
			private DetailedPortSettings portSettings;

			private Thread eventThread;
			private ManualResetEvent threadStarted = new ManualResetEvent(false);
		
			private IntPtr closeEvent;
			private string closeEventName = "CloseEvent";

			private int rts = -1;
//			private bool rtsavail = false;
			private int dtr = -1;
//			private bool dtravail = false;
//			private int brk = -1;
//			private int		setir	;//	= 0;
			private bool isOpen = false;

			private IntPtr txOverlapped ;//= IntPtr.Zero;
			private IntPtr rxOverlapped ;//= IntPtr.Zero;

			/// <summary>
			/// 
			/// </summary>
			/// <remarks>Added by IDENTEC</remarks>
			private bool m_bOverlappedEnabled ;//= false;

			private bool m_bRxThreadEnabled ;//= false; //ADDED BY IDENTEC
			#endregion
			
			/// <summary>
			/// 
			/// </summary>
			/// <remarks>Added by IDENTEC</remarks>
			internal bool OverlappedEnabled
			{
//				get
//				{
//					return m_bOverlappedEnabled;
//				}
				set
				{
					//TODO: check if PCMCIA, ensure it is disabled if so
					m_bOverlappedEnabled = value;
				}
			}

			
			private void Init()
			{
				// create a system event for synchronizing Closing
				closeEvent = NativeMethods.CreateEvent(true, false, closeEventName);

				rxBuffer = new byte[rxBufferSize];
				txBuffer = new byte[txBufferSize];
				portSettings = new DetailedPortSettings();
				GC.KeepAlive(this);
			}

			#region constructors
			/// <summary>
			/// Create a serial port class.  The port will be created with defualt settings.
			/// </summary>
			/// <param name="PortName">The port to open (i.e. "COM1:")</param>
			public Port(string PortName)
			{
				portName = PortName;
				Init();
			}

			/// <summary>
			/// Create a serial port class.
			/// </summary>
			/// <param name="PortName">The port to open (i.e. "COM1:")</param>
			/// <param name="InitialSettings">BasicPortSettings to apply to the new Port</param>
			public Port(string PortName, BasicPortSettings InitialSettings)
			{
				portName = PortName;
				Init();

				//override default ettings
				portSettings.BasicSettings = InitialSettings;
			}

			/// <summary>
			/// Create a serial port class.
			/// </summary>
			/// <param name="PortName">The port to open (i.e. "COM1:")</param>
			/// <param name="InitialSettings">DetailedPortSettings to apply to the new Port</param>
			public Port(string PortName, DetailedPortSettings InitialSettings)
			{
				portName = PortName;
				Init();

				//override default ettings
				portSettings = InitialSettings;
			}

			/// <summary>
			/// Create a serial port class.
			/// </summary>
			/// <param name="PortName">The port to open (i.e. "COM1:")</param>
			/// <param name="RxBufferSize">Receive buffer size, in bytes</param>
			/// <param name="TxBufferSize">Transmit buffer size, in bytes</param>
			public Port(/*string PortName,*/ int RxBufferSize, int TxBufferSize)
			{
				rxBufferSize = RxBufferSize;
				txBufferSize = TxBufferSize;
				Init();
			}
		
			/// <summary>
			/// Create a serial port class.
			/// </summary>
			/// <param name="PortName">The port to open (i.e. "COM1:")</param>
			/// <param name="InitialSettings">BasicPortSettings to apply to the new Port</param>
			/// <param name="RxBufferSize">Receive buffer size, in bytes</param>
			/// <param name="TxBufferSize">Transmit buffer size, in bytes</param>
			public Port(/*string PortName, */BasicPortSettings InitialSettings, int RxBufferSize, int TxBufferSize)
			{
				rxBufferSize = RxBufferSize;
				txBufferSize = TxBufferSize;
				Init();

				//override default ettings
				portSettings.BasicSettings = InitialSettings;
			}

			/// <summary>
			/// Create a serial port class.
			/// </summary>
			/// <param name="PortName">The port to open (i.e. "COM1:")</param>
			/// <param name="InitialSettings">DetailedPortSettings to apply to the new Port</param>
			/// <param name="RxBufferSize">Receive buffer size, in bytes</param>
			/// <param name="TxBufferSize">Transmit buffer size, in bytes</param>
			public Port(/*string PortName, */DetailedPortSettings InitialSettings, int RxBufferSize, int TxBufferSize)
			{
				rxBufferSize = RxBufferSize;
				txBufferSize = TxBufferSize;
				Init();

				//override default ettings
				portSettings = InitialSettings;
			}
			#endregion

			// since the event thread blocks until the port handle is closed
			// implement both a Dispose and destrucor to make sure that we
			// clean up as soon as possible
			/// <summary>
			/// Dispose the object's resources
			/// </summary>
			public void Dispose()
			{
				if(isOpen)
					this.Close();
			}
		
			/// <summary>
			/// Class destructor
			/// </summary>
			~Port()
			{
				if(isOpen)
					this.Close();
			}

		/*
			/// <summary>
			/// The name of the Port (i.e. "COM1:")
			/// </summary>
			public string PortName
			{
				get
				{
					return portName;
				}
				set
				{
					portName = value;
				}
			}
			*/

			/// <summary>
			/// Returns whether or not the port is currently open
			/// </summary>
			public bool IsOpen
			{
				get
				{
					return isOpen;
				}
			}
/*
			/// <summary>
			/// Gets or sets the com port for IR use (true = 1, false = 0)
			/// </summary>

			public bool IREnable
			{
				get
				{
					return (setir == 1);
				}
				set
				{
					if(setir < 0) return;
					if(hPort == (IntPtr)NativeMethods.INVALID_HANDLE_VALUE) return;

					if (value)
					{
						if (NativeMethods.EscapeCommFunction(hPort, CommEscapes.SETIR))
							setir = 1;
						else
							throw new CommPortException("Failed to set IR!");
					}
					else
					{
						if (NativeMethods.EscapeCommFunction(hPort, CommEscapes.CLRIR))
							setir = 0;
						else
							throw new CommPortException("Failed to clear IR!");
					}
				}
			}
*/
			/// <summary>
			/// Open the current port
			/// </summary>
			/// <returns>true if successful, false if it fails</returns>
			public bool Open()
			{
				if(isOpen) return false;
				GC.KeepAlive(this);

				if((NativeMethods.FullFramework) && (m_bOverlappedEnabled)) //IDENTEC added second check
				{
					// set up the overlapped tx IO
					//				AutoResetEvent are = new AutoResetEvent(false);
					OVERLAPPED o = new OVERLAPPED();
					txOverlapped = LocalAlloc(0x40, Marshal.SizeOf(o));
					o.Offset = 0; 
					o.OffsetHigh = 0;
					o.hEvent = IntPtr.Zero;
					Marshal.StructureToPtr(o, txOverlapped, true);
				}

				hPort = NativeMethods.CreateFile(portName, m_bOverlappedEnabled);

				if(hPort == (IntPtr)NativeMethods.INVALID_HANDLE_VALUE)
				{
					int e = Marshal.GetLastWin32Error();

					if(e == (int)APIErrors.ERROR_ACCESS_DENIED)
					{
						// port is unavailable
						return false;
					}

					// ClearCommError failed!					
					string error = String.Format("CreateFile Failed: {0}", e);
					switch (e)
					{
						case 2:
							error = "The system cannot find the specified device.";
							break;

						case 5:
							error = "Access to the device is denied.";
							break;

						case 31:
							error = "A device attached to the system is not functioning or unavailabledd.";
							break;

						case 32:
							error = "The process cannot access the device because it is being used by another process.";
							break;
					}
					throw new CommPortException(error, e);
				}

			
				isOpen = true;

				// set queue sizes
				NativeMethods.SetupComm(hPort, rxBufferSize, txBufferSize);

				// transfer the port settings to a DCB structure
				dcb.BaudRate = (uint)portSettings.BasicSettings.BaudRate;
				dcb.ByteSize = portSettings.BasicSettings.ByteSize;
				dcb.EofChar = (sbyte)portSettings.EOFChar;
				dcb.ErrorChar = (sbyte)portSettings.ErrorChar;
				dcb.EvtChar = (sbyte)portSettings.EVTChar;
				dcb.fAbortOnError = portSettings.AbortOnError;
				dcb.fBinary = true;
				dcb.fDsrSensitivity = portSettings.DSRSensitive;
				dcb.fDtrControl = (DCB.DtrControlFlags)portSettings.DTRControl;
				dcb.fErrorChar = portSettings.ReplaceErrorChar;
				dcb.fInX = portSettings.InX;
				dcb.fNull = portSettings.DiscardNulls;
				dcb.fOutX = portSettings.OutX;
				dcb.fOutxCtsFlow = portSettings.OutCTS;
				dcb.fOutxDsrFlow = portSettings.OutDSR;
				dcb.fParity = (portSettings.BasicSettings.Parity == Parity.none) ? false : true;
				dcb.fRtsControl = (DCB.RtsControlFlags)portSettings.RTSControl;
				dcb.fTXContinueOnXoff = portSettings.TxContinueOnXOff;
				dcb.Parity = (byte)portSettings.BasicSettings.Parity;
				dcb.StopBits = (byte)portSettings.BasicSettings.StopBits;
				dcb.XoffChar = (sbyte)portSettings.XoffChar;
				dcb.XonChar = (sbyte)portSettings.XonChar;

				dcb.XonLim = dcb.XoffLim = (ushort)(rxBufferSize / 10);

				//TODO: purge comm!
			
				NativeMethods.SetCommState(hPort, dcb);

				// store some state values
//				brk = 0;
				dtr = dcb.fDtrControl == DCB.DtrControlFlags.Enable ? 1 : 0;
				rts = dcb.fRtsControl == DCB.RtsControlFlags.Enable ? 1 : 0;

				// set the Comm timeouts
				CommTimeouts ct = new CommTimeouts();

				// reading we'll return immediately
				// this doesn't seem to work as documented
				ct.ReadIntervalTimeout = uint.MaxValue; // this = 0xffffffff
				ct.ReadTotalTimeoutConstant = 0;
				ct.ReadTotalTimeoutMultiplier = 0;

				// writing we'll give 0 seconds
				ct.WriteTotalTimeoutConstant = 0;
				ct.WriteTotalTimeoutMultiplier = 0;

				NativeMethods.SetCommTimeouts(hPort, ct);

				// start the receive thread
			
				if (m_bRxThreadEnabled)
				{
					eventThread = new Thread(new ThreadStart(CommEventThread));
					eventThread.Start();

					// wait for the thread to actually get spun up
					threadStarted.WaitOne();
				}

				return true;
			}

			/// <summary>
			/// Close the current serial port
			/// </summary>
			/// <returns>true indicates success, false indicated failure</returns>
			public bool Close()
			{

				if(txOverlapped != IntPtr.Zero)
				{
					LocalFree(txOverlapped);
					txOverlapped = IntPtr.Zero;
				}

				if(!isOpen) return false;
				GC.KeepAlive(this);

				if(NativeMethods.CloseHandle(hPort))
				{
					NativeMethods.SetEvent(closeEvent);

					isOpen = false;

					hPort = (IntPtr)NativeMethods.INVALID_HANDLE_VALUE;
				
					NativeMethods.SetEvent(closeEvent);

					return true;
				}

				return false;
			}

			/// <summary>
			/// The Port's output buffer.  Set this property to send data.
			/// </summary>
			public byte[] Output
			{
				set
				{
					if(!isOpen) return;

					GC.KeepAlive(this);
					int written = 0;

					// more than threshold amount so send without buffering
					if(value.GetLength(0) > sthreshold)
					{
						// first send anything already in the buffer
						if(ptxBuffer > 0)
						{
							NativeMethods.WriteFile(hPort, txBuffer, ptxBuffer, ref written, txOverlapped);
							ptxBuffer = 0;
						}

						NativeMethods.WriteFile(hPort, value, (int)value.GetLength(0), ref written, txOverlapped);
					}
					else
					{
						// copy it to the tx buffer
						value.CopyTo(txBuffer, (int)ptxBuffer);
						ptxBuffer += (int)value.Length;

						// now if the buffer is above sthreshold, send it
						if(ptxBuffer >= sthreshold)
						{
							NativeMethods.WriteFile(hPort, txBuffer, ptxBuffer, ref written, txOverlapped);
							ptxBuffer = 0;
						}
					}
				}
			}

			internal bool ReadPort(byte [] byDataRead, int nBytesToRead, ref int nBytesRead)
			{
				Debug.Assert(byDataRead.Length >= nBytesToRead);
				Debug.Assert(nBytesToRead >= 0);		
				GC.KeepAlive(this);
					

				// data came in, put it in the buffer and set the event
				nBytesRead = 0;
				if (!NativeMethods.ReadFile(hPort, byDataRead, nBytesToRead, ref nBytesRead, IntPtr.Zero)) 
				{
					int nError = Marshal.GetLastWin32Error();
					string error = String.Format("ReadFile Failed: {0}", nError);
					throw new CommPortException(error, nError);
					//return false;
				}
				Debug.Assert(byDataRead.Length >= nBytesRead);
			
#if DEBUG
				if (nBytesRead == nBytesToRead)
				{
					bool bGoodToGo = false;
					// Test every byte for the weird 0xFF error we get...
					for (int i = 0; i < nBytesRead; i++)
					{
						if (byDataRead[i] != 0xFF)
						{
							bGoodToGo = true;
							break;
						}
					}

					if (!bGoodToGo)
					{
						int nError = Marshal.GetLastWin32Error();
						string error = String.Format("ReadFile Failed: {0}", nError);
						throw new CommPortException(error, nError);
					}
				}			
			
#endif

				return true;		
			}

		
//			/*
//			/// <summary>
//			/// The Port's input buffer.  Incoming data is read from here.
//			/// </summary>
//			public byte[] Input
//			{
//				get
//				{
//					if(!isOpen) return null;
//
//					// if inputLength is zero, then read all available data
//					int inputLen;
//					if(inputLength == 0) 
//						inputLen = prxBuffer;
//					else
//						inputLen = inputLength;
//
//					byte[] data = new byte[inputLen];
//					data.Initialize();
//
//					// check to see if we actually have inputLength bytes in the buffer
//					if(prxBuffer >= inputLen)
//					{
//						// prevent the rx thread from adding to the buffer while we use it
//						rxBufferBusy.WaitOne();
//					
//						// copy the buffer to an output variable for inputLen bytes
//						Array.Copy(rxBuffer, 0, data, 0, inputLen);
//
//						// shift the data in the Rx Buffer to remove inputLength bytes
//						if(inputLen < rxBufferSize)
//							Array.Copy(rxBuffer, inputLen, rxBuffer, 0, (int)(rxBuffer.GetUpperBound(0) - inputLen));
//						prxBuffer -= inputLen;
//				
//						// release the mutex so the Rx thread can work
//						rxBufferBusy.ReleaseMutex();
//					}
//				
//					return data;
//				}
//			}
//*/
//			/// <summary>
//			/// The length of the input buffer
//			/// </summary>
//			public int InputLen
//			{
//				get
//				{
//					return inputLength;
//				}
//				set
//				{
//					inputLength = value;
//				}
//			}

		/*
			/// <summary>
			/// The actual amount of data in the input buffer
			/// </summary>
			public int InBufferCount
			{
				get
				{
					if(!isOpen) return 0;

					return prxBuffer;
				}
			}
*/

			/*
			public int OutBufferCount
			{
				get
				{
					if(!isOpen) return 0;

					return ptxBuffer;
				}
			}
			*/
/*
			/// <summary>
			/// The number of bytes that the receive buffer must exceed to trigger a Receive event
			/// </summary>
			public int RThreshold
			{
				get
				{
					return rthreshold;
				}
				set
				{
					rthreshold = value;
				}
			}
*/
			/*
			/// <summary>
			/// The number of bytes that the transmit buffer must exceed to trigger a Transmit event
			/// </summary>
			public int SThreshold
			{
				get
				{
					return sthreshold;
				}
				set
				{
					sthreshold = value;
				}
			}
*/
			/*
			/// <summary>
			/// Send or check for a communications BREAK event
			/// </summary>
			public bool Break
			{
				get 
				{
					if(!isOpen) return false;

					return (brk == 1);
				}		
				set 
				{
					if(!isOpen) return;
					if(brk < 0) return;
					if(hPort == (IntPtr)NativeMethods.INVALID_HANDLE_VALUE) return;

					if (value)
					{
						if (NativeMethods.EscapeCommFunction(hPort, CommEscapes.SETBREAK))
							brk = 1;
						else
							throw new CommPortException("Failed to set break!");
					}
					else
					{
						if (NativeMethods.EscapeCommFunction(hPort, CommEscapes.CLRBREAK))
							brk = 0;
						else
							throw new CommPortException("Failed to clear break!");
					}
				}
			}

			/// <summary>
			/// Returns whether or not the current port support a DTR signal
			/// </summary>
			public bool DTRAvailable
			{
				get
				{
					return dtravail;
				}
			}

			/// <summary>
			/// Gets or sets the current DTR line state (true = 1, false = 0)
			/// </summary>
			public bool DTREnable
			{
				get
				{
					return (dtr == 1);
				}
				set
				{
					if(dtr < 0) return;
					if(hPort == (IntPtr)NativeMethods.INVALID_HANDLE_VALUE) return;

					if (value)
					{
						if (NativeMethods.EscapeCommFunction(hPort, CommEscapes.SETDTR))
							dtr = 1;
						else
							throw new CommPortException("Failed to set DTR!");
					}
					else
					{
						if (NativeMethods.EscapeCommFunction(hPort, CommEscapes.CLRDTR))
							dtr = 0;
						else
							throw new CommPortException("Failed to clear DTR!");
					}
				}
			}			
			/// <summary>
			/// Returns whether or not the current port support an RTS signal
			/// </summary>
			public bool RTSAvailable
			{
				get
				{
					return rtsavail;
				}
			}
			*/

//			public bool RTSEnable
//			{
//				get
//				{
//					return (rts == 1);
//				}
//				set
//				{
//					if(rts < 0) return;
//					if(hPort == (IntPtr)NativeMethods.INVALID_HANDLE_VALUE) return;
//
//					if (value)
//					{
//						if (NativeMethods.EscapeCommFunction(hPort, CommEscapes.SETRTS))
//							rts = 1;
//						else
//							throw new CommPortException("Failed to set RTS!");
//					}
//					else
//					{
//						if (NativeMethods.EscapeCommFunction(hPort, CommEscapes.CLRRTS))
//							rts = 0;
//						else
//							throw new CommPortException("Failed to clear RTS!");
//					}
//				}
//			}
/*
			/// <summary>
			/// Get or Set the Port's DetailedPortSettings
			/// </summary>
			public DetailedPortSettings DetailedSettings
			{
				get
				{
					return portSettings;
				}
				set
				{
					portSettings = value;
				}
			}
			*/
			/*

			/// <summary>
			/// Get or Set the Port's BasicPortSettings
			/// </summary>
			public BasicPortSettings Settings
			{
				get
				{
					return portSettings.BasicSettings;
				}
				set
				{
					portSettings.BasicSettings = value;
				}
			}
*/
			//todo: we need to take this out of a thread and have it on demand instead...
			private void CommEventThread()
			{
				GC.KeepAlive(this);
				CommEventFlags	eventFlags	= new CommEventFlags();
				byte[]			readbuffer	= new Byte[rxBufferSize];
				int				bytesread	= 0;
				AutoResetEvent	rxevent		= new AutoResetEvent(false);

				// specify the set of events to be monitored for the port.
				if((NativeMethods.FullFramework) && m_bOverlappedEnabled) //m_bOverlappedEnabled added by IDENTEC
				{
					NativeMethods.SetCommMask(hPort, CommEventFlags.ALLPC);

					// set up the overlapped IO
					OVERLAPPED o = new OVERLAPPED();
					rxOverlapped = LocalAlloc(0x40, Marshal.SizeOf(o));
					o.Offset = 0; 
					o.OffsetHigh = 0;
					o.hEvent = rxevent.Handle;
					Marshal.StructureToPtr(o, rxOverlapped, true);
				}
				else
				{
					NativeMethods.SetCommMask(hPort, CommEventFlags.ALLCE);
				}
			

				try
				{
					// let Open() know we're started
					threadStarted.Set();

					while(hPort != (IntPtr)NativeMethods.INVALID_HANDLE_VALUE)
					{
						if (m_bOverlappedEnabled) //added by IDENTEC
							// wait for a Comm event
							if(!NativeMethods.WaitCommEvent(hPort, ref eventFlags))
							{
								int e = Marshal.GetLastWin32Error();

								if(e == (int)APIErrors.ERROR_IO_PENDING)
								{
									// IO pending so just wait and try again
									rxevent.WaitOne();
									Thread.Sleep(0);
									continue;
								}

								if(e == (int)APIErrors.ERROR_INVALID_HANDLE)
								{
									// Calling Port.Close() causes hPort to become invalid
									// Since Thread.Abort() is unsupported in the CF, we must
									// accept that calling Close will throw an error here.

									// Close signals the closeEvent, so wait on it
									// We wait 1 second, though Close should happen much sooner
									//int eventResult = NativeMethods.WaitForSingleObject(closeEvent, 1000);
									int eventResult = NativeMethods.WaitForSingleObject(closeEvent, 3000);

									if(eventResult == (int)APIConstants.WAIT_OBJECT_0)
									{
										// the event was set so close was called
										hPort = (IntPtr)NativeMethods.INVALID_HANDLE_VALUE;
					
										// reset our ResetEvent for the next call to Open
										threadStarted.Reset();

										return;
									}
								}

								// WaitCommEvent failed
								// 995 means an exit was requested (thread killed)
								if(e == 995)
								{
									return;
								}
								else
								{
									string error = String.Format("Wait Failed: {0}", e);
									throw new CommPortException(error);
								}
							}

						// Re-specify the set of events to be monitored for the port.
						if(NativeMethods.FullFramework)
						{
							NativeMethods.SetCommMask(hPort, CommEventFlags.ALLPC);
						}
						else
						{
							NativeMethods.SetCommMask(hPort, CommEventFlags.ALLCE);
						}

						// check the event for errors
						if(((uint)eventFlags & (uint)CommEventFlags.ERR) != 0)
						{
							CommErrorFlags errorFlags = new CommErrorFlags();
							CommStat commStat = new CommStat();

							// get the error status
							if(!NativeMethods.ClearCommError(hPort, ref errorFlags, commStat))
							{
								// ClearCommError failed!
								string error = String.Format("ClearCommError Failed: {0}", Marshal.GetLastWin32Error());
								throw new CommPortException(error);
							}

							if(((uint)errorFlags & (uint)CommErrorFlags.BREAK) != 0)
							{
								// BREAK can set an error, so make sure the BREAK bit is set an continue
								eventFlags |= CommEventFlags.BREAK;
							}
							else
							{
								// we have an error.  Build a meaningful string and throw an exception
								StringBuilder s = new StringBuilder("UART Error: ", 80);
								if ((errorFlags & CommErrorFlags.FRAME) != 0) 
								{ s = s.Append("Framing,");	}
								if ((errorFlags & CommErrorFlags.IOE) != 0) 
								{ s = s.Append("IO,"); }
								if ((errorFlags & CommErrorFlags.OVERRUN) != 0)
								{ s = s.Append("Overrun,"); }
								if ((errorFlags & CommErrorFlags.RXOVER) != 0)
								{ s = s.Append("Receive Overflow,"); }
								if ((errorFlags & CommErrorFlags.RXPARITY) != 0)
								{ s = s.Append("Parity,"); }
								if ((errorFlags & CommErrorFlags.TXFULL) != 0)
								{ s = s.Append("Transmit Overflow,"); }

								// no known bits are set
								if(s.Length == 12)
								{ s = s.Append("Unknown"); }
					
								// raise an error event
								if(OnError != null)
									OnError(s.ToString());

								continue;
							}
						} // if(((uint)eventFlags & (uint)CommEventFlags.ERR) != 0)

						// check for status changes
						uint status = 0;
						NativeMethods.GetCommModemStatus(hPort, ref status);

						// check the CTS
						if(((uint)eventFlags & (uint)CommEventFlags.CTS) != 0)
						{
							if(CTSChange != null)
								CTSChange((status & (uint)CommModemStatusFlags.MS_CTS_ON) != 0);
						}

						// check the DSR
						if(((uint)eventFlags & (uint)CommEventFlags.DSR) != 0)
						{
							if(DSRChange != null)
								DSRChange((status & (uint)CommModemStatusFlags.MS_DSR_ON) != 0);
						}
				
						// check for a RING
						if(((uint)eventFlags & (uint)CommEventFlags.RING) != 0)
						{
							if(RingChange != null)
								RingChange((status & (uint)CommModemStatusFlags.MS_RING_ON) != 0);
						}

						// check for a RLSD
						if(((uint)eventFlags & (uint)CommEventFlags.RLSD) != 0)
						{
							if(RLSDChange != null)
								RLSDChange((status & (uint)CommModemStatusFlags.MS_RLSD_ON) != 0);
						}

						// check for TXEMPTY
						if(((uint)eventFlags & (uint)CommEventFlags.TXEMPTY) != 0)
						{
							if(TxDone != null)
								TxDone();
						}

						// check for RXFLAG
						if(((uint)eventFlags & (uint)CommEventFlags.RXFLAG) != 0)
						{
							if(FlagCharReceived != null)
								FlagCharReceived();
						}

						// check for POWER
						if(((uint)eventFlags & (uint)CommEventFlags.POWER) != 0)
						{
							if(PowerEvent != null)
								PowerEvent();
						}

						// check for high-water state
						if((eventFlags & CommEventFlags.RX80FULL) != 0)
						{
							if(HighWater != null)
								HighWater();
						}

						// check for RXCHAR
						if((eventFlags & CommEventFlags.RXCHAR) != 0) 
						{
							do 
							{
								// data came in, put it in the buffer and set the event
								if (!NativeMethods.ReadFile(hPort, readbuffer, rxBufferSize, ref bytesread, rxOverlapped)) 
								{
									int nError = Marshal.GetLastWin32Error();
									string error = String.Format("ReadFile Failed: {0}", nError);
									throw new CommPortException(error, nError);
								}
								if (bytesread >= 1) 
								{
								{
									rxBufferBusy.WaitOne();

									// store the data in buffer and increment the pointer

									if (prxBuffer + bytesread <= readbuffer.Length)
									{ 
										Array.Copy(readbuffer,0, rxBuffer, prxBuffer, bytesread);
										prxBuffer += bytesread;
									}
									else
									{
										Array.Copy(rxBuffer, rxBuffer.Length - bytesread, rxBuffer, 0, rxBuffer.Length - bytesread);
										Array.Copy(readbuffer, 0, rxBuffer, prxBuffer, bytesread);
										prxBuffer = rxBuffer.Length;
										if (RxOverrun != null)
											RxOverrun();
									}

									rxBufferBusy.ReleaseMutex();

									// prxBuffer gets reset when the Input value is read. (FIFO)

									// fire the DataReceived event every RThreshold bytes
									if(rthreshold != 0 & readbuffer.Length >= rthreshold)
										DataReceived();
								}
								}
							} while (bytesread > 0);
						} // if((eventFlags & CommEventFlags.RXCHAR) != 0) 
					} // while(true)
				} // try
				catch
				{
					if(rxOverlapped != IntPtr.Zero) 
						LocalFree(rxOverlapped);

					throw;
				}
			}
		}
	}
}

