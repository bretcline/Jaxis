using System;
using System.Collections.Generic;
using System.Data;
#if PocketPC
using System.Data.SqlServerCe;
#else
using System.Data.SqlClient;
#endif

namespace LFI.Sync.DataManager
{
	public class ConnectionObject
	{
		public ConnectionObject(IDbConnection connection, IDbCommand command, bool inUse, int id)
		{
			Command = command;
			Connection = connection;
			IsInUse = inUse;
			ID = id;
		}

		public IDbCommand Command;
		public IDbConnection Connection;
		public bool IsInUse;
		public int ID;
	}

	public class ConnectionPool : IDisposable 
	{
		public int CommandTimeOutSeconds { get; set; }
		private readonly List<ConnectionObject> _connections;
		private int _connectionsInUse;
		private readonly object _connectionLock;
		private readonly object _returnLock;

		//----------------------------------------------------------------------
		public string ConnectionString
		{
			get;
			private set;
		}

		//----------------------------------------------------------------------
		public ConnectionPool(string connectionString)
		{
			_connections = new List<ConnectionObject>();
			ConnectionString = connectionString;
			_connectionLock = new object();
			_returnLock = new object();
		}

		//----------------------------------------------------------------------
		/// <summary>
		/// Gets the first available connection.
		/// </summary>
		/// <returns>An open connection</returns>
		public ConnectionObject GetConnection()
		{
			// gets first available connection or creates a connection if none are available
			// each connection is left open for compact edition
			lock (_connectionLock)
			{
				++_connectionsInUse;

				// loop through _connections until an available one is found
				ConnectionObject connectionObject = null;
				foreach (ConnectionObject connectionObj in _connections)
				{
					if (!connectionObj.IsInUse)
					{
						connectionObject = connectionObj;
					}
				}
				if (connectionObject != null)
					connectionObject.IsInUse = true;
				// if not found, create new connection and add to connection pool
				else
				{
					connectionObject = CreateConnectionObject();
					_connections.Add(connectionObject);
				}

				// make sure connection is open
				if (connectionObject.Connection.State != ConnectionState.Open)
					connectionObject.Connection.Open();

				return connectionObject;
			}
		}

		//----------------------------------------------------------------------
		/// <summary>
		/// Creates an open connection object that is not added to the pool
		/// </summary>
		public ConnectionObject GetUnmanagedConnection()
		{
			ConnectionObject connection = CreateConnectionObject();
			connection.ID = -1;
			connection.Connection.Open();

			return connection;
		}

		//----------------------------------------------------------------------
		private ConnectionObject CreateConnectionObject()
		{
			IDbConnection connection = null;
			IDbCommand command = null;

#if PocketPC
			connection = new SqlCeConnection(ConnectionString);
			command = new SqlCeCommand();
#else
			connection = new SqlConnection(ConnectionString);
			command = new SqlCommand();
#endif
			command.Connection = connection;
			command.CommandTimeout = CommandTimeOutSeconds;

			return new ConnectionObject(connection, command, true, _connectionsInUse);
		}

		//----------------------------------------------------------------------
		/// <summary>
		/// Returns a connection back to the connection pool
		/// </summary>
		public void Return(ConnectionObject connection)
		{
			lock (_returnLock)
			{
				--_connectionsInUse;

				// find connection in pool and set it to available
				foreach (ConnectionObject connectionObj in _connections)
				{
					if (connectionObj == connection)
					{
						connection.IsInUse = false;
						break;
					}
				}
			}
		}

		//----------------------------------------------------------------------
		public void Close()
		{
			foreach (ConnectionObject connection in _connections)
			{
				connection.Connection.Close();
			}

			_connectionsInUse = 0;
		}

		//----------------------------------------------------------------------
		public void Dispose()
		{
			Close();
		}
	}
}