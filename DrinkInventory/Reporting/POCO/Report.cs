using System.Data;
using System.Linq;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;
using Jaxis.Utilities.Database;
using System.Text.RegularExpressions;

namespace Jaxis.DrinkInventory.Reporting.Data.POCO
{
    public partial class Report
    {
        public DataTable GetData(object[] _parameterValues)
        {
            var rc = new DataTable( );
            var dataset = new DataSet( );
            
            string connection = System.Configuration.ConfigurationManager.ConnectionStrings[ "DrinkReporting" ].ConnectionString;
            // Extract the parameter names
            var pNames = Regex.Matches( SelectCommand, @"@\w+\b" ).OfType<Match>( ).Select( _m => _m.Value ).ToList( );
            if ( pNames.Count == _parameterValues.Length )
            {
                using ( var sql = new SqlTool( connection ) )
                {
                    var parameters = new SqlParameterList( );
                    for ( int i = 0; i < _parameterValues.Length; i++ )
                    {
                        parameters.AddInParameter( pNames[ i ], _parameterValues[ i ] );
                    }
                    sql.ExecuteQuery( ref dataset, string.Empty, SelectCommand, parameters, true );
                    if ( dataset.Tables.Count > 0 )
                    {
                        rc = dataset.Tables[ 0 ];
                    }
                }
            }

            return rc;
        }
    }
}
