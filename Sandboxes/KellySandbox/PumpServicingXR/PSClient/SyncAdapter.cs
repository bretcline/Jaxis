using System;
using LFI.Sync.DataManager;
using LFI.Sync.Shared;
using WFT.PSService.ServiceLibrary;
using System.Collections.Generic;
using WFT.PS.Shared;

namespace PSClient
{
    //public class SyncAdapter<T> : IDownloadAdapter<T>, IUploadAdapter<T>, IResolveAdapter<T> where T : IBaseData
    public class SyncAdapter<T> : IUploadAdapter<T>  where T : IBaseData
    {
        protected DateTime _minDate = new DateTime( 1970, 1, 1 );
        public DateTime MinDateToSync { get { return _minDate; } set { _minDate = value; } }

        protected int _commitBatchSize = 100;
        public int CommitBatchSize { get { return _commitBatchSize; } set { _commitBatchSize = value; } }

        protected int _downloadBatchSize = 100;
        public int DownloadBatchSize { get { return _downloadBatchSize; } set { _downloadBatchSize = value; } }

        /// <summary>
        /// Fired when an item created on the mobile updates its actual key from the server
        /// </summary>
        public event EventHandler<ItemXRefChangedEventArgs> ItemXRefChanged;

        /// <summary>
        /// Fired when a data type recieves new or changed data from WSM
        /// </summary>
        public event EventHandler<DataChangedEventArgs> DataChanged;

        /// <summary>
        /// Fired when the adapter has a progress update to report
        /// </summary>
        public event EventHandler<AdapterProgressChangedEventArgs> ProgressChanged;

        /// <summary>
        /// Fired when the adpater has an error to report
        /// </summary>
        public event EventHandler<ErrorReportedEventArgs> ErrorReported;

        protected readonly SyncContext _syncContext;
        protected readonly DataTag _dataTag;
        protected readonly DataManager _dataManager;
        protected int _prevNumItemsCompleted;
        protected ISyncTransactionFactory _transactionFactory;

