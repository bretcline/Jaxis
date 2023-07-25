using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using log4net;

namespace Level2Web
{
    public partial class Survey : System.Web.UI.Page
    {
        protected void Page_Load( object sender, EventArgs e )
        {
            //Connect to database and retrieve survey questions based on the Promotion ID sent to the page
            Level2Web.Properties.Settings props = new Level2Web.Properties.Settings( );
            SqlConnection connection = new SqlConnection( props.ConnectionString );
            //SqlCommand command = new SqlCommand( "SELECT Question FROM dbo.SurveyQuestions WHERE PromotionID = " + Request.QueryString.Get( "PromotionID" ) );
            SqlCommand command = new SqlCommand( "SELECT Question FROM dbo.SurveyQuestions WHERE PromotionID = 0" );
            connection.Open( );

            command.Connection = connection;
            SqlDataReader reader = command.ExecuteReader( );
            if( reader.HasRows )
            {
                //Local collection to avoid multiple calls to FindControl()
                ControlCollection controls = FindControl( "Survey" ).Controls;

                //Declare dynamic controls
                int count = 1;
                Button submit = new Button( );

                //For each question found, increment the question number and display question
                while( reader.Read( ) )
                {
                    //Declare dynamic controls
                    Label questionNumber = new Label( );
                    Label question = new Label( );
                    TextBox answer = new TextBox( );
                    answer.Style["Width"] = "641px"; //Random selection from design view
                    answer.TextMode = TextBoxMode.SingleLine;

                    questionNumber.Text = count.ToString( ) + ") ";
                    question.ID = "lblQuestion" + count++;
                    question.Text = reader["Question"].ToString( );
                    controls.Add( questionNumber );
                    controls.Add( question );
                    controls.Add( new LiteralControl( "\n<br />\n" ) );
                    controls.Add( answer );
                    controls.Add( new LiteralControl( "\n<br />\n" ) );
                    controls.Add( new LiteralControl( "\n<br />\n" ) );
                }

                //Setup the Submit button when finished with the questions
                submit.ID = "btnSubmit";
                submit.Text = "Submit";
                submit.Click += new EventHandler( submit_Click );
                controls.Add( new LiteralControl( "\n<br />\n" ) );
                controls.Add( submit );
            }

            if( connection.State == System.Data.ConnectionState.Open )
            {
                connection.Close( );
            }
        }

        /// <summary>
        /// Event handler for the Submit button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void submit_Click( object sender, EventArgs e )
        {
            //Setup database connection and prepare for updating user record with XML data of survey responses
            Level2Web.Properties.Settings props = new Level2Web.Properties.Settings( );
            SqlConnection connection = new SqlConnection( props.ConnectionString );

            try
            {
                SqlCommand command = new SqlCommand( );
                connection.Open( );
                command.Connection = connection;

                //Setup XML document
                XDocument xmlData = new XDocument( new XDeclaration( "1.0", "UTF-8", "yes" ) );
                xmlData.Add( new XElement( "Survey" ) );

                int promotionID = Convert.ToInt32( Request.QueryString.Get( "PromotionID" ) );
                int count = 1;
                foreach( TextBox answer in FindControl( "Survey" ).Controls.OfType<TextBox>( ) )
                {
                    Label question = FindControl("lblQuestion" + count++) as Label;
                    xmlData.Root.Add( new XElement( "Response",
                                         new XElement( "Question", question.Text ),
                                         new XElement( "Answer", answer.Text ),
                                         new XElement( "PromotionLevel", promotionID ) ) );
                }

                //Update the EmailList table with formatted XML data
                command.CommandText = "UPDATE dbo.EmailList SET dbo.EmailList.SurveyData = '" + xmlData + "' WHERE dbo.EmailList.EmailListID = 0"; // + Request.QueryString.Get( "EmailID" );
                command.ExecuteNonQuery( );

                Controls.Add( new LiteralControl( "Thank you for responding to the survey." ) );
                Button btnSubmit = FindControl( "btnSubmit" ) as Button;
                btnSubmit.Text = "Close";
                btnSubmit.Attributes.Add( "onclick", "self.close()" );
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
}
