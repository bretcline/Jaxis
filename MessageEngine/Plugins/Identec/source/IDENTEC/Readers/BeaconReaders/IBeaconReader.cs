namespace IDENTEC.Readers.BeaconReaders
{
    using IDENTEC.Tags;
    using System;

    public interface IBeaconReader
    {
        bool ClearTagList();
        bool EnableHighRfSensitivity(bool enable);
        int GetMaxTagReported();
        TagListBehavior GetTagListBehavior();
        TimeSpan GetTagListInhibitTime();
        TimeSpan GetTagReReportingInterval();
        TagCollection GetTags(bool enableExtendedInfo);
        bool SetAllTagsInListAsNotYetReported();
        bool SetMaxTagReported(int maxTax);
        bool SetTagListBehavior(TagListBehavior mode);
        bool SetTagListInhibitTime(TimeSpan lifetimeInList);
        bool SetTagReReportingInterval(TimeSpan interval);
    }
}

