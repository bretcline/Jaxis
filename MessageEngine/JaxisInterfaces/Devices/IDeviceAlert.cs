using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jaxis.Interfaces
{
    //public enum DeviceAlertMessageType
    //{
    //    CannotConnect = 0,
    //    NotReadingTags = 1,
    //}

    public interface IDeviceAlertMessage : IDeviceMessage, IAlertMessage
    {
    }
}