using System;
using System.Linq;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.DrinkInventory.BeverageManagement.UnitTest.Fakes
{
    class FakeCategoryManager : FakeManager<IBLCategory,ICategory,Category>, ICategoryBLManager
    {
        public IQueryable<IBLCategory> GetRootCategories()
        {
            throw new NotImplementedException();
        }

        public IQueryable<IBLCategory> GetSubCategories()
        {
            throw new NotImplementedException();
        }

        public IQueryable<IBLCategory> GetSubCategories(Guid parentID)
        {
            throw new NotImplementedException();
        }
    }
}
