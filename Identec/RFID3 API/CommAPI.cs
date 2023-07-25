//==========================================================================================
//		MODIFIED BY IDENTEC SOLUTIONS INC. APRIL 26, 2004
//		namespace OpenNETCF.IO.Serial.NativeMethods
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
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace IDENTEC
{
	[StructLayout(LayoutKind.Sequential)]
	internal class CommTimeouts 
	{
		public UInt32 ReadIntervalTimeout;
		public UInt32 ReadTotalTimeoutMultiplier;
		public UInt32 ReadTotalTimeoutConstant;
		public UInt32 WriteTotalTimeoutMultiplier;
		public UInt32 WriteTotalTimeoutConstant;
	}

	#region API structs and enums

	[StructLayout( LayoutKind.Sequential )] 
	internal struct OVERLAPPED 
	{
		internal UIntPtr Internal;
		internal UIntPtr InternalHigh;
		internal UInt32 Offset;
		internal UInt32 OffsetHigh;
		internal IntPtr hEvent;
	}

	/// <summary>
	/// Event Flags
	/// </summary>
	[Flags]
	internal enum CommEventFlags : int
	{
		/// <summary>
		/// No flags
		/// </summary>
		NONE        = 0x0000, //
		/// <summary>
		/// Event on receive
		/// </summary>
		RXCHAR      = 0x0001, // Any Character received
		/// <summary>
		/// Event when specific character is received
		/// </summary>
		RXFLAG      = 0x0002, // Received specified flag character
		/// <summary>
		/// Event when the transmit buffer is empty
		/// </summary>
		TXEMPTY     = 0x0004, // Tx buffer Empty
		/// <summary>
		/// Event on CTS state change
		/// </summary>
		CTS         = 0x0008, // CTS changed
		/// <summary>
		/// Event on DSR state change
		/// </summary>
		DSR         = 0x0010, // DSR changed
		/// <summary>
		/// Event on RLSD state change
		/// </summary>
		RLSD        = 0x0020, // RLSD changed
		/// <summary>
		/// Event on BREAK
		/// </summary>
		BREAK       = 0x0040, // BREAK received
		/// <summary>
		/// Event on line error
		/// </summary>
		ERR         = 0x0080, // Line status error
		/// <summary>
		/// Event on ring detect
		/// </summary>
		RING        = 0x0100, // ring detected
		/// <summary>
		/// Event on printer error
		/// </summary>
		PERR        = 0x0200, // printer error
		/// <summary>
		/// Event on 80% high-water
		/// </summary>
		RX80FULL    = 0x0400, // rx buffer is at 80%
		/// <summary>
		/// Provider event 1
		/// </summary>
		EVENT1      = 0x0800, // provider event
		/// <summary>
		/// Provider event 2
		/// </summary>
		EVENT2      = 0x1000, // provider event
		/// <summary>
		/// Event on CE power notification
		/// </summary>
		POWER       = 0x2000, // wince power notification
		/// <summary>
		/// Mask for all flags under CE
		/// </summary>
		ALLCE			= 0x3FFF,  // mask of all flags for CE
		/// <summary>
		/// Mask for all flags under desktop Windows
		/// </summary>
		ALLPC			= BREAK | CTS | DSR | ERR | RING | RLSD | RXCHAR | RXFLAG | TXEMPTY 
	}

	internal enum EventFlags
	{
		EVENT_PULSE     = 1,
		EVENT_RESET     = 2,
		EVENT_SET       = 3
	}

	/// <summary>
	/// Error flags
	/// </summary>
	[Flags]
	internal enum CommErrorFlags : int
	{
		/// <summary>
		/// Receive overrun
		/// </summary>
		RXOVER = 0x0001,
		/// <summary>
		/// Overrun
		/// </summary>
		OVERRUN = 0x0002,
		/// <summary>
		/// Parity error
		/// </summary>
		RXPARITY = 0x0004,
		/// <summary>
		/// Frame error
		/// </summary>
		FRAME = 0x0008,
		/// <summary>
		/// BREAK received
		/// </summary>
		BREAK = 0x0010,
		/// <summary>
		/// Transmit buffer full
		/// </summary>
		TXFULL = 0x0100,
		/// <summary>
		/// IO Error
		/// </summary>
		IOE = 0x0400,
		/// <summary>
		/// Requested mode not supported
		/// </summary>
		MODE = 0x8000
	}

	/// <summary>
	/// Modem status flags
	/// </summary>
	[Flags]
	internal enum CommModemStatusFlags : int
	{		
		/// <summary>
		/// The CTS (Clear To Send) signal is on.
		/// </summary>
		MS_CTS_ON	= 0x0010,
		/// <summary>
		/// The DSR (Data Set Ready) signal is on.
		/// </summary>
		MS_DSR_ON	= 0x0020,
		/// <summary>
		/// The ring indicator signal is on.
		/// </summary>
		MS_RING_ON	= 0x0040, 
		/// <summary>
		/// The RLSD (Receive Line Signal Detect) signal is on.
		/// </summary>
		MS_RLSD_ON	= 0x0080
	}

	/// <summary>
	/// Communication escapes
	/// </summary>
	internal enum CommEscapes : uint
	{
		/// <summary>
		/// Causes transmission to act as if an XOFF character has been received.
		/// </summary>
		SETXOFF		= 1,
		/// <summary>
		/// Causes transmission to act as if an XON character has been received.
		/// </summary>
		SETXON		= 2,
		/// <summary>
		/// Sends the RTS (Request To Send) signal.
		/// </summary>
		SETRTS		= 3,
		/// <summary>
		/// Clears the RTS (Request To Send) signal
		/// </summary>
		CLRRTS		= 4,
		/// <summary>
		/// Sends the DTR (Data Terminal Ready) signal.
		/// </summary>
		SETDTR		= 5,
		/// <summary>
		/// Clears the DTR (Data Terminal Ready) signal.
		/// </summary>
		CLRDTR		= 6,
		/// <summary>
		/// Suspends character transmission and places the transmission line in a break state until the ClearCommBreak function is called (or EscapeCommFunction is called with the CLRBREAK extended function code). The SETBREAK extended function code is identical to the SetCommBreak function. This extended function does not flush data that has not been transmitted.
		/// </summary>
		SETBREAK	= 8,
		/// <summary>
		/// Restores character transmission and places the transmission line in a nonbreak state. The CLRBREAK extended function code is identical to the ClearCommBreak function
		/// </summary>
		CLRBREAK	= 9,
		///Set the port to IR mode.
		SETIR		= 10,
		/// <summary>
		/// Set the port to non-IR mode.
		/// </summary>
		CLRIR		= 11
	}

	/// <summary>
	/// Error values from serial API calls
	/// </summary>
	internal enum APIErrors : int
	{
		/// <summary>
		/// Port not found
		/// </summary>
		ERROR_FILE_NOT_FOUND	= 2,
		/// <summary>
		/// Invalid port name
		/// </summary>
		ERROR_INVALID_NAME		= 123,
		/// <summary>
		/// Access denied
		/// </summary>
		ERROR_ACCESS_DENIED		= 5,
		/// <summary>
		/// invalid handle
		/// </summary>
		ERROR_INVALID_HANDLE	= 6,
		/// <summary>
		/// IO pending
		/// </summary>
		ERROR_IO_PENDING		= 997
	}

	internal enum APIConstants : uint
	{
		WAIT_OBJECT_0   	= 0x00000000,
		WAIT_ABANDONED  	= 0x00000080,
		WAIT_ABANDONED_0	= 0x00000080,
		WAIT_FAILED         = 0xffffffff,
		INFINITE            = 0xffffffff,	

		PURGE_TXABORT       = 0x0001,  // Kill the pending/current writes to the comm port.
		PURGE_RXABORT       = 0x0002,  // Kill the pending/current reads to the comm port.
		PURGE_TXCLEAR       = 0x0004,  // Kill the transmit queue if there.
		PURGE_RXCLEAR       = 0x0008,  // Kill the typeahead buffer if there.
		PURGE_ALL			= 1 | 2 | 4 | 8

	}
	#endregion

	[StructLayout(LayoutKind.Sequential)]
	internal class CommStat 
	{
		//
		// typedef struct _COMSTAT {
		//     DWORD fCtsHold : 1;
		//     DWORD fDsrHold : 1;
		//     DWORD fRlsdHold : 1;
		//     DWORD fXoffHold : 1;
		//     DWORD fXoffSent : 1;
		//     DWORD fEof : 1;
		//     DWORD fTxim : 1;
		//     DWORD fReserved : 25;
		//     DWORD cbInQue;
		//     DWORD cbOutQue;
		// } COMSTAT, *LPCOMSTAT;
		//

		//
		// Since the structure contains a bit-field, use a UInt32 to contain
		// the bit field and then use properties to expose the individual
		// bits as a bool.
		//
		private UInt32 bitfield;
		public UInt32 cbInQue	= 0;
		public UInt32 cbOutQue	= 0;

		// Helper constants for manipulating the bit fields.
		private readonly UInt32 fCtsHoldMask    = 0x00000001;
		private readonly Int32 fCtsHoldShift    = 0;
		private readonly UInt32 fDsrHoldMask    = 0x00000002;
		private readonly Int32 fDsrHoldShift    = 1;
		private readonly UInt32 fRlsdHoldMask   = 0x00000004;
		private readonly Int32 fRlsdHoldShift   = 2;
		private readonly UInt32 fXoffHoldMask   = 0x00000008;
		private readonly Int32 fXoffHoldShift   = 3;
		private readonly UInt32 fXoffSentMask   = 0x00000010;
		private readonly Int32 fXoffSentShift   = 4;
		private readonly UInt32 fEofMask        = 0x00000020;
		private readonly Int32 fEofShift        = 5;        
		private readonly UInt32 fTximMask       = 0x00000040;
		private readonly Int32 fTximShift       = 6;
/*
		public bool fCtsHold 
		{
			get { return ((bitfield & fCtsHoldMask) != 0); }
			set { bitfield |= (Convert.ToUInt32(value) << fCtsHoldShift); }
		}
		public bool fDsrHold 
		{
			get { return ((bitfield & fDsrHoldMask) != 0); }
			set { bitfield |= (Convert.ToUInt32(value) << fDsrHoldShift); }
		}
		public bool fRlsdHold 
		{
			get { return ((bitfield & fRlsdHoldMask) != 0); }
			set { bitfield |= (Convert.ToUInt32(value) << fRlsdHoldShift); }
		}
		public bool fXoffHold 
		{
			get { return ((bitfield & fXoffHoldMask) != 0); }
			set { bitfield |= (Convert.ToUInt32(value) << fXoffHoldShift); }
		}
		public bool fXoffSent 
		{
			get { return ((bitfield & fXoffSentMask) != 0); }
			set { bitfield |= (Convert.ToUInt32(value) << fXoffSentShift); }
		}
		public bool fEof 
		{
			get { return ((bitfield & fEofMask) != 0); }
			set { bitfield |= (Convert.ToUInt32(value) << fEofShift); }
		}
		public bool fTxim 
		{
			get { return ((bitfield & fTximMask) != 0); }
			set { bitfield |= (Convert.ToUInt32(value) << fTximShift); }
		}
		*/
	}

	internal class NativeMethods // was CommAPI
	{
		private NativeMethods()
		{
		}
		private const UInt32 FILE_FLAG_OVERLAPPED = 0x40000000;

		// These functions wrap the P/Invoked API calls and:
		// - make the correct call based on whether we're running under the full or compact framework
		// - eliminate empty parameters and defaults
		//		
		internal static IntPtr CreateFile(string FileName, bool bOverlapped)
		{
			uint access = GENERIC_WRITE | GENERIC_READ;

			if(FullFramework)
			{
				if (bOverlapped)
					return WinCreateFileW(FileName, access, 0, IntPtr.Zero, OPEN_EXISTING, FILE_FLAG_OVERLAPPED, IntPtr.Zero);
				else
					return WinCreateFileW(FileName, access, 0, IntPtr.Zero, OPEN_EXISTING, 0, IntPtr.Zero);
			}
			else
			{
				return CECreateFileW(FileName, access, 0, IntPtr.Zero, OPEN_EXISTING, 0, IntPtr.Zero);
			}
		}

		internal static bool WaitCommEvent(IntPtr hPort, ref CommEventFlags flags) 
		{
			if (FullFramework) 
			{
				return Convert.ToBoolean(WinWaitCommEvent(hPort, ref flags, IntPtr.Zero));
			} 
			else 
			{
				return Convert.ToBoolean(CEWaitCommEvent(hPort, ref flags, IntPtr.Zero));
			}
		}

		internal static bool ClearCommError(IntPtr hPort, ref CommErrorFlags flags, CommStat stat) 
		{
			if (FullFramework) 
			{
				return Convert.ToBoolean(WinClearCommError(hPort, ref flags, stat));
			} 
			else 
			{
				return Convert.ToBoolean(CEClearCommError(hPort, ref flags, stat));
			}
		}

		internal static bool GetCommModemStatus(IntPtr hPort, ref uint lpModemStat)
		{
			if (FullFramework) 
			{
				return Convert.ToBoolean(WinGetCommModemStatus(hPort, ref lpModemStat));
			} 
			else 
			{
				return Convert.ToBoolean(CEGetCommModemStatus(hPort, ref lpModemStat));
			}
		}

		internal static bool SetCommMask(IntPtr hPort, CommEventFlags dwEvtMask) 
		{
			if (FullFramework) 
			{
				return Convert.ToBoolean(WinSetCommMask(hPort, dwEvtMask));
			} 
			else 
			{
				return Convert.ToBoolean(CESetCommMask(hPort, dwEvtMask));
			}
		}	

		internal static bool ReadFile(IntPtr hPort, byte[] buffer, int cbToRead, ref Int32 cbRead, IntPtr lpOverlapped) 
		{
			if (FullFramework) 
			{
				//return Convert.ToBoolean(WinReadFile(hPort, buffer, cbToRead, ref cbRead, lpOverlapped));
				bool b = Convert.ToBoolean(WinReadFile(hPort, buffer, cbToRead, ref cbRead, lpOverlapped));
#if DEBUG
				/* --sometimes the entire buffer is full of 255... not sure why the error code is "Incorrect function."
				if (cbRead !=0)
				if (cbToRead == cbToRead)
				{
					bool bGoodToGo = false;
					// Test every byte for the weird 0xFF error we get...
					for (int i = 0; i < cbRead; i++)
					{
						if (buffer[i] != 0xFF)
						{
							bGoodToGo = true;
							break;
						}
					}
					if (!bGoodToGo)
					{
						string error = String.Format("ReadFile Failed: {0}", Marshal.GetLastWin32Error());
						throw new Exception(error);
					}
				}
				*/			
			
#endif
				return b;
			} 
			else 
			{
				return Convert.ToBoolean(CEReadFile(hPort, buffer, cbToRead, ref cbRead, IntPtr.Zero));
			}
		}		

		internal static bool WriteFile(IntPtr hPort, byte[] buffer, Int32 cbToWrite, ref Int32 cbWritten, IntPtr lpOverlapped) 
		{
			if (FullFramework) 
			{
				WinPurgeComm(hPort, (uint)APIConstants.PURGE_TXCLEAR);
				return Convert.ToBoolean(WinWriteFile(hPort, buffer, cbToWrite, ref cbWritten, lpOverlapped));
			} 
			else 
			{
				CEPurgeComm(hPort, (uint)APIConstants.PURGE_TXCLEAR);
				return Convert.ToBoolean(CEWriteFile(hPort, buffer, cbToWrite, ref cbWritten, IntPtr.Zero));
			}
		}

		internal static bool CloseHandle(IntPtr hPort) 
		{
			if (FullFramework) 
			{
				return Convert.ToBoolean(WinCloseHandle(hPort));
			} 
			else 
			{
				return Convert.ToBoolean(CECloseHandle(hPort));
			}
		}

		internal static bool SetupComm(IntPtr hPort, Int32 dwInQueue, Int32 dwOutQueue)
		{
			if (FullFramework) 
			{
				return Convert.ToBoolean(WinSetupComm(hPort, dwInQueue, dwOutQueue));
			} 
			else 
			{
				return Convert.ToBoolean(CESetupComm(hPort, dwInQueue, dwOutQueue));
			}
		}

		internal static bool SetCommState(IntPtr hPort, DCB dcb) 
		{
			if (FullFramework) 
			{
				return Convert.ToBoolean(WinSetCommState(hPort, dcb));
			} 
			else 
			{
				return Convert.ToBoolean(CESetCommState(hPort, dcb));
			}
		}

//		internal static bool GetCommState(IntPtr hPort, DCB dcb) 
//		{
//			if (FullFramework) 
//			{
//				return Convert.ToBoolean(WinGetCommState(hPort, dcb));
//			} 
//			else 
//			{
//				return Convert.ToBoolean(CEGetCommState(hPort, dcb));
//			}
//		}

		internal static bool SetCommTimeouts(IntPtr hPort, CommTimeouts timeouts) 
		{
			
			if (FullFramework) 
			{
				WinPurgeComm(hPort, (uint)APIConstants.PURGE_ALL );
				return Convert.ToBoolean(WinSetCommTimeouts(hPort, timeouts));
			} 
			else 
			{
				CEPurgeComm(hPort, (uint)APIConstants.PURGE_ALL );
				return Convert.ToBoolean(CESetCommTimeouts(hPort, timeouts));
			}
		}
		
//		internal static bool EscapeCommFunction(IntPtr hPort, CommEscapes escape)
//		{
//			if (FullFramework) 
//			{
//				return Convert.ToBoolean(WinEscapeCommFunction(hPort, (uint)escape));
//			} 
//			else 
//			{
//				return Convert.ToBoolean(CEEscapeCommFunction(hPort, (uint)escape));
//			}
//		}

		internal static IntPtr CreateEvent(bool bManualReset, bool bInitialState, string lpName)
		{
			if (FullFramework) 
			{
				return WinCreateEvent(IntPtr.Zero, Convert.ToInt32(bManualReset), Convert.ToInt32(bInitialState), lpName);
			} 
			else 
			{
				return CECreateEvent(IntPtr.Zero, Convert.ToInt32(bManualReset), Convert.ToInt32(bInitialState), lpName);
			}
		}

		internal static bool SetEvent(IntPtr hEvent)
		{
			if (FullFramework) 
			{
				return Convert.ToBoolean(WinSetEvent(hEvent));
			} 
			else 
			{
				return Convert.ToBoolean(CEEventModify(hEvent, (uint)EventFlags.EVENT_SET));
			}
		}

//		internal static bool ResetEvent(IntPtr hEvent)
//		{
//			if (FullFramework) 
//			{
//				return Convert.ToBoolean(WinResetEvent(hEvent));
//			} 
//			else 
//			{
//				return Convert.ToBoolean(CEEventModify(hEvent, (uint)EventFlags.EVENT_RESET));
//			}
//		}

//		internal static bool PulseEvent(IntPtr hEvent)
//		{
//			if (FullFramework) 
//			{
//				return Convert.ToBoolean(WinPulseEvent(hEvent));
//			} 
//			else 
//			{
//				return Convert.ToBoolean(CEEventModify(hEvent, (uint)EventFlags.EVENT_PULSE));
//			}
//		}

		internal static int WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds)
		{
			if (FullFramework) 
			{
				return WinWaitForSingleObject(hHandle, dwMilliseconds);
			} 
			else 
			{
				return CEWaitForSingleObject(hHandle, dwMilliseconds);
			}
		}

		static internal System.PlatformID m_platform = System.Environment.OSVersion.Platform;
		#region Helper methods
		internal static bool FullFramework
		{
			get{return /*System.Environment.OSVersion.Platform*/ m_platform != PlatformID.WinCE;}
		}
		#endregion

		#region API Constants
		internal const Int32 INVALID_HANDLE_VALUE = -1;
		internal const UInt32 OPEN_EXISTING = 3;
		internal const UInt32 GENERIC_READ = 0x80000000;
		internal const UInt32 GENERIC_WRITE = 0x40000000;
		#endregion

		#region Windows CE API imports

		//Added by IDENTEC
		[DllImport("coredll.dll", EntryPoint="PurgeComm", SetLastError = true)]
		private static extern int CEPurgeComm(IntPtr hHandle, uint dwFlags); 

		[DllImport("coredll.dll", EntryPoint="WaitForSingleObject", SetLastError = true)]
		private static extern int CEWaitForSingleObject(IntPtr hHandle, uint dwMilliseconds); 

		[DllImport("coredll.dll", EntryPoint="EventModify", SetLastError = true)]
		private static extern int CEEventModify(IntPtr hEvent, uint function); 

		[DllImport("coredll.dll", EntryPoint="CreateEvent", SetLastError = true)]
		private static extern IntPtr CECreateEvent(IntPtr lpEventAttributes, int bManualReset, int bInitialState, string lpName); 

//		[DllImport("coredll.dll", EntryPoint="EscapeCommFunction", SetLastError = true)]
//		private static extern int CEEscapeCommFunction(IntPtr hFile, UInt32 dwFunc);

		[DllImport("coredll.dll", EntryPoint="SetCommTimeouts", SetLastError = true)]
		private static extern int CESetCommTimeouts(IntPtr hFile, CommTimeouts timeouts);

		/*
		[DllImport("coredll.dll", EntryPoint="GetCommState", SetLastError = true)]
		private static extern int CEGetCommState(IntPtr hFile, DCB dcb);
		*/

		[DllImport("coredll.dll", EntryPoint="SetCommState", SetLastError = true)]
		private static extern int CESetCommState(IntPtr hFile, DCB dcb);

		[DllImport("coredll.dll", EntryPoint="SetupComm", SetLastError = true)]
		private static extern int CESetupComm(IntPtr hFile, Int32 dwInQueue, Int32 dwOutQueue);

		[DllImport("coredll.dll", EntryPoint="CloseHandle", SetLastError = true)]
		private static extern int CECloseHandle(IntPtr hObject);

		[DllImport("coredll.dll", EntryPoint="WriteFile", SetLastError = true)]
		private static extern int CEWriteFile(IntPtr hFile, byte[] lpBuffer, Int32 nNumberOfBytesToRead, ref Int32 lpNumberOfBytesRead, IntPtr lpOverlapped);

		[DllImport("coredll.dll", EntryPoint="ReadFile", SetLastError = true)]
		private static extern int CEReadFile(IntPtr hFile, byte[] lpBuffer, Int32 nNumberOfBytesToRead, ref Int32 lpNumberOfBytesRead, IntPtr lpOverlapped);

		[DllImport("coredll.dll", EntryPoint="SetCommMask", SetLastError = true)]
		private static extern int CESetCommMask(IntPtr handle, CommEventFlags dwEvtMask);

		[DllImport("coredll.dll", EntryPoint="GetCommModemStatus", SetLastError = true)]
		extern private static int CEGetCommModemStatus(IntPtr hFile, ref uint lpModemStat);

		[DllImport("coredll.dll", EntryPoint="ClearCommError", SetLastError = true)]
		extern private static int CEClearCommError(IntPtr hFile, ref CommErrorFlags lpErrors, CommStat lpStat);

		[DllImport("coredll.dll", EntryPoint="WaitCommEvent", SetLastError = true)]
		private static extern int CEWaitCommEvent(IntPtr hFile, ref CommEventFlags lpEvtMask, IntPtr lpOverlapped);
		
		[DllImport("coredll.dll", EntryPoint="CreateFileW", SetLastError = true)]
		private static extern IntPtr CECreateFileW(
			String lpFileName, UInt32 dwDesiredAccess, UInt32 dwShareMode,
			IntPtr lpSecurityAttributes, UInt32 dwCreationDisposition, UInt32 dwFlagsAndAttributes,
			IntPtr hTemplateFile);

		#endregion

		#region Desktop Windows API imports

		//Added by IDENTEC
		[DllImport("kernel32.dll", EntryPoint="PurgeComm", SetLastError = true)]
		private static extern int WinPurgeComm(IntPtr hHandle, uint dwFlags); 


		[DllImport("kernel32.dll", EntryPoint="WaitForSingleObject", SetLastError = true)]
		private static extern int WinWaitForSingleObject(IntPtr hHandle, uint dwMilliseconds); 

		[DllImport("kernel32.dll", EntryPoint="SetEvent", SetLastError = true)]
		private static extern int WinSetEvent(IntPtr hEvent); 

//		[DllImport("kernel32.dll", EntryPoint="ResetEvent", SetLastError = true)]
//		private static extern int WinResetEvent(IntPtr hEvent); 

		/*
		[DllImport("kernel32.dll", EntryPoint="PulseEvent", SetLastError = true)]
		private static extern int WinPulseEvent(IntPtr hEvent); 
		*/

		[DllImport("kernel32.dll", EntryPoint="CreateEvent", SetLastError = true)]
		private static extern IntPtr WinCreateEvent(IntPtr lpEventAttributes, int bManualReset, int bInitialState, string lpName); 

//		[DllImport("kernel32.dll", EntryPoint="EscapeCommFunction", SetLastError = true)]
//		private static extern int WinEscapeCommFunction(IntPtr hFile, UInt32 dwFunc);

		[DllImport("kernel32.dll", EntryPoint="SetCommTimeouts", SetLastError = true)]
		private static extern int WinSetCommTimeouts(IntPtr hFile, CommTimeouts timeouts);

		/*
		[DllImport("kernel32.dll", EntryPoint="GetCommState", SetLastError = true)]
		private static extern int WinGetCommState(IntPtr hFile, DCB dcb);
		*/
		
		[DllImport("kernel32.dll", EntryPoint="SetCommState", SetLastError = true)]
		private static extern int WinSetCommState(IntPtr hFile, DCB dcb);

		[DllImport("kernel32.dll", EntryPoint="SetupComm", SetLastError = true)]
		private static extern int WinSetupComm(IntPtr hFile, Int32 dwInQueue, Int32 dwOutQueue);

		[DllImport("kernel32.dll", EntryPoint="CloseHandle", SetLastError = true)]
		private static extern int WinCloseHandle(IntPtr hObject);

		[DllImport("kernel32.dll", EntryPoint="WriteFile", SetLastError = true)]
		extern private static int WinWriteFile(IntPtr hFile, byte[] lpBuffer, Int32 nNumberOfBytesToRead, ref Int32 lpNumberOfBytesRead, IntPtr lpOverlapped);

		[DllImport("kernel32.dll", EntryPoint="ReadFile", SetLastError = true)]
		private static extern int WinReadFile(IntPtr hFile, byte[] lpBuffer, Int32 nNumberOfBytesToRead, ref Int32 lpNumberOfBytesRead, IntPtr lpOverlapped);

		[DllImport("kernel32.dll", EntryPoint="SetCommMask", SetLastError = true)]
		private static extern int WinSetCommMask(IntPtr handle, CommEventFlags dwEvtMask);

		[DllImport("kernel32.dll", EntryPoint="GetCommModemStatus", SetLastError = true)]
		extern private static int WinGetCommModemStatus(IntPtr hFile, ref uint lpModemStat);

		[DllImport("kernel32.dll", EntryPoint="ClearCommError", SetLastError = true)]
		extern private static int WinClearCommError(IntPtr hFile, ref CommErrorFlags lpErrors, CommStat lpStat);

		[DllImport("kernel32.dll", EntryPoint="CreateFileW", SetLastError = true, CharSet = CharSet.Unicode)]
		private static extern IntPtr WinCreateFileW(String lpFileName, UInt32 dwDesiredAccess, UInt32 dwShareMode,
			IntPtr lpSecurityAttributes, UInt32 dwCreationDisposition, UInt32 dwFlagsAndAttributes,
			IntPtr hTemplateFile);

		[DllImport("kernel32.dll", EntryPoint="WaitCommEvent", SetLastError = true)]
		private static extern int WinWaitCommEvent(IntPtr hFile, ref CommEventFlags lpEvtMask, IntPtr lpOverlapped);


		#endregion
	}
}


