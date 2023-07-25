using System;
using System.Linq;
using Jaxis.Inventory.Data.IDataItems;

namespace Jaxis.Inventory.Data.DataAccess.DataManagers
{
    class TicketItemAliasDataViewManager : DataManager<ITicketItemAliasView, vwTicketItemAlias>, ITicketItemAliasViewManager
    {
        public IQueryable<ITicketItemAliasView> GetAll()
        {
            return vwTicketItemAlias.All();
        }

        public ITicketItemAliasView Get(Guid ID)
        {
            return vwTicketItemAlias.GetByID(ID);
        }
    }
}