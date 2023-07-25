using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jaxis.Inventory.Data.IUIDataItems
{
    public interface IUIRecipeItem
    {
        string Name { get; set; }
        string TicketItemAlias { get; set; }
    }
}
