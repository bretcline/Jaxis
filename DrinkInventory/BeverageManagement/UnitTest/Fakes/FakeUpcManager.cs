using System;
using System.Collections.Generic;
using System.Linq;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.DrinkInventory.BeverageManagement.UnitTest.Fakes
{
    class FakeUpcManager : FakeManager<IBLUPCItem,IUPCItem,UPC>, IUPCItemBLManager
    {
        public IQueryable<IBLUPCItem> GetUPCsByRootCategory(IBLCategory rootCategory, IQueryable<IBLUPCItem> query)
        {
            throw new NotImplementedException();
        }

        public IQueryable<IBLUPCItem> GetUPCsBySubCategory(IBLCategory subCategory, IQueryable<IBLUPCItem> query)
        {
            throw new NotImplementedException();
        }

        public IQueryable<IBLUPCItem> GetUPCsByManufacturer(IBLCategory subCategory, string manufacturer, IQueryable<IBLUPCItem> query)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetManufacturersBySubCategory(Guid CategoryID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetManufacturers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IBLUPCItem> GetAllView()
        {
            throw new NotImplementedException();
        }

        public bool ImportUPCList(string _fileName)
        {
            throw new NotImplementedException();
        }


        public IBLUPCItem GetUPCByUPCNumber(string _upcNumber)
        {
            throw new NotImplementedException();
        }
    }
}
