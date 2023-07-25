using System;
using System.Collections.Generic;
using System.Linq;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Inventory.Data.IDataItems;
using Jaxis.Inventory.Data.IUIDataItems;

namespace Jaxis.DrinkInventory.BeverageManagement.UnitTest.Fakes
{
    class FakePosPourManager : FakeManager<IBLPosPour, IPosPour, vwPosPour>, IPosPourBLManager
    {
        public new IEnumerable<IBLPosPour> GetAll()
        {
            return from p in FakeManagerFactory.Get().ManagePours().GetAll()
                join u in FakeManagerFactory.Get().ManageUPCs().GetAll() on p.UPCID equals u.UPCID
                join c in FakeManagerFactory.Get().ManageCategories().GetAll() on u.CategoryID equals c.CategoryID
                select new vwPosPour
                {
                Category = c.Name,
                PourAmount = p.Volume,
                PourTime = p.PourTime,
                StatusText = "Pending",
                Type = u.Name
                };
        }

        public IEnumerable<IUIPosPour> GetAfter(DateTime _beginDate)
        {
            return (from p in GetAll() where p.PourTime >= _beginDate select p).ToList().Cast<IUIPosPour>();
        }
    }
}
