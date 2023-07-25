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

public partial class _Default : System.Web.UI.Page
{
    TimeEntryService m_Service = new TimeEntryService( );
    UserSession m_Session = null;

    protected void Page_Load( object sender, EventArgs e )
    {
        Page.Title = "Projects Admin";
        m_Session = Session["UserSession"] as UserSession;

        if( m_Session != null )
        {
            lblError.Text = string.Empty;
        }
        else
        {
            Server.Transfer( "Login.aspx" );
        }
    }

    protected void lstProjects_SelectedIndexChanged( object sender, EventArgs e )
    {
        int ProjectID = 0;
        if( true == int.TryParse( lstProjects.SelectedValue, out ProjectID ) )
        {
            Project SelectedProject = m_Service.GetProjectByProjectID( m_Session.SessionID, ProjectID );
            Session["SelectedProject"] = SelectedProject;

            txtProjectName.Text = SelectedProject.Name;
            txtProjectDescription.Text = SelectedProject.Description;
        }
    }

    protected void btnAddProject_Click( object sender, EventArgs e )
    {
        ClearFields( );

        Session["SelectedProject"] = null;
    }

    protected void btnRemoveProject_Click( object sender, EventArgs e )
    {
        Project ProjectToBeRemoved = Session["SelectedProject"] as Project;

        lstProjects.Items.Remove( lstProjects.Items.FindByText( ProjectToBeRemoved.Name ) );
        ClearFields( );

        m_Service.RemoveAProject( m_Session.SessionID, ProjectToBeRemoved );

        Session["SelectedProject"] = null;
    }

    protected void btnProjectSave_Click( object sender, EventArgs e )
    {
        bool ProjectIsNew = false;
        Project ChangingProject = Session["SelectedProject"] as Project;
        if( null == ChangingProject )
        {
            ChangingProject = new Project( );
            ProjectIsNew = true;
        }

        ChangingProject.Name = txtProjectName.Text;
        ChangingProject.Description = txtProjectDescription.Text;

        if( true == ProjectIsNew )
        {
            bool AlreadyThere = false;

            foreach( Project p in m_Service.GetAllProjects( m_Session.SessionID ) )
            {
                if( p.Name.Equals( txtProjectName.Text ) )
                {
                    AlreadyThere = true;
                    break;
                }
            }

            if( AlreadyThere == false )
            {
                UserSession SessionWithProject = Session["UserSession"] as UserSession;
                m_Service.AddOrEditProject( SessionWithProject.SessionID, ChangingProject, true );
                lstProjects.Items.Add( new ListItem( ChangingProject.Name, Convert.ToString( ChangingProject.ProjectID ) ) );

                Session["SelectedProject"] = null;
                ClearFields( );
            }
            else
            {
                lblError.Text = "A project with that name already exists. Please enter a new name.";
            }
        }
        else
        {
            m_Service.AddOrEditProject( m_Session.SessionID, ChangingProject, false );

            Session["SelectedProject"] = null;
        }
    }

    public void ClearFields( )
    {
        txtProjectDescription.Text = string.Empty;
        txtProjectName.Text = string.Empty;
    }

    protected void btnManageProjectGroups_Click( object sender, EventArgs e )
    {
        Session["SelectedProject"] = null;
        Session["PageFrom"] = Page.Title;
        Server.Transfer( "ProjectToGroupAdmin.aspx" );
    }
}
