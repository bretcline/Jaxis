using System.Collections.Generic;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data
{
    public partial class POSTicket : IBLPOSTicket
    {
        private IEnumerable<IBLPOSTicketItem> m_items;

        #region IDataObject<IPOSTicket> Members


        public IEnumerable<IPOSTicket> GetAll( )
        {
            return All( );
        }

        #endregion IDataObject<IPOSTicket> Members

        public IEnumerable<IBLPOSTicketItem> Items
        {
            get
            {
                return m_items ?? (m_items = new List<IBLPOSTicketItem>(
                    POSTicketItem.Find(_i => _i.POSTicketID == POSTicketID)));
            }
        }

        public IBLPOSTicketItem NewItem()
        {
            var item = new POSTicketItem { POSTicketID = POSTicketID };
            ((List<IBLPOSTicketItem>)Items).Add(item);
            return item;
        }

        public string Server { get; set; }
        public string ServerNumber { get; set; }
        public double TipAmount { get; set; }
        public int TouchCount { get; set; }
    }
}
