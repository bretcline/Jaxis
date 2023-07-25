using System;
using System.Collections.Generic;
using System.Linq;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Inventory.Data.IUIDataItems;

namespace Jaxis.Inventory.Data
{
    public enum StandardPourType
    {
        Liquor = 1,
        Beer = 2,
        Wine = 3,
    }

    public interface IBLManager : IDataManager
    {
    }

    public interface IBLManager<T> : IBLManager
    {
        T Create( );
        bool Save( T item  );
        bool Delete( T item  );

        IEnumerable<T> GetAll( );
        T Get( Guid ID );
    }


    public interface ISecurableItemBLManager : IBLManager<IBLSecurableItem> { }
    public interface IBrandedBottleBLManager : IBLManager<IBLBrandedBottle>
    {
        bool SaveBrandedBottles(IEnumerable<IBLBrandedBottle> _bottles);
    }
    public interface IAssociatedBottleBLManager : IBLManager<IBLAssociatedBottle> { }
    public interface ICategoryBLManager : IBLManager<IBLCategory>
    {
        IEnumerable<IBLCategory> GetRootCategories();
        IEnumerable<IBLCategory> GetSubCategories();
        IEnumerable<IBLCategory> GetSubCategories(Guid parentID);
    }
    public interface IGroupBLManager : IBLManager<IBLGroup>{}
    public interface IInventoryBLManager : IBLManager<IBLInventory> 
    {
        IBLInventory Create(IBLUPCItem _UPCItem, IBLLocation _Location, decimal _Cost);
        IBLInventory Create(IBLBrandedBottle _Bottle);
        bool AddToInventory(IBLInventory _Inventory, int _Quantity);
        IBLInventory GetInventoryByTag(Guid _tagID);
        bool TagInventory(IBLUPCItem _UPC, IBLTag _tag, IBLLocation _fromLocation, IBLLocation _toLocation);
        bool TagInventory<T>(T _bottle) where T : IBLBottle;
        //bool TagInventory(IBLAssociatedBottle _Bottle);
        //bool TagInventory(IBLBrandedBottle _Bottle);
        //bool TagInventory(Guid _UPCID, Guid _TagID, Guid _LocationID);
        bool RemoveUnTaggedFromInventory(Guid _upcid, Guid _locationId, ExitReasons _reason, string _memo = null);
        bool RemoveUnTaggedItemsFromInventory(Guid _upcid, Guid _locationId, int _count, ExitReasons _reason, string _memo = null);
        bool RemoveTagFromInventory(Guid _tagId, ExitReasons _reason );
        bool MoveUnTaggedInventory(Guid _upcid, Guid _from, Guid _to, decimal _cost = 0.0m);
        int CheckAvailableInventory(Guid _upcID, Guid _locationID);
        int GetInventoryCount(Guid _upcid, Guid _locationId);
    }
    public interface IInventoryItemBLManager : IBLManager<IBLInventoryItem>
    {
        IBLInventoryItem Create(IBLInventory _Item);
        IBLInventoryItem Create(IBLTag _Tag);
        void CleanupInventory();
    }
    public interface IInventoryItemViewBLManager : IBLManager<IBLInventoryItemView>{ }
    public interface ILocationBLManager : IBLManager<IBLLocation>
    {
        IEnumerable<IBLLocation> GetStorageLocations();
        IBLLocation NewInventoryLocation { get; }
        string GetLocationByDeviceID(string _deviceID);
    }
    public interface IOrganizationBLManager : IBLManager<IBLOrganization> { }
    public interface IParLevelBLManager : IBLManager<IBLParLevel> { }
    public interface IPourBLManager : IBLManager<IBLPour>
    {
        IEnumerable<IBLPour> Top( int count );
        Dictionary<string, KeyValuePair<DateTime, double>> GetPourTotals(DateTime start, DateTime end);
        IList<IUIPourPoint> GetPourPoints(int pointCount);
    }
    public interface IPosPourBLManager : IBLManager<IBLPosPour>
    {
        IEnumerable<IUIPosPour> GetAfter(DateTime _beginDate);
    }
    public interface ISizeTypeBLManager : IBLManager<IBLSizeType>
    {
    }
    public interface ITagBLManager : IBLManager<IBLTag>
    {
        void UnBrand(IBLTag _tag, ExitReasons _reason);
//        bool BrandBottle(string _tagID, string _upc, IBLStandardNozzle _nozzle);
        IEnumerable<IBLTag> GetTagWithInventory(string _tagId);
        IBLTag GetTagByTagNumber(string _tagId);
        string GetTagNumber(Guid _id);
        void UpdateTagLocations(Guid _location, Guid _tagId);
    }
    public interface IDeviceBLManager : IBLManager<IBLDevice> { }
    public interface IActivityLogBLManager : IBLManager<IBLActivityLog>
    {
        IEnumerable<IBLActivityLog> Top( int count );
    }
    public interface ITagActivityBLManager : IBLManager<IBLTagActivity>
    {
        IEnumerable<IBLTagActivity> Top( int count );
    }
    public interface IUPCItemBLManager : IBLManager<IBLUPCItem>
    {
        IEnumerable<IBLUPCItem> GetUPCsByRootCategory(IBLCategory rootCategory, IEnumerable<IBLUPCItem> query);
        IEnumerable<IBLUPCItem> GetUPCsBySubCategory(IBLCategory subCategory, IEnumerable<IBLUPCItem> query);
        IEnumerable<IBLUPCItem> GetUPCsByManufacturer(IBLCategory subCategory, string manufacturer, IEnumerable<IBLUPCItem> query);
        IEnumerable<String> GetManufacturersBySubCategory( Guid CategoryID );
        IEnumerable<String> GetManufacturers( );
        IBLUPCItem GetUPCByUPCNumber(string _upcNumber);
        string GetUPCNameByTagNumber(string _tagNumber);

