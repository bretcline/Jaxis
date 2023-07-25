using System;
using BevClasses;
using System.ServiceModel;
using System.Collections.Generic;

namespace BevWCFDB
{
    // NOTE: If you change the interface name "IService1" here, you must also update the reference to "IService1" in App.config.
    [ServiceContract]
    public interface IWCFDB
    {
        [OperationContract]
        string AddUpdatePour(Pour _Pour);

        [OperationContract]
        List<Pour> GetPours();

        [OperationContract]
        Pour GetPour(Guid _ID);

        [OperationContract]
        string AddUpdateBottle(Bottle _Bottle);

        [OperationContract]
        List<Bottle> GetBottles();

        [OperationContract]
        Bottle GetBottle(Guid _ID);

        [OperationContract]
        Bottle GetBottleForTag(string _Tag);

        [OperationContract]
        string AddUpdateBeverage(Beverage _Beverage);

        [OperationContract]
        List<Beverage> GetBeverages();

        [OperationContract]
        Beverage GetBeverage(Guid _ID);
    }
}
