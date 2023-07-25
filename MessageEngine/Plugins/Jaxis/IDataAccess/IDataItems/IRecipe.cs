using System;
using System.Collections.Generic;
using System.Linq;

namespace Jaxis.Inventory.Data
{
    /// <summary>
    /// A class which represents the mobPours table in the BevMetMobile Database.
    /// </summary>
    public interface IRecipe : IDataObject<IRecipe>
    {
        Guid RecipeID { get; set; }
        string Description { get; set; }
        IQueryable<IIngredient> Ingredients { get; }
    }
}
