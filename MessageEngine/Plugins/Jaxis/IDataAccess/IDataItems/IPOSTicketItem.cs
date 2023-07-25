using System;
using Jaxis.Interfaces;

namespace Jaxis.Inventory.Data
{
    public interface IPOSTicketItem : IDataObject<IPOSTicketItem>
    {
        Guid POSTicketID { get; set; }
        string Comment { get; set; }
        string Description { get; set; }
        decimal? Price { get; set; }
//        bool IsVoid { get; set; }
//        bool Alerted { get; set; }
        int Reconciled { get; set; }
        int Quantity { get; set; }
        int Status { get; }
        PosStatus ItemStatus { get; set; }
    }
}
