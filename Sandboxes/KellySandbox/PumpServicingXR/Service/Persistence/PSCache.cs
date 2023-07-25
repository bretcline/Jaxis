using System;
using System.Collections.Generic;
using WFT.PSService.ServiceLibrary;
using System.Linq;

namespace WFT.PSService.Service
{
    public class XRCache<T> where T : XRData<T>
    {
    //    #region member variables

    //    protected SortedDictionary<string, T> dataSortedByXRef;
    //    protected SortedDictionary<Guid, T> dataSortedByGuid;
    //    protected List<T> itemsByDate;

    //    protected static Func<T, T, bool> compareDate = ( item1, item2 ) => item1.LastModified < item2.LastModified;

    //    #endregion

    //    #region properties

    //    public SortedDictionary<string, T> DataSortedByXRef
    //    {
    //        get { return dataSortedByXRef; }
    //    }

    //    /// <summary>
    //    /// Gets or sets the last successful sync.
    //    /// </summary>
    //    /// <value>The last successful sync.</value>
    //    public DateTime LastSuccessfulSync { get; set; }

    //    #endregion

    //    //----------------------------------------------------------------------
    //    public XRCache( )
    //    {
    //        Init( );
    //    }

    //    //----------------------------------------------------------------------
    //    /// <summary>
    //    /// Initializes a new instance of the <see cref="XRCache{T}"/> class.
    //    /// </summary>
    //    /// <param name="itemInstances">The item instances.</param>
    //    public XRCache( List<T> itemInstances )
    //    {
    //        Init( );
    //        AddItemsInOrder( itemInstances );
    //    }

    //    //----------------------------------------------------------------------
    //    protected virtual void Init( )
    //    {
    //        this.dataSortedByXRef = new SortedDictionary<string, T>( );
    //        this.dataSortedByGuid = new SortedDictionary<Guid, T>( );
    //        itemsByDate = new List<T>( );
    //    }

    //    //----------------------------------------------------------------------
    //    protected void AddItemsInOrder( List<T> items )
    //    {
    //        foreach( T item in items )
    //        {
    //            AddItem( item );
    //        }
    //    }

    //    //----------------------------------------------------------------------
    //    public virtual void Clear( )
    //    {
    //        dataSortedByXRef.Clear( );
    //        dataSortedByGuid.Clear( );
    //        itemsByDate.Clear( );
    //    }

    //    //----------------------------------------------------------------------
    //    public virtual void AddItem( T item )
    //    {
    //        Insert( item, itemsByDate, compareDate );

    //        if( !dataSortedByGuid.ContainsKey( item.ID ) )
    //        {
    //            if( item.ID != Guid.Empty )
    //                dataSortedByGuid.Add( item.ID, item );
    //        }

    //        if( item.XRefID != null && !dataSortedByXRef.ContainsKey( item.XRefID.ToUpper( ) ) )
    //            dataSortedByXRef.Add( item.XRefID.ToUpper( ), item );
    //    }

    //    //----------------------------------------------------------------------
    //    public bool ContainsXRef( string xref )
    //    {
    //        if( String.IsNullOrEmpty( xref ) )
    //            return false;

    //        return dataSortedByXRef.ContainsKey( xref.ToUpper( ) );
    //    }

    //    //----------------------------------------------------------------------
    //    public bool ContainsGuid( Guid id )
    //    {
    //        return dataSortedByGuid.ContainsKey( id );
    //    }

    //    //----------------------------------------------------------------------
    //    public string GetXRefIDByGuid( Guid id )
    //    {
    //        if( id == Guid.Empty )
    //            return "CSI00000";

    //        if( dataSortedByGuid.ContainsKey( id ) )
    //            return dataSortedByGuid[ id ].XRefID;

    //        throw new KeyNotFoundException( String.Format( "Key not found for GUID '{0}' on '{1}'.", id, BaseData<T>.ConstantTypeID ) );
    //    }

    //    //----------------------------------------------------------------------
    //    public string GetXRefIDByGuid( Guid? id )
    //    {
    //        if( id == null )
    //            return "CSI00000";

    //        return GetXRefIDByGuid( id.Value );
    //    }

    //    //----------------------------------------------------------------------
    //    public virtual T GetItemByGuid( Guid id )
    //    {
    //        if( dataSortedByGuid.ContainsKey( id ) )
    //            return dataSortedByGuid[ id ];

