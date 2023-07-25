namespace IDENTEC.ILR350.Tags
{
    using IDENTEC.ILR350.Readers;
    using IDENTEC.Tags;
    using System;

    public interface IWritableInterface
    {
        TagReadDataResult ReadData(ILR350Reader reader, int address, int bytesToRead, int retries);
        TagWriteDataResult WriteData(ILR350Reader reader, int address, byte[] data, int bytesToWrite, int retries);
    }
}

