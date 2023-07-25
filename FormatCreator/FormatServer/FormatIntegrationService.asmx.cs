using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using LFI.RFID.Format;

namespace LFI.RFID.FormatServer
{
    /// <summary>
    /// Summary description for FormatIntegrationService
    /// </summary>
    [WebService(Namespace = "http://www.lyonsforge.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class FormatIntegrationService : System.Web.Services.WebService
    {
        public FormatIntegrationService()
        {
            forwarder = new DataForwarder();
        }

        [WebMethod]
        public void PostTagData(TagData data)
        {
            forwarder.Forward(data);
        }

        private DataForwarder forwarder;
    }
}
