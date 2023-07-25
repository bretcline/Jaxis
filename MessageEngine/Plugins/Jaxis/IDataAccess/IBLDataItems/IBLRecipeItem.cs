using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jaxis.Inventory.Data.IBLDataItems
{
    public interface IBLRecipeItem
    {
        string Name { get; set; }
        string TicketItemAlias { get; set; }
        List<IBLIngredientItem> Ingredients { get; set; }
    }
}
