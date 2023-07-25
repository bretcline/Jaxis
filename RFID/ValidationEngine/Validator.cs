using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.Utilities.Database;
using System.Data;
using System.Data.SqlServerCe;

namespace ValidationEngine
{
    public class Validator : IValidator
    {
        public IValidationResults Validate( IValidationKey _Key )
        {
            ValidationResults rc = new ValidationResults( );
            if( null != _Key )
            {
                // Open the same connection with the same connection string.
                string ConnString = ValidationEngine.Properties.Settings.Default.DBConn;
                using( SqlCeConnection con = new SqlCeConnection( ConnString ) )
                {
                    con.Open( );
                    // Read in all values in the table.
                    using( SqlCeCommand com = new SqlCeCommand( string.Format( "SELECT * FROM CustomerInfo WHERE TagID = '{0}'", _Key.Key ), con ) )
                    {
                        SqlCeDataReader Reader = com.ExecuteReader( );
                        if( Reader.Read( ) )
                        {
                            Guid CustomerID = Reader.GetGuid( Reader.GetOrdinal( "CustomerID" ) );
                            rc.IsValid = true;
                            Entity E = new Entity( );
                            rc.IsCurrent = Convert.ToBoolean( Reader.GetInt32( Reader.GetOrdinal( "Active" ) ) );
                            E.Name = Reader.GetString( Reader.GetOrdinal( "Name" ) );
                            E.Address = Reader.GetString( Reader.GetOrdinal( "Address" ) );
                            E.City = Reader.GetString( Reader.GetOrdinal( "City" ) );
                            E.State = Reader.GetString( Reader.GetOrdinal( "State" ) );
                            E.Zip = Reader.GetString( Reader.GetOrdinal( "Zip" ) );
                            rc.Entity = E;

                            com.CommandText = string.Format( "INSERT INTO PickupData SELECT NEWID( ), '{0}', GETDATE( )", CustomerID );
                            int Rows = com.ExecuteNonQuery( );
                            if( 0 == Rows )
                            {
                                rc.Results = "Update Failed";
                            }
                        }
                        else
                        {
                            rc.Results = string.Format( "Unknown Account for {0}", _Key.Key );
                            rc.IsValid = false;
                        }
                        _Key.ProcessValidation( rc );
                    }
                }
            }
            return rc;
        }

    }
}
