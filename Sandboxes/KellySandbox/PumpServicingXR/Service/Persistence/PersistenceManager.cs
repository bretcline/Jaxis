using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using LFI.Sync.Shared;
using LFI.Sync.DataManager;
using System.IO;
using WFT.PSService.Data;
using WFT.PSService.ServiceLibrary;
using System.Timers;

namespace WFT.PSService.Service
{
    public class PersistenceManager
    {
        public Version Version { get; set; }
        //private SyncManager syncMgr;
        private readonly log4net.ILog logger = log4net.LogManager.GetLogger( "PersistenceManager" );
        public DataManager PMDataManager;
        private int commandTimeoutSeconds = 600;
        private Timer configTimer;

        public void Init( )
        {
            string directory = System.IO.Path.GetDirectoryName( System.Reflection.Assembly.GetExecutingAssembly( ).CodeBase );
            // to get rid of the 'file:\' at the start
            directory = directory.Substring( 6 );

            FileInfo fileInfo = new FileInfo( directory + @"\log4net.config" );
            log4net.Config.XmlConfigurator.ConfigureAndWatch( fileInfo );

            logger.Info( "Beginning AppServer Initialization..." );

            // load timeout setting
            try
            {
                string timeout = ConfigurationManager.AppSettings[ "SQLTimeOutSeconds" ];
                commandTimeoutSeconds = Convert.ToInt32( timeout );
                if( logger.IsDebugEnabled ) logger.DebugFormat( "Setting SQL Time-out to {0} seconds.", timeout );
            }
            catch
            {
                logger.WarnFormat( "Unable to load 'SQLTimeOutSeconds' from web.config. Setting default to 600 seconds (10 minutes)" );
            }

            // build data manager
            if( ConfigurationManager.ConnectionStrings[ "SQLServer" ] != null )
            {
                string wsConnectionString = ConfigurationManager.ConnectionStrings[ "SQLServer" ].ConnectionString;

                try
                {
                    logger.DebugFormat( "Adding connection string, '{0}'.", wsConnectionString );
                    PMDataManager = new DataManager( wsConnectionString );
                    PMDataManager.CommandTimeOutSeconds = commandTimeoutSeconds;

                    // validate server exists
                    string message = PMDataManager.TestConnection( );
                    if( message != "SUCCESS" )
                        throw new Exception( message );

                    // validate server version
                    string version = SettingProperties.Get( PMDataManager, "Database Schema Version" );
                    System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly( );
                    object[ ] attributes = asm.GetCustomAttributes( typeof( AssemblyInformationalVersionAttribute ), false );
                    AssemblyInformationalVersionAttribute currentVersion = ( AssemblyInformationalVersionAttribute ) attributes[ 0 ];

                    if( version != currentVersion.InformationalVersion )
                        throw new Exception( "Server version is incompatible with Configuration database." );
                }
                catch( Exception ex )
                {
                    logger.Error( "Failed to add data manager.", ex );
                }
            }

            // configure config timer
            configTimer = new Timer( 5 * 60 * 1000 ); // 5 minute update
            configTimer.Elapsed += new ElapsedEventHandler( OnUpdateTick );
            configTimer.Start( );

            UpdateConfig( );

            //syncMgr.Init( );

            logger.Info( "Completed PumpServicing AppServer Initialization." );
        }

        public DataManager GetWSDataManager( )
        {
            return PMDataManager;
        }

        public DateTime GetServerTime( )
        {
            return DateTime.Now.ToUniversalTime( );
        }

#warning STUB
        private void UpdateConfig( )
        {
        }

        private void OnUpdateTick( object sender, ElapsedEventArgs e )
        {
            UpdateConfig( );
        }

        //----------------------------------------------------------------------
        public static string GetExceptionString( Exception ex )
        {
            string outException = String.Format( "Exception: {0}\r\n", ex.Message );

            while( ex.InnerException != null )
            {
                ex = ex.InnerException;
                outException += String.Format( "\tInner Exception: {0}\r\n", ex.Message );
            }

            return outException;
        }
    }
}
