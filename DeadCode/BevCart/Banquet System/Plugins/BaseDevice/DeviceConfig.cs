using System;
using System.Collections.Generic;
using JaxisInterfaces;

namespace JaxisEngine.Base
{
    [Serializable]
    public class DeviceConfig : IDeviceConfig
    {
        public string AssemblyName { get; set; }
        public string AssemblyType { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public DeviceType Type { get; set; }
        public MessageType ConsumerMessageType { get; set; }
        public List<string> Options { get; set; }
    }

    [Serializable]
    public class DeviceConfigCollection
    {
        public DeviceConfigCollection()
        {
            Configs = new List<DeviceConfig>();
        }
        public List<DeviceConfig> Configs { get; set; }
    }
}
