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

public partial class UsersAdmin : System.Web.UI.Page
{
    TimeEntryService m_Service = new TimeEntryService( );
    UserSession m_UserSession = null;

    protected void Page_Load( object sender, EventArgs e )
    {
        Page.Title = "Users Admin";
        m_UserSession = Session["UserSession"] as UserSession;

        if( m_UserSession != null )
        {
            //This code allows the user to hit enter from any of the text boxes to save the data.
            txtUserLogin.Attributes.Add( "onkeypress", "return clickButton(event,'" + btnSave.ClientID + "')" );
            txtPassword.Attributes.Add( "onkeypress", "return clickButton(event,'" + btnSave.ClientID + "')" );
            txtConfirmPassword.Attributes.Add( "onkeypress", "return clickButton(event,'" + btnSave.ClientID + "')" );
            txtFirstName.Attributes.Add( "onkeypress", "return clickButton(event,'" + btnSave.ClientID + "')" );
            txtLastName.Attributes.Add( "onkeypress", "return clickButton(event,'" + btnSave.ClientID + "')" );
            txtCompany.Attributes.Add( "onkeypress", "return clickButton(event,'" + btnSave.ClientID + "')" );
        }
        else
        {
            Server.Transfer( "Login.aspx" );
        }
    }

    protected void lstUsers_SelectedIndexChanged( object sender, EventArgs e )
    {
        lblError.Text = string.Empty;

        int UserID = 0;
        if( true == int.TryParse( lstUsers.SelectedValue, out UserID ) )
        {
            User SelectedUser = m_Service.GetUserByUserID( m_UserSession.SessionID, UserID );
            Session["SelectedUser"] = SelectedUser;

            txtUserLogin.Text = SelectedUser.Login;
            txtPassword.Text = SelectedUser.Password;
            txtFirstName.Text = SelectedUser.FirstName;
            txtLastName.Text = SelectedUser.LastName;
            txtCompany.Text = SelectedUser.Company;
        }
    }

    protected void btnSave_Click( object sender, EventArgs e )
    {
        lblError.Text = string.Empty;

        bool UserIsNew = false;
        bool LoginAlreadyThere = false;
        bool FullNameAlreadyThere = false;
        bool PasswordConfirmed = true;
        User ChangingUser = Session["SelectedUser"] as User;

        if( null == ChangingUser )
        {
            ChangingUser = new User( );
            UserIsNew = true;
        }

        if( 0 < txtPassword.Text.Length )
        {
            PasswordConfirmed = false;
            if( txtConfirmPassword.Text == txtPassword.Text )
            {
                PasswordConfirmed = true;
            }
        }

        if( true == PasswordConfirmed )
        {
            ChangingUser = FillInUserData( ChangingUser, txtUserLogin.Text, txtPassword.Text, txtFirstName.Text,
                txtLastName.Text, txtCompany.Text );

            if( true == UserIsNew )
            {
                foreach( User u in m_Service.GetAllUsers( m_UserSession.SessionID ) )
                {
                    if( u.Login.Equals( txtUserLogin.Text ) )
                    {
                        LoginAlreadyThere = true;
                        break;
                    }
                }

                foreach( User u in m_Service.GetAllUsers( m_UserSession.SessionID ) )
                {
                    if( u.FullName.Equals( txtFirstName.Text + " " + txtLastName.Text ) )
                    {
                        FullNameAlreadyThere = true;
                        break;
                    }
                }

                if( FullNameAlreadyThere == false && LoginAlreadyThere == false )
                {
                    m_Service.AddOrEditUser( m_UserSession.SessionID, ChangingUser, true );
                    lstUsers.Items.Add( new ListItem( ChangingUser.FullName, Convert.ToString( ChangingUser.UserID ) ) );

                    Session["SelectedUser"] = null;
                    ClearFields( );
                }
                else if( FullNameAlreadyThere == true && LoginAlreadyThere == true )
                {
                    lblError.Text = "A user with that login and full name already exists.";
                }
                else if( FullNameAlreadyThere == true && LoginAlreadyThere == false )
                {
                    lblError.Text = "A user with that full name already exists.";
                }
                else
                {
                    lblError.Text = "A user with that login already exists.";
                }
            }
            else
            {
                m_Service.AddOrEditUser( m_UserSession.SessionID, ChangingUser, false );

                Session["SelectedUser"] = null;
                ClearFields( );
            }
        }
        else
        {
            lblError.Text = "New password not confirmed. Please retype new password into both the password field and the confirm password field.";
        }
    }

    protected void btnAddUser_Click( object sender, EventArgs e )
    {
        lblError.Text = string.Empty;
        ClearFields( );

        Session["SelectedUser"] = null;
    }

    public User FillInUserData( User RecievedUser, string FILogin, string FIPassword, string FIFirstName, string FILastName, string FICompany )
    {
        RecievedUser.Login = FILogin;
        if( 0 < txtPassword.Text.Length )
        {
            RecievedUser.Password = FIPassword;
        }
        RecievedUser.FirstName = FIFirstName;
        RecievedUser.LastName = FILastName;
        RecievedUser.Company = FICompany;

        return RecievedUser;
    }

    protected void btnRemove_Click( object sender, EventArgs e )
    {
        lblError.Text = string.Empty;
        
        User UserToBeRemoved = Session["SelectedUser"] as User;

        lstUsers.Items.Remove( lstUsers.Items.FindByText( UserToBeRemoved.FullName ) );
        ClearFields( );

        m_Service.RemoveUser( m_UserSession.SessionID, UserToBeRemoved );

        Session["SelectedUser"] = null;
    }

    public void ClearFields( )
    {
        txtUserLogin.Text = string.Empty;
        txtPassword.Text = string.Empty;
        txtFirstName.Text = string.Empty;
        txtLastName.Text = string.Empty;
        txtCompany.Text = string.Empty;
    }

    protected void btnManageUserGroups_Click( object sender, EventArgs e )
    {
        Session["SelectedUser"] = null;
        Session["PageFrom"] = Page.Title;
        Server.Transfer( "UserToGroupAdmin.aspx" );
    }
}
