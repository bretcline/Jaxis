using System;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data
{
    /// <summary>
    /// A Business Logic interface which represents the Tags table in the BevMetMobile Database.
    /// </summary>
    public interface IUITag
    {
        //Guid TagID { get; set; }
        //Guid? UPCID { get; set; }
        string TagNumber { get; set; }
        IBLLocation CurrentLocation { get; set; }
        //Guid? EventID { get; set; }
        // MLF moved to Inventory double? Quantity { get; set; }
        IStandardNozzle Nozzle { get; set; }

        IBLUPCItem UPC { get; /*set;*/  }

        //IBLCart Cart { get; set; }
        //IBLEvent CurrentEvent { get; set; }
    }
}

