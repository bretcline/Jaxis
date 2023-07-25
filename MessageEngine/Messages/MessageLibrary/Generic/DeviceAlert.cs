using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.Interfaces;

namespace Jaxis.MessageLibrary
{
    public class DeviceAlertMessage : AlertNotification, IDeviceAlertMessage
    {
        #region IDeviceAlert Members

        public string DeviceID { get; set; }

        #endregion

        #region IMessage Members

        public byte[] RawData { get; set; }

        #endregion
    }
}
