using System;
using System.Linq;
using Jaxis.Inventory.Data.IDataItems;

namespace Jaxis.Inventory.Data.DataAccess.DataManagers
{
    class TicketItemAliasDataManager : DataManager<ITicketItemAlias, TicketItemAlias>, ITicketItemAliasManager, IDataManager
    {
        public IQueryable<ITicketItemAlias> GetAll()
        {
            return TicketItemAlias.All();
        }

        public ITicketItemAlias Get(Guid ID)
        {
            return TicketItemAlias.GetByID(ID);
        }
    }
}
