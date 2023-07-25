namespace IDENTEC.Readers
{
    using IDENTEC;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;

    internal class Port : IDisposable
    {
        private IntPtr closeEvent;
        private string closeEventName;
        private DCB dcb;
        private int dtr;
        private Thread eventThread;
        private IntPtr hPort;
        private bool isOpen;
        private bool m_bOverlappedEnabled;
        private bool m_bRxThreadEnabled;
        private string portName;
        private DetailedPortSettings portSettings;
        private int prxBuffer;
        private int ptxBuffer;
        private int rthreshold;
        private int rts;
        private byte[] rxBuffer;
        private Mutex rxBufferBusy;
        private int rxBufferSize;
        private IntPtr rxOverlapped;
        private int sthreshold;
        private ManualResetEvent threadStarted;
        private byte[] txBuffer;
        private int txBufferSize;
        private IntPtr txOverlapped;

        public event CommChangeEvent CTSChange;

        public event CommEvent DataReceived;

        public event CommChangeEvent DSRChange;

        public event CommEvent FlagCharReceived;

        public event CommEvent HighWater;

        public event CommErrorEvent OnError;

        public event CommEvent PowerEvent;

        public event CommChangeEvent RingChange;

        public event CommChangeEvent RLSDChange;

        public event CommEvent RxOverrun;

        public event CommEvent TxDone;

        public Port(string PortName)
        {
            this.hPort = (IntPtr) (-1);
            this.rxBufferSize = 0x80;
            this.rthreshold = 1;
            this.txBufferSize = 0x80;
            this.sthreshold = 1;
            this.rxBufferBusy = new Mutex();
            this.dcb = new DCB();
            this.threadStarted = new ManualResetEvent(false);
            this.closeEventName = "CloseEvent";
            this.rts = -1;
            this.dtr = -1;
            this.portName = PortName;
            this.Init();
        }

        public Port(int RxBufferSize, int TxBufferSize)
        {
            this.hPort = (IntPtr) (-1);
            this.rxBufferSize = 0x80;
            this.rthreshold = 1;
            this.txBufferSize = 0x80;
            this.sthreshold = 1;
            this.rxBufferBusy = new Mutex();
            this.dcb = new DCB();
            this.threadStarted = new ManualResetEvent(false);
            this.closeEventName = "CloseEvent";
            this.rts = -1;
            this.dtr = -1;
            this.rxBufferSize = RxBufferSize;
            this.txBufferSize = TxBufferSize;
            this.Init();
        }

        public Port(string PortName, BasicPortSettings InitialSettings)
        {
            this.hPort = (IntPtr) (-1);
            this.rxBufferSize = 0x80;
            this.rthreshold = 1;
            this.txBufferSize = 0x80;
            this.sthreshold = 1;
            this.rxBufferBusy = new Mutex();
            this.dcb = new DCB();
            this.threadStarted = new ManualResetEvent(false);
            this.closeEventName = "CloseEvent";
            this.rts = -1;
            this.dtr = -1;
            this.portName = PortName;
            this.Init();
            this.portSettings.BasicSettings = InitialSettings;
        }

        public Port(string PortName, DetailedPortSettings InitialSettings)
        {
            this.hPort = (IntPtr) (-1);
            this.rxBufferSize = 0x80;
            this.rthreshold = 1;
            this.txBufferSize = 0x80;
            this.sthreshold = 1;
            this.rxBufferBusy = new Mutex();
            this.dcb = new DCB();
            this.threadStarted = new ManualResetEvent(false);
            this.closeEventName = "CloseEvent";
            this.rts = -1;
            this.dtr = -1;
            this.portName = PortName;
            this.Init();
            this.portSettings = InitialSettings;
        }

        public Port(BasicPortSettings InitialSettings, int RxBufferSize, int TxBufferSize)
        {
            this.hPort = (IntPtr) (-1);
            this.rxBufferSize = 0x80;
            this.rthreshold = 1;
            this.txBufferSize = 0x80;
            this.sthreshold = 1;
            this.rxBufferBusy = new Mutex();
            this.dcb = new DCB();
            this.threadStarted = new ManualResetEvent(false);
            this.closeEventName = "CloseEvent";
            this.rts = -1;
            this.dtr = -1;
            this.rxBufferSize = RxBufferSize;
            this.txBufferSize = TxBufferSize;
            this.Init();
            this.portSettings.BasicSettings = InitialSettings;
        }

        public Port(DetailedPortSettings InitialSettings, int RxBufferSize, int TxBufferSize)
        {
            this.hPort = (IntPtr) (-1);
            this.rxBufferSize = 0x80;
            this.rthreshold = 1;
            this.txBufferSize = 0x80;
            this.sthreshold = 1;
            this.rxBufferBusy = new Mutex();
            this.dcb = new DCB();
            this.threadStarted = new ManualResetEvent(false);
            this.closeEventName = "CloseEvent";
            this.rts = -1;
            this.dtr = -1;
            this.rxBufferSize = RxBufferSize;
            this.txBufferSize = TxBufferSize;
            this.Init();
            this.portSettings = InitialSettings;
        }

        public bool Close()
        {
            if (this.txOverlapped != IntPtr.Zero)
            {
                LocalFree(this.txOverlapped);
                this.txOverlapped = IntPtr.Zero;
            }
            if (this.isOpen)
            {
                GC.KeepAlive(this);
                if (IDENTEC.NativeMethods.CloseHandle(this.hPort))
                {
                    IDENTEC.NativeMethods.SetEvent(this.closeEvent);
                    this.isOpen = false;
                    this.hPort = (IntPtr) (-1);
                    IDENTEC.NativeMethods.SetEvent(this.closeEvent);
                    return true;
                }
            }
            return false;
        }

        private void CommEventThread()
        {
            GC.KeepAlive(this);
            CommEventFlags nONE = CommEventFlags.NONE;
            byte[] buffer = new byte[this.rxBufferSize];
            int cbRead = 0;
            AutoResetEvent event2 = new AutoResetEvent(false);
            if (IDENTEC.NativeMethods.FullFramework && this.m_bOverlappedEnabled)
            {
                IDENTEC.NativeMethods.SetCommMask(this.hPort, CommEventFlags.ALLPC);
                OVERLAPPED structure = new OVERLAPPED();
                this.rxOverlapped = LocalAlloc(0x40, Marshal.SizeOf(structure));
                structure.Offset = 0;
                structure.OffsetHigh = 0;
                structure.hEvent = event2.Handle;
                Marshal.StructureToPtr(structure, this.rxOverlapped, true);
            }
            else
            {
                IDENTEC.NativeMethods.SetCommMask(this.hPort, CommEventFlags.ALLCE);
            }
            try
            {
                this.threadStarted.Set();
                while (this.hPort != ((IntPtr) (-1)))
                {
                    if (this.m_bOverlappedEnabled && !IDENTEC.NativeMethods.WaitCommEvent(this.hPort, ref nONE))
                    {
                        int num2 = Marshal.GetLastWin32Error();
                        if (num2 == 0x3e5)
                        {
                            event2.WaitOne();
                            Thread.Sleep(0);
                            continue;
                        }
                        if ((num2 != 6) || (IDENTEC.NativeMethods.WaitForSingleObject(this.closeEvent, 0xbb8) != 0))
                        {
                            if (num2 != 0x3e3)
                            {
                                throw new CommPortException(string.Format("Wait Failed: {0}", num2));
                            }
                        }
                        else
                        {
                            this.hPort = (IntPtr) (-1);
                            this.threadStarted.Reset();
                        }
                        return;
                    }
                    if (IDENTEC.NativeMethods.FullFramework)
                    {
                        IDENTEC.NativeMethods.SetCommMask(this.hPort, CommEventFlags.ALLPC);
                    }
                    else
                    {
                        IDENTEC.NativeMethods.SetCommMask(this.hPort, CommEventFlags.ALLCE);
                    }
                    if ((nONE & CommEventFlags.ERR) != CommEventFlags.NONE)
                    {
                        CommErrorFlags flags = 0;
                        CommStat stat = new CommStat();
                        if (!IDENTEC.NativeMethods.ClearCommError(this.hPort, ref flags, stat))
                        {
                            throw new CommPortException(string.Format("ClearCommError Failed: {0}", Marshal.GetLastWin32Error()));
                        }
                        if ((flags & CommErrorFlags.BREAK) != 0)
                        {
                            nONE |= CommEventFlags.BREAK;
                        }
                        else
                        {
                            StringBuilder builder = new StringBuilder("UART Error: ", 80);
                            if ((flags & CommErrorFlags.FRAME) != 0)
                            {
                                builder = builder.Append("Framing,");
                            }
                            if ((flags & CommErrorFlags.IOE) != 0)
                            {
                                builder = builder.Append("IO,");
                            }
                            if ((flags & CommErrorFlags.OVERRUN) != 0)
                            {
                                builder = builder.Append("Overrun,");
                            }
                            if ((flags & CommErrorFlags.RXOVER) != 0)
                            {
                                builder = builder.Append("Receive Overflow,");
                            }
                            if ((flags & CommErrorFlags.RXPARITY) != 0)
                            {
                                builder = builder.Append("Parity,");
                            }
                            if ((flags & CommErrorFlags.TXFULL) != 0)
                            {
                                builder = builder.Append("Transmit Overflow,");
                            }
                            if (builder.Length == 12)
                            {
                                builder = builder.Append("Unknown");
                            }
                            if (this.OnError != null)
                            {
                                this.OnError(builder.ToString());
                            }
                            continue;
                        }
                    }
                    uint lpModemStat = 0;
                    IDENTEC.NativeMethods.GetCommModemStatus(this.hPort, ref lpModemStat);
                    if (((nONE & CommEventFlags.CTS) != CommEventFlags.NONE) && (this.CTSChange != null))
                    {
                        this.CTSChange((lpModemStat & 0x10) != 0);
                    }
                    if (((nONE & CommEventFlags.DSR) != CommEventFlags.NONE) && (this.DSRChange != null))
                    {
                        this.DSRChange((lpModemStat & 0x20) != 0);
                    }
                    if (((nONE & CommEventFlags.RING) != CommEventFlags.NONE) && (this.RingChange != null))
                    {
                        this.RingChange((lpModemStat & 0x40) != 0);
                    }
                    if (((nONE & CommEventFlags.RLSD) != CommEventFlags.NONE) && (this.RLSDChange != null))
                    {
                        this.RLSDChange((lpModemStat & 0x80) != 0);
                    }
                    if (((nONE & CommEventFlags.TXEMPTY) != CommEventFlags.NONE) && (this.TxDone != null))
                    {
                        this.TxDone();
                    }
                    if (((nONE & CommEventFlags.RXFLAG) != CommEventFlags.NONE) && (this.FlagCharReceived != null))
                    {
                        this.FlagCharReceived();
                    }
                    if (((nONE & CommEventFlags.POWER) != CommEventFlags.NONE) && (this.PowerEvent != null))
                    {
                        this.PowerEvent();
                    }
                    if (((nONE & CommEventFlags.RX80FULL) != CommEventFlags.NONE) && (this.HighWater != null))
                    {
                        this.HighWater();
                    }
                    if ((nONE & CommEventFlags.RXCHAR) != CommEventFlags.NONE)
                    {
                        do
                        {
                            IDENTEC.NativeMethods.ReadFile(this.hPort, buffer, this.rxBufferSize, ref cbRead, this.rxOverlapped);
                            if (cbRead >= 1)
                            {
                                this.rxBufferBusy.WaitOne();
                                if ((this.prxBuffer + cbRead) <= buffer.Length)
                                {
                                    Array.Copy(buffer, 0, this.rxBuffer, this.prxBuffer, cbRead);
                                    this.prxBuffer += cbRead;
                                }
                                else
                                {
                                    Array.Copy(this.rxBuffer, this.rxBuffer.Length - cbRead, this.rxBuffer, 0, this.rxBuffer.Length - cbRead);
                                    Array.Copy(buffer, 0, this.rxBuffer, this.prxBuffer, cbRead);
                                    this.prxBuffer = this.rxBuffer.Length;
                                    if (this.RxOverrun != null)
                                    {
                                        this.RxOverrun();
                                    }
                                }
                                this.rxBufferBusy.ReleaseMutex();
                                if ((this.rthreshold != 0) & (buffer.Length >= this.rthreshold))
                                {
                                    this.DataReceived();
                                }
                            }
                        }
                        while (cbRead > 0);
                    }
                }
            }
            catch
            {
                if (this.rxOverlapped != IntPtr.Zero)
                {
                    LocalFree(this.rxOverlapped);
                }
                throw;
            }
        }

        public void Dispose()
        {
            if (this.isOpen)
            {
                this.Close();
            }
            GC.SuppressFinalize(this);
        }

        internal void EnableDTR(bool enable)
        {
            if (this.IsOpen)
            {
                if (enable)
                {
                    this.portSettings.DTRControl = DTRControlFlows.enable;
                }
                else
                {
                    this.portSettings.DTRControl = DTRControlFlows.disable;
                }
                IDENTEC.NativeMethods.GetCommState(this.hPort, this.dcb);
                this.dcb.BaudRate = (uint) this.portSettings.BasicSettings.BaudRate;
                this.dcb.ByteSize = this.portSettings.BasicSettings.ByteSize;
                this.dcb.EofChar = (sbyte) this.portSettings.EOFChar;
                this.dcb.ErrorChar = (sbyte) this.portSettings.ErrorChar;
                this.dcb.EvtChar = (sbyte) this.portSettings.EVTChar;
                this.dcb.fAbortOnError = this.portSettings.AbortOnError;
                this.dcb.fBinary = true;
                this.dcb.fDsrSensitivity = this.portSettings.DSRSensitive;
                this.dcb.fDtrControl = (DCB.DtrControlFlags) ((byte) this.portSettings.DTRControl);
                this.dcb.fErrorChar = this.portSettings.ReplaceErrorChar;
                this.dcb.fInX = this.portSettings.InX;
                this.dcb.fNull = this.portSettings.DiscardNulls;
                this.dcb.fOutX = this.portSettings.OutX;
                this.dcb.fOutxCtsFlow = this.portSettings.OutCTS;
                this.dcb.fOutxDsrFlow = this.portSettings.OutDSR;
                this.dcb.fParity = this.portSettings.BasicSettings.Parity != Parity.none;
                this.dcb.fRtsControl = (DCB.RtsControlFlags) ((byte) this.portSettings.RTSControl);
                this.dcb.fTXContinueOnXoff = this.portSettings.TxContinueOnXOff;
                this.dcb.Parity = (byte) this.portSettings.BasicSettings.Parity;
                this.dcb.StopBits = (byte) this.portSettings.BasicSettings.StopBits;
                this.dcb.XoffChar = (sbyte) this.portSettings.XoffChar;
                this.dcb.XonChar = (sbyte) this.portSettings.XonChar;
                this.dcb.XonLim = this.dcb.XoffLim = (ushort) (this.rxBufferSize / 10);
                IDENTEC.NativeMethods.SetCommState(this.hPort, this.dcb);
            }
        }

        ~Port()
        {
            if (this.isOpen)
            {
                this.Close();
            }
        }

        private void Init()
        {
            this.closeEvent = IDENTEC.NativeMethods.CreateEvent(true, false, this.closeEventName);
            if (!IDENTEC.NativeMethods.FullFramework)
            {
                this.rxBufferSize = 0x40;
                this.txBufferSize = 0x40;
            }
            this.rxBuffer = new byte[this.rxBufferSize];
            this.txBuffer = new byte[this.txBufferSize];
            this.portSettings = new DetailedPortSettings();
            GC.KeepAlive(this);
        }

        [DllImport("kernel32", SetLastError=true)]
        internal static extern IntPtr LocalAlloc(int uFlags, int uBytes);
        [DllImport("kernel32", SetLastError=true)]
        internal static extern IntPtr LocalFree(IntPtr hMem);
        public bool Open()
        {
            if (this.isOpen)
            {
                return false;
            }
            GC.KeepAlive(this);
            if (IDENTEC.NativeMethods.FullFramework && this.m_bOverlappedEnabled)
            {
                OVERLAPPED structure = new OVERLAPPED();
                this.txOverlapped = LocalAlloc(0x40, Marshal.SizeOf(structure));
                structure.Offset = 0;
                structure.OffsetHigh = 0;
                structure.hEvent = IntPtr.Zero;
                Marshal.StructureToPtr(structure, this.txOverlapped, true);
            }
            this.hPort = IDENTEC.NativeMethods.CreateFile(this.portName, this.m_bOverlappedEnabled);
            if (this.hPort == ((IntPtr) (-1)))
            {
                int error = Marshal.GetLastWin32Error();
                Win32Exception exception = new Win32Exception(error);
                throw new CommPortException(string.Format("CreateFile Failed: {0} ", error) + exception.Message, error);
            }
            this.isOpen = true;
            if (!IDENTEC.NativeMethods.SetupComm(this.hPort, this.rxBufferSize, this.txBufferSize))
            {
                Marshal.GetLastWin32Error();
            }
            IDENTEC.NativeMethods.GetCommState(this.hPort, this.dcb);
            this.dcb.BaudRate = (uint) this.portSettings.BasicSettings.BaudRate;
            this.dcb.ByteSize = this.portSettings.BasicSettings.ByteSize;
            this.dcb.EofChar = (sbyte) this.portSettings.EOFChar;
            this.dcb.ErrorChar = (sbyte) this.portSettings.ErrorChar;
            this.dcb.EvtChar = (sbyte) this.portSettings.EVTChar;
            this.dcb.fAbortOnError = this.portSettings.AbortOnError;
            this.dcb.fBinary = true;
            this.dcb.fDsrSensitivity = this.portSettings.DSRSensitive;
            this.dcb.fDtrControl = (DCB.DtrControlFlags) ((byte) this.portSettings.DTRControl);
            this.dcb.fErrorChar = this.portSettings.ReplaceErrorChar;
            this.dcb.fInX = this.portSettings.InX;
            this.dcb.fNull = this.portSettings.DiscardNulls;
            this.dcb.fOutX = this.portSettings.OutX;
            this.dcb.fOutxCtsFlow = this.portSettings.OutCTS;
            this.dcb.fOutxDsrFlow = this.portSettings.OutDSR;
            this.dcb.fParity = this.portSettings.BasicSettings.Parity != Parity.none;
            this.dcb.fRtsControl = (DCB.RtsControlFlags) ((byte) this.portSettings.RTSControl);
            this.dcb.fTXContinueOnXoff = this.portSettings.TxContinueOnXOff;
            this.dcb.Parity = (byte) this.portSettings.BasicSettings.Parity;
            this.dcb.StopBits = (byte) this.portSettings.BasicSettings.StopBits;
            this.dcb.XoffChar = (sbyte) this.portSettings.XoffChar;
            this.dcb.XonChar = (sbyte) this.portSettings.XonChar;
            this.dcb.XonLim = this.dcb.XoffLim = (ushort) (this.rxBufferSize / 10);
            IDENTEC.NativeMethods.SetCommState(this.hPort, this.dcb);
            this.dtr = (this.dcb.fDtrControl == DCB.DtrControlFlags.Enable) ? 1 : 0;
            this.rts = (this.dcb.fRtsControl == DCB.RtsControlFlags.Enable) ? 1 : 0;
            CommTimeouts timeouts = new CommTimeouts {
                ReadIntervalTimeout = uint.MaxValue,
                ReadTotalTimeoutConstant = 0,
                ReadTotalTimeoutMultiplier = 0,
                WriteTotalTimeoutConstant = 0,
                WriteTotalTimeoutMultiplier = 0
            };
            IDENTEC.NativeMethods.SetCommTimeouts(this.hPort, timeouts);
            if (this.m_bRxThreadEnabled)
            {
                this.eventThread = new Thread(new ThreadStart(this.CommEventThread));
                this.eventThread.Start();
                this.threadStarted.WaitOne();
            }
            return true;
        }

        public bool PurgeComm()
        {
            return IDENTEC.NativeMethods.PurgeRx(this.hPort);
        }

        internal bool ReadPort(byte[] byDataRead, int nBytesToRead, ref int nBytesRead)
        {
            GC.KeepAlive(this);
            nBytesRead = 0;
            IDENTEC.NativeMethods.ReadFile(this.hPort, byDataRead, nBytesToRead, ref nBytesRead, IntPtr.Zero);
            if (nBytesRead < 0)
            {
                int nError = Marshal.GetLastWin32Error();
                if (nError != 0)
                {
                    IDENTEC.NativeMethods.ThrowOnWin32ErrorHelper(nError);
                }
            }
            return true;
        }

        public bool IsOpen
        {
            get
            {
                return this.isOpen;
            }
        }

        public virtual byte[] Output
        {
            set
            {
                if (this.isOpen)
                {
                    GC.KeepAlive(this);
                    int cbWritten = 0;
                    if (value.GetLength(0) > this.sthreshold)
                    {
                        if (this.ptxBuffer > 0)
                        {
                            IDENTEC.NativeMethods.WriteFile(this.hPort, this.txBuffer, this.ptxBuffer, ref cbWritten, this.txOverlapped);
                            this.ptxBuffer = 0;
                        }
                        IDENTEC.NativeMethods.WriteFile(this.hPort, value, value.GetLength(0), ref cbWritten, this.txOverlapped);
                    }
                    else
                    {
                        value.CopyTo(this.txBuffer, this.ptxBuffer);
                        this.ptxBuffer += value.Length;
                        if (this.ptxBuffer >= this.sthreshold)
                        {
                            IDENTEC.NativeMethods.WriteFile(this.hPort, this.txBuffer, this.ptxBuffer, ref cbWritten, this.txOverlapped);
                            this.ptxBuffer = 0;
                        }
                    }
                }
            }
        }

        internal bool OverlappedEnabled
        {
            set
            {
                this.m_bOverlappedEnabled = value;
            }
        }

        public delegate void CommChangeEvent(bool NewState);

        public delegate void CommErrorEvent(string Description);

        public delegate void CommEvent();
    }
}

