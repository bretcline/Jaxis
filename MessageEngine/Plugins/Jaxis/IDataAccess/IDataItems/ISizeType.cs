using System;

namespace Jaxis.Inventory.Data
{
    /// <summary>
    /// A class which represents the SizeType table in the BevMetMobile Database.
    /// </summary>
    public interface ISizeType : INamedObject, IDataObject<ISizeType>
    {
        Guid SizeTypeID { get; set; }
        string Abbreviation { get; set; }
        double ConversionMultiplier { get; set; }
    }
}