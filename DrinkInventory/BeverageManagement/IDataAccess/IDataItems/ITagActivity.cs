using System;
using Jaxis.Interfaces;

namespace Jaxis.Inventory.Data
{
    public interface ITagActivity : IDataObject<ITagActivity>, IMessageWrapper
    {
        Guid TagActivityID { get; set; }
        Guid TagID { get; set; }
        Guid DeviceID { get; set; }
        Guid LocationID { get; set; }
        DateTime ActivityTime { get; set; }
        double SignalStrength { get; set; }
        int ActivityType { get; set; }
        string RawData { get; set; }
    }
}