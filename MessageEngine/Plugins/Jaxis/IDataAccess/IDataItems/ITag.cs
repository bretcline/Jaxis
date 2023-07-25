using System;

namespace Jaxis.Inventory.Data
{
    /// <summary>
    /// A class which represents the mobTags table in the BevMetMobile Database.
    /// </summary>
    public interface ITag : IDataObject<ITag>
    {
        Guid TagID { get; set; }
        //Guid? UPCID { get; /*set;*/ }
        string TagNumber { get; set; }
        Guid LocationID { get; set; }
        double? Quantity { get; set; }
        IStandardNozzle Nozzle { get; set; }
        Guid? StandardNozzleID { get; }
        IInventory CurrentInventory { get; }
        ExitReasons ExitReason { get; set; }
        string Memo { get; set; }
    }
}