using System;
using System.Collections.Generic;
using System.Linq;

namespace Jaxis.Inventory.Data
{
    public class CategoryDataManager : DataManager<ICategory, Category>, ICategoryDataManager
    {
        #region IDataManager<ICategory> Members


        public IQueryable<ICategory> GetAll( )
        {
            return Category.All( );
        }

        public ICategory Get( Guid ID )
        {
            return Category.GetByID(ID);
        }

        #endregion
    }
}