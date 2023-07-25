using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Jaxis.MessageLibrary;
using System.Xml;

namespace Jaxis.BeverageManagement.Plugin.PourCollector
{
    public partial class frmDataCollector : Form
    {
        private bool m_WriteData = false;
        List<TextEdit> m_Angles = new List<TextEdit>();
        private StreamWriter m_Writer = null;

        public frmDataCollector( )
        {
            InitializeComponent( );

            m_Angles.Add( txt60to80 );
            m_Angles.Add( txt81to87 );
            m_Angles.Add( txt87to92 );
            m_Angles.Add( txt92to98 );
            m_Angles.Add( txt98to103 );
            m_Angles.Add( txt103to106 );
            m_Angles.Add( txt106to112 );
            m_Angles.Add( txt112to115 );
            m_Angles.Add( txt115to121 );
            m_Angles.Add( txt121to142 );
            m_Angles.Add( txt142to160 );
            m_Angles.Add( txt160to180 );

            DateTime now = DateTime.Now;
        }

        public void ReceivePour(IdentecPour _msg)
        {
            if( InvokeRequired )
            {
                Invoke( (MethodInvoker)( ( ) => ReceivePour( _msg ) ) );
            }
            else
            {
                if( true == m_WriteData )
                {
                    WriteData( );
                }

                txtPourAmount.Focus();
                txtPourAmount.SelectAll();
                TimeSpan totalTime = new TimeSpan( );
                for( int i = 0; i < _msg.Angles.Count; ++i )
                {
                    var angle = _msg.Angles[i];
                    m_Angles[i].Text = angle.Duration.ToString( );
                    totalTime += angle.Duration;
                }
                txtTotalTime.Text = totalTime.ToString( );
            }
        }

        private void WriteData()
        {
            double value = double.Parse( txtPourAmount.Text );
            double previousAmount = double.Parse( txtAmountLeft.Text );
            txtAmountLeft.Text = ( previousAmount - value ).ToString( );

            StringBuilder builder = new StringBuilder();

            builder.Append( txtPourAmount.Text );
            builder.Append( "," + txtBottleSize.Text );
            builder.Append( "," + txtNozzle.Text );
            builder.Append( "," + txtAmountLeft.Text );
            builder.Append("," + chkLastPour.Checked);
            builder.Append( "," + txtTotalTime.Text );

            foreach (var angle in m_Angles )
            {
                builder.Append( "," + angle.Text );
            }
            m_Writer.WriteLine( builder.ToString( ) );
            m_Writer.Flush();
        }


        private void TxtPourAmountKeyPress( object sender, KeyPressEventArgs e )
        {
            try
            {
                if( e.KeyChar == (char)Keys.Enter )
                {
                    m_WriteData = true;
                }
            }
            catch( Exception )
            {
            }
        }

        private void btnStore_Click( object sender, EventArgs e )
        {
            WriteData();
        }

        private void btnOpenFile_Click( object sender, EventArgs e )
        {
            if( btnOpenFile.Text == "Open File" )
            {
                m_Writer = new StreamWriter( txtFileName.Text, true );
                btnOpenFile.Text = "Close File";
            }
            else
            {
                m_Writer.Flush();
                m_Writer.Close();
                btnOpenFile.Text = "Open File";
            }
        }
    }
}
