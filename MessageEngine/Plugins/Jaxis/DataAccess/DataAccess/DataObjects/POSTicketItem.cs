using Jaxis.Interfaces;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data
{
    public partial class POSTicketItem : IBLPOSTicketItem
    {
        #region IDataObject<IPOSTicketItem> Members


        public System.Collections.Generic.IEnumerable<IPOSTicketItem> GetAll( )
        {
            return All( );
        }

        #endregion

        //public int Status { get; set; }

        public PosStatus ItemStatus
        {
            get
            {
                return (PosStatus) Status;
            }
            set { Status = (int) value; }
        }

    }
}
