using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeverageMonitor.Entities
{
    public interface  IUPC
    {
        string ItemNumber { get; set; }
        string Name { get; set; }
        int Size { get; }
        int Quality { get; set; }
        decimal? UnitPrice { get; set; }
        Guid ManufacturerID { get; set; }
    }

    public partial class UPC : IUPC
    {
    }
}
