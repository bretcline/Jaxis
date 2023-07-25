using System;
using System.Collections.Generic;

namespace Jaxis.Inventory.Data.IBLDataItems
{
    public interface IBLPOSTicket : IPOSTicket
    {
        string CheckNumber { get; set; }
        string Comments { get; set; }
        DateTime TicketDate { get; set; }
        string Establishment { get; set; }
        int GuestCount { get; set; }
        string CustomerTable { get; set; }
        IEnumerable<IBLPOSTicketItem> Items { get; }
        IBLPOSTicketItem NewItem();
    }
}