        IEnumerable<IBLUPCItem> GetAllView( );

        bool ImportUPCList(string _fileName);

    }
    public interface IUserBLManager : IBLManager<IBLUser>
    {
        bool Login(string _UserID, string _Password, out IBLUserSession _SessionID, out string ProperName );
    }
    public interface IUserSessionBLManager : IBLManager<IBLUserSession>{}
    public interface ITagAlertBLManager : IBLManager<IBLTagAlert>
    {
        IEnumerable<IBLTagAlert> Top( int count );
    }
    public interface IReportBLManager : IBLManager<IBLReport>
    {
        IEnumerable<IBLReport> GetReportsByUser( Guid _SessionID );
    }
    public interface IManufacturerViewBLManager : IBLManager<IBLManufacturerView> {}
    public interface IManufacturerBLManager : IBLManager<IBLManufacturer> {}
    public interface IStandardNozzleBLManager : IBLManager<IBLStandardNozzle> { }
    public interface IStandardPourBLManager : IBLManager<IBLStandardPour>
    {
        double Get(string _name );
    }
    public interface IStandardPriceBLManager : IBLManager<IBLStandardPrice> { }
    public interface IQualityBLManager : IBLManager<IBLQuality> { }
    public interface IPOSTicketBLManager : IBLManager<IBLPOSTicket> { }
    public interface IRecipeBLManager : IBLManager<IBLRecipe> {}
    public interface ITicketItemAliasBLManager : IBLManager<IBLTicketItemAlias> 
    {
        IEnumerable<string> GetUnknownAliases();
    }
    public interface IIngredientBLManager : IBLManager<IBLIngredient> { }
    public interface IPOSTicketItemBLManager : IBLManager<IBLPOSTicketItem>
    {
        IEnumerable<string> GetUniqueDescriptions();
    }

    //public interface ICartBLManager : IBLManager<IBLCart> { }
    //public interface IEventBLManager : IBLManager<IBLEvent> { }

}

