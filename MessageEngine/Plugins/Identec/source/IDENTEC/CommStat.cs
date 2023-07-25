namespace IDENTEC
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal class CommStat
    {
        private uint bitfield;
        public uint cbInQue;
        public uint cbOutQue;
        private readonly uint fCtsHoldMask = 1;
        private readonly int fCtsHoldShift;
        private readonly uint fDsrHoldMask = 2;
        private readonly int fDsrHoldShift = 1;
        private readonly uint fRlsdHoldMask = 4;
        private readonly int fRlsdHoldShift = 2;
        private readonly uint fXoffHoldMask = 8;
        private readonly int fXoffHoldShift = 3;
        private readonly uint fXoffSentMask = 0x10;
        private readonly int fXoffSentShift = 4;
        private readonly uint fEofMask = 0x20;
        private readonly int fEofShift = 5;
        private readonly uint fTximMask = 0x40;
        private readonly int fTximShift = 6;
    }
}

