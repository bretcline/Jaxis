using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Jaxis.LogicalModel.Data;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Service : System.Web.Services.WebService
{
    public Service () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public void LogMessage( string _userApplication, string _message )
    {
        Log item = new Log( );
        item.LogUserApplication = _userApplication;
        item.LogMessage = _message;
        item.LogDate = DateTime.Now;
        item.Save( );
    }
    
}
