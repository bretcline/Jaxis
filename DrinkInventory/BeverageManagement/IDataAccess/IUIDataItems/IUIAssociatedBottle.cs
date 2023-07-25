using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data
{
    public interface IUIAssociatedBottle
    {
        IBLTag Tag { get; set; }
        IBLUPCItem UPC { get; set; }
        IBLLocation ToLocation{ get; set; }
        IBLLocation FromLocation { get; set; }
        double? Quantity { get; set; }
        //double? NozzleDiameter { get; set; }
        IBLStandardNozzle Nozzle { get; set; }
    }


}
