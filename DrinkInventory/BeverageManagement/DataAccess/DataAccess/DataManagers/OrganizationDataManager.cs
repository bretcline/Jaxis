using System;
using System.Collections.Generic;
using System.Linq;

namespace Jaxis.Inventory.Data
{
    public class OrganizationDataManager : DataManager<IOrganization, Organization>, IOrganizationDataManager
    {
        #region IDataManager<IOrganization> Members


        public IQueryable<IOrganization> GetAll( )
        {
            return Organization.All();
        }

        public IOrganization Get( Guid ID )
        {
            return Organization.GetByID(ID);
        }

        #endregion
    }
}