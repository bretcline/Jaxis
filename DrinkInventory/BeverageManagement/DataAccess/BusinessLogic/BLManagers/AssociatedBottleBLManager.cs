using System.Linq;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data
{
    public class AssociatedBottleBLManager : BLOnlyManager<IBLAssociatedBottle, BLBrandedBottle>, IAssociatedBottleBLManager
    {

        #region IBLManager<IUIAssociatedBottle> Members


        public IBLAssociatedBottle Get( System.Guid ID )
        {
            return m_DataItems.Cast<BLBrandedBottle>().Where(B => B.BottleID == ID).FirstOrDefault();
        }

        #endregion
    }
}