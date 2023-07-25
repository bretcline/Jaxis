namespace IDENTEC
{
    using System;

    [Flags]
    internal enum CommErrorFlags
    {
        BREAK = 0x10,
        FRAME = 8,
        IOE = 0x400,
        MODE = 0x8000,
        OVERRUN = 2,
        RXOVER = 1,
        RXPARITY = 4,
        TXFULL = 0x100
    }
}

