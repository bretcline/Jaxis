using System;

namespace Jaxis.Inventory.Data
{
    public interface IPOSTicket : IDataObject<IPOSTicket>
    {
        Guid POSTicketID { get; }
        string CheckNumber { get; set; }
        string Comments { get; set; }
        DateTime TicketDate { get; set; }
        string Establishment { get; set; }
        int GuestCount { get; set; }
        string RawData { get; set; }
        string CustomerTable { get; set; }
        string Server { get; set; }
        string ServerNumber { get; set; }
        double TipAmount { get; set; }
        int TouchCount { get; set; }
    }
}