using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JaxisEngine.Base;
using JaxisInterfaces;

namespace Jaxis.BucketPlugin
{
    public class PourBucketConsumer :  BaseDevice, IConsumer
    {
        //public DeviceState State { get; set; }

        public event ProduceHandler Produce;

        public PourBucketConsumer(IDeviceConfig _Config)
            : base( _Config )
        {
            Config.ConsumerMessageType = MessageType.RawData;
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
