using System;
using System.Collections.Generic;
using System.Linq;

namespace Jaxis.Inventory.Data
{
    public class PourDataManager : DataManager<IPour, Pour>, IPourDataManager
    {
        #region IDataManager<IPour> Members

        public IQueryable<IPour> GetAll()
        {
            return Pour.All();
        }

        public IPour Get(Guid ID)
        {
            return Pour.GetByID(ID);
        }

        #endregion
    }
}