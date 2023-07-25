using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Jaxis.Engine;
using Jaxis.Engine.Base.Device;
using Jaxis.Interfaces;
using Jaxis.Interfaces.Tags;

namespace TestApp
{
    public partial class TestForm : Form
    {
        private EngineServiceDll m_Engine;
        private UIDataConsumer m_Con;
        private System.Threading.Thread m_EngineThread;
        protected List<string> m_Data = new List<string>( );

        public TestForm( )
        {
            InitializeComponent( );
            Disposed += OnDispose;
            m_Con = new UIDataConsumer(UIDataConsumer.GetDefaultDeviceConfig());
            m_Con.DisplayData = ProcessData; // Using an IDevice Produce event to get data to Form...
            m_Engine = new EngineServiceDll ( );
        }

        public void OnDispose( object sender, EventArgs e )
        {
            if( null != m_Engine )
            {
                m_Engine.Stop( );
            }
        }

        private void sbStart_Click( object sender, EventArgs e )
        {
            if( null != m_Engine )
            {
                m_EngineThread = new  System.Threading.Thread( m_Engine.Start );
                m_EngineThread.Start();
                System.Threading.Thread.Sleep(20000);// give engine time to get up and running
                m_Engine.RegisterDevice(m_Con);
                m_Engine.StartDevice(m_Con.Config.ID);
                gridPours.DataSource = m_Data;

                //IDevice TicketCon = new Jaxis.BeverageManagement.Plugin.TicketConsumer(Jaxis.BeverageManagement.Plugin.TicketConsumer.GetDefaultDeviceConfig());
                //m_Engine.RegisterDevice(TicketCon);
                //m_Engine.StartDevice(TicketCon.Config.ID);
            }
        }

        private void sbStop_Click( object sender, EventArgs e )
        {
            if( null != m_Engine )
            {
                m_Engine.Stop( );
            }
            gridPours.DataSource = null;
        }

        private string ProcessData( IMessage _Message )
        {
            string rc = null;
            //ITagRead Msg = _Message as ITagRead;
            //if( null != Msg )
            {
                m_Data.Add(string.Format("{0}   {1}   {2}", _Message.Driver, _Message.Type.ToString(), _Message.ReadTime.ToString()));
                gridPours.RefreshDataSource( );
                SetText(string.Format("{0}   {1}   {2}", _Message.Driver, _Message.Type.ToString(), _Message.ReadTime.ToString()));
            }
            return rc;
        }

        // This delegate enables asynchronous calls for setting
        // the text property on a TextBox control.
        delegate void SetTextCallback( string text );

        private void SetText( string _Text )
        {
            string text = _Text;
            if( this.InvokeRequired )
            {
                SetTextCallback d = SetText;
                this.BeginInvoke( d, new object[] { _Text } );
            }
            else
            {
                tePourData.Text = text;
            }
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            //if( tePourData.InvokeRequired )
            //{
            //    SetTextCallback d = new SetTextCallback( SetText );
            //    this.BeginInvoke( d, new object[] { _Text } );
            //}
            //else
            //{
            //    tePourData.Text = _Text;
            //}
        }
    }
}