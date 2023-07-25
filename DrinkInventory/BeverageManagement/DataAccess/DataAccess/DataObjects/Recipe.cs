using System.Collections.Generic;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data
{
    /// <summary>
    /// A class which represents the mobPours table in the BevMetMobile Database.
    /// </summary>
    public partial class Recipe : IBLRecipe
    {
        private IList<IBLIngredient> m_ingredients;
        private IList<Ingredient> m_removedIngredients;

        public IEnumerable<IRecipe> GetAll()
        {
            return All();
        }

        public override string ToString()
        {
            return Description;
        }

        IEnumerable<IBLIngredient> IBLRecipe.Ingredients
        {
            get
            {
                return m_ingredients ?? (m_ingredients = new List<IBLIngredient>(
                    Ingredient.Find(_i => _i.RecipeID == RecipeID)));
            }
        }

        public IBLIngredient NewIngredient()
        {
            var ingredient = new Ingredient { RecipeID = RecipeID };
            ((List<IBLIngredient>)((IBLRecipe)this).Ingredients).Add(ingredient);
            return ingredient;
        }
       
        public void RemoveIngredient(IBLIngredient _ingredient)
        {
            var dbIngredient = (Ingredient)_ingredient;

            if (!dbIngredient.IsNew())
            {
                if (m_removedIngredients == null)
                    m_removedIngredients = new List<Ingredient>();
                m_removedIngredients.Add(dbIngredient);
            }

            ((List<IBLIngredient>)((IBLRecipe)this).Ingredients).Remove(_ingredient);
        }

        partial void OnSaved()
        {
            if (m_ingredients != null)
            {
                foreach (var lineItem in m_ingredients)
                    ((Ingredient)lineItem).Save();
            }

            if (m_removedIngredients != null)
            {
                foreach (var ingredient in m_removedIngredients)
                    ingredient.Delete();
            }
        }


        System.Linq.IQueryable<IIngredient> IRecipe.Ingredients
        {
            get { return this.Ingredients; }
        }
    }
}
