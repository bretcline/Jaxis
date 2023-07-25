using System;

namespace Jaxis.Inventory.Data.IBLDataItems
{
    /// <summary>
    /// A Business Logic interface which represents the Locations table in the BevMetMobile Database.
    /// </summary>
    public interface IBLLocation : ILocation
    {
        Guid LocationID { get; set; }
        Guid OrganizationID { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        Guid? DeviceID { get; set; }
        string POSAlias { get; set; }
    }
}