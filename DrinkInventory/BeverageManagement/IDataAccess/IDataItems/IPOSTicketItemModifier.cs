using System;

namespace Jaxis.Inventory.Data
{
    public interface IPOSTicketItemModifier : IDataObject<IPOSTicketItemModifier>
    {
        Guid POSTicketItemID { get; set; }
        string Name { get; set; }
        decimal? Price { get; set; }
    }
}