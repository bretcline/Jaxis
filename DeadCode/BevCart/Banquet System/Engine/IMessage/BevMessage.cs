using System;
using BevClasses;
using JaxisInterfaces;

namespace JaxisEngine
{
    public class BevMessage : IMessage
    {
        public MessageType Type {get; set;}
        public string DeviceName { get; set; }
        public string DeviceID { get; set; }
        public string Tag { get; set; }
        public Pour Pour {get; set;}
        public DateTime ReadTime { get; set; }

        public BevMessage()
        {
            Pour = new Pour();
        }
    }
}
