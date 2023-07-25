using System;

namespace Jaxis.Inventory.Data
{
    public interface IManufacturer : IDataObject<IManufacturer>
    {
        Guid ManufacturerID { get; }
        string Name { get; }
    }
    public interface IManufacturerView : IDataObject<IManufacturerView>
    {
        Guid ManufacturerID { get; }
        string Name { get; }
        Guid CategoryID { get; }
        Guid RootCategoryID { get; }
    }
}