    //        throw new KeyNotFoundException( String.Format( "Key not found for GUID '{0}' on '{1}'.", id, BaseData<T>.ConstantTypeID ) );
    //    }

    //    //----------------------------------------------------------------------
    //    public virtual T GetItemByGuid( Guid? id )
    //    {
    //        if( id == null )
    //            throw new ArgumentException( String.Format( "Guid Key parameter was null in GetItemByGuid for '{0}'.", BaseData<T>.ConstantTypeID ) );

    //        if( dataSortedByGuid.ContainsKey( id.Value ) )
    //            return dataSortedByGuid[ id.Value ];

    //        throw new KeyNotFoundException( String.Format( "Guid Key not found for GUID '{0}' on '{1}'.", id, BaseData<T>.ConstantTypeID ) );
    //    }

    //    //----------------------------------------------------------------------
    //    public virtual T GetItemByXRef( string xref )
    //    {
    //        if( String.IsNullOrEmpty( xref ) )
    //            return null;

    //        return dataSortedByXRef.ContainsKey( xref ) ? dataSortedByXRef[ xref ] : null;
    //    }

    //    //----------------------------------------------------------------------
    //    public List<T> GetItems( )
    //    {
    //        return itemsByDate.ToList( );
    //    }

    //    //----------------------------------------------------------------------
    //    public List<T> GetData( List<T> items, int pageIndex, int pageSize, DateTime lastSyncDate, bool filterDeleted )
    //    {
    //        int firstIndex;
    //        int lastIndex;
    //        SetPagingIndex( items, pageIndex, pageSize, out firstIndex, out lastIndex );

    //        return items.GetRange( firstIndex, lastIndex );
    //    }

    //    //----------------------------------------------------------------------
    //    private void SetPagingIndex( List<T> items, int pageIndex, int pageSize, out int firstIndex, out int itemRange )
    //    {
    //        int maxIndex = items.Count - 1;

    //        // if pageSize of 0 is passed in, return all items with date greate than last sync date
    //        if( pageSize == 0 )
    //        {
    //            firstIndex = 0;
    //            itemRange = maxIndex + 1;
    //        }
    //        else
    //        {
    //            firstIndex = pageIndex * pageSize;
    //            itemRange = pageSize;
    //        }

    //        if( firstIndex > maxIndex )
    //        {
    //            firstIndex = maxIndex;
    //            itemRange = 0;
    //        }

    //        if( itemRange > items.Count )
    //            itemRange = items.Count;

    //        if( firstIndex + itemRange > items.Count )
    //            itemRange = items.Count - firstIndex;
    //    }

    //    //----------------------------------------------------------------------
    //    public List<T> GetDataSortedByDate( List<T> items, int pageIndex, int pageSize, DateTime lastSyncDate )
    //    {
    //        if( items.Count == 0 )
    //            return items;

    //        int firstIndex;
    //        int itemRange;

    //        // if a page index and size is requested that is outside the bounds of the collection, an empty list is returned
    //        // this is how the client applications previously checked to see if they were finished collecting data
    //        try
    //        {
    //            SetPagingIndex( items, pageIndex, pageSize, out firstIndex, out itemRange );
    //        }
    //        catch( ArgumentException )
    //        {
    //            return new List<T>( );
    //        }

    //        // if first item is greater than last sync date, return all items in the page size
    //        if( items[ 0 ].LastModified > lastSyncDate )
    //            return items.GetRange( firstIndex, itemRange );

    //        // if the last item is less than the last sync date, return none of the items
    //        if( items[ items.Count - 1 ].LastModified < lastSyncDate )
    //            return new List<T>( );

    //        // otherwise, search for first item that has a date greater than the last sync date
    //        // and return everything after that item within the page size
    //        int firstIndexGreater = GetTargetIndex( items, lastSyncDate, firstIndex );

    //        if( firstIndexGreater == items.Count )
    //            return new List<T>( );

    //        // increment index so that the first item we are returning is based on pageIndex and pageSize
    //        firstIndexGreater += firstIndex;

    //        // if page size would be out of bounds, adjust page size to the size of remaining items
    //        if( firstIndexGreater >= items.Count )
    //            return new List<T>( );

    //        if( firstIndexGreater + pageSize >= items.Count )
    //            pageSize = items.Count - firstIndexGreater;

    //        return items.GetRange( firstIndexGreater, pageSize );
    //    }

