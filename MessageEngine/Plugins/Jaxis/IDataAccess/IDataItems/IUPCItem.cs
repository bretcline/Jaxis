using System;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data
{
    /// <summary>
    /// A class which represents the mobUPCs table in the BevMetMobile Database.
    /// </summary>
    public interface IUPCItem : INamedObject, IDataObject<IUPCItem>
    {
        Guid UPCID { get; set; }
        Guid CategoryID { get; set; }
        string ItemNumber { get; set; }
        string Manufacturer { get; }
        Guid ManufacturerID { get; set; }
        int Size { get; set; }
        IStandardNozzle Nozzle { get; set; }
       // IStandardPrice Price { get; set; }
        decimal? UnitCost { get; set; }
        int Quality { get; set; }

        ISizeType SizeType { get; set; }
        ICategory Category { get; set; }
        Guid SizeTypeID { get; set; }
        Guid RootCategoryID { get; set; }

        string CustomID { get; set; }
        double PourModifier { get; set; }
        int MinimumParLevel { get; set; }

        bool AllowHalfPour { get; set; }
        Guid? ChildUPCID { get; set; }
        int? BottleCount { get; set; }
    }
}