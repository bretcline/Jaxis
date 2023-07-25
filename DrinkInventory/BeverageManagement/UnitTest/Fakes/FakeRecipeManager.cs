using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.DrinkInventory.BeverageManagement.UnitTest.Fakes
{
    internal class FakeRecipeManager : FakeManager<IBLRecipe,IRecipe,Recipe>, IRecipeBLManager
    {
    }
}
