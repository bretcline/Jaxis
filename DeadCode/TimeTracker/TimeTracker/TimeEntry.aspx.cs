using System;
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
using System.Data.Linq;
using TimetrackerData;
using System.Collections.Generic;


public partial class _Default : System.Web.UI.Page
{
    TimeEntryService m_Service = new TimeEntryService( );
    UserSession m_UserSession = null;

    protected void Page_Load( object sender, EventArgs e )
    {
        Page.Title = "Time Entry";
        m_UserSession = Session["UserSession"] as UserSession;

        if( false == IsPostBack )
        {
            if( m_UserSession != null )
            {
                List<Project> ListOfProjects = new List<Project>( );
                ListOfProjects.AddRange( m_Service.GetProjects( m_UserSession.SessionID, false ) );

                ddlProjects.DataTextField = "Name";
                ddlProjects.DataValueField = "ProjectID";
                ddlProjects.DataSource = ListOfProjects;
                ddlProjects.DataBind( );

                if( null != Session["TimeEntryID"] )
                {
                    TimeEntry EntryToModify = m_Service.GetTimeEntryByTEID( m_UserSession.SessionID, Convert.ToInt32( Session["TimeEntryID"] ) );

                    EntryToModify.UserID = m_UserSession.UserID;

                    clndrDay.SelectedDate = EntryToModify.StartTime;
                    txtDate.Text = Convert.ToString( EntryToModify.StartTime );

                    FillDateFields( EntryToModify.StartTime, EntryToModify.EndTime.Value );

                    txtPreviousNotes.Text = EntryToModify.Notes;

                    ddlProjects.SelectedValue = Convert.ToString( EntryToModify.ProjectID );

                    Session["EntryBeingModified"] = EntryToModify;

                    Session["TimeEntryID"] = null;
                }
                else
                {
                    clndrDay.SelectedDate = DateTime.Now;
                    clndrDay_SelectionChanged1( null, null );

                    FillDateFields( DateTime.Now, DateTime.Now );
                }
            }
            else
            {
                Server.Transfer( "Login.aspx" );
            }
        }
    }

    protected void txtNotes_TextChanged( object sender, EventArgs e )
    {

    }

    protected void btnAdd_Click( object sender, EventArgs e )
    {
        if( null != m_UserSession )
        {
            if( null == clndrDay.SelectedDate )
            {
                lblError.Text = "Sorry, incorrect date entry. Please use MM/DD/YYYY format. (Entry was not saved.)";
            }
            else
            {
                lblError.Text = string.Empty;

                TimeEntry MyEntry = Session["EntryBeingModified"] as TimeEntry;

                if( null == MyEntry )
                {
                    MyEntry = new TimeEntry( );
                }

                MyEntry.StartTime = FormatDate( txtStartMonth.Text, txtStartDay.Text, txtStartYear.Text, txtStartHour.Text, txtStartMinute.Text );
                MyEntry.EndTime = FormatDate( txtEndMonth.Text, txtEndDay.Text, txtEndYear.Text, txtEndHour.Text, txtEndMinute.Text );

                if( null != MyEntry.StartTime && null != MyEntry.EndTime.Value && 0 > MyEntry.StartTime.CompareTo( MyEntry.EndTime.Value ) )
                {

                    MyEntry.UserID = m_UserSession.UserID;

                    MyEntry.ProjectID = Convert.ToInt32( ddlProjects.SelectedValue );

                    MyEntry.Notes = txtNewNotes.Text + System.Environment.NewLine +
                        System.Environment.NewLine + txtPreviousNotes.Text.Trim( );

                    m_Service.SaveTimeEntry( m_UserSession.SessionID, MyEntry );

                    ClearForm( false );
                }
                else
                {
                    lblError.Text = "Invalid start and/or end time. Please re-enter these fields. (Entry was not saved.)";
                    ClearForm( true );
                }
            }
        }
    }

    protected void txtDate_TextChanged( object sender, EventArgs e )
    {
        DateTime NewDate = DateTime.Today;

        try
        {
            NewDate = Convert.ToDateTime( txtDate.Text );
            clndrDay.SelectedDate = NewDate;

            FillDateFields( NewDate, NewDate );

            lblError.Text = string.Empty;
        }
        catch( FormatException ex )
        {
            lblError.Text = "Sorry, incorrect date entry. Please use MM/DD/YYYY format.";
            txtDate.Text = clndrDay.TodaysDate.ToShortDateString( );
        }
    }

    protected void btnDelete_Click( object sender, EventArgs e )
    {
        TimetrackerData.TimetrackerData Data = new TimetrackerData.TimetrackerData( );
        TimeEntry Entry = Session["EntryBeingModified"] as TimeEntry;

        if( null != Entry && null != Entry.TimeEntryID )
        {
            Data.DeleteTimeEntry( Entry.TimeEntryID );

            ClearForm( false );
        }
        else
        {
            ClearForm( false );
        }

    }

    protected void clndrDay_SelectionChanged1( object sender, EventArgs e )
    {
        FillDateFields( clndrDay.SelectedDate, clndrDay.SelectedDate );
    }

    public void FillDateFields( DateTime TheStartTime, DateTime TheEndTime )
    {
        txtDate.Text = TheStartTime.ToShortDateString( );

        txtStartMonth.Text = Convert.ToString( TheStartTime.Month );
        txtStartDay.Text = Convert.ToString( TheStartTime.Day );
        txtStartYear.Text = Convert.ToString( TheStartTime.Year );

        txtStartHour.Text = Convert.ToString( TheStartTime.Hour );
        txtStartMinute.Text = Convert.ToString( TheStartTime.Minute );

        if( null != TheEndTime )
        {
            txtEndMonth.Text = Convert.ToString( TheEndTime.Month );
            txtEndDay.Text = Convert.ToString( TheEndTime.Day );
            txtEndYear.Text = Convert.ToString( TheEndTime.Year );

            txtEndHour.Text = Convert.ToString( TheEndTime.Hour );
            txtEndMinute.Text = Convert.ToString( TheEndTime.Minute );
        }
        else
        {
            txtEndMonth.Text = string.Empty;
            txtEndDay.Text = string.Empty;
            txtEndYear.Text = string.Empty;

            txtEndHour.Text = string.Empty;
            txtEndMinute.Text = string.Empty;
        }
    }

    public void ClearForm( bool TimesOnly )
    {
        if( false == TimesOnly )
        {
            txtPreviousNotes.Text = string.Empty;

            txtNewNotes.Text = string.Empty;

            clndrDay.SelectedDate = DateTime.Today;

            txtDate.Text = DateTime.Today.ToShortDateString( );

            Session["EntryBeingModified"] = null;
        }

        txtStartMonth.Text = string.Empty;
        txtStartDay.Text = string.Empty;
        txtStartYear.Text = string.Empty;

        txtStartHour.Text = string.Empty;
        txtStartMinute.Text = string.Empty;

        txtEndMonth.Text = string.Empty;
        txtEndDay.Text = string.Empty;
        txtEndYear.Text = string.Empty;

        txtEndHour.Text = string.Empty;
        txtEndMinute.Text = string.Empty;
    }

    public DateTime FormatDate( string Month, string Day, string Year, string Hour, string Minute )
    {
        string DateAsString = "{0}/{1}/{2} {3}:{4}";

        DateAsString = string.Format( DateAsString, Month, Day, Year, Hour, Minute );

        bool CatchTryReturn = true;
        DateTime DateToReturn = new DateTime( );
        CatchTryReturn = DateTime.TryParse( DateAsString, out DateToReturn );

        return DateToReturn;
    }
}