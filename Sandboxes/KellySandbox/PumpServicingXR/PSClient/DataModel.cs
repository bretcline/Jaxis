using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using log4net;
using LFI.Sync.DataManager;
using LFI.Sync.Shared;
using WFT.PS.Shared;
using WFT.PSService.ServiceLibrary;
using PSClient.Properties;
using System.Data.SqlClient;

namespace PSClient
{
    public class DataModel
    {
        #region member variables

        private ILog log = LogManager.GetLogger( "DataModel" );
        private const int DefaultPageSize = 100;
        private DataManager dataMgr;
        private System.Threading.Thread backgroundSyncThread;
        //private Guid activeWSM = Guid.Empty;
        //private Guid activeAppServerID = Guid.Empty;

        private bool waitingToSync = false;
        private bool completionsChanged = false;
        private System.Collections.Hashtable adHocEvents = new System.Collections.Hashtable( );

        public SyncManager SyncManager;

        public event EventHandler<SyncEventArgs> AsyncCompleted;
        public event EventHandler SyncFailed;
        public event EventHandler<SyncProgressReportEventArgs> SyncProgressReported;
        public event EventHandler Exiting;

        #endregion

        #region constants

        /// <summary>
        /// Gets or sets the default X ref ID.
        /// </summary>
        /// <value>The default X ref ID.</value>
        public const string DefaultXRefID = "CSI00000";

        private const string WHERE_JOBS_XREF_SQL = "WHERE Jobs.XRefID = '{0}'";

        #endregion

        #region properties

        public string Version { get; set; }
        public bool IsSyncing { get; set; }
        //public DeviceInfo Device { get; set; }
        //public IList<CompletionInfo> Completions { get; set; }
        //public System.Collections.Hashtable AdHocEvents { get { return adHocEvents; } }
        //public EventType CurrentEventType { get; set; }
        //public EventTypeGroupInfo CurrentEventTypeGroup { get; set; }
        //public CompletionLight CurrentCompletion { get; set; }
        //public List<JobInfo> CurrentCompletionJobs { get; set; }
        //public JobLight CurrentJob { get; set; }

        //public bool IsCostEvent
        //{
        //    get
        //    {
        //        if( SelectedEvent != null )
        //        {
        //            return SelectedEvent.EventType.XRefID.ToUpper( ) == "CSI0003U";
        //        }

        //        return false;
        //    }
        //}

        //public EventLight SelectedEvent { get; set; }
        //public ExtendedEventProvider ExtendedEventProvider { get; set; }
        //public EventLight InProgressEvent { get; set; }
        //public JobTypeInfo CurrentJobType { get; set; }
        //public JobReasonInfo CurrentJobReason { get; set; }
        //public AssetInfo CurrentTruckingUnit { get; set; }
        //public CompanyLight AssignedServiceProvider
        //{
        //    get
        //    {
        //        return GetAssignedServiceProvider( );
        //    }
        //}

        #endregion

        public DataModel( DataManager dataManager )
        {
            log.Debug( "Initializing DataModel" );
            dataMgr = dataManager;

            //if( PlatformManager.IsMobile )
            //{
            //    if( ApplicationSettings.BarcodeEnabled )
            //    {
            //        log.Debug( "Getting Available Barcode Devices" );
            //        Device[ ] devices = Symbol.Barcode.Device.AvailableDevices;
            //        Device barcodeDevice = null;
            //        if( log.IsDebugEnabled )
            //        {
            //            log.Debug( string.Format( "There are {0} devices available", devices == null ? 0 : devices.Length ) );
            //            if( devices != null )
            //            {
            //                int i = 0;
            //                foreach( Device dev in devices )
            //                {
            //                    log.Debug( string.Format( "Device {0} = {1}", i++, dev.FriendlyName ) );
            //                }
            //            }
            //        }

            //        if( devices != null )
            //            barcodeDevice = devices.FirstOrDefault( a => a.DeviceName == BarcodeScannerDeviceName );

            //        if( barcodeDevice != null )
            //        {
            //            log.Debug( string.Format( "Initializing barcode scanner as {0}", barcodeDevice.FriendlyName ) );
            //            barcodeReader = new Reader( barcodeDevice );
            //            barcodeReader.Actions.Enable( );
            //            barcodeReader.ReadNotify -= barcodeReader_ReadNotify;
            //            barcodeReader.ReadNotify += barcodeReader_ReadNotify;
            //            barcodeReader.Actions.Read( barcodeReader.Actions.NewReaderData( ) );

            //            barcodeReader.ReaderParameters.ReaderSpecific.ImagerSpecific.PicklistMode = true;
            //            barcodeReader.Actions.SetParameters( );
            //        }
            //        else
            //        {
            //            log.Debug( string.Format( "Unable to locate a barcode scanner on this Device. '{0}' does not exist", BarcodeScannerDeviceName ) );
            //        }
            //    }
            //}

            Configure( );
        }

