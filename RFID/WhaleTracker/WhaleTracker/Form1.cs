using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Jaxis.Interfaces;
using JaxisEngine;
using JaxisEngine.Base.Device;
using Jaxis.Plugins.PureRF;


namespace WhaleTracker
{
    public partial class Form1 : Form
    {
        protected Engine m_Engine = null;
        public Form1( )
        {
            InitializeComponent( );

            IDeviceConfig Config = new DeviceConfig { Name = "Test Data Producer", ID = Guid.NewGuid( ).ToString( ) };
            TestDriver P = new TestDriver( Config );

            m_Engine = new Engine( );
            m_Engine.RegisterDevice( P );

            m_Engine.Start( );
        }
    }
}
