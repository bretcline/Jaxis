using System;
using System.Collections.Generic;
using System.Linq;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.BusinessLogic.BLObjects;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Inventory.Data.IUIDataItems;

namespace Jaxis.DrinkInventory.BeverageManagement.UnitTest.Fakes
{
    internal class FakePourManager : FakeManager<IBLPour,IPour,Pour>, IPourBLManager
    {
        public IEnumerable<IBLPour> Top(int _count)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, KeyValuePair<DateTime, double>> GetPourTotals(DateTime _start, DateTime _end)
        {
            throw new NotImplementedException();
        }

        public IList<IUIPourPoint> GetPourPoints(int _pointCount)
        {
            throw new NotImplementedException();
        }
    }
}
