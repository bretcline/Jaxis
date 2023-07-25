using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.Inventory.Data.IDataItems;
using Jaxis.Inventory.Data.Reports;
using SubSonic.Schema;

namespace Jaxis.Inventory.Data
{
    public interface IDataManager
    {
        
    }

    public interface IDataManager<T> : IDataManager
    {
        T Create( );
        bool Save(T item);
        bool Save(T item, out IList<string> results);
        bool Delete(T item, out IList<string> results);

        IQueryable<T> GetAll( );
        T Get( Guid ID );
    }


    public interface IDeviceManager : IDataManager<IDevice> { }
    public interface ISecurableItemDataManager : IDataManager<ISecurableItem> { }
    public interface ICartDataManager : IDataManager<ICart> { }
    public interface ICategoryDataManager : IDataManager<ICategory> { }
    public interface IEventDataManager : IDataManager<IEvent>{}
    public interface IGroupDataManager : IDataManager<IGroup>{}
    public interface IInventoryDataManager : IDataManager<IInventory> { }
    public interface IInventoryItemViewDataManager : IDataManager<IInventoryItemView> { }
    public interface ILocationDataManager : IDataManager<ILocation> { }
    public interface IOrganizationDataManager : IDataManager<IOrganization>{}
    public interface IPourDataManager : IDataManager<IPour>{}
    public interface ISizeTypeDataManager : IDataManager<ISizeType>{}
    public interface ITagActivityDataManager : IDataManager<ITagActivity> { }
    public interface ITagAlertDataManager : IDataManager<ITagAlert> { }
    public interface ITagDataManager : IDataManager<ITag> { }
    public interface ITagMoveDataManager : IDataManager<ITagMove> { }
    public interface IActivityLogDataManager : IDataManager<IActivityLog> { }
    public interface IDeviceDataManager : IDataManager<IDevice> { }
    public interface IUPCItemDataManager : IDataManager<IUPCItem> { }
    public interface IUPCItemDataViewManager : IDataManager<IUPCItemView> { }
    public interface IUserDataManager : IDataManager<IUser> { }
    public interface IUserSessionDataManager : IDataManager<IUserSession> { }
    public interface IReportDataManager : IDataManager<IReport> { }
    public interface IPourReportDataManager : IDataManager<IRPourTotals> { }
    public interface ITagPourReportDataManager : IDataManager<IRTagPourTotals> { }
    public interface IReportParameterDataManager : IDataManager<IReportParameter> { }
    public interface IManufacturerDataManager : IDataManager<IManufacturer> { }
    public interface IManufacturerViewDataManager : IDataManager<IManufacturerView> { }
    public interface IStandardNozzleDataManager : IDataManager<IStandardNozzle> { }
    public interface IStandardPriceDataManager : IDataManager<IStandardPrice> { }
    public interface IStandardPourDataManager : IDataManager<IStandardPour> { }
    public interface IParLevelDataManager : IDataManager<IParLevel> { }
    public interface IPosPourDataManager : IDataManager<IPosPour> { }

    public interface IPOSTicketManager : IDataManager<IPOSTicket> { }
    public interface IPOSTicketItemManager : IDataManager<IPOSTicketItem> { }
    public interface IPOSTicketItemModifierManager : IDataManager<IPOSTicketItemModifier> { }

    public interface IRecipeManager : IDataManager<IRecipe> { }
    public interface IIngredientManager : IDataManager<IIngredient> { }
    public interface ITicketItemAliasManager : IDataManager<ITicketItemAlias> {}
    public interface IPosPourManager : IDataManager<IPosPour> { }
    public interface IQualityManager : IDataManager<IQuality> { }

}
