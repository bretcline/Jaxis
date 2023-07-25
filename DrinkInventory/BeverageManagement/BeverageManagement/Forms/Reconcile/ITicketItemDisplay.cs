using Jaxis.Interfaces;
using Jaxis.Inventory.Data.IUIDataItems;

namespace BeverageManagement.Forms.Reconcile
{
    public interface ITicketItemDisplay : IUIPosDisplay
    {
#pragma warning disable 108,114
        PosStatus Status { get; }
#pragma warning restore 108,114
        string Description { get; }
        string IsVoid { get; }
        string Quantity { get; }
        string ReconciledQty { get; }
    }
}
