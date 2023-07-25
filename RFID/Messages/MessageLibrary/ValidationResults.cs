using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.Interfaces;

namespace Jaxis.AlienRFID.MessageLibrary
{
    public class ValidationResults : AlienMessages
    {
        #region IValidationResults Members

        public ValidationResults( )
        {
            this.Type = MessageType.UIData;
        }

        public ValidationResults( AlienMessages _Message )
        {
            this.Type = _Message.Type;
            this.DeviceName = _Message.DeviceName;
            this.DeviceID = _Message.DeviceID;
            this.Tag = _Message.Tag;
            this.ReadTime = _Message.ReadTime;
        }

        public bool IsValid { get; set; }
        public bool IsCurrent { get; set; }
        public string Name { get; set; }
        public string HTMLOutput { get; set; }
        public string Results { get; set; }


        #endregion
    }
}
