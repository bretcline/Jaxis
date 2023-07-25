namespace IDENTEC
{
    using System;
    using System.Runtime.InteropServices;

    internal class NativeMethods
    {
        private const uint FILE_FLAG_OVERLAPPED = 0x40000000;
        internal const uint GENERIC_READ = 0x80000000;
        internal const uint GENERIC_WRITE = 0x40000000;
        internal const int INVALID_HANDLE_VALUE = -1;
        internal static PlatformID m_platform = Environment.OSVersion.Platform;
        internal const uint OPEN_EXISTING = 3;

        private NativeMethods()
        {
        }

        [DllImport("coredll.dll", EntryPoint="ClearCommError", SetLastError=true)]
        private static extern int CEClearCommError(IntPtr hFile, ref CommErrorFlags lpErrors, CommStat lpStat);
        [DllImport("coredll.dll", EntryPoint="CloseHandle", SetLastError=true)]
        private static extern int CECloseHandle(IntPtr hObject);
        [DllImport("coredll.dll", EntryPoint="CreateEvent", SetLastError=true)]
        private static extern IntPtr CECreateEvent(IntPtr lpEventAttributes, int bManualReset, int bInitialState, string lpName);
        [DllImport("coredll.dll", EntryPoint="CreateFileW", SetLastError=true)]
        private static extern IntPtr CECreateFileW(string lpFileName, uint dwDesiredAccess, uint dwShareMode, IntPtr lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);
        [DllImport("coredll.dll", EntryPoint="EventModify", SetLastError=true)]
        private static extern int CEEventModify(IntPtr hEvent, uint function);
        [DllImport("coredll.dll", EntryPoint="GetCommModemStatus", SetLastError=true)]
        private static extern int CEGetCommModemStatus(IntPtr hFile, ref uint lpModemStat);
        [DllImport("coredll.dll", EntryPoint="GetCommState", SetLastError=true)]
        private static extern int CEGetCommState(IntPtr hFile, DCB dcb);
        [DllImport("coredll.dll", EntryPoint="PurgeComm", SetLastError=true)]
        private static extern int CEPurgeComm(IntPtr hHandle, uint dwFlags);
        [DllImport("coredll.dll", EntryPoint="ReadFile", SetLastError=true)]
        private static extern int CEReadFile(IntPtr hFile, byte[] lpBuffer, int nNumberOfBytesToRead, ref int lpNumberOfBytesRead, IntPtr lpOverlapped);
        [DllImport("coredll.dll", EntryPoint="SetCommMask", SetLastError=true)]
        private static extern int CESetCommMask(IntPtr handle, CommEventFlags dwEvtMask);
        [DllImport("coredll.dll", EntryPoint="SetCommState", SetLastError=true)]
        private static extern int CESetCommState(IntPtr hFile, DCB dcb);
        [DllImport("coredll.dll", EntryPoint="SetCommTimeouts", SetLastError=true)]
        private static extern int CESetCommTimeouts(IntPtr hFile, CommTimeouts timeouts);
        [DllImport("coredll.dll", EntryPoint="SetupComm", SetLastError=true)]
        private static extern int CESetupComm(IntPtr hFile, int dwInQueue, int dwOutQueue);
        [DllImport("coredll.dll", EntryPoint="WaitCommEvent", SetLastError=true)]
        private static extern int CEWaitCommEvent(IntPtr hFile, ref CommEventFlags lpEvtMask, IntPtr lpOverlapped);
        [DllImport("coredll.dll", EntryPoint="WaitForSingleObject", SetLastError=true)]
        private static extern int CEWaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);
        [DllImport("coredll.dll", EntryPoint="WriteFile", SetLastError=true)]
        private static extern int CEWriteFile(IntPtr hFile, byte[] lpBuffer, int nNumberOfBytesToRead, ref int lpNumberOfBytesRead, IntPtr lpOverlapped);
        internal static bool ClearCommError(IntPtr hPort, ref CommErrorFlags flags, CommStat stat)
        {
            if (FullFramework)
            {
                return Convert.ToBoolean(WinClearCommError(hPort, ref flags, stat));
            }
            return Convert.ToBoolean(CEClearCommError(hPort, ref flags, stat));
        }

        internal static bool CloseHandle(IntPtr hPort)
        {
            if (FullFramework)
            {
                return Convert.ToBoolean(WinCloseHandle(hPort));
            }
            return Convert.ToBoolean(CECloseHandle(hPort));
        }

        internal static IntPtr CreateEvent(bool bManualReset, bool bInitialState, string lpName)
        {
            if (FullFramework)
            {
                return WinCreateEvent(IntPtr.Zero, Convert.ToInt32(bManualReset), Convert.ToInt32(bInitialState), lpName);
            }
            return CECreateEvent(IntPtr.Zero, Convert.ToInt32(bManualReset), Convert.ToInt32(bInitialState), lpName);
        }

        internal static IntPtr CreateFile(string FileName, bool bOverlapped)
        {
            uint dwDesiredAccess = 0xc0000000;
            if (!FullFramework)
            {
                return CECreateFileW(FileName, dwDesiredAccess, 0, IntPtr.Zero, 3, 0, IntPtr.Zero);
            }
            if (bOverlapped)
            {
                return WinCreateFileW(FileName, dwDesiredAccess, 0, IntPtr.Zero, 3, 0x40000000, IntPtr.Zero);
            }
            return WinCreateFileW(FileName, dwDesiredAccess, 0, IntPtr.Zero, 3, 0, IntPtr.Zero);
        }

        internal static bool GetCommModemStatus(IntPtr hPort, ref uint lpModemStat)
        {
            if (FullFramework)
            {
                return Convert.ToBoolean(WinGetCommModemStatus(hPort, ref lpModemStat));
            }
            return Convert.ToBoolean(CEGetCommModemStatus(hPort, ref lpModemStat));
        }

        internal static bool GetCommState(IntPtr hPort, DCB dcb)
        {
            if (FullFramework)
            {
                return Convert.ToBoolean(WinGetCommState(hPort, dcb));
            }
            return Convert.ToBoolean(CEGetCommState(hPort, dcb));
        }

        internal static bool PurgeRx(IntPtr hPort)
        {
            if (FullFramework)
            {
                WinPurgeComm(hPort, 8);
            }
            else
            {
                CEPurgeComm(hPort, 8);
            }
            return true;
        }

        internal static bool ReadFile(IntPtr hPort, byte[] buffer, int cbToRead, ref int cbRead, IntPtr lpOverlapped)
        {
            if (FullFramework)
            {
                if (WinReadFile(hPort, buffer, cbToRead, ref cbRead, lpOverlapped) == 0)
                {
                    ThrowOnWin32ErrorHelper();
                    return false;
                }
                return true;
            }
            if (CEReadFile(hPort, buffer, cbToRead, ref cbRead, IntPtr.Zero) == 0)
            {
                ThrowOnWin32ErrorHelper();
                return false;
            }
            return true;
        }

        internal static bool SetCommMask(IntPtr hPort, CommEventFlags dwEvtMask)
        {
            if (FullFramework)
            {
                return Convert.ToBoolean(WinSetCommMask(hPort, dwEvtMask));
            }
            return Convert.ToBoolean(CESetCommMask(hPort, dwEvtMask));
        }

        internal static bool SetCommState(IntPtr hPort, DCB dcb)
        {
            if (FullFramework)
            {
                return Convert.ToBoolean(WinSetCommState(hPort, dcb));
            }
            return Convert.ToBoolean(CESetCommState(hPort, dcb));
        }

        internal static bool SetCommTimeouts(IntPtr hPort, CommTimeouts timeouts)
        {
            if (FullFramework)
            {
                WinPurgeComm(hPort, 15);
                return Convert.ToBoolean(WinSetCommTimeouts(hPort, timeouts));
            }
            CEPurgeComm(hPort, 15);
            return Convert.ToBoolean(CESetCommTimeouts(hPort, timeouts));
        }

        internal static bool SetEvent(IntPtr hEvent)
        {
            if (FullFramework)
            {
                return Convert.ToBoolean(WinSetEvent(hEvent));
            }
            return Convert.ToBoolean(CEEventModify(hEvent, 3));
        }

        internal static bool SetupComm(IntPtr hPort, int dwInQueue, int dwOutQueue)
        {
            if (FullFramework)
            {
                return Convert.ToBoolean(WinSetupComm(hPort, dwInQueue, dwOutQueue));
            }
            return Convert.ToBoolean(CESetupComm(hPort, dwInQueue, dwOutQueue));
        }

        internal static void ThrowOnWin32ErrorHelper()
        {
            ThrowOnWin32ErrorHelper(Marshal.GetLastWin32Error());
        }

        internal static void ThrowOnWin32ErrorHelper(int nError)
        {
            switch (nError)
            {
                case 2:
                    throw new CommPortException("The device is unavailable or could not be detected", nError);

                case 5:
                    throw new CommPortException("The device cannot be accessed anymore.", nError);

                case 0x15:
                    throw new CommPortException("The device is not ready.", nError);

                case 0x20:
                    throw new CommPortException("The device is being used by another process.", nError);

                case 0x37:
                    throw new CommPortException("The specified network resource or device is no longer available", nError);

                case 0x48f:
                    throw new CommPortException("The specified network resource or device is no longer connected", nError);

                case 0x649:
                    throw new CommPortException("File/device access failed: Handle is in an invalid state.", nError);
            }
            throw new CommPortException(string.Format("Win32 Error: {0}", nError), nError);
        }

        internal static bool WaitCommEvent(IntPtr hPort, ref CommEventFlags flags)
        {
            if (FullFramework)
            {
                return Convert.ToBoolean(WinWaitCommEvent(hPort, ref flags, IntPtr.Zero));
            }
            return Convert.ToBoolean(CEWaitCommEvent(hPort, ref flags, IntPtr.Zero));
        }

        internal static int WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds)
        {
            if (FullFramework)
            {
                return WinWaitForSingleObject(hHandle, dwMilliseconds);
            }
            return CEWaitForSingleObject(hHandle, dwMilliseconds);
        }

        [DllImport("kernel32.dll", EntryPoint="ClearCommError", SetLastError=true)]
        private static extern int WinClearCommError(IntPtr hFile, ref CommErrorFlags lpErrors, CommStat lpStat);
        [DllImport("kernel32.dll", EntryPoint="CloseHandle", SetLastError=true)]
        private static extern int WinCloseHandle(IntPtr hObject);
        [DllImport("kernel32.dll", EntryPoint="CreateEvent", SetLastError=true)]
        private static extern IntPtr WinCreateEvent(IntPtr lpEventAttributes, int bManualReset, int bInitialState, string lpName);
        [DllImport("kernel32.dll", EntryPoint="CreateFileW", CharSet=CharSet.Unicode, SetLastError=true)]
        private static extern IntPtr WinCreateFileW(string lpFileName, uint dwDesiredAccess, uint dwShareMode, IntPtr lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);
        [DllImport("kernel32.dll", EntryPoint="GetCommModemStatus", SetLastError=true)]
        private static extern int WinGetCommModemStatus(IntPtr hFile, ref uint lpModemStat);
        [DllImport("kernel32.dll", EntryPoint="GetCommState", SetLastError=true)]
        private static extern int WinGetCommState(IntPtr hFile, DCB dcb);
        [DllImport("kernel32.dll", EntryPoint="PurgeComm", SetLastError=true)]
        private static extern int WinPurgeComm(IntPtr hHandle, uint dwFlags);
        [DllImport("kernel32.dll", EntryPoint="ReadFile", SetLastError=true)]
        private static extern int WinReadFile(IntPtr hFile, byte[] lpBuffer, int nNumberOfBytesToRead, ref int lpNumberOfBytesRead, IntPtr lpOverlapped);
        [DllImport("kernel32.dll", EntryPoint="SetCommMask", SetLastError=true)]
        private static extern int WinSetCommMask(IntPtr handle, CommEventFlags dwEvtMask);
        [DllImport("kernel32.dll", EntryPoint="SetCommState", SetLastError=true)]
        private static extern int WinSetCommState(IntPtr hFile, DCB dcb);
        [DllImport("kernel32.dll", EntryPoint="SetCommTimeouts", SetLastError=true)]
        private static extern int WinSetCommTimeouts(IntPtr hFile, CommTimeouts timeouts);
        [DllImport("kernel32.dll", EntryPoint="SetEvent", SetLastError=true)]
        private static extern int WinSetEvent(IntPtr hEvent);
        [DllImport("kernel32.dll", EntryPoint="SetupComm", SetLastError=true)]
        private static extern int WinSetupComm(IntPtr hFile, int dwInQueue, int dwOutQueue);
        [DllImport("kernel32.dll", EntryPoint="WaitCommEvent", SetLastError=true)]
        private static extern int WinWaitCommEvent(IntPtr hFile, ref CommEventFlags lpEvtMask, IntPtr lpOverlapped);
        [DllImport("kernel32.dll", EntryPoint="WaitForSingleObject", SetLastError=true)]
        private static extern int WinWaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);
        [DllImport("kernel32.dll", EntryPoint="WriteFile", SetLastError=true)]
        private static extern int WinWriteFile(IntPtr hFile, byte[] lpBuffer, int nNumberOfBytesToRead, ref int lpNumberOfBytesRead, IntPtr lpOverlapped);
        internal static bool WriteFile(IntPtr hPort, byte[] buffer, int cbToWrite, ref int cbWritten, IntPtr lpOverlapped)
        {
            if (FullFramework)
            {
                if (WinWriteFile(hPort, buffer, cbToWrite, ref cbWritten, lpOverlapped) == 0)
                {
                    ThrowOnWin32ErrorHelper();
                    return false;
                }
                return true;
            }
            if (CEWriteFile(hPort, buffer, cbToWrite, ref cbWritten, IntPtr.Zero) == 0)
            {
                ThrowOnWin32ErrorHelper();
                return false;
            }
            return true;
        }

        internal static bool FullFramework
        {
            get
            {
                return (m_platform != PlatformID.WinCE);
            }
        }
    }
}

