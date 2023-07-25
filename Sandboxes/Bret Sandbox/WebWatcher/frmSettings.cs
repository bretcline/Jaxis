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
    public partial class frmSettings : Form
    {
        WatcherSettings m_Settings;

        public frmSettings( WatcherSettings _Settings )
        {
            m_Settings = _Settings;
            InitializeComponent( );

            numInterval.Value = m_Settings.RefreshInterval;
            txtLoginURL.Text = m_Settings.LoginURL;
            numWatchers.Value = m_Settings.NumberOfWatchers;
        }

        private void btnOK_Click( object sender, EventArgs e )
        {
            m_Settings.LoginURL = txtLoginURL.Text;
            m_Settings.RefreshInterval = Convert.ToInt32( numInterval.Value );
            m_Settings.NumberOfWatchers = Convert.ToInt32( numWatchers.Value );
        }
    }
}