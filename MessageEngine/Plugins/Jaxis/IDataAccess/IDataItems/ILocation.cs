using System;

namespace Jaxis.Inventory.Data
{
    /// <summary>
    /// A class which represents the Locations table in the BevMetMobile Database.
    /// </summary>
    public interface ILocation : INameDescription, IDataObject<ILocation>
    {
        Guid LocationID { get; set; }
        Guid? ParentID { get; set; }
        Guid OrganizationID { get; set; }
        Guid? DeviceID { get; set; }
        bool AllowHalfPour { get; set; }
        string POSAlias { get; set; }
    }
}