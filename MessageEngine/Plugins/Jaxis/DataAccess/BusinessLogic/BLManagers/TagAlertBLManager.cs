using System.Collections.Generic;
using System.Linq;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data
{
    public class TagAlertBLManager : BLManager<ITagAlert, IBLTagAlert>, ITagAlertBLManager
    {
        public IEnumerable<IBLTagAlert> Top( int count )
        {
            return DataManagerFactory.Get( ).Manage<ITagAlert>( ).GetAll( )
                .OrderByDescending( A => A.AlertTime ).Take( count ).ToList( ).Cast<IBLTagAlert>( );
        }
    }
}