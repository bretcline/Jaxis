using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Sunglass_Mirror
{
    public partial class FullSizeForm : DevExpress.XtraEditors.XtraForm
    {
        public FullSizeForm(Image _Image)
        {
            InitializeComponent();
            if (null != _Image)
            {
                Bitmap B = new Bitmap(Width - 10, Height - 10);
                Graphics G = Graphics.FromImage(B);
                G.DrawImage(_Image, 0, 0, Width - 10, Height - 10);
                sbPicture.Image = B;
            }
        }

        private void sbPicture_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}