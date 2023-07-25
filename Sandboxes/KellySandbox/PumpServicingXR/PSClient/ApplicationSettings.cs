using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using log4net;
using LFI.Sync.DataManager;
using LFI.Sync.Shared;
using PSClient.Properties;
using System.Configuration;

namespace PSClient
{
    public class ApplicationSettings : IDisposable
    {
        //private const string ActiveWSMKey = "Active WSM";
        private const string LastServiceProviderKey = "Last Service Provider";
        private const string AllowDoubleClickKey = "Allow Double Click";
        private const string ApplicationServerFileName = "ApplicationSettings.txt";
        //private const string AppServerIDKey = "AppServerID";
        private const string CurrentCultureKey = "Current Culture";
        private const string DatabasePathKey = "Database File";
        private const string DatabaseVersionKey = "Database Schema Version";
        public const string DateTimeFormatString = "{0:o}";
        private const string PlanTextKey = "Plan Text";

        public const double DefaultGracePeriodSeconds = 20;
        private const string DeviceNameKey = "Device Name";
        private const string GracePeriodKey = "Event Grace Period (seconds)";
        private const string LastJobKey = "Last Job";
        private const string OfflineModeKey = "Offline Mode";
        private const string ServerKey = "Server";
        private const string MaximumSearchRadiusKey = "Maximum Search Radius (ft)";

        private static readonly string ApplicationServerFilePath;
        private static readonly ILog logger = LogManager.GetLogger( "ApplicationSettings" );

        private static IDictionary<string, string> fileSettings;
        private bool _disposed;

        /// <summary>
        /// Initializes the <see cref="ApplicationSettings"/> class.
        /// </summary>
        static ApplicationSettings( )
        {
            ApplicationServerFilePath = Path.Combine(
                Path.GetDirectoryName( Assembly.GetExecutingAssembly( ).GetName( ).CodeBase ),
                ApplicationServerFileName );
        }

        /// <summary>
        /// Gets or sets the data manager.
        /// </summary>
        /// <value>The data manager.</value>
        public static DataManager DataManager { get; set; }

        public static string DatabasePath
        {
            get
            {
                string path = GetFileSetting( DatabasePathKey );

                bool exists;

                try
                {
                    exists = File.Exists( path );
                }
                catch
                {
                    throw new ApplicationException( Resources.ApplicationException_DBDirectoryInvalid );
                }

                if( !exists )
                    throw new ApplicationException( Resources.ApplicationException_NoDB );

                return path;
            }
            set { SetFileSetting( DatabasePathKey, value ); }
        }

