namespace IDENTEC.ILR350.Tags
{
    using System;

    public enum TagType : byte
    {
        iB350T_HH = 3,
        iQ350 = 6,
        iQ350_RTLS = 0x20,
        iQ350_RTLSPB = 0x30,
        iQ350L = 0x40,
        iQ350L_RTLS = 0x60,
        iQ350L_RTLSPB = 0x70,
        iQ350LM = 0x41,
        iQ350LW = 80,
        [Obsolete("use iQ350_RTLS")]
        iQ350RTLS = 0x20,
        iQ350T = 0x80,
        iQ350T_RTLS = 160,
        iQ350TL = 0xc0,
        iQ350TL_RTLS = 0xe0,
        iQ350TLPB = 0xd0,
        iQ350WAM = 1,
        UNKNOWN = 0xff
    }
}

