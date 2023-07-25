using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jaxis.Inventory.Data.Reports
{
    public interface IRPourTotals
    {
        string Type { get; }
        string Manufacturer { get; }
        string Name { get; }
        int Count { get; }
        double Volume { get; }
    }

    public interface IRTagPourTotals
    {
        string Type { get; }
        string Manufacturer { get; }
        string Name { get; }
        string TagNumber { get; }
        int Count { get; }
        double Volume { get; }
    }
}
