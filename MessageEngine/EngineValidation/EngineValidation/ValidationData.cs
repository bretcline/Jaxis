using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jaxis.Engine.Validation
{
    public class ValidationData
    {
        public string MachineID { get; set; }
        public string EngineVersion { get; set; }
        public List<PluginData> PluginList { get; set; }
    }

    public class PluginData
    {
        public string PluginID { get; set; }
        public string PluginVersion { get; set; }
    }
}