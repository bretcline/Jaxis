using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Linq;
using System.Reflection;
using System.Text;
using Jaxis.AlienRFID.MessageLibrary;
using Jaxis.Engine.Base.Device;
using Jaxis.Interfaces;
using Jaxis.Util.Log4Net;

namespace DBValidation
{
    public class DBValidator : BaseProducerDevice, IProducer, IConsumer
    {
        [Obfuscation(Exclude = true)]
        public static DeviceConfig GetDefaultDeviceConfig()
        {
            DeviceConfig rc = new DeviceConfig();
            System.Reflection.Assembly Asm = System.Reflection.Assembly.GetExecutingAssembly();
            rc.AssemblyName = Asm.ManifestModule.Name;
            rc.AssemblyType = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName;
            rc.AssemblyVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(BaseDevice.GetPath() + "\\" + rc.AssemblyName).FileVersion;
            rc.ID = Guid.NewGuid().ToString();
            rc.Name = "DBValidator";
            rc.Type = DeviceType.DataProducer;
            rc.State = DeviceState.Stopped;
            rc.ProducerMessageType = 4;
            rc.ConsumerMessageType = 0;
            return rc;
        }

        Dictionary<string, AlienMessages> m_Tags = new Dictionary<string, AlienMessages>( );
        Queue<string> m_KeysToRemove = new Queue<string>( );

        
        public DBValidator( )
            : this(GetDefaultDeviceConfig())
        {
        }

        public DBValidator( IDeviceConfig _Config )
            : base( _Config )
        {
            //Config.ConsumerMessageType = MessageType.DBData;
            Config.Type = DeviceType.DataProducer | DeviceType.DataConsumer;
            State = DeviceState.Stopped;
        }

        public override string Consume( IMessage _message )
        {
            return Log.Wrap<string>( "DBValidator::Consume", LogType.Debug, true, ( ) =>
            {
                string rc = null;

                DataMessage Message = _message as DataMessage;

                try
                {
                    Validate( Message );
                }
                catch( Exception err )
                {
                    Log.WriteException( string.Format( "On Read: {0}", Message.TagID ), err );
                }
                return rc;
            } );
        }

        override public void Start( )
        {
            Log.Wrap<int>( "DBValidator::Start", LogType.Debug, true, ( ) =>
            {
                State = DeviceState.Started;
                return 1;
            } );
        }

        override public void Stop( )
        {
            Log.Wrap<int>( "DBValidator::Stop", LogType.Debug, true, ( ) =>
            {
                State = DeviceState.Stopped;
                return 1;
            } );
        }

        public ValidationResults Validate( DataMessage _Key )
        {
            return Log.Wrap<ValidationResults>( "DBValidator::Validate", LogType.Debug, true, ( ) =>
            {
                ValidationResults rc = new ValidationResults( );
                if( null != _Key )
                {
                    // Open the same connection with the same connection string.
                    string ConnString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
                    using( SqlCeConnection con = new SqlCeConnection( ConnString ) )
                    {
                        con.Open( );
                        // Read in all values in the table.
                        using( SqlCeCommand com = new SqlCeCommand( string.Format( "SELECT * FROM CustomerInfo WHERE TagID = '{0}'", _Key.TagID ), con ) )
                        {
                            SqlCeDataReader Reader = com.ExecuteReader( );
                            if( Reader.Read( ) )
                            {
                                Guid CustomerID = Reader.GetGuid( Reader.GetOrdinal( "CustomerID" ) );
                                rc.IsValid = true;
                                Entity E = new Entity( );
                                rc.IsCurrent = Convert.ToBoolean( Reader.GetInt32( Reader.GetOrdinal( "Active" ) ) );
                                E.Name = Reader.GetString( Reader.GetOrdinal( "Name" ) );
                                E.Address = Reader.GetString( Reader.GetOrdinal( "Address" ) );
                                E.City = Reader.GetString( Reader.GetOrdinal( "City" ) );
                                E.State = Reader.GetString( Reader.GetOrdinal( "State" ) );
                                E.Zip = Reader.GetString( Reader.GetOrdinal( "Zip" ) );
                                rc.HTMLOutput = E.HTMLOutput;
                                rc.Name = E.Name;

                                com.CommandText = string.Format( "INSERT INTO PickupData SELECT NEWID( ), '{0}', GETDATE( )", CustomerID );
                                int Rows = com.ExecuteNonQuery( );
                                if( 0 == Rows )
                                {
                                    rc.Results = "Update Failed";
                                }
                            }
                            else
                            {
                                rc.Results = string.Format( "Unknown Account for {0}", _Key.TagID );
                                rc.IsValid = false;
                            }
                            ProduceMessage( rc );
                        }
                    }
                }
                return rc;
            } );
        }
    }
}