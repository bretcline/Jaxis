using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace WebWatcher
{
    public partial class frmMain : Form
    {
        protected WatcherSettings m_Settings;

        public frmMain( )
        {
            InitializeComponent( );
        }

        private void frmMain_Load( object sender, EventArgs e )
        {
            if( File.Exists( "Settings.xml" ) )
            {
                using( StreamReader Reader = new StreamReader( "Settings.xml" ) )
                {
                    m_Settings = DeserializeObject<WatcherSettings>( Reader.ReadToEnd( ) );
                }
            }
            else
            {
                m_Settings = new WatcherSettings( ) { Password = "SilverDoe@11", UserName = "oldhomestead", LoginURL = "http://irisval.com/RWC-web/login.jsf", RefreshInterval = 5 };
            }

            this.Visible = false;
            frmLogin Login = new frmLogin( m_Settings );
            if( DialogResult.OK == Login.ShowDialog( ) )
            {
                using( StreamWriter Writer = new StreamWriter( "Settings.xml" ) )
                {
                    SerializeObject<WatcherSettings>( Writer, m_Settings );

                    frmWatcher Watcher = new frmWatcher( m_Settings );
                    Watcher.MdiParent = this;
                    Watcher.Show( );

                }
            }
            else
            {
                this.Close( );
            }
        }

        public static T DeserializeObject<T>( String pXmlizedString ) where T : class
        {
            try
            {
                XmlSerializer xs = new XmlSerializer( typeof( T ) );
                UTF8Encoding encoding = new UTF8Encoding( );
                Byte[] byteArray = encoding.GetBytes( pXmlizedString );
                using( MemoryStream memoryStream = new MemoryStream( byteArray ) )
                {
                    return (T)xs.Deserialize( memoryStream );
                }
            }
            catch( Exception exp )
            {
                return null;
            }
        }

        public static void SerializeObject<T>( StreamWriter Writer, T data ) where T : class
        {
            try
            {
                XmlSerializer xs = new XmlSerializer( typeof( T ) );
                UTF8Encoding encoding = new UTF8Encoding( );
                xs.Serialize( Writer, data );
            }
            catch( Exception exp )
            {
            }
        }

        private void btnAddWatcher_Click( object sender, EventArgs e )
        {
            frmWatcher Watcher = new frmWatcher( m_Settings );
            Watcher.MdiParent = this;
            Watcher.Show( );
        }
    }
}