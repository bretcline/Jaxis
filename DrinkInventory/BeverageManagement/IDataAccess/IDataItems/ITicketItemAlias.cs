using System;

namespace Jaxis.Inventory.Data.IDataItems
{
    public interface ITicketItemAlias : IDataObject<ITicketItemAlias>
    {
        string Description { get; set; }
        Guid? RecipeID { get; set; }
        Guid? PosUPC { get; set; }
        decimal Price { get; set; }
    }
}
