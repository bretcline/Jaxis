using System;

namespace Jaxis.Inventory.Data
{
    public interface IStandardPrice : IDataObject<IStandardPrice>
    {
        Guid StandardPriceID { get; set; }
        decimal SinglePrice { get; set; }
        decimal DoublePrice { get; set; }
    }
}