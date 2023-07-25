


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SubSonic.DataProviders;
using SubSonic.Extensions;
using System.Linq.Expressions;
using SubSonic.Schema;
using System.Collections;
using SubSonic;
using SubSonic.Repository;
using System.ComponentModel;
using System.Data.Common;

namespace Jaxis.Inventory.Data
{
    /// <summary>
    /// A class which represents the BanquetMenus table in the BeverageMonitor Database.
    /// </summary>
    public interface IBanquetMenu
    {
    }
    
    /// <summary>
    /// A class which represents the Banquets table in the BeverageMonitor Database.
    /// </summary>
    public interface IBanquet
    {
    }
    
    /// <summary>
    /// A class which represents the MenuItems table in the BeverageMonitor Database.
    /// </summary>
    public interface IMenuItem
    {
    }
    
    /// <summary>
    /// A class which represents the POSPaymentData table in the BeverageMonitor Database.
    /// </summary>
    public interface IPOSPaymentDatum
    {
    }
    
    /// <summary>
    /// A class which represents the POSTicketItemModifiers table in the BeverageMonitor Database.
    /// </summary>
    public interface IPOSTicketItemModifier
    {
    }
    
    /// <summary>
    /// A class which represents the POSTicketItems table in the BeverageMonitor Database.
    /// </summary>
    public interface IPOSTicketItem
    {
    }
    
    /// <summary>
    /// A class which represents the POSTickets table in the BeverageMonitor Database.
    /// </summary>
    public interface IPOSTicket
    {
    }
    
    /// <summary>
    /// A class which represents the POSTVAData table in the BeverageMonitor Database.
    /// </summary>
    public interface IPOSTVADatum
    {
    }
    


    /// <summary>
    /// A class which represents the BanquetMenus table in the BeverageMonitor Database.
    /// </summary>
    public partial class BanquetMenu: IBanquetMenu, IDataObject<IBanquetMenu>
    {
        public System.Linq.IQueryable<IBanquetMenu> GetAll( )
        {
            return BanquetMenu.All();
        }
    }
    


    /// <summary>
    /// A class which represents the Banquets table in the BeverageMonitor Database.
    /// </summary>
    public partial class Banquet: IBanquet, IDataObject<IBanquet>
    {
        public System.Linq.IQueryable<IBanquet> GetAll( )
        {
            return Banquet.All();
        }
    }
    


    /// <summary>
    /// A class which represents the MenuItems table in the BeverageMonitor Database.
    /// </summary>
    public partial class MenuItem: IMenuItem, IDataObject<IMenuItem>
    {
        public System.Linq.IQueryable<IMenuItem> GetAll( )
        {
            return MenuItem.All();
        }
    }
    


    /// <summary>
    /// A class which represents the POSPaymentData table in the BeverageMonitor Database.
    /// </summary>
    public partial class POSPaymentDatum: IPOSPaymentDatum, IDataObject<IPOSPaymentDatum>
    {
        public System.Linq.IQueryable<IPOSPaymentDatum> GetAll( )
        {
            return POSPaymentDatum.All();
        }
    }
    


    /// <summary>
    /// A class which represents the POSTicketItemModifiers table in the BeverageMonitor Database.
    /// </summary>
    public partial class POSTicketItemModifier: IPOSTicketItemModifier, IDataObject<IPOSTicketItemModifier>
    {
        public System.Linq.IQueryable<IPOSTicketItemModifier> GetAll( )
        {
            return POSTicketItemModifier.All();
        }
    }
    


    /// <summary>
    /// A class which represents the POSTicketItems table in the BeverageMonitor Database.
    /// </summary>
    public partial class POSTicketItem: IPOSTicketItem, IDataObject<IPOSTicketItem>
    {
        public System.Linq.IQueryable<IPOSTicketItem> GetAll( )
        {
            return POSTicketItem.All();
        }
    }
    


    /// <summary>
    /// A class which represents the POSTickets table in the BeverageMonitor Database.
    /// </summary>
    public partial class POSTicket: IPOSTicket, IDataObject<IPOSTicket>
    {
        public System.Linq.IQueryable<IPOSTicket> GetAll( )
        {
            return POSTicket.All();
        }
    }
    


    /// <summary>
    /// A class which represents the POSTVAData table in the BeverageMonitor Database.
    /// </summary>
    public partial class POSTVADatum: IPOSTVADatum, IDataObject<IPOSTVADatum>
    {
        public System.Linq.IQueryable<IPOSTVADatum> GetAll( )
        {
            return POSTVADatum.All();
        }
    }
    
}
