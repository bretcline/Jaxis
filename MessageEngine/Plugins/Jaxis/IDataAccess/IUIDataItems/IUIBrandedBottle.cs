using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data
{
    public interface IUIBrandedBottle
    {
        IBLTag Tag { get; set; }
        IBLUPCItem UPC { get; set; }
        double? Quantity { get; set; }
        IBLStandardNozzle Nozzle { get; set; }
        //double? NozzleDiameter { get; set; }
    }
}