        /// <summary>
        /// Gets or sets the grace period.
        /// </summary>
        /// <value>The grace period.</value>
        public static TimeSpan GracePeriod
        {
            get
            {
                string setting = GetFileSetting( GracePeriodKey );
                double seconds = DefaultGracePeriodSeconds;

                try
                {
                    seconds = Math.Max( double.Parse( setting ), 1 );
                }
                catch
                {
                    if( logger.IsErrorEnabled ) logger.ErrorFormat( "Grace Period had invalid value: {0}", setting );
                }

                return TimeSpan.FromSeconds( seconds );
            }
            set { SetFileSetting( GracePeriodKey, value.TotalSeconds.ToString( ) ); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [offline mode].
        /// </summary>
        /// <value><c>true</c> if [offline mode]; otherwise, <c>false</c>.</value>
        public static bool OfflineMode
        {

            get
            {
                bool offlineMode = false;
                string setting = GetFileSetting( OfflineModeKey, false );

                if( setting == null )
                    return offlineMode;

                try
                {
                    offlineMode = bool.Parse( setting );
                }
                catch
                {
                    if( logger.IsErrorEnabled ) logger.ErrorFormat( "Offline Mode had invalid value: {0}", setting );
                }
                return offlineMode;
            }

            set { SetFileSetting( OfflineModeKey, value.ToString( ) ); }
        }

        /// <summary>
        /// Gets or sets the server.
        /// </summary>
        /// <value>The server.</value>
        private static string m_Server;
        public static string Server
        {
            get
            {
                if ( null == m_Server )
                {
                    m_Server = ConfigurationManager.AppSettings[ "Server" ].ToString( );
                }
                return m_Server;
            }
            //get { return GetFileSetting( ServerKey ); }
            //set { SetFileSetting( ServerKey, value ); }
        }

        /// <summary>
        /// Gets or sets the name of the device.
        /// </summary>
        /// <value>The name of the device.</value>
        private static string m_DeviceName;
        public static string DeviceName
        {
            get
            {
                if ( null == m_DeviceName )
                {
                    m_DeviceName = ConfigurationManager.AppSettings[ "Location" ].ToString( );
                }
                return m_DeviceName;
            }
        //    get { return GetFileSetting( DeviceNameKey ); }
        //    set { SetFileSetting( DeviceNameKey, value ); }
        }

        public static string PlanText
        {
            get
            {
                string setting = GetFileSetting( PlanTextKey );
                if( String.IsNullOrEmpty( setting ) )
                    logger.ErrorFormat( "Plan Text had invalid value: {0}", setting );

                return setting;
            }

            set { SetFileSetting( PlanTextKey, value ); }
        }

        /// <summary>
        /// Gets or sets the active WSM.
        /// </summary>
        /// <value>The active WSM.</value>
        //public static Guid ActiveWSM
        //{
        //    get
        //    {
        //        if( activeWSM == Guid.Empty )
        //        {
        //            string activeWSMStr = ReadSetting( ActiveWSMKey );
        //            if( !String.IsNullOrEmpty( activeWSMStr ) )
        //                activeWSM = new Guid( activeWSMStr );
        //        }

        //        return activeWSM;
        //    }
        //    set
        //    {
        //        if( activeWSM == value )
        //            return;

        //        activeWSM = value;
        //        WriteSetting( ActiveWSMKey, value.ToString( ) );
        //    }
        //}
        //private static Guid activeWSM = Guid.Empty;

        /// <summary>
        /// The last service provider used by this device. Used to look-up service provider when device is offline
        /// </summary>
        public static Guid LastServiceProvider
        {
            get
            {
                string lastServiceProvider = ReadSetting( LastServiceProviderKey );
                if( !String.IsNullOrEmpty( lastServiceProvider ) )
                    return new Guid( lastServiceProvider );

                return Guid.Empty;
            }
            set { WriteSetting( LastServiceProviderKey, value.ToString( ) ); }
        }

        /// <summary>
        /// Gets the database version.
        /// </summary>
        /// <value>The database version.</value>
        public static string DatabaseVersion
        {
            get
            {
                string databaseVersionStr = ReadSetting( DatabaseVersionKey );
                if( !String.IsNullOrEmpty( databaseVersionStr ) )
                    return databaseVersionStr;

                return "0";
            }
        }

        /// <summary>
        /// Gets or sets the active app server ID.
        /// </summary>
        /// <value>The active app server ID.</value>
        //public static Guid ActiveAppServerID
        //{
        //    get
        //    {
        //        string activeAppServer = ReadSetting( AppServerIDKey );
        //        if( !String.IsNullOrEmpty( activeAppServer ) )
        //            return new Guid( activeAppServer );

        //        return Guid.Empty;
        //    }
        //    set { WriteSetting( AppServerIDKey, value.ToString( ) ); }
        //}

        /// <summary>
        /// Gets or sets the current culture.
        /// </summary>
        /// <value>The current culture.</value>
        public static int CurrentCulture
        {
            get
            {
                string _currentCulture = null;

                try
                {
                    _currentCulture = GetFileSetting( CurrentCultureKey );
                }
                catch
                {
                    if( logger.IsErrorEnabled ) logger.ErrorFormat( "Current culture setting had invalid value: {0}", _currentCulture );
                }

                if( _currentCulture != null ) return int.Parse( _currentCulture );

                return 1033;
            }

            set { SetFileSetting( CurrentCultureKey, value.ToString( ) ); }
        }

        /// <summary>
        /// Gets or sets the maximum search radius.
        /// </summary>
        /// <value>The maximum search radius.</value>
        public static double MaximumSearchRadius
        {
            get { return double.Parse( GetFileSetting( MaximumSearchRadiusKey ) ); }
            set { SetFileSetting( MaximumSearchRadiusKey, value.ToString( ) ); }
        }

        /// <summary>
        /// Gets or sets the last job id.
        /// </summary>
        /// <value>The last job id.</value>
        public static string LastJobId
        {
            get
            {
                string lastJob = ReadSetting( LastJobKey );
                if( !String.IsNullOrEmpty( lastJob ) )
                    return lastJob;

                return Guid.Empty.ToString( );
            }
            set { WriteSetting( LastJobKey, value ); }
        }

        /// <summary>
        /// Indicates if the requested file is locked
        /// </summary>
        /// <param name="strFullFileName">Name of the STR full file.</param>
        /// <returns></returns>
        public static bool FileIsLocked( string strFullFileName )
        {
            bool blnReturn = false;
            try
            {
                FileStream fs = File.Open( strFullFileName, FileMode.OpenOrCreate, FileAccess.Read, FileShare.None );
                fs.Close( );
            }
            catch( IOException )
            {
                blnReturn = true;
            }
            return blnReturn;
        }

        /// <summary>
        /// Loads the settings file.
        /// </summary>
        private static void LoadSettingsFile( )
        {
            string fileName = ApplicationServerFilePath.Replace( "file:\\", string.Empty );

            if( FileIsLocked( fileName ) ) throw new IOException( "The ApplicationSettings.txt is current locked by another process" );

            FileStream stream = File.Open( fileName, FileMode.OpenOrCreate, FileAccess.Read, FileShare.None );
            StreamReader reader = new StreamReader( stream );//ApplicationServerFilePath.Replace("file:\\", string.Empty));

            if( logger.IsInfoEnabled ) logger.InfoFormat( "Reading settings from {0}", ApplicationServerFilePath );
            try
            {
                fileSettings = new Dictionary<string, string>( );
                string line;

                int lineNumber = 1;
                while( ( line = reader.ReadLine( ) ) != null )
                {
                    line = line.Trim( );
                    if( ( line != string.Empty ) && !line.StartsWith( "#" ) )
                    {
                        string[ ] parts = line.Split( '=' );
                        if( logger.IsInfoEnabled ) logger.InfoFormat( "Read {0} from line {1}", line, lineNumber );
                        if( parts.Length != 2 )
                        {
                            Exception invalidData = new InvalidDataException( string.Format( "The Application Settings file contains invalid data on line {0} : The line is \"{1}\"", lineNumber, line ) );
                            logger.Fatal( "Invalid data in Application Settings File", invalidData );
                            throw invalidData;
                        }
                        fileSettings.Add( parts[ 0 ].Trim( ).ToUpper( ), parts[ 1 ].Trim( ) );
                        if( logger.IsInfoEnabled ) logger.InfoFormat( "Read Key: {0}, Value:{1} on line {2}", parts[ 0 ].Trim( ), parts[ 1 ].Trim( ), lineNumber );
                    }
                    lineNumber++;
                }
            }
            finally
            {
                reader.Close( );
            }
        }

        /// <summary>
        /// Writes the settings file.
        /// </summary>
        private static void WriteSettingsFile( )
        {
            if( fileSettings == null )
                LoadSettingsFile( );

            // Delete the old file
            if( File.Exists( ApplicationServerFilePath ) )
                File.Delete( ApplicationServerFilePath );

            FileStream stream = File.Open( ApplicationServerFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None );
            StreamWriter writer = new StreamWriter( stream );

            try
            {
                if( fileSettings != null )
                {
                    foreach( KeyValuePair<string, string> setting in fileSettings )
                    {
                        string outputLine = string.Format( "{0} = {1}\n", setting.Key, setting.Value );
                        writer.WriteLine( "{0} = {1}\n", setting.Key, setting.Value );
                        if( logger.IsInfoEnabled ) logger.InfoFormat( "Wrote {0} to {1}", outputLine, ApplicationServerFilePath );
                    }
                }

                writer.Flush( );
            }
            catch( Exception ex )
            {
                logger.Fatal( string.Format( "Failed to write settigns to {0}", ApplicationServerFilePath ), ex );
            }
            finally
            {
                writer.Close( );
                stream.Close( );
            }
        }

        private static string GetFileSetting( string key )
        {
            return GetFileSetting( key, true );
        }

        private static string GetFileSetting( string key, bool requiredSetting )
        {
            if( fileSettings == null )
                LoadSettingsFile( );
            if( fileSettings != null )
            {
                if( fileSettings.ContainsKey( key.ToUpper( ) ) )
                    return fileSettings[ key.ToUpper( ) ];

                if( requiredSetting )
                {
                    string errorMsg = String.Format( "Required Setting {0} was not found", key );
                    if( logger.IsErrorEnabled ) logger.ErrorFormat( errorMsg );
                    throw new Exception( errorMsg );
                }
                else
                    if( logger.IsWarnEnabled ) logger.WarnFormat( "Optional setting {0} was not found", key );
            }
            return null;
        }

        /// <summary>
        /// Sets the file setting.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        private static void SetFileSetting( string key, string value )
        {
            if( logger.IsInfoEnabled ) logger.InfoFormat( "Setting {0} to {1}", key, value );
            if( fileSettings.ContainsKey( key.ToUpper( ) ) )
            {
                if( logger.IsInfoEnabled ) logger.InfoFormat( "{0} exists updating value", key );
                fileSettings[ key.ToUpper( ) ] = value;
            }
            else
            {
                if( logger.IsInfoEnabled ) logger.InfoFormat( "{0} does not exist.  Adding it to settings.", key );
                fileSettings.Add( key.ToUpper( ), value );
            }

            WriteSettingsFile( );
        }

        /// <summary>
        /// Reads the setting.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static string ReadSetting( string key )
        {
            try
            {
                return SettingProperties.Get( DataManager, key );
            }
            catch( Exception ex )
            {
                logger.Error( "Failed to ReadSetting '" + key + "' from database.", ex );
                return String.Empty;
            }
        }

        /// <summary>
        /// Writes the date setting.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public static void WriteDateSetting( string key, DateTime value )
        {
            SettingProperties.Put( DataManager, key, string.Format( DateTimeFormatString, value ) );
        }

        /// <summary>
        /// Writes the setting.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public static void WriteSetting( string key, string value )
        {
            SettingProperties.Put( DataManager, key, value );
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose( )
        {
            Dispose( true );

            // Use SupressFinalize in case a subclass
            // of this type implements a finalizer.
            GC.SuppressFinalize( this );
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose( bool disposing )
        {
            // If you need thread safety, use a lock around these 
            // operations, as well as in your methods that use the resource.
            if( !_disposed )
            {
                // Indicate that the instance has been disposed.
                _disposed = true;
            }
        }

    }
}