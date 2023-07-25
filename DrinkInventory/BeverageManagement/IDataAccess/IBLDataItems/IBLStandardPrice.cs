using System;

namespace Jaxis.Inventory.Data.IBLDataItems
{
    public interface IBLStandardPrice : IStandardPrice
    {
        Guid StandardPriceID { get; set; }
        decimal SinglePrice { get; set; }
        decimal DoublePrice { get; set; }
    }
}