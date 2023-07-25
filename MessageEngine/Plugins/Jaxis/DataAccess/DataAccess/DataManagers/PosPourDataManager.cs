using System;
using System.Linq;
using Jaxis.Inventory.Data.IDataItems;

namespace Jaxis.Inventory.Data.DataAccess
{
    class PosPourDataManager : DataManager<IPosPour, vwPosPour>, IPosPourManager
    {
        public IQueryable<IPosPour> GetAll()
        {
            return vwPosPour.All();
        }

        public IPosPour Get(Guid _id)
        {
            return vwPosPour.GetByID(_id);
        }
    }
}
