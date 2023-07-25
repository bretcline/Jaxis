using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace LFI.Sync.DataManager
{
#if !PocketPC
    public enum TransactionType
    {
        Select,
        Insert,
        Update,
		ResultCount
    }
#else
	public enum TransactionType
	{
		Select,
		Insert,
		Update,
		ResultCount,
		ResultSet
	}
#endif

	public enum JoinType
    {
        Inner,
        Left,
        Right
    }

    public abstract class BaseTransaction<T> : IReaderTransaction<T>, IDisposable
    {
		protected static class Const
		{
            public static string ParamPrefix = "@";
            public static string PrimaryKeyParam = "@PrimaryKey";

            public static string Space = " ";
            public static string Period = ".";
            public static string Comma = ",";
            public static string Star = "*";

            public static string BracketFormatZeroComma = "[{0}],";
            public static string FormatZeroComma = "{0},";
            public static string CommaFormatZero = ",{0}";

            public static string SelectFrom = "SELECT {0} FROM [{1}]\r\n";
            public static string SelectAllFrom = "SELECT * FROM ";
            public static string SelectDistinctFrom = "SELECT DISTINCT {0} FROM [{1}]";
            public static string SelectCountFrom = "SELECT COUNT(*) FROM [{0}]";
            public static string SelectIDCountFrom = "SELECT COUNT({1}.{0}) FROM [{1}]";

            public static string AndEquals = " AND {0}.[{1}] = '{2}'";
            public static string WhereEquals = " WHERE {0}.[{1}] = '{2}'";
			public static string SimpleWhere = " WHERE {0} = '{1}'";
            public static string AndGreater = " AND {0}.[{1}] > '{2}'";
            public static string WhereGreater = " WHERE {0}.[{1}] > '{2}'";
            public static string ColumnEquals = "[{0}]={1},";

			public static string SimpleInnerJoin = "INNER JOIN {0} ON {1} = {2}";

            public static string AddNullColumn = ",null AS [{0}]";
            public static string AddColumnNoCommaOrAlias = "[{0}].[{1}]\r\n";
            public static string AddColumnAlias = ",{0} AS {1}\r\n";

            public static string Insert = "INSERT INTO {0} ({1}) VALUES ({2})";
            public static string Update = "UPDATE {0} SET {1} WHERE [{2}]={3}";

            public static string PagingSelect = "(SELECT ROW_NUMBER() OVER (ORDER BY {2}.{0}) AS RowNumber, {1} FROM [{2}]";
            public static string PagingAppend = " {0} ";
            public static string PagingResult = ") Results WHERE RowNumber BETWEEN {0} AND {1}";
		}

        public TransactionType TransactionType { get; protected set; }
		private bool isManualTransaction;

        protected IBaseData dataObj;
        

#if PocketPC
        protected static readonly string EmptyGuid = Guid.Empty.ToString();
#endif

        public object PrimaryKey { get { return dataObj.PrimaryKey; } }

        protected string tableName;
        public string Table { get { return tableName; } }

        protected string selectSQL;
        public string Select { get { return selectSQL; } }

        protected string insertSQL;
        public string Insert { get { return insertSQL; } }

        protected string updateSQL;
        public string Update { get { return updateSQL; } }

		public string IDColumn { get { return idColumn; } }

        protected Dictionary<string, object> sqlParams;
        public Dictionary<string, object> Params { get { return sqlParams; } }

        private Dictionary<string, string> columnParamMap;
		private Dictionary<string, Type> columnTypeMap;

        public string SelectColumns { get; protected set; }

        private DateTime lastUpdate;

        public Func<string> KeyGen { get; set; }

        public bool IgnorePaging { get; set; }

        private string joinSQL;
        protected string whereSQL;
        protected bool distinct;
        private string idColumn;
        private string updateColumn;
        private readonly string storedProcedure = String.Empty;
        private readonly int pageIndex;
        private readonly int pageSize;
		private bool compiled;
		protected TransactionReader _reader;

        private string whereKey;

#if PocketPC
        private static Dictionary<string, int> columnOrdinalDictionary;
#endif

        //----------------------------------------------------------------------
		/// <summary>
		/// Runs the provided stored procedure.
		/// </summary>
		/// <param name="storedProcedureName">The name of the stored procedure to execute.</param>
        protected BaseTransaction(string storedProcedureName)
        {
            storedProcedure = storedProcedureName;
        }

        //----------------------------------------------------------------------
		/// <summary>
		/// INSERT/UPDATE constructor. INSERT/UPDATE data are taken from the provided IBaseData object.
		/// </summary>
		/// <param name="transactionObj">The object to be inserted/updated.</param>
		/// <param name="tableName">The name of the target table in the database.</param>
		/// <param name="idColumn">The name of the primary key column in the table. If the table does not have a primary key, any column with unique data will work.</param>
		protected BaseTransaction(IBaseData transactionObj, string tableName, string idColumn)
        {
            dataObj = transactionObj;

			Init(tableName, idColumn, String.Empty, DataManager.KMinDateTime, String.Empty);
		}

        //----------------------------------------------------------------------
		/// <summary>
		/// Basic SELECT query constructor.
		/// </summary>
		/// <param name="tableName">The name of the target table in the database.</param>
		/// <param name="idColumn">The name of the primary key column in the table. If the table does not have a primary key, any column with data will work.</param>
		protected BaseTransaction(string tableName, string idColumn)
        {
			Init(tableName, idColumn, String.Empty, DataManager.KMinDateTime, String.Empty);
        }

        //----------------------------------------------------------------------
		/// <summary>
		/// Constuctor for SELECTing all results after a given date.
		/// </summary>
		/// <param name="tableName">The name of the target table in the database.</param>
		/// <param name="idColumn">The name of the primary key column in the table. If the table does not have a primary key, any column with data will work.</param>
		/// <param name="lastUpdated">All results after (but not including) this date will be returned.</param>
		/// <param name="updateColumn">Name of the column that holds the last modified date in the table.</param>
		protected BaseTransaction(string tableName, string idColumn, DateTime lastUpdated, string updateColumn)
        {
            Init(tableName, idColumn, String.Empty, lastUpdated, updateColumn);
        }

        //----------------------------------------------------------------------
		/// <summary>
		/// Constructor for SELECTing a single row by its primary key.
		/// </summary>
		/// <param name="tableName">The name of the target table in the database.</param>
		/// <param name="idColumn">The name of the primary key column in the table. If the table does not have a primary key, any column with data will work.</param>
		/// <param name="primaryKey">Primary key of the row to return.</param>
		protected BaseTransaction(string tableName, string idColumn, string primaryKey)
        {
			Init(tableName, idColumn, primaryKey, DataManager.KMinDateTime, String.Empty);
        }

        //----------------------------------------------------------------------
		/// <summary>
		/// Constructor for SELECTing results with pagination. EX: Returns results 1-499 for (PageIndex 0, PageSize 500) and results 500-999 for (PageIndex 1, PageSize 500).
		/// </summary>
		/// <param name="tableName">The name of the target table in the database.</param>
		/// <param name="idColumn">The name of the primary key column in the table. If the table does not have a primary key, any column with data will work.</param>
		/// <param name="pageIndex">The index of the page to return.</param>
		/// <param name="pageSize">The number of results in the given page.</param>
		protected BaseTransaction(string tableName, string idColumn, int pageIndex, int pageSize)
        {
            Init(tableName, idColumn, String.Empty, DataManager.KMinDateTime, String.Empty);
            this.pageIndex = pageIndex;
            this.pageSize = pageSize;
        }

        //----------------------------------------------------------------------
		/// <summary>
		/// Constructor for SELECTing all results after the provided date with pagination. EX: Returns results 1-499 for (PageIndex 0, PageSize 500) and results 500-999 for (PageIndex 1, PageSize 500).
		/// </summary>
		/// <param name="tableName">The name of the target table in the database.</param>
		/// <param name="idColumn">The name of the primary key column in the table. If the table does not have a primary key, any column with data will work.</param>
		/// <param name="pageIndex">The index of the page to return.</param>
		/// <param name="pageSize">The number of results in the given page.</param>
		/// <param name="lastUpdated">All results after (but not including) this date will be returned.</param>
		/// <param name="updateColumn">Name of the column that holds the last modified date in the table.</param>
		protected BaseTransaction(string tableName, string idColumn, int pageIndex, int pageSize, DateTime lastUpdated, string updateColumn)
        {
            Init(tableName, idColumn, String.Empty, lastUpdated, updateColumn);
            this.pageIndex = pageIndex;
            this.pageSize = pageSize;
        }

        private bool _disposed = false;

		//----------------------------------------------------------------------
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            // Use SupressFinalize in case a subclass
            // of this type implements a finalizer.
            GC.SuppressFinalize(this);
        }

		//----------------------------------------------------------------------
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        public virtual void Dispose(bool disposing)
        {
            // If you need thread safety, use a lock around these 
            // operations, as well as in your methods that use the resource.
            if (!_disposed)
            {
                // Release managed resources
                if (disposing)
                {
                    if (columnParamMap != null) columnParamMap.Clear();
                    if (columnTypeMap != null) columnTypeMap.Clear();
                    if (sqlParams != null) sqlParams.Clear();
                    insertSQL = null;
                    joinSQL = null;
                    selectSQL = null;
                    updateSQL = null;
                    whereSQL = null;
                }

                // Release unmanaged resources
                _disposed = true;
            }
        }

        //----------------------------------------------------------------------
        private void Init(string table, string idCol, string whereRef, DateTime lastUpd, string updateCol)
        {
            tableName = table;
            idColumn = idCol;
            lastUpdate = lastUpd;
            updateColumn = updateCol;
            whereKey = whereRef;
			compiled = false;
            IgnorePaging = false;

            if (dataObj == null)
				TransactionType = TransactionType.Select;
            else
                TransactionType = dataObj.PrimaryKey == null ? TransactionType.Insert : TransactionType.Update;
        }

        //----------------------------------------------------------------------
        private static Guid GuidGenerator()
        {
            return Guid.NewGuid();
        }

		//----------------------------------------------------------------------
		public void SetTransactionType(TransactionType type)
		{
			compiled = false;
			TransactionType = type;
			isManualTransaction = true;
		}

		//----------------------------------------------------------------------
        /// <summary>
        /// Compiles the specified condition.
        /// </summary>
        /// <param name="condition">The condition.</param>
        public void Compile(CompileCondition condition)
        {
            if (compiled)
                return;

#if PocketPC
            // minimal set of compile code is called for result set manual insert
            if (TransactionType == TransactionType.ResultSet)
            {
                SetPrimaryKey(idColumn);
                RegisterParams();
                compiled = true;
                return;
            }
#endif

            if (TransactionType == TransactionType.Update || TransactionType == TransactionType.Insert)
            {
                if (TransactionType == TransactionType.Insert && !isManualTransaction)
                {
                    if (KeyGen == null)
                        dataObj.PrimaryKey = GuidGenerator();
                    else
                        dataObj.PrimaryKey = KeyGen();
                }

                SetPrimaryKey(idColumn);
            }

            if (!String.IsNullOrEmpty(storedProcedure))
            {
                selectSQL = storedProcedure;
                RegisterParams();
                return;
            }

            if (TransactionType == TransactionType.Select)
            {
                if ((condition & CompileCondition.PrimaryKeyOnly) == CompileCondition.PrimaryKeyOnly)
                {
                    SelectColumns = IDColumn;
                }
                else
                {
                    RegisterSelectColumns();
                }

                if (SelectColumns.StartsWith(Const.Comma))
                    SelectColumns = SelectColumns.Remove(0, 1);

                selectSQL = String.Format(Const.SelectFrom, SelectColumns, tableName);
                if (distinct)
                    selectSQL = String.Format(Const.SelectDistinctFrom, SelectColumns, tableName);
            }
            else if (TransactionType == TransactionType.ResultCount)
            {
                selectSQL = (condition & CompileCondition.PrimaryKeyOnly) == CompileCondition.PrimaryKeyOnly ? 
                    String.Format(Const.SelectIDCountFrom, IDColumn, tableName) : 
                    String.Format(Const.SelectCountFrom, tableName);
            }
            if (TransactionType == TransactionType.Select || TransactionType == TransactionType.ResultCount)
            {
                if ((condition & CompileCondition.NoJoins) != CompileCondition.NoJoins)
                {
                    joinSQL = BuildJoins();
                    if (!String.IsNullOrEmpty(joinSQL))
                        selectSQL += Const.Space + joinSQL;
                }

                if ((condition & CompileCondition.NoWhere) != CompileCondition.NoWhere)
                {
                    whereSQL = BuildWhere();

                    if (!String.IsNullOrEmpty(whereSQL))
                    {
                        if (whereKey != String.Empty)
                            whereSQL += String.Format(Const.AndEquals, tableName, idColumn, whereKey);
                        if (updateColumn != String.Empty)
                            whereSQL += String.Format(Const.AndGreater, tableName, updateColumn, lastUpdate);
                    }
                    else
                    {
                        if (whereKey != String.Empty)
                            whereSQL += String.Format(Const.WhereEquals, tableName, idColumn, whereKey);
                        if (updateColumn != String.Empty)
                            whereSQL += String.Format(Const.WhereGreater, tableName, updateColumn, lastUpdate);
                    }

                    selectSQL += Const.Space + whereSQL;
                }

                // If the no order by condition is not set, add the clause
                if ((condition & CompileCondition.NoOrderBy) != CompileCondition.NoOrderBy)
                {
                    selectSQL += Const.Space + BuildOrderBy();
                }

                if (!IgnorePaging)
                    SetPaging();
            }
            else if (TransactionType == TransactionType.Insert)
            {
                RegisterParams();
                insertSQL = BuildInsert();
            }
            else
            {
                RegisterParams();
                updateSQL = BuildUpdate();
            }

            compiled = true;
        }

        //----------------------------------------------------------------------
		/// <summary>
		/// This compiles the parameters and columns into SQL. This is called by the DataManager before execution.
		/// </summary>
        public virtual void Compile()
        {
		    Compile(CompileCondition.Defaults);
        }

        //----------------------------------------------------------------------
        /// <summary>
		/// Loops through the data reader and passes reader rows off to the BuildFromReader() function. In most cases, this function should not be overridden.
		/// </summary>
		/// <param name="reader">Data reader to be processed.</param>
		/// <returns>List of objects constructed in the BuildFromReader() function</returns>
        public virtual List<T> FromReader(TransactionReader reader)
        {
			_reader = reader;
            List<T> outList = new List<T>();
            try
            {
                while (reader.ReadNextRow())
                {
                    outList.Add(BuildFromReader(reader));
                }

                return outList;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to build " + GetType().FullName + " in FromReader.", ex);
            }
        }

        //----------------------------------------------------------------------
		/// <summary>
		/// Required function. This is used to build the output object from a data reader row.
		/// </summary>
		/// <param name="reader"></param>
		/// <returns>Object of type T specified that has been constructed from the provided reader.</returns>
        public abstract T BuildFromReader(TransactionReader reader);

        //----------------------------------------------------------------------
        protected internal virtual void RegisterSelectColumns()
        {
            if (String.IsNullOrEmpty(SelectColumns))
            {
                SelectColumns = Const.Star;
            }
        }

#if PocketPC
        //----------------------------------------------------------------------
        /// <summary>
        /// This method is used for pocket pc so that result sets can be used for much faster 
        /// inserts.
        /// </summary>
        public virtual void RegisterColumnOrdinals()
        {
			throw new Exception("Transaction failed to compile because no column ordinals have been specified.");
        }
#endif

        //----------------------------------------------------------------------
		/// <summary>
		/// Adds a manual null entry for a select column. This does not edit the database, it overrides the database result with a null value.
		/// </summary>
		/// <param name="columnName">Name of column to return null for in the SQL.</param>
        protected void AddNullColumn(string columnName)
        {
            SelectColumns += String.Format(Const.AddNullColumn, columnName);
        }

		//----------------------------------------------------------------------
        /// <summary>
        /// Adds a column to be selected in the SQL. This should only be used for simple selects (not joins).
        /// </summary>
        /// <param name="name">Name of the column to be selected</param>
        protected void AddColumn(string name)
        {
            if (String.IsNullOrEmpty(name))
                return;

            SelectColumns += GetColumnFormat(name, true);
        }

		//----------------------------------------------------------------------
		/// <summary>
		/// Adds a column to be selected in the SQL. This should be used with an alias to prevent duplicate column names on joins.
		/// </summary>
		/// <param name="name">Name of the column to be selected</param>
		/// <param name="alias">Alias of column to be referenced in the reader</param>
		protected virtual void AddColumn(string name, string alias)
		{
			if (String.IsNullOrEmpty(name))
				return;

			SelectColumns += String.Format(Const.AddColumnAlias, name, alias);
		}

		//----------------------------------------------------------------------
		/// <summary>
		/// Gets the column in the form created/used by AddColumn.
		/// </summary>
		/// <param name="name">The column name.</param>
		/// <param name="useSeperator">if set to <c>true</c> [use seperator].</param>
		/// <returns></returns>
		protected string GetColumnFormat(string name, bool useSeperator)
		{
			string seperator;
			// Value already contains the table name
			if (name.Contains(Const.Period))
			{
				seperator = useSeperator ? Const.Comma : String.Empty;
				return seperator + name;
			}

			seperator = useSeperator ? Const.Comma : String.Empty;
			return seperator + String.Format(Const.AddColumnNoCommaOrAlias, tableName, name);
		}

#if PocketPC
        //----------------------------------------------------------------------
		/// <summary>
		/// Adds a column to be selected in the SQL. Columns will be prefixed in the SQL with their table name and a unique number to allow for multiple joins to the same table.
		/// This method can be used for a single table query or for joining to other Transactions (using the AddTransaction function).
		/// </summary>
		/// <param name="name">Name of the column to be selected.</param>
        /// <param name="ordinal">the order the column is in the database</param>
        protected void AddColumnOrdinal(string name, int ordinal)
        {
            if (String.IsNullOrEmpty(name))
                return;

            if (columnOrdinalDictionary == null)
                columnOrdinalDictionary = new Dictionary<string, int>(20);

			if (!columnOrdinalDictionary.ContainsKey(name))
				columnOrdinalDictionary.Add(name, ordinal);
        }

        /// <summary>
        /// Returns column order of passed in column name.  A key not found exception will 
        /// be thrown if column does not exist.</returns>
        /// </summary>
        /// <param name="columnName">Name of column.</param>
        /// <returns>ordinal of the passed in column name.  
        public int GetColumnOrdinal(string columnName)
        {
            return columnOrdinalDictionary[columnName];
        }
#endif

        //----------------------------------------------------------------------
		/// <summary>
		/// Adds a transaction to join to the current transaction.
		/// </summary>
		/// <typeparam name="TDataObject">Object type of the join transaction.</typeparam>
		/// <param name="joinTransaction">Transaction to be joined.</param>
		protected void AddTransaction<TDataObject>(BaseTransaction<TDataObject> joinTransaction)
        {
            joinTransaction.Compile();
            if (String.IsNullOrEmpty(joinTransaction.SelectColumns))
                return;

            SelectColumns += String.Format(Const.CommaFormatZero, joinTransaction.SelectColumns);
        }

        //----------------------------------------------------------------------
		/// <summary>
		/// Override this function to MANUALLY build an INSERT statement.
		/// </summary>
		/// <returns>The manually created insert string to be executed.</returns>
        protected virtual string BuildInsert()
        {
            if (columnParamMap == null)
                return String.Empty;

            StringBuilder insertBuilder = new StringBuilder();
            StringBuilder columnBuilder = new StringBuilder();
            StringBuilder paramBuilder = new StringBuilder();

            foreach (KeyValuePair<string, string> pair in columnParamMap)
            {
                columnBuilder.AppendFormat(Const.BracketFormatZeroComma, pair.Key);
                paramBuilder.AppendFormat(Const.FormatZeroComma, pair.Value);
            }

            string columnStr = columnBuilder.ToString();
            if (columnStr.EndsWith(Const.Comma))
                columnStr = columnStr.Remove(columnStr.LastIndexOf(Const.Comma), 1);

            string paramStr = paramBuilder.ToString();
            if (paramStr.EndsWith(Const.Comma))
                paramStr = paramStr.Remove(paramStr.LastIndexOf(Const.Comma), 1);

            insertBuilder.AppendFormat(Const.Insert, tableName, columnStr, paramStr);

            return insertBuilder.ToString();
        }

#if PocketPC
		//----------------------------------------------------------------------
        public virtual void PerpareResultSet(System.Data.SqlServerCe.SqlCeResultSet resultSet, System.Data.SqlServerCe.SqlCeUpdatableRecord record)
        {
			foreach (KeyValuePair<string, string> pair in columnParamMap)
            {
                int ordinal = GetColumnOrdinal(pair.Key);
				object value = sqlParams[pair.Value];
				record.SetValue(ordinal, value);
            }
        }
#endif

		//----------------------------------------------------------------------
		/// <summary>
		/// Override this function to MANUALLY build an UPDATE statement.
		/// </summary>
		/// <returns>The manually created update string to be executed.</returns>
        protected virtual string BuildUpdate()
        {
            if (columnParamMap == null)
                return String.Empty;

            StringBuilder updateBuilder = new StringBuilder();
            StringBuilder paramBuilder = new StringBuilder();

            foreach (KeyValuePair<string, string> pair in columnParamMap)
            {
                string column = pair.Key;
                string param = pair.Value;

                if (param == Const.PrimaryKeyParam)
                    continue;

                paramBuilder.AppendFormat(Const.ColumnEquals, column, param);
            }

            string paramStr = paramBuilder.ToString();
            if (paramStr.EndsWith(Const.Comma))
                paramStr = paramStr.Remove(paramStr.LastIndexOf(Const.Comma), 1);

            updateBuilder.AppendFormat(Const.Update, tableName, paramStr, idColumn, Const.PrimaryKeyParam);

            return updateBuilder.ToString();
        }

        //----------------------------------------------------------------------
		/// <summary>
		/// Override this function to add parameters for INSERT/UPDATE statements.
		/// This is the ideal place to call AddParam() and AddPrimayKey() functions to build INSERT/UPDATE statements.
		/// </summary>
        public virtual void RegisterParams()
        {
        }

        //----------------------------------------------------------------------
		/// <summary>
		/// Override this function to MANUALLY build a join string.
		/// </summary>
		/// <returns>Nothing by default. If overridden, returns manually built SQL join string.</returns>
        public virtual string BuildJoins()
        {
            return String.Empty;
        }

        //----------------------------------------------------------------------
		/// <summary>
		/// Override this function to build a where string.
		/// </summary>
		/// <returns>Nothing by default. If overridden, returns manually built SQL where string.</returns>
        protected virtual string BuildWhere()
        {
            return String.Empty;
        }

		//----------------------------------------------------------------------
		/// <summary>
		/// Override this function to build an ORDER BY string.
		/// </summary>
		/// <returns>Nothing by default. If overridden, returns manually built SQL ORDER BY string.</returns>
        protected virtual string BuildOrderBy()
        {
            return String.Empty;
        }

        //----------------------------------------------------------------------
		/// <summary>
		/// Called by Compile() to build pagination from paging parameters.
		/// </summary>
        protected void SetPaging()
        {
            if (pageSize == 0)
                return;

            int firstRecord = pageIndex * pageSize + 1;
            int lastRecord = firstRecord + pageSize - 1;

            StringBuilder pagingBuilder = new StringBuilder();

            pagingBuilder.Append(Const.SelectAllFrom);
            pagingBuilder.AppendFormat(Const.PagingSelect, idColumn, SelectColumns, tableName);

            if (!String.IsNullOrEmpty(joinSQL))
                pagingBuilder.AppendFormat(Const.PagingAppend, joinSQL);

            if (!String.IsNullOrEmpty(whereSQL))
				pagingBuilder.AppendFormat(Const.PagingAppend, whereSQL);

            pagingBuilder.AppendFormat(Const.PagingResult, firstRecord, lastRecord);

            selectSQL = pagingBuilder.ToString();
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Adds parameters for stored procedures
        /// </summary>
        /// <param name="name">Parameter name, precede with '@'</param>
        /// <param name="value">Value of paramter</param>
		protected void AddStoredProcedureParam<TValue>(string name, TValue value)
        {
            if (sqlParams == null)
                sqlParams = new Dictionary<string, object>(20);

            if (value == null)
                sqlParams.Add(name, DBNull.Value);
			else
				sqlParams.Add(name, value);
        }

        //----------------------------------------------------------------------
        private void SetPrimaryKey(string column)
        {
            AddParam(column, Const.PrimaryKeyParam, dataObj.PrimaryKey);
        }

		//----------------------------------------------------------------------
        /// <summary>
        /// Adds parameters for auto-generated insert and update SQL.
        /// Automatically assigns the parameter name as "@column".
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="column">SourceColumnName name in the database table</param>
        /// <param name="value">The value.</param>
		protected void AddParam<TValue>(string column, TValue value)
		{
			AddParam(column, Const.ParamPrefix + column, value);
		}

        //----------------------------------------------------------------------
        /// <summary>
        /// Adds parameters for auto-generated insert and update SQL.
        /// </summary>
        /// <param name="column">SourceColumnName name in the database table</param>
        /// <param name="name">Parameter name, perceded with '@'</param>
        /// <param name="value">Value of parameter</param>
		protected void AddParam<TValue>(string column, string name, TValue value)
        {
			// primary key column (idColumn) is automatically added in Init()
            // Id column should already be in collection 
			if (columnParamMap != null && columnParamMap.ContainsKey(column)) return;

            if (sqlParams == null) sqlParams = new Dictionary<string, object>(20);
            if (columnParamMap == null) columnParamMap = new Dictionary<string, string>(20);
			if (columnTypeMap == null) columnTypeMap = new Dictionary<string, Type>(20);

            if (value == null)
                sqlParams.Add(name, DBNull.Value);
            else
                sqlParams.Add(name, value);

            columnParamMap.Add(column, name);
			columnTypeMap.Add(column, typeof(TValue));
        }

        //----------------------------------------------------------------------
		/// <summary>
		/// Utility function to truncate a given string to a max size.
		/// </summary>
		/// <param name="toTruncate">String to truncate.</param>
		/// <param name="maxSize">MAX size allowed for the string.</param>
		/// <returns>Truncated string.</returns>
        protected string TruncateString(string toTruncate, int maxSize)
        {
            if (toTruncate == null)
                return toTruncate;

            return toTruncate.Length > maxSize ? toTruncate.Substring(0, maxSize) : toTruncate;
        }

#if DEBUG
		//----------------------------------------------------------------------
		public override string ToString()
		{
			string outString = "TABLE: " + this.tableName;
			if (this.TransactionType == TransactionType.Select)
				outString += " SELECT: " + this.Select;
			else 
				outString += " " + TransactionType.ToString("G") + " VALUES: ";

			if (Params != null && Params.Count > 0)
			{
				foreach (KeyValuePair<string, object> pairs in Params)
				{
					outString += String.Format("{0} = {1} | ", pairs.Key, pairs.Value);
				}
			}

			outString = outString.Remove(outString.Length - 2, 2);
			return outString;
		}

    
#endif

#if !PocketPC
		//----------------------------------------------------------------------
		public System.Data.DataTable BuildDataTable()
		{
			if (columnTypeMap == null || columnTypeMap.Count == 0)
				throw new Exception("Unable to create data table because transaction lacks column data. This is probably because the transaction parameters use the wrong version of AddParam().");

			System.Data.DataTable table = new System.Data.DataTable(this.tableName);

		    table.BeginInit();
			foreach (KeyValuePair<string, Type> pair in columnTypeMap)
			{
				table.Columns.Add(CreateDataColumn(pair.Key, pair.Value));
			}
            table.EndInit();

			return table;
		}

		//----------------------------------------------------------------------
        public IDictionary<string, object> GetData()
        {
            if (columnParamMap == null || columnParamMap.Count == 0)
                throw new Exception("Unable to create data row because transaction lacks column data. This is probably because the transaction parameters use the wrong version of AddParam().");

            Dictionary<string, object> data = new Dictionary<string, object>(20);
            foreach (KeyValuePair<string, string> pair in columnParamMap)
            {
                string columnName = pair.Key;
                string paramName = pair.Value;
                data.Add(columnName, sqlParams[paramName]);
            }

            return data;
        }


        //----------------------------------------------------------------------
		public void AddDataRow(System.Data.DataTable table)
		{
			if (columnParamMap == null || columnParamMap.Count == 0)
				throw new Exception("Unable to create data row because transaction lacks column data. This is probably because the transaction parameters use the wrong version of AddParam().");

			System.Data.DataRow row = table.NewRow();
			foreach (KeyValuePair<string, string> pair in columnParamMap)
			{
				string columnName = pair.Key;
				string paramName = pair.Value;
				object value = sqlParams[paramName];

				row[columnName] = value;
			}

			table.Rows.Add(row);
		}

		//----------------------------------------------------------------------
		public void CreateColumnMapping(System.Data.SqlClient.SqlBulkCopyColumnMappingCollection mappingCollection)
		{
		    mappingCollection.Clear();

			foreach (KeyValuePair<string, string> pair in columnParamMap)
			{
				mappingCollection.Add(new System.Data.SqlClient.SqlBulkCopyColumnMapping(pair.Key, pair.Key));
			}
		}

		//----------------------------------------------------------------------
		private static System.Data.DataColumn CreateDataColumn(string columnName, Type columnType)
		{
			if (columnType == typeof(DateTime?))
				columnType = typeof(DateTime);
			else if (columnType == typeof(double?))
				columnType = typeof(double);
			else if (columnType == typeof(Guid?))
				columnType = typeof(Guid);
			else if (columnType == typeof(int?))
				columnType = typeof(int);
			else if (columnType == typeof(float?))
				columnType = typeof(float);

			return new System.Data.DataColumn {DataType = columnType, ColumnName = columnName};
		}
#endif

		//----------------------------------------------------------------------
		protected DateTime PrepareDate(DateTime inDate)
		{
			if (inDate.Year < 1753)
			{
				DateTime newDate = new DateTime(1900, inDate.Month, inDate.Day, inDate.Hour, inDate.Minute, inDate.Second, inDate.Millisecond);
				return newDate;
			}
			return inDate;
		}

		//----------------------------------------------------------------------
		protected DateTime? PrepareDate(DateTime? inDate)
		{
			if (inDate == null)
				return new DateTime(1900, 1, 1);

			if (inDate.Value.Year < 1753)
			{
				DateTime newDate = new DateTime(1900, inDate.Value.Month, inDate.Value.Day, inDate.Value.Hour, inDate.Value.Minute, inDate.Value.Second, inDate.Value.Millisecond);
				return newDate;
			}
			return inDate;
		}

		//----------------------------------------------------------------------
		/// <summary>
		/// Gets and clears the transaction errors after exectution.
		/// </summary>
		/// <returns>string representing all the transaction errors.</returns>
		public string GetAndClearErrors()
		{
			if (_reader == null)
				return this.tableName + " - No Errors";

			return this.tableName + " - " + _reader.GetAndClearErrors();
		}

		//----------------------------------------------------------------------
        /// <summary>
        /// Resets this instance back to a precompiled state
        /// </summary>
        public void Reset()
        {
            compiled = false;

            if (columnParamMap != null) columnParamMap.Clear();
            if (columnTypeMap != null) columnTypeMap.Clear();
#if PocketPC
            if (columnOrdinalDictionary != null) columnOrdinalDictionary.Clear();
#endif
            selectSQL = string.Empty;
            joinSQL = string.Empty;
            insertSQL = string.Empty;
            whereSQL = string.Empty;
        }
    }
}
