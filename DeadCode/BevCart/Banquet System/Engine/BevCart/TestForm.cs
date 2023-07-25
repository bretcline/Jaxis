using System;
using System.Windows.Forms;
using JaxisInterfaces;
using JaxisEngine;
using JaxisEngine.Base;
using BevWCFDB;

namespace BevCart
{
    public partial class TestForm : Form
    {
        private Engine m_BevEngine;
        private UIDataConsumer m_Con;

        public TestForm( )
        {
            InitializeComponent( );
            Disposed += OnDispose;

            IDeviceConfig Config = new DeviceConfig { Name = "Test Data Consumer", ID = Guid.NewGuid().ToString() };

            m_Con = new UIDataConsumer( Config );
            m_Con.Produce += ProcessData; // Using an IDevice Produce event to get data to Form...
            m_BevEngine = new Engine( );
            m_BevEngine.RegisterDevice( m_Con );
        }

        public void OnDispose( object sender, EventArgs e )
        {
            if( null != m_BevEngine )
            {
                m_BevEngine.Stop( );
            }
        }

        private void sbStart_Click( object sender, EventArgs e )
        {
            if( null != m_BevEngine )
            {
                m_BevEngine.Start( );
            }
        }

        private void sbStop_Click( object sender, EventArgs e )
        {
            if( null != m_BevEngine )
            {
                m_BevEngine.Stop( );
            }
            gridPours.DataSource = null;
            WCFDB DB = new WCFDB();
            gridPours.DataSource = DB.GetPours();
        }

        private string ProcessData( IMessage _Message )
        {
            string rc = null;
            BevMessage Msg = _Message as BevMessage;
            if( null != Msg )
            {
                SetPourText(string.Format("{0} {1} {2} {3}", Msg.Tag, Msg.ReadTime.ToString(), Msg.Pour.Duration.Seconds.ToString(), Msg.Pour.Amount.ToString()));
            }
            return rc;
        }

        // This delegate enables asynchronous calls for setting
        // the text property on a TextBox control.
        delegate void SetTextCallback( string text );

        private void SetPourText( string _Text )
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if( tePourData.InvokeRequired )
            {
                SetTextCallback d = SetPourText;
                Invoke( d, new object[] { _Text } );
            }
            else
            {
                tePourData.Text = _Text;
            }
        }
    }
}
