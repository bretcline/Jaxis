using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using COMWriter;

namespace WinCOM
{
    public partial class Form1 : Form
    {
        RFIDCom m_Device = null;

        public Form1( )
        {
            InitializeComponent( );
        }

        private void btnConnect_Click( object sender, EventArgs e )
        {
            m_Device = new RFIDCom( );

            m_Device.Open( );

            Array.ForEach( Enum.GetNames( typeof( PUKCommandType ) ), P => cmbCommands.Items.Add( P ) );

        }

        private void btnClose_Click( object sender, EventArgs e )
        {
            m_Device.Close( );
        }

        private void btnClear_Click( object sender, EventArgs e )
        {
            lstResults.Items.Clear( );
        }

        private void btnExecute_Click( object sender, EventArgs e )
        {
            PUKCommandType CmdType = (PUKCommandType)Enum.Parse( typeof( PUKCommandType ), cmbCommands.Text );

            byte Options = 0x00;
            if( !string.IsNullOrEmpty( txtOptions.Text ) )
            {
                Options = (byte)int.Parse( txtOptions.Text, System.Globalization.NumberStyles.HexNumber );
            }

            char[] Chars = txtData.Text.ToCharArray( );
            byte[] Data = new byte[txtData.Text.Length];

            for( int i = 0; i < Chars.Length; ++i )
            {
                Data[i] = (byte)Chars[i];
            }

            PUKCommand Cmd = new PUKCommand( CmdType, Options, Data );

            PUKCommand Resp = m_Device.SendMessage( Cmd );
            byte Error = 0xFF;
            while( 0xFF == Error )
            {
                Resp = m_Device.SendMessage( Cmd );
                //                Resp = Com.SendMessage( new PUKCommand( PUKCommandType.TIRIS_Read ) );
                Error = Resp.Options;
    
                StringBuilder Msg = new StringBuilder( );
                Resp.Parameters.ForEach( C => Msg.Append( (char)C ) );

                WriteToList( Msg.ToString( ) );
            }
            m_Device.ProcessResponse( Resp.CommandType, Resp, WriteToList );
            Resp.Parameters.ForEach( C => Console.Write( (char)C ) );
            Console.WriteLine( );
            Console.WriteLine( BitConverter.ToString( Resp.Parameters.ToArray( ) ) );
        }

        private void WriteToList( string _String )
        {
            if( lstResults.InvokeRequired )
            {
                Action<string> d = WriteToList;
                d.Invoke( _String );
            }
            else
            {
                lstResults.Items.Add( _String );
            }
        }

    }
}
