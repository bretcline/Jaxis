using System;

namespace Jaxis.Inventory.Data
{
    /// <summary>
    /// A class which represents the mobEvents table in the BevMetMobile Database.
    /// </summary>
    public interface IEvent : INamedObject, IDataObject<IEvent>
    {
        string Type { get; set; }
        string Customer { get; set; }
        DateTime? EventDate { get; set; }
        DateTime? StartTime { get; set; }
        DateTime? EndTime { get; set; }
    }
}