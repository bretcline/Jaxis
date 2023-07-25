using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BeverageManagement.Forms
{
    public partial class frmPopup : DevExpress.XtraEditors.XtraForm
    {
        protected int m_Count = 0;
        protected frmPopup()
        {
            InitializeComponent();
        }

        private void frmPopup_Shown(object sender, EventArgs e)
        {
            m_Count = 0;
            tmrAutoClose.Enabled = true;
        }

        private void tmrAutoClose_Tick(object sender, EventArgs e)
        {
            m_Count++;
            if (m_Count > 10)
            {
                this.Close();
            }
        }

        public string Message { get { return lblMessage.Text; } set { lblMessage.Text = value; } }
        
        public static void ShowPopup( string _message )
        {
            var popup = new frmPopup();
            popup.Message = _message;    
            popup.ShowDialog();
        }

    }
}