


using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using SubSonic.DataProviders;
using SubSonic.Extensions;
using SubSonic.Linq.Structure;
using SubSonic.Query;
using SubSonic.Schema;
using System.Data.Common;
using System.Collections.Generic;

namespace Sakila.Data
{
    public partial class SakilaDB : IQuerySurface
    {

        public IDataProvider DataProvider;
        public DbQueryProvider provider;
        
        public bool TestMode
		{
            get
			{
                return DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            }
        }

        public SakilaDB() 
        { 
            DataProvider = ProviderFactory.GetProvider("Sakila");
            Init();
        }

        public SakilaDB(string connectionStringName)
        {
            DataProvider = ProviderFactory.GetProvider(connectionStringName);
            Init();
        }

		public SakilaDB(string connectionString, string providerName)
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
               
        public IDataProvider Provider
        {
            get { return DataProvider; }
            set {DataProvider=value;}
        }
        
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
			
        public Query<actor> actors { get; set; }
        public Query<address> addresses { get; set; }
        public Query<category> categories { get; set; }
        public Query<city> cities { get; set; }
        public Query<country> countries { get; set; }
        public Query<customer> customers { get; set; }
        public Query<film> films { get; set; }
        public Query<filmactor> filmactors { get; set; }
        public Query<filmcategory> filmcategories { get; set; }
        public Query<filmlistview> filmlistviews { get; set; }
        public Query<filmtext> filmtexts { get; set; }
        public Query<inventory> inventories { get; set; }
        public Query<language> languages { get; set; }
        public Query<payment> payments { get; set; }
        public Query<rental> rentals { get; set; }
        public Query<staff> staffs { get; set; }
        public Query<store> stores { get; set; }

			

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
            actors = new Query<actor>(provider);
            addresses = new Query<address>(provider);
            categories = new Query<category>(provider);
            cities = new Query<city>(provider);
            countries = new Query<country>(provider);
            customers = new Query<customer>(provider);
            films = new Query<film>(provider);
            filmactors = new Query<filmactor>(provider);
            filmcategories = new Query<filmcategory>(provider);
            filmlistviews = new Query<filmlistview>(provider);
            filmtexts = new Query<filmtext>(provider);
            inventories = new Query<inventory>(provider);
            languages = new Query<language>(provider);
            payments = new Query<payment>(provider);
            rentals = new Query<rental>(provider);
            staffs = new Query<staff>(provider);
            stores = new Query<store>(provider);
            #endregion


