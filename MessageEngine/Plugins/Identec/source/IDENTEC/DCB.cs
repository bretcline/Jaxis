namespace IDENTEC
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    [StructLayout(LayoutKind.Sequential)]
    internal class DCB
    {
        private uint DCBlength;
        public uint BaudRate;
        internal uint Control;
        internal ushort wReserved;
        public ushort XonLim;
        public ushort XoffLim;
        public byte ByteSize;
        public byte Parity;
        public byte StopBits;
        public sbyte XonChar;
        public sbyte XoffChar;
        public sbyte ErrorChar;
        public sbyte EofChar;
        public sbyte EvtChar;
        internal ushort wReserved1;
        private readonly uint fBinaryMask = 1;
        private readonly int fBinaryShift;
        private readonly uint fParityMask = 2;
        private readonly int fParityShift = 1;
        private readonly uint fOutxCtsFlowMask = 4;
        private readonly int fOutxCtsFlowShift = 2;
        private readonly uint fOutxDsrFlowMask = 8;
        private readonly int fOutxDsrFlowShift = 3;
        private readonly uint fDtrControlMask = 0x30;
        private readonly int fDtrControlShift = 4;
        private readonly uint fDsrSensitivityMask = 0x40;
        private readonly int fDsrSensitivityShift = 6;
        private readonly uint fTXContinueOnXoffMask = 0x80;
        private readonly int fTXContinueOnXoffShift = 7;
        private readonly uint fOutXMask = 0x100;
        private readonly int fOutXShift = 8;
        private readonly uint fInXMask = 0x200;
        private readonly int fInXShift = 9;
        private readonly uint fErrorCharMask = 0x400;
        private readonly int fErrorCharShift = 10;
        private readonly uint fNullMask = 0x800;
        private readonly int fNullShift = 11;
        private readonly uint fRtsControlMask = 0x3000;
        private readonly int fRtsControlShift = 12;
        private readonly uint fAbortOnErrorMask = 0x4000;
        private readonly int fAbortOnErrorShift = 14;
        public DCB()
        {
            this.DCBlength = (uint) Marshal.SizeOf(this);
        }

        public bool fBinary
        {
            get
            {
                return ((this.Control & this.fBinaryMask) != 0);
            }
            set
            {
                this.Control |= Convert.ToUInt32(value) << this.fBinaryShift;
            }
        }
        public bool fParity
        {
            get
            {
                return ((this.Control & this.fParityMask) != 0);
            }
            set
            {
                this.Control |= Convert.ToUInt32(value) << this.fParityShift;
            }
        }
        public bool fOutxCtsFlow
        {
            get
            {
                return ((this.Control & this.fOutxCtsFlowMask) != 0);
            }
            set
            {
                this.Control |= Convert.ToUInt32(value) << this.fOutxCtsFlowShift;
            }
        }
        public bool fOutxDsrFlow
        {
            get
            {
                return ((this.Control & this.fOutxDsrFlowMask) != 0);
            }
            set
            {
                this.Control |= Convert.ToUInt32(value) << this.fOutxDsrFlowShift;
            }
        }
        public DtrControlFlags fDtrControl
        {
            get
            {
                return (DtrControlFlags) ((byte) ((this.Control & this.fDtrControlMask) >> this.fDtrControlShift));
            }
            set
            {
                this.Control |= Convert.ToUInt32((DtrControlFlags) value) << this.fDtrControlShift;
            }
        }
        public bool fDsrSensitivity
        {
            get
            {
                return ((this.Control & this.fDsrSensitivityMask) != 0);
            }
            set
            {
                this.Control |= Convert.ToUInt32(value) << this.fDsrSensitivityShift;
            }
        }
        public bool fTXContinueOnXoff
        {
            get
            {
                return ((this.Control & this.fTXContinueOnXoffMask) != 0);
            }
            set
            {
                this.Control |= Convert.ToUInt32(value) << this.fTXContinueOnXoffShift;
            }
        }
        public bool fOutX
        {
            get
            {
                return ((this.Control & this.fOutXMask) != 0);
            }
            set
            {
                this.Control |= Convert.ToUInt32(value) << this.fOutXShift;
            }
        }
        public bool fInX
        {
            get
            {
                return ((this.Control & this.fInXMask) != 0);
            }
            set
            {
                this.Control |= Convert.ToUInt32(value) << this.fInXShift;
            }
        }
        public bool fErrorChar
        {
            set
            {
                this.Control |= Convert.ToUInt32(value) << this.fErrorCharShift;
            }
        }
        public bool fNull
        {
            get
            {
                return ((this.Control & this.fNullMask) != 0);
            }
            set
            {
                this.Control |= Convert.ToUInt32(value) << this.fNullShift;
            }
        }
        public RtsControlFlags fRtsControl
        {
            get
            {
                return (RtsControlFlags) ((byte) ((this.Control & this.fRtsControlMask) >> this.fRtsControlShift));
            }
            set
            {
                this.Control |= Convert.ToUInt32((RtsControlFlags) value) << this.fRtsControlShift;
            }
        }
        public bool fAbortOnError
        {
            get
            {
                return ((this.Control & this.fAbortOnErrorMask) != 0);
            }
            set
            {
                this.Control |= Convert.ToUInt32(value) << this.fAbortOnErrorShift;
            }
        }
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DCB:\r\n");
            builder.AppendFormat(null, "  BaudRate:     {0}\r\n", new object[] { this.BaudRate });
            builder.AppendFormat(null, "  Control:      0x{0:x}\r\n", new object[] { this.Control });
            builder.AppendFormat(null, "    fBinary:           {0}\r\n", new object[] { this.fBinary });
            builder.AppendFormat(null, "    fParity:           {0}\r\n", new object[] { this.fParity });
            builder.AppendFormat(null, "    fOutxCtsFlow:      {0}\r\n", new object[] { this.fOutxCtsFlow });
            builder.AppendFormat(null, "    fOutxDsrFlow:      {0}\r\n", new object[] { this.fOutxDsrFlow });
            builder.AppendFormat(null, "    fDtrControl:       {0}\r\n", new object[] { this.fDtrControl });
            builder.AppendFormat(null, "    fDsrSensitivity:   {0}\r\n", new object[] { this.fDsrSensitivity });
            builder.AppendFormat(null, "    fTXContinueOnXoff: {0}\r\n", new object[] { this.fTXContinueOnXoff });
            builder.AppendFormat(null, "    fOutX:             {0}\r\n", new object[] { this.fOutX });
            builder.AppendFormat(null, "    fInX:              {0}\r\n", new object[] { this.fInX });
            builder.AppendFormat(null, "    fNull:             {0}\r\n", new object[] { this.fNull });
            builder.AppendFormat(null, "    fRtsControl:       {0}\r\n", new object[] { this.fRtsControl });
            builder.AppendFormat(null, "    fAbortOnError:     {0}\r\n", new object[] { this.fAbortOnError });
            builder.AppendFormat(null, "  XonLim:       {0}\r\n", new object[] { this.XonLim });
            builder.AppendFormat(null, "  XoffLim:      {0}\r\n", new object[] { this.XoffLim });
            builder.AppendFormat(null, "  ByteSize:     {0}\r\n", new object[] { this.ByteSize });
            builder.AppendFormat(null, "  Parity:       {0}\r\n", new object[] { this.Parity });
            builder.AppendFormat(null, "  StopBits:     {0}\r\n", new object[] { this.StopBits });
            builder.AppendFormat(null, "  XonChar:      {0}\r\n", new object[] { this.XonChar });
            builder.AppendFormat(null, "  XoffChar:     {0}\r\n", new object[] { this.XoffChar });
            builder.AppendFormat(null, "  ErrorChar:    {0}\r\n", new object[] { this.ErrorChar });
            builder.AppendFormat(null, "  EofChar:      {0}\r\n", new object[] { this.EofChar });
            builder.AppendFormat(null, "  EvtChar:      {0}\r\n", new object[] { this.EvtChar });
            return builder.ToString();
        }
        public enum DtrControlFlags : byte
        {
            Disable = 0,
            Enable = 1,
            Handshake = 2
        }

        public enum RtsControlFlags : byte
        {
            Disable = 0,
            Enable = 1,
            Handshake = 2,
            Toggle = 3
        }
    }
}