        //----------------------------------------------------------------------
        /// <summary>
        /// Initializes a new instance of the <see cref="SyncAdapter&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="dataTag">The data tag.</param>
        /// <param name="dataManager">The data manager.</param>
        /// <param name="context">The context.</param>
        public SyncAdapter( DataTag dataTag, DataManager dataManager, SyncContext context, ISyncTransactionFactory factory )
        {
            _dataTag = dataTag;
            _dataManager = dataManager;
            _syncContext = context;
            _transactionFactory = factory;
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Resolves data integrity contraint issues between local and remote data instances.
        /// </summary>
        /// <param name="resolveData">The delegate for aquiring data required for resolution.</param>
        /// <param name="lastResolve">The last time data was resolve.</param>
        /// <returns></returns>
        //public virtual int Resolve( Func<SyncContext, IList<T>> resolveData, DateTime lastResolve )
        //{
        //    bool initialSync = lastResolve == _minDate;

        //    _syncContext.LastSync = initialSync ? DateTime.Now : lastResolve;

        //    List<ITransaction> transactions = new List<ITransaction>( );
        //    IList<T> dataList = resolveData( _syncContext );

        //    foreach( T dataItem in dataList )
        //    {
        //        ITransaction transaction = _transactionFactory.BuildPutTransaction( dataItem, _dataTag );

        //        if( !RowExists( transaction.Table, transaction.IDColumn, dataItem.PrimaryKey ) )
        //            ReportError( String.Format( "The item with ID = {0} is not in the DB, deleted status disregarded.", dataItem.PrimaryKey ) );
        //        else
        //            transactions.Add( transaction );
        //    }

        //    if( transactions.Count >= 0 )
        //    {
        //        bool success;
        //        ProcessTransactions( transactions, false, out success );

        //        if( success )
        //            return transactions.Count;
        //    }

        //    return -1;
        //}

        //----------------------------------------------------------------------
        public virtual int Upload( Func<SyncContext, T, ServiceResult> uploadToServer, DateTime lastUpload )
        {
            _syncContext.LastSync = lastUpload;
            List<T> itemsToUpdate;

            try
            {
                IReaderTransaction<T> itemsToUploadTransaction = _transactionFactory.BuildUploadTransaction<T>( _dataTag, _syncContext.LastSync );
                itemsToUpdate = _dataManager.GetData( itemsToUploadTransaction );
            }
            catch( Exception ex )
            {
                ReportError( String.Format( "Failed to determine items to upload from database for {0}. Exception: {1}", _dataTag, ex.Message ) );
                return -1;
            }

            if( itemsToUpdate.Count == 0 )
                return 0;

            UpdateProgress( 0, itemsToUpdate.Count, SyncType.Upload );

            bool uploadSucceeded = true;
            int uploadedItemCount = 0;
            foreach( T item in itemsToUpdate )
            {
                ServiceResult result = new ServiceResult( );
                try
                {
                    result = uploadToServer.Invoke( _syncContext, item );
                }
                catch( Exception ex )
                {
                    result.Success = false;
                    result.Message = ex.Message;
                }
                if( result.Success == false )
                {
                    uploadSucceeded = false;
                    if( !String.IsNullOrEmpty( result.Message ) )
                        ReportError( String.Format( "Failed to upload item {0} with key {1} and message: '{2}'.", _dataTag, item.PrimaryKey, result.Message ) );

                    continue;
                }

                // update the guid on the mobile database with the newly created guid from the server
                if( CheckXRefChanged( item, result.ObjectKey ) )
                    InvokeXRefChanged( item, result.ObjectKey );

                ++uploadedItemCount;
            }

            UpdateProgress( uploadedItemCount, itemsToUpdate.Count, SyncType.Upload );

            if( uploadSucceeded )
                return uploadedItemCount;

            return -1;
        }

        //----------------------------------------------------------------------
        public virtual int Upload( Func<SyncContext, List<T>, List<ServiceResult>> uploadToServer, DateTime lastUpload )
        {
            _syncContext.LastSync = lastUpload;
            List<T> itemsToUpdate;

            try
            {
                IReaderTransaction<T> itemsToUploadTransaction = _transactionFactory.BuildUploadTransaction<T>( _dataTag, _syncContext.LastSync );
                itemsToUpdate = _dataManager.GetData( itemsToUploadTransaction );
            }
            catch( Exception ex )
            {
                ReportError( String.Format( "Failed to determine items to upload from database for {0}. Exception: {1}", _dataTag, ex.Message ) );
                return -1;
            }

            if( itemsToUpdate.Count == 0 )
                return 0;

            UpdateProgress( 0, itemsToUpdate.Count, SyncType.Upload );

            List<ServiceResult> results = new List<ServiceResult>( );
            try
            {
                results = uploadToServer.Invoke( _syncContext, itemsToUpdate );
            }
            catch( Exception ex )
            {
                results.Add( new ServiceResult { Success = false, Message = ex.Message } );
            }

            UpdateProgress( itemsToUpdate.Count, itemsToUpdate.Count, SyncType.Upload );

            bool success = true;

            Dictionary<string, T> itemsByID = new Dictionary<string, T>( );
            foreach( T item in itemsToUpdate )
            {
                itemsByID.Add( ( string ) item.PrimaryKey, item );
            }

            Dictionary<string, ServiceResult> resultsByID = new Dictionary<string, ServiceResult>( );
            foreach( ServiceResult result in results )
            {
                if( itemsByID.ContainsKey( result.ObjectGuid.ToString( ) ) )
                {
                    T item = itemsByID[ result.ObjectGuid.ToString( ) ];

                    // update the guid on the mobile database with the newly created guid from the server
                    if( CheckXRefChanged( item, result.ObjectKey ) )
                        InvokeXRefChanged( item, result.ObjectKey );
                }

                resultsByID.Add( result.ObjectGuid.ToString( ), result );
                success &= result.Success;
            }

            return success ? 1 : 0;
        }

        //----------------------------------------------------------------------
        //public virtual int Download( Func<SyncContext, IList<T>> getData, int numItemsToSync, DateTime lastDownload )
        //{
        //    bool isMergeSuccessful = true;
        //    int numItemsDownloaded = 0;
        //    _prevNumItemsCompleted = 0;
        //    _syncContext.PageSize = DownloadBatchSize;

        //    UpdateProgress( numItemsDownloaded, numItemsToSync, SyncType.Download );

        //    _syncContext.LastSync = lastDownload;
        //    bool initialSync = _syncContext.LastSync == _minDate;
        //    List<ITransaction> transactions = new List<ITransaction>( );
        //    IList<T> dataList = getData( _syncContext );

        //    while( dataList.Count != 0 )
        //    {
        //        UpdateProgress( numItemsDownloaded, numItemsToSync, SyncType.Download );

        //        foreach( T dataItem in dataList )
        //        {
        //            ITransaction transaction = _transactionFactory.BuildPutTransaction( dataItem, _dataTag );
        //            if( !RowExists( transaction.Table, transaction.IDColumn, dataItem.PrimaryKey ) )
        //                transaction.SetTransactionType( TransactionType.Insert );
        //            ++numItemsDownloaded;

        //            transactions.Add( transaction );
        //        }

        //        UpdateProgress( numItemsDownloaded, numItemsToSync, SyncType.Download );

        //        if( transactions.Count >= _commitBatchSize || numItemsToSync == numItemsDownloaded )
        //        {
        //            bool success;
        //            ProcessTransactions( transactions, initialSync, out success );
        //            if( success == false )
        //                isMergeSuccessful = false;
        //        }

        //        if( numItemsToSync != numItemsDownloaded )
        //        {
        //            _syncContext.PageIndex++;
        //            dataList = getData( _syncContext );
        //        }
        //        else
        //        {
        //            dataList.Clear( );
        //        }
        //    }

        //    if( transactions.Count > 0 )
        //    {
        //        try
        //        {
        //            bool success;
        //            ProcessTransactions( transactions, initialSync, out success );
        //            if( success == false )
        //                isMergeSuccessful = false;
        //        }
        //        finally
        //        {
        //            transactions.Clear( );
        //        }
        //    }

        //    _syncContext.PageIndex = 0;

        //    // if database updated successfully, update the SyncMetadata table
        //    if( isMergeSuccessful )
        //    {
        //        if( !initialSync )
        //            InvokeDataChanged( );

        //        return numItemsDownloaded;
        //    }

        //    return -1;
        //}

        //----------------------------------------------------------------------
        /// <summary>
        /// Fires an event that the sync progress has changed.
        /// </summary>
        protected void UpdateProgress( int numItemsCompleted, int totalItems, int syncType )
        {
            if( ProgressChanged == null )
                return;

            int numItemsSinceLastProgress = numItemsCompleted - _prevNumItemsCompleted;
            ProgressChanged( this, new AdapterProgressChangedEventArgs( syncType, _dataTag, numItemsSinceLastProgress, numItemsCompleted, totalItems, String.Empty ) );
            _prevNumItemsCompleted = numItemsCompleted;
        }

        //----------------------------------------------------------------------
        protected virtual bool CheckXRefChanged( T originalItem, string newXRefID )
        {
            return false;
        }

        //----------------------------------------------------------------------
        protected void InvokeXRefChanged( T item, string newXRefID )
        {
            if( ItemXRefChanged == null )
                return;

            ItemXRefChanged( this, new ItemXRefChangedEventArgs( _dataTag, new Guid( item.PrimaryKey.ToString( ) ), newXRefID ) );
        }

        //----------------------------------------------------------------------
        protected void InvokeDataChanged( )
        {
            if( DataChanged == null )
                return;

            DataChanged( this, new DataChangedEventArgs( _dataTag ) );
        }

        //----------------------------------------------------------------------
        protected void ReportError( string errorMessage )
        {
            if( ErrorReported == null )
                return;

            ErrorReported( this, new ErrorReportedEventArgs( errorMessage ) );
        }

        //----------------------------------------------------------------------
//        protected List<TransactionResult> ProcessTransactions( List<ITransaction> transactions, bool initialSync, out bool success )
//        {
//            List<TransactionResult> results;

//            if( initialSync )
//            {
//#if PocketPC
//                results = _dataManager.PutResultSet(transactions, out success);
//#else
//                results = new List<TransactionResult>( );
//                results.Add( _dataManager.DoBulkCopy( transactions ) );
//                success = results.Count == 0 ? false : results[ 0 ].Success;
//#endif

//                if( success == false )
//                {
//                    foreach( TransactionResult result in results )
//                    {
//                        if( result.Success ) continue;

//                        if( !String.IsNullOrEmpty( result.Message ) )
//                            ReportError( String.Format( "Failed to sync {0} with message {1}.", result.TransactionInfo, result.Message ) );
//                    }
//                }
//            }
//            else
//            {
//                results = _dataManager.PutDataBatch( transactions );

//                //determines if the PutDataBatch was a success
//                success = true;
//                foreach( TransactionResult result in results )
//                {
//                    if( result.Success ) continue;

//                    success = false;
//                    if( !String.IsNullOrEmpty( result.Message ) )
//                    {
//                        ReportError( String.Format( "Failed to sync {0} with message {1}.", result.TransactionInfo, result.Message ) );
//                        if( !String.IsNullOrEmpty( result.TransactionInfo ) )
//                            ReportError( String.Format( "Transaction Info: '{0}.'", result.TransactionInfo ) );
//                    }
//                }
//            }

//            transactions.Clear( );
//            return results;
//        }

        //----------------------------------------------------------------------
        protected bool RowExists( string table, string idColumn, object primaryKey )
        {
            CountTransaction countTrans = new CountTransaction( table, idColumn, String.Format( "WHERE {0} = '{1}'", idColumn, primaryKey ) );
            int count = _dataManager.GetFirstDataResult( countTrans );
            return count > 0;
        }
    }
}
