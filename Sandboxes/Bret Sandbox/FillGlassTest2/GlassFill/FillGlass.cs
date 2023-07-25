using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Jaxis.Controls.GlassFill
{
    public partial class FillGlass : UserControl
    {
        private int m_Increment = 0;
        private float m_Step = 0.0f;
        protected PictureManager m_Manager = new PictureManager( );

        public FillGlass( )
        {
            InitializeComponent( );
            timer1.Interval = 15;

            FillLevel = 100;

            m_Manager.AddPictures( GlassTypes.Wine, new PictureContainer( ) { Height = 233, Empty = picWineEmpty, Dark = picWineDark, Light = picWineLight } );
            m_Manager.AddPictures( GlassTypes.Shot, new PictureContainer( ) { Height = 334, Empty = picShotEmpty, Dark = picShotDark, Light = picShotLight } );
            m_Manager.AddPictures( GlassTypes.Beer, new PictureContainer( ) { Height = 369, Empty = picBeerEmpty, Dark = picBeerDark, Light = picBeerLight } );
        }

        public int FillLevel { get; set; }

        private void timer1_Tick( object sender, EventArgs e )
        {
            m_Manager.Active.Empty.Height = m_Manager.Active.Height - (int)Math.Floor( m_Increment * m_Step );

            if( m_Increment >= FillLevel )
            {
                timer1.Stop( );
            }
            m_Increment++;
        }

        public void Fill( )
        {
            m_Manager.Active.Empty.Height = m_Manager.Active.Height;
            m_Step = m_Manager.Active.Empty.Height / 100.0f;

            m_Increment = 1;
            timer1.Start( );
        }

        public void Reset( )
        {
            m_Manager.Active.Empty.Height = m_Manager.Active.Height;
            m_Manager.Active.Empty.Visible = true;
        }

        public bool DarkLiquid
        {
            get
            {
                return m_Manager.Dark;
            }
            set
            {
                m_Manager.Dark = value;
            }
        }

        protected GlassTypes m_GlassType;

        public GlassTypes GlassType
        {
            set
            {
                m_GlassType = value;
                m_Manager.SelectContainer( m_GlassType, false );
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

    public class PictureContainer
    {
        public int Height { get; set; }

        public PictureBox Empty { get; set; }

        public PictureBox Full { get; set; }

        public PictureBox Light { get; set; }

        public PictureBox Dark { get; set; }
    }

    public class PictureManager
    {
        protected bool m_Dark = false;

        public bool Dark
        {
            get
            {
                return m_Dark;
            }
            set
            {
                m_Dark = value;
                if( null != m_Active )
                {
                    if( m_Dark )
                    {
                        m_Active.Full = m_Active.Dark;
                        m_Active.Light.Visible = false;
                    }
                    else
                    {
                        m_Active.Full = m_Active.Light;
                        m_Active.Dark.Visible = false;
                    }
                    m_Active.Full.Visible = true;
                    m_Active.Empty.Visible = true;
                }
            }
        }

        protected Dictionary<GlassTypes, PictureContainer> m_Pictures = new Dictionary<GlassTypes, PictureContainer>( );

        protected PictureContainer m_Active = null;

        public PictureContainer Active
        {
            get
            {
                return m_Active;
            }
            set
            {
                m_Active = value;
            }
        }

        public void AddPictures( GlassTypes _Type, PictureContainer _Container )
        {
            m_Pictures[_Type] = _Container;
        }

        public void SelectContainer( GlassTypes _Type, bool _Dark )
        {
            foreach( GlassTypes GType in m_Pictures.Keys )
            {
                if( GType == _Type )
                {
                    Active = m_Pictures[GType];
                }
                m_Pictures[GType].Empty.Visible = false;
                m_Pictures[GType].Dark.Visible = false;
                m_Pictures[GType].Light.Visible = false;
            }
            Dark = _Dark;
            Active.Full.Visible = true;
            Active.Empty.Visible = true;
        }
    }
}