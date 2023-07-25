using Jaxis.Inventory.Data.IDataItems;
using System;

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

    // ReSharper disable InconsistentNaming
    public interface IBLTicketItemAliasView : ITicketItemAliasView
    // ReSharper restore InconsistentNaming
    {
        string Description { get; set; }
        string Recipe { get; set; }
        decimal Price { get; set; }
        IBLTicketItemAlias GetTicketItemAlias();
    }

    public interface IUITicketItemAliasView
    {
        string Description { get; set; }
        bool AutoInventory { get; }
        decimal Price { get; set; }
    }

    public interface ITicketItemAliasDisplayView : IBLTicketItemAliasView
    {
        string Description { get; }
        string Recipe { get; }
        decimal Price { get; set; }
    }

}
