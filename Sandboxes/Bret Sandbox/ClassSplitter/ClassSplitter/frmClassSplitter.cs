using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ClassSplitter
{
    public partial class frmClassSplitter : Form
    {
        public frmClassSplitter( )
        {
            InitializeComponent( );
        }

        private void btnFile_Click( object sender, EventArgs e )
        {
            if( DialogResult.OK == ofdClassList.ShowDialog( ) )
            {
                txtFile.Text = ofdClassList.FileName;
            }
        }

        private void btnSplit_Click( object sender, EventArgs e )
        {
            txtClassOne.Text = string.Empty;
            txtClassTwo.Text = string.Empty;

            List<string> Items = new List<string>( );
            using( StreamReader Reader = new StreamReader( txtFile.Text ) )
            {
                string File = Reader.ReadToEnd( );
                string[] Names = File.Split( System.Environment.NewLine.ToString( ).ToCharArray( ) );
                foreach( string Line in Names )
                {
                    if( !string.IsNullOrWhiteSpace( Line ) )
                    {
                        Items.Add( Line );
                    }
                }
            }
            
            Dictionary<int, string> UsedNames = new Dictionary<int, string>( );

            var CurrentClass = new List<TextBox> { txtClassOne, txtClassTwo };

            bool ClassIndex = false;
            Random Rnd = new Random( );
            while( UsedNames.Keys.Count < Items.Count )
            {
                int Index = Rnd.Next( Items.Count );
                if( !UsedNames.ContainsKey( Index ) )
                {
                    UsedNames.Add( Index, Items[Index] );
                    CurrentClass[Convert.ToInt32( ClassIndex )].Text += Items[Index] + System.Environment.NewLine;
                    ClassIndex = !ClassIndex;
                }
            }
        }
    }
}
