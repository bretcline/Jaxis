using System;
using System.Reflection;
using Jaxis.Engine.Base.Device;
using Jaxis.Interfaces;
using Jaxis.Util.Log4Net;

namespace Jaxis.Engine.Device
{
    public class TestMessageConsumer : BaseDevice, IConsumer
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
            rc.Type = DeviceType.DataConsumer;
            rc.State = DeviceState.Stopped;
            rc.ProducerMessageType = 0;
            rc.ConsumerMessageType = 1;
            return rc;
        }

        public TestMessageConsumer()
            : this(GetDefaultDeviceConfig())
        {
        }

        public TestMessageConsumer(IDeviceConfig _config)
            : base(_config)
        {
            //Config.ConsumerMessageType = MessageType.RawData;
            Config.Type = DeviceType.DataConsumer;
            Config.State = DeviceState.Stopped;
            State = DeviceState.Stopped;
        }

        override public void Start()
        {
            try
            {
                Log.Debug(string.Format("Start Message Consumer"));

                if (null != m_DeviceConfig)
                {
                    State = DeviceState.Started;
                    Config.State = DeviceState.Started;
                    m_Stop = false;
                    //m_Worker = new System.Threading.Thread(SimulateData);
                    //m_Worker.Start();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException("TestMessageConsumer::Start", exp);
            }
        }

        override public void Stop()
        {
            Config.State = DeviceState.Stopped;
        }

        public override string Consume(IMessage _message)
        {
            if (_message is GeneratedMessage)
            {
                var message = _message as GeneratedMessage;

                Log.Debug( string.Format( "{0} - {1}, {2} - {3}", message.TestMessage, message.TestTime, message.Type, message.ReadTime ) );

                System.Threading.Thread.Sleep(20);

            }
            return "true";
        }
    }
}