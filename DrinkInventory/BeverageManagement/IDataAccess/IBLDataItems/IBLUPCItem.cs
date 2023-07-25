using System;

namespace Jaxis.Inventory.Data.IBLDataItems
{
    /// <summary>
    /// A Business Logic interface which represents the UPCs table in the BevMetMobile Database.
    /// </summary>
    public interface IBLUPCItem : IUPCItem
    {
        Guid UPCID { get; set; }
        string ItemNumber { get; set; }
        string Manufacturer { get; }
        Guid ManufacturerID { get; set; }
        string Name { get; set; }
        int Size { get; set; }
        Guid SizeTypeID { get; set; }
        IBLSizeType SizeType { get; set; }
        Guid CategoryID { get; set; }
        IBLCategory Category { get; set; }
        IBLStandardNozzle Nozzle { get; set; }
        IBLStandardPrice Price { get; set; }
        decimal? UnitPrice { get; set; }
        double PourModifier { get; set; }
        int MinimumParLevel { get; set; }
        Guid? ChildUPCID { get; set; }
        int? BottleCount { get; set; }
    }
}