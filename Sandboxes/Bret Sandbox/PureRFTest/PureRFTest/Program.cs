using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PureRF;

namespace PureRFTest
{
    class Program
    {
        PureRF.ReceiversManager m_Manager = null;
        string[] m_ReaderNames;

        static void Main( string[] args )
        {
            Program P = new Program( );
            P.Start( );
        }

        public void Start( )
        {
            m_ReaderNames = new string[] { "COM15-1" };
            m_Manager = new PureRF.ReceiversManager( );
            m_Manager.AddSerialLoop( "COM15", "COM15", 0xe100 );
            m_Manager.AddReceiver( "COM15-1", "COM15", 1 );

            m_Manager.SetEventCallback( new ReceiversManager.EventCallback( this.ReceiversManagerEvent ) );
            ReceiversManager.RetVal allResults = m_Manager.GetAllTags( m_ReaderNames, true );

            while( true == true )
            {
                System.Threading.Thread.Sleep( 1000 );
            }

            
        }

        private void ReceiversManagerEvent( ReceiversManager m, ReceiversManager.ProgressEvent e )
        {
            switch( e.EventID )
            {
                case ReceiversManager.ProgressEvent.ID.COMPLETED:
                {
                    this.RequestCompleted( e );

                    System.Threading.Thread.Sleep( 1000 );
                    m_Manager.GetAllTags( m_ReaderNames, true );
                    break;
                }
                default:
                {
                    break;
                }

            }
        }


        private void RequestCompleted( ReceiversManager.ProgressEvent e )
        {
            ReceiversManager.ResultSet set;
            ReceiversManager.RetVal allResults = m_Manager.GetAllResults( out set );
            if( allResults == ReceiversManager.RetVal.SUCCESS )
            {
                foreach( ReceiversManager.ManagedReceiver receiver in set.Keys )
                {
                    ReceiversManager.ReceiverResult result = set[receiver];
                    Receiver.Tag[] tagArray = (Receiver.Tag[])result.Result;
                    if( result.RetVal == ReceiverRetVal.SUCCESS )
                    {
                        foreach( PureRF.Receiver.Tag T in tagArray )
                        {
                            StringBuilder TagMessage = new StringBuilder( );
                            TagMessage.Append( string.Format( "TagID: {0} RSSI: {1} Reader:{2} Message Count:{3} Read Time:{4} {5}", T.tagID.GetPureRFTagID( ), T.RSSI, receiver.Name, T.transmissionIndex, T.ts.ToString( ), T.tagMsg ) );

                            Console.WriteLine( TagMessage );
                        }
                    }
                }
            }
        }
    }
}
