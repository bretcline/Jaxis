using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Net.Sockets;

namespace AccountValidator
{
    public partial class frmAccountEntry : DevExpress.XtraEditors.XtraForm
    {
        public frmAccountEntry( )
        {
            InitializeComponent( );
        }

        private void btnSubmit_Click( object sender, EventArgs e )
        {
            string Tag = String.Format("<Alien-RFID-Tag><TagID>{0}</TagID><DiscoveryTime/><LastSeenTime>{1}</LastSeenTime></Alien-RFID-Tag>", txtAccountKey.Text, DateTime.Now);

            using( TcpClient Client = new TcpClient( ) )
            {
                Client.Connect( "localhost", 4000 );

                using( NetworkStream networkStream = Client.GetStream( ) )
                {
                    using( System.IO.StreamWriter Writer = new System.IO.StreamWriter( networkStream ) )
                    {
                        Writer.WriteLine( Tag );
                        Writer.Flush( );
                    }
                }
            }
        }

        //static public void Main( string[] Args )
        //{
        //    TCPClient socketForServer;
        //    try
        //    {
        //        socketForServer = new TCPClient( "localHost", 10 );
        //    }
        //    catch
        //    {
        //        Console.WriteLine(
        //        "Failed to connect to server at {0}:999", "localhost" );
        //        return;
        //    }
        //    NetworkStream networkStream = socketForServer.GetStream( );
        //    System.IO.StreamWriter streamWriter = new System.IO.StreamWriter( networkStream );
        //    try
        //    {
        //        string outputString;
        //        // read the data from the host and display it
        //        {
        //            outputString = streamReader.ReadLine( );
        //            Console.WriteLine( outputString );
        //            streamWriter.WriteLine( "Client Message" );
        //            Console.WriteLine( "Client Message" );
        //            streamWriter.Flush( );
        //        }
        //    }
        //    catch
        //    {
        //        Console.WriteLine( "Exception reading from Server" );
        //    }
        //    // tidy up
        //    networkStream.Close( );
        //}
    }
}