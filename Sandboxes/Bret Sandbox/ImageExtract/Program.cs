using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;

namespace ImageExtract
{
    class Program
    {
        static void Main( string[] args )
        {
            //Application A = new ApplicationClass( );
            //Workbook W = A.Workbooks[0];
            //            W.Open( @"C:\Source\BevMetrics\trunk\Documents\UPC codes W-Pics 9-3\Bev Met UPC List-E-M.edit.xls" );

            string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=""C:\Source\BevMetrics\trunk\Documents\UPC codes W-Pics 9-3\Bev Met UPC List-E-M.edit.xls"";Extended Properties= ""Excel 8.0;HDR=YES;""";

            DbProviderFactory factory =
              DbProviderFactories.GetFactory( "System.Data.OleDb" );

            using( DbConnection connection = factory.CreateConnection( ) )
            {
                connection.ConnectionString = connectionString;

                using( DbCommand command = connection.CreateCommand( ) )
                {
                    // Cities$ comes from the name of the worksheet
                    command.CommandText = "SELECT * FROM [Sheet1$]";

                    connection.Open( );

                    using( DbDataReader dr = command.ExecuteReader( ) )
                    {
                        while( dr.Read( ) )
                        {
                            Debug.WriteLine( dr[0].ToString( ) );
                            Debug.WriteLine( dr[1].ToString( ) );
                            Debug.WriteLine( dr[2].ToString( ) );
                        }
                    }
                }
            }
        }
    }
}