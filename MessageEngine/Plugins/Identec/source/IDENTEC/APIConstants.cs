namespace IDENTEC
{
    using System;

    internal enum APIConstants : uint
    {
        INFINITE = 0xffffffff,
        PURGE_ALL = 15,
        PURGE_RXABORT = 2,
        PURGE_RXCLEAR = 8,
        PURGE_TXABORT = 1,
        PURGE_TXCLEAR = 4,
        WAIT_ABANDONED = 0x80,
        WAIT_ABANDONED_0 = 0x80,
        WAIT_FAILED = 0xffffffff,
        WAIT_OBJECT_0 = 0
    }
}

