using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlServerCe;

namespace PassivePickup
{
    public partial class Form1 : Form
    {
        protected DataAccess m_DataAccess = null;

        public Form1( )
        {
            InitializeComponent( );
        }

        private void btnSubmit_Click( object sender, EventArgs e )
        {
            Customer Cust = m_DataAccess.ValidateTag( txtTagID.Text );
            lblAddress.Text = string.Format("{0}{1}{2}", Cust.Name, System.Environment.NewLine, Cust.Address );
            if( Cust.Paid )
            {
                pbPickup.Image = new Bitmap( ".\\TrashPickup.jpg" );
            }
        }

        private void btnInvalid_Click( object sender, EventArgs e )
        {
            m_DataAccess.InvalidAddress( txtTagID.Text );
        }

        private void Form1_Load( object sender, EventArgs e )
        {
            m_DataAccess = new DataAccess( );
        }
    }
}