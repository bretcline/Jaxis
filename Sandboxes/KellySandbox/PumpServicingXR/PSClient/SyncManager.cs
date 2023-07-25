using System;
using System.Linq;
using System.Collections.Generic;
using WFT.PS.Shared;
using LFI.Sync.DataManager;
using LFI.Sync.Shared;
using WFT.PSService.ServiceLibrary;
using WFT.PSService.Proxy;
using System.Data;
using JaxisForeignKeyHelpers;

namespace PSClient
{
    // IMPORTANT: When the service references are updated, the regex below is used to manually
    // replace array usages in Reference.cs with Lists. Regenerated web references won't work
    // without doing a find and replace with the parameters below.
    // FIND: ~(object){<[A-Za-z]+}\[\]
    // REPLACE: List<\1>
    public sealed class SyncManager
    {
        #region member variables
        private IRecordAccessor recordAccessor;

        public const int MAX_NUM_TO_PROCESS = 500;
        public static readonly DateTime MIN_DATE = new DateTime( 1970, 1, 1 );
        private readonly DataManager dataManager;
        private int completedItemCount;
        private int totalItemCount;
        //private Guid _activeWSMDataSource;

        private ISyncAdapterFactory _syncAdapterFactory;
        private ISyncTransactionFactory _syncTransactionFactory;

        /// <summary>
        /// DataTagID is the key and SyncTrackingID is the value.
        /// </summary>
        private Dictionary<DataTag, int> _syncItems;

        public Dictionary<DataTag, SyncTracking> DownSyncTracking;
        public Dictionary<DataTag, SyncTracking> UpSyncTracking;
        public Dictionary<DataTag, SyncTracking> ResolveSyncTracking;

        private Dictionary<string, int> m_DataTags;
        public Dictionary<string, int> DataTags
        {
            get
            {
                if( null == this.m_DataTags )
                {
                    this.m_DataTags = new Dictionary<string, int>( );
                    foreach ( string dataTag in Enum.GetNames( typeof( DataTag ) ) )
                    {
                        if ( dataTag != "None" )
                        {
                            m_DataTags.Add( dataTag, 0 );
                        }
                    }

                    ForeignKeyHelper helper = new ForeignKeyHelper( );
                    List<ForeignKeyRelationship> FKRs = helper.GetRelationshipsInOrder( );
                    foreach ( ForeignKeyRelationship FKR in FKRs )
                    {
                        if ( m_DataTags.ContainsKey( FKR.TableName ) )
                        {
                            m_DataTags[ FKR.TableName ] = FKR.Level;
                        }
                    }
                }
                return this.m_DataTags;
            }
        }

        /// <summary>
        /// Gets the last synchronization date for JobInfo items.
        /// </summary>
        public DateTime LastSyncDate
        {
            get
            {
                //return DownSyncTracking.ContainsKey( DataTag.JobInfo ) ? DownSyncTracking[ DataTag.JobInfo ].LastSync : new DateTime( 1990, 1, 1 );
                return new DateTime( 1990, 1, 1 );
            }
        }

        #endregion

        #region event handlers

        /// <summary>
        /// Fired when an new item created on the client updates its wsm xref key from the server
        /// </summary>
        public event EventHandler<ItemXRefChangedEventArgs> ItemXRefChanged;

        /// <summary>
        /// Fired when a data type recieves new or changed data from WSM
        /// </summary>
        public event EventHandler<DataChangedEventArgs> DataChanged;

        public event EventHandler<SyncProgressChangedEventArgs> SyncProgressChanged;
        public event EventHandler<ErrorReportedEventArgs> SyncErrorReported;

        #endregion

        #region properties

        public bool HasSyncErrors { get { return SyncErrors != null && SyncErrors.Count > 0; } }
        public IList<string> SyncErrors { get; private set; }

        #endregion

        //----------------------------------------------------------------------
        public SyncManager( string appServerUrl, DataManager dataManager, ISyncTransactionFactory transactionFactory )
            : this( appServerUrl, dataManager, transactionFactory, Guid.Empty )
        {
        }

        //----------------------------------------------------------------------
        public SyncManager( string appServerUrl, DataManager dataManager, ISyncTransactionFactory transactionFactory, Guid wsmDataSourceID )
        {
            this.dataManager = dataManager;
            _syncAdapterFactory = new SyncAdapterFactory( );
            _syncTransactionFactory = transactionFactory;

            SyncErrors = new List<string>( );
            //InitSyncTracking( wsmDataSourceID );
            InitSyncTracking( );

            Configure( appServerUrl );
        }

