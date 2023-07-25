using System;
using System.Data;
using LFI.Sync.DataManager;

namespace LFI.Sync.Shared
{
    public class SyncTrackingTransaction : BaseTransaction<SyncTracking>
    {
        private Guid? _dataSourceID;
        private int _syncType;

        //----------------------------------------------------------------------
        /// <summary>
        /// Initializes a new instance of the <see cref="SyncTrackingTransaction"/> class.
        /// </summary>
        public SyncTrackingTransaction( Guid dataSourceID )
            : base( SyncTrackingMap.TABLE_NAME, SyncTrackingMap.SyncTrackingID )
        {
            _dataSourceID = dataSourceID;
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Initializes a new instance of the <see cref="SyncTrackingTransaction"/> class.
        /// </summary>
        public SyncTrackingTransaction( )
            : base( SyncTrackingMap.TABLE_NAME, SyncTrackingMap.SyncTrackingID )
        {
            _dataSourceID = null;
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Initializes a new instance of the <see cref="SyncTrackingTransaction"/> class.
        /// </summary>
        /// <param name="syncTracking">The sync tracking.</param>
        public SyncTrackingTransaction( IBaseData syncTracking, int syncType )
            : base( syncTracking, SyncTrackingMap.TABLE_NAME, SyncTrackingMap.SyncTrackingID )
        {
            _syncType = syncType;
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Registers the select columns.
        /// </summary>
        protected override void RegisterSelectColumns( )
        {
            AddColumn( SyncTrackingMap.SyncTrackingID );
            AddColumn( SyncTrackingMap.DataTagID );
            AddColumn( SyncTrackingMap.LastSuccessfulSync );
            AddColumn( SyncTrackingMap.SyncType );
            AddColumn( SyncTrackingMap.DataSourceID );
            AddColumn( SyncTrackingMap.DataTag );
        }

        //----------------------------------------------------------------------
        protected override string BuildWhere( )
        {
            if( _dataSourceID == null )
                return base.BuildWhere( );

            return String.Format( "WHERE DataSourceID = '{0}' OR DataSourceID IS NULL", _dataSourceID );
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Override this function to add parameters for INSERT/UPDATE statements.
        /// This is the ideal place to call AddParam() and AddPrimayKey() functions to build INSERT/UPDATE statements.
        /// </summary>
        public override void RegisterParams( )
        {
            SyncTracking syncTracking = ( SyncTracking ) dataObj;

            AddParam( SyncTrackingMap.SyncType, SyncTrackingMap.Param.SyncType, _syncType );
            AddParam( SyncTrackingMap.DataTagID, SyncTrackingMap.Param.DataTagID, syncTracking.DataTagID );
            AddParam( SyncTrackingMap.DataSourceID, SyncTrackingMap.Param.DataSourceID, syncTracking.DataSourceID );
            AddParam( SyncTrackingMap.LastSuccessfulSync, SyncTrackingMap.Param.LastSuccessfulSync, syncTracking.LastSync );
            AddParam( SyncTrackingMap.DataTag, SyncTrackingMap.Param.DataTag, syncTracking.DataTag );
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Required function. This is used to build the output object from a data reader row.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns>
        /// Object of type T specified that has been constructed from the provided reader.
        /// </returns>
        public override SyncTracking BuildFromReader( TransactionReader reader )
        {
            SyncTracking syncTracking = new SyncTracking
            {
                SyncTrackingID = reader.TryReadGuid( SyncTrackingMap.SyncTrackingID ),
                DataTagID = reader.TryReadGuid( SyncTrackingMap.DataTagID ),
                LastSync = reader.TryReadDate( SyncTrackingMap.LastSuccessfulSync ),
                SyncType = reader.TryReadInt( SyncTrackingMap.SyncType ),
                DataSourceID = reader.TryReadGuidWithNull( SyncTrackingMap.DataSourceID ),
                DataTag = reader.TryReadString( SyncTrackingMap.DataTag )
            };
            return syncTracking;
        }
    }
}