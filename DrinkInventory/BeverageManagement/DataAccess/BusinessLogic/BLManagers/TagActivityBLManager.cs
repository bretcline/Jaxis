using System.Collections.Generic;
using System.Linq;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data
{
    public class TagActivityBLManager : BLManager<ITagActivity, IBLTagActivity>, ITagActivityBLManager
    {
        public IEnumerable<IBLTagActivity> Top( int count )
        {
            return DataManagerFactory.Get( ).Manage<ITagActivity>( ).GetAll( )
                .OrderByDescending( A => A.ActivityTime ).Take( count ).ToList( ).Cast<IBLTagActivity>( );
        }

    }
}