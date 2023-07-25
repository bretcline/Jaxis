using System.Collections.Generic;
using System.Linq;
using Jaxis.Interfaces;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Inventory.Data.IUIDataItems;

namespace BeverageManagement.Forms.Reconcile
{
    public class TicketDisplay : ITicketDisplay
    {
        public TicketDisplay(IBLPOSTicket _ticket)
        {
            DateTime = _ticket.TicketDate.ToString("MM/dd/yyyy hh:mm tt");
            Comments = _ticket.Comments;
            CheckNumber = _ticket.CheckNumber;
            Establishment = _ticket.Establishment;
            GuestCount = _ticket.GuestCount.ToString();
            CustomerTable = _ticket.CustomerTable;
            Items = (from t in _ticket.Items select new TicketItemDisplay(t)).Cast<ITicketItemDisplay>().ToList();
            Status = Items.Count > 0 ? Items.Min(_i => _i.Status) : PosStatus.Complete;
        }

        public string DateTime { get; private set; }
        public string Comments { get; private set; }
        public string CheckNumber { get; private set; }
        public string Establishment { get; private set; }
        public string GuestCount { get; private set; }
        public string CustomerTable { get; private set; }
        public List<ITicketItemDisplay> Items { get; private set; }
        public PosStatus Status { get; private set; }
    }
}
