using System;
using System.Collections.Generic;
using System.Linq;

namespace Jaxis.Inventory.Data
{
    public class UPCItemDataManager : DataManager<IUPCItem, UPC>, IUPCItemDataManager
    {
        #region IDataManager<IUPCItem> Members


        public IQueryable<IUPCItem> GetAll( )
        {
            return UPC.All();
        }


        public IUPCItem Get( Guid ID )
        {
            return UPC.GetByID( ID );
        }

        public IUPCItem GetUPC( Guid ID )
        {
            return UPC.GetByID( ID );
        }

        #endregion
    }

    public class UPCItemDataViewManager : DataManager<IUPCItemView, vwUPCItem>, IUPCItemDataViewManager
    {
        #region IDataManager<IUPCItem> Members


        public IQueryable<IUPCItemView> GetAll( )
        {
            return vwUPCItem.All( );
        }


        public IUPCItemView Get( Guid ID )
        {
            return vwUPCItem.GetByID( ID );
        }

        public IUPCItem GetUPC( Guid ID )
        {
            return UPC.GetByID( ID );
        }

        #endregion
    }

}