using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Jaxis.Controls.GlassFill;

namespace FillGlassTest
{
    public partial class Form1 : Form
    {
        //FillGlass fillGlass = new FillGlass();
        public Form1( )
        {
            InitializeComponent( );

            trackBar1.Value = 100;
            fillGlass1.GlassType = GlassTypes.Wine;
        }

        private void btnFill_Click( object sender, EventArgs e )
        {
            fillGlass1.FillLevel = trackBar1.Value;
            fillGlass1.Fill( );
        }

        private void btnWine_Click( object sender, EventArgs e )
        {
            fillGlass1.GlassType = GlassTypes.Wine;
            fillGlass1.Reset( );
        }

        private void btnBeer_Click( object sender, EventArgs e )
        {
            fillGlass1.GlassType = GlassTypes.Beer;
            fillGlass1.Reset( );
        }

        private void btnShot_Click( object sender, EventArgs e )
        {
            fillGlass1.GlassType = GlassTypes.Shot;
            fillGlass1.Reset( );
        }

        private void btnReset_Click( object sender, EventArgs e )
        {
            fillGlass1.Reset( );
        }
    }
}