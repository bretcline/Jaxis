using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.Data.POCO
{
    public partial class Pour
    {
        public IPOSTicketItem POSTicketItem
        {
            get
            {
                return NavPOSTicketItem;
            }
            set
            {
                NavPOSTicketItem = ( POSTicketItem ) value;
                if ( value != null )
                {
                    POSTicketItemId = value.POSTicketItemId;
                }
            }
        }

    }
}
