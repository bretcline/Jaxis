using System;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data
{

    public enum ExitReasons
    {
        Unknown = 0,
        Cleaned = 1,
        Rebrand = 2,
        Dispensed = 100,
        Broken = 200,
        Sold = 300,
        Stolen = 400,
        POSRingup = 500,
    }

    /// <summary>
    /// A class which represents the mobInventory table in the BevMetMobile Database.
    /// </summary>
    public interface IInventory : IDataObject<IInventory>
    {
        Guid InventoryID { get; set; }
        Guid UPCID { get; set; }
        Guid LocationID { get; set; }
        //long Quantity { get; set; }
        decimal Cost { get; set; }
        DateTime EnterDate { get; set; }
        DateTime? ExitDate { get; set; }
        DateTime? TagDate { get; set; }
        int? ExitReason { get; set; }
        double Amount { get; set; }
        Guid? TagID { get; set; }
        String Memo { get; set; }
    }


    public interface IInventoryItemView
    {
        string Name { get; set; }
        int? TaggedQuantity { get; }
        int? StockQuantity { get; }
        int? TotalQuantity { get; }
        double? ParBottleCount { get; set; }
    }

    public interface IBLInventoryItemView : IDataObject<IInventoryItemView>, IInventoryItemView
    {
        //IBLUPCItem UPC { get; set; }
        string Name { get; set; }
        Guid UPCID { get; set; }
        //IBLLocation Location { get; set; }
        int? TaggedQuantity { get; }
        int? StockQuantity { get; }
        int? TotalQuantity { get; }
        double? ParBottleCount { get; set; }
    }


    public interface IUIInventoryItemView : IDataObject<IInventoryItemView>, IInventoryItemView
    {
        string ItemNumber { get; }
        string Name { get; }
        string LocationName { get; }
        string CustomID { get; }
        int? TaggedQuantity { get; }
        int? StockQuantity { get; }
        int? TotalQuantity { get; }
        decimal BottleCost { get; }
        decimal? TotalCost { get; }
        double? ParBottleCount { get; set; }
    }
}