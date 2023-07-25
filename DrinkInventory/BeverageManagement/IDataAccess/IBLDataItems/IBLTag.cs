using System;

namespace Jaxis.Inventory.Data.IBLDataItems
{
    /// <summary>
    /// A Business Logic interface which represents the Tags table in the BevMetMobile Database.
    /// </summary>
    public interface IBLTag : ITag
    {
        Guid TagID { get; set; }
        //Guid? UPCID { get; /*set;*/ }
        string TagNumber { get; set; }
        IBLLocation CurrentLocation { get; set; }
        IBLStandardNozzle Nozzle { get; set; }
        IBLUPCItem UPC { get; /*set;*/ }

        IBLInventory CurrentInventory { get; }
        ExitReasons ExitReason { get; set; }
        string Memo { get; set; }

    }
}