using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WebWatcher
{
    public partial class frmLogin : Form
    {
        WatcherSettings m_Settings;

        public frmLogin( WatcherSettings _Settings )
        {
            m_Settings = _Settings;
            InitializeComponent( );
        }

        private void btnLogin_Click( object sender, EventArgs e )
        {
            m_Settings.UserName = txtUserName.Text;
            m_Settings.Password = txtPassword.Text;
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click( object sender, EventArgs e )
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnSettings_Click( object sender, EventArgs e )
        {
            using( frmSettings Settings = new frmSettings( m_Settings ) )
            {
                Settings.ShowDialog( );
            }
        }
    }
}