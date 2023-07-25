using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFT.PSService.Service;

namespace WFT.PSService.ServiceHost
{
    public partial class ServerStatus : System.Web.UI.Page
    {
        protected void Page_Load( object sender, EventArgs e )
        {
            PersistenceManager perMgr = Global.PersistenceMgr;

            this.form1.InnerHtml = "<b>Pump Servicing XR Server Version:</b> " + perMgr.Version + "<br><br>";
            this.form1.InnerHtml += "<b>Server Time:</b><br>";
            this.form1.InnerHtml += perMgr.GetServerTime( ) + " (UTC)<br>";
            this.form1.InnerHtml += perMgr.GetServerTime( ).ToLocalTime( ) + " (Local)<br><br>";

            //if( perMgr.GetMasterServerID( ) == Guid.Empty )
            //    this.form1.InnerHtml += "<b>Last Sync Time (UTC):</b> Not Yet Configured<br>";
            //else
            //    this.form1.InnerHtml += "<b>Last Sync Time (UTC):</b> " + perMgr.GetLastSyncTime( perMgr.GetMasterServerID( ) ) + "<br>";
        }
    }
}
