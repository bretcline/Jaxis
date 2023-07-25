using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LFI.Sync.DataManager
{
	public class DataManager
	{
		public static readonly DateTime KMinDateTime = new DateTime(1753, 1, 1);
		private ConnectionPool connectionPool;

	    public const int BulkCopyBatchSize = 50000;
		public string DataSource { get; private set; }
		public string InitialCatalog { get; private set; }
		public Guid SourceDB { get; set; }
		public int CommandTimeOutSeconds { get { return connectionPool.CommandTimeOutSeconds; } set { connectionPool.CommandTimeOutSeconds = value; } }
		public int TimeZoneOffset { get; set; }

		public string SourceName { get; set; }

		public string ConnectionString { get { return connectionPool.ConnectionString; } }

		//----------------------------------------------------------------------
		public DataManager(string connectionString)
		{
			Init(connectionString);
		}

		//----------------------------------------------------------------------
		public DataManager(string sourceName, string connectionString)
		{
			SourceName = sourceName;
			Init(connectionString);
		}

		//----------------------------------------------------------------------
		public DataManager(string sourceName, Guid sourceDB, string connectionString)
		{
			SourceDB = sourceDB;
			SourceName = sourceName;
			Init(connectionString);
		}

		//----------------------------------------------------------------------
		private void Init(string connectionString)
		{
			try
			{
				int dataSourceStart = connectionString.IndexOf('=', connectionString.IndexOf("Data Source"));
				DataSource = connectionString.Substring(dataSourceStart + 1, connectionString.IndexOf(';', dataSourceStart) - dataSourceStart - 1);
				int initialCatalogStart = connectionString.IndexOf('=', connectionString.IndexOf("Initial Catalog"));
				InitialCatalog = connectionString.Substring(initialCatalogStart + 1, connectionString.IndexOf(';', initialCatalogStart) - initialCatalogStart - 1);

				string builtConnectionString = BuildConnectionString(DataSource, InitialCatalog);
				connectionPool = new ConnectionPool(builtConnectionString);
				CommandTimeOutSeconds = 600; // 10 minute default timeout

				string testSuccess = TestConnection();
				if (testSuccess != "SUCCESS")
					throw new Exception(String.Format("TestConnection() failed for connection string '{0}'.", connectionPool.ConnectionString));
			}
			catch (Exception ex)
			{
				throw new Exception("Failed to build connection string in DataManager.Init().", ex);
			}
		}

		//----------------------------------------------------------------------
		public void Close()
		{
			connectionPool.Close();
		}

		//----------------------------------------------------------------------
		public bool CheckExists(string tableName, Dictionary<string, object> columnValueMap)
		{
			return CheckExists(tableName, "*", columnValueMap);
		}

		//----------------------------------------------------------------------
		/// <summary>
		/// Checks if a record with a specified column value exists in the database 
		/// </summary>
		/// <param name="tableName">Table to be checked</param>
		/// <param name="columnValueMap">Column and value pairs to compare for existence</param>
		/// <returns>True if exists, false otherwise</returns>
		public bool CheckExists(string tableName, string countColumn, Dictionary<string, object> columnValueMap)
		{
			const string existsSqlFormat = "SELECT Count({0}) FROM {1} WHERE {2}";

			string compareSql = String.Empty;
			int index = 1;
			foreach (KeyValuePair<string, object> pair in columnValueMap)
			{
				compareSql += String.Format(" {0} = '{1}'", pair.Key, pair.Value);
				if (index < columnValueMap.Count)
					compareSql += " AND";
				++index;
			}

			ConnectionObject connection = connectionPool.GetConnection();
			connection.Command.CommandText = String.Format(existsSqlFormat, countColumn, tableName, compareSql);
			connection.Command.CommandType = CommandType.Text;

			int count = (int)connection.Command.ExecuteScalar();

			connectionPool.Return(connection);
			return count > 0;
		}

		//----------------------------------------------------------------------
		public void CalculateTimeZoneOffset()
		{
			DateTime dataSourceTime = (DateTime)InvokeRawQueryScalar("SELECT GetDate()");
			TimeSpan diff = dataSourceTime - DateTime.Now;
			double hours = Math.Round(diff.TotalHours);
			TimeZoneOffset = (int)hours;
		}

        //----------------------------------------------------------------------
        /// <summary>
        /// Gets the result count, the number of items returned by the specified transaction
        /// </summary>
        /// <param name="transaction">The transaction to process</param>
        /// <returns>The result count</returns>
        public int GetResultCountByKey(ITransaction transaction)
        {
            transaction.SetTransactionType(TransactionType.ResultCount);
            transaction.Compile(CompileCondition.CountOnly);
            ConnectionObject connection = connectionPool.GetConnection();

            connection.Command.CommandText = transaction.Select;
            connection.Command.CommandType = CommandType.Text;

            int count = 0;
            try
            {
                count = (int)connection.Command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured in GetResultCount for transaction type: {0}", transaction.GetType()), ex);
            }

            connectionPool.Return(connection);
            return count;
        }

	    //----------------------------------------------------------------------
        /// <summary>
        /// Gets the result count, the number of items returned by the specified transaction
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="transaction">The transaction to process</param>
        /// <returns>The result count</returns>
        public int GetResultCount(ITransaction transaction)
		{
			transaction.SetTransactionType(TransactionType.ResultCount);
            transaction.Compile();

			ConnectionObject connection = connectionPool.GetConnection();

			connection.Command.CommandText = transaction.Select;
			connection.Command.CommandType = CommandType.Text;

            int count = 0;
            try
            {
                count = (int)connection.Command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured in GetResultCount for transaction type: {0}", transaction.GetType()), ex);
            }

            connectionPool.Return(connection);
			return count;
		}

		//----------------------------------------------------------------------
		public List<T> GetData<T>(IReaderTransaction<T> transaction)
		{
		    transaction.Compile();

			ConnectionObject connection = connectionPool.GetConnection();

			connection.Command.CommandText = transaction.Select;
			connection.Command.CommandType = CommandType.Text;
			TransactionReader reader = null;
			try
			{
				reader = new TransactionReader(connection.Command.ExecuteReader(), TimeZoneOffset);
				List<T> outData = transaction.FromReader(reader);
				return outData;
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("Error occured in GetData for transaction type: {0}", transaction.GetType()), ex);
			}
			finally
			{
                if(reader !=  null)
                    reader.Dispose();
			    
				connectionPool.Return(connection);
			}
		}

		//----------------------------------------------------------------------
		public IList<Dictionary<string, object>> GetDataRows<T>(IReaderTransaction<T> transaction)
		{
			IList<Dictionary<string, object>> outRows = new List<Dictionary<string, object>>();
			
			transaction.Compile();
            ConnectionObject connection = connectionPool.GetConnection();
            connection.Command.CommandText = transaction.Select;
			connection.Command.CommandType = CommandType.Text;
			TransactionReader reader = null;

			try
			{
				reader = new TransactionReader(connection.Command.ExecuteReader(), TimeZoneOffset);
				while (reader.ReadNextRow())
				{
					outRows.Add(reader.GetCurrentRow());
				}

				return outRows;

			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("Error occured in GetData for transaction type: {0}", transaction.GetType()), ex);
			}
			finally
			{
				if (reader != null)
					reader.Dispose();

				connectionPool.Return(connection);
			}
		}

		//----------------------------------------------------------------------
		public T GetFirstDataResult<T>(IReaderTransaction<T> transaction)
		{
			List<T> data = GetData(transaction);
			if (data.Count > 0)
				return data[0];

			return default(T);
		}

		//----------------------------------------------------------------------
		public List<T> GetStoredProcedureData<T>(IReaderTransaction<T> transaction)
		{
			transaction.Compile();

			ConnectionObject connection = connectionPool.GetConnection();

			connection.Command.CommandText = transaction.Select;
			connection.Command.CommandType = CommandType.StoredProcedure;
			connection.Command.Parameters.Clear();

			foreach (KeyValuePair<string, object> keyValPair in transaction.Params)
			{
				IDbDataParameter param = ParamFactory.CreateParam(keyValPair.Key, keyValPair.Value);
				connection.Command.Parameters.Add(param);
				
			}

			TransactionReader reader = null;
			try
			{
				reader = new TransactionReader(connection.Command.ExecuteReader());
				List<T> outData = transaction.FromReader(reader);
				return outData;
			}
			finally
			{
				if (reader != null)
					reader.Dispose();

				connectionPool.Return(connection);
			}
		}

		//----------------------------------------------------------------------
		public TransactionResult PutData(ITransaction transaction, TransactionType type)
		{
			transaction.SetTransactionType(type);
			return PutData(transaction);
		}

		//----------------------------------------------------------------------
		/// <summary>
		/// Inserts or Updates a single transaction
		/// </summary>
		public TransactionResult PutData(ITransaction transaction)
		{
			transaction.Compile();
			TransactionResult result = new TransactionResult();
			ConnectionObject connection = connectionPool.GetConnection();

			try
			{
				connection.Command.CommandText = (transaction.TransactionType == TransactionType.Insert) ? transaction.Insert : transaction.Update;
				connection.Command.CommandType = CommandType.Text;
				connection.Command.Parameters.Clear();

				foreach (KeyValuePair<string, object> keyValPair in transaction.Params)
				{
					connection.Command.Parameters.Add(ParamFactory.CreateParam(keyValPair.Key, keyValPair.Value));
				}

				int rowsAffected = connection.Command.ExecuteNonQuery();
				result.Success = true;
				result.PrimaryKey = transaction.PrimaryKey.ToString();
				result.Message = String.Format("{0} rows successfully affected.", rowsAffected);

				if (rowsAffected == 0 && transaction.TransactionType == TransactionType.Update && result.Success)
				{
					result.Success = false;
					result.Message = "Update ran successfully but did not update any rows. Check object Guid to make sure there is a matching object in the database.";
				}

				return result;
			}
			catch (Exception ex)
			{
				result.Success = false;
				result.PrimaryKey = null;
				result.Message = BuildExceptionString(ex);
				result.TransactionInfo = transaction.ToString();

				return result;
			}
			finally
			{
				connectionPool.Return(connection);
			}
		}

		//----------------------------------------------------------------------
		/// <summary>
		/// Intended for inserting/updating multiple transaction of the same type
		/// </summary>
		public List<TransactionResult> PutDataBatch(List<ITransaction> transactions)
		{
			List<TransactionResult> results = new List<TransactionResult>(50);
			if (transactions.Count == 0)
				return results;

			ConnectionObject connection = connectionPool.GetConnection();

			try
			{
				connection.Command.CommandType = CommandType.Text;

				for (int curTrans = 0; curTrans < transactions.Count; ++curTrans)
				{
					TransactionResult result = new TransactionResult();
					ITransaction transaction = transactions[curTrans];

					transaction.Compile();
					connection.Command.Parameters.Clear();
					foreach (KeyValuePair<string, object> keyValPair in transaction.Params)
					{
						connection.Command.Parameters.Add(ParamFactory.CreateParam(keyValPair.Key, keyValPair.Value));
					}

					try
					{
						connection.Command.CommandText = (transaction.TransactionType == TransactionType.Insert) ? transaction.Insert : transaction.Update;
						int rowsAffected = connection.Command.ExecuteNonQuery();
						result.Success = true;
                        result.PrimaryKey = transaction.PrimaryKey.ToString();
                        result.Message = String.Format("{0} rows successfully affected.", rowsAffected);

                        if (rowsAffected == 0 && result.Success)
                        {
                            result.Success = false;
                            result.Message = "Statement ran successfully but did not update any rows.";
                        }

						results.Add(result);
					}
					catch (Exception ex)
					{
						result.Success = false;
						result.Message = BuildExceptionString(ex);
						result.TransactionInfo = transaction.ToString();

						results.Add(result);
					}

				    ((IDisposable) transaction).Dispose();
				}
				return results;
			}
			finally
			{
			    GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
                connectionPool.Return(connection);
			}
		}

		//----------------------------------------------------------------------
        /// <summary>
        /// Does a bulk copy of a set of the same type of transaction instances to a sql database.
        /// </summary>
        /// <param name="transactions">The transactions to bulk insert.</param>
        /// <returns></returns>
        //public TransactionResult DoBulkCopy(List<ITransaction> transactions)
        //{
        //    TransactionResult result = new TransactionResult {Success = true, Message = "Transaction list was empty."};

        //    if (transactions.Count == 0) return result;

        //    ConnectionObject connection = connectionPool.GetConnection();
        //    SqlBulkCopy bulkCopy = new SqlBulkCopy(connection.Connection.ConnectionString, SqlBulkCopyOptions.TableLock)
        //    {
        //        BulkCopyTimeout = CommandTimeOutSeconds,
        //        BatchSize = BulkCopyBatchSize
        //    };
        //    BulkTransactionReader reader = new BulkTransactionReader(transactions);

        //    try
        //    {
        //        // Compile the first transaction in order to get the 
        //        // set of column mapping necessary for the bulk copy
        //        transactions[0].Compile();
        //        transactions[0].CreateColumnMapping(bulkCopy.ColumnMappings);

        //        bulkCopy.DestinationTableName = transactions[0].Table;

        //        bulkCopy.WriteToServer(reader);

        //        result.Success = true;
        //        result.Message = "Bulk Copy completed with no errors.";
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Success = false;
        //        result.Message = BuildExceptionString(ex);
        //    }
        //    finally
        //    {
        //        bulkCopy.Close();
        //        reader.Close();
        //        reader.Dispose();

        //        connectionPool.Return(connection);
                
        //        // Force GC to release managed/unmanaged memory
        //        GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        //    }

        //    return result;
        //}

		//----------------------------------------------------------------------
		public void InvokeRawQuery( string querySQL, CommandBehavior behavior, Action<IDataReader> RawQueryHandler )
		{
			ConnectionObject connection = connectionPool.GetUnmanagedConnection();

			connection.Command.CommandText = querySQL;
			connection.Command.CommandType = CommandType.Text;

			try
			{
                RawQueryHandler( connection.Command.ExecuteReader( behavior ) );
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("Error occured in InvokeRawQuery: '{0}'", querySQL), ex);
			}
			finally
			{
				if (behavior != CommandBehavior.CloseConnection)
				{
					connection.Connection.Close();
				}
			}
		}

		//----------------------------------------------------------------------
		public object InvokeRawQueryScalar(string querySQL)
		{
			ConnectionObject connection = connectionPool.GetUnmanagedConnection();

			connection.Command.CommandText = querySQL;
			connection.Command.CommandType = CommandType.Text;

			try
			{
				return connection.Command.ExecuteScalar();
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("Error occured in InvokeRawQueryScalar: '{0}'", querySQL), ex);
			}
			finally
			{
				connection.Connection.Close();
			}
		}

		//----------------------------------------------------------------------
		public TransactionResult InvokeRawNonQuery(string nonQuerySQL)
		{
			TransactionResult result = new TransactionResult();
			ConnectionObject connection = connectionPool.GetConnection();

			try
			{
				connection.Command.CommandText = nonQuerySQL;
				connection.Command.CommandType = CommandType.Text;

				int rowsAffected = connection.Command.ExecuteNonQuery();
				result.Success = true;
				result.Message = String.Format("{0} rows successfully affected.", rowsAffected);

				return result;
			}
			catch (Exception ex)
			{
				result.Success = false;
				result.Message = BuildExceptionString(ex);
				return result;
			}
			finally
			{
				connectionPool.Return(connection);
			}
		}

		//----------------------------------------------------------------------
		public string TestConnection()
		{
			IDbConnection connection = null;
			if (!String.IsNullOrEmpty(DataSource) && !String.IsNullOrEmpty(InitialCatalog))
			{
				string connectionString = String.Format("Data Source={0};Initial Catalog={1};Integrated Security=SSPI;Connection Timeout=10; pooling='false'; ", DataSource, InitialCatalog);
				connection = new System.Data.SqlClient.SqlConnection(connectionString);
			}
			else
				connection = new System.Data.SqlClient.SqlConnection(connectionPool.ConnectionString);

			try
			{
				connection.Open();
				return "SUCCESS";
			}
			catch (Exception ex)
			{
				string message = String.Format("Unable to connect to database '{0}'. with Exception: {1}.", SourceName, BuildExceptionString(ex));
				return message;
			}
			finally
			{
				connection.Close();
			}
		}

		//----------------------------------------------------------------------
		public static bool TestConnection(string dataSource, string initialCatalog, out string message)
		{
			System.Data.SqlClient.SqlConnection connection = null;

			try
			{
				string connectionString = BuildConnectionString(dataSource, initialCatalog);
				connection = new System.Data.SqlClient.SqlConnection(connectionString);
				connection.Open();
				message = "SUCCESS";
				return true;
			}
			catch (Exception ex)
			{
				message = String.Format("Unable to connect to database '{0}'. {1}.", initialCatalog, BuildExceptionString(ex));
				return false;
			}
			finally
			{
				if (connection != null && connection.State != ConnectionState.Closed)
					connection.Close();
			}
		}

		//----------------------------------------------------------------------
		public static string BuildConnectionString(string dataSource, string initialCatalog)
		{
			return String.Format("Data Source={0};Initial Catalog={1};Integrated Security=SSPI;Connection Timeout=120; pooling='false';", dataSource, initialCatalog);
		}

		//----------------------------------------------------------------------
		public static string BuildConnectionString(string dataSource, string initialCatalog, int timeOutSeconds)
		{
			return String.Format("Data Source={0};Initial Catalog={1};Integrated Security=SSPI;Connection Timeout={2}; pooling='false';", dataSource, initialCatalog, timeOutSeconds);
		}

		//----------------------------------------------------------------------
		public static DataManager FromConfig(string dataSource, string initialCatalog)
		{
			string connectionString = BuildConnectionString(dataSource, initialCatalog);
			return new DataManager(initialCatalog, Guid.Empty, connectionString);
		}

		//----------------------------------------------------------------------
		public static DataManager FromConfig(string dataSource, string initialCatalog, int timeOutSeconds)
		{
			string connectionString = BuildConnectionString(dataSource, initialCatalog, timeOutSeconds);
			return new DataManager(initialCatalog, Guid.Empty, connectionString);
		}

		//----------------------------------------------------------------------
		public static DataManager FromServerAndInstance(string server, string instance)
		{
			string connectionStr = BuildConnectionString(server, instance);
			return new DataManager(connectionStr);
		}

		//----------------------------------------------------------------------
		public static string BuildExceptionString(Exception ex)
		{
			string outException = String.Format("Exception: {0}\r\n", ex.Message);

			while (ex.InnerException != null)
			{
				ex = ex.InnerException;
				outException += String.Format("\tInner Exception: {0}\r\n", ex.Message);
			}

			return outException;
		}
	}
}
