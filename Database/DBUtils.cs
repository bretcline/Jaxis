#define ENABLE_TIMER

using System;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Web;
using System.Collections;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using JaxisExtensions;

namespace Jaxis.Utilities.Database
{
    public enum DBTypes
    {
        Undefinded,
        SqlServer,
        OleDB,
        ODBC,
    }

    /// <summary>
    /// Summary description for SqlTool.
    /// </summary>
    public class SqlTool : IDisposable
    {

        public class InvalidColumnException : Exception
        {
            public InvalidColumnException( string _Message )
                : base( "The column: " + _Message + " is invalid." )
            {

            }
        }

        #region Protected Member variables

        protected DBTypes m_DatabaseType = DBTypes.Undefinded;
        protected string m_ConnectionString = string.Empty;
        protected bool m_Schema = false;
        //		protected bool m_Schema = true;
        protected bool m_LogMessages = false;
        protected StringBuilder m_Messages = new StringBuilder( );

        #endregion //Protected Member variables

        #region Private Member variables

        private string m_Error = "";
        private IDbConnection m_Conn;
        private IDbTransaction m_Transaction = null;
        private string m_ProcPrefix = null;
        private int m_Timeout = 0;
        private string m_ApplicationName;

        
        protected IDbConnection DBConnection
        {
            get
            {
                return Open( );
            }
        }

        #endregion // Private Member variables

        #region Public Properties

        
        public bool Schema
        {
            get
            {
                return m_Schema;
            }
            set
            {
                m_Schema = value;
            }
        }


        
        public bool LogMessages
        {
            get
            {
                return m_LogMessages;
            }
            set
            {
                m_LogMessages = value;
            }
        }

        
        public string Messages
        {
            get
            {
                string rc = m_Messages.ToString( );
                m_Messages.Remove( 0, m_Messages.Length );
                return rc;
            }
        }

        
        public int Timeout
        {
            get
            {
                return m_Timeout;
            }
            set
            {
                m_Timeout = value;
            }
        }

        
        public string ProcPrefix
        {
            get
            {
                string rc = "dbo";
                if( m_ProcPrefix != null && m_ProcPrefix.Length > 0 )
                {
                    rc = m_ProcPrefix;
                }
                return rc;
            }
            set
            {
                m_ProcPrefix = value;
                if( true == m_ProcPrefix.EndsWith( "." ) )
                {
                    m_ProcPrefix = m_ProcPrefix.Remove( m_ProcPrefix.Length - 1, 1 );
                }
            }
        }

        
        // Property for the Connection string
        
        public string ConnectionString
        {
            get
            {
                if( 0 == m_ConnectionString.Length )
                {
                    m_ConnectionString = "server=%SERVERNAME%;database=%DATABASE%;uid=%UID%;pwd=%PWD%;";
                }
                return m_ConnectionString;
            }
            set
            {
                m_ConnectionString = value;
            }
        }

        
        public string Server
        {
            set
            {
                ReplaceConnectionStringValue( "server=", ";", value );
                Reconnect( );
            }
        }

        
        public string Database
        {
            set
            {
                ReplaceConnectionStringValue( "database=", ";", value );
                Reconnect( );
            }
        }

        
        public string UserID
        {
            set
            {
                ReplaceConnectionStringValue( "uid=", ";", value );
                Reconnect( );
            }
        }

        
        public string Password
        {
            set
            {
                ReplaceConnectionStringValue( "pwd=", ";", value );
                Reconnect( );
            }
        }

        
        public string ApplicationName
        {
            get
            {
                try
                {
                    return m_ApplicationName;
                }
                catch
                {
                    throw;
                }
            }
            set
            {
                try
                {
                    m_ApplicationName = value;
                }
                catch
                {
                    throw;
                }
            }
        }

        
        public string ErrorMsg
        {
            get { return m_Error; }
        }


        
        public DBTypes DatabaseType
        {
            get 
            {
                return m_DatabaseType; 
            }
        }

        #endregion // Public Properties

        
        /// <summary>
        /// 
        /// This updates the connection string the the value passed in.
        /// It assumes the tag is included in _NewValue.  For example,
        /// server={servername} or database={databasename}.
        /// </summary>
        /// <param name="_TagName"></param>
        /// <param name="_TagEnd"></param>
        /// <param name="_NewValue"></param>
        
