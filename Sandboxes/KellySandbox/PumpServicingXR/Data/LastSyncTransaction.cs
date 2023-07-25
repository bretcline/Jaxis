using System;
using LFI.Sync.DataManager;

namespace WFT.PSService.Data
{
    //public class LastSyncTransaction : BaseTransaction<DateTime?>
    //{
        //private const string WHERE_SQL = "WHERE DataSourceID = {0}";
        //private const string TABLENAME = "DataSources";

        //private static class Column
        //{
        //    public const string DataSourceID = "DataSourceID";
        //    public const string LastSync = "LastSync";
        //}

        //private Guid dataSourceID = Guid.Empty;
        //private DateTime _lastSync = DateTime.MinValue;

        ////----------------------------------------------------------------------
        ///// <summary>
        ///// SELECT constructor
        ///// </summary>
        ///// <param name="dataSourceID">ID of the datasource whose last sync time is being queried</param>
        //public LastSyncTransaction(Guid dataSourceID)
        //    : base(TABLENAME, Column.DataSourceID)
        //{
        //    this.dataSourceID = dataSourceID;
        //}
		
        ////----------------------------------------------------------------------
        ///// <summary>
        ///// UPDATE constructor
        ///// </summary>
        ///// <param name="dataSourceID">ID of the datasource whose last sync time is being updated</param>
        ///// <param name="lastSync">DateTime of the last sync</param>
        //public LastSyncTransaction(WFT.PSService.ServiceLibrary.DataSource lastSyncDataSource)
        //    : base(lastSyncDataSource, TABLENAME, Column.DataSourceID)
        //{
        //    this.dataSourceID = lastSyncDataSource.ID;
        //    _lastSync = lastSyncDataSource.LastSync ?? new DateTime(1970, 1, 1);
        //}

        ////----------------------------------------------------------------------
        //protected override void RegisterSelectColumns()
        //{
        //    AddColumn(Column.LastSync);
        //}

        ////----------------------------------------------------------------------
        //protected override string BuildWhere()
        //{
        //    return String.Format(WHERE_SQL, dataSourceID);
        //}

        ////----------------------------------------------------------------------
        //public override void RegisterParams()
        //{
        //    AddParam(Column.LastSync, "@" + Column.LastSync, _lastSync);
        //}

        //----------------------------------------------------------------------
    //    public override DateTime? BuildFromReader( TransactionReader reader )
    //    {
    //        DateTime? lastSync = reader.TryReadDateWithNull( "LastModified" );
    //        return lastSync;
    //    }
    //}
}
