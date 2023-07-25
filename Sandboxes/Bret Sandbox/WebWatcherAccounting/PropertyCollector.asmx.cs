using System;
using System.Web.Services;
using Jaxis.Utilities.Database;

namespace WebWatcherAccounting
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService( Namespace = "http://tempuri.org/" )]
    [WebServiceBinding( ConformsTo = WsiProfiles.BasicProfile1_1 )]
    [System.ComponentModel.ToolboxItem( false )]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    // [System.Web.Script.Services.ScriptService]
    public class PropertyCollector : System.Web.Services.WebService
    {
        [WebMethod]
        public bool AcceptProperty( string _PropertyInformation )
        {
            bool rc = false;
            string ConnectionString = string.Empty;
            using( SqlTool DBConn = new SqlTool( DBTypes.SqlServer, ConnectionString ) )
            {
                SqlParameterList Params = new SqlParameterList( );
                Params.AddInParameter( "@AccountingID", Guid.NewGuid( ) );
                Params.AddInParameter( "@Description", _PropertyInformation );
                rc = DBConn.Execute( "INSERT INTO dbo.AcceptedProperties ( AccountingID, Description ) VALUES ( @AccountingID, @Description )", Params );
            }
            return rc;
        }
    }
}