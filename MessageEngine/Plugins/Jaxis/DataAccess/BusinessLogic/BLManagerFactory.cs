using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using Jaxis.Inventory.Data.BusinessLogic.BLManagers;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Inventory.Data.IDataItems;
using Jaxis.Util.Log4Net;
using SubSonic.Query;

namespace Jaxis.Inventory.Data
{
    public class BLManagerFactory : IBLManagerFactory
    {
        static private readonly Mutex m_Mutex = new Mutex();
        private static BLManagerFactory m_Factory = null;
        private Dictionary<Type, IBLManager> m_Managers = new Dictionary<Type, IBLManager>( );
        private IBLStandardNozzle m_LiquorNozzle = null;
        private IBLStandardNozzle m_WineNozzle = null;
        private IBLSizeType m_Conversion = null; 

        public IBLUserSession UserSession { get; set; }

        protected BLManagerFactory( )
        {
            try
            {
                m_Managers.Add( typeof( ISecurableItem ), new SecurableItemBLManager( ) );
                m_Managers.Add( typeof( IBLBrandedBottle ), new BrandedBottleBLManager( ) );
                m_Managers.Add( typeof( IBLAssociatedBottle ), new AssociatedBottleBLManager( ) );
                m_Managers.Add( typeof( ITag ), new TagBLManager( ) );
                m_Managers.Add( typeof( ICategory ), new CategoryBLManager( ) );
                m_Managers.Add( typeof( IGroup ), new GroupBLManager( ) );
                m_Managers.Add( typeof( IInventory ), new InventoryBLManager( ) );
                m_Managers.Add(typeof(IBLInventoryItem), new InventoryItemBLManager());
                m_Managers.Add(typeof(IBLInventoryItemView), new InventoryItemViewBLManager());
                m_Managers.Add(typeof(ILocation), new LocationBLManager());
                m_Managers.Add( typeof( IOrganization ), new OrganizationBLManager( ) );
                m_Managers.Add(typeof(IParLevel), new ParLevelBLManager());
                m_Managers.Add(typeof(IPour), new PourBLManager());
                m_Managers.Add(typeof(ISizeType), new SizeTypeBLManager());
                m_Managers.Add( typeof( IUPCItem ), new UPCItemBLManager( ) );
                m_Managers.Add( typeof( IUser ), new UserBLManager( ) );
                m_Managers.Add( typeof( IUserSession ), new UserSessionBLManager( ) );
                m_Managers.Add( typeof( IActivityLog ), new ActivityLogBLManager( ) );
                m_Managers.Add( typeof( ITagActivity ), new TagActivityBLManager( ) );
                m_Managers.Add( typeof( ITagAlert ), new TagAlertBLManager( ) );
                m_Managers.Add( typeof( IReport ), new ReportBLManager( ) );
                m_Managers.Add(typeof(IManufacturerView), new ManufacturerViewBlManager());
                m_Managers.Add(typeof(IManufacturer), new ManufacturerBlManager());
                m_Managers.Add(typeof(IDevice), new DeviceBLManager());

                m_Managers.Add( typeof( IStandardNozzle ), new StandardNozzleBLManager( ) );
                m_Managers.Add( typeof( IStandardPour ), new StandardPourBLManager( ) );
                m_Managers.Add( typeof( IStandardPrice ), new StandardPriceBLManager( ) );
                m_Managers.Add(typeof(IQuality), new QualityBLManager());

                //m_Managers.Add( typeof( ICart ), new CartBLManager( ) );
                //m_Managers.Add( typeof( IEvent ), new EventBLManager( ) );

                m_Managers.Add(typeof(IPOSTicket), new POSTicketBLManager());
                m_Managers.Add(typeof(IRecipe), new RecipeBLManager());
                m_Managers.Add(typeof(ITicketItemAlias), new TicketItemAliasBLManager());
                m_Managers.Add(typeof(ITicketItemAliasView), new TicketItemAliasViewBLManager());
                m_Managers.Add(typeof(IIngredient), new IngredientBLManager());
                m_Managers.Add(typeof(IPOSTicketItem), new POSTicketItemBLManager());
                m_Managers.Add(typeof(IPOSTicketItemView), new POSTicketItemViewBLManager());
                m_Managers.Add(typeof(IPosPour), new PosPourBLManager());
            }
            catch
            {
                throw;
            }
        }

        public static IBLManagerFactory Get()
        {
            IBLManagerFactory rc = null;
            try
            {
                m_Mutex.WaitOne( );
                rc = m_Factory ?? ( m_Factory = new BLManagerFactory( ) );
            }
            catch (Exception)
            {
               
                throw;
            }
            finally
            {
                m_Mutex.ReleaseMutex( );
            }
            return rc;
        }

        #region IBLManagerFactory Members

        public IBrandedBottleBLManager ManageBrandedBottles()
        {
            return m_Managers[typeof( IBLBrandedBottle )] as IBrandedBottleBLManager;
        }

