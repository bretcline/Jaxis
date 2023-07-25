using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.ServiceModel;
using Jaxis.Util.Log4Net;
using Jaxis.Engine.Base.Device;
using Jaxis.Interfaces;
using System.ServiceModel.Channels;

namespace Jaxis.Reader.POS
{
    class POSClientPlugin : BaseProducerDevice, IProducer
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
            rc.Name = "POSClientPlugin";
            rc.Type = DeviceType.DataProducer;
            rc.State = DeviceState.Stopped;
            rc.ProducerMessageType = 8;
            rc.ConsumerMessageType = 0;
            return rc;
        }

        protected ServiceHost m_Host = null;
                
        public POSClientPlugin( )
            : this(GetDefaultDeviceConfig())
        {
        }


        public POSClientPlugin( IDeviceConfig _Config )
            : base( _Config )
        {
            TicketPublisher Publisher = TicketPublisher.GetPublisher( );
            Publisher.Publish = this.ProduceMessage;
        }

        public override void Start( )
        {
            if( m_Host != null )
            {
                m_Host.Close( );
            }
            try
            {
                //Uri tcpBaseAddress = new Uri("http://localhost:8765/POSClientInterface/");
 
                //m_Host = new ServiceHost( typeof( POSClientInterface ), tcpBaseAddress );
                
                //WSHttpBinding HTTPBinding = new WSHttpBinding( );
                //HTTPBinding.Security.Mode = SecurityMode.Message;
                //HTTPBinding.MessageEncoding = WSMessageEncoding.Text;
                //m_Host.AddServiceEndpoint( typeof( IPOSClientInterface ), HTTPBinding, "" );
                //m_Host.AddServiceEndpoint( typeof( IPOSClientInterface ), HTTPBinding, "Test" );

                m_Host = new CustomServiceHost( "", typeof( POSClientInterface ) );
                m_Host.Open( );
            }
            catch( Exception err )
            {
                Log.WriteException( "POSClientPlugin.StartWCFService()", err );
            }
        }

        public override void Stop( )
        {
            try
            {
                if( m_Host != null )
                {
                    m_Host.Close( );
                    m_Host = null;
                }
            }
            catch( Exception err )
            {
                Log.WriteException( "POSClientPlugin.StopWCFService()", err );
            }
        }
    }

    public class CustomServiceHost : ServiceHost
    {
        public CustomServiceHost( string _CustomConfigPath, Type _ServiceType, params Uri[] _BaseAddresses )
            : base( )
        {
            this.CustomConfigPath = _CustomConfigPath;
            InitializeDescription( _ServiceType, new UriSchemeKeyedCollection( _BaseAddresses ) );
        }

        public string CustomConfigPath { get; private set; }

        protected override void ApplyConfiguration( )
        {
            if( string.IsNullOrEmpty( CustomConfigPath ) || !System.IO.File.Exists( CustomConfigPath ) )
            {
                base.ApplyConfiguration( );
            }
            else
            {
                LoadConfigFromCustomLocation( CustomConfigPath );
            }
        }

        private void LoadConfigFromCustomLocation( string configFilename )
        {
            var filemap = new System.Configuration.ExeConfigurationFileMap( );

            filemap.ExeConfigFilename = configFilename;

            System.Configuration.Configuration config = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration( filemap, System.Configuration.ConfigurationUserLevel.None );

            var serviceModel = System.ServiceModel.Configuration.ServiceModelSectionGroup.GetSectionGroup( config );

            bool loaded = false;
            foreach( System.ServiceModel.Configuration.ServiceElement se in serviceModel.Services.Services )
            {
                if( !loaded )
                {
                    if( se.Name == this.Description.ConfigurationName )
                    {
                        base.LoadConfigurationSection( se );
                        loaded = true;
                        break;
                    }
                }
            }
            if( !loaded )
            {
                throw new ArgumentException( "ServiceElement doesn't exist" );
            }
        }
    }
}
