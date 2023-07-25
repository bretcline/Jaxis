using System;
using Jaxis.Interfaces;
using JaxisEngine.Base.Device;

namespace Jaxis.Engine.Base
{
    class MessageSniffer : BaseDevice, IDevice, IConsumer
    {
        public Func<IMessage, string> DisplayData;

        public MessageSniffer( IDeviceConfig _Config )
            : base( _Config )
        {
            Config.ConsumerMessageType = MessageType.All;
            Config.Type = DeviceType.DataConsumer;
            State = DeviceState.Stopped;
        }

        public override void Start( )
        {
            State = DeviceState.Started;
        }

        public override void Stop( )
        {
            State = DeviceState.Stopped;
            // may want to tell UI engine device was stopped
        }

        public override string Consume( IMessage _Message )
        {
            string rc = null;

            rc = DisplayData( _Message ); // Using the IDevice produce event to get data to Form...

            return rc;
        }

        #region IDevice Members

        public string HardwareID { get; set; }

        #endregion IDevice Members
    }
}
