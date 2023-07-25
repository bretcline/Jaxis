using System.Collections.Generic;
using System.Linq;
using Jaxis.Inventory.Data.BusinessLogic.BLObjects;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data
{
    public class InventoryItemBLManager : BLOnlyManager<IBLInventoryItem, BLInventoryItem>, IInventoryItemBLManager
    {
        #region IBLManager<IUIAssociatedBottle> Members


        public IBLInventoryItem Get( System.Guid ID )
        {
            return null;
        }

        #endregion

        public IBLInventoryItem Create( IBLInventory _Item )
        {
            BLInventoryItem rc = new BLInventoryItem();
            rc.Inventory = _Item;

            rc.Location = _Item.Location;
            rc.UPC = _Item.UPC;
            //rc.StockQuantity = _Item.Quantity;
            rc.Name = rc.UPC.Name;
            return rc;
        }

        public IBLInventoryItem Create( IBLTag _Item )
        {
            BLInventoryItem rc = new BLInventoryItem( );
            rc.Tag = _Item;

            IBLInventory item = BLManagerFactory.Get().ManageInventory().Create();
            item.Location = _Item.CurrentLocation;
            item.UPC = _Item.UPC;
            item.Amount = _Item.UPC.Size;
            item.Cost = _Item.UPC.UnitPrice.HasValue ? _Item.UPC.UnitPrice.Value : 0;
            rc.Inventory = item;

            rc.Location = _Item.CurrentLocation;
            rc.UPC = _Item.UPC;
            rc.Name = rc.UPC.Name;

            return rc;
        }

        public override bool Save( IBLInventoryItem item )
        {
            var tagManager = BLManagerFactory.Get().ManageTags();
            var inventoryManager = BLManagerFactory.Get().ManageInventory();
            BLInventoryItem BLItem = item as BLInventoryItem;
            if( null != BLItem )
            {
                if( null != BLItem.Inventory )
                {
                    inventoryManager.Save(BLItem.Inventory);
                    //BLItem.Inventory.Save();
                }
                else if( null != BLItem.Tag )
                {
                    tagManager.Save(BLItem.Tag);
                    //BLItem.Tag.Save();
                }
            }
            return base.Save( item );
        }

        #region IInventoryItemBLManager Members


        public void CleanupInventory( )
        {
            //var Man = DataManagerFactory.Get().Manage<IInventory>();

            //List<IInventory> Items = Man.GetAll();


        }

        #endregion
    }
}