using System;
using System.Collections.Generic;
using System.Linq;
using WFT.PSService.ServiceLibrary;
using LFI.Sync.DataManager;
using WFT.PSService.Proxy;
using System.Configuration;
using System.Reflection;
using PSClient.Properties;

namespace PSClient
{
    public class Program
    {
        private DataManager dataManager;
        private SyncManager syncManager;
        private DataModel _dataModel;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main( )
        {
            Program p = new Program( );
            p.Run( );
        }

        public void Run( )
        {
            string connectionStr = ConfigurationManager.ConnectionStrings[ "LocalData" ].ToString( );
            dataManager = new DataManager( connectionStr );

            string testConnectionResult = dataManager.TestConnection( );
            if( testConnectionResult != "SUCCESS" )
                throw new ConfigurationErrorsException( testConnectionResult );

            ApplicationSettings.DataManager = dataManager;

            // Verify database version
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly( );
            object[ ] attributes = asm.GetCustomAttributes( typeof( AssemblyInformationalVersionAttribute ), false );
            AssemblyInformationalVersionAttribute currentVersion = ( AssemblyInformationalVersionAttribute ) attributes[ 0 ];

            if( ApplicationSettings.DatabaseVersion != currentVersion.InformationalVersion )
                throw new ConfigurationErrorsException( String.Format( Resources.IncompatibleDatabaseError + " " + Resources.Error, 
                    ApplicationSettings.DatabaseVersion ) );

            // configure data model
            _dataModel = new DataModel( dataManager );
            _dataModel.RequestSync( false );
        }
    }
}
