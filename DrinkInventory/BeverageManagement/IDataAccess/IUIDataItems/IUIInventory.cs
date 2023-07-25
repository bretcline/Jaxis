using System;

namespace Jaxis.Inventory.Data
{
    /// <summary>
    /// A Business Logic interface which represents the Inventories table in the BevMetMobile Database.
    /// </summary>
    public interface IUIInventory
    {
        //Guid InventoryID { get; set; }
        Guid UPCID { get; set; }
        Guid LocationID { get; set; }
        //long TotalQuantity { get; set; }
        decimal Cost { get; set; }
    }
}