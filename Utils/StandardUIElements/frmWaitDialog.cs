using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Jaxis.Utilities.StandardUIElements
{
    public partial class frmWaitDialog : Form
    {
        protected BackgroundWorker m_Worker = null;

        public frmWaitDialog( )
        {
            InitializeComponent( );
        }

        private void btnCancel_Click( object sender, EventArgs e )
        {
            m_Worker.CancelAsync( );
            pbBar.Value = 0;
            this.Hide( );
        }

        public void ShowModeless( BackgroundWorker _Worker, string _Title )
        {
            this.Text = _Title;

            m_Worker = _Worker;
            // Show this dialog in modeless state.
            this.Show( );

            // Process events until thread has died.

            while( m_Worker.IsBusy )
            {
                {
                    pbBar.Increment( 1 );
                    if( pbBar.Maximum == pbBar.Value )
                    {
                        pbBar.Value = pbBar.Minimum;
                    }
                }
                Application.DoEvents( );
                System.Threading.Thread.Sleep( 250 );
            }
            // Hide window.
            pbBar.Value = 0;
            this.Hide( );
        }
    }
}
