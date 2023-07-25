using Jaxis.Inventory.Data.IDataItems;

namespace Jaxis.Inventory.Data.IBLDataItems
{
// ReSharper disable InconsistentNaming
    public interface IBLTicketItemAlias : ITicketItemAlias
// ReSharper restore InconsistentNaming
    {
    }

    public interface IUITicketItemAlias
    {
        string Description { get; set; }
        bool AutoInventory { get; }
        decimal Price { get; set; }
    }
}
