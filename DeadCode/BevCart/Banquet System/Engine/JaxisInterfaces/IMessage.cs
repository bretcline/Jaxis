using System;

namespace JaxisInterfaces
{
    [Flags]
    public enum MessageType
    {
        None = 0x0000,
        RawData = 0x0001,
        DBData = 0x0002,
    }

    public interface IMessage
    {
        MessageType Type {get; set;}
        string DeviceName { get; set; }
        string DeviceID { get; set; }
        string Tag { get; set; }
        DateTime ReadTime { get; set; }
    }
}
