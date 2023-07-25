using System;
using System.Collections.Generic;

namespace Jaxis.Inventory.Data.IBLDataItems
{
// ReSharper disable InconsistentNaming
    public interface IBLRecipe : IRecipe
// ReSharper restore InconsistentNaming
    {
        IBLIngredient NewIngredient();
        IEnumerable<IBLIngredient> Ingredients { get; }
        void RemoveIngredient(IBLIngredient _ingredient);
        string Description { get; set; }
        Guid RecipeID { get; set; }
    }
}