        private void ReplaceConnectionStringValue( string _TagName, string _TagEnd, string _NewValue )
        {
            try
            {
                int TagStart = m_ConnectionString.ToUpper( ).IndexOf( _TagName.ToUpper( ) );
                int TagEnd = m_ConnectionString.IndexOf( _TagEnd, TagStart );
                string CurrentValue = m_ConnectionString.Substring( TagStart, TagEnd - TagStart );
                m_ConnectionString.Replace( CurrentValue, _NewValue );
            }
            catch
            {
                throw;
            }
        }

        
        private void Reconnect( )
        {
            try
            {
                if( true == IsConnected( m_Conn ) )
                {
                    Close( );
                    Open( );
                }
            }
            catch
            {
                throw;
            }
        }

        #region Public Methods

        
        public bool IsConnected( IDbConnection _Conn )
        {
            bool ret = true;
            if( _Conn == null || _Conn.State == System.Data.ConnectionState.Closed ||
                _Conn.State == System.Data.ConnectionState.Broken )
            {
                ret = false;
            }
            return ret;
        }

        #region Constructor

        
        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="_Type"></param>
        
        public SqlTool( DBTypes _Type )
        {
            m_DatabaseType = _Type;
        }

        
        public SqlTool( DBTypes _Type, string _ConnString )
            : this( _Type, _ConnString, false, false )
        {
        }

        
        public SqlTool( string _ConnString )
            : this( DBTypes.SqlServer, _ConnString, false, false )
        {
        }

        
        public SqlTool( string _ConnString, bool _GetSchema )
            : this( DBTypes.SqlServer, _ConnString, _GetSchema, false )
        {
        }

        public SqlTool( string _ConnString, bool _GetSchema, bool _SaveMessages )
            : this( DBTypes.SqlServer, _ConnString, _GetSchema, false )
        {

        }

        
        public SqlTool( DBTypes _Type, string _ConnString, bool _GetSchema, bool _SaveMessages )
        {
            m_DatabaseType = _Type;
            ConnectionString = _ConnString;
            Schema = _GetSchema;
            m_LogMessages = _SaveMessages;
            Open( );
        }

        
        public SqlTool( IDbConnection _Connection )
        {
            m_Conn = _Connection;
            if( m_Conn is SqlConnection )
            {
                m_DatabaseType = DBTypes.SqlServer;
            }
            else if( m_Conn is OleDbConnection )
            {
                m_DatabaseType = DBTypes.OleDB;
            }
            else if( m_Conn is OdbcConnection )
            {
                m_DatabaseType = DBTypes.ODBC;
            }
        }

        #endregion // Constructor

