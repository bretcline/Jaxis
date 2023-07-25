namespace IDENTEC.Readers
{
    using IDENTEC.Tags;
    using System;

    public interface ITagReaderID2
    {
        bool BlinkTag(iD2Tag tag, TimeSpan LEDOn, TimeSpan LEDOff, int blinkCount);
        bool PingTag(iD2Tag tag);
        TagReadDataResult ReadTagData(iD2Tag tag, int address, int bytesToRead);
        TagReadStringResult ReadTagDataString(iD2Tag tag, int address);
        TagReadDataResult ReadTagDataWithCRCAndLength(iD2Tag tag, int address);
        TagCollection ScanForID2Tags(int maxTagsThatCanRespond);
        TagCollection ScanForID2Tags(int maxTagsThatCanRespond, bool blink);
        bool SessionSetupTag(iD2Tag tag, int seconds, bool sleep, bool quiet);
        TagWriteDataResult WriteTagData(iD2Tag tag, int address, byte[] byData, int bytesToWrite);
        TagWriteDataResult WriteTagDataString(iD2Tag tag, int address, string text);
        TagWriteDataResult WriteTagDataWithCRCAndLength(iD2Tag tag, int address, byte[] byData, int bytesToWrite);
    }
}

