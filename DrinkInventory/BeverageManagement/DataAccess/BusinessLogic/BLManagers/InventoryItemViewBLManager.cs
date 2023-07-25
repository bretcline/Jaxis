using System.Linq;

namespace Jaxis.Inventory.Data
{
    public class InventoryItemViewBLManager : BLManager<IInventoryItemView, IBLInventoryItemView>, IInventoryItemViewBLManager
    {
        public override bool Save(IBLInventoryItemView item)
        {
            bool rc = false;
            var concrete = item as vwInventoryItem;
            if (concrete.ParBottleCount.HasValue && 0 < concrete.ParBottleCount.Value)
            {
                var db = new BeverageMonitorDB();
                var proc = db.procUpdateParLevel(item.UPCID, concrete.LocationID, concrete.ParBottleCount.Value);
                proc.Execute();
                rc = true;

                //var parMan = BLManagerFactory.Get().ManageParLevels();
                //var par = parMan.GetAll().Where(p => p.UPCID == concrete.UPCID && p.LocationID == concrete.LocationID).FirstOrDefault();
                //if (null == par)
                //{
                //    par = parMan.Create();
                //    par.LocationID = concrete.LocationID;
                //    par.UPCID = concrete.UPCID;
                //}
                //if (par.BottleCount != concrete.ParBottleCount.Value)
                //{
                //    par.BottleCount = concrete.ParBottleCount.Value;
                //    ((ParLevel)par).Save();
                //    rc = parMan.Save(par);
                //}
            }
            return rc;
        }
    }
}