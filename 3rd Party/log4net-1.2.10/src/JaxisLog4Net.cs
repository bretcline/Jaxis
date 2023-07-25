namespace Jaxis.Util.Log4Net
{
    using System;
    using System.Diagnostics;

    using log4net;

    #region Enumerations

    public enum LogType
    {
        Debug,
        Information,
        Warning,
        Error,
        Fatal,
    }

    #endregion Enumerations

    public class ExceptionHandler
    {
        #region Methods

        public static void Handle( Exception ex, string Where )
        {
            string Message = "UNHANDLED EXCEPTION: " + ex.Message + " at " + Where;
            Log.WriteException( Message, ex );

            EventLog ELog = new EventLog( "", ".", "EdgeBase" );
            ELog.WriteEntry( Message, EventLogEntryType.Error );
        }

        #endregion Methods
    }

    /// <summary>
    /// Summary description for Log.
    /// </summary>
    public class Log
    {
        #region Fields

        protected static ILog m_Log = null;

        #endregion Fields

        #region Constructors

        protected Log( )
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #endregion Constructors

        #region Methods

        public static T Wrap<T>(bool _Throw, Func<T> _Code)
        {
            return Wrap<T>(string.Empty, LogType.Debug, _Throw, _Code);
        }

        public static T Wrap<T>(string _LogText, LogType _Level, bool _Throw, Func<T> _Code )
        {
            T rc = default( T );
            try
            {
                if (!string.IsNullOrEmpty(_LogText))
                {
                    Write(_LogText, _Level);
                }
                rc = _Code();
            }
            catch (Exception err)
            {
                WriteException(_LogText, err);
                if (true == _Throw)
                {
                    throw;
                }
            }
            return rc;
        }

        public static ILog GetLogger( )
        {
            if( null == m_Log )
            {
                m_Log = log4net.LogManager.GetLogger( System.Reflection.MethodBase.GetCurrentMethod( ).DeclaringType );
                log4net.Config.XmlConfigurator.Configure( );

                m_Log.Info( "Log::GetLogger( ) - Created Logger" );
            }
            return m_Log;
        }

        public static void Write( byte[] _Message, int _Length, LogType _Level )
        {
            string Message = Convert.ToBase64String( _Message );
            int Index = Message.IndexOf( "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" );
            Message = Message.Substring( 0, Index );
            ILog Logger = GetLogger( );
            Write( Message, _Level );
        }

        public static void Write( string _Message, LogType _Level )
        {
            ILog Logger = GetLogger( );
            switch( _Level )
            {
                case LogType.Fatal:
                {
                    Logger.Fatal( _Message );
                    break;
                }
                case LogType.Error:
                {
                    Logger.Error( _Message );
                    break;
                }
                case LogType.Warning:
                {
                    Logger.Warn( _Message );
                    break;
                }
                case LogType.Information:
                {
                    Logger.Info( _Message );
                    break;
                }
                case LogType.Debug:
                {
                    Logger.Debug( _Message );
                    break;
                }
                default:
                {
                    Logger.Debug( _Message );
                    break;
                }
            }
        }

        public static void Fatal( string _Message )
        {
            GetLogger( ).Fatal( _Message );
        }

        public static void Error( string _Message )
        {
            GetLogger( ).Error( _Message );
        }

        public static void Warn( string _Message )
        {
            GetLogger( ).Warn( _Message );
        }

        public static void Info( string _Message )
        {
            GetLogger( ).Info( _Message );
        }

        public static void Debug( string _Message )
        {
            GetLogger( ).Debug( _Message );
        }

        public static void Exception( Exception _Error )
        {
            WriteException( "Error", _Error );
        }

        public static void WriteException( string _Message, Exception _Error )
        {
            ILog Logger = GetLogger( );
            Logger.Error( _Message, _Error );
        }

        #endregion Methods
    }
}