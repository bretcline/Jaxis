using System.Collections.Generic;
using System.Linq;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data
{
    public class ActivityLogBLManager : BLManager<IActivityLog, IBLActivityLog>, IActivityLogBLManager
    {
        public IEnumerable<IBLActivityLog> Top( int count )
        {
            return DataManagerFactory.Get().Manage<IActivityLog>().GetAll()
                .OrderByDescending(A => A.ActivityTime).Take(count).ToList().Cast<IBLActivityLog>();
        }
    }
}