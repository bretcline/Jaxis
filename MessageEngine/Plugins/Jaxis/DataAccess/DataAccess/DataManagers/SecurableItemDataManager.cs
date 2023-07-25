using System;
using System.Collections.Generic;
using System.Linq;

namespace Jaxis.Inventory.Data
{
    public class SecurableItemDataManager : DataManager<ISecurableItem, SecurableItem>, ISecurableItemDataManager
    {
        #region IDataManager<ISecurableItem> Members


        public IQueryable<ISecurableItem> GetAll( )
        {
            return SecurableItem.All( );
        }

        public ISecurableItem Get( Guid ID )
        {
            return SecurableItem.GetByID( ID );
        }

        #endregion
    }
}