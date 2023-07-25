using System;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.DrinkInventory.BeverageManagement.UnitTest.Fakes
{
    class FakeManagerFactory : IBLManagerFactory
    {
        private static FakeManagerFactory m_instance;

        private FakeManagerFactory()
        {
        }

        public IBLUserSession UserSession
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public IBrandedBottleBLManager ManageBrandedBottles()
        {
            throw new NotImplementedException();
        }

        public IAssociatedBottleBLManager ManageAssociatedBottles()
        {
            throw new NotImplementedException();
        }

        public ITagBLManager ManageTags()
        {
            throw new NotImplementedException();
        }

        public IDeviceBLManager ManageDevices()
        {
            throw new NotImplementedException();
        }

        private ICategoryBLManager m_categoryManager;
        public ICategoryBLManager ManageCategories()
        {
            return m_categoryManager ?? (m_categoryManager = new FakeCategoryManager());
        }

        public IGroupBLManager ManageGroups()
        {
            throw new NotImplementedException();
        }

        public IInventoryBLManager ManageInventory()
        {
            throw new NotImplementedException();
        }

        public IInventoryItemBLManager ManageInventoryItems()
        {
            throw new NotImplementedException();
        }

        public ILocationBLManager ManageLocations()
        {
            throw new NotImplementedException();
        }

        public IOrganizationBLManager ManageOrganizations()
        {
            throw new NotImplementedException();
        }

        private IPourBLManager m_pourManager;
        public IPourBLManager ManagePours()
        {
            return m_pourManager ?? (m_pourManager = new FakePourManager());
        }

        private IPosPourBLManager m_posPourManager;
        public IPosPourBLManager ManagePosPours()
        {
            return m_posPourManager ?? (m_posPourManager = new FakePosPourManager());
        }

        public ISizeTypeBLManager ManageSizeTypes()
        {
            throw new NotImplementedException();
        }

        private IUPCItemBLManager m_upcManager;
        public IUPCItemBLManager ManageUPCs()
        {
            return m_upcManager ?? (m_upcManager = new FakeUpcManager());
        }

        public IUserBLManager ManageUsers()
        {
            throw new NotImplementedException();
        }

        public IUserSessionBLManager ManageUserSessions()
        {
            throw new NotImplementedException();
        }

        public IActivityLogBLManager ManageActivityLog()
        {
            throw new NotImplementedException();
        }

        public ITagActivityBLManager ManageTagActivity()
        {
            throw new NotImplementedException();
        }

        public ITagAlertBLManager ManageTagAlert()
        {
            throw new NotImplementedException();
        }

        public IReportBLManager ManageReport()
        {
            throw new NotImplementedException();
        }

        public IManufacturerBLManager ManageManufacturers()
        {
            throw new NotImplementedException();
        }

        public IManufacturerViewBLManager ManageManufacturerViews()
        {
            throw new NotImplementedException();
        }

        public IStandardNozzleBLManager ManageStandardNozzles()
        {
            throw new NotImplementedException();
        }

        public IStandardPourBLManager ManageStandardPours()
        {
            throw new NotImplementedException();
        }

        public IStandardPriceBLManager ManageStandardPrices()
        {
            throw new NotImplementedException();
        }

        private IPOSTicketBLManager m_posTicketManager;
        public IPOSTicketBLManager ManagePOSTickets()
        {
            return m_posTicketManager ?? (m_posTicketManager = new FakeTicketManager());
        }

        private IRecipeBLManager m_recipeManager;
        public IRecipeBLManager ManageRecipes()
        {
            return m_recipeManager ?? (m_recipeManager = new FakeRecipeManager());
        }

        private ITicketItemAliasBLManager m_ticketItemAliasManager;
        public ITicketItemAliasBLManager ManageTicketItemAliases()
        {
            return m_ticketItemAliasManager ?? (m_ticketItemAliasManager = new FakeTicketItemAliasManager());
        }

        public IIngredientBLManager ManageIngredients()
        {
            throw new NotImplementedException();
        }

        private IPOSTicketItemBLManager m_ticketItemManager;
        public IPOSTicketItemBLManager ManagePOSTicketItems()
        {
            return m_ticketItemManager ?? (m_ticketItemManager = new FakeTicketItemManager());
        }

        public string GetAdminValue(string _propertyName)
        {
            throw new NotImplementedException();
        }

        public void SetAdminValue(string _propertyName, string _propertyValue)
        {
            throw new NotImplementedException();
        }

        public IBLStandardNozzle GetDefaultNozzle()
        {
            throw new NotImplementedException();
        }

        public double ConvertPourToUnits(double _value)
        {
            throw new NotImplementedException();
        }

        public double ConvertPourFromUnits(double _value)
        {
            throw new NotImplementedException();
        }

        public IBLSizeType GetDefaultSizeType()
        {
            throw new NotImplementedException();
        }

        public static IBLManagerFactory Get()
        {
            return m_instance ?? (m_instance = new FakeManagerFactory());
        }

        public static IBLManagerFactory Recreate()
        {
            return m_instance = new FakeManagerFactory();
        }

        IBLUserSession IBLManagerFactory.UserSession
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        IBrandedBottleBLManager IBLManagerFactory.ManageBrandedBottles()
        {
            throw new NotImplementedException();
        }

        IAssociatedBottleBLManager IBLManagerFactory.ManageAssociatedBottles()
        {
            throw new NotImplementedException();
        }

        ITagBLManager IBLManagerFactory.ManageTags()
        {
            throw new NotImplementedException();
        }

        IDeviceBLManager IBLManagerFactory.ManageDevices()
        {
            throw new NotImplementedException();
        }

        ICategoryBLManager IBLManagerFactory.ManageCategories()
        {
            throw new NotImplementedException();
        }

        IGroupBLManager IBLManagerFactory.ManageGroups()
        {
            throw new NotImplementedException();
        }

        IInventoryBLManager IBLManagerFactory.ManageInventory()
        {
            throw new NotImplementedException();
        }

        IInventoryItemBLManager IBLManagerFactory.ManageInventoryItems()
        {
            throw new NotImplementedException();
        }

        IInventoryItemViewBLManager IBLManagerFactory.ManageInventoryItemViews()
        {
            throw new NotImplementedException();
        }

        ILocationBLManager IBLManagerFactory.ManageLocations()
        {
            throw new NotImplementedException();
        }

        IOrganizationBLManager IBLManagerFactory.ManageOrganizations()
        {
            throw new NotImplementedException();
        }

        IPourBLManager IBLManagerFactory.ManagePours()
        {
            throw new NotImplementedException();
        }

        IPosPourBLManager IBLManagerFactory.ManagePosPours()
        {
            throw new NotImplementedException();
        }

        ISizeTypeBLManager IBLManagerFactory.ManageSizeTypes()
        {
            throw new NotImplementedException();
        }

        IUPCItemBLManager IBLManagerFactory.ManageUPCs()
        {
            throw new NotImplementedException();
        }

        IUserBLManager IBLManagerFactory.ManageUsers()
        {
            throw new NotImplementedException();
        }

        IUserSessionBLManager IBLManagerFactory.ManageUserSessions()
        {
            throw new NotImplementedException();
        }

        IActivityLogBLManager IBLManagerFactory.ManageActivityLog()
        {
            throw new NotImplementedException();
        }

        ITagActivityBLManager IBLManagerFactory.ManageTagActivity()
        {
            throw new NotImplementedException();
        }

        ITagAlertBLManager IBLManagerFactory.ManageTagAlert()
        {
            throw new NotImplementedException();
        }

        IReportBLManager IBLManagerFactory.ManageReport()
        {
            throw new NotImplementedException();
        }

        IManufacturerViewBLManager IBLManagerFactory.ManageManufacturerViews()
        {
            throw new NotImplementedException();
        }

        IManufacturerBLManager IBLManagerFactory.ManageManufacturers()
        {
            throw new NotImplementedException();
        }

        IStandardNozzleBLManager IBLManagerFactory.ManageStandardNozzles()
        {
            throw new NotImplementedException();
        }

        IStandardPourBLManager IBLManagerFactory.ManageStandardPours()
        {
            throw new NotImplementedException();
        }

        IStandardPriceBLManager IBLManagerFactory.ManageStandardPrices()
        {
            throw new NotImplementedException();
        }

        IPOSTicketBLManager IBLManagerFactory.ManagePOSTickets()
        {
            throw new NotImplementedException();
        }

        IRecipeBLManager IBLManagerFactory.ManageRecipes()
        {
            throw new NotImplementedException();
        }

        ITicketItemAliasBLManager IBLManagerFactory.ManageTicketItemAliases()
        {
            throw new NotImplementedException();
        }

        IIngredientBLManager IBLManagerFactory.ManageIngredients()
        {
            throw new NotImplementedException();
        }

        IPOSTicketItemBLManager IBLManagerFactory.ManagePOSTicketItems()
        {
            throw new NotImplementedException();
        }

        string IBLManagerFactory.GetAdminValue(string _propertyName)
        {
            throw new NotImplementedException();
        }

        void IBLManagerFactory.SetAdminValue(string _propertyName, string _propertyValue)
        {
            throw new NotImplementedException();
        }

        IBLStandardNozzle IBLManagerFactory.GetDefaultNozzle()
        {
            throw new NotImplementedException();
        }

        double IBLManagerFactory.ConvertPourToUnits(double _value)
        {
            throw new NotImplementedException();
        }

        double IBLManagerFactory.ConvertPourFromUnits(double _value)
        {
            throw new NotImplementedException();
        }

        IBLSizeType IBLManagerFactory.GetDefaultSizeType()
        {
            throw new NotImplementedException();
        }


        public IParLevelBLManager ManageParLevels()
        {
            throw new NotImplementedException();
        }


        public IQualityBLManager ManageQuality()
        {
            throw new NotImplementedException();
        }
    }
}
