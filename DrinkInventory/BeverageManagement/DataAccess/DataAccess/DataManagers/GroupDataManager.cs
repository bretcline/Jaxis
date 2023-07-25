using System;
using System.Collections.Generic;
using System.Linq;

namespace Jaxis.Inventory.Data
{
    public class GroupDataManager : DataManager<IGroup, Group>, IGroupDataManager
    {
        #region IDataManager<IGroup> Members


        public IQueryable<IGroup> GetAll( )
        {
            return Group.All();
        }

        public IGroup Get( Guid ID )
        {
            return Group.GetByID(ID);
        }

        #endregion
    }
}