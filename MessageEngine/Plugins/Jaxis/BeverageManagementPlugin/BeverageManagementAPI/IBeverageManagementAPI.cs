using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Jaxis.BeverageManagement.Plugin
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IBeverageManagementAPI
    {
        [OperationContract]
        List<PourInformation> GetPourInformation(DateTime _startTime, DateTime _endTime);

        [OperationContract]
        bool BrandBottle(string _tagID, string _upc, Guid _nozzle);

    }
}
