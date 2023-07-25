namespace IDENTEC.ILR350.Tags
{
    using IDENTEC.ILR350.Readers;
    using System;

    public interface IRTLSTagInterface
    {
        TimeSpan ReadMotionTriggeredRangingInterval(ILR350Reader reader);
        TimeSpan ReadRangingInterval(ILR350Reader reader);
        int ReadRangingTXPower(ILR350Reader reader);
        bool WriteMotionTriggeredRangingInterval(ILR350Reader reader, TimeSpan interval);
        bool WriteRangingInterval(ILR350Reader reader, TimeSpan interval);
        bool WriteRangingTXPower(ILR350Reader reader, int txpower);
    }
}

