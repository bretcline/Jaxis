using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Windows.Forms;
using JaxisPubSubManager;

namespace TestClient
{
    public partial class Form1 : Form
    {
        private PersistentSubscribers m_PersistentSub;
        private LINQPersistentSubscribers m_subscribers;

        public Form1( )
        {
            m_PersistentSub = new PersistentSubscribers( );
            InitializeComponent( );
        }

        private void btnRead_Click( object sender, EventArgs e )
        {
            string DisplayMessage = string.Empty;

            m_PersistentSub.ReadXml( "c:\\temp\\Temp.xml" );
            PersistentSubscribers.PersistentSubscribersXMLRow row = m_PersistentSub.FirstRow as PersistentSubscribers.PersistentSubscribersXMLRow;
            DisplayMessage = row.ID.ToString( );
            DisplayMessage += "\n";
            DisplayMessage += row.Address.ToString( );
            DisplayMessage += "\n";
            DisplayMessage += row.Contract.ToString( );
            DisplayMessage += "\n";
            DisplayMessage += row.Operation.ToString( );
            MessageBox.Show( DisplayMessage );
        }

        private void btnWrite_Click( object sender, EventArgs e )
        {
            m_PersistentSub.WriteXml( "c:\\temp\\Temp.xml" );
        }

        private void btnAdd_Click( object sender, EventArgs e )
        {
            //if ( !m_PersistentSub.AddPersistent( "Operation", "Contract", "Address" ) )
            //{
            //    MessageBox.Show( "Adding subscriber failed" );
            //}

            m_subscribers.Subscribe( "Address1", "Contract1", "Operation1" );
        }

        private void btnDelete_Click( object sender, EventArgs e )
        {
        }

        private void btnInitialize_Click( object sender, EventArgs e )
        {
            m_subscribers = new LINQPersistentSubscribers( );
            //m_subscribers.InitializeXmlFile( );
        }

        private void btnGetAll_Click( object sender, EventArgs e )
        {
            //List<XElement> subscribers = m_subscribers.GetAllSubscribers( );
            //foreach( XElement element in subscribers )
            //{
            //    Console.WriteLine( element.ToString( ) );
            //}
        }

        private void btnDelete2_Click( object sender, EventArgs e )
        {
            m_subscribers.Unsubscribe( "Address1", "Contract1", "Operation1" );
        }
    }
}
