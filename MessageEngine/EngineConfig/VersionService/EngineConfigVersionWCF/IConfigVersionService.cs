using System;
using System.ServiceModel;
using EngineConfigVersionData;

namespace EngineConfigVersionWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IConfigVersionService
    {
        [OperationContract]
        void SendCurrentVersion(EngineVersionData _VersionData);
    }
}
