using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using TimetrackerData;

public partial class TimeMaster : System.Web.UI.MasterPage
{
    protected void Page_Load( object sender, EventArgs e )
    {
        UserSession MySession = Session["UserSession"] as UserSession;
        if( null == MySession )
        {
            Server.Transfer( "Login.aspx" );
        }
    }
    //protected void btnTimeEntry_Click( object sender, EventArgs e )
    //{
    //    Server.Transfer( "TimeEntry.aspx" );
    //}
    //protected void btnTimeView_Click( object sender, EventArgs e )
    //{
    //    Server.Transfer( "TimeView.aspx" );
    //}
    //protected void btnAdmin_Click( object sender, EventArgs e )
    //{
    //    Server.Transfer( "UsersAdmin.aspx" );
    //}
    //protected void lnkbtnLogout_Click( object sender, EventArgs e )
    //{
    //    Server.Transfer( "Login.aspx" );
    //    Session["UserSession"] = null;
    //}
}
