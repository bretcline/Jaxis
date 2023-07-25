using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Inventory.Data.IDataItems;

namespace Jaxis.DrinkInventory.BeverageManagement.UnitTest.Fakes
{
    class FakeTicketItemAliasManager : FakeManager<IBLTicketItemAlias,ITicketItemAlias,TicketItemAlias>, ITicketItemAliasBLManager
    {
    }
}
