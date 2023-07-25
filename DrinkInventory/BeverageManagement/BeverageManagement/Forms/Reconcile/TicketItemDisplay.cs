using Jaxis.Interfaces;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Inventory.Data.IUIDataItems;

namespace BeverageManagement.Forms.Reconcile
{
    public class TicketItemDisplay : ITicketItemDisplay
    {
        public TicketItemDisplay(IBLPOSTicketItem _ticketItem)
        {
            Description = _ticketItem.Description;
            IsVoid = _ticketItem.ItemStatus == PosStatus.Void ? "Yes" : "No";
            Quantity = _ticketItem.Quantity.ToString();
            ReconciledQty = _ticketItem.Reconciled.ToString();
            Status = _ticketItem.ItemStatus;
        }

        public string Description { get; private set; }
        public string IsVoid { get; private set; }
        public string Quantity { get; private set; }
        public string ReconciledQty { get; private set; }
        public PosStatus Status { get; private set; }
    }
}
