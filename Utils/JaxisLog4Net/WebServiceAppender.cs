using System;
using System.Collections.Generic;
using System.Text;
using log4net.Core;
using log4net.Appender;
using JaxisLog4Net.JaxisWebLog4Net;

namespace Jaxis.Util.Log4Net
{
    /// <summary>
    /// Class defining the basic functionality of a web service appender.
    /// </summary>
    public class WebServiceAppender : AppenderSkeleton
    {
        #region Properties
        // Defines the Web Service URL
        protected string m_ServiceUrl;
        public string ServiceUrl
        {
            get { return this.m_ServiceUrl; }
            set { this.m_ServiceUrl = value; }
        } 

        // Defines the UserApplication that is using the appender.
        protected string m_UserApplication;
        public string UserApplication
        {
            get { return this.m_UserApplication; }
            set { this.m_UserApplication = value; }
        }
        #endregion

        #region AppenderSkeleton implementation.
        /// <see cref="AppenderSkeleton.Append(LoggingEvent)"/>
        protected override void Append( LoggingEvent loggingEvent )
        {
            // Render the logging event to a string.
            string logMessage = RenderLoggingEvent( loggingEvent );

            // Send the log message to the web service.
            try
            {
                Send( UserApplication, logMessage.ToString( ) );
            }
            catch( Exception e )
            {
                ErrorHandler.Error( "An error occurred while connecting to the logging service.", e );
            }

        }

        /// <see cref="AppenderSkeleton.Append(LoggingEvent[])"/>
        protected override void Append( LoggingEvent[ ] loggingEvents )
        {
            // Prepare a StringBuilder to write the messages to.
            StringBuilder logMessages = new StringBuilder( );

            // Render each logging event to a string, separating events with a new line.
            foreach( LoggingEvent loggingEvent in loggingEvents )
                logMessages.Append( RenderLoggingEvent( loggingEvent ) ).AppendLine( );

            // Send the log message to the web service.
            try
            {
                Send( UserApplication, logMessages.ToString( ) );
            }
            catch( Exception e )
            {
                ErrorHandler.Error( "An error occurred while connecting to the logging service.", e );
            }
        }
        #endregion

        #region Internal logging handler.
        /// <summary>
        /// Sends the log message to the logging service.
        /// </summary>
        /// <param name="message">The message to log.</param>
        protected void Send( string _UserApplication, string _message )
        {
            // Trim the message received.
            _message = _message.Trim( );

            // Connect to the logging service.
            using( Service wsLogger = new Service( ) )
            {
                wsLogger.Url = ServiceUrl;
                wsLogger.LogMessage( _UserApplication, _message );
            }
        }

        #endregion
    }
}
