using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BeverageManagement.Controls
{
    public partial class ReportParameter : DevExpress.XtraEditors.XtraUserControl
    {
        public ReportParameter( )
        {
            InitializeComponent( );

            chkUse.Checked = true;
            cmbWhereCompare.SelectedIndex = 0;
            cmbCompare.SelectedIndex = 0;
        }

        public string Lable
        {
            get { return lblName.Text; }
            set { lblName.Text = value; }
        }

        public string SQLName { get; set; }

        public string Value
        {
            get { return txtValue.Text; }
            set
            {
            	txtValue.Text = value;
            }
        }

        public bool ShowComparitor
        {
            set { cmbWhereCompare.Visible = value; }
        }

        public string WhereClause
        {
            get { return ( chkUse.Checked && false == string.IsNullOrWhiteSpace( Value ) ) ? string.Format("{0} {1} {2} '{3}'", cmbWhereCompare.Text, SQLName, cmbCompare.Text, Value) : string.Empty; }
        }

        private void chkUse_CheckedChanged( object sender, EventArgs e )
        {
            lblName.Enabled = cmbWhereCompare.Enabled = cmbCompare.Enabled = chkUse.Checked;
        }

    }

}
