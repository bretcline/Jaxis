using System;

namespace Jaxis.Inventory.Data.IBLDataItems
{
    public interface  IBLBottle
    {
        IBLTag Tag { get; set; }
        IBLUPCItem UPC { get; set; }
        double? Quantity { get; set; }
        IBLStandardNozzle Nozzle { get; set; }
        bool NewInventory { get; set; }
        IBLLocation FromLocation { get; set; }
        IBLLocation ToLocation { get; set; }
    }

    public interface IBLBrandedBottle : IBLBottle
    {
        string TagNumber { get; }
        //IBLTag Tag { get; set; }
        string UPCName { get; }
        //IBLUPCItem UPC { get; set; }
        double? Quantity { get; set; }
        IBLStandardNozzle Nozzle { get; set; }
        IBLLocation FromLocation { get; set; }
        IBLLocation ToLocation { get; set; }
        bool NewInventory { get; set; }
        decimal Price { get; set; }
    }
}