namespace IDENTEC.Readers
{
    using IDENTEC.Tags;
    using IDENTEC.Tags.DigitalInputLogging;
    using IDENTEC.Tags.Logging;
    using System;

    public interface ITagReaderIQ
    {
        bool BlinkTag(iQTag tag, int blinkCount);
        bool PingTag(iQTag tag);
        LoopData ReadiQLTagMarkerInfo(iQTag tag);
        TimeSpan ReadIQTagAbsoluteEngineHourCounter(iQTag tag);
        TimeSpan ReadIQTagUserEngineHourCounter(iQTag tag);
        iQTagVersionInfo ReadiQTagVersion(iQTag tag);
        TemperatureLogSample ReadLastSampledTemperature(iQTag tag);
        TemperatureLogSample ReadTagCurrentTemperature(iQTag tag);
        TagReadDataResult ReadTagData(iQTag tag, int address, int bytesToRead);
        TagReadStringResult ReadTagDataString(iQTag tag, int address);
        TagReadDataResult ReadTagDataWithCRCAndLength(iQTag tag, int address);
        DigitalInputLog ReadTagDigitalInputEventLog(iQTag tag);
        TimeSpan ReadTagLogSamplingInterval(iQTag tag);
        RawLogData ReadTagRawLog(iQTag tag);
        TemperatureExtremes ReadTagTemperatureExtremes(iQTag tag);
        TemperatureLogData ReadTagTemperatureLog(iQTag tag);
        TagCollection ScanForIQTags(int maxTagsThatCanRespond);
        TagCollection ScanForIQTags(int maxTagsThatCanRespond, bool blink);
        bool SetTagRangeState(iQTag tag, bool enableExtendedRange);
        bool SleepTag(iQTag tag, int seconds);
        void StartTagDigitalInputEventLog(iQTag tag);
        void StartTagDigitalInputEventLog(iQTag tag, bool synchronize);
        void StartTagLogging(iQTag tag, TimeSpan samplingRate);
        void StopTagLogging(iQTag tag);
        void WriteIQTagUserEngineHourCounter(iQTag tag, TimeSpan ts);
        TagWriteDataResult WriteTagData(iQTag tag, int address, byte[] byData, int bytesToWrite);
        TagWriteDataResult WriteTagDataString(iQTag tag, int address, string text);
        TagWriteDataResult WriteTagDataWithCRCAndLength(iQTag tag, int address, byte[] byData, int bytesToWrite);
    }
}

