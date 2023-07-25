using System;
using Jaxis.Inventory.Data.IUIDataItems;

namespace Jaxis.Inventory.Data.IDataItems
{
    public interface IPosPour : IDataObject<IPosPour>
    {
        string StatusText { get; set; }
        double PourAmount { get; set; }
        string Type { get; set; }
        string Category { get; set; }
        DateTime PourTime { get; set; }
    }
}
