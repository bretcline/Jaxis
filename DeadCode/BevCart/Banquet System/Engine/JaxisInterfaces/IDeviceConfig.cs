using System;
using System.Collections.Generic;

namespace JaxisInterfaces
{
    public interface IDeviceConfig
    {
        string ID { get; set; }
        string Name { get; set; }
        DeviceType Type { get; set; }
        MessageType ConsumerMessageType { get; set; }
        List<string> Options { get; set; }
    }
}
