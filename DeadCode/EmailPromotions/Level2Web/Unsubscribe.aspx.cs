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
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using log4net;

namespace Level2Web
{
    public partial class Unsubscribe : System.Web.UI.Page
    {
        protected void Page_Load( object sender, EventArgs e )
        {
        }

        /// <summary>
        /// Event handler for the Unsubscribe button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUnsubscribe_Click( object sender, EventArgs e )
        {
            if( (bool)Session["SubmitData"] == true )
            {
                Level2Web.Properties.Settings props = new Level2Web.Properties.Settings( );
                SqlConnection connection = new SqlConnection( props.ConnectionString );

                try
                {
                    connection.Open( );

                    SqlCommand command = new SqlCommand( "UPDATE dbo.EmailList SET StatusID = (SELECT StatusID FROM dbo.Status WHERE dbo.Status.Name = 'Removed') WHERE dbo.EmailList.EmailAddress = '" + txtEmail.Text + "'", connection );
                    if( 0 >= command.ExecuteNonQuery( ) )
                    {
                        Controls.Add( new LiteralControl( "<font color=red>Unsubscribe failed.</font>" ) );
                        return;
                    }

                    Controls.Add( new LiteralControl( "Your email address has been removed from the mailing list." ) );
                    btnUnsubscribe.Text = "Close";
                    btnUnsubscribe.Attributes.Add( "onclick", "self.close()" );
                }
                catch( SqlException ex )
                {
                    ILog log = LogManager.GetLogger( System.Reflection.MethodBase.GetCurrentMethod( ).DeclaringType );
                    log4net.Config.XmlConfigurator.Configure( );
                    if( log.IsErrorEnabled )
                    {
                        log.Error( "SQL Exception occurred during insert process: " + ex.Message );
                    }
                }
                finally
                {
                    if( connection.State == System.Data.ConnectionState.Open )
                    {
                        connection.Close( );
                    }
                }
            }
        }

        /// <summary>
        /// Server side validation for the email address entered by the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void EmailValidate( object sender, ServerValidateEventArgs args )
        {
            //Check for valid email address including not allowing an empty string
            Regex exp = new Regex( "[a-zA-Z0-9_-]+(\\.[a-zA-Z0-9_-]+)*@[a-zA-Z0-9_-]+(\\.[a-zA-Z0-9_-]+)+" );
            Match email = exp.Match( args.Value );
            if( !email.Success )
            {
                args.IsValid = false;
                SetFocus( "txtEmail" );
                txtEmail.Attributes.Add( "onfocus", "document.Unsubscribe.txtEmail.select();" );
                Session["SubmitData"] = false;
                return;
            }

            Session["SubmitData"] = true;
            args.IsValid = true;
        }
    }
}
