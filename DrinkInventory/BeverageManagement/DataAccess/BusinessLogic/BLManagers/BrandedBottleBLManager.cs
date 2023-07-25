using System;
using System.Collections.Generic;
using System.Linq;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data
{
    public class BrandedBottleBLManager : BLOnlyManager<IBLBrandedBottle, BLBrandedBottle>, IBrandedBottleBLManager
    {
        #region IBLManager<IUIAssociatedBottle> Members


        public IBLBrandedBottle Get( System.Guid ID )
        {
            return m_DataItems.Cast<BLBrandedBottle>().Where(B => B.BottleID == ID).FirstOrDefault();
        }

        public bool SaveBrandedBottles( IEnumerable<IBLBrandedBottle> _bottles )
        {
            bool rc = false;
            var man = BLManagerFactory.Get().ManageTags();
            var nozzleMan = BLManagerFactory.Get().ManageStandardNozzles();
            var invMan = BLManagerFactory.Get().ManageInventory();
            foreach (var bottle in _bottles)
            {
                var tag = bottle.Tag;
                if (null != bottle.Nozzle)
                {
                    if (bottle.Nozzle.StandardNozzleID != Guid.Empty &&
                        !bottle.Nozzle.CheckNozzle(tag.UPC.Nozzle))
                    {
                        nozzleMan.Save(bottle.Nozzle);
                        tag.Nozzle = bottle.Nozzle;
                    }
                }
                // TODO: If nozzle is different from Standard Nozzle or the UPC's nozzle, save it as well...associated with the tag.
                if (null == bottle.Tag.CurrentLocation)
                {
                    bottle.Tag.CurrentLocation = bottle.ToLocation;
                }
                man.Save(bottle.Tag);

                invMan.TagInventory(bottle);

                rc = true;
            }
            return rc;
        }

        #endregion
    }
}