        /// <summary>
        /// Configures the DataModel with the provided application server information.  This is used
        /// if the application server has not been preconfigured on a given Device.
        /// </summary>
        public void Configure( )
        {
            log.Debug( "Configuring Data Model" );

            //this.activeWSM = ApplicationSettings.ActiveWSM;
            //this.activeAppServerID = ApplicationSettings.ActiveAppServerID;
            this.IsSyncing = false;

            if ( SyncManager == null )
            {
                //SyncManager = new SyncManager( ApplicationSettings.Server, dataMgr, TransactionFactory.GetInstance( ), this.activeWSM );
                SyncManager = new SyncManager( ApplicationSettings.Server, dataMgr, TransactionFactory.GetInstance( ) );
                SyncManager.RegisterSyncAdapterFactory( new SyncAdapterFactory( ) );

                // register sync items

                Dictionary<string, int> DataTags = SyncManager.DataTags;

                foreach ( string s in DataTags.Keys )
                {
                    SyncManager.RegisterSyncItem( (DataTag)Enum.Parse( typeof( DataTag ), s ), SyncType.Upload );
                }
            }
            else
            {
                SyncManager.Configure( ApplicationSettings.Server );
            }

            //SyncManager.InitSyncTracking( this.activeWSM );
            SyncManager.InitSyncTracking( );
        }

        private void OnSyncProgressChanged( object sender, SyncProgressChangedEventArgs e )
        {
            string syncStatus = String.Empty;
            if ( e.Tag == DataTag.None )
            {
                if ( e.Progress == 0 )
                {
                    if ( ( e.Type & SyncType.Download ) == SyncType.Download )
                        syncStatus = Resources.SyncManager_CheckingForUpdates;
                    else if ( ( e.Type & SyncType.Upload ) == SyncType.Upload )
                        syncStatus = Resources.SyncManager_CheckingForLocalModification;
                    else
                        syncStatus = Resources.SyncManager_ResolvingDeletedData;
                }
                else if ( e.Progress == 100 )
                {
                    if ( ( e.Type & SyncType.Download ) == SyncType.Download )
                        syncStatus = Resources.SyncManager_UpdatingConfiguration;
                    else if ( ( e.Type & SyncType.Upload ) == SyncType.Upload )
                        syncStatus = Resources.SyncManager_UploadComplete;
                    else
                        syncStatus = Resources.SyncManager_ResolvingDeletedData;
                }
            }
            else if ( DataTagMapping.FriendlyNames != null ) syncStatus = String.Format( "{0}: {1}/{2}", DataTagMapping.FriendlyNames[ e.Tag ], e.CurrentItemCount, e.TotalItemCount );
            UpdateSyncStatus( e.Type, syncStatus, e.Progress );
        }

        private void UpdateSyncStatus( int type, string status, int progress )
        {
            if ( SyncProgressReported != null )
                SyncProgressReported( this, new SyncProgressReportEventArgs( type, progress, status ) );
        }

        private void OnSyncDataChanged( object sender, DataChangedEventArgs e )
        {
            switch ( e.DataTag )
            {
                //case DataTag.CompletionInfo:
                //    OnSyncCompletionsChanged( );
                //    break;
                default:
                    break;
            }
        }

        private void OnSyncItemXRefChanged( object sender, ItemXRefChangedEventArgs e )
        {
            switch ( e.DataTag )
            {
                //case DataTag.EventTypeDataValue:
                //    UpdateEventTypeDataValueXRef( e.ItemID.ToString( ), e.NewXRef );
                //    break;
                default:
                    break;
            }
        }

        private static void OnSyncError( object sender, ErrorReportedEventArgs e )
        {
            ILog syncLog = LogManager.GetLogger( "Sync" );
            if ( syncLog.IsErrorEnabled ) syncLog.ErrorFormat( e.Error );
        }

        private void OnSyncCompletionsChanged( )
        {
            completionsChanged = true;
        }

