using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Jaxis.Interfaces;
using Jaxis.Util.Log4Net;

namespace Jaxis.Engine.Base.Device
{
    public class DeviceConfigCollection
    {
        public DeviceConfigCollection( )
        {
            Configs = new List<DeviceConfig>( );
            Filters = new List<FilterConfig>( );
        }

        public List<DeviceConfig> Configs { get; set; }

        public List<FilterConfig> Filters { get; set; }
    }
}