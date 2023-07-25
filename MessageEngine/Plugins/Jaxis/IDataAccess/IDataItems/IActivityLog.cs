using System;
using Jaxis.Interfaces;

namespace Jaxis.Inventory.Data
{
    public interface IActivityLog : IDataObject<IActivityLog>, IMessageWrapper 
    {
        int ActivityIndex { get; set; }
        Guid TagID { get; set; }
        Guid DeviceID { get; set; }
        Guid LocationID { get; set; }
        DateTime ActivityTime { get; set; }
        double SignalStrength { get; set; }
        int ActivityType { get; set; }
        string RawData { get; set; }
        string TagNumber { get; set; }
    }

    public interface IActivityItem
    {
    }

    public interface ITagActivityItem : IActivityItem
    {
        string TagNumber { get; set; }
    }
}