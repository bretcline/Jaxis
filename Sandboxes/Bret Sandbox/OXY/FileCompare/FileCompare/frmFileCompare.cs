using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FileCompare
{
    public partial class frmFileCompare : Form
    {
        public frmFileCompare()
        {
            InitializeComponent();
        }

        private void btnLoadFileOne_Click(object sender, EventArgs e)
        {
            string file = GetFile();
            if( !string.IsNullOrWhiteSpace(file ) )
            {
                txtFileOne.Text = file;
            }
        }

        private string GetFile()
        {
            string rc = string.Empty;
            if (DialogResult.OK == ofdFiles.ShowDialog(this))
            {
                rc = ofdFiles.FileName;
            }
            return rc;
        }

        private void btnLoadFileTwo_Click(object sender, EventArgs e)
        {
            string file = GetFile();
            if (!string.IsNullOrWhiteSpace(file))
            {
                txtFileTwo.Text = file;
            }
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            var comp = new FileComparer(txtFileOne.Text, txtFileTwo.Text);

            var flags = new List<bool>( clbColumns.Items.Count );

            if (0 < clbColumns.Items.Count)
            {
                for (int i = 0; i < clbColumns.Items.Count; ++i)
                {
                    flags.Add(clbColumns.GetItemChecked(i));
                }
            }
            else
            {
                for
            }
            var data = comp.CompareFiles(flags);
            lstDifferences.Items.AddRange(data.ToArray());

            lblLineDifferences.Text = string.Format("Line Count {0}", data.Count()/2);
        }

        private void btnColumns_Click(object sender, EventArgs e)
        {
            var comp = new FileComparer(txtFileOne.Text, txtFileTwo.Text);
            var columns = comp.GetColumns( txtFileOne.Text );
            foreach (var column in columns)
            {
                clbColumns.Items.Add(column, true);
            }
        }
    }
}
