using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using Jaxis.Engine.Base.Device;
using Jaxis.Interfaces;
using Jaxis.Util.Log4Net;

namespace Jaxis.BeverageManagement.Plugin
{
    public class BeverageManagementDataService : BaseProducerDevice
    {
        private ServiceHost m_Service = null;

        [Obfuscation(Exclude = true)]
        public static DeviceConfig GetDefaultDeviceConfig()
        {
            var rc = new DeviceConfig();
            Assembly asm = Assembly.GetExecutingAssembly();
            rc.AssemblyType = MethodBase.GetCurrentMethod().DeclaringType.FullName;
            rc.AssemblyName = asm.ManifestModule.Name;
            rc.AssemblyVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(BaseDevice.GetPath() + "\\" + rc.AssemblyName).FileVersion;
            rc.ID = Guid.NewGuid().ToString();
            rc.Name = "Beverage Management Data Service";
            rc.Type = DeviceType.DataProducer;
            rc.State = DeviceState.Stopped;
            rc.ProducerMessageType = 0;
            rc.ConsumerMessageType = 0;
            var option1 = new DeviceConfigOption { Name = "Read Window", Value = "1.5" };
            rc.Options.Add(option1);
            return rc;
        }
                
        public BeverageManagementDataService( )
            : this(GetDefaultDeviceConfig())
        {
        }

        public BeverageManagementDataService(IDeviceConfig _config)
            : base(_config)
        {
            
        }

        public override void Start()
        {
            if (m_Service != null)
            {
                m_Service.Close();
            }
            try
            {
                m_Service = new ServiceHost(typeof(BeverageManagementAPI));
                m_Service.Open();
            }
            catch (Exception exp)
            {
                Log.WriteException("Engine::BeverageManagementAPI", exp);
            }
        }

        public override void Stop()
        {
            if (m_Service != null)
            {
                m_Service.Close();
                m_Service = null;
            }
        }
    }
}
