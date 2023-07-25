using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlServerCe;
using System.Data;

namespace PassivePickup
{
    public class DataAccess
    {
        private string ConnectString( )
        {
            string fileName = "CustomerInfo.sdf";
            string password = "Waste";

            return string.Format( "DataSource=\"{0}\"; Password='{1}'", fileName, password );

        }

        public Customer ValidateTag( string _TagID )
        {
            Customer rc = null;
            SqlCeConnection cn = new SqlCeConnection( ConnectString( ) );
            if( cn.State == ConnectionState.Closed )
            {
                cn.Open( );
            }

            SqlCeCommand cmd;
            string sql = "SELECT * FROM Customer WHERE TagID = @TagID";

            try
            {
                cmd = new SqlCeCommand( sql, cn );
                cmd.Parameters.AddWithValue( "@TagID", _TagID );
                SqlCeDataReader Reader = cmd.ExecuteReader( );
                if( Reader.Read( ) )
                {
                    rc = new Customer( Reader );
                }
            }
            finally
            {
                cn.Close( );
            }
            return rc;
        }

        public void InvalidAddress( string _TagID )
        {
            SqlCeConnection cn = new SqlCeConnection( ConnectString( ) );
            if( cn.State == ConnectionState.Closed )
            {
                cn.Open( );
            }

            SqlCeCommand cmd;
            string sql = "UPDATE Customer SET InvalidAddress = 1 WHERE TagID = @TagID";

            try
            {
                cmd = new SqlCeCommand( sql, cn );
                cmd.Parameters.AddWithValue( "@TagID", _TagID );
                cmd.ExecuteNonQuery( );
            }
            finally
            {
                cn.Close( );
            }
        }
    }
}
