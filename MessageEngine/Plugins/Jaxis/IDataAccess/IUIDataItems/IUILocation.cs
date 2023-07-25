using System;

namespace Jaxis.Inventory.Data
{
    /// <summary>
    /// A Business Logic interface which represents the Locations table in the BevMetMobile Database.
    /// </summary>
    public interface IUILocation
    {
        //Guid LocationID { get; set; }
        //Guid OrganizationID { get; set; }
        string Name { get; set; }
        string Description { get; set; }
    }
}