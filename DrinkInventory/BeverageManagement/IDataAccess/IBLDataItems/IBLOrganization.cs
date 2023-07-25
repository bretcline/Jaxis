using System;

namespace Jaxis.Inventory.Data.IBLDataItems
{
    /// <summary>
    /// A Business Logic interface which represents the Organizations table in the BevMetMobile Database.
    /// </summary>
    public interface IBLOrganization : IOrganization
    {
        Guid OrganizationID { get; set; }
        string Name { get; set; }
        string Description { get; set; }
    }

}