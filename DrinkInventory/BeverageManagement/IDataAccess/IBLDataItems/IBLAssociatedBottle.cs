namespace Jaxis.Inventory.Data.IBLDataItems
{
    public interface IBLAssociatedBottle : IBLBottle
    {
        string TagNumber { get; }
        //IBLTag Tag { get; set; }
        string UPCName { get; }
        //IBLUPCItem UPC { get; set; }
        double? Quantity { get; set; }
        IBLStandardNozzle Nozzle { get; set; }
        IBLLocation ToLocation { get; set; }
        IBLLocation FromLocation { get; set; }
        int BottleCount { get; set; }
        bool NewInventory { get; set; }
        decimal Price { get; set; }
    }
}
