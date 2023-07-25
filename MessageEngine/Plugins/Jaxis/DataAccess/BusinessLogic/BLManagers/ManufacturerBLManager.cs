using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data
{
    public class ManufacturerViewBlManager : BLManager<IManufacturerView, IBLManufacturerView>, IManufacturerViewBLManager { }

    public class ManufacturerBlManager : BLManager<IManufacturer, IBLManufacturer>, IManufacturerBLManager { }
}