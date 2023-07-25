using System;
using System.Collections.Generic;
using System.Linq;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Inventory.Data.IDataItems;
using Jaxis.Inventory.Data.IUIDataItems;

namespace Jaxis.Inventory.Data.BusinessLogic.BLManagers
{
// ReSharper disable InconsistentNaming
    public class PosPourBLManager : BLManager<IPosPour, IBLPosPour>, IPosPourBLManager
// ReSharper restore InconsistentNaming
    {
        public IEnumerable<IUIPosPour> GetAfter(DateTime _beginDate)
        {
            return (from p in DataManagerFactory.Get().Manage<IPosPour>().GetAll()
                where p.PourTime >= _beginDate
                select p).ToList().Cast<IUIPosPour>();
        }
    }
}
