using System;
using JaxisInterfaces;
using JaxisEngine.Base;

namespace BevCart
{
    class UIDataConsumer : BaseDevice, IDevice
    {
        //public DeviceState State { get; set; }

        public event ProduceHandler Produce;

        public UIDataConsumer( IDeviceConfig _Config )
            : base( _Config )
        {
            Config.ConsumerMessageType = MessageType.DBData;
            Config.Type = DeviceType.DataConsumer;
            State = DeviceState.Stopped;
        }

        public override void Start()
        {
            State = DeviceState.Started;
        }

        public override void Stop()
        {
            State = DeviceState.Stopped;
            // may want to tell UI engine device was stopped
        }

        public override string Consume(IMessage _Message)
        {
            string rc = null;

            Produce(_Message); // Using the IDevice produce event to get data to Form...

            return rc;
        }
    }
}
