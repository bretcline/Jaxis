using System;
using System.Collections.Generic;
using System.Data;
using System.Data.EntityClient;
using System.Data.SqlClient;

namespace Jaxis.DrinkInventory.Reporting.Data
{
    /// <summary>
    /// Extension methods for database connection.
    /// With thanks to Dierderik Krols, http://blogs.u2u.be/diederik/2010/08/default.aspx.
    /// </summary>
    public static class ConnectionExtensions
    {
        /// <summary>
        /// Translates an isolation level to its T-SQL name.
        /// </summary>
        private static Dictionary<IsolationLevel, string> isolationLevels = new Dictionary<IsolationLevel, string>();

        static ConnectionExtensions()
        {
            isolationLevels[IsolationLevel.ReadUncommitted] = "READ UNCOMMITTED";
            isolationLevels[IsolationLevel.ReadCommitted] = "READ COMMITTED";
            isolationLevels[IsolationLevel.RepeatableRead] = "REPEATABLE READ";
            isolationLevels[IsolationLevel.Snapshot] = "SNAPSHOT";
            isolationLevels[IsolationLevel.Serializable] = "SERIALIZABLE";
        }

        /// <summary>
        /// Gets the current transaction isolation level on a connection.
        /// </summary>
        /// <param name="_connection">The connection.</param>
        /// <returns>The transaction isolation level.</returns>
        public static IsolationLevel GetIsolationLevel(this IDbConnection _connection)
        {
            string query =
                @"SELECT CASE transaction_isolation_level
                            WHEN 0 THEN 'Unspecified'
                            WHEN 1 THEN 'ReadUncommitted'
                            WHEN 2 THEN 'ReadCommitted'
                            WHEN 3 THEN 'RepeatableRead'
                            WHEN 4 THEN 'Serializable'
                            WHEN 5 THEN 'Snapshot'
                            END AS [Transaction Isolation Level]
                    FROM sys.dm_exec_sessions
                    WHERE session_id = @@SPID";

            if (_connection is EntityConnection)
            {
                return (_connection as EntityConnection).StoreConnection.GetIsolationLevel();
            }
            else if (_connection is SqlConnection)
            {
                IDbCommand command = _connection.CreateCommand();
                command.CommandText = query;
                string result = command.ExecuteScalar().ToString();

                return (IsolationLevel)Enum.Parse(typeof(IsolationLevel), result);
            }

            return IsolationLevel.Unspecified;
        }

        /// <summary>
        /// Sets the transaction level on a connection.
        /// </summary>
        /// <param name="_connection">The connection.</param>
        /// <param name="_isolationLevel">The new isolation level.</param>
        public static void SetIsolationLevel(this IDbConnection _connection, IsolationLevel _isolationLevel)
        {
            if (_isolationLevel == IsolationLevel.Unspecified || _isolationLevel == IsolationLevel.Chaos)
            {
                throw new Exception(string.Format("Isolation Level '{0}' cannot be set.", _isolationLevel.ToString()));
            }

            if (_connection is EntityConnection)
            {
                SqlConnection sqlConnection = (_connection as EntityConnection).StoreConnection as SqlConnection;
                sqlConnection.SetIsolationLevel(_isolationLevel);
            }
            else if (_connection is SqlConnection)
            {
                IDbCommand command = _connection.CreateCommand();
                command.CommandText = string.Format("SET TRANSACTION ISOLATION LEVEL {0}", isolationLevels[_isolationLevel]);
                command.ExecuteNonQuery();
            }
        }
    }
}
