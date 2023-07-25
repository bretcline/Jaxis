using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Jaxis.Interfaces;
using Jaxis.Util.Log4Net;

namespace Jaxis.Engine.Base.Device
{
    public class FilterConfig : IFilterConfig
    {
        public string AssemblyName { get; set; }

        public string AssemblyType { get; set; }

        public string AssemblyVersion { get; set; }

        #region IFilterConfig Members

        public string Name { get; set; }

        public FilterType Type { get; set; }

        public List<DeviceConfigOption> Options { get; set; }

        #endregion IFilterConfig Members

        public FilterConfig( )
        {
            Options = new List<DeviceConfigOption>();
        }
    }
}