        //----------------------------------------------------------------------
        public void Configure( string appServerUrl )
        {
            recordAccessor = ServiceFactory.GetRecordAccessor( appServerUrl );
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Registers items to be sync'd along with SyncType flags to determine how they sync.
        /// Items should be registered in order of dependency (dependencies first).
        /// </summary>
        /// <param name="tag">DataTag of the sync item</param>
        /// <param name="syncFlags">SyncType flags (i.e. upload | download | resolve | refdata)</param>
        public void RegisterSyncItem( DataTag tag, int syncFlags )
        {
            if( _syncItems == null )
                _syncItems = new Dictionary<DataTag, int>( );

            if( !_syncItems.ContainsKey( tag ) )
                _syncItems.Add( tag, syncFlags );
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Optional method to register a new sync adapter factory if default factory is insufficient
        /// </summary>
        public void RegisterSyncAdapterFactory( ISyncAdapterFactory factory )
        {
            _syncAdapterFactory = factory;
        }

        //----------------------------------------------------------------------
        //public void InitSyncTracking( Guid wsmDataSource )
        public void InitSyncTracking( )
        {
            //_activeWSMDataSource = wsmDataSource;

            //SyncTrackingTransaction trans = new SyncTrackingTransaction( _activeWSMDataSource );
            SyncTrackingTransaction trans = new SyncTrackingTransaction( );
            List<SyncTracking> syncTracking = dataManager.GetData( trans );

            //DownSyncTracking = new Dictionary<DataTag, SyncTracking>( );
            UpSyncTracking = new Dictionary<DataTag, SyncTracking>( );
            //ResolveSyncTracking = new Dictionary<DataTag, SyncTracking>( );

            // go through syncTracking results and handle the items we have sync tracking data for
            foreach( SyncTracking syncItem in syncTracking )
            {
                DataTag tag = DataTagMapping.DataTagsByID[ syncItem.DataTagID ];

                if( _syncItems != null && !_syncItems.ContainsKey( tag ) )
                    continue;

                //if( ( syncItem.SyncType & SyncType.Download ) == SyncType.Download )
                //    DownSyncTracking.Add( tag, syncItem );
                //else if( ( syncItem.SyncType & SyncType.Resolve ) == SyncType.Resolve )
                //    ResolveSyncTracking.Add( tag, syncItem );
                //else
                UpSyncTracking.Add( tag, syncItem );
            }

            if( _syncItems == null )
                return;

            // go through syncitems and create new tracking for items not found in the database
            foreach( KeyValuePair<DataTag, int> pair in _syncItems )
            {
                //if( !DownSyncTracking.ContainsKey( pair.Key ) && ( pair.Value & SyncType.Download ) == SyncType.Download )
                //    DownSyncTracking.Add( pair.Key, CreateSyncItem( pair.Key, pair.Value ) );
                if ( ( pair.Value & SyncType.Upload ) == SyncType.Upload && !UpSyncTracking.ContainsKey( pair.Key ) )
                    UpSyncTracking.Add( pair.Key, CreateSyncItem( pair.Key, pair.Value ) );
                //if( ( pair.Value & SyncType.Resolve ) == SyncType.Resolve && !ResolveSyncTracking.ContainsKey( pair.Key ) )
                //    ResolveSyncTracking.Add( pair.Key, CreateSyncItem( pair.Key, pair.Value ) );
            }
        }

        //----------------------------------------------------------------------
        private SyncTracking CreateSyncItem( DataTag tag, int syncType )
        {
            SyncTracking syncItem = new SyncTracking( );
            syncItem.DataTagID = DataTagMapping.DataTagIDs[ tag ];
            syncItem.LastSync = new DateTime( 1970, 1, 1 );
            syncItem.SyncType = syncType;
            syncItem.DataTag = tag.ToString( );

            //if( !( ( syncType & SyncType.RefData ) == SyncType.RefData ) && _activeWSMDataSource != Guid.Empty )
            //    syncItem.DataSourceID = _activeWSMDataSource;

            return syncItem;
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Does a sync of specific SyncType flags only
        /// </summary>
        /// <param name="context"></param>
        /// <param name="syncFlags">SyncType flags (i.e. SyncType.Upload | SyncType.Download)</param>
        public void Sync( SyncContext context, int syncFlags )
        {
            // reset sync errors
            SyncErrors.Clear( );

            // resolve deleted data, prior to uploading and downloading
            //if( ( syncFlags & SyncType.Resolve ) == SyncType.Resolve )
            //    Resolve( context );

            bool uploadSucceeded = true;
            if( ( syncFlags & SyncType.Upload ) == SyncType.Upload )
                uploadSucceeded = Upload( context );

            // Per ML - We don't want to download unless we successfully uploaded our changes
            //if( ( syncFlags & SyncType.Download ) == SyncType.Download && uploadSucceeded )
            //    Download( context );
        }

        //---------------------------------------------------------------------
        public void Sync( SyncContext context )
        {
            // reset sync errors
            SyncErrors.Clear( );

            // Must resolve deleted data, prior to uploading and downloading
            //Resolve( context );

            // Per ML - We don't want to download unless we successfully uploaded our changes
            //if( Upload( context ) )
            //    Download( context );

            Upload( context );
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Resolves the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        //private void Resolve( SyncContext context )
        //{
            //UpdateSyncStatus( SyncType.Resolve, DataTag.None, 0, 100 );

            //InvokeAdapterResolve<EventInfo>( DataTag.EventInfo, context, jobAccessor.GetDeletedEvents );

            //UpdateSyncStatus( SyncType.Resolve, DataTag.None, 50, 100 );

            //InvokeAdapterResolve<JobInfo>( DataTag.JobInfo, context, jobAccessor.GetDeletedJobs );

            //UpdateSyncStatus( SyncType.Resolve, DataTag.None, 100, 100 );

        //    return;
        //}

        //----------------------------------------------------------------------
        public bool Upload( SyncContext context )
        {
            bool rc = true;

            List<string> tables = new List<string>(
                from d in DataTags.OrderBy( keySelector => keySelector.Value )
                select d.Key );

            foreach ( string s in tables )
            {
                switch ( s )
                {
                    case "Assembly":
                        rc = InvokeAdapterUpload<Assembly>( DataTag.Assembly, context, this.recordAccessor.PutAssembly );
                        break;
                    case "AssemblyComponent":
                        rc = InvokeAdapterUpload<AssemblyComponent>( DataTag.AssemblyComponent, context, this.recordAccessor.PutAssemblyComponent );
                        break;
                    case "AssemblyComponentBblPlgMeasure":
                        rc = InvokeAdapterUpload<AssemblyComponentBblPlgMeasure>( DataTag.AssemblyComponentBblPlgMeasure, context, this.recordAccessor.PutAssemblyComponentBblPlgMeasure );
                        break;
                    case "AssemblyComponentSRPump":
                        rc = InvokeAdapterUpload<AssemblyComponentSRPump>( DataTag.AssemblyComponentSRPump, context, this.recordAccessor.PutAssemblyComponentSRPump );
                        break;
                    case "BusinessOrganization":
                        rc = InvokeAdapterUpload<BusinessOrganization>( DataTag.BusinessOrganization, context, this.recordAccessor.PutBusinessOrganization );
                        break;
                    case "Component":
                        rc = InvokeAdapterUpload<Component>( DataTag.Component, context, this.recordAccessor.PutComponent );
                        break;
                    case "ComponentSRPump":
                        rc = InvokeAdapterUpload<ComponentSRPump>( DataTag.ComponentSRPump, context, this.recordAccessor.PutComponentSRPump );
                        break;
                    case "DatabaseConfiguration":
                        rc = InvokeAdapterUpload<DatabaseConfiguration>( DataTag.DatabaseConfiguration, context, this.recordAccessor.PutDatabaseConfiguration );
                        break;
                    case "DeletedLog":
                        rc = InvokeAdapterUpload<DeletedLog>( DataTag.DeletedLog, context, this.recordAccessor.PutDeletedLog );
                        break;
                    case "Document":
                        rc = InvokeAdapterUpload<Document>( DataTag.Document, context, this.recordAccessor.PutDocument );
                        break;
                    case "Event":
                        rc = InvokeAdapterUpload<Event>( DataTag.Event, context, this.recordAccessor.PutEvent );
                        break;
                    case "EventAssembleSRPump":
                        rc = InvokeAdapterUpload<EventAssembleSRPump>( DataTag.EventAssembleSRPump, context, this.recordAccessor.PutEventAssembleSRPump );
                        break;
                    case "EventComponentFailure":
                        rc = InvokeAdapterUpload<EventComponentFailure>( DataTag.EventComponentFailure, context, this.recordAccessor.PutEventComponentFailure );
                        break;
                    case "EventDetailCosts":
                        rc = InvokeAdapterUpload<EventDetailCosts>( DataTag.EventDetailCosts, context, this.recordAccessor.PutEventDetailCosts );
                        break;
                    case "EventDisassembleSRPump":
                        rc = InvokeAdapterUpload<EventDisassembleSRPump>( DataTag.EventDisassembleSRPump, context, this.recordAccessor.PutEventDisassembleSRPump );
                        break;
                    case "EventInstallPump":
                        rc = InvokeAdapterUpload<EventInstallPump>( DataTag.EventInstallPump, context, this.recordAccessor.PutEventInstallPump );
                        break;
                    case "EventPullPump":
                        rc = InvokeAdapterUpload<EventPullPump>( DataTag.EventPullPump, context, this.recordAccessor.PutEventPullPump );
                        break;
                    case "Facility":
                        rc = InvokeAdapterUpload<Facility>( DataTag.Facility, context, this.recordAccessor.PutFacility );
                        break;
                    case "Invoice":
                        rc = InvokeAdapterUpload<Invoice>( DataTag.Invoice, context, this.recordAccessor.PutInvoice );
                        break;
                    case "Job":
                        rc = InvokeAdapterUpload<Job>( DataTag.Job, context, this.recordAccessor.PutJob );
                        break;
                    case "JobStatusChangeLog":
                        rc = InvokeAdapterUpload<JobStatusChangeLog>( DataTag.JobStatusChangeLog, context, this.recordAccessor.PutJobStatusChangeLog );
                        break;
                    case "Lease":
                        rc = InvokeAdapterUpload<Lease>( DataTag.Lease, context, this.recordAccessor.PutLease );
                        break;
                    case "Owner":
                        rc = InvokeAdapterUpload<Owner>( DataTag.Owner, context, this.recordAccessor.PutOwner );
                        break;
                    case "StickyNotes":
                        rc = InvokeAdapterUpload<StickyNotes>( DataTag.StickyNotes, context, this.recordAccessor.PutStickyNotes );
                        break;
                    case "TemplatePump":
                        rc = InvokeAdapterUpload<TemplatePump>( DataTag.TemplatePump, context, this.recordAccessor.PutTemplatePump );
                        break;
                    case "TemplatePumpDetail":
                        rc = InvokeAdapterUpload<TemplatePumpDetail>( DataTag.TemplatePumpDetail, context, this.recordAccessor.PutTemplatePumpDetail );
                        break;
                    case "TemplateSubAssembly":
                        rc = InvokeAdapterUpload<TemplateSubAssembly>( DataTag.TemplateSubAssembly, context, this.recordAccessor.PutTemplateSubAssembly );
                        break;
                    case "TemplateSubAssemblyDetail":
                        rc = InvokeAdapterUpload<TemplateSubAssemblyDetail>( DataTag.TemplateSubAssemblyDetail, context, this.recordAccessor.PutTemplateSubAssemblyDetail );
                        break;
                    case "UserMaster":
                        rc = InvokeAdapterUpload<UserMaster>( DataTag.UserMaster, context, this.recordAccessor.PutUserMaster );
                        break;
                    case "Well":
                        rc = InvokeAdapterUpload<Well>( DataTag.Well, context, this.recordAccessor.PutWell );
                        break;
                    case "WellCompletionReservoirs":
                        rc = InvokeAdapterUpload<WellCompletionReservoirs>( DataTag.WellCompletionReservoirs, context, this.recordAccessor.PutWellCompletionReservoirs );
                        break;
                    case "WellCompletionXRef":
                        rc = InvokeAdapterUpload<WellCompletionXRef>( DataTag.WellCompletionXRef, context, this.recordAccessor.PutWellCompletionXRef );
                        break;
                    case "Workorder":
                        rc = InvokeAdapterUpload<Workorder>( DataTag.Workorder, context, this.recordAccessor.PutWorkorder );
                        break;
                    case "WorkorderStatusHistory":
                        rc = InvokeAdapterUpload<WorkorderStatusHistory>( DataTag.WorkorderStatusHistory, context, this.recordAccessor.PutWorkorderStatusHistory );
                        break;
                    case "WorkorderSubAssemblies":
                        rc = InvokeAdapterUpload<WorkorderSubAssemblies>( DataTag.WorkorderSubAssemblies, context, this.recordAccessor.PutWorkorderSubAssemblies );
                        break;
                    case "WorkorderSubAssembliesStatusHistory":
                        rc = InvokeAdapterUpload<WorkorderSubAssembliesStatusHistory>( DataTag.WorkorderSubAssembliesStatusHistory, context, this.recordAccessor.PutWorkorderSubAssembliesStatusHistory );
                        break;
                }
                if ( !rc )
                {
                    break;
                }
            }
            return rc;

            //UpdateSyncStatus( SyncType.Upload, DataTag.None, 0, 100 );
            
            //if( !InvokeAdapterUpload<BusinessOrganization>( DataTag.BusinessOrganization, context, this.recordAccessor.PutBusinessOrganization ) )
            //    return false;

            //if ( !InvokeAdapterUpload<Well>( DataTag.Well, context, this.recordAccessor.PutWell ) )
            //    return false;

            //if( !InvokeAdapterUpload<JobInfo>( DataTag.JobInfo, context, jobAccessor.PutJob ) )
            //    return false;

            //UpdateSyncStatus( SyncType.Upload, DataTag.None, 50, 100 );

            //if( !InvokeAdapterUpload<EventInfo>( DataTag.EventInfo, context, jobAccessor.PutEvent ) )
            //    return false;

            //UpdateSyncStatus( SyncType.Upload, DataTag.None, 75, 100 );

            //if( !InvokeAdapterUpload<EventTypeDataValue>( DataTag.EventTypeDataValue, context, jobAccessor.PutExtendedEventData ) )
            //    return false;

            //UpdateSyncStatus( SyncType.Upload, DataTag.None, 100, 100 );

            //return true;
        }

        //----------------------------------------------------------------------
        private bool InvokeAdapterUpload<T>( DataTag tag, SyncContext context, Func<SyncContext, List<T>, List<ServiceResult>> uploadToServer ) where T : IBaseData
        {
            bool success = true;
            IUploadAdapter<T> adapter = _syncAdapterFactory.GetSyncAdapter<T>( tag, dataManager, context, _syncTransactionFactory ) as IUploadAdapter<T>;
            if( adapter == null )
                return true;

            if( !UpSyncTracking.ContainsKey( tag ) )
                return true;

            SyncTracking tracking = UpSyncTracking[ tag ];

            // non ref-data must have a data source
            if( !( ( tracking.SyncType & SyncType.RefData ) == SyncType.RefData ) && tracking.DataSourceID == null )
                return true;

            RegisterAdapter( adapter );

            DateTime uploadTime = DateTime.Now.ToUniversalTime( );
            int uploadedItemCount = adapter.Upload( uploadToServer, UpSyncTracking[ tag ].LastSync );

            // update the last successful sync date
            if( uploadedItemCount != -1 )
            {
                UpdateSyncTracking( tag, SyncType.Upload, uploadTime );
                LogHelper.ForceLog( String.Format( "UPLOAD - {0} {1} item(s).", uploadedItemCount, tag ) );
            }
            else
                success = false;

            UnregisterAdapter( adapter );

            return success;
        }

        //----------------------------------------------------------------------
        private bool InvokeAdapterUpload<T>( DataTag tag, SyncContext context, Func<SyncContext, T, ServiceResult> uploadToServer ) where T : IBaseData
        {
            bool success = true;
            IUploadAdapter<T> adapter = _syncAdapterFactory.GetSyncAdapter<T>( tag, dataManager, context, _syncTransactionFactory ) as IUploadAdapter<T>;
            if( adapter == null )
                return true;

            if( !UpSyncTracking.ContainsKey( tag ) )
                return true;

            SyncTracking tracking = UpSyncTracking[ tag ];

            // non ref-data must have a data source
            //if( !( ( tracking.SyncType & SyncType.RefData ) == SyncType.RefData ) && tracking.DataSourceID == null )
            //    return true;

            RegisterAdapter( adapter );

            DateTime uploadTime = DateTime.Now.ToUniversalTime( );
            int uploadedItemCount = adapter.Upload( uploadToServer, UpSyncTracking[ tag ].LastSync );

            // update the last successful sync date
            if( uploadedItemCount != -1 )
            {
                UpdateSyncTracking( tag, SyncType.Upload, uploadTime );
                LogHelper.ForceLog( String.Format( "UPLOAD - {0} {1} item(s).", uploadedItemCount, tag ) );
            }
            else
                success = false;

            UnregisterAdapter( adapter );

            return success;
        }

        //----------------------------------------------------------------------
        //private void InvokeAdapterResolve<T>( DataTag tag, SyncContext context, Func<SyncContext, IList<T>> getData ) where T : IBaseData
        //{
        //    IResolveAdapter<T> adapter = _syncAdapterFactory.GetSyncAdapter<T>( tag, dataManager, context, _syncTransactionFactory ) as IResolveAdapter<T>;
        //    if( adapter == null )
        //        return;

        //    if( !ResolveSyncTracking.ContainsKey( tag ) )
        //        return;

        //    SyncTracking tracking = ResolveSyncTracking[ tag ];

        //    // non ref-data must have a data source
        //    if( !( ( tracking.SyncType & SyncType.RefData ) == SyncType.RefData ) && tracking.DataSourceID == null )
        //        return;

        //    RegisterAdapter( adapter );

        //    DateTime resolveTime = DateTime.Now.ToUniversalTime( );
        //    int resolveCount = adapter.Resolve( getData, ResolveSyncTracking[ tag ].LastSync );

        //    if( resolveCount != -1 )
        //        UpdateSyncTracking( tag, SyncType.Resolve, resolveTime );

        //    UnregisterAdapter( adapter );
        //}

        //----------------------------------------------------------------------
        /// <summary>
        /// Gets the total item count.
        /// </summary>
        /// <param name="changeList">The change list.</param>
        /// <returns></returns>
        //private static int GetTotalItemCount( IEnumerable<ItemCount> changeList )
        //{
        //    int totalCount = 0;
        //    foreach( ItemCount changeItem in changeList )
        //    {
        //        totalCount += changeItem.Count;
        //    }

        //    return totalCount;
        //}

        //----------------------------------------------------------------------
        //public void Download( SyncContext context )
        //{
        //    //UpdateSyncStatus( SyncType.Download, DataTag.None, 0, 100 );

            //// build itemized change list request
            //List<ItemModified> requestItems = new List<ItemModified>( );
            //foreach( KeyValuePair<DataTag, SyncTracking> pair in DownSyncTracking )
            //{
            //    ItemModified requestItem = new ItemModified( );
            //    requestItem.DataTagID = DataTagMapping.DataTagIDs[ pair.Key ].ToString( );
            //    requestItem.LastModified = pair.Value.LastSync;
            //    requestItems.Add( requestItem );
            //}

            //// get change list from server
            //DateTime changeListTime = DateTime.Now.ToUniversalTime( );
            //List<ItemCount> changeList = serverAccessor.GetItemizedChangeList( context, requestItems );

            //totalItemCount = GetTotalItemCount( changeList );
            //completedItemCount = 0;

            //// request download for each change item
            //foreach( ItemCount changeItem in changeList )
            //{
            //    DataTag tag = DataTagMapping.DataTagsByID[ new Guid( changeItem.DataTagID ) ];
            //    if( changeItem.Count == 0 )
            //    {
            //        SyncTracking item = DownSyncTracking[ tag ];
            //        if( !( ( item.SyncType & SyncType.RefData ) == SyncType.RefData ) && item.DataSourceID == null )
            //            continue;

            //        UpdateSyncTracking( tag, SyncType.Download, changeListTime );
            //        continue;
            //    }

            //    switch( tag )
            //    {
            //        case DataTag.AssetGroupInfo:
            //            InvokeAdapterDownload<AssetGroupInfo>( tag, context, assetAccessor.GetAssetGroups, changeItem.Count );
            //            break;
            //        default:
            //            break;
            //    }
            //}

        //    UpdateSyncStatus( SyncType.Download, DataTag.None, 100, 100 );
        //}

        //----------------------------------------------------------------------
        //private void InvokeAdapterDownload<T>( DataTag tag, SyncContext context, Func<SyncContext, IList<T>> getData, int numItemsToSync ) where T : IBaseData
        //{
        //    IDownloadAdapter<T> adapter = _syncAdapterFactory.GetSyncAdapter<T>( tag, dataManager, context, _syncTransactionFactory ) as IDownloadAdapter<T>;
        //    if( adapter == null )
        //        return;

        //    if( !DownSyncTracking.ContainsKey( tag ) )
        //        return;

        //    SyncTracking tracking = DownSyncTracking[ tag ];

        //    // non ref-data must have a data source
        //    if( !( ( tracking.SyncType & SyncType.RefData ) == SyncType.RefData ) && tracking.DataSourceID == null )
        //        return;

        //    RegisterAdapter( adapter );

        //    DateTime syncStartTime = DateTime.Now.ToUniversalTime( );
        //    int numItemsDownloaded = adapter.Download( getData, numItemsToSync, DownSyncTracking[ tag ].LastSync );

        //    if( numItemsDownloaded != -1 )
        //    {
        //        UpdateSyncTracking( tag, SyncType.Download, syncStartTime );
        //        LogHelper.ForceLog( String.Format( "DOWNLOAD - {0} {1} item(s).", numItemsDownloaded, tag ) );
        //    }

        //    UnregisterAdapter( adapter );
        //}

        //----------------------------------------------------------------------
        private void OnAdapterDataChanged( object sender, DataChangedEventArgs e )
        {
            if( DataChanged != null )
                DataChanged( sender, e );
        }

        //----------------------------------------------------------------------
        private void OnAdapterIDChanged( object sender, ItemXRefChangedEventArgs e )
        {
            if( ItemXRefChanged != null )
                ItemXRefChanged( sender, e );
        }

        //----------------------------------------------------------------------
        private void OnAdapterProgressChanged( object sender, AdapterProgressChangedEventArgs e )
        {
            completedItemCount += e.NumSinceLastProgress;
            UpdateSyncStatus( e.Type, e.Tag, e.NumItemsComplete, e.TotalItems );
        }

        //----------------------------------------------------------------------
        private void OnAdapterErrorReported( object sender, ErrorReportedEventArgs e )
        {
            ReportError( e.Error );
        }

        //----------------------------------------------------------------------
        private void ReportError( string error )
        {
            SyncErrors.Add( error );
            if( SyncErrorReported != null )
                SyncErrorReported( this, new ErrorReportedEventArgs( error ) );
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Updates the mobile SyncMetadata table with the current datetime as the last successful sync.
        /// </summary>
        /// <param name="dataTag">determines the row to update in the synctracking table.</param>
        /// <param name="syncType">type of sync, either upload or download</param>
        /// <param name="updateTime">time to update synctracking LastSuccessfulSync field</param>
        private void UpdateSyncTracking( DataTag dataTag, int syncType, DateTime updateTime )
        {
            SyncTracking syncTracking;
            if( ( syncType & SyncType.Upload ) == SyncType.Upload )
                syncTracking = UpSyncTracking[ dataTag ];
            else if( ( syncType & SyncType.Resolve ) == SyncType.Resolve )
                syncTracking = ResolveSyncTracking[ dataTag ];
            else if( ( syncType & SyncType.Download ) == SyncType.Download )
                syncTracking = DownSyncTracking[ dataTag ];
            else
                return;

            // if this is a ref data item, it is shared data between all data sources
            int syncTypeToSave = syncType;
            //if( ( _syncItems[ dataTag ] & SyncType.RefData ) == SyncType.RefData )
            //{
            //    syncTracking.DataSourceID = null;
            //    syncTypeToSave |= SyncType.RefData;
            //}
            //else if( _activeWSMDataSource != Guid.Empty )
            //    syncTracking.DataSourceID = _activeWSMDataSource;

            syncTracking.LastSync = updateTime;

            SyncTrackingTransaction trans = new SyncTrackingTransaction( syncTracking, syncTypeToSave );
            TransactionResult result = dataManager.PutData( trans );

            if( result.Success )
            {
                if( syncTracking.SyncTrackingID == null )
                    syncTracking.SyncTrackingID = new Guid( result.PrimaryKey );
            }
            else
            {
                ReportError( String.Format( "Failed to update sync tracking for item '{0}' with message '{1}.", dataTag, result.Message ) );
                if( !String.IsNullOrEmpty( result.TransactionInfo ) )
                    ReportError( String.Format( "Transaction Info: '{0}.'", result.TransactionInfo ) );
            }
        }

        //----------------------------------------------------------------------
        public void UpdateSyncStatus( int type, DataTag tag, int currentCount, int totalCount )
        {
            // We do not have totalItemCount...
            //int progress = ( int ) Math.Round( ( 100.0 * completedItemCount ) / totalItemCount );
            //SyncProgressChangedEventArgs syncProgressChangedEventArgs = new SyncProgressChangedEventArgs( type, tag, currentCount, totalCount, progress );
            //if( SyncProgressChanged != null )
            //    SyncProgressChanged( this, syncProgressChangedEventArgs );
        }

        //----------------------------------------------------------------------
        public bool IsInitialSync( )
        {
            foreach( KeyValuePair<DataTag, SyncTracking> pair in DownSyncTracking )
            {
                if( pair.Value.LastSync == MIN_DATE )
                {
                    DataTag tag = DataTagMapping.DataTagsByID[ pair.Value.DataTagID ];
                    LogHelper.ForceLog( String.Format( "This is an initial sync because section {0} has not been synchronized", DataTagMapping.DataTagTables[ tag ] ) );
                    return true;
                }
            }

            return false;
        }

        //----------------------------------------------------------------------
        public bool HasUnSychronizedData( )
        {
            foreach( KeyValuePair<DataTag, SyncTracking> pair in UpSyncTracking )
            {
                if( HasUnSychronizedData( pair.Key ) )
                    return true;
            }

            return false;
        }

        //----------------------------------------------------------------------
        public bool HasUnSychronizedData( DataTag objectTag )
        {
            const string whereFormatSql = "WHERE LastModified > '{0}' AND HasLocalChange = 1";
            SyncTracking syncTracking = UpSyncTracking[ objectTag ];
            CountTransaction countTrans = new CountTransaction( DataTagMapping.DataTagTables[ objectTag ], String.Format( whereFormatSql, syncTracking.LastSync ) );
            int count = dataManager.GetFirstDataResult( countTrans );

            if( count > 0 )
            {
                ReportError( String.Format( "{0} contains unsynchronized data", objectTag ) );
                return true;
            }

            return false;
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Clears itemized, non-reference data from partial initial sync. Leave successfully completed itemized syncs alone
        /// </summary>
        public void ClearIncompleteSyncData( )
        {
            foreach( KeyValuePair<DataTag, int> pair in _syncItems.Reverse( ) )
            {
                if( DownSyncTracking[ pair.Key ].LastSync == MIN_DATE )
                {
                    if( ( pair.Value & SyncType.RefData ) == SyncType.RefData )
                        DeleteTableData( DataTagMapping.DataTagTables[ pair.Key ] );
                    else
                        DeleteTableDataByActiveDataSource( DataTagMapping.DataTagTables[ pair.Key ] );
                }
            }
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Clears all sync data
        /// </summary>
        public void ClearAllSyncData( )
        {
            foreach( KeyValuePair<DataTag, SyncTracking> pair in DownSyncTracking.Reverse( ) )
            {
                DeleteSyncData( pair.Key );
            }
        }

        //----------------------------------------------------------------------
        public bool DeleteSyncDataByActiveDataSource( DataTag dataTag )
        {
            if( !DataTagMapping.DataTagTables.ContainsKey( dataTag ) )
                return false;

            bool result = DeleteTableDataByActiveDataSource( DataTagMapping.DataTagTables[ dataTag ] );
            ResetSyncTrackingByActiveDataSource( dataTag );
            return result;
        }

        //----------------------------------------------------------------------
        private bool DeleteTableDataByActiveDataSource( string tableName )
        {
#warning STUB
            return true;
            //try
        //    {
        //        const string deleteFormatStr = "DELETE FROM {0} WHERE SourceDB = '{1}'";

        //        TransactionResult result = dataManager.InvokeRawNonQuery( String.Format( deleteFormatStr, tableName, _activeWSMDataSource ) );
        //        if( result.Success == false )
        //        {
        //            string errorStr = String.Format( "Failed to delete table {0} with message {1}.", tableName, result.Message );
        //            ReportError( errorStr );
        //            throw new Exception( errorStr );
        //        }
        //    }
        //    catch
        //    {
        //    }

        //    return true;
        }

        //----------------------------------------------------------------------
        public bool DeleteSyncData( DataTag dataTag )
        {
            if( !DataTagMapping.DataTagTables.ContainsKey( dataTag ) )
                return false;

            bool result = DeleteTableData( DataTagMapping.DataTagTables[ dataTag ] );
            ResetSyncTracking( dataTag );
            return result;
        }

        //----------------------------------------------------------------------
        public void ClearErrors( )
        {
            SyncErrors.Clear( );
        }

        //----------------------------------------------------------------------
        private bool DeleteTableData( string tableName )
        {
            const string deleteFormatStr = "DELETE FROM {0}";
            TransactionResult result = dataManager.InvokeRawNonQuery( String.Format( deleteFormatStr, tableName ) );
            if( result.Success == false )
            {
                string errorStr = String.Format( "Failed to delete table {0} with message {1}.", tableName, result.Message );
                ReportError( errorStr );
                throw new Exception( errorStr );
            }

            return true;
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// resets tracking for a sync item for current data source
        /// </summary>
        private void ResetSyncTrackingByActiveDataSource( DataTag dataTag )
        {
            if( DownSyncTracking.ContainsKey( dataTag ) )
                DownSyncTracking[ dataTag ].LastSync = MIN_DATE;
            if( UpSyncTracking.ContainsKey( dataTag ) )
                UpSyncTracking[ dataTag ].LastSync = MIN_DATE;
            if( ResolveSyncTracking.ContainsKey( dataTag ) )
                ResolveSyncTracking[ dataTag ].LastSync = MIN_DATE;
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Resets sync tracking on a sync item for all data sources
        /// </summary>
        private void ResetSyncTracking( DataTag dataTag )
        {
            string resetTrackingSQL = String.Format( "UPDATE SyncTracking SET LastSuccessfulSync = '{0}' WHERE DataTagID = '{1}'", MIN_DATE, DataTagMapping.DataTagIDs[ dataTag ] );
            TransactionResult result = dataManager.InvokeRawNonQuery( resetTrackingSQL );
            if( result.Success )
                ResetSyncTrackingByActiveDataSource( dataTag );
            else
            {
                string errorStr = String.Format( "Failed to reset sync tracking for '{0}' with message {1}.", dataTag, result.Message );
                ReportError( errorStr );
                throw new Exception( errorStr );
            }
        }

        //----------------------------------------------------------------------
        private void RegisterAdapter<T>( ISyncAdapter<T> adapter ) where T : IBaseData
        {
            adapter.ProgressChanged += OnAdapterProgressChanged;
            adapter.ErrorReported += OnAdapterErrorReported;

            if( adapter is IDownloadAdapter<T> )
                ( ( IDownloadAdapter<T> ) adapter ).DataChanged += OnAdapterDataChanged;
            if( adapter is IUploadAdapter<T> )
                ( ( IUploadAdapter<T> ) adapter ).ItemXRefChanged += OnAdapterIDChanged;
        }

        //----------------------------------------------------------------------
        private void UnregisterAdapter<T>( ISyncAdapter<T> adapter ) where T : IBaseData
        {
            adapter.ProgressChanged -= OnAdapterProgressChanged;
            adapter.ErrorReported -= OnAdapterErrorReported;

            if( adapter is IDownloadAdapter<T> )
                ( ( IDownloadAdapter<T> ) adapter ).DataChanged -= OnAdapterDataChanged;
            if( adapter is IUploadAdapter<T> )
                ( ( IUploadAdapter<T> ) adapter ).ItemXRefChanged -= OnAdapterIDChanged;
        }
    }
}
