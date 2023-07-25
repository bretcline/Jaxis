using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jaxis.Inventory.Data
{
    public class IngredientDataManager : DataManager<IIngredient, Ingredient>, IIngredientManager, IDataManager
    {
        #region IDataManager<IIngredient> Members


        public IQueryable<IIngredient> GetAll()
        {
            return Ingredient.All();
        }

        public IIngredient Get(Guid ID)
        {
            return Ingredient.GetByID(ID);
        }

        #endregion
    }
}
