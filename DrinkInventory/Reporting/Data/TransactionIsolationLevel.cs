using System;
using System.Data;
using System.Data.SqlClient;

namespace Jaxis.DrinkInventory.Reporting.Data
{
    /// <summary>
    /// Transaction Isolation Level.
    /// With thanks to Dierderik Krols, http://blogs.u2u.be/diederik/2010/08/default.aspx.
    /// </summary>
    public class TransactionIsolationLevel : IDisposable
    {
        /// <summary>
        /// The database connection.
        /// </summary>
        private IDbConnection connection;

        /// <summary>
        /// Original isolation level of the connection.
        /// </summary>
        private IsolationLevel originalIsolationLevel;

        /// <summary>
        /// Initializes a new instance of the TransactionIsolationLevel class.
        /// </summary>
        /// <param name="_connection">Database connection.</param>
        /// <param name="_isolationLevel">Required isolation level.</param>
        public TransactionIsolationLevel( IDbConnection _connection, IsolationLevel _isolationLevel )
        {
            this.connection = _connection;
            this.originalIsolationLevel = this.connection.GetIsolationLevel( );
            this.connection.SetIsolationLevel( _isolationLevel );
        }

        /// <summary>
        /// Resets the isolation level back to the original value.
        /// </summary>
        public void Dispose( )
        {
            this.connection.SetIsolationLevel( this.originalIsolationLevel );
        }
    }
}
