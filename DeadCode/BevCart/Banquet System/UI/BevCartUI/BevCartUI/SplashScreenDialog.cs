using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Jaxis.Util.Log4Net;

namespace BevCartUI
{
    public partial class SplashScreenDialog : Form
    {
        private Thread ms_oThread = null;
        private double m_dblOpacityIncrement = .25;
        private double m_dblOpacityDecrement = .15;
        private const int TIMER_INTERVAL = 50;
        private const int SPLASH_MINIMUM_SHOW = 4000;
        private string m_sStatus;
        private bool m_minimumTimeReached = false;
        private bool m_bCloseForm = false;

        public bool FormIsClosing = false;

        public SplashScreenDialog()
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
            this.ClientSize = this.BackgroundImage.Size;
            this.Opacity = 0.0;
            timer1.Interval = TIMER_INTERVAL;
            timer2.Interval = SPLASH_MINIMUM_SHOW;
            timer1.Start();
            lblProductVersion.BackColor = Color.Transparent;
            lblClose.BackColor = Color.Transparent;
            lblStatus.BackColor = Color.Transparent;

            lblProductVersion.Text = Application.ProductVersion;
        }

        public void ShowSplashScreen()
        {
            timer1.Enabled = true;
            timer2.Start();
            timer2.Enabled = true;
            ms_oThread = new Thread(new ThreadStart(this.ShowForm));
            ms_oThread.IsBackground = true;
            ms_oThread.SetApartmentState(ApartmentState.STA);
            ms_oThread.Start();
        }

        public void CloseSplashScreen()
        {
            m_bCloseForm = true;
            if (m_minimumTimeReached)
                this.m_dblOpacityIncrement = -this.m_dblOpacityDecrement;
            ms_oThread = null; 

        }
        private void ShowForm()
        {
            Application.Run(this);
        }

        public delegate void SetOpacityDelegate(double opacity);
        public void SetFormOpacity(double opacity)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SetOpacityDelegate(SetFormOpacity), opacity);
            }
            else
            {
                this.Opacity += opacity;
            }
        }

        public delegate void CloseFormDelegate();
        public void CloseForm()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new CloseFormDelegate(CloseForm));
                }
                else
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Log.WriteException("SlashScreenDialog Error", ex);
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (m_bCloseForm && m_minimumTimeReached)
            {
                this.m_dblOpacityIncrement = -this.m_dblOpacityDecrement;
                FormIsClosing = true;
            }
            if (m_dblOpacityIncrement > 0)
            {
                if (this.Opacity < 1)
                    this.SetFormOpacity(m_dblOpacityIncrement);
            }
            else
            {
                if (this.Opacity > 0)
                    this.SetFormOpacity(m_dblOpacityIncrement);
                else
                    this.CloseForm();
            }
            updateStatus(m_sStatus);
        }

        public delegate void updateStatusDelegate(string status);
        private void updateStatus(string status)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new updateStatusDelegate(updateStatus), new object[] { status });
            }
            else
            {
                lblStatus.Text = status;
            }
        }

        public string SetStatus(string newStatus)
        {
            //if (ms_frmSplash == null)
            //    return;
            this.m_sStatus = newStatus;
            return "";
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            m_minimumTimeReached = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            //this.CloseForm();
            this.Hide();
        }

        private void SplashScreenDialog_Click(object sender, EventArgs e)
        {
            //this.CloseForm();
            this.Hide();
        }

        private void website_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://www.beveragemetrics.com");
            }
            catch (Exception ex)
            {
            }
        }


    }

}
