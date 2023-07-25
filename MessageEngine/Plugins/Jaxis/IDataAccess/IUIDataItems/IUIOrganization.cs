using System;

namespace Jaxis.Inventory.Data
{
    /// <summary>
    /// A Business Logic interface which represents the Organizations table in the BevMetMobile Database.
    /// </summary>
    public interface IUIOrganization
    {
        //Guid OrganizationID { get; set; }
        string Name { get; set; }
        string Description { get; set; }
    }

}