        public IAssociatedBottleBLManager ManageAssociatedBottles()
        {
            return m_Managers[typeof( IBLAssociatedBottle )] as IAssociatedBottleBLManager;
        }

        public ITagBLManager ManageTags( )
        {
            return m_Managers[typeof( ITag )] as ITagBLManager;
        }

        public ICategoryBLManager ManageCategories( )
        {
            return m_Managers[typeof( ICategory )] as ICategoryBLManager;
        }

        public IGroupBLManager ManageGroups( )
        {
            return m_Managers[typeof( IGroup )] as IGroupBLManager;
        }

        public IInventoryBLManager ManageInventory( )
        {
            return m_Managers[typeof( IInventory )] as IInventoryBLManager;
        }


        public IInventoryItemBLManager ManageInventoryItems()
        {
            return m_Managers[typeof(IBLInventoryItem)] as IInventoryItemBLManager;
        }

        public IInventoryItemViewBLManager ManageInventoryItemViews()
        {
            return m_Managers[typeof(IBLInventoryItemView)] as IInventoryItemViewBLManager;
        }

        public ILocationBLManager ManageLocations( )
        {
            return m_Managers[typeof( ILocation )] as ILocationBLManager;
        }

        public IOrganizationBLManager ManageOrganizations( )
        {
            return m_Managers[typeof( IOrganization )] as IOrganizationBLManager;
        }

        public IParLevelBLManager ManageParLevels()
        {
            return m_Managers[typeof(IParLevel)] as IParLevelBLManager;
        }

        public IPourBLManager ManagePours()
        {
            return m_Managers[typeof(IPour)] as IPourBLManager;
        }

        public IPosPourBLManager ManagePosPours()
        {
            return m_Managers[typeof(IPosPour)] as IPosPourBLManager;
        }

        public ISizeTypeBLManager ManageSizeTypes( )
        {
            return m_Managers[typeof( ISizeType )] as ISizeTypeBLManager;
        }

        public IUPCItemBLManager ManageUPCs( )
        {
            return m_Managers[typeof( IUPCItem )] as IUPCItemBLManager;
        }

        public IUserBLManager ManageUsers( )
        {
            return m_Managers[typeof( IUser )] as IUserBLManager;
        }

        public IUserSessionBLManager ManageUserSessions( )
        {
            return m_Managers[typeof( IUserSession )] as IUserSessionBLManager;
        }

        public IActivityLogBLManager ManageActivityLog( )
        {
            return m_Managers[typeof( IActivityLog )] as IActivityLogBLManager;
        }

        public ITagActivityBLManager ManageTagActivity( )
        {
            return m_Managers[typeof( ITagActivity )] as ITagActivityBLManager;
        }

        public ITagAlertBLManager ManageTagAlert( )
        {
            return m_Managers[typeof( ITagAlert )] as ITagAlertBLManager;
        }

        public IReportBLManager ManageReport( )
        {
            return m_Managers[typeof( IReport )] as IReportBLManager;
        }

        public IManufacturerBLManager ManageManufacturers()
        {
            return m_Managers[typeof(IManufacturer)] as IManufacturerBLManager;
        }

        public IManufacturerViewBLManager ManageManufacturerViews()
        {
            return m_Managers[typeof(IManufacturerView)] as IManufacturerViewBLManager;
        }

        public IDeviceBLManager ManageDevices()
        {
            return m_Managers[typeof (IDevice)] as IDeviceBLManager;
        }

        #endregion


        //public ICartBLManager ManageCarts( )
        //{
        //    return m_Managers[typeof( ICart )] as ICartBLManager;
        //}

        //public IEventBLManager ManageEvents( )
        //{
        //    return m_Managers[typeof( IEvent )] as IEventBLManager;
        //}


        #region IBLManagerFactory Members

        public string GetAdminValue(string _propertyName)
        {
            CodingHorror horror = new CodingHorror("SELECT PropertyValue FROM dbo.AdministrativeValues WHERE PropertyName = @Name", _propertyName);
            return horror.ExecuteScalar<string>();
        }
        public void SetAdminValue(string _propertyName, string _propertyValue)
        {
            CodingHorror horror = new CodingHorror("UPDATE dbo.AdministrativeValues SET PropertyValue = @Value WHERE PropertyName = @Name", _propertyValue, _propertyName);
            horror.Execute();
        }
        
        public static double ConvertToMLFromSizeType(double _amount, IBLSizeType _sizeType)
        {
            double rc = _amount;
            try
            {
                rc = _amount / _sizeType.ConversionMultiplier;
            }
            catch
            {
            }
            return rc;
        }