            #region ' Schemas '
        	if(DataProvider.Schema.Tables.Count == 0)
			{
				
				// Table: actor
				// Primary Key: actor_id
				ITable actorSchema = new DatabaseTable("actor", DataProvider) { ClassName = "actor", SchemaName = "Sakila" };
            	actorSchema.Columns.Add(new DatabaseColumn("actor_id", actorSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Int16,
													IsNullable = false,
													AutoIncrement = true,
													IsForeignKey = false
												});
            	actorSchema.Columns.Add(new DatabaseColumn("first_name", actorSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.AnsiString,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	actorSchema.Columns.Add(new DatabaseColumn("last_name", actorSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.AnsiString,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	actorSchema.Columns.Add(new DatabaseColumn("last_update", actorSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.DateTime,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add actor to schema
            	DataProvider.Schema.Tables.Add(actorSchema);
				
				// Table: address
				// Primary Key: address_id
				ITable addressSchema = new DatabaseTable("address", DataProvider) { ClassName = "address", SchemaName = "Sakila" };
            	addressSchema.Columns.Add(new DatabaseColumn("address_id", addressSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Int16,
													IsNullable = false,
													AutoIncrement = true,
													IsForeignKey = false
												});
            	addressSchema.Columns.Add(new DatabaseColumn("address1", addressSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.AnsiString,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	addressSchema.Columns.Add(new DatabaseColumn("address2", addressSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.AnsiString,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	addressSchema.Columns.Add(new DatabaseColumn("district", addressSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.AnsiString,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	addressSchema.Columns.Add(new DatabaseColumn("city_id", addressSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int16,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	addressSchema.Columns.Add(new DatabaseColumn("postal_code", addressSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.AnsiString,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	addressSchema.Columns.Add(new DatabaseColumn("phone", addressSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.AnsiString,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	addressSchema.Columns.Add(new DatabaseColumn("last_update", addressSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.DateTime,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add address to schema
            	DataProvider.Schema.Tables.Add(addressSchema);
				
				// Table: category
				// Primary Key: category_id
				ITable categorySchema = new DatabaseTable("category", DataProvider) { ClassName = "category", SchemaName = "Sakila" };
            	categorySchema.Columns.Add(new DatabaseColumn("category_id", categorySchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Byte,
													IsNullable = false,
													AutoIncrement = true,
													IsForeignKey = false
												});
            	categorySchema.Columns.Add(new DatabaseColumn("name", categorySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.AnsiString,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	categorySchema.Columns.Add(new DatabaseColumn("last_update", categorySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.DateTime,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add category to schema
            	DataProvider.Schema.Tables.Add(categorySchema);
				
				// Table: city
				// Primary Key: city_id
				ITable citySchema = new DatabaseTable("city", DataProvider) { ClassName = "city", SchemaName = "Sakila" };
            	citySchema.Columns.Add(new DatabaseColumn("city_id", citySchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Int16,
													IsNullable = false,
													AutoIncrement = true,
													IsForeignKey = false
												});
            	citySchema.Columns.Add(new DatabaseColumn("city_name", citySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.AnsiString,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	citySchema.Columns.Add(new DatabaseColumn("country_id", citySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int16,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	citySchema.Columns.Add(new DatabaseColumn("last_update", citySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.DateTime,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add city to schema
            	DataProvider.Schema.Tables.Add(citySchema);
				
				// Table: country
				// Primary Key: country_id
				ITable countrySchema = new DatabaseTable("country", DataProvider) { ClassName = "country", SchemaName = "Sakila" };
            	countrySchema.Columns.Add(new DatabaseColumn("country_id", countrySchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Int16,
													IsNullable = false,
													AutoIncrement = true,
													IsForeignKey = false
												});
            	countrySchema.Columns.Add(new DatabaseColumn("country_name", countrySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.AnsiString,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	countrySchema.Columns.Add(new DatabaseColumn("last_update", countrySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.DateTime,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add country to schema
            	DataProvider.Schema.Tables.Add(countrySchema);
				
				// Table: customer
				// Primary Key: customer_id
				ITable customerSchema = new DatabaseTable("customer", DataProvider) { ClassName = "customer", SchemaName = "Sakila" };
            	customerSchema.Columns.Add(new DatabaseColumn("customer_id", customerSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Int16,
													IsNullable = false,
													AutoIncrement = true,
													IsForeignKey = false
												});
            	customerSchema.Columns.Add(new DatabaseColumn("store_id", customerSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Byte,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	customerSchema.Columns.Add(new DatabaseColumn("first_name", customerSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.AnsiString,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	customerSchema.Columns.Add(new DatabaseColumn("last_name", customerSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.AnsiString,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	customerSchema.Columns.Add(new DatabaseColumn("email", customerSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.AnsiString,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	customerSchema.Columns.Add(new DatabaseColumn("address_id", customerSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int16,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	customerSchema.Columns.Add(new DatabaseColumn("active", customerSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Byte,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	customerSchema.Columns.Add(new DatabaseColumn("create_date", customerSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.DateTime,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	customerSchema.Columns.Add(new DatabaseColumn("last_update", customerSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.DateTime,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add customer to schema
            	DataProvider.Schema.Tables.Add(customerSchema);
				
				// Table: film
				// Primary Key: film_id
				ITable filmSchema = new DatabaseTable("film", DataProvider) { ClassName = "film", SchemaName = "Sakila" };
            	filmSchema.Columns.Add(new DatabaseColumn("film_id", filmSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Int16,
													IsNullable = false,
													AutoIncrement = true,
													IsForeignKey = false
												});
            	filmSchema.Columns.Add(new DatabaseColumn("title", filmSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.AnsiString,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	filmSchema.Columns.Add(new DatabaseColumn("description", filmSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.AnsiString,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	filmSchema.Columns.Add(new DatabaseColumn("release_year", filmSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.AnsiString,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	filmSchema.Columns.Add(new DatabaseColumn("language_id", filmSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Byte,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	filmSchema.Columns.Add(new DatabaseColumn("original_language_id", filmSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Byte,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	filmSchema.Columns.Add(new DatabaseColumn("rental_duration", filmSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Byte,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	filmSchema.Columns.Add(new DatabaseColumn("rental_rate", filmSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Decimal,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	filmSchema.Columns.Add(new DatabaseColumn("length", filmSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int16,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	filmSchema.Columns.Add(new DatabaseColumn("replacement_cost", filmSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Decimal,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	filmSchema.Columns.Add(new DatabaseColumn("rating", filmSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.AnsiString,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	filmSchema.Columns.Add(new DatabaseColumn("special_features", filmSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.AnsiString,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	filmSchema.Columns.Add(new DatabaseColumn("last_update", filmSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.DateTime,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add film to schema
            	DataProvider.Schema.Tables.Add(filmSchema);
				
				// Table: filmactor
				// Primary Key: film_id
				ITable filmactorSchema = new DatabaseTable("filmactor", DataProvider) { ClassName = "filmactor", SchemaName = "Sakila" };
            	filmactorSchema.Columns.Add(new DatabaseColumn("actor_id", filmactorSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Int16,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	filmactorSchema.Columns.Add(new DatabaseColumn("film_id", filmactorSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Int16,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	filmactorSchema.Columns.Add(new DatabaseColumn("last_update", filmactorSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.DateTime,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add filmactor to schema
            	DataProvider.Schema.Tables.Add(filmactorSchema);
				
				// Table: filmcategory
				// Primary Key: category_id
				ITable filmcategorySchema = new DatabaseTable("filmcategory", DataProvider) { ClassName = "filmcategory", SchemaName = "Sakila" };
            	filmcategorySchema.Columns.Add(new DatabaseColumn("film_id", filmcategorySchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Int16,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	filmcategorySchema.Columns.Add(new DatabaseColumn("category_id", filmcategorySchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Byte,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	filmcategorySchema.Columns.Add(new DatabaseColumn("last_update", filmcategorySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.DateTime,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add filmcategory to schema
            	DataProvider.Schema.Tables.Add(filmcategorySchema);
				
				// Table: filmlistview
				// Primary Key: 
				ITable filmlistviewSchema = new DatabaseTable("filmlistview", DataProvider) { ClassName = "filmlistview", SchemaName = "Sakila" };
            	filmlistviewSchema.Columns.Add(new DatabaseColumn("FID", filmlistviewSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int16,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	filmlistviewSchema.Columns.Add(new DatabaseColumn("title", filmlistviewSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.AnsiString,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	filmlistviewSchema.Columns.Add(new DatabaseColumn("description", filmlistviewSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.AnsiString,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	filmlistviewSchema.Columns.Add(new DatabaseColumn("category", filmlistviewSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.AnsiString,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	filmlistviewSchema.Columns.Add(new DatabaseColumn("price", filmlistviewSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Decimal,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	filmlistviewSchema.Columns.Add(new DatabaseColumn("length", filmlistviewSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int16,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	filmlistviewSchema.Columns.Add(new DatabaseColumn("rating", filmlistviewSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.AnsiString,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	filmlistviewSchema.Columns.Add(new DatabaseColumn("actors", filmlistviewSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.AnsiString,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add filmlistview to schema
            	DataProvider.Schema.Tables.Add(filmlistviewSchema);
				
				// Table: filmtext
				// Primary Key: film_id
				ITable filmtextSchema = new DatabaseTable("filmtext", DataProvider) { ClassName = "filmtext", SchemaName = "Sakila" };
            	filmtextSchema.Columns.Add(new DatabaseColumn("film_id", filmtextSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Int16,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	filmtextSchema.Columns.Add(new DatabaseColumn("title", filmtextSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.AnsiString,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	filmtextSchema.Columns.Add(new DatabaseColumn("description", filmtextSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.AnsiString,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add filmtext to schema
            	DataProvider.Schema.Tables.Add(filmtextSchema);
				
				// Table: inventory
				// Primary Key: inventory_id
				ITable inventorySchema = new DatabaseTable("inventory", DataProvider) { ClassName = "inventory", SchemaName = "Sakila" };
            	inventorySchema.Columns.Add(new DatabaseColumn("inventory_id", inventorySchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.AnsiString,
													IsNullable = false,
													AutoIncrement = true,
													IsForeignKey = false
												});
            	inventorySchema.Columns.Add(new DatabaseColumn("film_id", inventorySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int16,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	inventorySchema.Columns.Add(new DatabaseColumn("store_id", inventorySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Byte,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	inventorySchema.Columns.Add(new DatabaseColumn("last_update", inventorySchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.DateTime,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add inventory to schema
            	DataProvider.Schema.Tables.Add(inventorySchema);
				
				// Table: language
				// Primary Key: language_id
				ITable languageSchema = new DatabaseTable("language", DataProvider) { ClassName = "language", SchemaName = "Sakila" };
            	languageSchema.Columns.Add(new DatabaseColumn("language_id", languageSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Byte,
													IsNullable = false,
													AutoIncrement = true,
													IsForeignKey = false
												});
            	languageSchema.Columns.Add(new DatabaseColumn("name", languageSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.AnsiStringFixedLength,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	languageSchema.Columns.Add(new DatabaseColumn("last_update", languageSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.DateTime,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add language to schema
            	DataProvider.Schema.Tables.Add(languageSchema);
				
				// Table: payment
				// Primary Key: payment_id
				ITable paymentSchema = new DatabaseTable("payment", DataProvider) { ClassName = "payment", SchemaName = "Sakila" };
            	paymentSchema.Columns.Add(new DatabaseColumn("payment_id", paymentSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Int16,
													IsNullable = false,
													AutoIncrement = true,
													IsForeignKey = false
												});
            	paymentSchema.Columns.Add(new DatabaseColumn("customer_id", paymentSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int16,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	paymentSchema.Columns.Add(new DatabaseColumn("staff_id", paymentSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Byte,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	paymentSchema.Columns.Add(new DatabaseColumn("rental_id", paymentSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.AnsiString,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	paymentSchema.Columns.Add(new DatabaseColumn("amount", paymentSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Decimal,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	paymentSchema.Columns.Add(new DatabaseColumn("payment_date", paymentSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.DateTime,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	paymentSchema.Columns.Add(new DatabaseColumn("last_update", paymentSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.DateTime,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add payment to schema
            	DataProvider.Schema.Tables.Add(paymentSchema);
				
				// Table: rental
				// Primary Key: rental_id
				ITable rentalSchema = new DatabaseTable("rental", DataProvider) { ClassName = "rental", SchemaName = "Sakila" };
            	rentalSchema.Columns.Add(new DatabaseColumn("rental_id", rentalSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.AnsiString,
													IsNullable = false,
													AutoIncrement = true,
													IsForeignKey = false
												});
            	rentalSchema.Columns.Add(new DatabaseColumn("rental_date", rentalSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.DateTime,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rentalSchema.Columns.Add(new DatabaseColumn("inventory_id", rentalSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.AnsiString,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rentalSchema.Columns.Add(new DatabaseColumn("customer_id", rentalSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int16,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rentalSchema.Columns.Add(new DatabaseColumn("return_date", rentalSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.DateTime,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rentalSchema.Columns.Add(new DatabaseColumn("staff_id", rentalSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Byte,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	rentalSchema.Columns.Add(new DatabaseColumn("last_update", rentalSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.DateTime,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add rental to schema
            	DataProvider.Schema.Tables.Add(rentalSchema);
				
				// Table: staff
				// Primary Key: staff_id
				ITable staffSchema = new DatabaseTable("staff", DataProvider) { ClassName = "staff", SchemaName = "Sakila" };
            	staffSchema.Columns.Add(new DatabaseColumn("staff_id", staffSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Byte,
													IsNullable = false,
													AutoIncrement = true,
													IsForeignKey = false
												});
            	staffSchema.Columns.Add(new DatabaseColumn("first_name", staffSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.AnsiString,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	staffSchema.Columns.Add(new DatabaseColumn("last_name", staffSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.AnsiString,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	staffSchema.Columns.Add(new DatabaseColumn("address_id", staffSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int16,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	staffSchema.Columns.Add(new DatabaseColumn("picture", staffSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.AnsiString,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	staffSchema.Columns.Add(new DatabaseColumn("email", staffSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.AnsiString,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	staffSchema.Columns.Add(new DatabaseColumn("store_id", staffSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Byte,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	staffSchema.Columns.Add(new DatabaseColumn("active", staffSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Byte,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	staffSchema.Columns.Add(new DatabaseColumn("username", staffSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.AnsiString,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	staffSchema.Columns.Add(new DatabaseColumn("password", staffSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.AnsiString,
													IsNullable = true,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	staffSchema.Columns.Add(new DatabaseColumn("last_update", staffSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.DateTime,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add staff to schema
            	DataProvider.Schema.Tables.Add(staffSchema);
				
				// Table: store
				// Primary Key: store_id
				ITable storeSchema = new DatabaseTable("store", DataProvider) { ClassName = "store", SchemaName = "Sakila" };
            	storeSchema.Columns.Add(new DatabaseColumn("store_id", storeSchema)
												{
													IsPrimaryKey = true,
													DataType = DbType.Byte,
													IsNullable = false,
													AutoIncrement = true,
													IsForeignKey = false
												});
            	storeSchema.Columns.Add(new DatabaseColumn("manager_staff_id", storeSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Byte,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	storeSchema.Columns.Add(new DatabaseColumn("address_id", storeSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.Int16,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
            	storeSchema.Columns.Add(new DatabaseColumn("last_update", storeSchema)
												{
													IsPrimaryKey = false,
													DataType = DbType.DateTime,
													IsNullable = false,
													AutoIncrement = false,
													IsForeignKey = false
												});
				// Add store to schema
            	DataProvider.Schema.Tables.Add(storeSchema);
            }
            #endregion
        }
    }
}