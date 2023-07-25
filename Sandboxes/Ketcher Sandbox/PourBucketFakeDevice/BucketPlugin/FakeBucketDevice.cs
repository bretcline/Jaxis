using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JaxisEngine.Base;
using JaxisInterfaces;
using BucketCollectionLib;

namespace Jaxis.BucketPlugin
{
    public class FakeBucketDevice : BaseProducerDevice, IProducer
    {
        private System.Threading.Thread m_Worker;

        BucketProcessor m_P;

        public FakeBucketDevice(IDeviceConfig _Config)
            : base(_Config)
        {
            Config.ConsumerMessageType = MessageType.RawData;
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
            //m_P = new BucketProcessor();
            //m_P.MessageDelegate += Produce;
            m_Stop = false;

            Random Rnd = new Random();
            while (false == m_Stop)
            {
                PourMessage Message = new PourMessage();
                Message.ReadTime = DateTime.Now;
                Dictionary<AngleBucket, double> SampleBucketData = new Dictionary<AngleBucket, double>();
                Random RandomTimeGenerator = new Random();

                foreach (AngleBucket AB in Enum.GetValues(typeof(AngleBucket)))
                {
                    SampleBucketData[AB] = RandomTimeGenerator.Next(0, 60001);
                }

                Message.Buckets = SampleBucketData;

                Message.PourStop = DateTime.Now;

                ProduceMessage(Message);
                //Produce(Message);
                
                System.Threading.Thread.Sleep(Rnd.Next( 1000, 8000 ) );
            }
        }

        public void ProcessMyMessage(PourMessage _Message)
        {
            //Produce(_Message);
        }

        public override void Stop()
        {
            m_Stop = true;
            //m_P.CloseComPort();
        }
    }
}
