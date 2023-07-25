using System.Collections.Generic;
using System.Linq;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Inventory.Data.IDataItems;

namespace Jaxis.Inventory.Data.BusinessLogic.BLManagers
{
// ReSharper disable InconsistentNaming
    public class POSTicketItemBLManager : BLManager<IPOSTicketItem, IBLPOSTicketItem>, IPOSTicketItemBLManager
// ReSharper restore InconsistentNaming
    {
        public IEnumerable<string> GetUniqueDescriptions()
        {
            var descriptions = (from item in GetAll() select item.Description).Distinct();
            return descriptions;
        }
    }
}
