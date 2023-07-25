using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JaxisInterfaces;
using BucketCollectionLib;


namespace PourBucketCollector
{
    public partial class frmTestOutputDispaly : Form
    {
        JaxisEngine.Engine m_Engine = null;

        public frmTestOutputDispaly()
        {
            InitializeComponent();
        
            IDeviceConfig Config = new FreeScaleConfig();
            Config.ConsumerMessageType = MessageType.RawData;
            Jaxis.BucketPlugin.PourBucketConsumer Consumer = new Jaxis.BucketPlugin.PourBucketConsumer(Config);
            Consumer.Produce += ProcessMyMessage;

            Jaxis.BucketPlugin.FakeBucketDevice Device = new Jaxis.BucketPlugin.FakeBucketDevice(Config);

            m_Engine = new JaxisEngine.Engine();
            m_Engine.RegisterDevice(Consumer);
            m_Engine.RegisterDevice(Device);

            m_Engine.Start();

        }

        private void frmTestOutputDispaly_Load(object sender, EventArgs e)
        {
            BucketProcessor BP = new BucketProcessor();
            BP.MessageDelegate += ProcessMyMessage;

            //Consumer.Produce += ProcessMyMessage;

            //BucketProcessor P = new BucketProcessor();
            //P.MessageDelegate += ProcessMyMessage;
            //txtTestDisplay.Text += P.OpenComPort();
        }

        protected string ProcessMyMessage(IMessage _Message)
        {
            string rc = string.Empty;
            if (InvokeRequired)
            {
                BeginInvoke(new ProduceHandler(ProcessMyMessage), new object[] { _Message });
            }
            else
            {
                //Test output only
                PourMessage Message = _Message as PourMessage;
                if (null != Message)
                {
                    StringBuilder Msg = new StringBuilder(string.Format(Environment.NewLine + "Start Time: {0}", _Message.ReadTime));

                    foreach (AngleBucket AB in Message.Buckets.Keys)
                    {
                        Msg.Append(string.Format(Environment.NewLine + "Time spent in {0} = {1}", AB, Message.Buckets[AB]));
                    }

                    Msg.Append(string.Format(Environment.NewLine + "End Time: {0}", Message.PourStop));

                    txtTestDisplay.Text += Msg.ToString();
                }
            }
            return rc;
        }

        private void frmTestOutputDispaly_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_Engine.Stop();
        }
    }

    public class FreeScaleConfig : IDeviceConfig
    {

        #region IDeviceConfig Members

        public MessageType ConsumerMessageType
        {
            get;
            set;
        }

        public string ID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public List<string> Options
        {
            get;
            set;
        }

        public DeviceType Type
        {
            get;
            set;
        }

        #endregion
    }

}