        //private SyncContext CreateSyncContext( DeviceInfo device )
        private SyncContext CreateSyncContext( )
        {
            SyncContext syncContext = new SyncContext
            {
                //LastSyncSpecified = true,
                LastSync = SyncManager.MIN_DATE,
                DeviceName = ApplicationSettings.DeviceName,
                PageIndex = 0,
                //PageIndexSpecified = true,
                PageSize = DefaultPageSize,
                //PageSizeSpecified = true,
                Version = this.Version,
                //AppServerID = this.activeAppServerID.ToString( ),
#warning TEMPORARY
                //Server = ApplicationSettings.ActiveWSM, // Local database?
                //Server = device.DataSourceID,
                //Company = String.IsNullOrEmpty( device.CompanyID ) ? Guid.Empty.ToString( ) : device.CompanyID
            };
            return syncContext;
        }

        public void RequestSync( bool async )
        {
            try
            {
                if ( waitingToSync )
                    waitingToSync = false;

                if ( IsSyncing )
                {
                    if ( async )
                    {
                        waitingToSync = true;
                        log.Warn( "Another sync currently in progress - queuing." );
                    }

                    log.Warn( "Another sync currently in progress - returning." );
                    return;
                }

                IsSyncing = true;

                //if( !ConnectionManager.Connect( ApplicationSettings.Server ) )
                //{
                //    CacheCompletions( );
                //    if( async )
                //    {
                //        log.Warn( "No network connection on background sync. Sync aborted." );
                //        IsSyncing = false;
                //        return;
                //    }

                //    if( SyncManager.LastSyncDate > DateTime.Now.ToUniversalTime( ) )
                //        throw new TimeOutOfSyncException( Resources.DataModel_TimeSync_Error );

                //    throw new NetworkException( Resources.DataModel_No_Network );
                //}

                //// validate Device
                //UpdateSyncStatus( SyncType.None, Resources.SyncManager_ValidatingDevice, 0 );
                //Device = ValidateDevice( ApplicationSettings.DeviceName );
                //if( Device == null )
                //{
                //    if( async )
                //    {
                //        log.Fatal( "No Device returned from server on background sync. Sync aborted." );
                //        IsSyncing = false;
                //        return;
                //    }

                //    log.Fatal( "No Device returned from server." );
                //    throw new ConfigurationException( Resources.DataModel_ConfigurationUtility_Error );
                //}
                //UpdateSyncStatus( SyncType.None, Properties.Resources.SyncManager_ValidatingDevice, 100 );

                //UpdateSyncStatus( SyncType.None, Properties.Resources.SyncManager_FinalizingSyncPreparations, 0 );
                // check to see if a wsm database has been stored, if so assign it and save
                //if ( activeWSM == Guid.Empty )
                //{
                //    //activeWSM = new Guid( Device.DataSourceID );
                //    activeWSM = new Guid( );
                //    ApplicationSettings.ActiveWSM = activeWSM;
                //    SyncManager.InitSyncTracking( activeWSM );
                //}
                //// if wsm database is different from the devices wsm database
                //else if( activeWSM.ToString( ) != Device.DataSourceID )
                //{
                //    // check to see if the Device has unsynchronized data to upload
                //    if( SyncManager.HasUnSychronizedData( ) )
                //    {
                //        SyncContext syncContext = CreateSyncContext( Device );

                //        // attempt to upload existing unsynchronized data to previous WSM database
                //        DialogResult oldWSMSyncResult = MessageBox.Show( Resources.DataModel_Database_Redirect_Unsynced, Resources.DataModel_Warning, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1 );
                //        if( oldWSMSyncResult == DialogResult.Yes )
                //        {
                //            log.Warn( "Device is switching to a new WSM Server and is now attempting to upload any outstanding data to its current WSM Server first." );
                //            SyncManager.ClearErrors( );

                //            // Flag the previous server in the context
                //            syncContext.Server = activeWSM.ToString( );

                //            SyncManager.Upload( syncContext );
                //            completionsChanged = true;
                //            if( SyncManager.HasSyncErrors )
                //            {
                //                throw new ConfigurationException( String.Format( Resources.DataModel_FailedToSyncData, activeWSM ) );
                //            }
                //        }
                //        else
                //            throw new ConfigurationException( Resources.DataModel_Clear_Datacache_Error );
                //    }

                //    // Reset itemized sync tracking to point to new WSM and re-sync
                //    DialogResult newWSMResult = MessageBox.Show( Resources.DataModel_Database_Redirect_Synced, Resources.DataModel_Warning, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1 );
                //    if( newWSMResult == DialogResult.Yes )
                //    {
                //        activeWSM = new Guid( Device.DataSourceID );
                //        ApplicationSettings.ActiveWSM = activeWSM;
                //        completionsChanged = true;
                //        SyncManager.InitSyncTracking( ApplicationSettings.ActiveWSM );
                //    }
                //    else
                //        throw new ConfigurationException( Resources.DataModel_Clear_Datacache_Error );
                //}

                //UpdateSyncStatus( SyncType.None, Properties.Resources.SyncManager_FinalizingSyncPreparations, 100 );

                //if( async )
                //{
                //    backgroundSyncThread = new Thread( DoThreadedSync );
                //    backgroundSyncThread.Priority = ThreadPriority.BelowNormal;
                //    backgroundSyncThread.Start( );
                //}
                //else
                {
                    DoSync( );

                    //if( SyncManager.HasSyncErrors )
                    //    throw new SyncException( Resources.DataModel_Sync_Errors );
                }
            }
            catch
            {
                IsSyncing = false;
                throw;
            }
        }

