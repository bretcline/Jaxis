using System;

namespace Jaxis.Inventory.Data.IBLDataItems
{
    /// <summary>
    /// A Business Logic interface which represents the SizeTypes table in the BevMetMobile Database.
    /// </summary>
    public interface IBLSizeType : ISizeType
    {
        Guid SizeTypeID { get; set; }
        string Name { get; set; }
        string Abbreviation { get; set; }
        double ConversionMultiplier { get; set; }

    }
}