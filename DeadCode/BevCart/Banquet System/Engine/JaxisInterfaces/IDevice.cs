using System;

namespace JaxisInterfaces
{
    public enum DeviceType
    {
        DataProducer = 0,
        DataConsumer = 1,
        DataProducerConsumer = 2,
    }

    public enum DeviceState
    {
        Started = 0,
        Stopped = 1
    }

    public delegate string ProduceHandler(IMessage _Message);
    public interface IDevice
    {

        event ProduceHandler Produce;
        string Consume( IMessage _Message );

        DeviceState State { get; set; }
        IDeviceConfig Config { get; }

        void Start();
        void Stop();
    }
}