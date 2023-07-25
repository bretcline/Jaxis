


using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using SubSonic.DataProviders;
using SubSonic.Extensions;
using SubSonic.Linq.Structure;
using SubSonic.Query;
using SubSonic.Schema;
using System.Data.Common;
using System.Collections.Generic;

namespace Jaxis.Inventory.Data
{
    public partial class BeverageMonitorDB : IQuerySurface
    {

        [NonSerialized]
        public IDataProvider DataProvider;
        [NonSerialized]
        public DbQueryProvider provider;
        
        public bool TestMode
		{
            get
			{
                return DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            }
        }

        public BeverageMonitorDB() 
        { 
            DataProvider = ProviderFactory.GetProvider("BeverageMonitor");
            Init();
        }

        public BeverageMonitorDB(string connectionStringName)
        {
            DataProvider = ProviderFactory.GetProvider(connectionStringName);
            Init();
        }

		public BeverageMonitorDB(string connectionString, string providerName)
        {
            DataProvider = ProviderFactory.GetProvider(connectionString,providerName);
            Init();
        }

		public ITable FindByPrimaryKey(string pkName)
        {
            return DataProvider.Schema.Tables.SingleOrDefault(x => x.PrimaryKey.Name.Equals(pkName, StringComparison.InvariantCultureIgnoreCase));
        }

        public Query<T> GetQuery<T>()
        {
            return new Query<T>(provider);
        }
        
        public ITable FindTable(string tableName)
        {
            return DataProvider.FindTable(tableName);
        }
        
        [IgnoreDataMember]
        public IDataProvider Provider
        {
            get { return DataProvider; }
            set {DataProvider=value;}
        }
        
        [IgnoreDataMember]
        public DbQueryProvider QueryProvider
        {
            get { return provider; }
        }
        
        BatchQuery _batch = null;
        public void Queue<T>(IQueryable<T> qry)
        {
            if (_batch == null)
                _batch = new BatchQuery(Provider, QueryProvider);
            _batch.Queue(qry);
        }

        public void Queue(ISqlQuery qry)
        {
            if (_batch == null)
                _batch = new BatchQuery(Provider, QueryProvider);
            _batch.Queue(qry);
        }

        public void ExecuteTransaction(IList<DbCommand> commands)
		{
            if(!TestMode)
			{
                using(var connection = commands[0].Connection)
				{
                   if (connection.State == ConnectionState.Closed)
                        connection.Open();
                   
                   using (var trans = connection.BeginTransaction()) 
				   {
                        foreach (var cmd in commands) 
						{
                            cmd.Transaction = trans;
                            cmd.Connection = connection;
                            cmd.ExecuteNonQuery();
                        }
                        trans.Commit();
                    }
                    connection.Close();
                }
            }
        }

        public IDataReader ExecuteBatch()
        {
            if (_batch == null)
                throw new InvalidOperationException("There's nothing in the queue");
            if(!TestMode)
                return _batch.ExecuteReader();
            return null;
        }
			
        public Query<ActivityLog> ActivityLogs { get; set; }
        public Query<AdministrativeValue> AdministrativeValues { get; set; }
        public Query<AlertGroupByRecipient> AlertGroupByRecipients { get; set; }
        public Query<AlertGroup> AlertGroups { get; set; }
        public Query<AlertSubscription> AlertSubscriptions { get; set; }
        public Query<Category> Categories { get; set; }
        public Query<DatabaseVersion> DatabaseVersions { get; set; }
        public Query<DeviceAlert> DeviceAlerts { get; set; }
        public Query<Device> Devices { get; set; }
        public Query<EmailRecipient> EmailRecipients { get; set; }
        public Query<Group> Groups { get; set; }
        public Query<GroupsXSecurableItem> GroupsXSecurableItems { get; set; }
        public Query<GroupXReport> GroupXReports { get; set; }
        public Query<Ingredient> Ingredients { get; set; }
        public Query<Inventory> Inventories { get; set; }
        public Query<Location> Locations { get; set; }
        public Query<Manufacturer> Manufacturers { get; set; }
        public Query<Move> Moves { get; set; }
        public Query<Organization> Organizations { get; set; }
        public Query<ParLevel> ParLevels { get; set; }
        public Query<PeriodicReportGroupByRecipient> PeriodicReportGroupByRecipients { get; set; }
        public Query<PeriodicReportGroup> PeriodicReportGroups { get; set; }
        public Query<PeriodicReport> PeriodicReports { get; set; }
        public Query<POSTicketItemModifier> POSTicketItemModifiers { get; set; }
        public Query<POSTicketItem> POSTicketItems { get; set; }
        public Query<POSTicket> POSTickets { get; set; }
        public Query<Pour> Pours { get; set; }
        public Query<Quality> Qualities { get; set; }
        public Query<Recipe> Recipes { get; set; }
        public Query<ReportParameter> ReportParameters { get; set; }
        public Query<Report> Reports { get; set; }
        public Query<rptCostAnalysi> rptCostAnalysis { get; set; }
        public Query<rptDisconnectWithVolume> rptDisconnectWithVolumes { get; set; }
        public Query<rptInventory> rptInventories { get; set; }
        public Query<rptPourTotal> rptPourTotals { get; set; }
        public Query<rptSummaryByGroup> rptSummaryByGroups { get; set; }
        public Query<rptSummaryByTag> rptSummaryByTags { get; set; }
        public Query<rptSummaryByUPC> rptSummaryByUPCs { get; set; }
        public Query<rptTaggedInventory> rptTaggedInventories { get; set; }
        public Query<rptTagPourTotal> rptTagPourTotals { get; set; }
        public Query<SecurableItem> SecurableItems { get; set; }
        public Query<SizeType> SizeTypes { get; set; }
        public Query<StandardNozzle> StandardNozzles { get; set; }
        public Query<StandardPour> StandardPours { get; set; }
        public Query<StandardPrice> StandardPrices { get; set; }
        public Query<TagActivity> TagActivities { get; set; }
        public Query<TagAlert> TagAlerts { get; set; }
        public Query<TagMove> TagMoves { get; set; }
        public Query<Tag> Tags { get; set; }
        public Query<TicketItemAlias> TicketItemAliases { get; set; }
        public Query<UPC> UPCS { get; set; }
        public Query<User> Users { get; set; }
        public Query<UserSession> UserSessions { get; set; }
        public Query<UsersXGroup> UsersXGroups { get; set; }
        public Query<UsersXOrganization> UsersXOrganizations { get; set; }
        public Query<vwDefaultNozzleValue> vwDefaultNozzleValues { get; set; }
        public Query<vwFullBottleInventory> vwFullBottleInventories { get; set; }
        public Query<vwInventoryItem> vwInventoryItems { get; set; }
        public Query<vwItemsCategoryQuantity> vwItemsCategoryQuantities { get; set; }
        public Query<vwManufacturer> vwManufacturers { get; set; }
        public Query<vwMissingInventory> vwMissingInventories { get; set; }
        public Query<vwParCount> vwParCounts { get; set; }
        public Query<vwParLevelIssue> vwParLevelIssues { get; set; }
        public Query<vwPartialBottleInventory> vwPartialBottleInventories { get; set; }
        public Query<vwPosPour> vwPosPours { get; set; }
        public Query<vwPOSTicketItem> vwPOSTicketItems { get; set; }
        public Query<vwPour> vwPours { get; set; }
        public Query<vwReader> vwReaders { get; set; }
        public Query<vwTicketItemAlias> vwTicketItemAliases { get; set; }
        public Query<vwUPCItem> vwUPCItems { get; set; }

			

        #region ' Aggregates and SubSonic Queries '
        public Select SelectColumns(params string[] columns)
        {
            return new Select(DataProvider, columns);
        }

        public Select Select
        {
            get { return new Select(this.Provider); }
        }

        public Insert Insert
		{
            get { return new Insert(this.Provider); }
        }

        public Update<T> Update<T>() where T:new()
		{
            return new Update<T>(this.Provider);
        }

        public SqlQuery Delete<T>(Expression<Func<T,bool>> column) where T:new()
        {
            LambdaExpression lamda = column;
            SqlQuery result = new Delete<T>(this.Provider);
            result = result.From<T>();
            result.Constraints=lamda.ParseConstraints().ToList();
            return result;
        }

        public SqlQuery Max<T>(Expression<Func<T,object>> column)
        {
            LambdaExpression lamda = column;
            string colName = lamda.ParseObjectValue();
            string objectName = typeof(T).Name;
            string tableName = DataProvider.FindTable(objectName).Name;
            return new Select(DataProvider, new Aggregate(colName, AggregateFunction.Max)).From(tableName);
        }

        public SqlQuery Min<T>(Expression<Func<T,object>> column)
        {
            LambdaExpression lamda = column;
            string colName = lamda.ParseObjectValue();
            string objectName = typeof(T).Name;
            string tableName = this.Provider.FindTable(objectName).Name;
            return new Select(this.Provider, new Aggregate(colName, AggregateFunction.Min)).From(tableName);
        }

        public SqlQuery Sum<T>(Expression<Func<T,object>> column)
        {
            LambdaExpression lamda = column;
            string colName = lamda.ParseObjectValue();
            string objectName = typeof(T).Name;
            string tableName = this.Provider.FindTable(objectName).Name;
            return new Select(this.Provider, new Aggregate(colName, AggregateFunction.Sum)).From(tableName);
        }

        public SqlQuery Avg<T>(Expression<Func<T,object>> column)
        {
            LambdaExpression lamda = column;
            string colName = lamda.ParseObjectValue();
            string objectName = typeof(T).Name;
            string tableName = this.Provider.FindTable(objectName).Name;
            return new Select(this.Provider, new Aggregate(colName, AggregateFunction.Avg)).From(tableName);
        }

        public SqlQuery Count<T>(Expression<Func<T,object>> column)
        {
            LambdaExpression lamda = column;
            string colName = lamda.ParseObjectValue();
            string objectName = typeof(T).Name;
            string tableName = this.Provider.FindTable(objectName).Name;
            return new Select(this.Provider, new Aggregate(colName, AggregateFunction.Count)).From(tableName);
        }

        public SqlQuery Variance<T>(Expression<Func<T,object>> column)
        {
            LambdaExpression lamda = column;
            string colName = lamda.ParseObjectValue();
            string objectName = typeof(T).Name;
            string tableName = this.Provider.FindTable(objectName).Name;
            return new Select(this.Provider, new Aggregate(colName, AggregateFunction.Var)).From(tableName);
        }

        public SqlQuery StandardDeviation<T>(Expression<Func<T,object>> column)
        {
            LambdaExpression lamda = column;
            string colName = lamda.ParseObjectValue();
            string objectName = typeof(T).Name;
            string tableName = this.Provider.FindTable(objectName).Name;
            return new Select(this.Provider, new Aggregate(colName, AggregateFunction.StDev)).From(tableName);
        }

        #endregion

