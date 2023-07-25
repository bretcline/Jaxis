using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.Interfaces;
using Jaxis.Interfaces.Tags;

namespace Jaxis.AlienRFID.MessageLibrary
{
    public class AlienMessages : BaseMessage, ITagRead //IDeviceMessage
    {
        public AlienMessages( )
        {
        }

        #region IMessage Members

        public string DeviceID { get; set; }

        public string TagID { get; set; }

        #endregion IMessage Members

        #region IDeviceMessage Members

        public string ReaderID { get; set; }

        public double SignalStrength { get; set; }

        public byte[] RawData { get; set; }

        #endregion IDeviceMessage Members
    }
}