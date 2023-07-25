using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jaxis.Inventory.Data.IBLDataItems
{
    public interface IBLIngredientItem
    {
        string TicketItemAlias { get; set; }
        string RecipeName { get; set; }
        string Name { get; set; }
        Guid IngredientID { get; set; }
        Guid StandardPourID { get; set; }
        int Quality { get; set; }
        string QualityName { get; }
        double PourStandard { get; set; }
        double StandardVariance { get; set; }
        Guid? PourID { get; set; }
    }
}
