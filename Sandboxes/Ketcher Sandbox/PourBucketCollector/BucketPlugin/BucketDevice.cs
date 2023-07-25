using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JaxisEngine.Base;
using JaxisInterfaces;
using BucketCollectionLib;

namespace Jaxis.BucketPlugin
{
    public class BucketDevice : BaseProducerDevice
    {
        private System.Threading.Thread m_Worker;
        public event ProduceHandler Produce;

        BucketProcessor m_P;

        public BucketDevice(IDeviceConfig _Config) : base(_Config)
        {
            Config.ConsumerMessageType = MessageType.None;
            Config.Type = DeviceType.DataProducer;
            State = DeviceState.Stopped;
        }

        public override void Start()
        {
            State = DeviceState.Started;
            m_Stop = false;
            m_Worker = new System.Threading.Thread(StartThread);
            m_Worker.Start();
        }

        public void StartThread()
        {
            m_P = new BucketProcessor();
            m_P.MessageDelegate += Produce;
            m_Stop = false;
            m_P.OpenComPort();

            while (false == m_Stop)
            {
                System.Threading.Thread.Sleep(250);
            }
        }

        public void ProcessMyMessage(PourMessage _Message)
        {
            Produce(_Message);
        }

        public override void Stop()
        {
            m_Stop = true;
            m_P.CloseComPort();
        }
    }
}
