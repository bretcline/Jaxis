using System;
using Jaxis.Interfaces;

namespace Jaxis.Inventory.Data.IUIDataItems
{
    public interface IUIPOSTicketItemView
    {
        string CheckNumber { get; }
        string Description { get; }
        int Quantity { get; }
        string StatusTicketItem { get; }
        DateTime TicketDate { get; }
    }
}