        public static double ConvertToMLFromSizeType(double _amount, Guid _sizeTypeID)
        {
            double rc = _amount;
            try
            {
                var sizeType = BLManagerFactory.Get().ManageSizeTypes().Get(_sizeTypeID);
                rc = _amount / sizeType.ConversionMultiplier;
            }
            catch
            {
            }
            return rc;
        }

        public static double ConvertFromMLFromSizeType(double _amount, IBLSizeType _sizeType)
        {
            double rc = _amount;
            try
            {
                rc = _amount * _sizeType.ConversionMultiplier;
            }
            catch
            {
            }
            return rc;
        }

        public double ConvertPourToUnits(double _value)
        {
            return _value * GetDefaultSizeType().ConversionMultiplier;
        }

        public double ConvertPourFromUnits(double _value)
        {
            return _value / GetDefaultSizeType().ConversionMultiplier;
        }

        public IBLSizeType GetDefaultSizeType( )
        {
            if (null == m_Conversion)
            {
                string id = GetAdminValue("Pour Conversion");
                m_Conversion = BLManagerFactory.Get().ManageSizeTypes().Get(new Guid(id));
            }
            return m_Conversion;
        }

        public IBLStandardNozzle GetDefaultNozzle(DefaultNozzleType _type = DefaultNozzleType.Liquor)
        {
            IBLStandardNozzle rc = m_LiquorNozzle;
            if (_type == DefaultNozzleType.Liquor)
            {
                if (null == m_LiquorNozzle)
                {
                    m_LiquorNozzle =
                        DataManagerFactory.Get().Manage<IStandardNozzle>().Get(Guid.Empty) as IBLStandardNozzle;

                    if (null == m_LiquorNozzle)
                    {
                        var nozzle = DataManagerFactory.Get().Manage<IStandardNozzle>().Create();
                        nozzle.StandardNozzleID = Guid.Empty;
                        nozzle.Shape = 1;
                        nozzle.Width = 5;
                        nozzle.Length = 5;
                        DataManagerFactory.Get().Manage<IStandardNozzle>().Save(nozzle);
                        m_LiquorNozzle = nozzle as IBLStandardNozzle;
                    }
                }
                rc = m_LiquorNozzle;
            }
            else if (_type == DefaultNozzleType.Wine)
            {
                if (null == m_WineNozzle)
                {
                    var WineGuid = new Guid("00000000-0000-0000-0000-000000000001");
                    m_WineNozzle =
                        DataManagerFactory.Get().Manage<IStandardNozzle>().Get(WineGuid) as IBLStandardNozzle;
                    if (null == m_WineNozzle )
                    {
                        var nozzle = DataManagerFactory.Get().Manage<IStandardNozzle>().Create();
                        nozzle.StandardNozzleID = WineGuid;
                        nozzle.Shape = 1;
                        nozzle.Width = 17.9;
                        nozzle.Length = 17.9;
                        DataManagerFactory.Get().Manage<IStandardNozzle>().Save(nozzle);
                        m_WineNozzle = nozzle as IBLStandardNozzle;
                    }
                }
                rc = m_WineNozzle;
            }
            return rc;
        }

        public IStandardNozzleBLManager ManageStandardNozzles( )
        {
            return m_Managers[typeof( IStandardNozzle )] as IStandardNozzleBLManager;
        }

        public IStandardPourBLManager ManageStandardPours( )
        {
            var manager = m_Managers[typeof (IStandardPour)];
            return manager as IStandardPourBLManager;
        }

        public IStandardPriceBLManager ManageStandardPrices( )
        {
            return m_Managers[typeof( IStandardPrice )] as IStandardPriceBLManager;
        }

        public IPOSTicketBLManager ManagePOSTickets()
        {
            return m_Managers[typeof(IPOSTicket)] as IPOSTicketBLManager;
        }

        public IRecipeBLManager ManageRecipes()
        {
            return m_Managers[typeof(IRecipe)] as IRecipeBLManager;
        }

        public ITicketItemAliasBLManager ManageTicketItemAliases()
        {
            return m_Managers[typeof(ITicketItemAlias)] as ITicketItemAliasBLManager;
        }

        public ITicketItemAliasViewBLManager ManageTicketItemAliasViews()
        {
            return m_Managers[typeof(ITicketItemAliasView)] as ITicketItemAliasViewBLManager;
        }

        public IIngredientBLManager ManageIngredients()
        {
            return m_Managers[typeof(IIngredient)] as IIngredientBLManager;
        }

        public IPOSTicketItemBLManager ManagePOSTicketItems()
        {
            return m_Managers[typeof(IPOSTicketItem)] as IPOSTicketItemBLManager;
        }

        public IPOSTicketItemViewBLManager ManagePOSTicketItemViews()
        {
            return m_Managers[typeof(IPOSTicketItemView)] as IPOSTicketItemViewBLManager;
        }

        public IQualityBLManager ManageQuality()
        {
            return m_Managers[typeof(IQuality)] as IQualityBLManager;
        }

        #endregion
    }
}