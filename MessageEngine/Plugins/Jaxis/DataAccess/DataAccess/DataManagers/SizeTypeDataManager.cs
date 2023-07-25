using System;
using System.Collections.Generic;
using System.Linq;

namespace Jaxis.Inventory.Data
{
    public class SizeTypeDataManager : DataManager<ISizeType, SizeType>, ISizeTypeDataManager
    {
        #region IDataManager<ISizeType> Members


        public IQueryable<ISizeType> GetAll( )
        {
            return SizeType.All();
        }

        public ISizeType Get( Guid ID )
        {
            return SizeType.GetByID(ID);
        }

        #endregion
    }
}