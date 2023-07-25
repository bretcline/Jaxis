using System;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace kson
{
    public partial class Form1 : Form
    {
        private string m_resultText = string.Empty;

        public Form1()
        {
            InitializeComponent();
        }

        private void ButtonGoClick(object _sender, EventArgs _e)
        {
            try
            {
                SendRequest();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void SendRequest()
        {
            var requestBody = JsonHelper.Serialize(new ServiceParameters(textBoxCustomers.Text.Split(',')));
            var client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            client.UploadStringCompleted += ServiceCallCompleted;
            var methodUri = new Uri(textBoxUrl.Text);
            client.UploadStringAsync(methodUri, "POST", requestBody);
        }

        private void ServiceCallCompleted(object _sender, UploadStringCompletedEventArgs _e)
        {
            try
            {
                m_resultText = _e.Result;
                var result = JsonHelper.JsonToDataTable(m_resultText, "root/d/*");
                gridControl.DataSource = result;
                gridView.PopulateColumns(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonViewSourceClick(object _sender, EventArgs _e)
        {
            new ViewSourceWindow(m_resultText).Show();
        }
    }
}
