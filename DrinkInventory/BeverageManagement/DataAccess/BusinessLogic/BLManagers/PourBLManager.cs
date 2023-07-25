using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.Inventory.Data.BusinessLogic.BLObjects;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Inventory.Data.IUIDataItems;
using Jaxis.Util.Log4Net;
using JaxisExtensions;

namespace Jaxis.Inventory.Data
{
    public class PourBLManager : BLManager<IPour, IBLPour>, IPourBLManager
    {
        public IEnumerable<IBLPour> Top( int count )
        {
            return DataManagerFactory.Get( ).Manage<IPour>( ).GetAll( )
                .OrderByDescending( A => A.PourTime ).Take( count ).ToList( ).Cast<IBLPour>( );
        }


        #region IPourBLManager Members


        public Dictionary<string, KeyValuePair<DateTime, double>> GetPourTotals( DateTime start, DateTime end )
        {
            var rc = new Dictionary<string, KeyValuePair<DateTime, double>>();

        //    var Cats = DataManagerFactory.Get( ).Manage<ICategory>( ).GetAll( ).Where( C => C.ParentID == null );
        //    foreach (var category in Cats)
        //    {
        //        rc[category.Name] = new KeyValuePair<DateTime, double>( end, 0.0f );
        //    }

        //    var q =
        //            from PC in DataManagerFactory.Get( ).Manage<ICategory>( ).GetAll( ) 
        //            join C in DataManagerFactory.Get( ).Manage<ICategory>( ).GetAll( ) on PC.CategoryID equals C.ParentID
        //            join U in DataManagerFactory.Get( ).Manage<IUPCItem>( ).GetAll( ) on C.CategoryID equals U.CategoryID
        //            join T in DataManagerFactory.Get( ).Manage<ITag>( ).GetAll( ) on U.UPCID equals T.UPCID
        //            join P in DataManagerFactory.Get( ).Manage<IPour>( ).GetAll( ) on T.TagID equals P.TagID
        //            where P.PourTime >= start && P.PourTime < end
        //            group P by PC.Name into pours
        //            select new { Cat = pours.Key, Vol = pours.Sum( a => a.Volume ) };

        //    foreach (var item in q)
        //    {
        //        rc[item.Cat] = new KeyValuePair<DateTime, double>( end, item.Vol );
        //    }
            return rc;
        }


        public IList<IUIPourPoint> GetPourPoints( int pointCount )
        {
            string size = BLManagerFactory.Get().GetDefaultSizeType().Abbreviation;

            return Log.Time( "GetPourPoints", LogType.Debug, true, ( ) =>
            {
                var items = new List<IUIPourPoint>();
                //var beer =
                //    from PC in DataManagerFactory.Get().Manage<ICategory>().GetAll()
                //    join C in DataManagerFactory.Get().Manage<ICategory>().GetAll() on PC.CategoryID equals C.ParentID
                //    join U in DataManagerFactory.Get().Manage<IUPCItem>().GetAll() on C.CategoryID equals U.CategoryID
                //    join T in DataManagerFactory.Get().Manage<ITag>().GetAll() on U.UPCID equals T.UPCID
                //    join P in DataManagerFactory.Get().Manage<IPour>().GetAll() on T.TagID equals P.TagID
                //    join L in DataManagerFactory.Get().Manage<ILocation>().GetAll() on P.LocationID equals  L.LocationID
                //    where PC.Name.Equals( "Beer" )
                //    orderby P.PourTime descending
                //    select
                //        new PourPoint()
                //            {
                //                Volume = P.Volume,
                //                Category = PC.Name,
                //                PourTime = P.PourTime,
                //                TagID = T.TagID,
                //                TagNumber = T.TagNumber,
                //                UPCName = U.Name,
                //                Location = L.Name,
                //                Units = size
                //            };

                //var list = beer.Take(pointCount).ToList();
                //items.AddRange( list.Cast<IUIPourPoint>().ToList() );

                //var liquor =
                //    from PC in DataManagerFactory.Get().Manage<ICategory>().GetAll()
                //    join C in DataManagerFactory.Get().Manage<ICategory>().GetAll() on PC.CategoryID equals C.ParentID
                //    join U in DataManagerFactory.Get().Manage<IUPCItem>().GetAll() on C.CategoryID equals U.CategoryID
                //    join T in DataManagerFactory.Get().Manage<ITag>().GetAll() on U.UPCID equals T.UPCID
                //    join P in DataManagerFactory.Get().Manage<IPour>().GetAll() on T.TagID equals P.TagID
                //    join L in DataManagerFactory.Get().Manage<ILocation>().GetAll() on P.LocationID equals L.LocationID
                //    where PC.Name.Equals("Liquor")
                //    orderby P.PourTime descending
                //    select
                //        new PourPoint()
                //            {
                //                Volume = P.Volume,
                //                Category = PC.Name,
                //                PourTime = P.PourTime,
                //                TagID = T.TagID,
                //                TagNumber = T.TagNumber,
                //                UPCName = U.Name,
                //                Location = L.Name,
                //                Units = size
                //            };

                //items.AddRange( liquor.Take( pointCount ).ToList( ).Cast<IUIPourPoint>( ).ToList( ) );
                //items.Reverse();
                return items;

            } )
            ;
        }

        #endregion
    }
}
