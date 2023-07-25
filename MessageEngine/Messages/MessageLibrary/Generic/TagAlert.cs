using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.Interfaces;
using Jaxis.Interfaces.Tags;

namespace Jaxis.MessageLibrary
{
    public class TagAlertMessage : AlertNotification, ITagAlertMessage
    {
        #region ITagAlert Members

        public string TagID { get; set; }
        
        public string DeviceID { get; set; }

        public double BatteryVoltage { get; set; }

        public Dictionary<string, object> Parameters { get; set; }

        public TagAlertMessage( )
        {
            Parameters = new Dictionary<string, object>( );
        }

        public override string ToString()
        {
            var rc = new StringBuilder();

            rc.Append(string.Format("Tag {0} reported by Device {1} is reporting: {2}", TagID, DeviceID, AlertType  ));

            return rc.ToString();
        }
        #endregion ITagAlert Members
    }
}