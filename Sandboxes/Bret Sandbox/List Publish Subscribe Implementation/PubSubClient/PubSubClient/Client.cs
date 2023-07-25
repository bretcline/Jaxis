using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using PubSubClient.PubSubService;

namespace PubSubClient
{
    public partial class Client : Form
    {
        public delegate void MyEventCallbackHandler(string Name);
        public static event MyEventCallbackHandler MyEventCallbackEvent;

        delegate void SafeThreadCheck(string Name);
        
        [CallbackBehaviorAttribute(UseSynchronizationContext = false)]
        public class ServiceCallback : IPubSubServiceCallback
        {
            public void NameChange(string Name)
            {
                Client.MyEventCallbackEvent(Name);                
            }
        }

        public Client()
        {
            InitializeComponent();            

            InstanceContext context = new InstanceContext(new ServiceCallback());
            PubSubServiceClient client = new PubSubServiceClient(context);

            MyEventCallbackHandler callbackHandler = new MyEventCallbackHandler(UpdateForm);
            MyEventCallbackEvent += callbackHandler;

            client.Subscribe();
        }

        public void UpdateForm(string Name)
        {
            if (lblDisplay.InvokeRequired)
            {
                SafeThreadCheck sc = new SafeThreadCheck(UpdateForm);
                this.BeginInvoke(sc, new object[] { Name });
            }
            else
            {
                lblDisplay.Text += Name;
            }
        }
    }
}