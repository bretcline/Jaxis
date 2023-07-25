using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Jaxis.Controls.GlassFill;

namespace SampleApp
{
    public partial class Form1 : Form
    {
        public Form1( )
        {
            InitializeComponent( );
        }

        private void button1_Click( object sender, EventArgs e )
        {
            if( rdoBeer.Checked )
                fillGlass1.GlassType = GlassTypes.Beer;
            else if( rdoLiquor.Checked )
                fillGlass1.GlassType = GlassTypes.Shot;
            else if( rdoWine.Checked )
                fillGlass1.GlassType = GlassTypes.Wine;

            fillGlass1.FillLevel = trackBar1.Value;// short.Parse( trackBar1.Text );

            fillGlass1.Fill();
        }
    }
}
