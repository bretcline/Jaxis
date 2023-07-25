namespace IDENTEC
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal class BasicPortSettings
    {
        public BaudRates BaudRate = BaudRates.CBR_19200;
        public byte ByteSize = 8;
        public IDENTEC.Parity Parity;
        public IDENTEC.StopBits StopBits;
    }
}

