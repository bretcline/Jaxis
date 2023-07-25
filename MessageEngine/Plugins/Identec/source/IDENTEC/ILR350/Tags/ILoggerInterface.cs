namespace IDENTEC.ILR350.Tags
{
    using IDENTEC.ILR350.Readers;
    using IDENTEC.Tags.Logging;
    using System;

    public interface ILoggerInterface
    {
        RawLogData ReadFirstnLogSamples(ILR350Reader reader, int nLogToRead, int retries);
        RawLogData ReadLastnLogSamples(ILR350Reader reader, int nLogToRead, int retries);
        LogInfoData ReadLogInformation(ILR350Reader reader);
        bool StartLogging(ILR350Reader reader, TimeSpan Interval);
        bool StopLogging(ILR350Reader reader);
    }
}

