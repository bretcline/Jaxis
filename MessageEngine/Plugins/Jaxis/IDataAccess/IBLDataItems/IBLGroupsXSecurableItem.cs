using System;

namespace Jaxis.Inventory.Data.IBLDataItems
{
    /// <summary>
    /// A Business Logic interface which represents the GroupsXActions table in the BevMetMobile Database.
    /// </summary>
    public interface IBLGroupsXSecurableItem
    {
        Guid GroupSecurableItemID { get; set; }
        Guid GroupID { get; set; }
        Guid SecurableItemID { get; set; }
    }
}