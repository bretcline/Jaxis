using System;
using System.Collections.Generic;
using System.Linq;

namespace Jaxis.Inventory.Data
{
    public class InventoryDataManager : DataManager<IInventory, Inventory>, IInventoryDataManager
    {
        #region IDataManager<IInventory> Members


        public IQueryable<IInventory> GetAll( )
        {
            // Should we ever be able to get inventory that has already exited the system?
            //return Inventory.All();
            return Inventory.All().Where(I => I.ExitDate == null);
        }

        public IInventory Get( Guid ID )
        {
            return Inventory.GetByID(ID);
        }

        #endregion
    }
}