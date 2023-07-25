namespace IDENTEC
{
    using System;

    [Flags]
    internal enum CommModemStatusFlags
    {
        MS_CTS_ON = 0x10,
        MS_DSR_ON = 0x20,
        MS_RING_ON = 0x40,
        MS_RLSD_ON = 0x80
    }
}

