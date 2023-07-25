using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PubSubService2
{
    [ServiceContract(Namespace = "http://ListPublishSubscribe.Service", SessionMode = SessionMode.Required, CallbackContract = typeof(IPubSubContract))]
    public interface IPubSubService
    {
        [OperationContract(IsOneWay = false, IsInitiating = true)]
        void Subscribe();
        [OperationContract(IsOneWay = false, IsInitiating = true)]
        void Unsubscribe();
        [OperationContract(IsOneWay = false)]
        void PublishNameChange(string Name);
    }

    public interface IPubSubContract
    {
        [OperationContract(IsOneWay = true)]
        void NameChange(string Name);
    }
}
