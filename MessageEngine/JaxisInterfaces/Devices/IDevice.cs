using System;
using System.Collections.Generic;

namespace Jaxis.Interfaces
{
    [Flags]
    public enum DeviceType
    {
        DataProducer = 0x0001,
        DataConsumer = 0x0002,
        DataProducerConsumer = 0x0003,
    }

    [Flags]
    public enum FilterType
    {
        Inbound = 0x0001,
        Outbound = 0x0002,
        InOut = 0x0003,
    }

    //public enum DeviceState
    //{
    //    Started = 0,
    //    Stopped = 1
    //}

    public interface IDevice : IDisposable
    {
        //DeviceState State { get; set; }
        string HardwareID { get; }

        IDeviceConfig Config { get; }

        List<IFilter> Filters { get; }

        DeviceType Type { get; set; }

        DeviceState InitalState { get; }

        void Start( );

        void Stop( );
    }
}