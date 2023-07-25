using System;
using System.Collections.Generic;
using System.Linq;

namespace Jaxis.Inventory.Data
{
    public class LocationDataManager : DataManager<ILocation, Location>, ILocationDataManager
    {
        #region IDataManager<ILocation> Members


        public IQueryable<ILocation> GetAll( )
        {
            return Location.All();
        }

        public ILocation Get( Guid ID )
        {
            return Location.GetByID(ID);
        }

        #endregion
    }
}