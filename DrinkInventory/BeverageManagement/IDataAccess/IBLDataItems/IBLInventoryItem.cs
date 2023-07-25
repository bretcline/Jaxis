namespace Jaxis.Inventory.Data.IBLDataItems
{
    public interface IBLInventoryItem
    {
        IBLUPCItem UPC { get; set; }
        string Name { get; set; }
        IBLLocation Location { get; set; }
        long TaggedQuantity { get; }
        long StockQuantity { get; }
        long TotalQuantity { get; }
    }


    public interface IUIInventoryItem
    {
        string Name { get; }
        string LocationName { get; }
        long TaggedQuantity { get; }
        long StockQuantity { get; }
        long TotalQuantity { get; }
    }




    //public interface IInventoryItem
    //{
    //    string Name { get; set; }
    //    int? TaggedQuantity { get; }
    //    int? StockQuantity { get; }
    //    int? TotalQuantity { get; }
    //}

    //public interface IBLInventoryItem : IInventoryItem
    //{
    //    IBLUPCItem UPC { get; set; }
    //    string Name { get; set; }
    //    IBLLocation Location { get; set; }
    //    int? TaggedQuantity { get; }
    //    int? StockQuantity { get; }
    //    int? TotalQuantity { get; }
    //}


    //public interface IUIInventoryItem : IInventoryItem
    //{
    //    string Name { get; }
    //    string LocationName { get; }
    //    int? TaggedQuantity { get; }
    //    int? StockQuantity { get; }
    //    int? TotalQuantity { get; }
    //}
}