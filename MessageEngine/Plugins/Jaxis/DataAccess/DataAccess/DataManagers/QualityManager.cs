using System;
using System.Linq;

namespace Jaxis.Inventory.Data
{
    public class QualityManager : DataManager<IQuality, Quality>, IQualityManager
    {
        #region IDataManager<IPour> Members

        public IQueryable<IQuality> GetAll()
        {
            return Quality.All();
        }

        public IQuality Get(Guid ID)
        {
            return Quality.GetByID(ID);
        }

        #endregion
    }
}