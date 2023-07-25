using System.Collections.Generic;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Inventory.Data.IDataItems;

namespace Jaxis.Inventory.Data
{
    public partial class vwTicketItemAlias : IUITicketItemAliasView, ITicketItemAliasDisplayView
    {
        public IEnumerable<ITicketItemAliasView> GetAll()
        {
            return All();
        }

        public bool AutoInventory
        {
            get { return this.PosUPC.HasValue; }
        }

        public IBLTicketItemAlias GetTicketItemAlias()
        {
            var rc = DataManagerFactory.Get().Manage<ITicketItemAlias>().Get(this.TicketItemAliasID);
            if (null == rc)
            {
                rc = DataManagerFactory.Get().Manage<ITicketItemAlias>().Create();
            }

            rc.Description = this.Description;
            rc.PosUPC = this.PosUPC;
            rc.Price = this.Price;
            rc.RecipeID = this.RecipeID;
            rc.TicketItemAliasID = this.ObjectID;

            return rc as IBLTicketItemAlias;
        }
    }
}