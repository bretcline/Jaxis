using System;
using System.Linq;
using Jaxis.Util.Log4Net;

namespace Jaxis.Inventory.Data
{
    public class ManufacturerDataManager : DataManager<IManufacturer, Manufacturer>, IManufacturerDataManager, IDataManager
    {


        #region IDataManager<IReport> Members


        public IQueryable<IManufacturer> GetAll()
        {
            return Manufacturer.All();
        }

        public IManufacturer Get(Guid ID)
        {
            IManufacturer rc = null;
            Log.Time("GetManufacturer", LogType.Debug, true, () => { rc = Manufacturer.GetByID(ID); });
            return rc;
        }

        #endregion
    }


    public class ManufacturerViewDataManager : DataManager<IManufacturerView, vwManufacturer>, IManufacturerViewDataManager, IDataManager
    {


        #region IDataManager<IReport> Members


        public IQueryable<IManufacturerView> GetAll()
        {
            return vwManufacturer.All();
        }

        public IManufacturerView Get(Guid ID)
        {
            IManufacturerView rc = null;
            Log.Time("GetManufacturerView", LogType.Debug, true, () => { rc = vwManufacturer.GetByID(ID); });
            return rc;
        }

        #endregion
    }
}