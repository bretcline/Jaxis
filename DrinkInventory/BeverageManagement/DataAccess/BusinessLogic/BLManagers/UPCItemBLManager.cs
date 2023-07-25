using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using FileHelpers;
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
            return
                DataManagerFactory.Get().Manage<IUPCItem>().GetAll().Where(u => u.ItemNumber == _upcNumber).
                    FirstOrDefault() as IBLUPCItem;
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
            if (0 < item.Count())
            {
                rc = item.FirstOrDefault();
            }
            return rc;
        }

        public IEnumerable<IBLUPCItem> GetUPCsByRootCategory(IBLCategory rootCategory, IEnumerable<IBLUPCItem> query)
        {
            return Log.Time("GetUPCsByRootCategory", LogType.Debug, true, () =>
            {
                Guid id = rootCategory.CategoryID;
                query =
                    query.Join(
                        DataManagerFactory.Get().Manage<ICategory>().
                            GetAll().Where(c => c.ParentID == id),
                        o => o.CategoryID,
                        i => i.CategoryID,
                        (o, i) => o);
                return
                    query.ToList().AsQueryable().Cast<IBLUPCItem>();

            });
        }

        public IEnumerable<IBLUPCItem> GetUPCsBySubCategory(IBLCategory subCategory, IEnumerable<IBLUPCItem> query)
        {
            return Log.Time( "GetUPCsBySubCategory", LogType.Debug, true, ( ) =>
            {
                Guid id = subCategory.CategoryID;
                query =
                    query.Join(
                        DataManagerFactory.Get().
                            Manage<ICategory>().GetAll()
                            .Where(
                                c =>
                                c.CategoryID == id),
                        o => o.CategoryID,
                        i => i.CategoryID,
                        (o, i) => o);
                return
                    query.ToList().AsQueryable().Cast
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


        public bool ImportUPCList(string _fileName)
        {
            return Log.Wrap("ImportUPCList", LogType.Debug, false, () =>
            {
                if (File.Exists(_fileName))
                {
                    var engine = new DelimitedFileEngine<UpcImportObject>(); 
	 
                        // To Read Use: 
                    var items = (engine.ReadFile(_fileName) as UpcImportObject[]).ToList(); 
                    
                    //var items = new List<UpcImportObject>();
                    //using (var reader = new StreamReader(_fileName))
                    //{
                    //    var line = reader.ReadLine();
                    //    if (line.StartsWith("UPC"))
                    //    {
                    //        line = reader.ReadLine();
                    //    }
                    //    do
                    //    {
                    //        items.Add( new UpcImportObject( line ));
                    //        line = reader.ReadLine();
                    //    } while (null != line);
                    //}
                    if (0 < items.Count)
                    {
                        var categories = items.Select(i => new {Category = i.Category}).Distinct().ToList();

                        var subCategories = items.Select(i => new { Category = i.Category, SubCategory = i.SubCategory }).Distinct().ToList();

                        var upc = items.Select(i => new { UPC = i.UPC, UPCName = i.Description }).ToList();
                    }
                }
                return true;
            });
        }
    }

    [IgnoreFirst(1)] 
    [DelimitedRecord("\t")] 
    class UpcImportObject
    {
        //public UpcImportObject( string _data )
        //{
        //    var data = _data.Split(',');
        //    UPC = data[0];
        //    Description = data[1];
        //    Category = data[2];
        //    SubCategory = data[3];
        //    Brand = data[4];
        //    BeverageType = data[5];
        //    Volume = data[6];
        //    UOM = data[7];
        //    Manufacturer = data[8];
        //    CartonNumber = data[9];
        //    BottleCost = data[10];
        //    SinglePrice = data[11];
        //    DoublePrice = data[12];
        //}

        public string UPC { get; set; }
        [FieldQuoted('"', QuoteMode.OptionalForRead)] 
        public string Description;
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Brand { get; set; }
        public string BeverageType { get; set; }
        public string Volume { get; set; }
        public string UOM { get; set; }
        public string Manufacturer { get; set; }
        public string CartonNumber { get; set; }
        public string BottleCost { get; set; }
        public string SinglePrice { get; set; }
        public string DoublePrice { get; set; }

    }
}
