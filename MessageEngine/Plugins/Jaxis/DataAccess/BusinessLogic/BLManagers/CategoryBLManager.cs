using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data
{
    public class CategoryBLManager : BLManager<ICategory, IBLCategory>, ICategoryBLManager
    {
        public IEnumerable<IBLCategory> GetRootCategories( )
        {
            return GetAll( ).Where( c => null == c.ParentID );
        }

        public IEnumerable<IBLCategory> GetSubCategories()
        {
            return GetAll( ).Where( c => null != c.ParentID );
        }

        public IEnumerable<IBLCategory> GetSubCategories(Guid parentID)
        {
            return GetAll( ).Where( c => c.ParentID == parentID );
        }
    }
}