        void Init()
        {
            provider = new DbQueryProvider(this.Provider);

            #region ' Query Defs '
            ActivityLogs = new Query<ActivityLog>(provider);
            AdministrativeValues = new Query<AdministrativeValue>(provider);
            AlertGroupByRecipients = new Query<AlertGroupByRecipient>(provider);
            AlertGroups = new Query<AlertGroup>(provider);
            AlertSubscriptions = new Query<AlertSubscription>(provider);
            Categories = new Query<Category>(provider);
            DatabaseVersions = new Query<DatabaseVersion>(provider);
            DeviceAlerts = new Query<DeviceAlert>(provider);
            Devices = new Query<Device>(provider);
            EmailRecipients = new Query<EmailRecipient>(provider);
            Groups = new Query<Group>(provider);
            GroupsXSecurableItems = new Query<GroupsXSecurableItem>(provider);
            GroupXReports = new Query<GroupXReport>(provider);
            Ingredients = new Query<Ingredient>(provider);
            Inventories = new Query<Inventory>(provider);
            Locations = new Query<Location>(provider);
            Manufacturers = new Query<Manufacturer>(provider);
            Moves = new Query<Move>(provider);
            Organizations = new Query<Organization>(provider);
            ParLevels = new Query<ParLevel>(provider);
            PeriodicReportGroupByRecipients = new Query<PeriodicReportGroupByRecipient>(provider);
            PeriodicReportGroups = new Query<PeriodicReportGroup>(provider);
            PeriodicReports = new Query<PeriodicReport>(provider);
            POSTicketItemModifiers = new Query<POSTicketItemModifier>(provider);
            POSTicketItems = new Query<POSTicketItem>(provider);
            POSTickets = new Query<POSTicket>(provider);
            Pours = new Query<Pour>(provider);
            Qualities = new Query<Quality>(provider);
            Recipes = new Query<Recipe>(provider);
            ReportParameters = new Query<ReportParameter>(provider);
            Reports = new Query<Report>(provider);
            rptCostAnalysis = new Query<rptCostAnalysi>(provider);
            rptDisconnectWithVolumes = new Query<rptDisconnectWithVolume>(provider);
            rptInventories = new Query<rptInventory>(provider);
            rptPourTotals = new Query<rptPourTotal>(provider);
            rptSummaryByGroups = new Query<rptSummaryByGroup>(provider);
            rptSummaryByTags = new Query<rptSummaryByTag>(provider);
            rptSummaryByUPCs = new Query<rptSummaryByUPC>(provider);
            rptTaggedInventories = new Query<rptTaggedInventory>(provider);
            rptTagPourTotals = new Query<rptTagPourTotal>(provider);
            SecurableItems = new Query<SecurableItem>(provider);
            SizeTypes = new Query<SizeType>(provider);
            StandardNozzles = new Query<StandardNozzle>(provider);
            StandardPours = new Query<StandardPour>(provider);
            StandardPrices = new Query<StandardPrice>(provider);
            TagActivities = new Query<TagActivity>(provider);
            TagAlerts = new Query<TagAlert>(provider);
            TagMoves = new Query<TagMove>(provider);
            Tags = new Query<Tag>(provider);
            TicketItemAliases = new Query<TicketItemAlias>(provider);
            UPCS = new Query<UPC>(provider);
            Users = new Query<User>(provider);
            UserSessions = new Query<UserSession>(provider);
            UsersXGroups = new Query<UsersXGroup>(provider);
            UsersXOrganizations = new Query<UsersXOrganization>(provider);
            vwDefaultNozzleValues = new Query<vwDefaultNozzleValue>(provider);
            vwFullBottleInventories = new Query<vwFullBottleInventory>(provider);
            vwInventoryItems = new Query<vwInventoryItem>(provider);
            vwItemsCategoryQuantities = new Query<vwItemsCategoryQuantity>(provider);
            vwManufacturers = new Query<vwManufacturer>(provider);
            vwMissingInventories = new Query<vwMissingInventory>(provider);
            vwParCounts = new Query<vwParCount>(provider);
            vwParLevelIssues = new Query<vwParLevelIssue>(provider);
            vwPartialBottleInventories = new Query<vwPartialBottleInventory>(provider);
            vwPosPours = new Query<vwPosPour>(provider);
            vwPOSTicketItems = new Query<vwPOSTicketItem>(provider);
            vwPours = new Query<vwPour>(provider);
            vwReaders = new Query<vwReader>(provider);
            vwTicketItemAliases = new Query<vwTicketItemAlias>(provider);
            vwUPCItems = new Query<vwUPCItem>(provider);
            #endregion


            #region ' Schemas '
        	if(DataProvider.Schema.Tables.Count == 0)
			{
				
				// Table: ActivityLogs
				// Primary Key: ActivityLogID
				ITable ActivityLogsSchema = new DatabaseTable("ActivityLogs", DataProvider) { ClassName = "ActivityLog", SchemaName = "dbo" };
            	ActivityLogsSchema.Columns.Add(new DatabaseColumn("ActivityLogID", ActivityLogsSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	ActivityLogsSchema.Columns.Add(new DatabaseColumn("ActivityIndex", ActivityLogsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	ActivityLogsSchema.Columns.Add(new DatabaseColumn("TagID", ActivityLogsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	ActivityLogsSchema.Columns.Add(new DatabaseColumn("DeviceID", ActivityLogsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	ActivityLogsSchema.Columns.Add(new DatabaseColumn("LocationID", ActivityLogsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	ActivityLogsSchema.Columns.Add(new DatabaseColumn("ActivityTime", ActivityLogsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.DateTime,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	ActivityLogsSchema.Columns.Add(new DatabaseColumn("SignalStrength", ActivityLogsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	ActivityLogsSchema.Columns.Add(new DatabaseColumn("ActivityType", ActivityLogsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	ActivityLogsSchema.Columns.Add(new DatabaseColumn("RawData", ActivityLogsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add ActivityLogs to schema
            	DataProvider.Schema.Tables.Add(ActivityLogsSchema);
				
				// Table: AdministrativeValues
				// Primary Key: AdministrativeValueID
				ITable AdministrativeValuesSchema = new DatabaseTable("AdministrativeValues", DataProvider) { ClassName = "AdministrativeValue", SchemaName = "dbo" };
            	AdministrativeValuesSchema.Columns.Add(new DatabaseColumn("AdministrativeValueID", AdministrativeValuesSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	AdministrativeValuesSchema.Columns.Add(new DatabaseColumn("PropertyName", AdministrativeValuesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	AdministrativeValuesSchema.Columns.Add(new DatabaseColumn("PropertyValue", AdministrativeValuesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add AdministrativeValues to schema
            	DataProvider.Schema.Tables.Add(AdministrativeValuesSchema);
				
				// Table: AlertGroupByRecipients
				// Primary Key: AlertGroupByRecipientID
				ITable AlertGroupByRecipientsSchema = new DatabaseTable("AlertGroupByRecipients", DataProvider) { ClassName = "AlertGroupByRecipient", SchemaName = "dbo" };
            	AlertGroupByRecipientsSchema.Columns.Add(new DatabaseColumn("AlertGroupByRecipientID", AlertGroupByRecipientsSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	AlertGroupByRecipientsSchema.Columns.Add(new DatabaseColumn("AlertGroupID", AlertGroupByRecipientsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	AlertGroupByRecipientsSchema.Columns.Add(new DatabaseColumn("EmailRecipientID", AlertGroupByRecipientsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
				// Add AlertGroupByRecipients to schema
            	DataProvider.Schema.Tables.Add(AlertGroupByRecipientsSchema);
				
				// Table: AlertGroups
				// Primary Key: AlertGroupID
				ITable AlertGroupsSchema = new DatabaseTable("AlertGroups", DataProvider) { ClassName = "AlertGroup", SchemaName = "dbo" };
            	AlertGroupsSchema.Columns.Add(new DatabaseColumn("AlertGroupID", AlertGroupsSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	AlertGroupsSchema.Columns.Add(new DatabaseColumn("Name", AlertGroupsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add AlertGroups to schema
            	DataProvider.Schema.Tables.Add(AlertGroupsSchema);
				
				// Table: AlertSubscriptions
				// Primary Key: AlertSubscriptionID
				ITable AlertSubscriptionsSchema = new DatabaseTable("AlertSubscriptions", DataProvider) { ClassName = "AlertSubscription", SchemaName = "dbo" };
            	AlertSubscriptionsSchema.Columns.Add(new DatabaseColumn("AlertSubscriptionID", AlertSubscriptionsSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	AlertSubscriptionsSchema.Columns.Add(new DatabaseColumn("AlertGroupID", AlertSubscriptionsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	AlertSubscriptionsSchema.Columns.Add(new DatabaseColumn("AlertType", AlertSubscriptionsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	AlertSubscriptionsSchema.Columns.Add(new DatabaseColumn("Title", AlertSubscriptionsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	AlertSubscriptionsSchema.Columns.Add(new DatabaseColumn("Body", AlertSubscriptionsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add AlertSubscriptions to schema
            	DataProvider.Schema.Tables.Add(AlertSubscriptionsSchema);
				
				// Table: Categories
				// Primary Key: CategoryID
				ITable CategoriesSchema = new DatabaseTable("Categories", DataProvider) { ClassName = "Category", SchemaName = "dbo" };
            	CategoriesSchema.Columns.Add(new DatabaseColumn("CategoryID", CategoriesSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	CategoriesSchema.Columns.Add(new DatabaseColumn("ParentID", CategoriesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	CategoriesSchema.Columns.Add(new DatabaseColumn("Name", CategoriesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	CategoriesSchema.Columns.Add(new DatabaseColumn("Description", CategoriesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	CategoriesSchema.Columns.Add(new DatabaseColumn("StandardPriceID", CategoriesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	CategoriesSchema.Columns.Add(new DatabaseColumn("StandardNozzleID", CategoriesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = true
												});
				// Add Categories to schema
            	DataProvider.Schema.Tables.Add(CategoriesSchema);
				
				// Table: DatabaseVersion
				// Primary Key: 
				ITable DatabaseVersionSchema = new DatabaseTable("DatabaseVersion", DataProvider) { ClassName = "DatabaseVersion", SchemaName = "dbo" };
            	DatabaseVersionSchema.Columns.Add(new DatabaseColumn("DatabaseVersion", DatabaseVersionSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add DatabaseVersion to schema
            	DataProvider.Schema.Tables.Add(DatabaseVersionSchema);
				
				// Table: DeviceAlerts
				// Primary Key: DeviceAlertID
				ITable DeviceAlertsSchema = new DatabaseTable("DeviceAlerts", DataProvider) { ClassName = "DeviceAlert", SchemaName = "dbo" };
            	DeviceAlertsSchema.Columns.Add(new DatabaseColumn("DeviceAlertID", DeviceAlertsSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	DeviceAlertsSchema.Columns.Add(new DatabaseColumn("DeviceID", DeviceAlertsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	DeviceAlertsSchema.Columns.Add(new DatabaseColumn("AlertType", DeviceAlertsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	DeviceAlertsSchema.Columns.Add(new DatabaseColumn("Message", DeviceAlertsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	DeviceAlertsSchema.Columns.Add(new DatabaseColumn("AlertTime", DeviceAlertsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.DateTime,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add DeviceAlerts to schema
            	DataProvider.Schema.Tables.Add(DeviceAlertsSchema);
				
				// Table: Devices
				// Primary Key: DeviceID
				ITable DevicesSchema = new DatabaseTable("Devices", DataProvider) { ClassName = "Device", SchemaName = "dbo" };
            	DevicesSchema.Columns.Add(new DatabaseColumn("DeviceID", DevicesSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	DevicesSchema.Columns.Add(new DatabaseColumn("HardwareID", DevicesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	DevicesSchema.Columns.Add(new DatabaseColumn("Name", DevicesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	DevicesSchema.Columns.Add(new DatabaseColumn("Description", DevicesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	DevicesSchema.Columns.Add(new DatabaseColumn("Settings", DevicesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Xml,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add Devices to schema
            	DataProvider.Schema.Tables.Add(DevicesSchema);
				
				// Table: EmailRecipients
				// Primary Key: EmailRecipientID
				ITable EmailRecipientsSchema = new DatabaseTable("EmailRecipients", DataProvider) { ClassName = "EmailRecipient", SchemaName = "dbo" };
            	EmailRecipientsSchema.Columns.Add(new DatabaseColumn("EmailRecipientID", EmailRecipientsSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	EmailRecipientsSchema.Columns.Add(new DatabaseColumn("Name", EmailRecipientsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	EmailRecipientsSchema.Columns.Add(new DatabaseColumn("Email", EmailRecipientsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add EmailRecipients to schema
            	DataProvider.Schema.Tables.Add(EmailRecipientsSchema);
				
				// Table: Groups
				// Primary Key: GroupID
				ITable GroupsSchema = new DatabaseTable("Groups", DataProvider) { ClassName = "Group", SchemaName = "dbo" };
            	GroupsSchema.Columns.Add(new DatabaseColumn("GroupID", GroupsSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	GroupsSchema.Columns.Add(new DatabaseColumn("Name", GroupsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.AnsiString,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add Groups to schema
            	DataProvider.Schema.Tables.Add(GroupsSchema);
				
				// Table: GroupsXSecurableItems
				// Primary Key: GroupSecurableItemID
				ITable GroupsXSecurableItemsSchema = new DatabaseTable("GroupsXSecurableItems", DataProvider) { ClassName = "GroupsXSecurableItem", SchemaName = "dbo" };
            	GroupsXSecurableItemsSchema.Columns.Add(new DatabaseColumn("GroupSecurableItemID", GroupsXSecurableItemsSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	GroupsXSecurableItemsSchema.Columns.Add(new DatabaseColumn("GroupID", GroupsXSecurableItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	GroupsXSecurableItemsSchema.Columns.Add(new DatabaseColumn("SecurableItemID", GroupsXSecurableItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
				// Add GroupsXSecurableItems to schema
            	DataProvider.Schema.Tables.Add(GroupsXSecurableItemsSchema);
				
				// Table: GroupXReport
				// Primary Key: GroupXReportID
				ITable GroupXReportSchema = new DatabaseTable("GroupXReport", DataProvider) { ClassName = "GroupXReport", SchemaName = "dbo" };
            	GroupXReportSchema.Columns.Add(new DatabaseColumn("GroupXReportID", GroupXReportSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	GroupXReportSchema.Columns.Add(new DatabaseColumn("GroupID", GroupXReportSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	GroupXReportSchema.Columns.Add(new DatabaseColumn("ReportID", GroupXReportSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
				// Add GroupXReport to schema
            	DataProvider.Schema.Tables.Add(GroupXReportSchema);
				
				// Table: Ingredients
				// Primary Key: IngredientID
				ITable IngredientsSchema = new DatabaseTable("Ingredients", DataProvider) { ClassName = "Ingredient", SchemaName = "dbo" };
            	IngredientsSchema.Columns.Add(new DatabaseColumn("IngredientID", IngredientsSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	IngredientsSchema.Columns.Add(new DatabaseColumn("RecipeID", IngredientsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	IngredientsSchema.Columns.Add(new DatabaseColumn("Number", IngredientsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	IngredientsSchema.Columns.Add(new DatabaseColumn("Type", IngredientsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	IngredientsSchema.Columns.Add(new DatabaseColumn("UPCID", IngredientsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	IngredientsSchema.Columns.Add(new DatabaseColumn("Quality", IngredientsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	IngredientsSchema.Columns.Add(new DatabaseColumn("StandardPourID", IngredientsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	IngredientsSchema.Columns.Add(new DatabaseColumn("ManufacturerID", IngredientsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	IngredientsSchema.Columns.Add(new DatabaseColumn("CategoryID", IngredientsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = true
												});
				// Add Ingredients to schema
            	DataProvider.Schema.Tables.Add(IngredientsSchema);
				
				// Table: Inventories
				// Primary Key: InventoryID
				ITable InventoriesSchema = new DatabaseTable("Inventories", DataProvider) { ClassName = "Inventory", SchemaName = "dbo" };
            	InventoriesSchema.Columns.Add(new DatabaseColumn("InventoryID", InventoriesSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	InventoriesSchema.Columns.Add(new DatabaseColumn("UPCID", InventoriesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	InventoriesSchema.Columns.Add(new DatabaseColumn("LocationID", InventoriesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	InventoriesSchema.Columns.Add(new DatabaseColumn("Cost", InventoriesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Currency,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	InventoriesSchema.Columns.Add(new DatabaseColumn("EnterDate", InventoriesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.DateTime,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	InventoriesSchema.Columns.Add(new DatabaseColumn("ExitDate", InventoriesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.DateTime,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	InventoriesSchema.Columns.Add(new DatabaseColumn("TagDate", InventoriesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.DateTime,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	InventoriesSchema.Columns.Add(new DatabaseColumn("Amount", InventoriesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	InventoriesSchema.Columns.Add(new DatabaseColumn("TagID", InventoriesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	InventoriesSchema.Columns.Add(new DatabaseColumn("Memo", InventoriesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	InventoriesSchema.Columns.Add(new DatabaseColumn("ExitReason", InventoriesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	InventoriesSchema.Columns.Add(new DatabaseColumn("UpdateTime", InventoriesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.DateTime,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	InventoriesSchema.Columns.Add(new DatabaseColumn("UserSessionID", InventoriesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add Inventories to schema
            	DataProvider.Schema.Tables.Add(InventoriesSchema);
				
				// Table: Locations
				// Primary Key: LocationID
				ITable LocationsSchema = new DatabaseTable("Locations", DataProvider) { ClassName = "Location", SchemaName = "dbo" };
            	LocationsSchema.Columns.Add(new DatabaseColumn("LocationID", LocationsSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	LocationsSchema.Columns.Add(new DatabaseColumn("OrganizationID", LocationsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	LocationsSchema.Columns.Add(new DatabaseColumn("Name", LocationsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	LocationsSchema.Columns.Add(new DatabaseColumn("Description", LocationsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	LocationsSchema.Columns.Add(new DatabaseColumn("ParentID", LocationsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	LocationsSchema.Columns.Add(new DatabaseColumn("AllowHalfPour", LocationsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Boolean,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	LocationsSchema.Columns.Add(new DatabaseColumn("POSAlias", LocationsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	LocationsSchema.Columns.Add(new DatabaseColumn("DeviceID", LocationsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = true
												});
				// Add Locations to schema
            	DataProvider.Schema.Tables.Add(LocationsSchema);
				
				// Table: Manufacturers
				// Primary Key: ManufacturerID
				ITable ManufacturersSchema = new DatabaseTable("Manufacturers", DataProvider) { ClassName = "Manufacturer", SchemaName = "dbo" };
            	ManufacturersSchema.Columns.Add(new DatabaseColumn("ManufacturerID", ManufacturersSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	ManufacturersSchema.Columns.Add(new DatabaseColumn("Name", ManufacturersSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add Manufacturers to schema
            	DataProvider.Schema.Tables.Add(ManufacturersSchema);
				
				// Table: Moves
				// Primary Key: MoveID
				ITable MovesSchema = new DatabaseTable("Moves", DataProvider) { ClassName = "Move", SchemaName = "dbo" };
            	MovesSchema.Columns.Add(new DatabaseColumn("MoveID", MovesSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	MovesSchema.Columns.Add(new DatabaseColumn("MoveTime", MovesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.DateTime,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	MovesSchema.Columns.Add(new DatabaseColumn("OldLocation", MovesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	MovesSchema.Columns.Add(new DatabaseColumn("NewLocation", MovesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	MovesSchema.Columns.Add(new DatabaseColumn("TagID", MovesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	MovesSchema.Columns.Add(new DatabaseColumn("InventoryID", MovesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	MovesSchema.Columns.Add(new DatabaseColumn("Quantity", MovesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add Moves to schema
            	DataProvider.Schema.Tables.Add(MovesSchema);
				
				// Table: Organizations
				// Primary Key: OrganizationID
				ITable OrganizationsSchema = new DatabaseTable("Organizations", DataProvider) { ClassName = "Organization", SchemaName = "dbo" };
            	OrganizationsSchema.Columns.Add(new DatabaseColumn("OrganizationID", OrganizationsSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	OrganizationsSchema.Columns.Add(new DatabaseColumn("Name", OrganizationsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	OrganizationsSchema.Columns.Add(new DatabaseColumn("Description", OrganizationsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add Organizations to schema
            	DataProvider.Schema.Tables.Add(OrganizationsSchema);
				
				// Table: ParLevels
				// Primary Key: ParLevelID
				ITable ParLevelsSchema = new DatabaseTable("ParLevels", DataProvider) { ClassName = "ParLevel", SchemaName = "dbo" };
            	ParLevelsSchema.Columns.Add(new DatabaseColumn("ParLevelID", ParLevelsSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	ParLevelsSchema.Columns.Add(new DatabaseColumn("LocationID", ParLevelsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	ParLevelsSchema.Columns.Add(new DatabaseColumn("UPCID", ParLevelsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	ParLevelsSchema.Columns.Add(new DatabaseColumn("BottleCount", ParLevelsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add ParLevels to schema
            	DataProvider.Schema.Tables.Add(ParLevelsSchema);
				
				// Table: PeriodicReportGroupByRecipients
				// Primary Key: PeriodicReportGroupByRecipientID
				ITable PeriodicReportGroupByRecipientsSchema = new DatabaseTable("PeriodicReportGroupByRecipients", DataProvider) { ClassName = "PeriodicReportGroupByRecipient", SchemaName = "dbo" };
            	PeriodicReportGroupByRecipientsSchema.Columns.Add(new DatabaseColumn("PeriodicReportGroupByRecipientID", PeriodicReportGroupByRecipientsSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	PeriodicReportGroupByRecipientsSchema.Columns.Add(new DatabaseColumn("PeriodicReportGroupID", PeriodicReportGroupByRecipientsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	PeriodicReportGroupByRecipientsSchema.Columns.Add(new DatabaseColumn("EmailRecipientID", PeriodicReportGroupByRecipientsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
				// Add PeriodicReportGroupByRecipients to schema
            	DataProvider.Schema.Tables.Add(PeriodicReportGroupByRecipientsSchema);
				
				// Table: PeriodicReportGroups
				// Primary Key: PeriodicReportGroupID
				ITable PeriodicReportGroupsSchema = new DatabaseTable("PeriodicReportGroups", DataProvider) { ClassName = "PeriodicReportGroup", SchemaName = "dbo" };
            	PeriodicReportGroupsSchema.Columns.Add(new DatabaseColumn("PeriodicReportGroupID", PeriodicReportGroupsSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	PeriodicReportGroupsSchema.Columns.Add(new DatabaseColumn("Name", PeriodicReportGroupsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	PeriodicReportGroupsSchema.Columns.Add(new DatabaseColumn("Title", PeriodicReportGroupsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	PeriodicReportGroupsSchema.Columns.Add(new DatabaseColumn("Body", PeriodicReportGroupsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	PeriodicReportGroupsSchema.Columns.Add(new DatabaseColumn("PeriodicityType", PeriodicReportGroupsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	PeriodicReportGroupsSchema.Columns.Add(new DatabaseColumn("Periodicity", PeriodicReportGroupsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	PeriodicReportGroupsSchema.Columns.Add(new DatabaseColumn("StartDate", PeriodicReportGroupsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.DateTime,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	PeriodicReportGroupsSchema.Columns.Add(new DatabaseColumn("EndDate", PeriodicReportGroupsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.DateTime,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	PeriodicReportGroupsSchema.Columns.Add(new DatabaseColumn("LastRun", PeriodicReportGroupsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.DateTime,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	PeriodicReportGroupsSchema.Columns.Add(new DatabaseColumn("NextRun", PeriodicReportGroupsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.DateTime,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	PeriodicReportGroupsSchema.Columns.Add(new DatabaseColumn("TimesRun", PeriodicReportGroupsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	PeriodicReportGroupsSchema.Columns.Add(new DatabaseColumn("ExecutionTime", PeriodicReportGroupsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int64,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add PeriodicReportGroups to schema
            	DataProvider.Schema.Tables.Add(PeriodicReportGroupsSchema);
				
				// Table: PeriodicReports
				// Primary Key: PeriodicReportID
				ITable PeriodicReportsSchema = new DatabaseTable("PeriodicReports", DataProvider) { ClassName = "PeriodicReport", SchemaName = "dbo" };
            	PeriodicReportsSchema.Columns.Add(new DatabaseColumn("PeriodicReportID", PeriodicReportsSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	PeriodicReportsSchema.Columns.Add(new DatabaseColumn("ReportID", PeriodicReportsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	PeriodicReportsSchema.Columns.Add(new DatabaseColumn("PeriodicReportGroupID", PeriodicReportsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	PeriodicReportsSchema.Columns.Add(new DatabaseColumn("DateBoundDays", PeriodicReportsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add PeriodicReports to schema
            	DataProvider.Schema.Tables.Add(PeriodicReportsSchema);
				
				// Table: POSTicketItemModifiers
				// Primary Key: POSTicketITemModifierID
				ITable POSTicketItemModifiersSchema = new DatabaseTable("POSTicketItemModifiers", DataProvider) { ClassName = "POSTicketItemModifier", SchemaName = "dbo" };
            	POSTicketItemModifiersSchema.Columns.Add(new DatabaseColumn("POSTicketITemModifierID", POSTicketItemModifiersSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	POSTicketItemModifiersSchema.Columns.Add(new DatabaseColumn("POSTicketItemID", POSTicketItemModifiersSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	POSTicketItemModifiersSchema.Columns.Add(new DatabaseColumn("Name", POSTicketItemModifiersSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	POSTicketItemModifiersSchema.Columns.Add(new DatabaseColumn("Price", POSTicketItemModifiersSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Currency,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add POSTicketItemModifiers to schema
            	DataProvider.Schema.Tables.Add(POSTicketItemModifiersSchema);
				
				// Table: POSTicketItems
				// Primary Key: POSTicketItemID
				ITable POSTicketItemsSchema = new DatabaseTable("POSTicketItems", DataProvider) { ClassName = "POSTicketItem", SchemaName = "dbo" };
            	POSTicketItemsSchema.Columns.Add(new DatabaseColumn("POSTicketItemID", POSTicketItemsSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	POSTicketItemsSchema.Columns.Add(new DatabaseColumn("POSTicketID", POSTicketItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	POSTicketItemsSchema.Columns.Add(new DatabaseColumn("Comment", POSTicketItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	POSTicketItemsSchema.Columns.Add(new DatabaseColumn("Description", POSTicketItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	POSTicketItemsSchema.Columns.Add(new DatabaseColumn("Price", POSTicketItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Currency,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	POSTicketItemsSchema.Columns.Add(new DatabaseColumn("Reconciled", POSTicketItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	POSTicketItemsSchema.Columns.Add(new DatabaseColumn("Quantity", POSTicketItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	POSTicketItemsSchema.Columns.Add(new DatabaseColumn("Status", POSTicketItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add POSTicketItems to schema
            	DataProvider.Schema.Tables.Add(POSTicketItemsSchema);
				
				// Table: POSTickets
				// Primary Key: POSTicketID
				ITable POSTicketsSchema = new DatabaseTable("POSTickets", DataProvider) { ClassName = "POSTicket", SchemaName = "dbo" };
            	POSTicketsSchema.Columns.Add(new DatabaseColumn("POSTicketID", POSTicketsSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	POSTicketsSchema.Columns.Add(new DatabaseColumn("LocationID", POSTicketsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	POSTicketsSchema.Columns.Add(new DatabaseColumn("CheckNumber", POSTicketsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	POSTicketsSchema.Columns.Add(new DatabaseColumn("Comments", POSTicketsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	POSTicketsSchema.Columns.Add(new DatabaseColumn("TicketDate", POSTicketsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.DateTime,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	POSTicketsSchema.Columns.Add(new DatabaseColumn("Establishment", POSTicketsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	POSTicketsSchema.Columns.Add(new DatabaseColumn("GuestCount", POSTicketsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	POSTicketsSchema.Columns.Add(new DatabaseColumn("CustomerTable", POSTicketsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	POSTicketsSchema.Columns.Add(new DatabaseColumn("RawData", POSTicketsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add POSTickets to schema
            	DataProvider.Schema.Tables.Add(POSTicketsSchema);
				
				// Table: Pours
				// Primary Key: PourID
				ITable PoursSchema = new DatabaseTable("Pours", DataProvider) { ClassName = "Pour", SchemaName = "dbo" };
            	PoursSchema.Columns.Add(new DatabaseColumn("PourID", PoursSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	PoursSchema.Columns.Add(new DatabaseColumn("TagID", PoursSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	PoursSchema.Columns.Add(new DatabaseColumn("DeviceID", PoursSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	PoursSchema.Columns.Add(new DatabaseColumn("Volume", PoursSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	PoursSchema.Columns.Add(new DatabaseColumn("PourTime", PoursSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.DateTime,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	PoursSchema.Columns.Add(new DatabaseColumn("Duration", PoursSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	PoursSchema.Columns.Add(new DatabaseColumn("AmountLeft", PoursSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	PoursSchema.Columns.Add(new DatabaseColumn("Temperature", PoursSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	PoursSchema.Columns.Add(new DatabaseColumn("RawData", PoursSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	PoursSchema.Columns.Add(new DatabaseColumn("BatteryVoltage", PoursSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	PoursSchema.Columns.Add(new DatabaseColumn("UPCID", PoursSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	PoursSchema.Columns.Add(new DatabaseColumn("POSTicketItemID", PoursSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	PoursSchema.Columns.Add(new DatabaseColumn("Alerted", PoursSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Boolean,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	PoursSchema.Columns.Add(new DatabaseColumn("LocationID", PoursSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	PoursSchema.Columns.Add(new DatabaseColumn("IngredientID", PoursSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	PoursSchema.Columns.Add(new DatabaseColumn("Status", PoursSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	PoursSchema.Columns.Add(new DatabaseColumn("ParentPourID", PoursSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	PoursSchema.Columns.Add(new DatabaseColumn("Split", PoursSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Boolean,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add Pours to schema
            	DataProvider.Schema.Tables.Add(PoursSchema);
				
				// Table: Quality
				// Primary Key: QualityID
				ITable QualitySchema = new DatabaseTable("Quality", DataProvider) { ClassName = "Quality", SchemaName = "dbo" };
            	QualitySchema.Columns.Add(new DatabaseColumn("QualityID", QualitySchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	QualitySchema.Columns.Add(new DatabaseColumn("QualityLevel", QualitySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	QualitySchema.Columns.Add(new DatabaseColumn("Name", QualitySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add Quality to schema
            	DataProvider.Schema.Tables.Add(QualitySchema);
				
				// Table: Recipe
				// Primary Key: RecipeID
				ITable RecipeSchema = new DatabaseTable("Recipe", DataProvider) { ClassName = "Recipe", SchemaName = "dbo" };
            	RecipeSchema.Columns.Add(new DatabaseColumn("RecipeID", RecipeSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	RecipeSchema.Columns.Add(new DatabaseColumn("Description", RecipeSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add Recipe to schema
            	DataProvider.Schema.Tables.Add(RecipeSchema);
				
				// Table: ReportParameters
				// Primary Key: ReportParameterID
				ITable ReportParametersSchema = new DatabaseTable("ReportParameters", DataProvider) { ClassName = "ReportParameter", SchemaName = "dbo" };
            	ReportParametersSchema.Columns.Add(new DatabaseColumn("ReportParameterID", ReportParametersSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	ReportParametersSchema.Columns.Add(new DatabaseColumn("ReportID", ReportParametersSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	ReportParametersSchema.Columns.Add(new DatabaseColumn("Name", ReportParametersSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	ReportParametersSchema.Columns.Add(new DatabaseColumn("DataType", ReportParametersSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	ReportParametersSchema.Columns.Add(new DatabaseColumn("SQLName", ReportParametersSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	ReportParametersSchema.Columns.Add(new DatabaseColumn("DefaultValue", ReportParametersSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add ReportParameters to schema
            	DataProvider.Schema.Tables.Add(ReportParametersSchema);
				
				// Table: Reports
				// Primary Key: ReportID
				ITable ReportsSchema = new DatabaseTable("Reports", DataProvider) { ClassName = "Report", SchemaName = "dbo" };
            	ReportsSchema.Columns.Add(new DatabaseColumn("ReportID", ReportsSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	ReportsSchema.Columns.Add(new DatabaseColumn("Name", ReportsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	ReportsSchema.Columns.Add(new DatabaseColumn("Description", ReportsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	ReportsSchema.Columns.Add(new DatabaseColumn("Command", ReportsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.AnsiString,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	ReportsSchema.Columns.Add(new DatabaseColumn("ReportFile", ReportsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	ReportsSchema.Columns.Add(new DatabaseColumn("SavedLayout", ReportsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	ReportsSchema.Columns.Add(new DatabaseColumn("DateBound", ReportsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Boolean,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	ReportsSchema.Columns.Add(new DatabaseColumn("Active", ReportsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Boolean,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add Reports to schema
            	DataProvider.Schema.Tables.Add(ReportsSchema);
				
				// Table: rptCostAnalysis
				// Primary Key: 
				ITable rptCostAnalysisSchema = new DatabaseTable("rptCostAnalysis", DataProvider) { ClassName = "rptCostAnalysi", SchemaName = "dbo" };
            	rptCostAnalysisSchema.Columns.Add(new DatabaseColumn("PourTime", rptCostAnalysisSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.DateTime,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptCostAnalysisSchema.Columns.Add(new DatabaseColumn("Name", rptCostAnalysisSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptCostAnalysisSchema.Columns.Add(new DatabaseColumn("TagNumber", rptCostAnalysisSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptCostAnalysisSchema.Columns.Add(new DatabaseColumn("PourStandard", rptCostAnalysisSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptCostAnalysisSchema.Columns.Add(new DatabaseColumn("Volume", rptCostAnalysisSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptCostAnalysisSchema.Columns.Add(new DatabaseColumn("SinglePourType", rptCostAnalysisSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.AnsiString,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptCostAnalysisSchema.Columns.Add(new DatabaseColumn("IdealCost", rptCostAnalysisSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Decimal,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptCostAnalysisSchema.Columns.Add(new DatabaseColumn("PourCost", rptCostAnalysisSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptCostAnalysisSchema.Columns.Add(new DatabaseColumn("ActualProfit", rptCostAnalysisSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptCostAnalysisSchema.Columns.Add(new DatabaseColumn("TheoreticalProfit", rptCostAnalysisSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptCostAnalysisSchema.Columns.Add(new DatabaseColumn("LostProfit", rptCostAnalysisSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptCostAnalysisSchema.Columns.Add(new DatabaseColumn("Size", rptCostAnalysisSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptCostAnalysisSchema.Columns.Add(new DatabaseColumn("Category", rptCostAnalysisSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptCostAnalysisSchema.Columns.Add(new DatabaseColumn("ItemNumber", rptCostAnalysisSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add rptCostAnalysis to schema
            	DataProvider.Schema.Tables.Add(rptCostAnalysisSchema);
				
				// Table: rptDisconnectWithVolume
				// Primary Key: 
				ITable rptDisconnectWithVolumeSchema = new DatabaseTable("rptDisconnectWithVolume", DataProvider) { ClassName = "rptDisconnectWithVolume", SchemaName = "dbo" };
            	rptDisconnectWithVolumeSchema.Columns.Add(new DatabaseColumn("Name", rptDisconnectWithVolumeSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptDisconnectWithVolumeSchema.Columns.Add(new DatabaseColumn("Message", rptDisconnectWithVolumeSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptDisconnectWithVolumeSchema.Columns.Add(new DatabaseColumn("AlertTime", rptDisconnectWithVolumeSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.DateTime,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add rptDisconnectWithVolume to schema
            	DataProvider.Schema.Tables.Add(rptDisconnectWithVolumeSchema);
				
				// Table: rptInventory
				// Primary Key: 
				ITable rptInventorySchema = new DatabaseTable("rptInventory", DataProvider) { ClassName = "rptInventory", SchemaName = "dbo" };
            	rptInventorySchema.Columns.Add(new DatabaseColumn("Name", rptInventorySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptInventorySchema.Columns.Add(new DatabaseColumn("Stock", rptInventorySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptInventorySchema.Columns.Add(new DatabaseColumn("Tagged", rptInventorySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptInventorySchema.Columns.Add(new DatabaseColumn("TotalQuantity", rptInventorySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptInventorySchema.Columns.Add(new DatabaseColumn("MinimumParLevel", rptInventorySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptInventorySchema.Columns.Add(new DatabaseColumn("TotalVolume", rptInventorySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptInventorySchema.Columns.Add(new DatabaseColumn("AverageBottleCost", rptInventorySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Currency,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptInventorySchema.Columns.Add(new DatabaseColumn("TotalInventoryCost", rptInventorySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptInventorySchema.Columns.Add(new DatabaseColumn("Category", rptInventorySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add rptInventory to schema
            	DataProvider.Schema.Tables.Add(rptInventorySchema);
				
				// Table: rptPourTotals
				// Primary Key: 
				ITable rptPourTotalsSchema = new DatabaseTable("rptPourTotals", DataProvider) { ClassName = "rptPourTotal", SchemaName = "dbo" };
            	rptPourTotalsSchema.Columns.Add(new DatabaseColumn("Type", rptPourTotalsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptPourTotalsSchema.Columns.Add(new DatabaseColumn("Manufacturer", rptPourTotalsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptPourTotalsSchema.Columns.Add(new DatabaseColumn("Name", rptPourTotalsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptPourTotalsSchema.Columns.Add(new DatabaseColumn("Count", rptPourTotalsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptPourTotalsSchema.Columns.Add(new DatabaseColumn("Volume", rptPourTotalsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add rptPourTotals to schema
            	DataProvider.Schema.Tables.Add(rptPourTotalsSchema);
				
				// Table: rptSummaryByGroup
				// Primary Key: 
				ITable rptSummaryByGroupSchema = new DatabaseTable("rptSummaryByGroup", DataProvider) { ClassName = "rptSummaryByGroup", SchemaName = "dbo" };
            	rptSummaryByGroupSchema.Columns.Add(new DatabaseColumn("Category", rptSummaryByGroupSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptSummaryByGroupSchema.Columns.Add(new DatabaseColumn("NumberOfPours", rptSummaryByGroupSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptSummaryByGroupSchema.Columns.Add(new DatabaseColumn("PourStandard", rptSummaryByGroupSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptSummaryByGroupSchema.Columns.Add(new DatabaseColumn("AveragePour", rptSummaryByGroupSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptSummaryByGroupSchema.Columns.Add(new DatabaseColumn("MinPour", rptSummaryByGroupSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptSummaryByGroupSchema.Columns.Add(new DatabaseColumn("MaxPour", rptSummaryByGroupSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptSummaryByGroupSchema.Columns.Add(new DatabaseColumn("TotalVolume", rptSummaryByGroupSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptSummaryByGroupSchema.Columns.Add(new DatabaseColumn("IdealCostTotal", rptSummaryByGroupSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Decimal,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptSummaryByGroupSchema.Columns.Add(new DatabaseColumn("TotalPourCost", rptSummaryByGroupSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptSummaryByGroupSchema.Columns.Add(new DatabaseColumn("TotalProfit", rptSummaryByGroupSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptSummaryByGroupSchema.Columns.Add(new DatabaseColumn("TotalSales", rptSummaryByGroupSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Decimal,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptSummaryByGroupSchema.Columns.Add(new DatabaseColumn("Average", rptSummaryByGroupSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add rptSummaryByGroup to schema
            	DataProvider.Schema.Tables.Add(rptSummaryByGroupSchema);
				
				// Table: rptSummaryByTag
				// Primary Key: 
				ITable rptSummaryByTagSchema = new DatabaseTable("rptSummaryByTag", DataProvider) { ClassName = "rptSummaryByTag", SchemaName = "dbo" };
            	rptSummaryByTagSchema.Columns.Add(new DatabaseColumn("Name", rptSummaryByTagSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptSummaryByTagSchema.Columns.Add(new DatabaseColumn("TagNumber", rptSummaryByTagSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptSummaryByTagSchema.Columns.Add(new DatabaseColumn("NumberOfPours", rptSummaryByTagSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptSummaryByTagSchema.Columns.Add(new DatabaseColumn("PourStandard", rptSummaryByTagSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptSummaryByTagSchema.Columns.Add(new DatabaseColumn("AveragePour", rptSummaryByTagSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptSummaryByTagSchema.Columns.Add(new DatabaseColumn("MinPour", rptSummaryByTagSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptSummaryByTagSchema.Columns.Add(new DatabaseColumn("MaxPour", rptSummaryByTagSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptSummaryByTagSchema.Columns.Add(new DatabaseColumn("TotalVolume", rptSummaryByTagSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptSummaryByTagSchema.Columns.Add(new DatabaseColumn("IdealCostTotal", rptSummaryByTagSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Decimal,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptSummaryByTagSchema.Columns.Add(new DatabaseColumn("TotalPourCost", rptSummaryByTagSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptSummaryByTagSchema.Columns.Add(new DatabaseColumn("TotalProfit", rptSummaryByTagSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptSummaryByTagSchema.Columns.Add(new DatabaseColumn("TotalSales", rptSummaryByTagSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Decimal,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptSummaryByTagSchema.Columns.Add(new DatabaseColumn("Average", rptSummaryByTagSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add rptSummaryByTag to schema
            	DataProvider.Schema.Tables.Add(rptSummaryByTagSchema);
				
				// Table: rptSummaryByUPC
				// Primary Key: 
				ITable rptSummaryByUPCSchema = new DatabaseTable("rptSummaryByUPC", DataProvider) { ClassName = "rptSummaryByUPC", SchemaName = "dbo" };
            	rptSummaryByUPCSchema.Columns.Add(new DatabaseColumn("ItemNumber", rptSummaryByUPCSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptSummaryByUPCSchema.Columns.Add(new DatabaseColumn("Name", rptSummaryByUPCSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptSummaryByUPCSchema.Columns.Add(new DatabaseColumn("NumberOfPours", rptSummaryByUPCSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptSummaryByUPCSchema.Columns.Add(new DatabaseColumn("PourStandard", rptSummaryByUPCSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptSummaryByUPCSchema.Columns.Add(new DatabaseColumn("AveragePour", rptSummaryByUPCSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptSummaryByUPCSchema.Columns.Add(new DatabaseColumn("MinPour", rptSummaryByUPCSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptSummaryByUPCSchema.Columns.Add(new DatabaseColumn("MaxPour", rptSummaryByUPCSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptSummaryByUPCSchema.Columns.Add(new DatabaseColumn("TotalVolume", rptSummaryByUPCSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptSummaryByUPCSchema.Columns.Add(new DatabaseColumn("IdealCostTotal", rptSummaryByUPCSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Decimal,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptSummaryByUPCSchema.Columns.Add(new DatabaseColumn("TotalPourCost", rptSummaryByUPCSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptSummaryByUPCSchema.Columns.Add(new DatabaseColumn("TotalProfit", rptSummaryByUPCSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptSummaryByUPCSchema.Columns.Add(new DatabaseColumn("TotalSales", rptSummaryByUPCSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Decimal,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptSummaryByUPCSchema.Columns.Add(new DatabaseColumn("Average", rptSummaryByUPCSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add rptSummaryByUPC to schema
            	DataProvider.Schema.Tables.Add(rptSummaryByUPCSchema);
				
				// Table: rptTaggedInventory
				// Primary Key: 
				ITable rptTaggedInventorySchema = new DatabaseTable("rptTaggedInventory", DataProvider) { ClassName = "rptTaggedInventory", SchemaName = "dbo" };
            	rptTaggedInventorySchema.Columns.Add(new DatabaseColumn("Location", rptTaggedInventorySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptTaggedInventorySchema.Columns.Add(new DatabaseColumn("Manufacturer", rptTaggedInventorySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptTaggedInventorySchema.Columns.Add(new DatabaseColumn("UPC Name", rptTaggedInventorySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptTaggedInventorySchema.Columns.Add(new DatabaseColumn("TagNumber", rptTaggedInventorySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptTaggedInventorySchema.Columns.Add(new DatabaseColumn("Quantity", rptTaggedInventorySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptTaggedInventorySchema.Columns.Add(new DatabaseColumn("NozzleArea", rptTaggedInventorySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add rptTaggedInventory to schema
            	DataProvider.Schema.Tables.Add(rptTaggedInventorySchema);
				
				// Table: rptTagPourTotals
				// Primary Key: 
				ITable rptTagPourTotalsSchema = new DatabaseTable("rptTagPourTotals", DataProvider) { ClassName = "rptTagPourTotal", SchemaName = "dbo" };
            	rptTagPourTotalsSchema.Columns.Add(new DatabaseColumn("Type", rptTagPourTotalsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptTagPourTotalsSchema.Columns.Add(new DatabaseColumn("Manufacturer", rptTagPourTotalsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptTagPourTotalsSchema.Columns.Add(new DatabaseColumn("Name", rptTagPourTotalsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptTagPourTotalsSchema.Columns.Add(new DatabaseColumn("TagNumber", rptTagPourTotalsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptTagPourTotalsSchema.Columns.Add(new DatabaseColumn("Count", rptTagPourTotalsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rptTagPourTotalsSchema.Columns.Add(new DatabaseColumn("Volume", rptTagPourTotalsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add rptTagPourTotals to schema
            	DataProvider.Schema.Tables.Add(rptTagPourTotalsSchema);
				
				// Table: SecurableItems
				// Primary Key: SecurableItemID
				ITable SecurableItemsSchema = new DatabaseTable("SecurableItems", DataProvider) { ClassName = "SecurableItem", SchemaName = "dbo" };
            	SecurableItemsSchema.Columns.Add(new DatabaseColumn("SecurableItemID", SecurableItemsSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	SecurableItemsSchema.Columns.Add(new DatabaseColumn("Name", SecurableItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.AnsiString,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	SecurableItemsSchema.Columns.Add(new DatabaseColumn("Description", SecurableItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.AnsiString,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add SecurableItems to schema
            	DataProvider.Schema.Tables.Add(SecurableItemsSchema);
				
				// Table: SizeTypes
				// Primary Key: SizeTypeID
				ITable SizeTypesSchema = new DatabaseTable("SizeTypes", DataProvider) { ClassName = "SizeType", SchemaName = "dbo" };
            	SizeTypesSchema.Columns.Add(new DatabaseColumn("SizeTypeID", SizeTypesSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	SizeTypesSchema.Columns.Add(new DatabaseColumn("Name", SizeTypesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	SizeTypesSchema.Columns.Add(new DatabaseColumn("Abbreviation", SizeTypesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	SizeTypesSchema.Columns.Add(new DatabaseColumn("ConversionMultiplier", SizeTypesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add SizeTypes to schema
            	DataProvider.Schema.Tables.Add(SizeTypesSchema);
				
				// Table: StandardNozzles
				// Primary Key: StandardNozzleID
				ITable StandardNozzlesSchema = new DatabaseTable("StandardNozzles", DataProvider) { ClassName = "StandardNozzle", SchemaName = "dbo" };
            	StandardNozzlesSchema.Columns.Add(new DatabaseColumn("StandardNozzleID", StandardNozzlesSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	StandardNozzlesSchema.Columns.Add(new DatabaseColumn("Length", StandardNozzlesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	StandardNozzlesSchema.Columns.Add(new DatabaseColumn("Width", StandardNozzlesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	StandardNozzlesSchema.Columns.Add(new DatabaseColumn("Shape", StandardNozzlesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add StandardNozzles to schema
            	DataProvider.Schema.Tables.Add(StandardNozzlesSchema);
				
				// Table: StandardPours
				// Primary Key: StandardPourID
				ITable StandardPoursSchema = new DatabaseTable("StandardPours", DataProvider) { ClassName = "StandardPour", SchemaName = "dbo" };
            	StandardPoursSchema.Columns.Add(new DatabaseColumn("StandardPourID", StandardPoursSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	StandardPoursSchema.Columns.Add(new DatabaseColumn("Name", StandardPoursSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	StandardPoursSchema.Columns.Add(new DatabaseColumn("PourStandard", StandardPoursSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	StandardPoursSchema.Columns.Add(new DatabaseColumn("StandardVariance", StandardPoursSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	StandardPoursSchema.Columns.Add(new DatabaseColumn("CategoryID", StandardPoursSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	StandardPoursSchema.Columns.Add(new DatabaseColumn("SystemStandard", StandardPoursSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Boolean,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add StandardPours to schema
            	DataProvider.Schema.Tables.Add(StandardPoursSchema);
				
				// Table: StandardPrices
				// Primary Key: StandardPriceID
				ITable StandardPricesSchema = new DatabaseTable("StandardPrices", DataProvider) { ClassName = "StandardPrice", SchemaName = "dbo" };
            	StandardPricesSchema.Columns.Add(new DatabaseColumn("StandardPriceID", StandardPricesSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	StandardPricesSchema.Columns.Add(new DatabaseColumn("SinglePrice", StandardPricesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Currency,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	StandardPricesSchema.Columns.Add(new DatabaseColumn("DoublePrice", StandardPricesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Currency,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add StandardPrices to schema
            	DataProvider.Schema.Tables.Add(StandardPricesSchema);
				
				// Table: TagActivities
				// Primary Key: TagActivityID
				ITable TagActivitiesSchema = new DatabaseTable("TagActivities", DataProvider) { ClassName = "TagActivity", SchemaName = "dbo" };
            	TagActivitiesSchema.Columns.Add(new DatabaseColumn("TagActivityID", TagActivitiesSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	TagActivitiesSchema.Columns.Add(new DatabaseColumn("TagID", TagActivitiesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	TagActivitiesSchema.Columns.Add(new DatabaseColumn("DeviceID", TagActivitiesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	TagActivitiesSchema.Columns.Add(new DatabaseColumn("LocationID", TagActivitiesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	TagActivitiesSchema.Columns.Add(new DatabaseColumn("ActivityTime", TagActivitiesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.DateTime,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	TagActivitiesSchema.Columns.Add(new DatabaseColumn("SignalStrength", TagActivitiesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	TagActivitiesSchema.Columns.Add(new DatabaseColumn("ActivityType", TagActivitiesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	TagActivitiesSchema.Columns.Add(new DatabaseColumn("RawData", TagActivitiesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add TagActivities to schema
            	DataProvider.Schema.Tables.Add(TagActivitiesSchema);
				
				// Table: TagAlerts
				// Primary Key: TagAlertID
				ITable TagAlertsSchema = new DatabaseTable("TagAlerts", DataProvider) { ClassName = "TagAlert", SchemaName = "dbo" };
            	TagAlertsSchema.Columns.Add(new DatabaseColumn("TagAlertID", TagAlertsSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	TagAlertsSchema.Columns.Add(new DatabaseColumn("AlertType", TagAlertsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	TagAlertsSchema.Columns.Add(new DatabaseColumn("TagID", TagAlertsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	TagAlertsSchema.Columns.Add(new DatabaseColumn("DeviceID", TagAlertsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	TagAlertsSchema.Columns.Add(new DatabaseColumn("LocationID", TagAlertsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	TagAlertsSchema.Columns.Add(new DatabaseColumn("Message", TagAlertsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	TagAlertsSchema.Columns.Add(new DatabaseColumn("Severity", TagAlertsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	TagAlertsSchema.Columns.Add(new DatabaseColumn("AlertTime", TagAlertsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.DateTime,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add TagAlerts to schema
            	DataProvider.Schema.Tables.Add(TagAlertsSchema);
				
				// Table: TagMoves
				// Primary Key: TagMoveID
				ITable TagMovesSchema = new DatabaseTable("TagMoves", DataProvider) { ClassName = "TagMove", SchemaName = "dbo" };
            	TagMovesSchema.Columns.Add(new DatabaseColumn("TagMoveID", TagMovesSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	TagMovesSchema.Columns.Add(new DatabaseColumn("TagID", TagMovesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	TagMovesSchema.Columns.Add(new DatabaseColumn("DeviceID", TagMovesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	TagMovesSchema.Columns.Add(new DatabaseColumn("LocationID", TagMovesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	TagMovesSchema.Columns.Add(new DatabaseColumn("MoveTime", TagMovesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.DateTime,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	TagMovesSchema.Columns.Add(new DatabaseColumn("SignalStrength", TagMovesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add TagMoves to schema
            	DataProvider.Schema.Tables.Add(TagMovesSchema);
				
				// Table: Tags
				// Primary Key: TagID
				ITable TagsSchema = new DatabaseTable("Tags", DataProvider) { ClassName = "Tag", SchemaName = "dbo" };
            	TagsSchema.Columns.Add(new DatabaseColumn("TagID", TagsSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	TagsSchema.Columns.Add(new DatabaseColumn("LocationID", TagsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	TagsSchema.Columns.Add(new DatabaseColumn("TagNumber", TagsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	TagsSchema.Columns.Add(new DatabaseColumn("RawData", TagsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	TagsSchema.Columns.Add(new DatabaseColumn("StandardNozzleID", TagsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = true
												});
				// Add Tags to schema
            	DataProvider.Schema.Tables.Add(TagsSchema);
				
				// Table: TicketItemAliases
				// Primary Key: TicketItemAliasID
				ITable TicketItemAliasesSchema = new DatabaseTable("TicketItemAliases", DataProvider) { ClassName = "TicketItemAlias", SchemaName = "dbo" };
            	TicketItemAliasesSchema.Columns.Add(new DatabaseColumn("TicketItemAliasID", TicketItemAliasesSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	TicketItemAliasesSchema.Columns.Add(new DatabaseColumn("RecipeID", TicketItemAliasesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	TicketItemAliasesSchema.Columns.Add(new DatabaseColumn("Description", TicketItemAliasesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	TicketItemAliasesSchema.Columns.Add(new DatabaseColumn("PosUPC", TicketItemAliasesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	TicketItemAliasesSchema.Columns.Add(new DatabaseColumn("Price", TicketItemAliasesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Currency,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add TicketItemAliases to schema
            	DataProvider.Schema.Tables.Add(TicketItemAliasesSchema);
				
				// Table: UPCs
				// Primary Key: UPCID
				ITable UPCsSchema = new DatabaseTable("UPCs", DataProvider) { ClassName = "UPC", SchemaName = "dbo" };
            	UPCsSchema.Columns.Add(new DatabaseColumn("UPCID", UPCsSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	UPCsSchema.Columns.Add(new DatabaseColumn("ItemNumber", UPCsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	UPCsSchema.Columns.Add(new DatabaseColumn("Name", UPCsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	UPCsSchema.Columns.Add(new DatabaseColumn("Size", UPCsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	UPCsSchema.Columns.Add(new DatabaseColumn("SizeTypeID", UPCsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	UPCsSchema.Columns.Add(new DatabaseColumn("CategoryID", UPCsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	UPCsSchema.Columns.Add(new DatabaseColumn("RootCategoryID", UPCsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	UPCsSchema.Columns.Add(new DatabaseColumn("Quality", UPCsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	UPCsSchema.Columns.Add(new DatabaseColumn("UnitCost", UPCsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Currency,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	UPCsSchema.Columns.Add(new DatabaseColumn("StandardNozzleID", UPCsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	UPCsSchema.Columns.Add(new DatabaseColumn("MinimumParLevel", UPCsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	UPCsSchema.Columns.Add(new DatabaseColumn("PourModifier", UPCsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	UPCsSchema.Columns.Add(new DatabaseColumn("AllowHalfPour", UPCsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Boolean,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	UPCsSchema.Columns.Add(new DatabaseColumn("ChildUPCID", UPCsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	UPCsSchema.Columns.Add(new DatabaseColumn("BottleCount", UPCsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	UPCsSchema.Columns.Add(new DatabaseColumn("ManufacturerID", UPCsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	UPCsSchema.Columns.Add(new DatabaseColumn("CustomID", UPCsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	UPCsSchema.Columns.Add(new DatabaseColumn("DefaultCost", UPCsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Currency,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add UPCs to schema
            	DataProvider.Schema.Tables.Add(UPCsSchema);
				
				// Table: Users
				// Primary Key: UserID
				ITable UsersSchema = new DatabaseTable("Users", DataProvider) { ClassName = "User", SchemaName = "dbo" };
            	UsersSchema.Columns.Add(new DatabaseColumn("UserID", UsersSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	UsersSchema.Columns.Add(new DatabaseColumn("UserName", UsersSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.AnsiString,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	UsersSchema.Columns.Add(new DatabaseColumn("ProperName", UsersSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.AnsiString,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	UsersSchema.Columns.Add(new DatabaseColumn("Password", UsersSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.AnsiString,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	UsersSchema.Columns.Add(new DatabaseColumn("UserData", UsersSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	UsersSchema.Columns.Add(new DatabaseColumn("Active", UsersSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Boolean,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add Users to schema
            	DataProvider.Schema.Tables.Add(UsersSchema);
				
				// Table: UserSessions
				// Primary Key: UserSessionID
				ITable UserSessionsSchema = new DatabaseTable("UserSessions", DataProvider) { ClassName = "UserSession", SchemaName = "dbo" };
            	UserSessionsSchema.Columns.Add(new DatabaseColumn("UserSessionID", UserSessionsSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	UserSessionsSchema.Columns.Add(new DatabaseColumn("UserID", UserSessionsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	UserSessionsSchema.Columns.Add(new DatabaseColumn("SessionID", UserSessionsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	UserSessionsSchema.Columns.Add(new DatabaseColumn("StartTime", UserSessionsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.DateTime,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	UserSessionsSchema.Columns.Add(new DatabaseColumn("EndTime", UserSessionsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.DateTime,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add UserSessions to schema
            	DataProvider.Schema.Tables.Add(UserSessionsSchema);
				
				// Table: UsersXGroups
				// Primary Key: GroupID
				ITable UsersXGroupsSchema = new DatabaseTable("UsersXGroups", DataProvider) { ClassName = "UsersXGroup", SchemaName = "dbo" };
            	UsersXGroupsSchema.Columns.Add(new DatabaseColumn("UserID", UsersXGroupsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	UsersXGroupsSchema.Columns.Add(new DatabaseColumn("GroupID", UsersXGroupsSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
				// Add UsersXGroups to schema
            	DataProvider.Schema.Tables.Add(UsersXGroupsSchema);
				
				// Table: UsersXOrganizations
				// Primary Key: UxOID
				ITable UsersXOrganizationsSchema = new DatabaseTable("UsersXOrganizations", DataProvider) { ClassName = "UsersXOrganization", SchemaName = "dbo" };
            	UsersXOrganizationsSchema.Columns.Add(new DatabaseColumn("UxOID", UsersXOrganizationsSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	UsersXOrganizationsSchema.Columns.Add(new DatabaseColumn("UserID", UsersXOrganizationsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
            	UsersXOrganizationsSchema.Columns.Add(new DatabaseColumn("OrganizationID", UsersXOrganizationsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = true
												});
				// Add UsersXOrganizations to schema
            	DataProvider.Schema.Tables.Add(UsersXOrganizationsSchema);
				
				// Table: vwDefaultNozzleValues
				// Primary Key: 
				ITable vwDefaultNozzleValuesSchema = new DatabaseTable("vwDefaultNozzleValues", DataProvider) { ClassName = "vwDefaultNozzleValue", SchemaName = "dbo" };
            	vwDefaultNozzleValuesSchema.Columns.Add(new DatabaseColumn("NozzleWidth", vwDefaultNozzleValuesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwDefaultNozzleValuesSchema.Columns.Add(new DatabaseColumn("NozzleLength", vwDefaultNozzleValuesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwDefaultNozzleValuesSchema.Columns.Add(new DatabaseColumn("NozzleShape", vwDefaultNozzleValuesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add vwDefaultNozzleValues to schema
            	DataProvider.Schema.Tables.Add(vwDefaultNozzleValuesSchema);
				
				// Table: vwFullBottleInventory
				// Primary Key: 
				ITable vwFullBottleInventorySchema = new DatabaseTable("vwFullBottleInventory", DataProvider) { ClassName = "vwFullBottleInventory", SchemaName = "dbo" };
            	vwFullBottleInventorySchema.Columns.Add(new DatabaseColumn("ItemNumber", vwFullBottleInventorySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwFullBottleInventorySchema.Columns.Add(new DatabaseColumn("UPCName", vwFullBottleInventorySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwFullBottleInventorySchema.Columns.Add(new DatabaseColumn("BottleCount", vwFullBottleInventorySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwFullBottleInventorySchema.Columns.Add(new DatabaseColumn("TaggedBottles", vwFullBottleInventorySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwFullBottleInventorySchema.Columns.Add(new DatabaseColumn("Quantity", vwFullBottleInventorySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwFullBottleInventorySchema.Columns.Add(new DatabaseColumn("Location", vwFullBottleInventorySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwFullBottleInventorySchema.Columns.Add(new DatabaseColumn("BottleCost", vwFullBottleInventorySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Currency,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwFullBottleInventorySchema.Columns.Add(new DatabaseColumn("TotalCost", vwFullBottleInventorySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwFullBottleInventorySchema.Columns.Add(new DatabaseColumn("Category", vwFullBottleInventorySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add vwFullBottleInventory to schema
            	DataProvider.Schema.Tables.Add(vwFullBottleInventorySchema);
				
				// Table: vwInventoryItems
				// Primary Key: 
				ITable vwInventoryItemsSchema = new DatabaseTable("vwInventoryItems", DataProvider) { ClassName = "vwInventoryItem", SchemaName = "dbo" };
            	vwInventoryItemsSchema.Columns.Add(new DatabaseColumn("InventoryItemID", vwInventoryItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwInventoryItemsSchema.Columns.Add(new DatabaseColumn("ItemNumber", vwInventoryItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwInventoryItemsSchema.Columns.Add(new DatabaseColumn("Name", vwInventoryItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwInventoryItemsSchema.Columns.Add(new DatabaseColumn("UPCID", vwInventoryItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwInventoryItemsSchema.Columns.Add(new DatabaseColumn("LocationName", vwInventoryItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwInventoryItemsSchema.Columns.Add(new DatabaseColumn("LocationID", vwInventoryItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwInventoryItemsSchema.Columns.Add(new DatabaseColumn("CustomID", vwInventoryItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwInventoryItemsSchema.Columns.Add(new DatabaseColumn("TaggedQuantity", vwInventoryItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwInventoryItemsSchema.Columns.Add(new DatabaseColumn("StockQuantity", vwInventoryItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwInventoryItemsSchema.Columns.Add(new DatabaseColumn("TotalQuantity", vwInventoryItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwInventoryItemsSchema.Columns.Add(new DatabaseColumn("TotalCost", vwInventoryItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Currency,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwInventoryItemsSchema.Columns.Add(new DatabaseColumn("ParBottleCount", vwInventoryItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add vwInventoryItems to schema
            	DataProvider.Schema.Tables.Add(vwInventoryItemsSchema);
				
				// Table: vwItemsCategoryQuantity
				// Primary Key: 
				ITable vwItemsCategoryQuantitySchema = new DatabaseTable("vwItemsCategoryQuantity", DataProvider) { ClassName = "vwItemsCategoryQuantity", SchemaName = "dbo" };
            	vwItemsCategoryQuantitySchema.Columns.Add(new DatabaseColumn("LocationName", vwItemsCategoryQuantitySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwItemsCategoryQuantitySchema.Columns.Add(new DatabaseColumn("Category", vwItemsCategoryQuantitySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwItemsCategoryQuantitySchema.Columns.Add(new DatabaseColumn("Subcategory", vwItemsCategoryQuantitySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwItemsCategoryQuantitySchema.Columns.Add(new DatabaseColumn("ItemNumber", vwItemsCategoryQuantitySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwItemsCategoryQuantitySchema.Columns.Add(new DatabaseColumn("Name", vwItemsCategoryQuantitySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwItemsCategoryQuantitySchema.Columns.Add(new DatabaseColumn("StockQuantity", vwItemsCategoryQuantitySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwItemsCategoryQuantitySchema.Columns.Add(new DatabaseColumn("TaggedQuantity", vwItemsCategoryQuantitySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwItemsCategoryQuantitySchema.Columns.Add(new DatabaseColumn("TotalQuantity", vwItemsCategoryQuantitySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add vwItemsCategoryQuantity to schema
            	DataProvider.Schema.Tables.Add(vwItemsCategoryQuantitySchema);
				
				// Table: vwManufacturers
				// Primary Key: 
				ITable vwManufacturersSchema = new DatabaseTable("vwManufacturers", DataProvider) { ClassName = "vwManufacturer", SchemaName = "dbo" };
            	vwManufacturersSchema.Columns.Add(new DatabaseColumn("ManufacturerID", vwManufacturersSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwManufacturersSchema.Columns.Add(new DatabaseColumn("Name", vwManufacturersSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwManufacturersSchema.Columns.Add(new DatabaseColumn("CategoryID", vwManufacturersSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwManufacturersSchema.Columns.Add(new DatabaseColumn("RootCategoryID", vwManufacturersSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add vwManufacturers to schema
            	DataProvider.Schema.Tables.Add(vwManufacturersSchema);
				
				// Table: vwMissingInventory
				// Primary Key: 
				ITable vwMissingInventorySchema = new DatabaseTable("vwMissingInventory", DataProvider) { ClassName = "vwMissingInventory", SchemaName = "dbo" };
            	vwMissingInventorySchema.Columns.Add(new DatabaseColumn("Name", vwMissingInventorySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwMissingInventorySchema.Columns.Add(new DatabaseColumn("ExitDate", vwMissingInventorySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.DateTime,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add vwMissingInventory to schema
            	DataProvider.Schema.Tables.Add(vwMissingInventorySchema);
				
				// Table: vwParCounts
				// Primary Key: 
				ITable vwParCountsSchema = new DatabaseTable("vwParCounts", DataProvider) { ClassName = "vwParCount", SchemaName = "dbo" };
            	vwParCountsSchema.Columns.Add(new DatabaseColumn("Location", vwParCountsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwParCountsSchema.Columns.Add(new DatabaseColumn("Type", vwParCountsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwParCountsSchema.Columns.Add(new DatabaseColumn("Category", vwParCountsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwParCountsSchema.Columns.Add(new DatabaseColumn("Name", vwParCountsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwParCountsSchema.Columns.Add(new DatabaseColumn("Manufacturer", vwParCountsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwParCountsSchema.Columns.Add(new DatabaseColumn("ParLevel", vwParCountsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwParCountsSchema.Columns.Add(new DatabaseColumn("BottleCount", vwParCountsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add vwParCounts to schema
            	DataProvider.Schema.Tables.Add(vwParCountsSchema);
				
				// Table: vwParLevelIssues
				// Primary Key: 
				ITable vwParLevelIssuesSchema = new DatabaseTable("vwParLevelIssues", DataProvider) { ClassName = "vwParLevelIssue", SchemaName = "dbo" };
            	vwParLevelIssuesSchema.Columns.Add(new DatabaseColumn("Location", vwParLevelIssuesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwParLevelIssuesSchema.Columns.Add(new DatabaseColumn("Type", vwParLevelIssuesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwParLevelIssuesSchema.Columns.Add(new DatabaseColumn("Category", vwParLevelIssuesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwParLevelIssuesSchema.Columns.Add(new DatabaseColumn("UPCName", vwParLevelIssuesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwParLevelIssuesSchema.Columns.Add(new DatabaseColumn("Manufacturer", vwParLevelIssuesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwParLevelIssuesSchema.Columns.Add(new DatabaseColumn("ParLevel", vwParLevelIssuesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwParLevelIssuesSchema.Columns.Add(new DatabaseColumn("BottleCount", vwParLevelIssuesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwParLevelIssuesSchema.Columns.Add(new DatabaseColumn("OverUnder", vwParLevelIssuesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.AnsiString,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwParLevelIssuesSchema.Columns.Add(new DatabaseColumn("OffBy", vwParLevelIssuesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add vwParLevelIssues to schema
            	DataProvider.Schema.Tables.Add(vwParLevelIssuesSchema);
				
				// Table: vwPartialBottleInventory
				// Primary Key: 
				ITable vwPartialBottleInventorySchema = new DatabaseTable("vwPartialBottleInventory", DataProvider) { ClassName = "vwPartialBottleInventory", SchemaName = "dbo" };
            	vwPartialBottleInventorySchema.Columns.Add(new DatabaseColumn("ItemNumber", vwPartialBottleInventorySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwPartialBottleInventorySchema.Columns.Add(new DatabaseColumn("UPCName", vwPartialBottleInventorySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwPartialBottleInventorySchema.Columns.Add(new DatabaseColumn("TagNumber", vwPartialBottleInventorySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwPartialBottleInventorySchema.Columns.Add(new DatabaseColumn("Size", vwPartialBottleInventorySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwPartialBottleInventorySchema.Columns.Add(new DatabaseColumn("Quantity", vwPartialBottleInventorySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwPartialBottleInventorySchema.Columns.Add(new DatabaseColumn("Location", vwPartialBottleInventorySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwPartialBottleInventorySchema.Columns.Add(new DatabaseColumn("BottleCost", vwPartialBottleInventorySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Currency,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwPartialBottleInventorySchema.Columns.Add(new DatabaseColumn("PartialInventory", vwPartialBottleInventorySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwPartialBottleInventorySchema.Columns.Add(new DatabaseColumn("Category", vwPartialBottleInventorySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add vwPartialBottleInventory to schema
            	DataProvider.Schema.Tables.Add(vwPartialBottleInventorySchema);
				
				// Table: vwPosPours
				// Primary Key: 
				ITable vwPosPoursSchema = new DatabaseTable("vwPosPours", DataProvider) { ClassName = "vwPosPour", SchemaName = "dbo" };
            	vwPosPoursSchema.Columns.Add(new DatabaseColumn("PourID", vwPosPoursSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwPosPoursSchema.Columns.Add(new DatabaseColumn("StatusText", vwPosPoursSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.AnsiString,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwPosPoursSchema.Columns.Add(new DatabaseColumn("PourAmount", vwPosPoursSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwPosPoursSchema.Columns.Add(new DatabaseColumn("Type", vwPosPoursSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwPosPoursSchema.Columns.Add(new DatabaseColumn("Category", vwPosPoursSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwPosPoursSchema.Columns.Add(new DatabaseColumn("PourTime", vwPosPoursSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.DateTime,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwPosPoursSchema.Columns.Add(new DatabaseColumn("POSTicketItemID", vwPosPoursSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwPosPoursSchema.Columns.Add(new DatabaseColumn("IngredientID", vwPosPoursSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add vwPosPours to schema
            	DataProvider.Schema.Tables.Add(vwPosPoursSchema);
				
				// Table: vwPOSTicketItems
				// Primary Key: 
				ITable vwPOSTicketItemsSchema = new DatabaseTable("vwPOSTicketItems", DataProvider) { ClassName = "vwPOSTicketItem", SchemaName = "dbo" };
            	vwPOSTicketItemsSchema.Columns.Add(new DatabaseColumn("POSTicketItemID", vwPOSTicketItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwPOSTicketItemsSchema.Columns.Add(new DatabaseColumn("TicketDate", vwPOSTicketItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.DateTime,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwPOSTicketItemsSchema.Columns.Add(new DatabaseColumn("CheckNumber", vwPOSTicketItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwPOSTicketItemsSchema.Columns.Add(new DatabaseColumn("Establishment", vwPOSTicketItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwPOSTicketItemsSchema.Columns.Add(new DatabaseColumn("Description", vwPOSTicketItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwPOSTicketItemsSchema.Columns.Add(new DatabaseColumn("Quantity", vwPOSTicketItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwPOSTicketItemsSchema.Columns.Add(new DatabaseColumn("Status", vwPOSTicketItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwPOSTicketItemsSchema.Columns.Add(new DatabaseColumn("POSTicketID", vwPOSTicketItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add vwPOSTicketItems to schema
            	DataProvider.Schema.Tables.Add(vwPOSTicketItemsSchema);
				
				// Table: vwPours
				// Primary Key: 
				ITable vwPoursSchema = new DatabaseTable("vwPours", DataProvider) { ClassName = "vwPour", SchemaName = "dbo" };
            	vwPoursSchema.Columns.Add(new DatabaseColumn("Location Name", vwPoursSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwPoursSchema.Columns.Add(new DatabaseColumn("TagNumber", vwPoursSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwPoursSchema.Columns.Add(new DatabaseColumn("Volume", vwPoursSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwPoursSchema.Columns.Add(new DatabaseColumn("Name", vwPoursSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwPoursSchema.Columns.Add(new DatabaseColumn("Status", vwPoursSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwPoursSchema.Columns.Add(new DatabaseColumn("PourTime", vwPoursSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add vwPours to schema
            	DataProvider.Schema.Tables.Add(vwPoursSchema);
				
				// Table: vwReaders
				// Primary Key: 
				ITable vwReadersSchema = new DatabaseTable("vwReaders", DataProvider) { ClassName = "vwReader", SchemaName = "dbo" };
            	vwReadersSchema.Columns.Add(new DatabaseColumn("HardwareID", vwReadersSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwReadersSchema.Columns.Add(new DatabaseColumn("Name", vwReadersSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwReadersSchema.Columns.Add(new DatabaseColumn("Description", vwReadersSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwReadersSchema.Columns.Add(new DatabaseColumn("Location", vwReadersSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add vwReaders to schema
            	DataProvider.Schema.Tables.Add(vwReadersSchema);
				
				// Table: vwTicketItemAliases
				// Primary Key: 
				ITable vwTicketItemAliasesSchema = new DatabaseTable("vwTicketItemAliases", DataProvider) { ClassName = "vwTicketItemAlias", SchemaName = "dbo" };
            	vwTicketItemAliasesSchema.Columns.Add(new DatabaseColumn("TicketItemAliasID", vwTicketItemAliasesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwTicketItemAliasesSchema.Columns.Add(new DatabaseColumn("RecipeID", vwTicketItemAliasesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwTicketItemAliasesSchema.Columns.Add(new DatabaseColumn("Description", vwTicketItemAliasesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwTicketItemAliasesSchema.Columns.Add(new DatabaseColumn("PosUPC", vwTicketItemAliasesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwTicketItemAliasesSchema.Columns.Add(new DatabaseColumn("Price", vwTicketItemAliasesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Currency,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwTicketItemAliasesSchema.Columns.Add(new DatabaseColumn("Recipe", vwTicketItemAliasesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwTicketItemAliasesSchema.Columns.Add(new DatabaseColumn("UPCName", vwTicketItemAliasesSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add vwTicketItemAliases to schema
            	DataProvider.Schema.Tables.Add(vwTicketItemAliasesSchema);
				
				// Table: vwUPCItems
				// Primary Key: 
				ITable vwUPCItemsSchema = new DatabaseTable("vwUPCItems", DataProvider) { ClassName = "vwUPCItem", SchemaName = "dbo" };
            	vwUPCItemsSchema.Columns.Add(new DatabaseColumn("UPCID", vwUPCItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwUPCItemsSchema.Columns.Add(new DatabaseColumn("ItemNumber", vwUPCItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwUPCItemsSchema.Columns.Add(new DatabaseColumn("Manufacturer", vwUPCItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwUPCItemsSchema.Columns.Add(new DatabaseColumn("Name", vwUPCItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwUPCItemsSchema.Columns.Add(new DatabaseColumn("Size", vwUPCItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwUPCItemsSchema.Columns.Add(new DatabaseColumn("SizeTypeID", vwUPCItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwUPCItemsSchema.Columns.Add(new DatabaseColumn("CategoryID", vwUPCItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwUPCItemsSchema.Columns.Add(new DatabaseColumn("RootCategoryID", vwUPCItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwUPCItemsSchema.Columns.Add(new DatabaseColumn("CategoryName", vwUPCItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwUPCItemsSchema.Columns.Add(new DatabaseColumn("RootCategory", vwUPCItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwUPCItemsSchema.Columns.Add(new DatabaseColumn("SubCategoryItemID", vwUPCItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwUPCItemsSchema.Columns.Add(new DatabaseColumn("NozzleLength", vwUPCItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwUPCItemsSchema.Columns.Add(new DatabaseColumn("NozzleWidth", vwUPCItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwUPCItemsSchema.Columns.Add(new DatabaseColumn("NozzleShape", vwUPCItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwUPCItemsSchema.Columns.Add(new DatabaseColumn("PourModifier", vwUPCItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Double,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwUPCItemsSchema.Columns.Add(new DatabaseColumn("MinimumParLevel", vwUPCItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwUPCItemsSchema.Columns.Add(new DatabaseColumn("UnitCost", vwUPCItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Currency,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwUPCItemsSchema.Columns.Add(new DatabaseColumn("CustomID", vwUPCItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwUPCItemsSchema.Columns.Add(new DatabaseColumn("QualityName", vwUPCItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.String,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwUPCItemsSchema.Columns.Add(new DatabaseColumn("Quality", vwUPCItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int32,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	vwUPCItemsSchema.Columns.Add(new DatabaseColumn("ManufacturerID", vwUPCItemsSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Guid,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add vwUPCItems to schema
            	DataProvider.Schema.Tables.Add(vwUPCItemsSchema);
            }
            #endregion
        }
    }
}