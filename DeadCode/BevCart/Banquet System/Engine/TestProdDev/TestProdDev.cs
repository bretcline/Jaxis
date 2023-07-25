using System;
using System.Collections.Generic;
using JaxisInterfaces;
using JaxisEngine;
using JaxisEngine.Base;

namespace TestProdDev
{
    public class BevProdDev : BaseDevice, IDevice
    {

        private System.Threading.Thread m_Worker;
        public event ProduceHandler Produce;

        public BevProdDev( IDeviceConfig _Config ) : base( _Config )
        {
            Config.ConsumerMessageType = MessageType.None;
            Config.Type = DeviceType.DataProducer;
            State = DeviceState.Stopped;
        }

        override public void Start()
        {
            State = DeviceState.Started;
            m_Stop = false;
            m_Worker = new System.Threading.Thread(StartThread);
            m_Worker.Start();
        }

        override public void Stop( )
        {
            State = DeviceState.Stopped;
            m_Stop = true;
        }

        public void StartThread()
        {
            Random R = new Random(DateTime.Now.Millisecond);
            while (!m_Stop)
            {
                BevMessage Message = new BevMessage { DeviceName = Config.Name, DeviceID = Config.ID };
                System.Threading.Thread.Sleep(R.Next(2000, 10000));
                Message.Pour.Duration = TimeSpan.FromSeconds (R.Next(3,6));
                Message.Tag = "Tag";
                Message.ReadTime = DateTime.Now;
                Message.Type = MessageType.RawData;
                if (null != Produce)
                {
                    Produce(Message);
                }
            }
        }
    }
}
