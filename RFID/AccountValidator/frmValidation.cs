using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using ValidationEngine;
using System.IO;
using JaxisEngine;
using Jaxis.MessageLibrary;
using Jaxis.MessageProcessor.Alien;
using JaxisEngine.Base.Device;
using Jaxis.Interfaces;
using DBValidation;
using System.Collections.Concurrent;
using Jaxis.AlienRFID.MessageLibrary;

namespace AccountValidator
{
    public partial class frmAccountValidator : DevExpress.XtraEditors.XtraForm
    {
        protected bool Pushed { get; set; }
        protected System.Threading.Thread UIUpdate = null;
        protected Engine m_Engine = null;
        protected ConcurrentQueue<ValidationResults> m_Queue = new ConcurrentQueue<ValidationResults>( );

        public frmAccountValidator( )
        {
            InitializeComponent( );

            IDeviceConfig Config = new DeviceConfig { Name = "Test Data Consumer", ID = Guid.NewGuid( ).ToString( ) };
            Processor P = new Processor( Config );

            Config = new DeviceConfig { Name = "Data Validator", ID = Guid.NewGuid( ).ToString( ) };
            Config.ConsumerMessageType = MessageType.UIData;
            DBValidator V = new DBValidator( Config );
            V.Produce = QueueMessage;

            m_Engine = new Engine( );
            m_Engine.RegisterDevice( P );
            m_Engine.RegisterDevice( V );

            m_Engine.Start( );
        }

        public string QueueMessage( IMessage _Results )
        {
            ValidationResults Msg = _Results as ValidationResults;
            if( null != Msg )
            {
                UpdateUI( Msg );
                //lock( m_Queue )
                //{
                //    m_Queue.Enqueue( Msg );
                //}
            }
            return string.Empty;
        }

//        public void UpdateUI( )
        public void UpdateUI( ValidationResults _Message )
        {
            ValidationResults Results = _Message;
//            if( m_Queue.TryDequeue( out Results ) )
            {
                if( this.InvokeRequired )
                {
                    BeginInvoke( new MethodInvoker( ( ) => UpdateUI( Results ) ) );
                }
                else
                {
                    try
                    {

                        string HTMLOutput = "<body style=\"background-color:{0}\">{1}</body>";
                        string BackColor = "Red";
                        if( null != Results && true == Results.IsValid )
                        {
                            if( Results.IsCurrent )
                            {
                                this.btnColor.Image = global::AccountValidator.Properties.Resources.Green_Check;
                                BackColor = "#00FF00";
                            }
                            else
                            {
                                this.btnColor.Image = global::AccountValidator.Properties.Resources.Red_Circle;
                            }
                            HTMLOutput = string.Format( HTMLOutput, BackColor, Results.HTMLOutput );
                        }
                        else
                        {
                            this.btnColor.Image = global::AccountValidator.Properties.Resources.Red_Circle;
                            HTMLOutput = string.Format( HTMLOutput, BackColor, Results.Results );
                        }
                        byte[] HTML = null;
                        HTML = System.Text.Encoding.ASCII.GetBytes( HTMLOutput );
                        MemoryStream MStream = new MemoryStream( HTML );
                        wbEntityInfo.DocumentStream = MStream;
                        string TimeFormat = string.Format( "{0}{1}{2}", "MM/dd/yyyy", System.Environment.NewLine, "hh:mm:ss" );
                        lblReadTime.Text = DateTime.Now.ToString( TimeFormat );

                        
                        this.Refresh( );
                        //System.Threading.Thread.Sleep( 10000 );
                    }
                    catch( Exception err )
                    {

                    }
                }
            }
            //else
            //{
            //    System.Threading.Thread.Sleep( 500 );
            //}
        }

        private void frmAccountValidator_Load( object sender, EventArgs e )
        {
            object empty = System.Reflection.Missing.Value;
            wbEntityInfo.Navigate( "about:blank" );
        }

        private void btnColor_Click( object sender, EventArgs e )
        {
            //ValidationKey Key = new ValidationKey( );
            //Key.ProcessValidation = UpdateAccount;

            //if( Pushed == true )
            //{
            //    Pushed = false;
            //    Key.Key = "9876";
            //}
            //else
            //{
            //    Pushed = true;
            //    Key.Key = "1234";
            //}
            //m_Engine.SubmitForProcessing( Key );
        }
    }
}
