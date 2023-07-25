using System.Collections.Generic;
using System.Linq;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.DrinkInventory.BeverageManagement.UnitTest.Fakes
{
    class FakeTicketItemManager : FakeManager<IBLPOSTicketItem, IPOSTicketItem, POSTicketItem>, IPOSTicketItemBLManager
    {
        public IEnumerable<string> GetUniqueDescriptions()
        {
            var descriptions = (from item in GetAll() select item.Description).Distinct();
            return descriptions;
        }
    }
}
