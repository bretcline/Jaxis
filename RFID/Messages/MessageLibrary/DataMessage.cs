using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jaxis.AlienRFID.MessageLibrary
{
    public class DataMessage : AlienMessages
    {

        public DataMessage( AlienMessages _Message )
        {
            this.Type = _Message.Type;
            this.DeviceName = _Message.DeviceName;
            this.DeviceID = _Message.DeviceID;
            this.Tag = _Message.Tag;
            this.ReadTime = _Message.ReadTime;
        }
    }
}
