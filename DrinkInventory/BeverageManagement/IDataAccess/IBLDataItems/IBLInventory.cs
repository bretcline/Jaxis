using System;

namespace Jaxis.Inventory.Data.IBLDataItems
{
    /// <summary>
    /// A Business Logic interface which represents the Inventories table in the BevMetMobile Database.
    /// </summary>
    public interface IBLInventory : IInventory
    {
        IBLUPCItem UPC { get; set; }
        IBLLocation Location { get; set; }
        //long Quantity { get; set; }
        decimal Cost { get; set; }
        DateTime EnterDate { get; set; }
        DateTime? ExitDate { get; set; }
        DateTime? TagDate { get; set; }
        int? ExitReason { get; set; }
        Guid? TagID { get; set; }
        String Memo { get; set; }
        double Amount { get; set; }
    }
}