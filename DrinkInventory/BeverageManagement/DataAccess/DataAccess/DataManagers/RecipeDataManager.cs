using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jaxis.Inventory.Data
{
    public class RecipeDataManager : DataManager<IRecipe, Recipe>, IRecipeManager, IDataManager
    {
        #region IDataManager<IRecipe> Members


        public IQueryable<IRecipe> GetAll()
        {
            return Recipe.All();
        }

        public IRecipe Get(Guid ID)
        {
            return Recipe.GetByID(ID);
        }

        #endregion
    }
}
