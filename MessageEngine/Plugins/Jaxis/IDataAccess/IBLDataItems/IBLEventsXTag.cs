using System;

namespace Jaxis.Inventory.Data.IBLDataItems
{
    /// <summary>
    /// A Business Logic interface which represents the EventsXTags table in the BevMetMobile Database.
    /// </summary>
    public interface IBLEventsXTag : IEventsXTag
    {
        Guid EventXTagID { get; set; }
        Guid EventID { get; set; }
        Guid TagID { get; set; }
    }
}
