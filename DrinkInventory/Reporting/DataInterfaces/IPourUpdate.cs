using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jaxis.DrinkInventory.Reporting.DataInterfaces
{
    public interface IPourUpdate
    {
        Guid PourId { get; set; }
        Guid? POSTicketItemId { get; set; }
        int? Status { get; set; }
    }
}