        //private void DoThreadedSync( )
        //{
        //    try
        //    {
        //        DoSync( );

        //        if( AsyncCompleted != null )
        //            AsyncCompleted( this, new SyncEventArgs( true, false ) );
        //    }
        //    catch( Exception ex )
        //    {
        //        if( log.IsFatalEnabled )
        //        {
        //            log.Fatal( "Backgound sync failed!", ex );
        //        }

        //        SyncManager.SyncErrors.Add( "Background synchronization failed." );

        //        if( SyncFailed != null ) SyncFailed( this, EventArgs.Empty );
        //    }
        //}

        private void DoSync( )
        {
            //// sync server time
            //UpdateSyncStatus( SyncType.None, Properties.Resources.SyncManager_SyncingServerTime, 0 );

            DateTime lastSync = DateTime.Now;
            //if( PlatformManager.IsMobile )
            //{
            //    lastSync = SystemClock.SyncServerTime( );
            //}
            LogHelper.ForceLog( String.Format( "Time synced to {0}.", lastSync ) );

            //// sync server time
            //UpdateSyncStatus( SyncType.None, Properties.Resources.SyncManager_SyncingServerTime, 100 );

            try
            {
                //KeepAlive.Enable( );
                //if( ConnectionManager.IsConnected( ) )
                //{
                //    if( SyncManager.IsInitialSync( ) )
                //    {
                //        log.Warn( "This is an initial sync. If there is data in the database from a previous sync, it is error prone and will be deleted." );
                //        if( SyncManager.HasUnSychronizedData( ) )
                //            log.Warn( "Initial sync has existing data. Deleting..." );

                //        SyncManager.ClearIncompleteSyncData( );
                //    }

                //SyncContext syncContext = CreateSyncContext( Device );
                SyncContext syncContext = CreateSyncContext( );

                SyncManager.Sync( syncContext );
                //}

                //    // If initial sync failed, bail. App will exit at a higher level
                //    if( SyncManager.IsInitialSync( ) && SyncManager.HasSyncErrors )
                //    {
                //        IsSyncing = false;
                //        return;
                //    }

                //    UpdateSyncStatus( SyncType.None, Properties.Resources.SyncManager_UpdatingConfiguration, 0 );

                //    // otherwise continue. load data even if non-initial sync failed, so we can continue working
                //    // load asset
                //    AssetInfo curAsset = CurrentTruckingUnit;
                //    if( Device.AssetID != null )
                //        LoadCurrentAsset( new Guid( Device.AssetID ) );
                //    if( CurrentTruckingUnit == null && curAsset != null )
                //        CurrentTruckingUnit = curAsset;

                //    // cache completions if none are cached or if they have changed
                //    if( Completions == null || Completions.Count == 0 || completionsChanged )
                //    {
                //        CacheCompletions( );
                //        completionsChanged = false;
                //    }

                //    UpdateSyncStatus( SyncType.None, Properties.Resources.SyncManager_UpdatingConfiguration, 100 );
            }
            finally
            {
                //    IsSyncing = false;
                //    ConnectionManager.Disconnect( );
                //    KeepAlive.Disable( );
            }

            //// if another sync was requested while a sync was in-progress, start it now on the background thread
            //if( waitingToSync && !SyncManager.HasSyncErrors )
            //    RequestSync( true );
        }
    }
}