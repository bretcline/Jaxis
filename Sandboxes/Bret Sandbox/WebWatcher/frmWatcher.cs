using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace WebWatcher
{
    public partial class frmWatcher : Form
    {
        protected WatcherSettings m_Settings;
        Dictionary<string, string> m_Pages = new Dictionary<string, string>( );
        bool m_Watching = false;
        bool m_StartWatching = false;

        public frmWatcher( WatcherSettings _Settings )
        {
            InitializeComponent( );
            m_Settings = _Settings;
        }

        private void Form1_Load( object sender, EventArgs e )
        {
            this.Visible = true;
            wBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler( wBrowser_DocumentCompleted );
            wBrowser.Url = new System.Uri( m_Settings.LoginURL );
            wBrowser.Refresh( );
        }

        private void tmrRefresh_Tick( object sender, EventArgs e )
        {
            if( true == m_Watching )
            {
                wBrowser.Refresh( );
            }
            try
            {
                HtmlElement JobCount = wBrowser.Document.GetElementById( "form1:availJobCount" );
                if( null != JobCount )
                {
                    string Text = JobCount.InnerText;

                    int Count;
                    if( true == int.TryParse( Text, out Count ) )
                    {
                        if( 0 < Count )
                        {
                            try
                            {
                                Stop( );
                                HtmlElement Accept = wBrowser.Document.GetElementById( "form1:j_id139" );
                                if( null != Accept )
                                {
                                    Accept.InvokeMember( "click" );
                                }

                                Text = wBrowser.DocumentText;
                                using( StreamWriter Writer = new StreamWriter( "AcceptDoc." + DateTime.Now.Ticks + ".txt" ) )
                                {
                                    Writer.Write( Text );
                                }
                            }
                            catch
                            {
                            }
                            this.BringToFront( );
                            WindowFlasher.Flash( this, 15 );
                            SoundPlayer simpleSound = new SoundPlayer( @"Alert.wav" );
                            simpleSound.PlayLooping( );
                            System.Threading.Thread.Sleep( 5000 );
                            simpleSound.Stop( );
                        }
                    }
                }
            }
            catch
            {
                try
                {
                    wBrowser.Navigate( wBrowser.Url );
                }
                catch
                {
                    // Logout Button: "j_id10:j_id14:newCorr:j_id24"
                }
            }
        }

        void wBrowser_DocumentCompleted( object sender, WebBrowserDocumentCompletedEventArgs e )
        {
            if( m_StartWatching )
            {
                Start( );
                m_StartWatching = false;
            }

            if( wBrowser.Url.AbsolutePath.EndsWith( "login.jsf", true, System.Globalization.CultureInfo.CurrentCulture ) )
            {
                Stop( );
                HtmlElement Username = wBrowser.Document.GetElementById( "j_username" );
                if( null != Username )
                {
                    Username.InnerText = m_Settings.UserName;
                }
                HtmlElement Password = wBrowser.Document.GetElementById( "j_password" );
                if( null != Password )
                {
                    Password.InnerText = m_Settings.Password;
                    wBrowser.Document.GetElementById( "login" ).InvokeMember( "click" );
                }
            }
            else if( wBrowser.Url.AbsolutePath.EndsWith( "disclaimer.jsf", true, System.Globalization.CultureInfo.CurrentCulture ) )
            {
                Stop( );
                HtmlElement Accept = wBrowser.Document.GetElementById( "form1:j_id93" );
                if( null != Accept )
                {
                    Accept.InvokeMember( "click" );
                    m_StartWatching = true;
                }
            }
            else if( wBrowser.Url.AbsolutePath.EndsWith( "sessionTimeout.jsf", true, System.Globalization.CultureInfo.CurrentCulture ) )
            {
                wBrowser.Navigate( m_Settings.LoginURL );
            }

            Uri Path = wBrowser.Url;
            if( !m_Pages.ContainsKey( Path.AbsolutePath ) )
            {
                m_Pages.Add( Path.AbsolutePath, wBrowser.DocumentText );

                using( StreamWriter Writer = new StreamWriter( Path.Segments[Path.Segments.Length - 1] + "-" + DateTime.Now.Ticks + ".txt" ) )
                {
                    Writer.Write( wBrowser.DocumentText );
                }
            }
        }

        private void btnStopWatching_Click( object sender, EventArgs e )
        {
            if( true == m_Watching )
            {
                Stop( );
                btnStopWatching.Text = "Start Watching";
            }
            else
            {
                Start( );
                btnStopWatching.Text = "Stop Watching";
            }
        }

        private void Start( )
        {
            tmrRefresh_Tick( null, null );
            tmrRefresh.Interval = m_Settings.RefreshInterval * 1000;
            tmrRefresh.Start( );
            m_Watching = true;
        }

        private void Stop( )
        {
            m_Watching = false;
            tmrRefresh.Stop( );
        }
    }
}