using System;
using Jaxis.MessageLibrary;

namespace Jaxis.Inventory.Data.IBLDataItems
{
    public interface IBLActivityLog : IActivityLog, IUIActivityLog
    {
        int ActivityIndex { get; set; }
        Guid TagID { get; set; }
        Guid DeviceID { get; set; }
        Guid LocationID { get; set; }
        DateTime ActivityTime { get; set; }
        double SignalStrength { get; set; }
        int ActivityTypeID { get; set; }
        string RawData { get; set; }
    }

    public interface IBLTagActivity : ITagActivity, IUITagActivity
    {
        Guid TagID { get; set; }
        Guid DeviceID { get; set; }
        Guid LocationID { get; set; }
        DateTime ActivityTime { get; set; }
        double SignalStrength { get; set; }
        string RawData { get; set; }
    }
}