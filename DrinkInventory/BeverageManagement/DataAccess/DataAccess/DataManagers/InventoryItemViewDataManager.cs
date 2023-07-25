using System;
using System.Linq;

namespace Jaxis.Inventory.Data
{
    public class InventoryItemViewDataManager : DataManager<IInventoryItemView, vwInventoryItem>, IInventoryItemViewDataManager
    {
        #region IDataManager<IInventoryItemView> Members


        public IQueryable<IInventoryItemView> GetAll()
        {
            return vwInventoryItem.All();
        }

        public IInventoryItemView Get(Guid ID)
        {
            return vwInventoryItem.GetByID(ID);
        }

        #endregion
    }
}