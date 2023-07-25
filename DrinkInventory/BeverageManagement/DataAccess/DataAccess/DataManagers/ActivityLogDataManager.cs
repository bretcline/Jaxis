using System;
using System.Linq;

namespace Jaxis.Inventory.Data
{
    public class ActivityLogDataManager : DataManager<IActivityLog, ActivityLog>, IActivityLogDataManager, IDataManager
    {
        #region IDataManager<ITag> Members

        public IQueryable<IActivityLog> GetAll( )
        {
            return ActivityLog.All( );
        }

        public IActivityLog Get( Guid ID )
        {
            return ActivityLog.GetByID( ID );
        }

        #endregion
    }
}