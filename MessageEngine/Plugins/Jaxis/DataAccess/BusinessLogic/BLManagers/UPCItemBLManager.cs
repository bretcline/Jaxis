using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Util.Log4Net;

namespace Jaxis.Inventory.Data
{
    public class UPCItemBLManager : BLManager<IUPCItem, IBLUPCItem>, IUPCItemBLManager
    {
        public UPCItemBLManager( )
        {
            OnCreate = ModifyCreated;
        }

        public override bool Save(IBLUPCItem item)
        {
            bool rc = false;
            if( item.UPCID != Guid.Empty )
            {
                rc = base.Save(item);
            }
            return rc;
        }

        private void ModifyCreated( IBLUPCItem _item )
        {
            //_item.Nozzle.Length;
            UPC item = (UPC) _item;
            item.ManufacturerID = Guid.Empty;
        }

        public IBLUPCItem GetUPCByUPCNumber( string _upcNumber )
        {
            IBLUPCItem rc = null;
            try
            {
                rc = DataManagerFactory.Get().Manage<IUPCItem>().GetAll().FirstOrDefault(u => u.ItemNumber == _upcNumber) as IBLUPCItem;
                if (null != rc && null == rc.Nozzle)
                {
                    var root = DataManagerFactory.Get().Manage<ICategory>().Get(rc.RootCategoryID);
                    if( null != root )
                    {
                        switch (root.Name)
                        {
                            case "Wine":
                            {
                                rc.Nozzle = BLManagerFactory.Get().GetDefaultNozzle(DefaultNozzleType.Wine);
                                break;
                            }
                            case "Liquor":
                            {
                                rc.Nozzle = BLManagerFactory.Get().GetDefaultNozzle();
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                
                throw;
            }

            return rc;
        }

        public string GetUPCNameByTagNumber( string _tagNumber )
        {
            string rc = "Unknown";
            var item = from upc in DataManagerFactory.Get().Manage<IUPCItem>().GetAll()
                       join inv in DataManagerFactory.Get().Manage<IInventory>().GetAll() on upc.UPCID equals inv.UPCID
                       join tag in DataManagerFactory.Get().Manage<ITag>().GetAll() on inv.TagID equals tag.TagID
                       where inv.ExitDate == null
                            && tag.TagNumber.Equals( _tagNumber )
                       select upc.Name;
            if (item.Any())
            {
                rc = item.FirstOrDefault();
            }
            return rc;
        }

        public IEnumerable<IBLUPCItem> GetUPCsByRootCategory(IBLCategory _rootCategory, IEnumerable<IBLUPCItem> _query)
        {
            return Log.Time("GetUPCsByRootCategory", LogType.Debug, true, () =>
            {
                Guid id = _rootCategory.CategoryID;
                _query =
                    _query.Join(
                        DataManagerFactory.Get().Manage<ICategory>().
                            GetAll().Where(c => c.ParentID == id),
                        o => o.CategoryID,
                        i => i.CategoryID,
                        (o, i) => o);
                return
                    _query.ToList().AsQueryable().Cast<IBLUPCItem>();

            });
        }

        public IEnumerable<IBLUPCItem> GetUPCsBySubCategory(IBLCategory _subCategory, IEnumerable<IBLUPCItem> _query)
        {
            return Log.Time( "GetUPCsBySubCategory", LogType.Debug, true, ( ) =>
            {
                Guid id = _subCategory.CategoryID;
                _query =
                    _query.Join(
                        DataManagerFactory.Get().
                            Manage<ICategory>().GetAll()
                            .Where(
                                c =>
                                c.CategoryID == id),
                        o => o.CategoryID,
                        i => i.CategoryID,
                        (o, i) => o);
                return
                    _query.ToList().AsQueryable().Cast
                        <IBLUPCItem>();
            });
        }

        public IEnumerable<IBLUPCItem> GetUPCsByManufacturer(IBLCategory subCategory, string manufacturer,
                                                      IEnumerable<IBLUPCItem> query)
        {
            return Log.Time( "GetUPCsByManufacturer", LogType.Debug, true, ( ) =>
            {
                Guid id = subCategory.CategoryID;
                query =
                    query.Where(
                        q =>
                        q.Manufacturer.Equals(
                            manufacturer) &&
                        q.CategoryID == id);
                return
                    query.ToList().AsQueryable().Cast
                        <IBLUPCItem>();
            });
        }

        public IEnumerable<String> GetManufacturersBySubCategory( Guid id )
        {
            return Log.Time( "GetManufacturersBySubCategory", LogType.Debug, true, ( ) =>
            {
                //var dataItems = DataManagerFactory.Get( ).Manage<IUPCItem>( ).GetAll( )
                //    .Where(m => m.CategoryID == id)
                //    .GroupBy(m => m.Manufacturer)
                //    .Select(m => m.FirstOrDefault());
                var dataItems =
                    DataManagerFactory.Get( ).Manage
                        <IManufacturerView>( ).GetAll( ).Where( M => M.CategoryID.Equals( id ) );
                return
                    dataItems.Select(
                        d => d.Name );
            });
        }

        public IEnumerable<String> GetManufacturers( )
        {
            return Log.Time( "GetManufacturers", LogType.Debug, true, ( ) =>
            {
                var dataItems =
                    DataManagerFactory.Get().Manage
                        <IManufacturerView>().GetAll();
                return
                    dataItems.Select(
                        d => d.Name );
            });
        }

        public IEnumerable<IBLUPCItem> GetAllView( )
        {
            return DataManagerFactory.Get( ).Manage<IUPCItemView>( ).GetAll( ).ToList( ).Cast<IBLUPCItem>( );
        }


        public IEnumerable<IUIUPCItemShort> GetByManufacturerAndQuality(IBLIngredient _ingredient)
        {
            return
                DataManagerFactory.Get().Manage<IUPCItemView>()
                                .GetAll()
                                .Where(
                                    u =>
                                    u.ManufacturerID == _ingredient.ManufacturerID.Value && u.Quality == _ingredient.Quality.Value).ToList()
                                .Cast<IUIUPCItemShort>()
                                .ToList();
        }

        public IEnumerable<IUIUPCItemShort> GetByManufacturerNotQuality(IBLIngredient _ingredient)
        {

            return
                DataManagerFactory.Get().Manage<IUPCItemView>()
                                .GetAll()
                                .Where(
                                    u =>
                                    u.ManufacturerID == _ingredient.ManufacturerID.Value && u.Quality != _ingredient.Quality.Value).ToList()
                                .Cast<IUIUPCItemShort>()
                                .ToList();
        }


        public IEnumerable<IUIUPCItemShort> GetByCategoryAndQuality(IBLIngredient _ingredient)
        {
            return
                DataManagerFactory.Get().Manage<IUPCItemView>()
                                .GetAll()
                                .Where(
                                    u =>
                                    u.CategoryID == _ingredient.CategoryID.Value && u.Quality == _ingredient.Quality.Value).ToList()
                                .Cast<IUIUPCItemShort>()
                                .ToList();
        }

        public IEnumerable<IUIUPCItemShort> GetByCategoryNotQuality(IBLIngredient _ingredient)
        {

            return
                DataManagerFactory.Get().Manage<IUPCItemView>()
                                .GetAll()
                                .Where(
                                    u =>
                                    u.CategoryID == _ingredient.CategoryID.Value && u.Quality != _ingredient.Quality.Value).ToList()
                                .Cast<IUIUPCItemShort>()
                                .ToList();
        }

        public IEnumerable<IBLUPCItem> GetActiveWithDefaultCost()
        {
            var query = (from upc in DataManagerFactory.Get().Manage<IUPCItem>().GetAll()
                        join inv in DataManagerFactory.Get().Manage<IInventory>().GetAll() on upc.UPCID equals inv.UPCID
                        where inv.ExitDate == null
                            && (upc.UnitCost == null)
                        select upc).Distinct().OrderBy(u => u.Name);
            return
                query.AsEnumerable().Cast<IBLUPCItem>();
        }

        public bool ImportUPCList(string _fileName)
        {
            return Log.Wrap("ImportUPCList", LogType.Debug, false, () =>
            {
                return true;
            });
        }
    }
}
