using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using TimetrackerData;

public partial class Default2 : System.Web.UI.Page
{
    TimeEntryService m_Service = new TimeEntryService( );

    protected void Page_Load( object sender, EventArgs e )
    {
        Session["UserSession"] = null;
        Page.Title = "Login";
    }
    protected void btnLogin_Click( object sender, EventArgs e )
    {
        Guid MySessionID = m_Service.Login( txtUsername.Text, txtPassword.Text );
        UserSession MySession = m_Service.GetSession( MySessionID );

        if( null != MySession )
        {
            Session.Add( "UserSession", MySession );
            Server.Transfer( "TimeEntry.aspx" );
        }
        else
        {
            lblInvalidLogin.Text = "Invalid login-password combination. Please re-enter login and/or password.";
        }

    }
}
