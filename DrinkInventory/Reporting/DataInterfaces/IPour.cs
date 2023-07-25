using System;

namespace Jaxis.DrinkInventory.Reporting.DataInterfaces
{
    public partial interface IPour : IDomainObject
    {
        IPOSTicketItem POSTicketItem { get; set; }
    }
}
