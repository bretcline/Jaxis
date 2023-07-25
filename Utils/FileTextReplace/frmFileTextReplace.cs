using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FileTextReplace
{
    public partial class frmFileTextReplace : Form
    {
        public frmFileTextReplace( )
        {
            InitializeComponent( );
        }

        private void btnOk_Click( object sender, EventArgs e )
        {
            if( System.IO.File.Exists( txtFileName.Text ) )
            {
                string NewFileName = txtFileName.Text + DateTime.Now.Ticks + ".old";
                System.IO.File.Copy( txtFileName.Text, NewFileName );
                using( StreamReader Reader = new StreamReader( NewFileName ) )
                {
                    string FileText = Reader.ReadToEnd( );
                    if( FileText.Contains( txtFind.Text ) )
                    {
                        string NewFileText = FileText.Replace( txtFind.Text, txtReplace.Text );
                        using( StreamWriter Writer = new StreamWriter( txtFileName.Text, false ) )
                        {
                            Writer.Write( NewFileText );
                        }
                    }
                }
            }
        }

        private void btnFilePicker_Click( object sender, EventArgs e )
        {
            if( DialogResult.OK == ofdFile.ShowDialog( ) )
            {
                txtFileName.Text = ofdFile.FileName;
            }
        }
    }
}
