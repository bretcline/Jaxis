using LFI.Sync.Shared;
namespace WFT.PSService.Data.Mappings
{
    public sealed class DataSourcesMap : TableMap
    {
        public const string TABLE_NAME = "DATASOURCES";
        public static string DataSourceID = "DataSourceID";
        public static string DataSourceConfigID = "DataSourceConfigID";
        public static string Name = "Name";
        public static string Server = "Server";
        public static string Instance = "Instance";
        public static string Deleted = "Deleted";
        public static string LastModified = "LastModified";
        public static string LastSync = "LastSync";

        public sealed class Prefixed
        {
            public static string DataSourceID = "DataSources.DataSourceID";
            public static string DataSourceConfigID = "DataSources.DataSourceConfigID";
            public static string Name = "DataSources.Name";
            public static string Server = "DataSources.Server";
            public static string Instance = "DataSources.Instance";
            public static string Deleted = "DataSources.Deleted";
            public static string LastModified = "DataSources.LastModified";
            public static string LastSync = "DataSources.LastSync";
        }

        public sealed class Aliased
        {
            public static string DataSourceID = "DataSourcesDataSourceID";
            public static string DataSourceConfigID = "DataSourcesDataSourceConfigID";
            public static string Name = "DataSourcesName";
            public static string Server = "DataSourcesServer";
            public static string Instance = "DataSourcesInstance";
            public static string Deleted = "DataSourcesDeleted";
            public static string LastModified = "DataSourcesLastModified";
            public static string LastSync = "DataSourcesLastSync";
        }

        public sealed class Param
        {
            public static string DataSourceID = "@DataSourceID";
            public static string DataSourceConfigID = "@DataSourceConfigID";
            public static string Name = "@Name";
            public static string Server = "@Server";
            public static string Instance = "@Instance";
            public static string Deleted = "@Deleted";
            public static string LastModified = "@LastModified";
            public static string LastSync = "@LastSync";
        }

        public static string OrderBy_Name = "ORDER BY DataSources.Name";
    }
}

