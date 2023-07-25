using System;

namespace Jaxis.Inventory.Data.IBLDataItems
{
    public interface IBLPOSTicketItemView : IPOSTicketItemView
    {
        Guid POSTicketItemID { get; set; }
        Guid POSTicketID { get; set; }
        DateTime TicketDate { get; set; }
        string CheckNumber { get; set; }
        string Establishment { get; set; }
        string Description { get; set; }
        int Quantity { get; set; }
        int Status { get; set; }
        string StatusTicketItem { get; }
    }
}