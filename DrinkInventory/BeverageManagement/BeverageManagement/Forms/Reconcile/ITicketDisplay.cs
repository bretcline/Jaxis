using System.Collections.Generic;
using Jaxis.Interfaces;
using Jaxis.Inventory.Data.IUIDataItems;

namespace BeverageManagement.Forms.Reconcile
{
    public interface ITicketDisplay : IUIPosDisplay
    {
#pragma warning disable 108,114
        PosStatus Status { get; }
#pragma warning restore 108,114
        string DateTime { get; }
        string Comments { get; }
        string CheckNumber { get; }
        string Establishment { get; }
        string GuestCount { get; }
        string CustomerTable { get; }
        List<ITicketItemDisplay> Items { get; }
    }
}
