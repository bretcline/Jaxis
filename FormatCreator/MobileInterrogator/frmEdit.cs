using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LFI.RFID.Format;

namespace MobileInterrogator
{
    public partial class frmEdit : Form
    {
        public TagData ActiveItem 
        {
            get
            {
                return dlItem.Element;
            }
            set
            {
                dlItem.Element = value;
            }
        }

        public frmEdit( )
        {
            InitializeComponent( );
        }

        private void sbtnCancel_Click( object sender, EventArgs e )
        {
            DialogResult = DialogResult.Cancel;
         
            this.Close( );
        }


        private void sbtnWrite_Click( object sender, EventArgs e )
        {
            DialogResult = DialogResult.OK;
            this.Close( );
        }

        private void sbtnScan_Click( object sender, EventArgs e )
        {

        }
    }
} 