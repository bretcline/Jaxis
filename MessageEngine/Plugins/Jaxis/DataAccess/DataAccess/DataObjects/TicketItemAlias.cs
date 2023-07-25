using System;
using System.Linq;
using System.Collections.Generic;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Inventory.Data.IDataItems;

namespace Jaxis.Inventory.Data
{
    public partial class TicketItemAlias : IBLTicketItemAlias, IUITicketItemAlias
    {
        public IEnumerable<ITicketItemAlias> GetAll()
        {
            return All();
        }

        public bool AutoInventory
        {
            get { return this.PosUPC.HasValue; }
        }
    }
}
