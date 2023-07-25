namespace IDENTEC.Readers
{
    using System;

    public interface ICompatibleIOStream
    {
        bool IsOpen();
        int ReadData(byte[] buffer, int offset, int nBytesToRead);
        int WriteData(byte[] buffer, int nBytesToWrite);
    }
}

