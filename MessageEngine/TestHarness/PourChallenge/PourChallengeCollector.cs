using System;
using System.Linq;
using System.Reflection;
using Jaxis.Engine.Base.Device;
using Jaxis.Interfaces;
using Jaxis.Inventory.Data;
using Jaxis.Util.Log4Net;

namespace PourChallenge {
    class PourChallengeCollector : BaseDevice, IConsumer
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
            rc.Name = "BevMet Pour Challenge";
            rc.Type = DeviceType.DataConsumer;
            rc.State = DeviceState.Stopped;
            rc.ProducerMessageType = 0;
            rc.ConsumerMessageType = 32;
            //rc.Options.Add(new DeviceConfigOption
            //{
            //    Name = "Config Name",
            //    Value = "BeverageManager"
            //});
            //rc.Options.Add(new DeviceConfigOption
            //{
            //    Name = "WCF Path",
            //    Value = "http://localhost:8223/HostWCFService/PourEngineService/"
            //});
            return rc;
        }

        private DateTime m_SkipTime = DateTime.Now;
        private TimeSpan m_SkipInterval = new TimeSpan(0, 0, 0, 30);

        protected System.Threading.Thread m_Worker = null;
        protected double m_Timeout = 0;
        protected TimeSpan m_ReadWindow;

        protected readonly string LOG_TYPE;

        public Action<Pour> ProcessPour { get; set; }

        public PourChallengeCollector()
            : this(GetDefaultDeviceConfig())
        {
        }


        public PourChallengeCollector(IDeviceConfig _config)
            : base(_config)
        {
            try
            {
                LOG_TYPE = this.GetType().Name;
                State = Config.State;
            }
            catch (Exception exp)
            {
                Log.WriteException(LOG_TYPE, "PourChallengeCollector::PourChallengeCollector", exp);
            }
        }

        override public void Start()
        {
            try
            {
                State = DeviceState.Started;
                Config.State = DeviceState.Started;
                m_Stop = false;
            }
            catch (Exception exp)
            {
                Log.WriteException(LOG_TYPE, "PourChallengeCollector:: Start", exp);
            }
        }

        override public void Stop()
        {
            try
            {
                State = DeviceState.Stopped;
                Config.State = DeviceState.Stopped;
            }
            catch (Exception exp)
            {
                Log.WriteException(LOG_TYPE, "PourChallengeCollector:: Stop", exp);
            }
            finally
            {
                m_Stop = true;
            }
        }

        public override string Consume(IMessage _message)
        {
            string rc = null;
            try
            {
                if (_message is ActivityLog)
                {
                }
                else if (_message is TagAlert)
                {
                }
                else if (_message is TagActivity)
                {
                }
                else if (_message is Pour)
                {
                    ProcessPour(_message as Pour);
                }
                else if (_message is DeviceAlert)
                {
                }

            }
            catch (Exception exp)
            {
                Log.WriteException(LOG_TYPE, "PourChallengeCollector::Consume", exp);
            }
            return rc;
        }
    }
}