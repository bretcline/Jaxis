namespace ReceiverApp
{
    using ReceiverApp.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class SplashScreen : Form
    {
        private IContainer components;
        private Timer timer1;

        public SplashScreen()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.timer1 = new Timer(this.components);
            base.SuspendLayout();
            this.timer1.Enabled = true;
            this.timer1.Interval = 0x7d0;
            this.timer1.Tick += new EventHandler(this.timer1_Tick);
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackgroundImage = Resources.PureRF_copy;
            base.ClientSize = new Size(0x1d4, 0x13c);
            base.ControlBox = false;
            base.FormBorderStyle = FormBorderStyle.None;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "SplashScreen";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "SplashScreen";
            base.TopMost = true;
            base.ResumeLayout(false);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            base.Dispose();
        }
    }
}

