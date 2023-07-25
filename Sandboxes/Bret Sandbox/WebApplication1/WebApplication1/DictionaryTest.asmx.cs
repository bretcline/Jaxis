using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Collections;

namespace WebApplication1
{
    /// <summary>
    /// Summary description for DictionaryTest
    /// </summary>
    [WebService( Namespace = "http://tempuri.org/" )]
    [WebServiceBinding( ConformsTo = WsiProfiles.BasicProfile1_1 )]
    [System.ComponentModel.ToolboxItem( false )]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class DictionaryTest : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld( )
        {
            return "Hello World";
        }

        [WebMethod]
        public System.Collections.Hashtable GetTheDictionary( )
        {
            Hashtable rc = new Hashtable( );
            rc[1] = "Hi";
            rc[2] = "There";
            return rc;
        }
    }
}
