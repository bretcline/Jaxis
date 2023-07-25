using System;

namespace Jaxis.Inventory.Data.IDataItems
{
    public interface ITicketItemAlias : IDataObject<ITicketItemAlias>
    {
        Guid TicketItemAliasID { get; set; }
        string Description { get; set; }
        Guid? RecipeID { get; set; }
        Guid? PosUPC { get; set; }
        decimal Price { get; set; }
    }

    public interface ITicketItemAliasView : IDataObject<ITicketItemAliasView>
    {
        string Description { get; set; }
        Guid? RecipeID { get; set; }
        Guid? PosUPC { get; set; }
        decimal Price { get; set; }
    }
}
