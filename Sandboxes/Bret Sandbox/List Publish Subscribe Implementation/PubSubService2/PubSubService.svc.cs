using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PubSubService2
{
    public class ServiceEventArgs : EventArgs
    {
        public string Name;
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class PubSubService : IPubSubService
    {
        public delegate void NameChangeEventHandler(object sender, ServiceEventArgs e);
        public static event NameChangeEventHandler NameChangeEvent;

        IPubSubContract ServiceCallback = null;
        NameChangeEventHandler NameHandler = null;

        public void Subscribe()
        {
            ServiceCallback = OperationContext.Current.GetCallbackChannel<IPubSubContract>();
            NameHandler = new NameChangeEventHandler(PublishNameChangeHandler);
            NameChangeEvent += NameHandler;
        }

        public void Unsubscribe()
        {
            NameChangeEvent -= NameHandler;
        }

        public void PublishNameChange(string Name)
        {
            ServiceEventArgs se = new ServiceEventArgs();
            se.Name = Name;
            NameChangeEvent(this, se);
        }

        public void PublishNameChangeHandler(object sender, ServiceEventArgs se)
        {
            ServiceCallback.NameChange(se.Name);

        }
    }
}
