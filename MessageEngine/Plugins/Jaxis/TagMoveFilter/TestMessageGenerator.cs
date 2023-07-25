using System;
using System.Reflection;
using Jaxis.Engine.Base.Device;
using Jaxis.Interfaces;
using Jaxis.MessageLibrary;
using Jaxis.Util.Log4Net;

namespace Jaxis.Engine.Device
{

    public class GeneratedMessage : BaseMessage
    {
        public string TestMessage { get; set; }
        public DateTime TestTime { get; set; }
    }

    public class TestMessageGenerator : BaseProducerDevice, IProducer
    {
        private System.Threading.Thread m_Worker;


        [Obfuscation(Exclude = true)]
        public static DeviceConfig GetDefaultDeviceConfig()
        {
            var rc = new DeviceConfig();
            var Asm = System.Reflection.Assembly.GetExecutingAssembly();
            rc.AssemblyName = Asm.ManifestModule.Name;
            rc.AssemblyType = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName;
            rc.AssemblyVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(BaseDevice.GetPath() + "\\" + rc.AssemblyName).FileVersion;
            rc.ID = Guid.NewGuid().ToString();
            rc.Name = "Test Message Generator Device";
            rc.Type = DeviceType.DataProducer;
            rc.State = DeviceState.Stopped;
            rc.ProducerMessageType = 1;
            rc.ConsumerMessageType = 0;
            return rc;
        }

        public TestMessageGenerator()
            : this(GetDefaultDeviceConfig())
        {
        }

        public TestMessageGenerator(IDeviceConfig _config)
            : base(_config)
        {
            //Config.ConsumerMessageType = MessageType.RawData;
            Config.Type = DeviceType.DataProducer;
            Config.State = DeviceState.Stopped;
            State = DeviceState.Stopped;
        }

        override public void Start()
        {
            try
            {
                Log.Debug(string.Format("Start Message Generator"));

                if (null != m_DeviceConfig)
                {
                    State = DeviceState.Started;
                    Config.State = DeviceState.Started;
                    m_Stop = false;
                    m_Worker = new System.Threading.Thread(SimulateData);
                    m_Worker.Start();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException("TestMessageGenerator::Start", exp);
            }
        }

        override public void Stop()
        {
            Config.State = DeviceState.Stopped;
        }

        private void SimulateData()
        {
            var rnd = new Random();
            int interval = rnd.Next(500, 5000);
            int id = rnd.Next(0, 9);
            int count = 0;
            while (!m_Stop)
            {
                var msg = new GeneratedMessage
                {
                    TestMessage = string.Format("{1} - Message #{0}", count++, id),
                    ReadTime = DateTime.Now,
                    TestTime = DateTime.Now
                };
                ProduceMessage(msg);

                System.Threading.Thread.Sleep(25);

                if (0 == count%interval)
                {
                    System.Threading.Thread.Sleep(60 * 1000);

                    if (count > Int16.MaxValue)
                    {
                        count = 0;
                    }
                }
            }
        }
    }
}