    //    //----------------------------------------------------------------------
    //    private int GetTargetIndex( List<T> items, DateTime lastSyncDate, int startIndex )
    //    {
    //        int lastIndex = items.Count - 1;
    //        int targetIndex = ( startIndex + lastIndex ) / 2;

    //        while( targetIndex != lastIndex && targetIndex != startIndex )
    //        {
    //            if( items[ targetIndex ].LastModified > lastSyncDate )
    //            {
    //                lastIndex = targetIndex;
    //                targetIndex = ( startIndex + lastIndex ) / 2;
    //            }
    //            else
    //            {
    //                startIndex = targetIndex;
    //                targetIndex = ( startIndex + lastIndex + 1 ) / 2;
    //            }
    //        }

    //        if( items[ targetIndex ].LastModified <= lastSyncDate )
    //            ++targetIndex;

    //        return targetIndex;
    //    }

    //    //----------------------------------------------------------------------
    //    public List<T> GetDataSortedByDate( int pageIndex, int pageSize, DateTime lastSyncDate )
    //    {
    //        return GetDataSortedByDate( pageIndex, pageSize, lastSyncDate, false );
    //    }

    //    //----------------------------------------------------------------------
    //    /// <summary>
    //    /// Returns a List of WSData based on pageIndex, pageSize, and lastSync Date
    //    /// If pageIndex == 0 and PageSize ==0, all records more recent than last Sync Date are returned
    //    /// </summary>
    //    /// <param name="pageIndex">page index to grab, 0 for first page</param>
    //    /// <param name="pageSize">number of records to grab per page</param>
    //    /// <param name="lastSyncDate">only records more recent than this date are returned</param>
    //    /// <param name="filterDeleted"></param>
    //    /// <returns></returns>
    //    public List<T> GetDataSortedByDate( int pageIndex, int pageSize, DateTime lastSyncDate, bool filterDeleted )
    //    {
    //        List<T> itemsList;

    //        if( filterDeleted )
    //        {
    //            IEnumerable<T> items = from item in itemsByDate
    //                                   where item.Deleted == false
    //                                   select item;
    //            itemsList = items.ToList( );
    //        }
    //        else
    //            itemsList = itemsByDate;

    //        return GetDataSortedByDate( itemsList, pageIndex, pageSize, lastSyncDate );
    //    }

    //    //----------------------------------------------------------------------
    //    protected void Insert( T item, List<T> items, Func<T, T, bool> leftLessThanRightComparer )
    //    {
    //        // If no items exist already, add the item and return
    //        if( items.Count == 0 )
    //        {
    //            items.Add( item );
    //            return;
    //        }

    //        int insertIndex = GetTargetIndex( items, item.LastModified, 0 );
    //        if( insertIndex >= items.Count )
    //            items.Add( item );
    //        else
    //            items.Insert( insertIndex, item );
    //    }

    //    //----------------------------------------------------------------------
    //    public void ReSort( )
    //    {
    //        itemsByDate.Sort( CompareByDate );
    //    }

    //    //----------------------------------------------------------------------
    //    public void Remove( T item )
    //    {
    //        if( dataSortedByGuid.ContainsKey( item.ID ) )
    //            dataSortedByGuid.Remove( item.ID );

    //        if( dataSortedByXRef.ContainsKey( item.XRefID ) )
    //            dataSortedByXRef.Remove( item.XRefID );

    //        int removeIndex = -1;
    //        for( int i = 0; i < itemsByDate.Count; ++i )
    //        {
    //            T dateItem = itemsByDate[ i ];
    //            if( dateItem.ID == item.ID )
    //            {
    //                removeIndex = i;
    //                break;
    //            }
    //        }

    //        if( removeIndex != -1 )
    //            itemsByDate.RemoveAt( removeIndex );
    //    }

    //    //----------------------------------------------------------------------
    //    private static int CompareByDate( XRData<T> leftItem, XRData<T> rightItem )
    //    {
    //        if( leftItem.LastModified < rightItem.LastModified )
    //            return -1;
    //        if( leftItem.LastModified > rightItem.LastModified )
    //            return 1;

    //        return 0;
    //    }

    //    //----------------------------------------------------------------------
    //    private static bool ItemsAreEqual( XRData<T> leftItem, XRData<T> rightItem )
    //    {
    //        if( leftItem.ID == rightItem.ID )
    //            return true;

    //        return false;
    //    }
    }
}
