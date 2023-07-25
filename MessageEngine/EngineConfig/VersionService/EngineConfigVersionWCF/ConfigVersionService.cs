using System;
using System.Collections.Generic;
using EngineConfigVersionData;
using EngineConfigVersions;
using Jaxis.Engine.Base.Device;
using Jaxis.Interfaces;

namespace EngineConfigVersionWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class ConfigVersionService : IConfigVersionService
    {
        public void SendCurrentVersion( EngineVersionData _VersionData )
        {
            EngineVersions.AddVersion( _VersionData );
        }
    }
}