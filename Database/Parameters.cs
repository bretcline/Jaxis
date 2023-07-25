
using System;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;


namespace Jaxis.Utilities.Database
{
    /// <summary>
    /// Summary description for SqlParameterList.
    /// </summary>
    public class SqlParameterList
    {
        protected DBTypes m_DBType = DBTypes.SqlServer;
        protected Dictionary<string, IDbDataParameter> m_Params = new Dictionary<string, IDbDataParameter>( );
        protected List<IDbDataParameter> m_OrderedParams = new List<IDbDataParameter>( );

        
        public SqlParameterList( DBTypes _DBType )
        {
            //
            // TODO: Add constructor logic here
            //
            m_DBType = _DBType;
        }

        
        public SqlParameterList( )
        {
            //
            // TODO: Add constructor logic here
            //
        }

        
        public System.Data.IDbDataParameter[] Parameters
        {
            get
            {
                System.Data.IDbDataParameter[] rc = null;
                try
                {
                    if( 0 < m_OrderedParams.Count )
                    {
                        rc = m_OrderedParams.ToArray( );
//                        rc = (System.Data.IDbDataParameter[])new ArrayList( m_Params.Values ).ToArray( typeof( System.Data.IDbDataParameter ) );
                    }
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
        }

        
        public void Clear( )
        {
            try
            {
                m_Params.Clear( );
                m_OrderedParams.Clear( );
            }
            catch
            {
                throw;
            }
        }


        
        /// <summary>
        /// Make input param.
        /// </summary>
        /// <param name="ParamName">Name of param.</param>
        /// <param name="DbType">Param type.</param>
        /// <param name="Size">Param size.</param>
        /// <param name="Value">Param value.</param>
        /// <returns>New parameter.</returns>
        
        public IDbDataParameter AddInParameter( string _ParamName, int _DbType, int _Size, object _Value )
        {
            return AddParameter( _ParamName, _DbType, _Size, ParameterDirection.Input, _Value );
        }


        
        /// <summary>
        /// Make input param.
        /// </summary>
        /// <param name="ParamName">Name of param.</param>
        /// <param name="DbType">Param type.</param>
        /// <param name="Size">Param size.</param>
        /// <param name="Value">Param value.</param>
        /// <returns>New parameter.</returns>
        
        public IDbDataParameter AddInParameter( string _ParamName, object _Value )
        {
            return AddParameter( _ParamName, ParameterDirection.Input, _Value );
        }

        
        /// <summary>
        /// Make input param.
        /// </summary>
        /// <param name="ParamName">Name of param.</param>
        /// <param name="DbType">Param type.</param>
        /// <param name="Size">Param size.</param>
        /// <returns>New parameter.</returns>
        
        public IDbDataParameter AddOutParameter( string _ParamName, int _DbType, int _Size )
        {
            return AddParameter( _ParamName, _DbType, _Size, ParameterDirection.Output, null );
        }


        
        /// <summary>
        /// Make input param.
        /// </summary>
        /// <param name="ParamName">Name of param.</param>
        /// <param name="DbType">Param type.</param>
        /// <param name="Size">Param size.</param>
        /// <returns>New parameter.</returns>
        
        public IDbDataParameter AddOutParameter( string _ParamName )
        {
            return AddParameter( _ParamName, ParameterDirection.Output, null );
        }



        
        /// <summary>
        /// Make stored procedure param.
        /// </summary>
        /// <param name="ParamName">Name of param.</param>
        /// <param name="DbType">Param type.</param>
        /// <param name="Size">Param size.</param>
        /// <param name="Direction">Parm direction.</param>
        /// <param name="Value">Param value.</param>
        /// <returns>New parameter.</returns>
        
        public IDbDataParameter AddParameter( string _ParamName, ParameterDirection _Direction, object _Value )
        {
            IDbDataParameter rc = null;

            try
            {
                rc = Create( _ParamName, _Value );

                rc.Direction = _Direction;
                if( !( _Direction == ParameterDirection.Output && _Value == null ) )
                {
                    rc.Value = _Value;
                }
                m_Params.Add( _ParamName, rc );
                m_OrderedParams.Add( rc );
            }
            catch
            {
                throw;
            }
            return rc;
        }


        
        /// <summary>
        /// Make stored procedure param.
        /// </summary>
        /// <param name="ParamName">Name of param.</param>
        /// <param name="DbType">Param type.</param>
        /// <param name="Size">Param size.</param>
        /// <param name="Direction">Parm direction.</param>
        /// <param name="Value">Param value.</param>
        /// <returns>New parameter.</returns>
        
        public IDbDataParameter AddParameter( string _ParamName, int _DbType, Int32 _Size, ParameterDirection _Direction, object _Value )
        {
            IDbDataParameter rc = null;

            try
            {
                if( _Size > 0 )
                {
                    rc = Create( _ParamName, _DbType, _Size );
                }
                else
                {
                    rc = Create( _ParamName, _DbType );
                }

                rc.Direction = _Direction;
                if( !( _Direction == ParameterDirection.Output && _Value == null ) )
                {
                    rc.Value = _Value;
                }
                m_Params.Add( _ParamName, rc );
                m_OrderedParams.Add( rc );
            }
            catch
            {
                throw;
            }
            return rc;
        }


        
        public IDbCommand AddParameters( DataTable _Data, IDbCommand _Command )
        {
            try
            {

                IDbDataParameter tmp = null;
                foreach( DataColumn Column in _Data.Columns )
                {
                    int DataType = GetColumnType( Column );

                    // For all the others convert it into a SqlParameter (by Appending an @ in front of the Var Name) with the same name as the Column Name 
                    if( 0 < Column.MaxLength )
                    {
                        tmp = Create( "@" + Column.ColumnName, DataType, Column.MaxLength );
                    }
                    else if( DBTypes.SqlServer == m_DBType && (SqlDbType)DataType == SqlDbType.VarChar )
                    {
                        tmp = Create( "@" + Column.ColumnName, DataType, 4000 );
                    }
                    else if( DBTypes.OleDB == m_DBType && (OleDbType)DataType == OleDbType.VarChar )
                    {
                        tmp = Create( "@" + Column.ColumnName, DataType, 4000 );
                    }
                    else if( DBTypes.ODBC == m_DBType && (OdbcType)DataType == OdbcType.VarChar )
                    {
                        tmp = Create( "@" + Column.ColumnName, DataType, 4000 );
                    }
                    else
                    {
                        tmp = Create( "@" + Column.ColumnName, DataType );
                    }
                    tmp.SourceColumn = Column.ColumnName;
                    if( DBTypes.SqlServer == m_DBType && SqlDbType.Text != (SqlDbType)DataType )
                    {
                        tmp.Direction = ParameterDirection.InputOutput;
                    }
                    else if( DBTypes.OleDB == m_DBType && OleDbType.LongVarChar != (OleDbType)DataType )
                    {
                        tmp.Direction = ParameterDirection.InputOutput;
                    }
                    else if( DBTypes.ODBC == m_DBType && OdbcType.Text != (OdbcType)DataType )
                    {
                        tmp.Direction = ParameterDirection.InputOutput;
                    }
                    else
                    {
                        tmp.Direction = ParameterDirection.Input;
                    }
                    _Command.Parameters.Add( tmp );
                }
            }
            catch
            {
                throw;
            }
            return _Command;
        }


        
        public void AddParameters( DataRow _Data )
        {
            try
            {
                IDbDataParameter tmp = null;
                foreach( DataColumn Column in _Data.Table.Columns )
                {
                    SqlDbType DataType;
                    GetColumnType( Column.DataType, Column.MaxLength, out DataType );
                    // For all the others convert it into a SqlParameter (by Appending an @ in front of the Var Name) with the same name as the Column Name 
                    if( 0 < Column.MaxLength )
                    {
                        tmp = Create( "@" + Column.ColumnName, (int)DataType, Column.MaxLength );
                    }
                    else
                    {
                        tmp = Create( "@" + Column.ColumnName, DataType );
                    }
                    tmp.SourceColumn = Column.ColumnName;
                    if( SqlDbType.Text != DataType )
                    {
                        tmp.Direction = ParameterDirection.InputOutput;
                    }
                    else
                    {
                        tmp.Direction = ParameterDirection.Input;
                    }
                    tmp.Value = _Data[Column.ColumnName];
                    m_Params.Add( tmp.ParameterName, tmp );
                    m_OrderedParams.Add( tmp );
                }
            }
            catch
            {
                throw;
            }
        }


        
        public void GetParameters( DataRow _Data )
        {
            try
            {
                foreach( IDbDataParameter Column in Parameters )
                {
                    _Data[Column.ParameterName.Substring( 1 )] = Column.Value;
                }
            }
            catch
            {
                throw;
            }
        }


        
        public IDbDataParameter GetParameter( string _ParamName )
        {
            IDbDataParameter rc = null;
            try
            {
                rc = (IDbDataParameter)m_Params[_ParamName];
            }
            catch
            {
                throw;
            }
            return rc;
        }


        #region Utility Functions

        
        protected void GetColumnType( System.Type _Type, int _MaxLength, out SqlDbType _DBType )
        {
            _DBType = ( 7999 < _MaxLength ) ? SqlDbType.Text : SqlDbType.VarChar;
            try
            {
                if( typeof( int ) == _Type ||
                    typeof( System.Int16 ) == _Type ||
                    typeof( System.Int32 ) == _Type ||
                    typeof( System.UInt16 ) == _Type )
                {
                    _DBType = SqlDbType.Int;
                }
                else if( typeof( System.Int64 ) == _Type ||
                    typeof( System.UInt32 ) == _Type ||
                    typeof( System.UInt64 ) == _Type )
                {
                    _DBType = SqlDbType.BigInt;
                }
                else if( typeof( double ) == _Type ||
                    typeof( float ) == _Type ||
                    typeof( System.Double ) == _Type ||
                    typeof( System.Single ) == _Type )
                {
                    _DBType = SqlDbType.Float;
                }
                else if( typeof( System.Decimal ) == _Type )
                {
                    _DBType = SqlDbType.Decimal;
                }
                else if( typeof( System.DateTime ) == _Type )
                {
                    _DBType = SqlDbType.DateTime;
                }
                else if( typeof( System.Guid ) == _Type )
                {
                    _DBType = SqlDbType.UniqueIdentifier;
                }
                else if( typeof( System.Boolean ) == _Type ||
                    typeof( bool ) == _Type )
                {
                    _DBType = SqlDbType.Bit;
                }
                else if( typeof( System.Byte[] ) == _Type )
                {
                    _DBType = SqlDbType.Timestamp;
                }
            }
            catch
            {
                throw;
            }
        }



        protected void GetColumnType( System.Type _Type, int _MaxLength, out OleDbType _DBType )
        {
            _DBType = ( 7999 < _MaxLength ) ? OleDbType.LongVarChar : OleDbType.VarChar;
            try
            {
                if( typeof( int ) == _Type ||
                    typeof( System.Int16 ) == _Type ||
                    typeof( System.Int32 ) == _Type ||
                    typeof( System.UInt16 ) == _Type )
                {
                    _DBType = OleDbType.Integer;
                }
                else if( typeof( System.Int64 ) == _Type ||
                    typeof( System.UInt32 ) == _Type ||
                    typeof( System.UInt64 ) == _Type )
                {
                    _DBType = OleDbType.BigInt;
                }
                else if( typeof( double ) == _Type ||
                    typeof( float ) == _Type ||
                    typeof( System.Double ) == _Type ||
                    typeof( System.Single ) == _Type )
                {
                    _DBType = OleDbType.Double;
                }
                else if( typeof( System.Decimal ) == _Type )
                {
                    _DBType = OleDbType.Decimal;
                }
                else if( typeof( System.DateTime ) == _Type )
                {
                    _DBType = OleDbType.Date;
                }
                else if( typeof( System.Guid ) == _Type )
                {
                    _DBType = OleDbType.Guid;
                }
                else if( typeof( System.Boolean ) == _Type ||
                    typeof( bool ) == _Type )
                {
                    _DBType = OleDbType.Binary;
                }
                else if( typeof( System.Byte[] ) == _Type )
                {
                    _DBType = OleDbType.DBTimeStamp;
                }
            }
            catch
            {
                throw;
            }
        }

        protected void GetColumnType( System.Type _Type, int _MaxLength, out OdbcType _DBType )
        {
            _DBType = ( 7999 < _MaxLength ) ? OdbcType.Text : OdbcType.VarChar;
            try
            {
                if( typeof( int ) == _Type ||
                    typeof( System.Int16 ) == _Type ||
                    typeof( System.Int32 ) == _Type ||
                    typeof( System.UInt16 ) == _Type )
                {
                    _DBType = OdbcType.Int;
                }
                else if( typeof( System.Int64 ) == _Type ||
                    typeof( System.UInt32 ) == _Type ||
                    typeof( System.UInt64 ) == _Type )
                {
                    _DBType = OdbcType.BigInt;
                }
                else if( typeof( double ) == _Type ||
                    typeof( float ) == _Type ||
                    typeof( System.Double ) == _Type ||
                    typeof( System.Single ) == _Type )
                {
                    _DBType = OdbcType.Double;
                }
                else if( typeof( System.Decimal ) == _Type )
                {
                    _DBType = OdbcType.Decimal;
                }
                else if( typeof( System.DateTime ) == _Type )
                {
                    _DBType = OdbcType.Date;
                }
                else if( typeof( System.Guid ) == _Type )
                {
                    _DBType = OdbcType.UniqueIdentifier;
                }
                else if( typeof( System.Boolean ) == _Type ||
                    typeof( bool ) == _Type )
                {
                    _DBType = OdbcType.Binary;
                }
                else if( typeof( System.Byte[] ) == _Type )
                {
                    _DBType = OdbcType.Timestamp;
                }
            }
            catch
            {
                throw;
            }
        }

        
        protected int GetColumnType( System.Type _Type )
        {
            int rc = 0;
            try
            {
                switch( m_DBType )
                {
                    case DBTypes.SqlServer:
                    {
                        SqlDbType Type;
                        GetColumnType( _Type, 0, out Type );
                        rc = (int)Type;

                        break;
                    }
                    case DBTypes.OleDB:
                    {
                        OleDbType Type;
                        GetColumnType( _Type, 0, out Type );
                        rc = (int)Type;
                        break;
                    }
                    case DBTypes.ODBC:
                    {
                        OdbcType Type;
                        GetColumnType( _Type, 0, out Type );
                        rc = (int)Type;
                        break;
                    }
                }

            }
            catch
            {
                throw;
            }
            return rc;
        }


        
        protected int GetColumnType( DataColumn _Column )
        {
            int rc = 0;
            try
            {
                switch( m_DBType )
                {
                    case DBTypes.SqlServer:
                    {
                        SqlDbType Type;
                        GetColumnType( _Column.DataType, _Column.MaxLength, out Type );
                        rc = (int)Type;

                        break;
                    }
                    case DBTypes.OleDB:
                    {
                        OleDbType Type;
                        GetColumnType( _Column.DataType, _Column.MaxLength, out Type );
                        rc = (int)Type;
                        break;
                    }
                    case DBTypes.ODBC:
                    {
                        OdbcType Type;
                        GetColumnType( _Column.DataType, _Column.MaxLength, out Type );
                        rc = (int)Type;
                        break;
                    }
                }

            }
            catch
            {
                throw;
            }
            return rc;
        }

        #endregion // Utility Functions

        #region Create Parameters

        
        protected IDbDataParameter Create( string _Name, object _Value )
        {
            IDbDataParameter Adapter = null;
            switch( m_DBType )
            {
                case DBTypes.SqlServer:
                {
                    Adapter = new SqlParameter( _Name, _Value );
                    break;
                }
                case DBTypes.OleDB:
                {
                    Adapter = new OleDbParameter( _Name, _Value );
                    break;
                }
                case DBTypes.ODBC:
                {
                    Adapter = new OdbcParameter( _Name, _Value );
                    break;
                }
            }
            return Adapter;
        }

        
        protected IDbDataParameter Create<DBType>( string _Name, DBType _Type )
        {
            IDbDataParameter Adapter = null;
            switch( m_DBType )
            {
                case DBTypes.SqlServer:
                {
                    Adapter = new SqlParameter( _Name, _Type );
                    break;

                }
                case DBTypes.OleDB:
                {
                    Adapter = new OleDbParameter( _Name, _Type );
                    break;
                }
                case DBTypes.ODBC:
                {
                    Adapter = new OdbcParameter( _Name, _Type );
                    break;
                }
            }
            return Adapter;
        }

        
        protected IDbDataParameter Create( string _Name, int _Type, int _Size )
        {
            IDbDataParameter Adapter = null;
            switch( m_DBType )
            {
                case DBTypes.SqlServer:
                {
                    Adapter = new SqlParameter( _Name, (SqlDbType)_Type, _Size );
                    break;

                }
                case DBTypes.OleDB:
                {
                    Adapter = new OleDbParameter( _Name, (OleDbType)_Type, _Size );
                    break;
                }
                case DBTypes.ODBC:
                {
                    Adapter = new OdbcParameter( _Name, (OdbcType)_Type, _Size );
                    break;
                }
            }
            return Adapter;
        }

        
        protected IDbDataParameter Create( string _Name, int _Type,
            int _Size, ParameterDirection _Direction,
            bool _IsNullable, byte _Precision,
            byte _Scale, string _SourceColumn,
            DataRowVersion _RowVersion, object _Value )
        {
            IDbDataParameter Adapter = null;
            switch( m_DBType )
            {
                case DBTypes.SqlServer:
                {
                    Adapter = new SqlParameter( _Name, (SqlDbType)_Type, _Size, _Direction, _IsNullable, _Precision, _Scale, _SourceColumn, _RowVersion, _Value );
                    break;

                }
                case DBTypes.OleDB:
                {
                    Adapter = new OleDbParameter( _Name, (OleDbType)_Type, _Size, _Direction, _IsNullable, _Precision, _Scale, _SourceColumn, _RowVersion, _Value );
                    break;
                }
                case DBTypes.ODBC:
                {
                    Adapter = new OdbcParameter( _Name, (OdbcType)_Type, _Size, _Direction, _IsNullable, _Precision, _Scale, _SourceColumn, _RowVersion, _Value );
                    break;
                }
            }

            return Adapter;
        }

        
        public IDbDataParameter CreateReturn( )
        {
            IDbDataParameter rc = null;
            switch( m_DBType )
            {
                case DBTypes.SqlServer:
                {
                    rc = Create( "ReturnValue", (int)SqlDbType.Int, 4,
                        ParameterDirection.ReturnValue, false, 0, 0,
                        string.Empty, DataRowVersion.Default, null );
                    break;

                }
                case DBTypes.OleDB:
                {
                    rc = Create( "ReturnValue", (int)OleDbType.Integer, 4,
                        ParameterDirection.ReturnValue, false, 0, 0,
                        string.Empty, DataRowVersion.Default, null );
                    break;
                }
                case DBTypes.ODBC:
                {
                    rc = Create( "ReturnValue", (int)OdbcType.Int, 4,
                        ParameterDirection.ReturnValue, false, 0, 0,
                        string.Empty, DataRowVersion.Default, null );
                    break;
                }
            }
            return rc;
        }

        #endregion // Create Parameters
    }

}
