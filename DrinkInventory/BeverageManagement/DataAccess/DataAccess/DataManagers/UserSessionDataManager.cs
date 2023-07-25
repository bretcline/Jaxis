using System;
using System.Collections.Generic;
using System.Linq;

namespace Jaxis.Inventory.Data
{
    public class UserSessionDataManager : DataManager<IUserSession, UserSession>, IUserSessionDataManager
    {
        #region IDataManager<IUserSession> Members


        public IQueryable<IUserSession> GetAll( )
        {
            return UserSession.All();
        }

        public IUserSession Get( Guid ID )
        {
            return UserSession.GetByID(ID);
        }

        #endregion
    }
}