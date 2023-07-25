using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data.BusinessLogic.BLManagers
{
    // ReSharper disable InconsistentNaming
    public class RecipeBLManager : BLManager<IRecipe, IBLRecipe>, IRecipeBLManager
    // ReSharper restore InconsistentNaming
    {
        public RecipeBLManager()
        {
            OnDelete = DeleteIngredients;
        }

        private void DeleteIngredients(IBLRecipe _recipe)
        {
            var man = BLManagerFactory.Get().ManageIngredients();
            foreach (var ingredient in _recipe.Ingredients)
            {
                man.Delete(ingredient);
                //ingredient.Delete();
            }
        }
    }
    
    // ReSharper disable InconsistentNaming
}
