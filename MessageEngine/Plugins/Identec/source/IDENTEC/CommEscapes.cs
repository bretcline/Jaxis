namespace IDENTEC
{
    using System;

    internal enum CommEscapes : uint
    {
        CLRBREAK = 9,
        CLRDTR = 6,
        CLRIR = 11,
        CLRRTS = 4,
        SETBREAK = 8,
        SETDTR = 5,
        SETIR = 10,
        SETRTS = 3,
        SETXOFF = 1,
        SETXON = 2
    }
}

