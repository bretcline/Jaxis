using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jaxis.Interfaces
{
    public interface IDeviceMessage : IMessage
    {
        //string DeviceName { get; set; }
        //string ReaderID { get; set; }
        string DeviceID { get; set; }

        byte[] RawData { get; set; }
    }
}