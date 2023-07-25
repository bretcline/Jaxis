using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskMessages
{
    public partial class Form1 : Form
    {
        delegate void SetTextCallback(string text);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //create a new server
            var server = new UdpListener();

            //start listening for messages and copy the messages back to the client
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    var received = await server.Receive();
                    DisplayMessage(received.Message);
//                    server.Reply("copy " + received.Message, received.Sender);
//                    if (received.Message == "quit")
//                        break;
                }
            });

        }

        private void DisplayMessage(string _message)
        {
            if (txtMessageList.InvokeRequired)
            {
                txtMessageList.BeginInvoke(new MethodInvoker(() => DisplayMessage(_message)));
            }
            else
            {
                txtMessageList.Text += string.Format( "{0}{1}", _message, System.Environment.NewLine );
            }
        }
    }
}
