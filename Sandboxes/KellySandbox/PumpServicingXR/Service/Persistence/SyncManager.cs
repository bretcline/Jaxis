using System;
using System.Collections.Generic;
using System.Timers;
using log4net;
using WFT.PS.Shared;
using WFT.PSService.Data;
using WFT.PSService.Service;
using WFT.PSService.ServiceLibrary;
using LFI.Sync.DataManager;
using System.Linq;
//using WFT.PSService.Data.Mappings.WSM;
using WFT.PSService.Data.Mappings;

namespace WFT.PSService.Service
{
//    public class SyncManager
//    {
//        public static readonly DateTime FirstSyncDate = new DateTime(1900, 1, 1);
//        private readonly ILog logger = LogManager.GetLogger("SyncManager");
//        private readonly PersistenceManager persistenceMgr;
//        private Dictionary<Guid, DateTime?> _lastSync;
//        private bool forceSync;
//        private Timer syncTimer;


//        //----------------------------------------------------------------------
//        public SyncManager(PersistenceManager persistenceMgr)
//        {
//            this.persistenceMgr = persistenceMgr;
//            IsSyncing = false;

//            MasterServerID = Guid.Empty;
//            SyncInterval = 0;
//            _lastSync = new Dictionary<Guid, DateTime?>();
//        }

//        public Guid MasterServerID { get; internal set; }
//        public int SyncInterval { get; internal set; }
//        public bool IsSyncing { get; set; }

//        //----------------------------------------------------------------------
//        public void Init()
//        {
//            try
//            {
//                if (MasterServerID != Guid.Empty)
//                {
//                    LoadDataCache();

//                    DoDataSync();
//                }
//                else if (logger.IsWarnEnabled) logger.Warn("Master Server is not yet configured. Sync cannot complete.");
//            }
//            finally
//            {
//                IsSyncing = false;
//            }
//        }

//        //----------------------------------------------------------------------
//        public void ForceSync()
//        {
//            if (IsSyncing)
//                return;

//            if (logger.IsDebugEnabled) logger.Debug("ForceSync initiated");

//            forceSync = true;

//            try
//            {
//                // Load WS Ref data in memory
//                if (MasterServerID == Guid.Empty)
//                {
//                    if (logger.IsErrorEnabled) logger.Error("Master Server has not been configured. Data-Sync cannot continue.");
//                    return;
//                }

//                LoadDataCache();

//                // Sync WSM Data with WS Data
//                DoDataSync();
//            }
//            finally
//            {
//                forceSync = false;
//                IsSyncing = false;
//            }
//        }

//        //----------------------------------------------------------------------
//        public bool SetLastSync(Guid sourceDB, DateTime? lastSyncTime)
//        {
//            return SetLastSync(sourceDB, lastSyncTime ?? FirstSyncDate);
//        }

//        //----------------------------------------------------------------------
//        public bool SetLastSync(Guid sourceDB, DateTime lastSyncTime)
//        {
//            if ( sourceDB == persistenceMgr.PumpServicingServerID )
//                return true;

//            DataSource lastSyncDataSouce = new DataSource();
//            lastSyncDataSouce.ID = sourceDB;
//            lastSyncDataSouce.LastSync = lastSyncTime;

//            if (logger.IsDebugEnabled) logger.DebugFormat("Writing last sync time {0} for database {1} to configuration database.", lastSyncTime, sourceDB);
//            TransactionResult result = persistenceMgr.GetWSDataManager( ).PutData( new LastSyncTransaction( lastSyncDataSouce ) );
//            if (result.Success)
//            {
//                if (_lastSync.ContainsKey(sourceDB))
//                    _lastSync[sourceDB] = lastSyncTime;
//                else
//                    _lastSync.Add(sourceDB, lastSyncTime);
//            }
//            else if (logger.IsErrorEnabled) logger.ErrorFormat("Failed to update sync time with Message '{0}'.", result.Message);

//            return result.Success;
//        }

//        //----------------------------------------------------------------------
//        public DateTime GetLastSync(Guid sourceDB)
//        {
//            if (_lastSync.ContainsKey(sourceDB) && _lastSync[sourceDB] != null)
//                return _lastSync[sourceDB].Value;

//            return FirstSyncDate;
//        }

//        //----------------------------------------------------------------------
//        /// <summary>
//        /// Caches all of the information from PumpServicing.
//        /// Note that some items never get cached and
//        /// others get cached based on 'UseInMemoryCache' which is set in the web.config.
//        /// If items are set to be cached (UseInMemoryCache = true), then they will only
//        /// be cached X days into the past, where X is set in the web.config under DateRangeForCacheInDays.
//        /// </summary>
//        private void LoadDataCache()
//        {
//            //if (logger.IsInfoEnabled) logger.Info("Loading Reference Data from PumpServicing Configuration Database.");
//            //IsSyncing = true;
            
//            //try
//            //{
//            //    DataManager dataManagerWS = persistenceMgr.GetWSDataManager();
//            //    DataManager dataMgrMaster = persistenceMgr.GetDataManagerByServer(MasterServerID);
//            //    DataCache dataCache = persistenceMgr.GetCache(MasterServerID);

//            //    if (logger.IsDebugEnabled) logger.Debug("Loading AssetTypes");
//            //    DataCache.AssetTypes = new WSCache<AssetType>(dataManagerWS.GetData(new AssetTypeTransaction()));
                
//            //    if (logger.IsDebugEnabled) logger.Debug("Loading AssetCaps");
//            //    DataCache.AssetCaps = new WSCache<AssetCapsInfo>(dataManagerWS.GetData(new AssetCapInfoTransaction()));
                
//            //    if (logger.IsDebugEnabled) logger.Debug("Loading AssetGroups");
//            //    DataCache.AssetGroups = new WSCache<AssetGroupInfo>(dataManagerWS.GetData(new AssetGroupInfoTransaction()));
                
//            //    if (logger.IsDebugEnabled) logger.Debug("Loading AssetGroupMembers");
//            //    DataCache.AssetGroupMembers = new WSCache<AssetGroupMemberInfo>(dataManagerWS.GetData(new AssetGroupMemberInfoTransaction()));
                
//            //    if (logger.IsDebugEnabled) logger.Debug("Loading Assets");
//            //    DataCache.Assets = new AssetCache(dataManagerWS.GetData(new AssetInfoTransaction()));
                
//            //    if (logger.IsDebugEnabled) logger.Debug("Loading Capabilities");
//            //    DataCache.Capabilities = new WSCache<Capability>(dataManagerWS.GetData(new CapabilityTransaction()));
                
//            //    if (logger.IsDebugEnabled) logger.Debug("Loading CompanyTypes");
//            //    DataCache.CompanyTypes = new WSCache<CompanyType>(dataManagerWS.GetData(new CompanyTypeTransaction()));

//            //    if (logger.IsDebugEnabled) logger.Debug("Loading Companies");
//            //    DataCache.Companies = new CompanyCache(dataManagerWS.GetData(new CompanyInfoTransaction()));

//            //    if (logger.IsDebugEnabled) logger.Debug("Loading EventTypes");
//            //    DataCache.EventTypes = new WSCache<EventType>(dataManagerWS.GetData(new EventTypeTransaction(false)));

//            //    if (logger.IsDebugEnabled) logger.Debug("Loading Extended Event Mappings");
//            //    persistenceMgr.LoadExtendedEvents(dataManagerWS.GetData(new ExtendedEventMapTransaction()));

//            //    if (logger.IsDebugEnabled) logger.Debug("Loading JobReasons");
//            //    DataCache.JobReasons = new WSCache<JobReasonInfo>(dataManagerWS.GetData(new JobReasonInfoTransaction()));
                
//            //    if (logger.IsDebugEnabled) logger.Debug("Loading JobStatuses");
//            //    DataCache.JobStatuses = new JobStatusCache(dataManagerWS.GetData(new JobStatusTransaction()));
                
//            //    if (logger.IsDebugEnabled) logger.Debug("Loading JobTypeEventTypes");
//            //    DataCache.JobTypeEventTypes = new WSCache<JobTypeEventTypeInfo>(dataManagerWS.GetData(new JobTypeEventTypeInfoTransaction(false)));
                
//            //    if (logger.IsDebugEnabled) logger.Debug("Loading JobTypes");
//            //    DataCache.JobTypes = new WSCache<JobTypeInfo>(dataManagerWS.GetData(new JobTypeInfoTransaction()));
                
//            //    if (logger.IsDebugEnabled) logger.Debug("Loading PlanTypes");
//            //    DataCache.PlanTypes = new WSCache<PlanType>(dataManagerWS.GetData(new PlanTypeTransaction()));

//            //    if (logger.IsDebugEnabled) logger.Debug("Loading Picklists");
//            //    DataCache.Picklists = new WSCache<Picklist>(dataManagerWS.GetData(new PicklistTransaction()));

//            //    if (logger.IsDebugEnabled) logger.Debug("Loading Picklist Members");
//            //    DataCache.PicklistMembers = new WSCache<PicklistMember>(dataManagerWS.GetData(new PicklistMemberTransaction()));

//            //    if (logger.IsDebugEnabled) logger.Debug("Loading Property Metadata");
//            //    DataCache.PropertyMetadata = new WSCache<Property>(dataManagerWS.GetData(new PropertyTransaction()));

//            //    if (logger.IsDebugEnabled) logger.Debug("Loading Data Entry Types");
//            //    DataCache.DataEntryTypes = new WSCache<DataEntryType>(dataManagerWS.GetData(new DataEntryTypeTransaction()));

//            //    if (logger.IsDebugEnabled) logger.Debug("Loading Event Type Property Map");
//            //    DataCache.EventTypePropertyMaps = new EventTypePropertyMapCache(dataManagerWS.GetData(new EventTypePropertyMapTransaction(0, 0, SyncManager.FirstSyncDate)));

//            //    // custom ref data
//            //    if (logger.IsDebugEnabled) logger.Debug("Loading EventTypeGroups");
//            //    DataCache.EventTypeGroups = new WSCache<EventTypeGroupInfo>(dataManagerWS.GetData(new EventTypeGroupInfoTransaction(false)));
//            //    if (logger.IsDebugEnabled) logger.Debug("Loading EventTypeGroupMembers");
//            //    DataCache.EventTypeGroupMembers = new WSCache<EventTypeGroupMemberInfo>(dataManagerWS.GetData(new EventTypeGroupMemberInfoTransaction(false)));

//            //    // wsm data-source specific cache
//            //    /*foreach (DataManager dataMgr in persistenceMgr.DataManagers.Values)
//            //    {
//            //        if (dataMgr.SourceDB == dataManagerWS.SourceDB)
//            //            continue;

//            //        DataCache cache = persistenceMgr.GetCache(dataMgr.SourceDB);
//            //        if (logger.IsDebugEnabled) logger.Debug("Loading Event Type Property Map");
//            //        cache.EventTypePropertyMaps = new EventTypePropertyMapCache(dataManagerWS.GetData(new EventTypePropertyMapTransaction(dataMgr.SourceDB, 0, 0, SyncManager.FirstSyncDate)));
//            //    }*/
//            //}
//            //catch (Exception ex)
//            //{
//            //    if (logger.IsFatalEnabled) logger.Fatal("Failed to load reference data.", ex);
//            //}

//            //if (logger.IsInfoEnabled) logger.Info("Reference Data Load completed.");
//        }

//        //----------------------------------------------------------------------
//        /// <summary>
//        /// Does the data synchronization between one or more WS instances into the WSM database.
//        /// </summary>
//        private void DoDataSync()
//        {
//            if( logger.IsInfoEnabled )
//            {
//                logger.Info( "Syncing Remote Data with PumpServicing Configuration Data" );
//            }

//            if (SyncInterval == 0)
//            {
//                if( logger.IsErrorEnabled )
//                {
//                    logger.Error( "No synchronization details were found in configuration database. Sync aborted." );
//                }
//                return;
//            }

//            if (MasterServerID == Guid.Empty)
//            {
//                if( logger.IsErrorEnabled )
//                {
//                    logger.Error( "No Master Remote Server specified in configuration database. Sync aborted." );
//                }
//                return;
//            }

//            IsSyncing = true;

//            // setup sync timer
//            if (syncTimer == null)
//            {
//                syncTimer = new Timer(15*60*1000);
//                syncTimer.Elapsed += OnSyncTick;
//                syncTimer.Start();
//            }

//            if (SyncInterval > 0)
//            {
//                syncTimer.Interval = SyncInterval*60*1000;
//                if( logger.IsDebugEnabled )
//                { 
//                    logger.DebugFormat( "Sync interval set to {0} minute(s).", SyncInterval ); 
//                }
//            }
//            else if (!forceSync)
//            {
//                if( logger.IsDebugEnabled )
//                {
//                    logger.Debug( "Sync aborted because sync interval is set to 0." );
//                }
//                return;
//            }

//            if( logger.IsDebugEnabled )
//            {
//                logger.Debug( "Initiating data sync transactions" );
//            }
//            DataManager dataMgrMaster = persistenceMgr.GetDataManagerByServer(MasterServerID);
//            DataManager dataMgrWS = persistenceMgr.GetWSDataManager();

//            DateTime syncFrom = GetLastSync(MasterServerID);
//            if( logger.IsDebugEnabled )
//            {
//                logger.DebugFormat( "Sync using last sync time {0}.", syncFrom );
//            }

//            bool isFullSync = false;
//            if( syncFrom == FirstSyncDate )
//            {
//                isFullSync = true;
//            }

//            if( forceSync )
//            {
//                syncFrom = FirstSyncDate;
//            }

//            if (!isFullSync && !forceSync)
//            {
//                // convert last sync time to local time on the data source
//                syncFrom = syncFrom.ToLocalTime();
//                TimeSpan offset = TimeSpan.FromHours(dataMgrMaster.TimeZoneOffset);
//                syncFrom -= offset;
//            }

//            DateTime syncTime = persistenceMgr.GetServerTime();
//            //bool success = true;
//            DataCache masterCache = persistenceMgr.GetCache(MasterServerID);

//            //#region sync company types

//            //try
//            //{
//            //    if (logger.IsDebugEnabled) logger.Debug("Syncing company types...");
//            //    MergeDataSourceToCache(dataMgrMaster.GetData(new WSMCompanyTypeTransaction(syncFrom)), DataCache.CompanyTypes, masterCache.PopulateCommonData, isFullSync);
//            //}
//            //catch (Exception ex)
//            //{
//            //    if (logger.IsErrorEnabled) logger.Error("Company Type sync failed.", ex);
//            //    success = false;
//            //}

//            //#endregion

//            //#region sync companies

//            //try
//            //{
//            //    if (logger.IsDebugEnabled) logger.Debug("Syncing companies...");
//            //    MergeDataSourceToCache(dataMgrMaster.GetData(new WSMCompanyInfoTransaction(syncFrom)), DataCache.Companies, masterCache.PopulateCompanyRefData, isFullSync);
//            //}
//            //catch (Exception ex)
//            //{
//            //    if (logger.IsErrorEnabled) logger.Error("Company sync failed.", ex);
//            //    success = false;
//            //}

//            //#endregion

//            //#region sync asset types

//            //try
//            //{
//            //    if (logger.IsDebugEnabled) logger.Debug("Syncing asset types...");
//            //    MergeDataSourceToCache(dataMgrMaster.GetData(new WSMAssetTypeTransaction(syncFrom)), DataCache.AssetTypes, masterCache.PopulateCommonData, isFullSync);
//            //}
//            //catch (Exception ex)
//            //{
//            //    if (logger.IsErrorEnabled) logger.Error("Asset Type sync failed.", ex);
//            //    success = false;
//            //}

//            //#endregion

//            //#region sync assets

//            //try
//            //{
//            //    if (logger.IsDebugEnabled) logger.Debug("Syncing assets...");
//            //    MergeDataSourceToCache(dataMgrMaster.GetData(new WSMAssetInfoTransaction(syncFrom)), DataCache.Assets, masterCache.PopulateAssetRefData, isFullSync);
//            //}
//            //catch (Exception ex)
//            //{
//            //    if (logger.IsErrorEnabled) logger.Error("Asset sync failed.", ex);
//            //    success = false;
//            //}

//            //#endregion

//            //#region sync asset groups

//            //try
//            //{
//            //    if (logger.IsDebugEnabled) logger.Debug("Syncing asset groups...");
//            //    MergeDataSourceToCache(dataMgrMaster.GetData(new WSMAssetGroupInfoTransaction(syncFrom)), DataCache.AssetGroups, masterCache.PopulateCommonData, isFullSync);
//            //}
//            //catch (Exception ex)
//            //{
//            //    if (logger.IsErrorEnabled) logger.Error("Syncing asset groups failed.", ex);
//            //    success = false;
//            //}

//            //#endregion

//            //#region sync asset group members

//            //try
//            //{
//            //    if (logger.IsDebugEnabled) logger.Debug("Syncing asset group members...");
//            //    MergeDataSourceToCache(dataMgrMaster.GetData(new WSMAssetGroupMemberInfoTransaction(syncFrom)), DataCache.AssetGroupMembers, masterCache.PopulateAssetGroupMemberRefData, isFullSync);
//            //}
//            //catch (Exception ex)
//            //{
//            //    if (logger.IsErrorEnabled) logger.Error("Syncing asset group members failed.", ex);
//            //    success = false;
//            //}

//            //#endregion

//            //#region sync capabilities

//            //try
//            //{
//            //    if (logger.IsDebugEnabled) logger.Debug("Syncing cababilities...");
//            //    MergeDataSourceToCache(dataMgrMaster.GetData(new WSMCapabilityTransaction(syncFrom)), DataCache.Capabilities, masterCache.PopulateCommonData, isFullSync);
//            //}
//            //catch (Exception ex)
//            //{
//            //    if (logger.IsErrorEnabled) logger.Error("Syncing capabilities failed.", ex);
//            //    success = false;
//            //}

//            //#endregion

//            //#region sync asset capabilities

//            //try
//            //{
//            //    if (logger.IsDebugEnabled) logger.Debug("Syncing asset capabilities...");
//            //    MergeDataSourceToCache(dataMgrMaster.GetData(new WSMAssetCapInfoTransaction(syncFrom)), DataCache.AssetCaps, masterCache.PopulateAssetCapsRefData, isFullSync);
//            //}
//            //catch (Exception ex)
//            //{
//            //    if (logger.IsErrorEnabled) logger.Error("Syncing asset capabilities failed.", ex);
//            //    success = false;
//            //}

//            //#endregion

//            //#region sync event types

//            //try
//            //{
//            //    if (logger.IsDebugEnabled) logger.Debug("Syncing event types...");
//            //    List<EventType> mergedItems = MergeDataSourceToCache(dataMgrMaster.GetData(new WSMEventTypeTransaction(syncFrom)), DataCache.EventTypes, masterCache.PopulateCommonData, isFullSync);

//            //    // build and register extended event table map from event types
//            //    persistenceMgr.RegisterExtendedEvents(mergedItems, isFullSync);
//            //}
//            //catch (Exception ex)
//            //{
//            //    if (logger.IsErrorEnabled) logger.Error("Syncing event types failed.", ex);
//            //    success = false;
//            //}

//            //#endregion

//            //#region sync event type groups (REMOVED PER OXY'S REQUEST)

//            ////try
//            ////{
//            ////    if (logger.IsDebugEnabled) logger.Debug("Syncing event type groups...");
//            ////	  SyncAndMerge(dataMgrMaster.GetData(new WSMEventTypeGroupInfoTransaction(syncFrom)), masterCache.EventTypeGroups, null);
//            ////}
//            ////catch (Exception ex)
//            ////{
//            ////    if (logger.IsErrorEnabled) logger.Error("Syncing event type groups failed.", ex);
//            ////}

//            //#endregion

//            //#region sync event type group members (REMOVED PER OXY'S REQUEST)

//            ////try
//            ////{
//            ////    if (logger.IsDebugEnabled) logger.Debug("Syncing event type group memebers...");
//            ////	  SyncAndMerge(dataMgrMaster.GetData(new WSMEventTypeGroupMemberInfoTransaction(syncFrom)), masterCache.EventTypeGroupMembers, null);
//            ////}
//            ////catch (Exception ex)
//            ////{
//            ////    if (logger.IsErrorEnabled) logger.Error("Syncing event type group members failed.", ex);
//            ////}

//            //#endregion

//            //#region sync job types

//            //List<JobTypeInfo> wsmJobTypes = null;
//            //try
//            //{
//            //    if (logger.IsDebugEnabled) logger.Debug("Syncing job types...");
//            //    wsmJobTypes = MergeDataSourceToCache(dataMgrMaster.GetData(new WSMJobTypeInfoTransaction(syncFrom)), DataCache.JobTypes, masterCache.PopulateCommonData, isFullSync);
//            //}
//            //catch (Exception ex)
//            //{
//            //    if (logger.IsErrorEnabled) logger.Error("Syncing job types failed.", ex);
//            //    success = false;
//            //}

//            //#endregion

//            //#region sync plan types

//            //try
//            //{
//            //    if (logger.IsDebugEnabled) logger.Debug("Syncing plan types...");
//            //    if (wsmJobTypes != null && wsmJobTypes.Count > 0)
//            //    {
//            //        // get plan types from job types
//            //        List<PlanType> wsmPlanTypes = new List<PlanType>(500);
//            //        foreach (JobTypeInfo jobType in wsmJobTypes)
//            //            wsmPlanTypes.Add(PlanType.FromJobType(jobType));

//            //        // merge and update
//            //        MergeDataSourceToCache(wsmPlanTypes, DataCache.PlanTypes, masterCache.PopulateCommonData, isFullSync);
//            //    }
//            //}
//            //catch (Exception ex)
//            //{
//            //    if (logger.IsErrorEnabled) logger.Error("Syncing plan types failed.", ex);
//            //    success = false;
//            //}

//            //#endregion

//            //#region sync job reasons

//            //try
//            //{
//            //    if (logger.IsDebugEnabled) logger.Debug("Syncing job reasons...");
//            //    MergeDataSourceToCache(dataMgrMaster.GetData(new WSMJobReasonInfoTransaction(syncFrom)), DataCache.JobReasons, masterCache.PopulateJobReasonRefData, isFullSync);
//            //}
//            //catch (Exception ex)
//            //{
//            //    if (logger.IsErrorEnabled) logger.Error("Syncing job reasons failed.", ex);
//            //    success = false;
//            //}

//            //#endregion

//            //#region sync job statuses

//            //try
//            //{
//            //    if (logger.IsDebugEnabled) logger.Debug("Syncing job statuses...");
//            //    MergeDataSourceToCache(dataMgrMaster.GetData(new WSMJobStatusTransaction(syncFrom)), DataCache.JobStatuses, masterCache.PopulateCommonData, isFullSync);
//            //}
//            //catch (Exception ex)
//            //{
//            //    if (logger.IsErrorEnabled) logger.Error("Syncing job statuses failed.", ex);
//            //    success = false;
//            //}

//            //#endregion

//            //#region sync job type event type members

//            //try
//            //{
//            //    if (logger.IsDebugEnabled) logger.Debug("Syncing job type event type members...");
//            //    MergeDataSourceToCache(dataMgrMaster.GetData(new WSMJobTypeEventTypeInfoTransaction(syncFrom)), DataCache.JobTypeEventTypes, masterCache.PopulateJobTypeEventTypeRefData, isFullSync);
//            //}
//            //catch (Exception ex)
//            //{
//            //    if (logger.IsErrorEnabled) logger.Error("Syncing job type event type members failed.", ex);
//            //    success = false;
//            //}

//            //#endregion

//            //#region sync picklist members and create picklists

//            //if (!SyncPicklist(new WSMAcidTypePicklistTransaction(syncFrom), dataMgrMaster, masterCache, isFullSync, "Acid Type", "Acid Type")) success = false;
//            //if (!SyncPicklist(new WSMCatalogItemPicklistTransaction(syncFrom), dataMgrMaster, masterCache, isFullSync, "Catalog Item", "Catalog Item")) success = false;
//            //if (!SyncPicklist(new WSMCatalogItemPricePicklistTransaction(syncFrom), dataMgrMaster, masterCache, isFullSync, "Catalog Item Price", "Catalog Item Price")) success = false;
//            //if (!SyncPicklist(new WSMCementClassPicklistTransaction(syncFrom), dataMgrMaster, masterCache, isFullSync, "Cement Class", "Cement Class")) success = false;
//            //if (!SyncPicklist(new WSMCementTOCMethodPicklistTransaction(syncFrom), dataMgrMaster, masterCache, isFullSync, "Cement TOC Method", "Cement TOC Method")) success = false;
//            //if (!SyncPicklist(new WSMCompanyPicklistTransaction(syncFrom), dataMgrMaster, masterCache, isFullSync, "Company", "Company")) success = false;
//            //if (!SyncPicklist(new WSMDisplacementFluidPicklistTransaction(syncFrom), dataMgrMaster, masterCache, isFullSync, "Displacement Fluid", "Displacement Fluid")) success = false;
//            //if (!SyncPicklist(new WSMDiverterTypePicklistTransaction(syncFrom), dataMgrMaster, masterCache, isFullSync, "Diverter Type", "Diverter Type")) success = false;
//            //if (!SyncPicklist(new WSMFillCleaningMethodPicklistTransaction(syncFrom), dataMgrMaster, masterCache, isFullSync, "Fill Cleaning Method", "Fill Cleaning Method")) success = false;
//            //if (!SyncPicklist(new WSMFracFluidsPicklistTransaction(syncFrom), dataMgrMaster, masterCache, isFullSync, "Frac Fluid", "Frac Fluid")) success = false;
//            //if (!SyncPicklist(new WSMFracStimProppantsPicklistTransaction(syncFrom), dataMgrMaster, masterCache, isFullSync, "Frac Stim Proppant", "Frac Stim Proppant")) success = false;
//            //if (!SyncPicklist(new WSMGasTypePicklistTransaction(syncFrom), dataMgrMaster, masterCache, isFullSync, "Gas Type", "Gas Type")) success = false;
//            //if (!SyncPicklist(new WSMPerfConveyedMethodPicklistTransaction(syncFrom), dataMgrMaster, masterCache, isFullSync, "Perf Conveyed Method", "Perf Conveyed Method")) success = false;
//            //if (!SyncPicklist(new WSMPerforationFluidTypePicklistTransaction(syncFrom), dataMgrMaster, masterCache, isFullSync, "Perforation Fluid Type", "Perforation Fluid Type")) success = false;
//            //if (!SyncPicklist(new WSMPurposeOfAnalysisPicklistTransaction(syncFrom), dataMgrMaster, masterCache, isFullSync, "Purpose of Analysis", "Purpose of Analysis")) success = false;
//            //if (!SyncPicklist(new WSMReasonForLogPicklistTransaction(syncFrom), dataMgrMaster, masterCache, isFullSync, "Reason for Log", "Reason for Log")) success = false;
//            //if (!SyncPicklist(new WSMReasonForWaitingPicklistTransaction(syncFrom), dataMgrMaster, masterCache, isFullSync, "Reason for Waiting", "Reason for Waiting")) success = false;
//            //if (!SyncPicklist(new WSMSafetyIncidentTypePicklistTransaction(syncFrom), dataMgrMaster, masterCache, isFullSync, "Safety Incident Type", "Safety Incident Type")) success = false;
//            //if (!SyncPicklist(new WSMTruckUnitPicklistTransaction(syncFrom), dataMgrMaster, masterCache, isFullSync, "Truck Unit", "Truck Unit")) success = false;
//            //if (!SyncPicklist(new WSMWBFlowAreaPicklistTransaction(syncFrom), dataMgrMaster, masterCache, isFullSync, "WB Flow Area", "WB Flow Area")) success = false;
//            //if (!SyncPicklist(new WSMWellStatePicklistTransaction(syncFrom), dataMgrMaster, masterCache, isFullSync, "Well State", "Well State")) success = false;

//            //#endregion

////            #region sync property meta-data

////            try
////            {
////                if (logger.IsDebugEnabled) logger.Debug("Syncing property metadata...");
////                MergeDataSourceToCache(dataMgrMaster.GetData(new WSMPropertyInfoTransaction()), DataCache.PropertyMetadata, masterCache.PopulatePropertyInfoRefData, isFullSync);
////            }
////            catch (Exception ex)
////            {
////                if (logger.IsErrorEnabled) logger.Error("Syncing property metadata failed.", ex);
////                success = false;
////            }

////            #endregion

////            #region sync event type property mapping

////            // This maps the property reference meta-data above to specific event types
////            try
////            {
////                if (logger.IsDebugEnabled) logger.Debug("Syncing event type property map...");
////                MergeDataSourceToCache(dataMgrMaster.GetData(new WSMEventTypePropertyMapTransaction()), DataCache.EventTypePropertyMaps, masterCache.PopulateEventTypePropertyMapRefData, isFullSync);
////            }
////            catch (Exception ex)
////            {
////                if (logger.IsErrorEnabled) logger.Error("event type property map failed.", ex);
////                success = false;
////            }

////            #endregion

////            #region non ref-data sync

////            foreach (DataManager dataMgr in persistenceMgr.DataManagers.Values)
////            {
////                if (dataMgr.SourceDB == dataMgrWS.SourceDB)
////                    continue;

////                syncFrom = GetLastSync(dataMgr.SourceDB);

////                isFullSync = false;
////                if (syncFrom == FirstSyncDate)
////                    isFullSync = true;

////                if (!isFullSync)
////                {
////                    syncFrom = syncFrom.ToLocalTime();
////                    TimeSpan offset = TimeSpan.FromHours(dataMgrMaster.TimeZoneOffset);
////                    syncFrom -= offset;
////                }

////                DataCache cache = persistenceMgr.GetCache(dataMgr.SourceDB);
////                DataManager dataManagerWS = persistenceMgr.GetWSDataManager();

////                #region sync wells

////                try
////                {
////                    if (logger.IsDebugEnabled) logger.DebugFormat("Syncing Wells from '{0}'...", dataMgr.SourceName);
////                    MergeDataSourceToCache(dataMgr.GetData(new WSMWellInfoTransaction(syncFrom)), cache.Wells, cache.PopulateWellRefData, isFullSync);
////                }
////                catch (Exception ex)
////                {
////                    if (logger.IsErrorEnabled) logger.Error("Syncing wells failed.", ex);
////                    success = false;
////                }

////                #endregion

////                #region sync completions

////                try
////                {
////                    if (logger.IsDebugEnabled) logger.DebugFormat("Syncing Completions from '{0}'...", dataMgr.SourceName);
////                    MergeDataSourceToCache(dataMgr.GetData(new WSMCompletionInfoTransaction(syncFrom)), cache.Completions, cache.PopulateCompletionRefData, isFullSync);
////                }
////                catch (Exception ex)
////                {
////                    if (logger.IsErrorEnabled) logger.Error("Syncing completions failed.", ex);
////                    success = false;
////                }

////                #endregion

////                #region sync jobs

////                List<JobInfo> jobsToUpdate = null;
////                Dictionary<string, JobInfo> jobsToSync = new Dictionary<string,JobInfo>();
////                try
////                {
////                    if (logger.IsDebugEnabled) logger.DebugFormat("Syncing Jobs from '{0}'...", dataMgr.SourceName);
////                    List<JobInfo> jobs = dataMgr.GetData(new WSMJobInfoTransaction(syncFrom));

////                    foreach (JobInfo jobInfo in jobs)
////                    {
////                        if (jobsToSync.ContainsKey(jobInfo.XRefID))
////                        {
////                            if (logger.IsErrorEnabled) logger.ErrorFormat("Duplicate job with key {0} found", jobInfo.XRefID);
////                            continue;
////                        }

////                        jobsToSync.Add(jobInfo.XRefID, jobInfo);
////                    }

////                    //jobsToSync = jobs.ToDictionary(job => job.XRefID);
////                }
////                catch (Exception ex)
////                {
////                    if (logger.IsErrorEnabled) logger.Error("Syncing jobs failed.", ex);
////                    success = false;
////                }

////                #endregion

////                #region sync plans from WSM

////                List<PlanInfo> plansToSync = null;
////                try
////                {
////                    if (logger.IsDebugEnabled) logger.DebugFormat("Syncing Plans from '{0}'...", dataMgr.SourceName);
////                    plansToSync = dataMgr.GetData(new WSMPlanInfoTransaction(syncFrom));
////                }
////                catch (Exception ex)
////                {
////                    if (logger.IsErrorEnabled) logger.Error("Syncing plans failed.", ex);
////                    success = false;
////                }

////                #endregion

////                #region link job plans to existing jobs

////                try
////                {
////                    if (logger.IsDebugEnabled) logger.DebugFormat("Linking job plans to jobs from '{0}'...", dataMgr.SourceName);

////                    foreach (PlanInfo plan in plansToSync)
////                    {
////                        JobInfo job = null;
////                        if (jobsToSync.ContainsKey(plan.JobXRefID))
////                            job = jobsToSync[plan.JobXRefID];
////                        else
////                        {
////                            // lookup job
////                            WSMJobInfoWithPlanTransaction jobTrans = new WSMJobInfoWithPlanTransaction(plan.JobXRefID);
////                            job = dataMgr.GetFirstDataResult(jobTrans);
////                            jobsToSync.Add(job.XRefID, job);
////                        }

////                        // link job plan "event" changes back to job if there are any
////                        if (job != null && job.LastModified < plan.LastModified)
////                        {
////                            job.LastModified = plan.LastModified;
////                            job.Recommendation = plan.Notes;
////                        }
////                    }

////                    if (logger.IsDebugEnabled) logger.DebugFormat("Committing jobs from '{0}'...", dataMgr.SourceName);
////                    jobsToUpdate = MergeDataSourceToCache(jobsToSync.Values, cache.Jobs, cache.PopulateJobRefData, isFullSync);

////                    if (!isFullSync)
////                    {
////                        //DeletedLogInfoTransaction deletedLogInfoTrans = new DeletedLogInfoTransaction("EventCategory", syncFrom);

////                        //if (logger.IsDebugEnabled) logger.DebugFormat("Syncing Deleted Jobs from '{0}'...", dataMgr.SourceName);
////                        //PopulateDeletedLogItems(deletedLogInfoTrans,
////                        //                        dataManagerWS.GetData(new JobInfoTransaction(dataMgr.GetData(deletedLogInfoTrans))),
////                        //                        arg => cache.Jobs.UpdateJobsHandler(arg, dataManagerWS),
////                        //                        dataMgr);
////                    }

////                    if (logger.IsDebugEnabled) logger.DebugFormat("Committing plans from '{0}'...", dataMgr.SourceName);
////                    MergeDataSourceToCache(plansToSync, cache.Plans, cache.PopulatePlanRefData, isFullSync);
////                }
////                catch (Exception ex)
////                {
////                    if (logger.IsErrorEnabled) logger.Error("Failed to link and commit jobs and plans", ex);
////                    success = false;
////                }

////                #endregion

////                #region create plans for jobs that don't have them in WSM

////                try
////                {
////                    if (logger.IsDebugEnabled) logger.DebugFormat("Creating Plans for new jobs that were missing plans in '{0}'...", dataMgr.SourceName);
////                    if (jobsToUpdate != null && jobsToUpdate.Count != 0)
////                    {
////                        // remove jobs with plans from list
////                        foreach (PlanInfo plan in cache.Plans.GetItems())
////                        {
////                            if (cache.Jobs.ContainsGuid(plan.JobID))
////                                jobsToUpdate.Remove(cache.Jobs.GetItemByGuid(plan.JobID));
////                        }

////                        // create plans for each remaining job
////                        List<PlanInfo> createdPlans = new List<PlanInfo>(500);
////                        foreach (JobInfo job in jobsToUpdate)
////                            createdPlans.Add(PlanInfo.FromJob(job, DataPrefix.Plan));

////                        // insert plans into ws database
////                        MergeDataSourceToCache(createdPlans, cache.Plans, cache.PopulatePlanRefData, isFullSync);
////                    }
////                }
////                catch (Exception ex)
////                {
////                    if (logger.IsErrorEnabled) logger.Error("Creating plans failed.", ex);
////                    success = false;
////                }

////                #endregion

////                #region sync events

////                // only sync 6 months of events
////                if (syncFrom == FirstSyncDate)
////#if RELEASE
////                    syncFrom = DateTime.Now - TimeSpan.FromDays(180);
////#else
////                    syncFrom = DateTime.Now - TimeSpan.FromDays(365);
////#endif
////                try
////                {
////                    if (logger.IsDebugEnabled) logger.DebugFormat("Syncing Events from '{0}'...", dataMgr.SourceName);
////                    List<EventInfo> wsmEvents = dataMgr.GetData(new WSMEventInfoWorkedTransaction(syncFrom));
////                    List<EventInfo> wsmEventsNoWork = dataMgr.GetData(new WSMEventInfoNoWorkTransaction(syncFrom));
////                    //wsmEvents.AddRange(wsmEventsNoWork);

////                    MergeDataSourceToCache(wsmEvents, cache.Events, cache.PopulateEventRefData, "CompletedXRefID", isFullSync);
////                    MergeDataSourceToCache(wsmEventsNoWork, cache.Events, cache.PopulateEventRefData, "ScheduledXRefID", isFullSync);

////                    if (!isFullSync)
////                    {
////                        //DeletedLogInfoTransaction deletedLogInfoTrans = new DeletedLogInfoTransaction("Event", syncFrom);

////                        //// Retrieve deleted events
////                        //if (logger.IsDebugEnabled) logger.DebugFormat("Syncing Deleted Events from '{0}'...", dataMgr.SourceName);
////                        //PopulateDeletedLogItems(deletedLogInfoTrans,
////                        //                        dataManagerWS.GetData(new EventInfoTransaction(dataMgr.GetData(deletedLogInfoTrans))),
////                        //                        arg => cache.Events.UpdateEventsHandler(arg, dataManagerWS),
////                        //                        dataMgr);
////                    }
////                }
////                catch (Exception ex)
////                {
////                    if (logger.IsErrorEnabled) logger.Error("Syncing events failed.", ex);
////                    success = false;
////                }

////                #endregion

////                #region sync event properties and details

////                //if (!SyncExtendedEvents(new WSMAcidizePropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMAssembleSRPumpPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMBHPSurveyPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMCasingTubingTallyPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMCementPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMCementPlugPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMCementSqueezePropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMChemAnlBacteriaPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMChemAnlCaliperPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMChemAnlCathodicPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMChemAnlCouponsPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMChemAnlFailedPartPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMChemAnlGasPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMChemAnlGrindoutPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMChemAnlIronPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMChemAnlMilliporePropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMChemAnlOilPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMChemAnlOilCarryoverPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMChemAnlResidualPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMChemAnlSolidsPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMChemAnlWaterPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMChemInventoryTransPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMChemPhysInventoryPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMChemTrtHistoryPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMChemTrtPlanPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMChemTrtSchedPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMCleanFillPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMCleanPerfsPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMDisassembleSRPumpPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMDrillingPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMDrillingBitPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMDrillingMudPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMDrillingMudPumpPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMDrillStemTestPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMEconomicAnalysisPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMEnvironmentalSurveyPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMFinalFailureAnalysisPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMFracStimulationPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMGravelPackPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMHaulFluidPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMLithologyPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMMudResistivityAnalysisPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMOperatingParametersPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMPlanAnalysisPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMPlanAnalysis1PropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMPlanAnalysis2PropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMPlanAnalysis3PropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMPlanAnalysis4PropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMPlanAnalysis5PropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMPostJobAnalysisPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMPressureTestPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMSafetyIncidentPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMSafetyPersonnelPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                ////if (!SyncExtendedEvents(new WSMScheduledActivityPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMShipReceivePropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMWaitingOnPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMWellborePropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMWellDirectionalSurveyPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMWellFluidLevelsPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMWellLogPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMWellPerforationPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMWellStatusChangePropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMWellTestsPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMWSPullPropertiesTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
				
////                //if (!SyncExtendedEvents(new WSMEventDetailChemAnlWaterTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMAssemblyComponentFailureTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMEventDetailDirectionalSurvey(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                ////if (!SyncExtendedEvents(new WSMEventDetailScheduleActivity(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMEventDetailCostsTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMEventDetailCasingTubingTallyTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMEventDetailBHPSurveyTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMEventDetailChemAnlSolidsTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                //if (!SyncExtendedEvents(new WSMEventDetailChemAnlGasTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                ////if (!SyncExtendedEvents(new WSMEventDetailPlanAnalysisTransaction(0, 0, syncFrom), cache, dataMgr, isFullSync)) success = false;

////                // Process Deleted Entries
////                if (!isFullSync)
////                {
////                    List<EventTypeDataValue> dataValues = dataManagerWS.GetData(new EventTypeDataValueTransaction(dataMgr.SourceDB, true));

////                    // EXTENDED ITEMS
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventAcidizeMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventAssembleSRPumpMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventBHPSurveyMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventCasingTubingTallyMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventCementMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventCementPlugMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventCementSqueezeMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventChemAnlBacteriaMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventChemAnlCaliperMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventChemAnlCathodicMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventChemAnlCouponsMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventChemAnlFailedPartMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventChemAnlGasMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventChemAnlGrindoutMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventChemAnlIronMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventChemAnlMilliporeMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventChemAnlOilMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventChemAnlOilCarryoverMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventChemAnlResidualMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventChemAnlSolidsMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventChemAnlWaterMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventChemInventoryTransMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventChemPhysInventoryMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventChemTrtHistoryMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventChemTrtPlanMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventChemTrtSchedMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventCleanFillMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventCleanPerfsMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventDisassembleSRPumpMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventDrillingMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventDrillingBitMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventDrillingMudMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventDrillingMudPumpMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventDrillStemTestMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventEconomicAnalysisMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventEnvironmentalSurveyMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventFinalFailureAnalysisMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventFracStimulationMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventGravelPackMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventHaulFluidMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventLithologyMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventMudResistivityAnalysisMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventOperatingParametersMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventPlanAnalysisMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventPlanAnalysis1Map.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventPlanAnalysis2Map.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventPlanAnalysis3Map.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventPlanAnalysis4Map.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventPlanAnalysis5Map.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventPostJobAnalysisMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventPressureTestMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventSafetyIncidentMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventSafetyPersonnelMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    ////if (!SyncDeletedEvents(new EventScheduledActivityMap.TABLE_NAME, syncFrom), cache, dataMgr, isFullSync)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventShipReceiveMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventWaitingOnMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventWellboreMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventWellDirectionalSurveyMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventWellFluidLevelsMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventWellLogMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventWellPerforationMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventWellStatusChangeMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventWellTestsMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventWSPullMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;

////                    //// DETAIL ITEMS
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventDetailCostsMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventDetailCasingTubingTallyMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventDetailDirectionalSurveyMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventDetailBHPSurveyMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventDetailChemAnlSolidsMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                    //if (!SyncDeletedEvents(new DeletedLogInfoTransaction(EventDetailChemAnlGasMap.TABLE_NAME, syncFrom), dataValues, dataMgr, dataManagerWS)) success = false;
////                }

////                #endregion

//            //    // remove temporarily cached items
//            //    cache.Wells.Clear();
//            //    cache.Completions.Clear();
//            //    cache.Events.Clear();
//            //    cache.Jobs.Clear();
//            //    cache.Plans.Clear();
//            //    cache.EventTypeDataValues.Clear();

//            //    if (success)
//            //        SetLastSync(dataMgr.SourceDB, syncTime);
//            //    else if (logger.IsWarnEnabled) logger.Info("WSM Data Sync was not successful, so last sync date was not updated.");
//            //}

//            //#endregion

//            // reset sync timer to make sure there is a full sync interval between syncs
//            syncTimer.Stop();
//            syncTimer.Start();

//            if (logger.IsInfoEnabled) logger.Info("WSM Data Sync completed.");
//        }

//        //----------------------------------------------------------------------
//        /// <summary>
//        /// Syncs the picklist.
//        /// </summary>
//        /// <param name="trans">The trans.</param>
//        /// <param name="dataMgrMaster">The data MGR master.</param>
//        /// <param name="masterCache">The master cache.</param>
//        /// <param name="isFullSync">if set to <c>true</c> [is full sync].</param>
//        /// <param name="name">The name.</param>
//        /// <param name="description">The description.</param>
//        /// <param name="type">The type.</param>
//        /// <returns></returns>
//        //private bool SyncPicklist(IReaderTransaction<PicklistMember> trans, DataManager dataMgrMaster, DataCache masterCache, bool isFullSync, string name, string description)
//        //{
//        //    try
//        //    {
//        //        if (logger.IsDebugEnabled) logger.DebugFormat("Syncing {0} picklist members...", name);

//        //        Picklist picklist = DataCache.Picklists.GetItemByXRef(trans.Table);
//        //        if (picklist == null)
//        //        {
//        //            picklist = new Picklist();
//        //            picklist.ID = new Guid();
//        //            picklist.XRefID = trans.Table;
//        //            picklist.Name = name;
//        //            picklist.Description = description;
//        //            picklist.LastModified = DateTime.Now.ToUniversalTime();
//        //        }

//        //        List<PicklistMember> members = dataMgrMaster.GetData(trans) ;

//        //        if (trans.GetType() == typeof(WSMCatalogItemPicklistTransaction) || trans.GetType() == typeof(WSMCatalogItemPricePicklistTransaction))
//        //        {
//        //            foreach (PicklistMember member in members)
//        //            {
//        //                CompanyInfo company = DataCache.Companies.GetItemByXRef(member.FilterXRefID);

//        //                if(company != null) member.FilterID = company.ID;
//        //            }
//        //        }

//        //        // Merge/cache picklists
//        //        MergeDataSourceToCache(new List<Picklist>() { picklist }, DataCache.Picklists, masterCache.PopulateCommonData, isFullSync);
//        //        MergeDataSourceToCache(members, DataCache.PicklistMembers, masterCache.PopulatePicklistMemberRefData, isFullSync);

//        //        return true;
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        if (logger.IsErrorEnabled) logger.Error("Syncing picklist members failed.", ex);
//        //        return false;
//        //    }
//        //}

//        //----------------------------------------------------------------------
//        /// <summary>
//        /// Retrieves and flags the deleted state of Jobs
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="extendedEvents">The extended events.</param>
//        /// <param name="dataMgr">The data MGR.</param>
//        /// <returns></returns>
//        //public List<TransactionResult> UpdateExtendedEvent<T>(IEnumerable<ExtendedEvent> extendedEvents, DataManager dataMgr) where T: ITransaction, new()
//        //{
//        //    var transactions = new List<ITransaction>();

//        //    if (logger.IsDebugEnabled) logger.DebugFormat("{0} extended events of type {1} have been deleted", transactions.Count, typeof(T));

//        //    foreach (ExtendedEvent extendedEvent in extendedEvents)
//        //    {
//        //        if (extendedEvent.Deleted)
//        //        {
//        //            T transaction = (T) Activator.CreateInstance(typeof (T), new object[] {extendedEvent});

//        //            // Batch deleted items into transactions for Update
//        //            transactions.Add(transaction);
//        //        }
//        //    }

//        //    List<TransactionResult> results = dataMgr.PutDataBatch(transactions);

//        //    return results;
//        //}

//        //----------------------------------------------------------------------
//        /// <summary>
//        /// Syncs the deleted events.
//        /// </summary>
//        /// <param name="transaction">The transaction.</param>
//        /// <param name="updateItems">The update items.</param>
//        /// <param name="dataManagerWS">The data manager WS.</param>
//        /// <returns></returns>
//        //private bool SyncDeletedEvents(IReaderTransaction<DeletedLogInfo> transaction, IEnumerable<EventTypeDataValue> updateItems, DataManager dataMgr, DataManager dataManagerWS)
//        //{
//        //    List<DeletedLogInfo> info = dataMgr.GetData(transaction);
//        //    if (info.Count == 0)
//        //        return true;
            
//        //    List<EventTypeDataValue> itemsToUpdate = new List<EventTypeDataValue>(updateItems);
//        //    SortedList<string, EventTypeDataValue> toDelete = new SortedList<string, EventTypeDataValue>();

//        //    foreach (EventTypeDataValue item in itemsToUpdate)
//        //    {
//        //        foreach (DeletedLogInfo infoItem in info)
//        //        {
//        //            // if deleted in WSM, queue this item for delete in WS
//        //            if (item.XRefID.EndsWith(infoItem.DeletedPK) && !toDelete.ContainsKey(item.XRefID))
//        //            {
//        //                toDelete.Add(item.XRefID, item);
//        //                logger.DebugFormat("Preparing to deleted EventTypeDataValue: '{0}' from WSM Table: '{1}'", item.XRefID, transaction.Table);
//        //            }
//        //        }
//        //    }

//        //    bool success = true;
//        //    foreach (KeyValuePair<string, EventTypeDataValue> pair in toDelete)
//        //    {
//        //        TransactionResult result = dataManagerWS.InvokeRawNonQuery(string.Format("UPDATE {0} SET [Deleted]=1, [LastModified]='{1}' WHERE {2} = '{3}' AND SourceDB = '{4}'", EventTypeDataValuesMap.TABLE_NAME, DateTime.Now.ToUniversalTime(), EventTypeDataValuesMap.XRefID, pair.Key, dataMgr.SourceDB));
//        //        if (result.Success == false)
//        //        {
//        //            logger.ErrorFormat("Failed to delete EventTypeDataValue '{0}' with Message: {1}.", pair.Key, result.Message);
//        //            success = false;
//        //        }
//        //    }

//        //    return success;
//        //}

//        //----------------------------------------------------------------------
//        //private bool SyncExtendedEvents(IReaderTransaction<ExtendedEvent> extendedEventTrans, DataCache cache, DataManager dataMgr, bool isBulkCopy)
//        //{
//        //    try
//        //    {
//        //        if (logger.IsDebugEnabled) logger.DebugFormat("Syncing extended event table '{0}' from '{1}'...", extendedEventTrans.Table, dataMgr.SourceName);
//        //        List<ExtendedEvent> extendedEvents = dataMgr.GetData(extendedEventTrans);
//        //        List<EventTypeDataValue> dataValues = new List<EventTypeDataValue>();

//        //        foreach (ExtendedEvent extendedEvent in extendedEvents)
//        //        {
//        //            string sourceKey = extendedEvent.Name + ".";
//        //            EventInfo evt = null;
//        //            evt = cache.Events.GetItemByXRef(extendedEvent.EventXRefID);
					
//        //            if (evt == null)
//        //            {
//        //                if (logger.IsWarnEnabled) logger.Warn(String.Format("Extended event '{0}' could not find parent event '{1}'.", extendedEvent.XRefID, extendedEvent.EventXRefID));
//        //                continue;
//        //            }

//        //            //Guid groupID = Guid.NewGuid();
//        //            foreach (KeyValuePair<string, object> kvp in extendedEvent.Properties)
//        //            {
//        //                EventTypeDataValue value = new EventTypeDataValue();
//        //                //value.EventTypeValueGroupID = groupID;
//        //                value.XRefID = evt.EventTypeXRefID + extendedEvent.XRefID;
//        //                value.PropertyID = DataCache.EventTypePropertyMaps.PropertyIDsBySource[sourceKey + kvp.Key];
//        //                value.EventID = evt.ID;
//        //                value.PropertyValue = kvp.Value;
//        //                value.LastModified = DateTime.Now.ToUniversalTime();
//        //                value.SourceDB = dataMgr.SourceDB;
//        //                value.Deleted = evt.Deleted;

//        //                dataValues.Add(value);
//        //            }
//        //        }

//        //        MergeDataSourceToCache(dataValues, cache.EventTypeDataValues, null, isBulkCopy);

//        //        return true;
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        if (logger.IsErrorEnabled) logger.Error("Syncing extended events failed.", ex);
//        //        return false;
//        //    }
//        //}

//        //----------------------------------------------------------------------
//        /// <summary>
//        /// Populates the deleted log items for the specified type and updates the deleted flag in WSM
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="deleteTransaction">The delete transaction for the DeletedLogInfo index</param>
//        /// <param name="updateItems">The WSM items to be updated.</param>
//        /// <param name="updateHandler">The update handler.</param>
//        /// <param name="wsmDataMgr">The ws data MGR.</param>
//        //private static void PopulateDeletedLogItems<T>(IReaderTransaction<DeletedLogInfo> deleteTransaction, IEnumerable<T> updateItems, Func<IEnumerable<T>, IEnumerable<TransactionResult>> updateHandler, DataManager wsmDataMgr) where T : XRData<T>
//        //{
//        //    List<DeletedLogInfo> info = wsmDataMgr.GetData(deleteTransaction);

//        //    List<T> itemsToUpdate = new List<T>(updateItems);
//        //    foreach (T item in itemsToUpdate)
//        //    {
//        //        T wsItem = item;
//        //        item.Deleted = (info.Find(i => wsItem.XRefID == i.DeletedPK) != null);
//        //        // Indicate that there is a modification so that the item will be synced.
//        //        item.LastModified = DateTime.Now.ToUniversalTime();
//        //    }

//        //    IEnumerable<TransactionResult> results = updateHandler(itemsToUpdate);
//        //}

//        //----------------------------------------------------------------------
//        private List<T> MergeDataSourceToCache<T>(IEnumerable<T> wsmItems, XRCache<T> mergeIntoItems, Action<List<T>> populateXRefData, bool isBulkCopy) where T : XRData<T>
//        {
//            return MergeDataSourceToCache(wsmItems, mergeIntoItems, populateXRefData, "XRefID", isBulkCopy);
//        }

//        //----------------------------------------------------------------------
//        private List<T> MergeDataSourceToCache<T>(IEnumerable<T> wsmItems, XRCache<T> mergeIntoItems, Action<List<T>> populateXRefData, string foreignKeyUpdateColumn, bool isBulkCopy) where T : XRData<T>
//        {
//            List<T> changedItems = DataConverter.Diff(mergeIntoItems, wsmItems, populateXRefData);
//            DataManager dataMgrWS = persistenceMgr.GetWSDataManager();

//            ProcessTransactions(changedItems, isBulkCopy, foreignKeyUpdateColumn, dataMgrWS);

//            DataConverter.Merge(mergeIntoItems, changedItems);

//            return changedItems;
//        }

//        //----------------------------------------------------------------------
//        private void ProcessTransactions<T>(List<T> changedItems, bool isBulkCopy, string foreignKeyUpdateColumn, DataManager dataMgrWS) where T : XRData<T>
//        {
//            int index = 0;
//            while (index < changedItems.Count)
//            {
//                List<ITransaction> transactions = new List<ITransaction>();
//                for(int j = index; j != changedItems.Count; j++)
//                {
//                    // if this is not a bulk copy, determine if object exists based on SourceDB and XRefID
//                    if (!isBulkCopy)
//                    {
//                        Dictionary<string, object> existsIDResults = dataMgrWS.GetFirstDataResult(changedItems[j].GetExistsTransaction(foreignKeyUpdateColumn));
    					
//                        // if it exists, lookup ID and and set it in current item
//                        if (existsIDResults != null && existsIDResults.Count > 0)
//                        {
//                            Guid id = (Guid)existsIDResults[DataTagMapping.DataTagIDColumns[BaseData<T>.DataTag]];
//                            changedItems[j].ID = id;
//                        }
//                    }

//                    ITransaction trans = TransactionFactory.BuildXRPutTransaction(BaseData<T>.DataTag, changedItems[j]);
//                    transactions.Add(trans);
//                    index++;

//                    if (index % 10000 == 0) break;
//                }

                
//                if (isBulkCopy)
//                {
//                    // try bulk copy
//                    TransactionResult result = dataMgrWS.DoBulkCopy(transactions);
//                    if (result.Success == false)
//                    {
//                        if (logger.IsWarnEnabled) logger.WarnFormat("SQL Bulk Copy failed, attempting batch merge. {0}", result.Message);

//                        // fall-back to batch insert
//                        if (logger.IsDebugEnabled) logger.Debug("Attempting batch merge as backup to bulk copy.");
//                        List<TransactionResult> results = dataMgrWS.PutDataBatch(transactions);
//                        LogTransactionResults(results);
//                    }
//                }
//                else
//                {
//                    List<TransactionResult> results = dataMgrWS.PutDataBatch(transactions);
//                    LogTransactionResults(results);
//                }

//                transactions.Clear();
//            }
//        }

//        //----------------------------------------------------------------------
//        private void OnSyncTick(object sender, ElapsedEventArgs e)
//        {
//            if (IsSyncing)
//                return;
//            try
//            {
//                DoDataSync();
//            }
//            finally
//            {
//                IsSyncing = false;
//            }
//        }

//        //----------------------------------------------------------------------
//        private void LogTransactionResults(IEnumerable<TransactionResult> results)
//        {
//            foreach (TransactionResult result in results)
//            {
//                if (!result.Success)
//                    if (logger.IsErrorEnabled) logger.ErrorFormat("Failed to commit transaction.\r\n TRANSACTION INFO -- {0}\r\nMESSAGE -- {1}.", result.TransactionInfo, result.Message);
//            }
//        }
//    }
}