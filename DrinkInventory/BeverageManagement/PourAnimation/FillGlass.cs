using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Jaxis.Controls.GlassFill
{
    public partial class FillGlass : UserControl
    {
        private int m_Increment = 0;
        private int m_FillAmount = 0;
        protected int m_Count = 0;
        protected double m_CurrentHeight = 0;
        private double m_Step = 0.0;

        private int m_StartHeight = 0;
        private int m_EndHeight = 326;

        public FillGlass( )
        {
            InitializeComponent( );
            timer1.Interval = 15;

            FillLevel = 100;

            m_EndHeight = picEmpty.Height;
        }

        protected double m_FillPercent = 100.0;
        protected int m_FillLevel = 100;

        public int FillLevel
        {
            get
            {
                return m_FillLevel;
            }
            set
            {
                m_FillLevel = value;
                m_FillPercent = m_FillLevel / 100.0;
            }
        }

        private void timer1_Tick( object sender, EventArgs e )
        {
            ++m_Count;

            picEmpty.Height = (int)m_CurrentHeight - ( m_Count * m_Increment );

            if( m_FillAmount <= m_Count * m_Increment )
            {
                timer1.Stop( );
            }
        }

        public void Fill( )
        {
            m_CurrentHeight = m_StartHeight;
            m_Step = Math.Abs( m_StartHeight - m_EndHeight ) / (float)m_FillLevel;
            picEmpty.BringToFront( );

            m_FillAmount = (int)( ( m_StartHeight - m_EndHeight ) * ( (double)m_FillLevel / 100.0 ) );
            m_Count = 0;

            timer1.Start( );
        }

        public void Reset( )
        {
            picEmpty.Height = 326;
            picEmpty.BringToFront( );
        }

        protected GlassTypes m_GlassType;

        public GlassTypes GlassType
        {
            set
            {
                m_GlassType = value;
                switch( m_GlassType )
                {
                    case GlassTypes.Beer:
                    {
                        m_Increment = 4;
                        picType.Image = Properties.Resources.Beer_full;
                        m_StartHeight = 326;
                        m_EndHeight = 0;
                        break;
                    }
                    case GlassTypes.Wine:
                    {
                        m_Increment = 2;
                        picType.Image = Properties.Resources.Wine_full;
                        m_StartHeight = 204;
                        m_EndHeight = 82;
                        break;
                    }
                    case GlassTypes.Shot:
                    {
                        m_Increment = 1;
                        picType.Image = Properties.Resources.Mixed_full;
                        m_StartHeight = 208;
                        m_EndHeight = 155;
                        break;
                    }
                }
                picEmpty.Height = m_StartHeight;
                picEmpty.BringToFront( );
            }

            get
            {
                return m_GlassType;
            }
        }
    }

    public enum GlassTypes
    {
        Beer,
        Wine,
        Shot,
    }
}