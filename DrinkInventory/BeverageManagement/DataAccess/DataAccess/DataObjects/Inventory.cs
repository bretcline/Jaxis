using System;
using System.Linq;
using System.Collections.Generic;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Util.Log4Net;

namespace Jaxis.Inventory.Data
{
    /// <summary>
    /// A class which represents the Inventory table in the BevMetMobile Database.
    /// </summary>
    public partial class Inventory : IInventory, IBLInventory, IUIInventory
    {
        public IEnumerable<IInventory> GetAll( )
        {
            return All( );
        }

        partial void OnSaving()
        {
            this.UpdateTime = DateTime.Now;
        }


        #region IBLInventory Members

        public IBLUPCItem UPC
        {
            get
            { //return this.UPCItem.FirstOrDefault(); }
                IUPCItemBLManager Man = BLManagerFactory.Get().ManageUPCs();
                return Man.Get(this.UPCID);
            }
            set { this.UPCID = value.ObjectID; }
        }

        public IBLLocation Location
        {
            get
            { //return this.LocationItem.FirstOrDefault(); }
                ILocationBLManager Man = BLManagerFactory.Get().ManageLocations();
                return Man.Get(this.LocationID);
            }
            set { this.LocationID = value.LocationID; }
        }

        #endregion
    }
}