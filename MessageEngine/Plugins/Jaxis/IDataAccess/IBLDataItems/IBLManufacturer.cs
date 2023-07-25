using System;

namespace Jaxis.Inventory.Data.IBLDataItems
{
    public interface IBLManufacturer : IManufacturer
    {
        Guid ManufacturerID { get; set; }
        string Name { get; set; }
    }
    public interface IBLManufacturerView : IManufacturerView
    {
        Guid ManufacturerID { get; set; }
        string Name { get; set; }
        Guid CategoryID { get; set; }
        Guid RootCategoryID { get; set; }
    }
}