using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using Publisher.PubSubService;

namespace Publisher
{
    public partial class Publisher : Form
    {
        InstanceContext context = null;
        PubSubServiceClient client = null;
        
        public class ServiceCallback : IPubSubServiceCallback
        {
            public void NameChange(string Name)
            {
                MessageBox.Show(Name);
            }
        }

        public Publisher()
        {
            InitializeComponent();    
        }

        private void btnPublish_Click(object sender, EventArgs e)
        {
            context = new InstanceContext(new ServiceCallback());
            client = new PubSubServiceClient(context);
            client.PublishNameChange(txtMessage.Text);
            client.Close();
        }
    }
}