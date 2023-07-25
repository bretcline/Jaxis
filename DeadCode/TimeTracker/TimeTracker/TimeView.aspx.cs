using System;
using System.Collections;
using System.Collections.Generic;
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

public partial class TimeView : System.Web.UI.Page
{
    protected void Page_Load( object sender, EventArgs e )
    {
        Page.Title = "Time Viewer";

        if( false == IsPostBack )
        {
            UserSession MySession = Session["UserSession"] as UserSession;

            if( MySession != null )
            {
                txtUserID.Text = MySession.UserID.ToString( );

                clndrStart.SelectedDate = DateTime.Now;
                clndrStart_SelectionChanged( null, null );

                clndrEnd.SelectedDate = DateTime.Now;
                clndrEnd_SelectionChanged( null, null );
            }
            else
            {
                Server.Transfer( "Login.aspx" );
            }
        }
    }
    protected void clndrStart_SelectionChanged( object sender, EventArgs e )
    {
        txtStart.Text = clndrStart.SelectedDate.ToShortDateString( );
    }
    protected void clndrEnd_SelectionChanged( object sender, EventArgs e )
    {
        DateTime Current = clndrEnd.SelectedDate.AddDays( 1 );
        Current = Current.AddSeconds( -1 );
        clndrEnd.SelectedDate = Current;
        txtEnd.Text = clndrEnd.SelectedDate.ToShortDateString( );
    }
    protected void txtStart_TextChanged( object sender, EventArgs e )
    {
        DateTime NewDate = DateTime.Today;

        try
        {
            NewDate = Convert.ToDateTime( txtStart.Text );
            clndrStart.SelectedDate = NewDate;
            lblStartError.Text = string.Empty;
        }
        catch( FormatException ex )
        {
            lblStartError.Text = "Sorry, incorrect date entry. Please use MM/DD/YYYY format.";
            txtStart.Text = clndrStart.TodaysDate.ToShortDateString( );
        }
    }
    protected void txtEnd_TextChanged( object sender, EventArgs e )
    {
        DateTime NewDate = DateTime.Today;

        try
        {
            NewDate = Convert.ToDateTime( txtEnd.Text );
            NewDate.AddDays( 1 );
            NewDate.AddSeconds( -1 );
            clndrEnd.SelectedDate = NewDate;
            lblEndError.Text = string.Empty;
        }
        catch( FormatException ex )
        {
            lblEndError.Text = "Sorry, incorrect date entry. Please use MM/DD/YYYY format.";
            txtEnd.Text = clndrEnd.TodaysDate.ToShortDateString( );
        }
    }

    protected void grdTime_SelectedIndexChanged( object sender, EventArgs e )
    {
        Session["TimeEntryID"] = grdTime.SelectedDataKey.Value;
        Server.Transfer( "TimeEntry.aspx" );
    }
}
