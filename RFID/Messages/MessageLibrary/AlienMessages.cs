using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.Interfaces;

namespace Jaxis.AlienRFID.MessageLibrary
{
    public class AlienMessages : IDeviceMessage
    {
        public AlienMessages( )
        {
        }

        #region IMessage Members

        public MessageType Type { get; set; }
        public string DeviceName { get; set; }
        public string DeviceID { get; set; }
        public string Tag { get; set; }
        public DateTime ReadTime { get; set; }

        #endregion


        #region IDeviceMessage Members

        public string ReaderID { get; set; }
        public byte[] RawData { get; set; }

        #endregion
    }
}
