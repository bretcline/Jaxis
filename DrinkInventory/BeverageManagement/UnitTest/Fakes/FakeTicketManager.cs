using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.DrinkInventory.BeverageManagement.UnitTest.Fakes
{
    class FakeTicketManager : FakeManager<IBLPOSTicket,IPOSTicket,POSTicket>, IPOSTicketBLManager
    {
    }
}
