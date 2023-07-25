using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FillGlassTest
{
    public partial class Form1 : Form
    {
        //FillGlass fillGlass = new FillGlass();
        public Form1()
        {
            InitializeComponent();
            
            trackBar1.Value = 100;
            fillGlass1.ShowWine();
        }

        private void btnFill_Click(object sender, EventArgs e)
        {
            fillGlass1.Fill();
        }



        private void btnWine_Click(object sender, EventArgs e)
        {
            fillGlass1.ShowWine();
        }

        private void btnBeer_Click(object sender, EventArgs e)
        {
            fillGlass1.ShowBeer();
        }

        private void btnShot_Click(object sender, EventArgs e)
        {
            fillGlass1.ShowShot();
        }

        private void cbDark_CheckedChanged(object sender, EventArgs e)
        {
            fillGlass1.SelectColor(cbDark.Checked);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            fillGlass1.SetLevel(trackBar1.Value);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            fillGlass1.Reset();
        }


    }
}
