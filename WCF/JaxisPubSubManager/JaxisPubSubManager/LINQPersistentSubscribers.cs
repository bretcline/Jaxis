using System;
using System.Xml;
using System.Data;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Collections.Generic;

namespace JaxisPubSubManager
{
    public class LINQPersistentSubscribers
    {
        private const string DEFAULT_XML_FILE = "Default.xml";
        
        //Debug
        private const string TEST_XML_FILE = "c:\\temp\\testLINQ.xml";
        //

        private XDocument m_xmlDoc;

        public LINQPersistentSubscribers( )
        {
            m_xmlDoc = new XDocument( new XDeclaration( "1.0", "UTF-8", "yes" ) );
            m_xmlDoc.Add( new XElement( "Subscribers" ) );
            //Debug
            m_xmlDoc.Save( TEST_XML_FILE );
            //
        }

        public LINQPersistentSubscribers( string _xmlFileName )
        {
            XmlFileName = _xmlFileName;
            m_xmlDoc = new XDocument( new XDeclaration( "1.0", "UTF-8", "yes" ) );
            m_xmlDoc.Add( new XElement( "Subscribers" ) );
            m_xmlDoc.Save( XmlFileName );
        }

        /// <summary>
        /// Property that gets/sets the name of the XML file to be used
        /// </summary>
        public string XmlFileName
        {
            get;
            set;
        }

        /// <summary>
        /// Property that returns the number of rows in the XML data set
        /// </summary>
        public int NumberOfSubscribers
        {
            get
            {
                var query = m_xmlDoc.Descendants( "Subscribers" );
                return query.Count( );
            }
        }

        public XDocument XmlDocument
        {
            get
            {
                return m_xmlDoc.Document;
            }
        }

        public bool Subscribe( XElement _addElement )
        {
            if( !ContainsPersistent( _addElement ) )
            {
                m_xmlDoc.Root.Add( _addElement );
                SaveSubscribers( TEST_XML_FILE );
                return true;
            }

            return false;
        }

        public bool Subscribe( string _address, string _contract, string _operation )
        {
            XElement subscriber = new XElement( "Subscriber",
                                      new XElement( "ID", Guid.NewGuid( ) ),
                                      new XElement( "Address", _address ),
                                      new XElement( "Contract", _contract ),
                                      new XElement( "Operation", _operation ) );

            return ( Subscribe( subscriber ) );
        }

        /// <summary>
        /// Determines if subscriber currently exists in the XML data.
        /// </summary>
        /// <param name="_id">Guid of subscriber ID to use in the search.</param>
        /// <returns>XElement object that contains the provided ID; Empty XElement object if not found.</returns>
        public XElement ContainsPersistent( Guid _id )
        {
            var query = m_xmlDoc.Descendants( "Subscriber" )
                                .Where( x => x.Elements( "ID" )
                                              .Any( y => (Guid)y == _id ) );

            if( query.Count( ) <= 0 )
            {
                return ( new XElement( "Empty", null ) );
            }

            return ( query.ElementAt( 0 ) );
        }

        /// <summary>
        /// Determines if subscriber currently exists in the XML data.
        /// </summary>
        /// <param name="_element">XElement object to search for in XML data.</param>
        /// <returns>True if element exists; False otherwise.</returns>
        public bool ContainsPersistent( XElement _element )
        {
            //Strip out the ID of the subscriber and see if it exists in the current XML data
            string elemID = _element.Element( "ID" ).Value;
            Guid guidID = new Guid( elemID );
            XElement tempElem = ContainsPersistent( guidID );

            return ( !tempElem.IsEmpty );
        }

        /// <summary>
        /// Remove the record with the corresponding ID if it exists.
        /// </summary>
        /// <param name="_id">ID of record to remove</param>
        /// <returns>True if removal succeeded; False otherwise.</returns>
        public bool Unsubscribe( Guid _id )
        {
            XElement element = ContainsPersistent( _id );
            if( !element.IsEmpty )
            {
                element.Remove( );
                SaveSubscribers( TEST_XML_FILE );
                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_address"></param>
        /// <param name="_contract"></param>
        /// <param name="_operation"></param>
        /// <returns></returns>
        public bool Unsubscribe( string _address, string _contract, string _operation )
        {
            List<XElement> elemList = new List<XElement>( );
            elemList.Add( new XElement( _address ) );
            elemList.Add( new XElement( _contract ) );
            elemList.Add( new XElement( _operation ) );

            var query = m_xmlDoc.Descendants( "Subscriber" )
                                .Where( x => elemList.Contains( x ) );

            if( query.Count( ) > 0 )
            {
                Guid removeElem = new Guid( query.ElementAt( 0 ).Element( "ID" ).Value );
                return Unsubscribe( removeElem );
            }

            return false;
        }

        /// <summary>
        /// Save all current XML data to the provided file.
        /// </summary>
        /// <param name="_fileName"></param>
        public void SaveSubscribers( string _fileName )
        {
            m_xmlDoc.Save( _fileName );
        }

        /// <summary>
        /// Default ReadXML method.
        /// Reads the XML data set from file name property previously set.
        /// </summary>
        private void ReadXML( )
        {
            if( string.IsNullOrEmpty( XmlFileName ) )
            {
                ReadXML( DEFAULT_XML_FILE );
            }
            else
            {
                ReadXML( XmlFileName );
            }
        }

        /// <summary>
        /// Read XML data set from the provided file name.
        /// </summary>
        /// <param name="_FileName">File that contains the XML data to load</param>
        private void ReadXML( string _fileName )
        {
            m_xmlDoc = XDocument.Load( _fileName );
        }

        /// <summary>
        /// Get an array of all the subscription structures
        /// </summary>
        /// <returns></returns>
        public PersistentSubscription[] GetAllSubscribers( )
        {
            List<XElement> subscriberList = GetSubscriberList( );

            Converter<XElement, PersistentSubscription> converter;
            converter = delegate( XElement element )
            {
                PersistentSubscription subscription = new PersistentSubscription( );
                subscription.Address = element.Element( "Address" ).ToString();
                subscription.Contract = element.Element( "Contract" ).ToString( );
                subscription.Operation = element.Element( "Operation" ).ToString( );
                return subscription;
            };

            return ( subscriberList.ConvertAll<PersistentSubscription>( converter ).ToArray( ) );
        }

        /// <summary>
        /// Get a list of all subscribers(elements)
        /// </summary>
        /// <returns></returns>
        public List<XElement> GetSubscriberList( )
        {
            List<XElement> subscribers = new List<XElement>( );
            //Debug - change file name
            ReadXML( TEST_XML_FILE );
            //

            var query = m_xmlDoc.Element( "Subscribers" )
                                .Elements( );
            foreach( XElement element in query )
            {
                subscribers.Add( element );
            }

            return subscribers;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_contract"></param>
        /// <param name="_operation"></param>
        /// <returns></returns>
        public string[] GetSubscribersToContractOperation( string _contract, string _operation )
        {
            List<XElement> subscriberList = new List<XElement>( );

            var query = from i in m_xmlDoc.Elements( "Subscriber" )
                        where _contract.Equals( i.Element( "Contract" ).Value ) && _operation.Equals( i.Element( "Operation" ).Value )
                        select i;

            foreach( XElement element in query )
            {
                subscriberList.Add( element );
            }

            Converter<XElement, string> converter;
            converter = delegate( XElement element )
            {
                return ( element.Element( "Address" ).Value );
            };

            return ( subscriberList.ConvertAll<string>( converter ).ToArray( ) );
        }
    }
}