        #region Connection Open

        
        public IDbConnection Open( string _Connection )
        {
            m_ConnectionString = _Connection;
            return Open( );
        }

        
        protected IDbConnection Open( )
        {
            if( null == m_Conn )
            {
                m_Conn = GetConnetion( );
                if( "" == m_ConnectionString )
                {
                    throw ( new Exception( "No Connection String" ) );
                }
                m_Conn.ConnectionString = m_ConnectionString;
            }

            if( m_Conn.State != System.Data.ConnectionState.Open )
            {
                try
                {
                    m_Conn.Open( );
                }
                catch( Exception err )
                {
                    m_Error = err.ToString( );
                    throw;
                    //MessageBox.Show(err.Message);
                }
            }
            return m_Conn;
        }

        
        public IDbTransaction BeginTransaction( )
        {
            try
            {
                this.Open( );
                m_Transaction = m_Conn.BeginTransaction( );
            }
            catch
            {
                throw;
            }
            return m_Transaction;
        }

        
        public void CommitTransaction( )
        {
            try
            {
                if( null != m_Transaction )
                {
                    m_Transaction.Commit( );
                    m_Transaction.Dispose( );
                    m_Transaction = null;
                }
                if( null == m_Transaction )
                {
                    m_Conn.Close( );
                }
            }
            catch
            {
                throw;
            }
        }

        
        public void RollbackTransaction( )
        {
            try
            {
                if( null != m_Transaction )
                {
                    m_Transaction.Rollback( );
                    m_Transaction.Dispose( );
                    m_Transaction = null;
                }
                if( null == m_Transaction )
                {
                    m_Conn.Close( );
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Close and Dispose

        /// <summary>
        /// Close the connection.
        /// </summary>
        
        public void Close( )
        {
            if( m_Conn != null )
            {
                m_Conn.Close( );
            }
        }

        /// <summary>
        /// Release resources.
        /// </summary>
        
        public void Dispose( )
        {
            // make sure connection is closed
            if( m_Conn != null )
            {
                m_Conn.Close( );
                m_Conn.Dispose( );
                m_Conn = null;
            }
        }

        #endregion // Close and Dispose

        #region Exectute Methods

        #region Execute Query

        
        public int ExecuteQuery( string _SQLString )
        {
            DataSet dsDataSet = new DataSet( );
            //			SqlDataAdapter m_daDataAdapter = new SqlDataAdapter();
            return ExecuteQuery( ref dsDataSet, "", _SQLString, false );
        }

        
        public int ExecuteQuery( string _SQLString, bool _Clear )
        {
            DataSet dsDataSet = new DataSet( );
            //			SqlDataAdapter m_daDataAdapter = new SqlDataAdapter();
            return ExecuteQuery( ref dsDataSet, "", _SQLString, _Clear );
        }

        
        /// <summary>
        /// Returns a DataSet from the query passed in.
        /// </summary>
        
        public int ExecuteQuery( ref DataTable _Data, string _Name, string _SQLString )
        {
            int rc = 0;
            IDbCommand Cmd = GetCommand( _SQLString );
            _Data = (DataTable)Fill( _Data, ref Cmd, _Name, false );
            return rc;
        }

        
        /// <summary>
        /// Returns a DataSet from the query passed in.
        /// </summary>
        
        public int ExecuteQuery( ref DataTable _Data, string _Name, string _SQLString, bool _Clear )
        {
            int rc = 0;
            IDbCommand Cmd = GetCommand( _SQLString );
            _Data = (DataTable)Fill( _Data, ref Cmd, _Name, _Clear );
            return rc;
        }

        
        /// <summary>
        /// Returns a DataSet from the query passed in.
        /// </summary>
        
        public int ExecuteQuery( ref DataSet _Data, string _Name, string _SQLString )
        {
            int rc = 0;
            try
            {
                IDbCommand Cmd = GetCommand( _SQLString );
                _Data = (DataSet)Fill( _Data, ref Cmd, _Name, false );
            }
            catch
            {
                throw;
            }
            finally
            {
                if( null == m_Transaction )
                {
                    m_Conn.Close( );
                }
            }
            return rc;
        }

        
        public int ExecuteQuery( ref DataSet _Data, string _Name, string _SQLString, bool _Clear )
        {
            return ExecuteQuery( ref _Data, _Name, _SQLString, null, _Clear );
        }

        
        /// <summary>
        /// Returns a DataSet from the query passed in.
        /// </summary>
        
        public int ExecuteQuery( ref DataSet _Data, string _Name, string _SQLString, SqlParameterList _Params, bool _Clear )
        {
            int rc = 0;
            try
            {
                IDbCommand Cmd = GetCommand( _SQLString );

                if( null != _Params )
                {
                    for( int i = 0; i < _Params.Parameters.Length; ++i )
                    {
                        Cmd.Parameters.Add( _Params.Parameters[i] );
                    }
                }
                _Data = (DataSet)Fill( _Data, ref Cmd, _Name, _Clear );
            }
            catch
            {
                throw;
            }
            finally
            {
                if( null == m_Transaction )
                {
                    m_Conn.Close( );
                }
            }
            return rc;
        }
        #endregion // Execute Query

        #region Execute Reader

        
        public IDataReader ExecuteReader( string _SQLString )
        {
            IDataReader rc = null;
            IDbCommand cDataCommand = null;
            try
            {
                Open( );
                cDataCommand = GetCommand( _SQLString );

                rc = cDataCommand.ExecuteReader( );
            }
            catch
            {
                throw;
            }
            finally
            {
            }
            return rc;
        }        
        
        
        public IDataReader ExecuteReader( string _SQLString, SqlParameterList _Params )
        {
            IDataReader rc = null;
            IDbCommand cDataCommand = null;
            try
            {
                Open( );
                cDataCommand = GetCommand( _SQLString );

                if( null != _Params )
                {
                    for( int i = 0; i < _Params.Parameters.Length; ++i )
                    {
                        cDataCommand.Parameters.Add( _Params.Parameters[i] );
                    }
                }

                rc = cDataCommand.ExecuteReader( );
            }
            catch
            {
                throw;
            }
            finally
            {
            }
            return rc;
        }


        
        public DataTable GetSchema( string _Query )
        {
            DataTable rc = null;
            IDataReader Reader = null;
            IDbCommand cDataCommand = null;
            try
            {
                Open( );
                cDataCommand = GetCommand( _Query );

                Reader = cDataCommand.ExecuteReader( CommandBehavior.KeyInfo );
                rc = Reader.GetSchemaTable( );
            }
            catch
            {
                throw;
            }
            finally
            {
                if( null != Reader )
                {
                    Reader.Close( );
                }
                if( null != cDataCommand )
                {
                    cDataCommand.Dispose( );
                    cDataCommand = null;
                }
                if( null == m_Transaction )
                {
                    m_Conn.Close( );
                }
            }
            return rc;
        }

        
        public DataTable GetTableSchema( string _TableName )
        {
            DataTable rc = null;
            IDataReader Reader = null;
            IDbCommand cDataCommand = null;
            try
            {
                string SQL = "SELECT TOP 0 * FROM " + _TableName;
                Open( );
                cDataCommand = GetCommand( SQL );

                Reader = cDataCommand.ExecuteReader( CommandBehavior.KeyInfo );
                rc = Reader.GetSchemaTable( );
            }
            catch
            {
                throw;
            }
            finally
            {
                if( null != Reader )
                {
                    Reader.Close( );
                }
                if( null != cDataCommand )
                {
                    cDataCommand.Dispose( );
                    cDataCommand = null;
                }
                if( null == m_Transaction )
                {
                    m_Conn.Close( );
                }
            }
            return rc;
        }

        #endregion // Execute Reader

        #region Execute Scalar

        
        public bool ExecuteScalar<T>( string _SQLString, ref T _Data )
        {
            bool rc = false;
            try
            {
                rc = ExecuteScalar( _SQLString, null, ref _Data );
            }
            catch( Exception err )
            {
                throw err;
            }
            return rc;
        }


        public bool ExecuteScalar<T>( string _SQLString, SqlParameterList _Params, ref T _Data )
        {
            bool rc = false;
            IDbCommand cDataCommand = null;
            try
            {
                Open( );

                cDataCommand = GetCommand( _SQLString );

                if( null != _Params )
                {
                    for( int i = 0; i < _Params.Parameters.Length; ++i )
                    {
                        cDataCommand.Parameters.Add( _Params.Parameters[i] );
                    }
                }

                object O = cDataCommand.ExecuteScalar( );
                if( null != O )
                {
                    _Data = (T)cDataCommand.ExecuteScalar( );
                    rc = true;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                cDataCommand.Dispose( );
                cDataCommand = null;
                if( null == m_Transaction )
                {
                    m_Conn.Close( );
                }
            }
            return rc;
        }


        #endregion // Execute Scalar

        #region Execute

        
        /// <summary>
        /// Returns a DataSet from the query passed in.
        /// </summary>
        
        public bool Execute( string _SQLString )
        {
            bool rc = false;
            try
            {
                rc = Execute( _SQLString, null );
            }
            catch
            {
                throw;
            }
            return rc;
        }

        
        public bool Execute( string _SQLString, SqlParameterList _Params )
        {
            bool rc = false;
            IDbCommand cmdCommand = null;
            try
            {
                Open( );

                cmdCommand = GetCommand( _SQLString );

                if( null != _Params )
                {
                    for( int i = 0; i < _Params.Parameters.Length; ++i )
                    {
                        cmdCommand.Parameters.Add( _Params.Parameters[i] );
                    }
                }
                int Lines = cmdCommand.ExecuteNonQuery( );
                if( 0 < Lines )
                {
                    rc = true;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                cmdCommand.Dispose( );
                cmdCommand = null;
                if( null == m_Transaction )
                {
                    m_Conn.Close( );
                }
            }
            return rc;
        }

        #endregion // Execute

        #region Execute Proc

        
        public int ExecuteProc( string _ProcName, SqlParameterList _Params, ref DataTable _Table )
        {
            return ExecuteProc( _ProcName, _Params, ref _Table, null );
        }
        
        public int ExecuteProc( string _ProcName, SqlParameterList _Params, ref DataTable _Table, string _TableName )
        {
            object tmp = _Table;
            return ExecuteProc( _ProcName, _Params, ref tmp, _TableName );
        }
        
        public int ExecuteProc( string _ProcName, SqlParameterList _Params )
        {
            //			SqlDataAdapter daDataAdapter = new SqlDataAdapter();
            DataSet tmp = null;
            return ExecuteProc( _ProcName, _Params, ref tmp );
        }
        
        public int ExecuteProc( string _ProcName, SqlParameterList _Params, ref DataSet _Set )
        {
            return ExecuteProc( _ProcName, _Params, ref _Set, null );
        }

        public int ExecuteProc( string _ProcName, SqlParameterList _Params, ref DataSet _Set, string _TableName )
        {
            object tmp = _Set;
            return ExecuteProc( _ProcName, _Params, ref tmp, _TableName );
        }

        public int ExecuteProc( string _ProcName, SqlParameterList _Params, ref object _Data, string _TableName )
        {
            int rc = -1;
            IDbCommand cmdCommand = GetCommand( );
            try
            {
                Open( );

                if( null != ProcPrefix && 0 < ProcPrefix.Length && false == _ProcName.StartsWith( ProcPrefix ) )
                {
                    _ProcName = ProcPrefix + "." + _ProcName;
                }

                if( null != _Params )
                {
                    for( int i = 0; i < _Params.Parameters.Length; ++i )
                    {
                        cmdCommand.Parameters.Add( _Params.Parameters[i] );
                    }
                    cmdCommand.Parameters.Add( _Params.CreateReturn( ) );
                }
                else
                {
                    _Params = new SqlParameterList( this.m_DatabaseType );
                    cmdCommand.Parameters.Add( _Params.CreateReturn( ) );

                }

                //Configure Command Object
                cmdCommand.Connection = m_Conn;
                cmdCommand.CommandType = System.Data.CommandType.StoredProcedure;
                cmdCommand.CommandText = _ProcName;
                if( null != _Data )
                {
                    _Data = Fill( _Data, ref cmdCommand, _TableName, false );
                }
                else
                {
                    cmdCommand.ExecuteNonQuery( );
                }
                rc = Convert.ToInt32( ( (SqlParameter)cmdCommand.Parameters["ReturnValue"] ).Value );
            }
            catch
            {
                throw;
            }
            finally
            {
                cmdCommand.Dispose( );
                cmdCommand = null;
                if( null == m_Transaction )
                {
                    m_Conn.Close( );
                }
            }
            return rc;
        }

        #endregion Execute Proc

        #endregion // Exectute Methods

        public DataTable GetOleDbSchemaTable( )
        {
            DataTable rc = new DataTable( );
            OleDbConnection o = this.m_Conn as OleDbConnection;
            if( null != o )
            {
                rc = o.GetOleDbSchemaTable( OleDbSchemaGuid.Tables, null );
            }
            return rc;
        }

        #endregion // Public Methods

        #region Protected Methods

        
        protected object Fill( object _Data, ref IDbCommand _Cmd, string _Name, bool _Clear )
        {
            try
            {
#if ENABLE_TIMER
                System.Diagnostics.Stopwatch Timer = new System.Diagnostics.Stopwatch( );
                Timer.Start( );
#endif //ENABLE_TIMER

                IDbDataAdapter dataAdapter = GetDataAdapter( );
                dataAdapter.SelectCommand = _Cmd;

                if( dataAdapter != null )
                {
                    if( typeof( DataSet ) == _Data.GetType( ) )
                    {
                        DataSet Data = (DataSet)_Data;
                        if( _Name != null && _Name.Length > 0 )
                        {
                            DataTable tmp = Data.Tables[_Name];
                            if( null != tmp && true == _Clear )
                            {
                                tmp.Clear( );
                            }
                            if( true == m_Schema )
                            {
                                ( (DbDataAdapter)dataAdapter ).FillSchema( Data, SchemaType.Source, _Name );
                                ( (DbDataAdapter)dataAdapter ).Fill( Data, _Name );
                            }
                            else
                            {
                                ( (DbDataAdapter)dataAdapter ).Fill( Data, _Name );
                            }
                        }
                        else
                        {
                            if( true == m_Schema )
                            {
                                ( (DbDataAdapter)dataAdapter ).FillSchema( Data, SchemaType.Source );
                                ( (DbDataAdapter)dataAdapter ).Fill( Data );
                            }
                            else
                            {
                                ( (DbDataAdapter)dataAdapter ).Fill( Data );
                            }
                        }
                    }
                    else if( typeof( DataTable ) == _Data.GetType( ) )
                    {
                        DataTable Table = (DataTable)_Data;
                        if( null != Table && true == _Clear )
                        {
                            Table.Clear( );
                        }
                        if( true == m_Schema )
                        {
                            ( (DbDataAdapter)dataAdapter ).FillSchema( Table, SchemaType.Source );
                            ( (DbDataAdapter)dataAdapter ).Fill( Table );
                        }
                        else
                        {
                            ( (DbDataAdapter)dataAdapter ).Fill( Table );
                        }
                        if( _Name != null && _Name.Length > 0 )
                        {
                            Table.TableName = _Name;
                        }
                    }
                }
#if ENABLE_TIMER
                Timer.Stop( );
                System.Diagnostics.Debug.WriteLine( "Fill: " + Timer.ElapsedMilliseconds );
#endif //ENABLE_TIMER

            }
            catch( Exception err )
            {
                this.m_Error = err.Message;
                throw;
            }
            return _Data;
        }

        
        protected IDbConnection GetConnetion( )
        {
            IDbConnection Conn = null;
            switch( m_DatabaseType )
            {
                case DBTypes.SqlServer:
                {
                    Conn = new SqlConnection( );
                    if( true == m_LogMessages )
                    {
                        ( (SqlConnection)Conn ).InfoMessage += new SqlInfoMessageEventHandler( SqlTool_InfoMessage );
                    }
                    break;
                }
                case DBTypes.OleDB:
                {
                    Conn = new OleDbConnection( );
                    if( true == m_LogMessages )
                    {
                        ( (OleDbConnection)Conn ).InfoMessage += new OleDbInfoMessageEventHandler( SqlTool_InfoMessage );
                    }
                    break;
                }
                case DBTypes.ODBC:
                {
                    Conn = new OdbcConnection( );
                    if( true == m_LogMessages )
                    {
                        ( (OdbcConnection)Conn ).InfoMessage += new OdbcInfoMessageEventHandler( SqlTool_InfoMessage );
                    }
                    break;
                }
            }
            return Conn;
        }


        
        protected IDbCommand GetCommand( string _SQLString )
        {
            IDbCommand Cmd = GetCommand( );
            Cmd.Transaction = m_Transaction;
            Cmd.CommandText = _SQLString;
            return Cmd;
        }

        
        protected IDbCommand GetCommand( )
        {
            IDbCommand Cmd = null;
            try
            {
                switch( m_DatabaseType )
                {
                    case DBTypes.SqlServer:
                    {
                        Cmd = new SqlCommand( );
                        break;
                    }
                    case DBTypes.OleDB:
                    {
                        Cmd = new OleDbCommand( );
                        break;
                    }
                    case DBTypes.ODBC:
                    {
                        Cmd = new OdbcCommand( );
                        break;
                    }
                }
                Cmd.Transaction = m_Transaction;
                Cmd.CommandTimeout = m_Timeout;

                if( null == m_Conn ||
                    ConnectionState.Closed == m_Conn.State ||
                    ConnectionState.Broken == m_Conn.State )
                {
                    m_Conn.Open( );
                }
                Cmd.Connection = (IDbConnection)m_Conn;
            }
            catch
            {
                throw;
            }
            return Cmd;
        }

        
        protected IDbDataAdapter GetDataAdapter( string _SQLString )
        {
            return GetDataAdapter( GetCommand( _SQLString ) );
        }

        
        protected IDbDataAdapter GetDataAdapter( IDbCommand _Command )
        {
            IDbDataAdapter Adapter = GetDataAdapter( );
            _Command.Connection = m_Conn;

            Adapter.SelectCommand = _Command;
            return Adapter;
        }

        
        protected IDbDataAdapter GetDataAdapter( )
        {
            IDbDataAdapter Adapter = null;
            try
            {
                switch( m_DatabaseType )
                {
                    case DBTypes.SqlServer:
                    {
                        Adapter = new SqlDataAdapter( );
                        break;
                    }
                    case DBTypes.OleDB:
                    {
                        Adapter = new OleDbDataAdapter( );
                        break;
                    }
                    case DBTypes.ODBC:
                    {
                        Adapter = new OdbcDataAdapter( );
                        break;
                    }
                }
            }
            catch
            {
                throw;
            }
            return Adapter;
        }


        #endregion // Protected Methods

        
        #region Static SqlTool Wrapper

        
        /// <summary>
        /// Static execute procedure
        /// </summary>
        /// <param name="_ProcName"></param>
        /// <param name="_Params"></param>
        /// <returns></returns>
        
        static public bool ExecuteProcedure( string _ConnStr, string _ProcName, SqlParameterList _Params )
        {
            bool rc = false;
            SqlTool DBConn = null;
            try
            {
                DBConn = new SqlTool( _ConnStr );

                DBConn.ExecuteProc( _ProcName, _Params );

                rc = true;
            }
            catch
            {
                throw;
            }
            finally
            {
                DBConn.Close( );
                DBConn.Dispose( );
                DBConn = null;
            }
            return rc;
        }

        
        /// <summary>
        /// Static execute procedure
        /// </summary>
        /// <param name="_ProcName"></param>
        /// <param name="_Params"></param>
        /// <param name="_Set"></param>
        /// <returns></returns>
        
        static public bool ExecuteProcedure( string _ConnStr, string _ProcName, SqlParameterList _Params, ref DataSet _Set )
        {
            bool rc = false;
            SqlTool DBConn = null;
            try
            {
                DBConn = new SqlTool( _ConnStr );

                DBConn.ExecuteProc( _ProcName, _Params, ref _Set );

                rc = true;
            }
            catch
            {
                throw;
            }
            finally
            {
                DBConn.Close( );
                DBConn.Dispose( );
                DBConn = null;
            }
            return rc;
        }

        
        static public void CopyRow( DataRow _Source, DataRow _Dest )
        {
            try
            {
                DataTable Table = _Dest.Table;
                for( int i = 0; i < Table.Columns.Count; ++i )
                {
                    string ColName = Table.Columns[i].ColumnName;
                    if( true == _Source.Table.Columns.Contains( ColName ) )
                    {
                        _Dest[ColName] = _Source[ColName];
                    }
                    else if( true == _Source.Table.Columns.Contains( ColName.Remove( 0, 1 ) ) )
                    {
                        string NewName = ColName.Remove( 0, 1 );
                        _Dest[ColName] = _Source[NewName];
                    }
                    else if( true == _Source.Table.Columns.Contains( "_" + ColName ) )
                    {
                        string NewName = "_" + ColName;
                        _Dest[ColName] = _Source[NewName];
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        static public object GetRowElement( DataRow _row, string _columnName )
        {
            return _row[ _columnName ].NullIfDBNull( );
        }

        static public T GetField<T>( DataRow _row, string _columnName )
        {
            object o = _row[ _columnName ].NullIfDBNull( );
            if( null != o )
            {
                return ( T ) o;
            }
            else
            {
                return default( T );
            }
        }

        #endregion // SqlTool Wrapper


        private void SqlTool_InfoMessage( object sender, SqlInfoMessageEventArgs e )
        {
            m_Messages.Append( e.ToString( ) );
        }


        private void SqlTool_InfoMessage( object sender, OdbcInfoMessageEventArgs e )
        {
            m_Messages.Append( e.ToString( ) );
        }

        
        private void SqlTool_InfoMessage( object sender, OleDbInfoMessageEventArgs e )
        {
            m_Messages.Append( e.ToString( ) );
        }
    }

}
