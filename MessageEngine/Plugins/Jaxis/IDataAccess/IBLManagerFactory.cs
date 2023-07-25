using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data
{

    public enum DefaultNozzleType
    {
        Liquor,
        Wine,
    }

    public interface IBLManagerFactory
    {
        IBLUserSession UserSession { get; set; }

        IBrandedBottleBLManager ManageBrandedBottles( );
        IAssociatedBottleBLManager ManageAssociatedBottles( );
        ITagBLManager ManageTags();
        IDeviceBLManager ManageDevices();
        ICategoryBLManager ManageCategories();
        IGroupBLManager ManageGroups( );
        IInventoryBLManager ManageInventory( );
        IInventoryItemBLManager ManageInventoryItems();
        IInventoryItemViewBLManager ManageInventoryItemViews();
        ILocationBLManager ManageLocations();
        IOrganizationBLManager ManageOrganizations( );
        IParLevelBLManager ManageParLevels();
        IPourBLManager ManagePours();
        IPosPourBLManager ManagePosPours();
        ISizeTypeBLManager ManageSizeTypes( );
        IUPCItemBLManager ManageUPCs( );
        IUserBLManager ManageUsers( );
        IUserSessionBLManager ManageUserSessions( );
        IActivityLogBLManager ManageActivityLog( );
        ITagActivityBLManager ManageTagActivity( );
        ITagAlertBLManager ManageTagAlert( );
        IReportBLManager ManageReport( );
        IManufacturerViewBLManager ManageManufacturerViews();
        IManufacturerBLManager ManageManufacturers();

        IStandardNozzleBLManager ManageStandardNozzles( );
        IStandardPourBLManager ManageStandardPours( );
        IStandardPriceBLManager ManageStandardPrices( );

        IPOSTicketBLManager ManagePOSTickets();
        IRecipeBLManager ManageRecipes();
        ITicketItemAliasBLManager ManageTicketItemAliases();
        ITicketItemAliasViewBLManager ManageTicketItemAliasViews();
        IIngredientBLManager ManageIngredients();
        IPOSTicketItemBLManager ManagePOSTicketItems();
        IPOSTicketItemViewBLManager ManagePOSTicketItemViews();

        //ICartBLManager ManageCarts( );
        //IEventBLManager ManageEvents( );

        string GetAdminValue(string _propertyName);
        void SetAdminValue(string _propertyName, string _propertyValue);
        IBLStandardNozzle GetDefaultNozzle(DefaultNozzleType _type = DefaultNozzleType.Liquor);
        double ConvertPourToUnits(double _value);
        double ConvertPourFromUnits(double _value);
        IBLSizeType GetDefaultSizeType();
        IQualityBLManager ManageQuality();
    }
}