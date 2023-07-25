using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CustomDraw
{
    public partial class Form1 : Form
    {
        List<PointF> m_Region = new List<PointF>( );
        int m_TimerMax = 1000;
        int m_TimerLoop = 0;
        int m_Fill = 0;
        Image m_CurrentImage = null;

        Graphics m_Drawer;

        public Form1( )
        {
            InitializeComponent( );

            m_Drawer = pbImage.CreateGraphics( );
        }

        private void Form1_Paint( object sender, PaintEventArgs e )
        {
        }

        private void tmrDrawSpeed_Tick( object sender, EventArgs e )
        {
            float Percent = (float)tbarFill.Value / (float)tbarFill.Maximum;

            int Value = tbarFill.Value;

            float interval = (float)m_TimerMax / (float)tmrDrawSpeed.Interval;
            Debug.WriteLine( string.Format( "( {0} * {1} ) < {2}", m_TimerLoop, tmrDrawSpeed.Interval, m_TimerMax ) );
            if( ( m_TimerLoop * tmrDrawSpeed.Interval ) > m_TimerMax || 0 == Value )
            {
                m_TimerLoop = 0;
                tmrDrawSpeed.Stop( );
            }
            else
            {
                ++m_TimerLoop;
                ++m_Fill;

                drawer.DrawImage( m_CurrentImage, new Point( 0, 0 ) );

                float MaxHeight = 296.0f * Percent;
                float Incriment = MaxHeight / interval;
                int Y = (int)( 322 - ( Incriment * m_TimerLoop ) );
                Debug.WriteLine( string.Format( "{0}%, {1}h, {2} time, {3}i, Y = {4}", Percent, MaxHeight, interval, Incriment, Y ) );
                Rectangle Rec = new Rectangle( 39, Y, 230, 500 );

                Brush Black = new SolidBrush( Color.Black );
                Brush B = new SolidBrush( Color.Blue );

                Pen P = new Pen( Black, 2.0f );

                m_Drawer.FillRectangle( B, Rec );

                //drawer.DrawPolygon( P, m_Region.ToArray( ) );
            }
        }

        private void btnFill_Click( object sender, EventArgs e )
        {
            m_Fill = 0;
            tmrDrawSpeed.Start( );
        }

        private void btnBeer_Click( object sender, EventArgs e )
        {
            m_CurrentImage = Image.FromFile( @"C:\Source\Jaxis\trunk\Sandboxes\Bret Sandbox\CustomDraw\beer-mug-12-cs.jpg" );
            pbImage.Image = m_CurrentImage;
        }

        private void btnWine_Click( object sender, EventArgs e )
        {
            m_CurrentImage = Image.FromFile( @"C:\Source\Jaxis\trunk\Sandboxes\Bret Sandbox\CustomDraw\SAVOY-WINE-GLASS.jpg" );
            pbImage.Image = m_CurrentImage;
        }

        private void btnShot_Click( object sender, EventArgs e )
        {
            m_CurrentImage = Image.FromFile( @"C:\Source\Jaxis\trunk\Sandboxes\Bret Sandbox\CustomDraw\shot-glass-drinking.jpg" );
            pbImage.Image = m_CurrentImage;

            //Left Side
            m_Region.Add( new PointF( 90, 322 ) );
            m_Region.Add( new PointF( 88, 236 ) );
            m_Region.Add( new PointF( 86, 203 ) );
            m_Region.Add( new PointF( 83, 176 ) );
            m_Region.Add( new PointF( 77, 150 ) );
            m_Region.Add( new PointF( 68, 112 ) );
            m_Region.Add( new PointF( 39, 26 ) );

            // Top
            m_Region.Add( new PointF( 72, 28 ) );
            m_Region.Add( new PointF( 118, 29 ) );
            m_Region.Add( new PointF( 188, 29 ) );

            // Right Side
            m_Region.Add( new PointF( 269, 28 ) );
            m_Region.Add( new PointF( 254, 72 ) );
            m_Region.Add( new PointF( 243, 112 ) );
            m_Region.Add( new PointF( 230, 164 ) );
            m_Region.Add( new PointF( 226, 188 ) );
            m_Region.Add( new PointF( 221, 245 ) );
            m_Region.Add( new PointF( 215, 322 ) );

            GraphicsPath Path = new GraphicsPath( );
            Path.AddPolygon( m_Region.ToArray( ) );
            m_Drawer.Clip = new Region( Path );
        }
    }
}