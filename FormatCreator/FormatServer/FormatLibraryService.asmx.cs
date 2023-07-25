using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using LFI.RFID.Editor;
using System.Configuration;
using LFI.RFID.Format;

namespace LFI.RFID.FormatServer
{
    /// <summary>
    /// Exposes the format library and integration services
    /// </summary>
    [WebService(Namespace = "http://www.lyonsforge.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class FormatLibraryService : System.Web.Services.WebService
    {
        public FormatLibraryService()
        {
            string formatPath = ConfigurationManager.AppSettings.Get("FormatPath");
            formatManager = new FormatManager(formatPath);
        }

        [WebMethod]
        public FormatDef GetFormat(Guid formatID)
        {
            return formatManager.GetFormatByID(formatID);
        }

        [WebMethod]
        public string GetFormatAsString(Guid formatID)
        {
            FormatDef def = formatManager.GetFormatByID(formatID);
            if (def == null)
                return string.Empty;
            else
                return formatManager.GetFormatXML(def);
        }

        private FormatManager formatManager;
    }
}
