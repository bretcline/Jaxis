
namespace LFI.Sync.Shared
{
    public sealed class SyncTrackingMap : TableMap
    {
        public const string TABLE_NAME = "SYNCTRACKING";
        public static string SyncTrackingID = "SyncTrackingID";
        public static string DataTagID = "DataTagID";
        public static string SyncType = "SyncType";
        public static string LastSuccessfulSync = "LastSuccessfulSync";
        public static string DataSourceID = "DataSourceID";
        public static string DataTag = "DataTag";

        public sealed class Prefixed
        {
            public static string SyncTrackingID = "SyncTracking.SyncTrackingID";
            public static string DataTagID = "SyncTracking.DataTagID";
            public static string SyncType = "SyncTracking.SyncType";
            public static string LastSuccessfulSync = "SyncTracking.LastSuccessfulSync";
            public static string DataSourceID = "SyncTracking.DataSourceID";
            public static string DataTag = "SyncTracking.DataTag";
        }

        public sealed class Aliased
        {
            public static string SyncTrackingID = "SyncTrackingSyncTrackingID";
            public static string DataTagID = "SyncTrackingDataTagID";
            public static string SyncType = "SyncTrackingSyncType";
            public static string LastSuccessfulSync = "SyncTrackingLastSuccessfulSync";
            public static string DataSourceID = "SyncTrackingDataSourceID";
            public static string DataTag = "SyncTrackingDataTag";
        }

        public sealed class Param
        {
            public static string SyncTrackingID = "@SyncTrackingID";
            public static string DataTagID = "@DataTagID";
            public static string SyncType = "@SyncType";
            public static string LastSuccessfulSync = "@LastSuccessfulSync";
            public static string DataSourceID = "@DataSourceID";
            public static string DataTag = "@DataTag";
        }

    }
}

