using System;
using System.Linq;

namespace Jaxis.Inventory.Data
{
    public class TagAlertDataManager : DataManager<ITagAlert, TagAlert>, ITagAlertDataManager, IDataManager
    {
        #region IDataManager<ITag> Members

        public IQueryable<ITagAlert> GetAll( )
        {
            return TagAlert.All( );
        }

        public ITagAlert Get( Guid ID )
        {
            return TagAlert.GetByID( ID );
        }

        #endregion
    }



    public class POSTicketItemModifierManager : DataManager<IPOSTicketItemModifier, POSTicketItemModifier>, IPOSTicketItemModifierManager, IDataManager
    {
        #region IDataManager<IPOSTicketItemModifier> Members


        public IQueryable<IPOSTicketItemModifier> GetAll( )
        {
            return POSTicketItemModifier.All();
        }

        public IPOSTicketItemModifier Get( Guid ID )
        {
            return POSTicketItemModifier.GetByID( ID );
        }

        #endregion
    }

    public class POSTicketItemManager : DataManager<IPOSTicketItem, POSTicketItem>, IPOSTicketItemManager, IDataManager
    {
        #region IDataManager<IPOSTicketItem> Members


        public IQueryable<IPOSTicketItem> GetAll( )
        {
            return POSTicketItem.All( );
        }

        public IPOSTicketItem Get( Guid ID )
        {
            return POSTicketItem.GetByID( ID );
        }

        #endregion
    }

    public class POSTicketManager : DataManager<IPOSTicket, POSTicket>, IPOSTicketManager, IDataManager
    {
        #region IDataManager<IPOSTicket> Members


        public IQueryable<IPOSTicket> GetAll( )
        {
            return POSTicket.All();
        }

        public IPOSTicket Get( Guid ID )
        {
            return POSTicket.GetByID(ID);
        }

        #endregion
    }
}