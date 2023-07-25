using System;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data
{
    /// <summary>
    /// A Business Logic interface which represents the UPCs table in the BevMetMobile Database.
    /// </summary>
    public interface IUIUPCItem
    {
        //Guid UPCID { get; set; }
        string ItemNumber { get; set; }
        string CategoryName { get; }
        //IUICategory Category { get; }
        string Manufacturer { get; }
        string Name { get; set; }
        int Size { get; set; }
        //Guid SizeTypeID { get; set; }
        IBLStandardNozzle Nozzle { get; set; }

        Guid CategoryID { get; set; }
        Guid RootCategoryID { get; set; }
    }

    public interface IUIUPCItemShort
    {
        string ItemNumber { get; set; }
        string Name { get; set; }
        int Size { get; set; }
    }
}