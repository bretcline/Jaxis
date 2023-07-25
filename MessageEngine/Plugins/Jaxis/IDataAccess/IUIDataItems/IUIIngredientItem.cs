using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jaxis.Inventory.Data.IUIDataItems
{
    public interface IUIIngredientItem
    {
        string Name { get; set; }
        string QualityName { get; }
        double PourStandard { get; set; }
        double StandardVariance { get; set; }
    }
}
