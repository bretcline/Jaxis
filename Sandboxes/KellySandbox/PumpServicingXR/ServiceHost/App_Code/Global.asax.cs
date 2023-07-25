using System;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Timers;
using WFT.PSService.Service;
using System.Web;

namespace WFT.PSService.ServiceHost
{
    public class Global : System.Web.HttpApplication
    {
        public static PersistenceManager PersistenceMgr;
        private log4net.ILog logger = log4net.LogManager.GetLogger( "Global" );

        protected void Application_Start( object sender, EventArgs e )
        {
            try
            {
                PersistenceMgr = new PersistenceManager( );

                System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly( );
                System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo( asm.Location );

                PersistenceMgr.Version = new Version( fvi.FileMajorPart, fvi.FileMinorPart, fvi.FileBuildPart, fvi.FilePrivatePart );
                PersistenceMgr.Init( );

                AppServer.PersistenceMgr = PersistenceMgr;
            }
            catch( Exception ex )
            {
                throw new Exception( "Server failed to initialize persistent data manager.", ex );
            }
        }

        protected void Session_Start( object sender, EventArgs e )
        {

        }

        protected void Application_BeginRequest( object sender, EventArgs e )
        {

        }

        protected void Application_AuthenticateRequest( object sender, EventArgs e )
        {

        }

        protected void Application_Error( object sender, EventArgs e )
        {
            Exception ex = Context.Server.GetLastError( );
            if( ex is HttpUnhandledException )
            {
                ex = Context.Error.InnerException;
            }

            logger.ErrorFormat( "Unhandled Exception reported to global.asax.\r\n{0}", ex );
        }

        protected void Session_End( object sender, EventArgs e )
        {

        }

        protected void Application_End( object sender, EventArgs e )
        {
            logger.Info( "Application shutting down." );
        }
    }
}