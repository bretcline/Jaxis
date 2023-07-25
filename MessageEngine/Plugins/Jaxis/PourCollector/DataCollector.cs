using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Jaxis.Engine.Base.Device;
using Jaxis.Interfaces;
using Jaxis.Inventory.Data;
using Jaxis.MessageLibrary;
using Jaxis.Util.Log4Net;
using JaxisExtensions;
using JaxisMath;
using Jaxis.Interfaces.Tags;

namespace Jaxis.BeverageManagement.Plugin.PourCollector
{
    /*
        <DeviceConfig>
            <AssemblyName>Jaxis.BeverageManagement.Plugin.DataCollector.dll</AssemblyName>
            <AssemblyType>Jaxis.BeverageManagement.Plugin.PourCollector.DataCollector</AssemblyType>
            <AssemblyVersion>1.0</AssemblyVersion>
            <ID>101</ID>
            <Name>Jaxis Consumer</Name>
            <Type>DataConsumer</Type>
            <State>Started</State>
            <ProducerMessageType>0</ProducerMessageType>
            <ConsumerMessageType>1</ConsumerMessageType>
            <Options>
                <string>http://localhost:8223/HostWCFService/PourEngineService/</string>
                <string>10</string>
                <string></string>
                <string>TEMP Event</string>
                <string>false</string>
            </Options>
        </DeviceConfig>

    */


    class DataCollector : BaseDevice, IConsumer
    {
        private frmDataCollector m_CollectionForm = null;

                
        public DataCollector( )
            : this(GetDefaultDeviceConfig())
        {
        }


        public DataCollector( IDeviceConfig _config )
            : base( _config )
        {
        }

        [Obfuscation(Exclude = true)]
        public static DeviceConfig GetDefaultDeviceConfig()
        {
            var rc = new DeviceConfig();
            Assembly asm = Assembly.GetExecutingAssembly();
            rc.AssemblyType = MethodBase.GetCurrentMethod().DeclaringType.FullName;
            rc.AssemblyName = asm.ManifestModule.Name;
            rc.AssemblyVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(BaseDevice.GetPath() + "\\" + rc.AssemblyName).FileVersion;
            rc.ID = Guid.NewGuid().ToString();
            rc.Name = "Jaxis Data Collector";
            rc.Type = DeviceType.DataProducerConsumer;
            rc.State = DeviceState.Stopped;
            rc.ProducerMessageType = 0;
            rc.ConsumerMessageType = 1;
            return rc;
        }

        public override void Start( )
        {
            try
            {
                State = DeviceState.Started;
                Config.State = DeviceState.Started;

                System.Windows.Forms.Application.EnableVisualStyles( );
                System.Windows.Forms.Application.SetCompatibleTextRenderingDefault( false );
                System.Windows.Forms.Application.Run( m_CollectionForm = new frmDataCollector( ) );
            }
            catch( Exception exp )
            {
                Log.WriteException( "DataCollector::Start", exp );
            }
        }

        public override void Stop( )
        {
            State = DeviceState.Stopped;
            Config.State = DeviceState.Stopped;
        }

        public override string Consume( IMessage _message )
        {
            string rc = null;

            try
            {
                var Msg = _message as IdentecPour;
                if( null != Msg )
                {
                    m_CollectionForm.ReceivePour( Msg );
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "DataCollector :: DataCollector::Consume", exp );
            }
            return rc;
        }
    }
}