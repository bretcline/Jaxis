using System;
using System.Collections.Generic;
using Jaxis.Inventory.Data.DataAccess;
using Jaxis.Inventory.Data.DataAccess.DataManagers;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Inventory.Data.IDataItems;
using Jaxis.Inventory.Data.Reports;

namespace Jaxis.Inventory.Data
{

    public class MissingManagerException : Exception
    {
        public MissingManagerException( string _error )
            : base( _error )
        {
        }
    }


    public class DataManagerFactory : IDataManagerFactory
    {
        private static DataManagerFactory m_Factory;
        private readonly Dictionary<Type, IDataManager> m_Managers = new Dictionary<Type, IDataManager>( );

        public IUserSession UserSession { get; set; }

        protected DataManagerFactory( )
        {
            m_Managers.Add(typeof(IActivityLog), new ActivityLogDataManager());
            //Administrative Values
            m_Managers.Add(typeof(ICategory), new CategoryDataManager());
            //Database Version
            //Device Alerts
            m_Managers.Add(typeof(IDevice), new DeviceDataManager());
            m_Managers.Add(typeof(IGroup), new GroupDataManager());
            //GroupXSecurableItems
            //GroupXReport
            m_Managers.Add(typeof(IIngredient), new IngredientDataManager());
            m_Managers.Add(typeof(IInventory), new InventoryDataManager());
            m_Managers.Add(typeof(ILocation), new LocationDataManager());
            m_Managers.Add(typeof(IManufacturer), new ManufacturerDataManager());
            m_Managers.Add(typeof(IManufacturerView), new ManufacturerViewDataManager());
            //Moves
            m_Managers.Add(typeof(IOrganization), new OrganizationDataManager());

            m_Managers.Add(typeof(IParLevel), new ParLevelDataManager());
            m_Managers.Add(typeof(IPOSTicketItemModifier), new POSTicketItemModifierManager());
            m_Managers.Add(typeof(IPOSTicketItem), new POSTicketItemManager());
            m_Managers.Add(typeof(IPOSTicketItemView), new POSTicketItemViewManager());
            m_Managers.Add(typeof(IPOSTicket), new POSTicketManager());
            m_Managers.Add(typeof(IPour), new PourDataManager());
            m_Managers.Add(typeof(IQuality), new QualityManager());
            m_Managers.Add(typeof(IRecipe), new RecipeDataManager());
            //Report Parameters
            m_Managers.Add(typeof(IReport), new ReportDataManager());
            m_Managers.Add(typeof(ISecurableItem), new SecurableItemDataManager());
            m_Managers.Add(typeof(ISizeType), new SizeTypeDataManager());
            m_Managers.Add(typeof(IStandardNozzle), new StandardNozzleDataManager());
            m_Managers.Add(typeof(IStandardPour), new StandardPourDataManager());
            m_Managers.Add(typeof(IStandardPrice), new StandardPriceDataManager());
            m_Managers.Add(typeof(ITagActivity), new TagActivityDataManager());
            m_Managers.Add(typeof(ITagAlert), new TagAlertDataManager());
            m_Managers.Add(typeof(ITagMove), new TagMoveDataManager());
            m_Managers.Add(typeof(ITag), new TagDataManager());
            m_Managers.Add(typeof(ITicketItemAlias), new TicketItemAliasDataManager());
            m_Managers.Add(typeof(ITicketItemAliasView), new TicketItemAliasDataViewManager());
            m_Managers.Add(typeof(IUPCItem), new UPCItemDataManager());
            m_Managers.Add(typeof(IUser), new UserDataManager( ) );
            m_Managers.Add(typeof(IUserSession), new UserSessionDataManager( ) );
            //UserXGroup
            //UserXOrganization

            m_Managers.Add(typeof(IRPourTotals), new PourReportDataManager());
            m_Managers.Add(typeof(IRTagPourTotals), new TagPourReportDataManager());
            m_Managers.Add(typeof(IUPCItemView), new UPCItemDataViewManager());
            m_Managers.Add(typeof(IPosPour), new PosPourDataManager());
            m_Managers.Add(typeof(IInventoryItemView), new InventoryItemViewDataManager());

            //m_Managers.Add( typeof( ICart ), new CartDataManager( ) );
            //m_Managers.Add( typeof( IEvent ), new EventDataManager( ) );
        }

        public static IDataManagerFactory Get()
        {
            return m_Factory ?? ( m_Factory = new DataManagerFactory( ) );
        }

        #region IDataManagerFactory Members

        public IDataManager<T> Manage<T> ( )
        {
            return m_Managers[ typeof ( T ) ] as IDataManager< T >;
        }

        #endregion
    }
}