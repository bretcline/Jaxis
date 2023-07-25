using System.Collections.Generic;
using System.Windows.Forms;

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

        protected static Dictionary<string,ILog> m_Log = new Dictionary<string, ILog>();
        protected const string DEFAULT = "DEFAULT";

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

        public static T Wrap<T>( string _LoggerName, bool _Throw, Func<T> _Code )
        {
            return Wrap<T>( _LoggerName, string.Empty, LogType.Debug, _Throw, _Code );
        }

        public static T Wrap<T>( string _LogText, LogType _Level, bool _Throw, Func<T> _Code )
        {
            return Wrap<T>( DEFAULT, _LogText, _Level, _Throw, _Code );
        }

        public static T Wrap<T>( string _LoggerName, string _LogText, LogType _Level, bool _Throw, Func<T> _Code )
        {
            T rc = default( T );
            try
            {
                if( !string.IsNullOrEmpty( _LogText ) )
                {
                    Write( _LoggerName, _LogText, _Level );
                }
                rc = _Code( );
            }
            catch( Exception err )
            {
                WriteException( _LoggerName, _LogText, err );
                if( true == _Throw )
                {
                    throw;
                }
            }
            return rc;
        }


        public static T MsgWrap<T>(bool _Throw, Func<T> _Code)
        {
            return MsgWrap<T>(string.Empty, LogType.Debug, _Throw, _Code);
        }

        public static T MsgWrap<T>(string _LoggerName, bool _Throw, Func<T> _Code)
        {
            return MsgWrap<T>(_LoggerName, string.Empty, LogType.Debug, _Throw, _Code);
        }

        public static T MsgWrap<T>(string _LogText, LogType _Level, bool _Throw, Func<T> _Code)
        {
            return MsgWrap<T>(DEFAULT, _LogText, _Level, _Throw, _Code);
        }

        public static T MsgWrap<T>(string _LoggerName, string _LogText, LogType _Level, bool _Throw, Func<T> _Code)
        {
            T rc = default( T );
            try
            {
                if( !string.IsNullOrEmpty( _LogText ) )
                {
                    Write( _LoggerName, _LogText, _Level );
                }
                rc = _Code( );
            }
            catch( Exception err )
            {
                WriteException( _LoggerName, _LogText, err );
                if (true == _Throw)
                {
                    throw;
                }
                else
                {
                    MessageBox.Show(err.Message, "Exception");
                }
            }
            return rc;
        }



        public static void Time( string _LogText, LogType _Level, bool _Throw, Action _Code )
        {
            Time( DEFAULT, _LogText, _Level, _Throw, _Code );
        }

        public static void Time( string _LoggerName, string _LogText, LogType _Level, bool _Throw, Action _Code )
        {
            Stopwatch Timer = new Stopwatch( );
            try
            {
                Timer.Start( );
                _Code( );

            }
            catch( Exception err )
            {
                WriteException( _LoggerName, _LogText, err );
                if( true == _Throw )
                {
                    throw;
                }
            }
            finally
            {
                Timer.Stop( );
                if( !string.IsNullOrEmpty( _LogText ) )
                {
                    Write( _LoggerName, string.Format( "{0} - {1}ms", _LogText, Timer.ElapsedMilliseconds ), _Level );
                }
            }
        }

        public static T Time<T>( string _LogText, LogType _Level, bool _Throw, Func<T> _Code )
        {
            return Time<T>(DEFAULT, _LogText, _Level, _Throw, _Code);
        }

        public static T Time<T>( string _LoggerName, string _LogText, LogType _Level, bool _Throw, Func<T> _Code )
        {
            T rc = default( T );
            Stopwatch Timer = new Stopwatch( );
            try
            {
                Timer.Start( );
                rc = _Code( );

            }
            catch( Exception err )
            {
                WriteException( _LoggerName, _LogText, err );
                if( true == _Throw )
                {
                    throw;
                }
            }
            finally
            {
                Timer.Stop( );
                if( !string.IsNullOrEmpty( _LogText ) )
                {
                    Write( _LoggerName, string.Format( "{0} - {1}ms", _LogText, Timer.ElapsedMilliseconds ), _Level );
                }
            }
            return rc;
        }

        public static ILog GetLogger( )
        {
            return GetLogger( DEFAULT );
        }

        public static ILog GetLogger( string _LoggerName )
        {
            lock (m_Log)
            {
                if (!m_Log.ContainsKey(_LoggerName))
                {
                    m_Log[_LoggerName] = log4net.LogManager.GetLogger(_LoggerName);
                    log4net.Config.XmlConfigurator.Configure();

                    m_Log[_LoggerName].Info(string.Format("Log::GetLogger( {0} ) - Created Logger", _LoggerName));
                }
                return m_Log[_LoggerName];
            }
        }

        public static void Write( byte[] _Message, int _Length, LogType _Level )
        {
            Write(DEFAULT, _Message, _Length, _Level);
        }

        public static void Write( string _LoggerName, byte[] _Message, int _Length, LogType _Level )
        {
            string Message = Convert.ToBase64String( _Message );
            int Index = Message.IndexOf( "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" );
            Message = Message.Substring( 0, Index );

            Write(_LoggerName, Message, _Level );
        }

        public static void Write( string _Message, LogType _Level )
        {
            Write( DEFAULT, _Message, _Level);
        }

        public static void Write(string _LoggerType, string _Message, LogType _Level )
        {
            ILog Logger = GetLogger( _LoggerType );
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

        public static void Fatal( string _LoggerType, string _Message )
        {
            GetLogger( _LoggerType ).Fatal( _Message );
        }

        public static void Fatal( string _Message )
        {
            Fatal( DEFAULT, _Message );
        }

        public static void Error( string _LoggerType, string _Message )
        {
            GetLogger( _LoggerType ).Error( _Message );
        }
        public static void Error( string _Message )
        {
            Error( DEFAULT, _Message );
        }

        public static void Warn( string _LoggerType, string _Message )
        {
            GetLogger( _LoggerType ).Warn( _Message );
        }

        public static void Warn( string _Message )
        {
            Warn( DEFAULT, _Message );
        }

        public static void Info( string _LoggerType, string _Message )
        {
            GetLogger( _LoggerType ).Info( _Message );
        }

        public static void Info( string _Message )
        {
            Info( DEFAULT, _Message );
        }

        public static void Debug( string _LoggerType, string _Message )
        {
            GetLogger( _LoggerType ).Debug( _Message );
        }

        public static void Debug( string _Message )
        {
            Debug( DEFAULT, _Message );
        }

        public static void Exception( string _LoggerType, Exception _Error )
        {
            WriteException( DEFAULT, "Error", _Error );
        }

        public static void Exception( Exception _Error )
        {
            Exception( DEFAULT, _Error );
        }

        public static void WriteException( string _LoggerType, string _Message, Exception _Error )
        {
            ILog Logger = GetLogger( _LoggerType );
            Logger.Error( _Message, _Error );
        }

        public static void WriteException( string _Message, Exception _Error )
        {
            WriteException( DEFAULT, _Message, _Error );
        }

        #endregion Methods
    }
}