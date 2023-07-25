using System;
using System.Linq;

namespace Jaxis.Inventory.Data
{
    public class ParLevelDataManager : DataManager<IParLevel, ParLevel>, IParLevelDataManager, IDataManager
    {
        #region IDataManager<IPOSTicket> Members


        public IQueryable<IParLevel> GetAll()
        {
            return ParLevel.All();
        }

        public IParLevel Get(Guid ID)
        {
            return ParLevel.GetByID(ID);
        }

        #endregion
    }
}