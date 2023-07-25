using System;
using System.Data;
using System.Collections.Generic;

namespace JaxisPubSubManager
{
    public partial class PersistentSubscribers
    {
        private const string DEFAULT_XML_FILE = "Default.xml";

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
        public int NumberOfRows
        {
            get
            {
                return tablePersistentSubscribersXML.Rows.Count;
            }
        }

        /// <summary>
        /// Property that returns the first row of the XML data set
        /// </summary>
        public DataRow FirstRow
        {
            get
            {
                return tablePersistentSubscribersXML.Rows[0];
            }
        }

        /// <summary>
        /// Add a new row to the XML data set
        /// </summary>
        /// <param name="_row"></param>
        //public PersistentSubscribersXMLRow AddRow( PersistentSubscribersXMLRow _row )
        //{
        //    return AddRow( _row.Operation, _row.Contract, _row.Address );
        //}

        /// <summary>
        /// Add a new row to the XML data set.
        /// TODO: Determine if we need to return a DataRow object; not sure it's necessary
        /// </summary>
        /// <param name="_operation"></param>
        /// <param name="_contract"></param>
        /// <param name="_address"></param>
        /// <returns></returns>
        //public PersistentSubscribersXMLRow AddRow( string _operation, string _contract, string _address )
        //{
        //    return tablePersistentSubscribersXML.AddPersistentSubscribersXMLRow( _operation, _contract, _address );
        //}

        /// <summary>
        /// Add a new subscriber to the persistent list
        /// </summary>
        /// <param name="_address"></param>
        /// <param name="_eventsContract"></param>
        /// <param name="_eventOperation"></param>
        /// <returns></returns>
        public bool AddPersistent( string _address, string _contract, string _operation )
        {
            if( !ContainsPersistent( _address, _contract, _operation ) )
            {
                tablePersistentSubscribersXML.AddPersistentSubscribersXMLRow( _operation, _contract, _address );
                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_address"></param>
        /// <param name="_eventsContract"></param>
        /// <param name="_eventOperation"></param>
        /// <returns></returns>
        public bool ContainsPersistent( string _address, string _eventsContract, string _eventOperation )
        {
            string[] addresses = GetSubscribersToContractEventOperation( _eventsContract, _eventOperation );
            Predicate<string> exists = delegate( string addressToMatch )
            {
                return addressToMatch == _address;
            };

            return Array.Exists( addresses, exists );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_address"></param>
        /// <param name="_eventsContract"></param>
        /// <param name="_eventOperation"></param>
        /// <returns></returns>
        public bool RemovePersistent( string _address, string _contract, string _operation )
        {
            if( !ContainsPersistent( _address, _contract, _operation ) )
            {
                PersistentSubscribersXMLRow removeRow = tablePersistentSubscribersXML.NewPersistentSubscribersXMLRow( );
                removeRow.Address = _address;
                removeRow.Contract = _contract;
                removeRow.Operation = _operation;
                tablePersistentSubscribersXML.RemovePersistentSubscribersXMLRow( removeRow );

                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_row"></param>
        /// <returns></returns>
        public bool RemovePersistent( PersistentSubscribersXMLRow _row )
        {
            return RemovePersistent( _row.Address, _row.Contract, _row.Operation );
        }

        /// <summary>
        /// Determine if row exists in the XML data set.
        /// All parameters do not have to be used; empty strings may be sent.
        /// However, at least one string needs to be provided in order to perform the search.
        /// </summary>
        /// <param name="_operation">Operation to search for in XML data</param>
        /// <param name="_contract">Contract to search for in XML data</param>
        /// <param name="_address">Address to search for in XML data</param>
        /// <returns>True if row exists; False if the row doesn't exist</returns>
        public PersistentSubscribersXMLRow RowExists( string _operation, string _contract, string _address )
        {
            return RowExists( XmlFileName, _operation, _contract, _address );
        }

        /// <summary>
        /// Determine if row exists in the XML data set.
        /// All parameters do not have to be used; empty strings may be sent.
        /// However, at least one string needs to be provided in order to perform the search.
        /// </summary>
        /// <param name="_fileName">Name of XML file to read</param>
        /// <param name="_operation">Operation to search for in XML data</param>
        /// <param name="_contract">Contract to search for in XML data</param>
        /// <param name="_address">Address to search for in XML data</param>
        /// <returns></returns>
        public PersistentSubscribersXMLRow RowExists( string _fileName, string _operation, string _contract, string _address )
        {
            if( string.IsNullOrEmpty( _operation ) && string.IsNullOrEmpty( _contract ) && string.IsNullOrEmpty( _address ) )
            {
                return null;
            }

            PersistentSubscribersXMLDataTable table = new PersistentSubscribersXMLDataTable( );
            table.ReadXml( _fileName );
            //KDW - Work in progress
            //table.Where( );

            //foreach( PersistentSubscribersXMLRow row in table.Rows )
            //{
            //    if( ( row.Operation == _operation ) || ( row.Contract == _contract ) || ( row.Address == _address ) )
            //    {
            //        return row;
            //    }
            //}

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataSet XMLDataSet
        {
            get
            {
                return tablePersistentSubscribersXML.DataSet;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void SaveSubscribers( )
        {
            if( string.IsNullOrEmpty( XmlFileName ) )
            {
                SaveSubscribers( DEFAULT_XML_FILE );
            }
            else
            {
                SaveSubscribers( XmlFileName );
            }
        }

        /// <summary>
        /// Write XML data to the provided file.
        /// </summary>
        /// <param name="_fileName"></param>
        public void SaveSubscribers( string _fileName )
        {
            tablePersistentSubscribersXML.WriteXml( _fileName );
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
        /// <param name="_FileName"></param>
        private void ReadXML( string _fileName )
        {
            tablePersistentSubscribersXML.ReadXml( _fileName );
        }

        //internal T[] GetPersistentList( string _eventOperation )
        //{
        //    string[] addresses = GetSubscribersToContractEventOperation( typeof( T ).ToString( ), _eventOperation );
        //    List<T> subscribers = new List<T>( addresses.Length );

        //    return subscribers.ToArray( );
        //}

        public string[] GetSubscribersToContractEventOperation( string _contract, string _operation )
        {
            return new string[] { };
        }

        public List<string> GetAllSubscribers( )
        {
            List<string> subscribers = new List<string>( NumberOfRows );
            ReadXML( );
            foreach( PersistentSubscribers.PersistentSubscribersDBRow row in tablePersistentSubscribersXML.Rows )
            {
                subscribers.Add( row.Address );
            }

            return subscribers;
        }
    }
}