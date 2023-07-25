using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Data.SqlClient;
using log4net;

namespace Level2Service
{
    class Program
    {
        static void Main( string[] args )
        {
            //Database
            Level2Service.Properties.Settings props = new Level2Service.Properties.Settings( );
            SqlConnection connection = new SqlConnection( props.ConnectionString );

            //Logging
            ILog log = LogManager.GetLogger( System.Reflection.MethodBase.GetCurrentMethod( ).DeclaringType );
            log4net.Config.XmlConfigurator.Configure( );

            try
            {
                //Connect to database and retrieve relevant email addresses
                SqlDataReader reader = null;
                SqlCommand query = new SqlCommand( "SELECT email.EmailAddress, email.PromotionID, email.PromotionLevelID, promLevel.PageAddress, proms.Name FROM dbo.EmailList email " +
                                                   "JOIN dbo.PromotionLevel promLevel ON email.PromotionLevelID = promLevel.PromotionLevelID " +
                                                   "JOIN dbo.Promotions proms ON email.PromotionID = proms.PromotionID " +
                                                   "JOIN dbo.Status statusTbl ON email.StatusID = statusTbl.StatusID " +
                                                   "WHERE datediff(day, dateadd(day, promLevel.SendAfterDays, email.StartDate), getdate()) >= 0 AND " +
                                                   "email.StatusID NOT IN(SELECT StatusID FROM dbo.Status WHERE dbo.Status.Name = 'Removed' OR dbo.Status.Name = 'No Response') " +
                                                   "ORDER BY email.PromotionLevelID ASC" );

                //Open database connection and execute query
                connection.Open( );
                query.Connection = connection;
                reader = query.ExecuteReader( );

                if( reader.HasRows )
                {
                    //Email objects
                    MailMessage message = new MailMessage( );
                    SmtpClient email = new SmtpClient( props.SmtpHost, props.SmtpPort );

                    //Setup contant values for email message
                    message.From = new MailAddress( "noreply@something.com" );
                    message.IsBodyHtml = true;
                    message.Priority = MailPriority.Normal;

                    while( reader.Read( ) )
                    {
                        message.To.Add( reader["EmailAddress"].ToString( ) );
                        message.Body = reader["PageAddress"].ToString( );
                        message.Subject = reader["Name"].ToString( );
                    }

                    //Uncomment this when email logic is in place; otherwise errors will occur
                    //email.Send( message );
                }

                reader.Close( );

                //Update promotion level for all records
                query.CommandText = "UPDATE dbo.EmailList SET PromotionLevelID = (CASE PromotionLevelID " +
                                    "WHEN 0 THEN 1 " +
                                    "WHEN 1 THEN 2 " +
                                    "WHEN 2 THEN 3 " +
                                    "WHEN 3 THEN 4 " +
                                    "ELSE 4 END), " +
                                    "StatusID = (SELECT dbo.Status.StatusID FROM dbo.Status WHERE dbo.Status.Name = 'Completed') " +
                                    "WHERE dbo.EmailList.EmailAddress IN " +
                                    "(SELECT email.EmailAddress FROM dbo.EmailList email " +
                                    "JOIN dbo.PromotionLevel promLevel ON email.PromotionLevelID = promLevel.PromotionLevelID " +
                                    "JOIN dbo.Status statusTbl ON email.StatusID = statusTbl.StatusID " +
                                    "WHERE datediff(day, dateadd(day, promLevel.SendAfterDays, email.StartDate), getdate()) >= 0 AND " +
                                    "email.StatusID NOT IN(SELECT StatusID FROM dbo.Status WHERE dbo.Status.Name = 'Removed' OR dbo.Status.Name = 'No Response'))";
                
                int rows = query.ExecuteNonQuery( );
                if( log.IsInfoEnabled )
                {
                    log.Info( "Updated " + rows + "rows: " + query.CommandText );
                }
            }
            catch( Exception ex )
            {   
                if( log.IsErrorEnabled )
                {
                    log.Error( "Exception occurred during processing: " + ex.Message );
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
