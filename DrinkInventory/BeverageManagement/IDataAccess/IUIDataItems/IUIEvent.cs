using System;

namespace Jaxis.Inventory.Data
{
    /// <summary>
    /// A Business Logic interface which represents the Events table in the BevMetMobile Database.
    /// </summary>
    public interface IUIEvent
    {
        //Guid EventID { get; set; }
        //Guid LocationID { get; set; }
        string Name { get; set; }
        string Type { get; set; }
        string Customer { get; set; }
        DateTime? EventDate { get; set; }
        DateTime? StartTime { get; set; }
        DateTime? EndTime { get; set; }
    }
}