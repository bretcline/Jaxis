using System;
using System.Collections.Generic;
using WFT.PSService.ServiceLibrary;
using log4net;

namespace WFT.PSService.Service
{
    //public class DataConverter
    //{
    //    private static log4net.ILog logger = log4net.LogManager.GetLogger( "DataConverter" );

    //    #region Dictionary Conversion
    //    public static SortedDictionary<string, T> ToDictionary<T>( List<T> list ) where T : XRData<T>
    //    {
    //        return DataConverter.ToDictionary( list, true );
    //    }

    //    public static SortedDictionary<string, T> ToDictionary<T>( List<T> list, bool logMissingXRefs ) where T : XRData<T>
    //    {
    //        SortedDictionary<string, T> output = new SortedDictionary<string, T>( );
    //        foreach( T item in list )
    //        {
    //            if( String.IsNullOrEmpty( item.XRefID ) && logMissingXRefs )
    //            {
    //                logger.ErrorFormat( "No XRefID found for '{0}' so it will not be processed in ToDictionary().", item.ToString( ) );
    //                continue;
    //            }

    //            if( output.ContainsKey( item.XRefID.ToUpper( ) ) )
    //                continue;

    //            output.Add( item.XRefID.ToUpper( ), item );
    //        }

    //        return output;
    //    }
    //    #endregion

    //    //#region Merge Methods from WSM Data into WS Data
    //    //public static List<T> Diff<T>( XRCache<T> mergeInto, IEnumerable<T> toMerge, Action<List<T>> populateXRefData ) where T : XRData<T>
    //    //{
    //    //    List<T> changedItems = new List<T>( 500 );
    //    //    foreach( T item in toMerge )
    //    //    {
    //    //        if( String.IsNullOrEmpty( item.XRefID ) )
    //    //        {
    //    //            logger.ErrorFormat( "No XRefID found for '{0}' so it will not be processed in Merge().", item.ToString( ) );
    //    //            continue;
    //    //        }

    //    //        if( populateXRefData != null )
    //    //            populateXRefData( new List<T>( ) { item } );

    //    //        if( mergeInto.ContainsXRef( item.XRefID.ToUpper( ) ) && mergeInto.GetItemByXRef( item.XRefID.ToUpper( ) ).DataEquals( item ) )
    //    //            continue;

    //    //        changedItems.Add( item );
    //    //    }

    //    //    return changedItems;
    //    //}

    //    //public static void Merge<T>( XRCache<T> mergeInto, IEnumerable<T> toMerge ) where T : XRData<T>
    //    //{
    //    //    foreach( T item in toMerge )
    //    //    {
    //    //        item.LastModified = DateTime.Now.ToUniversalTime( );

    //    //        if( mergeInto.ContainsXRef( item.XRefID.ToUpper( ) ) )
    //    //            mergeInto.GetItemByXRef( item.XRefID.ToUpper( ) ).Merge( item );
    //    //        else
    //    //            mergeInto.AddItem( item );
    //    //    }
    //    //}

    //    //public static List<T> DiffAndMerge<T>( XRCache<T> mergeInto, IEnumerable<T> toMerge, Action<List<T>> populateXRefData ) where T : XRData<T>
    //    //{
    //    //    List<T> changedItems = Diff( mergeInto, toMerge, populateXRefData );
    //    //    Merge( mergeInto, changedItems );
    //    //    return changedItems;
    //    //}
    //    //#endregion
    //}
}
