using System;
using System.Linq;
using System.Collections.Generic;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data
{
    public partial class vwInventoryItem : IInventoryItemView, IBLInventoryItemView, IUIInventoryItemView
    {
        public IEnumerable<IInventoryItemView> GetAll()
        {
            return vwInventoryItem.All();
        }

        partial void OnSaving()
        {
            if (ParBottleCount.HasValue && 0 < ParBottleCount.Value)
            {
                var parMan = BLManagerFactory.Get().ManageParLevels();
                var par = parMan.GetAll().Where(p => p.UPCID == this.UPCID && p.LocationID == this.LocationID).FirstOrDefault();
                if (null == par)
                {
                    par = parMan.Create();
                    par.LocationID = LocationID;
                    par.UPCID = UPCID;
                }
                if (par.BottleCount != ParBottleCount.Value)
                {
                    par.BottleCount = ParBottleCount.Value;
                    parMan.Save(par);
                }
            }
        }

        public decimal BottleCost
        {
            get
            {
                decimal rc = 0.0M;
                if( this.TotalCost.HasValue && this.TotalQuantity.HasValue && this.TotalQuantity.Value > 0 )
                {
                    rc = Math.Round(TotalCost.Value/TotalQuantity.Value, 2 );
                }
                return rc;
            }
        }
    }
}