using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace JaxisPubSubManager
{
    public class DataConversion
    {
        /// <summary>
        /// Convert a data table into an array of structures
        /// </summary>
        /// <param name="subscribers"></param>
        /// <returns>Array of PersistentSubscription structures</returns>
        public static PersistentSubscription[] Convert( PersistentSubscribers.PersistentSubscribersDBDataTable _subscribers )
        {
            Converter<PersistentSubscribers.PersistentSubscribersDBRow, PersistentSubscription> converter;
            converter = delegate( PersistentSubscribers.PersistentSubscribersDBRow row )
            {
                PersistentSubscription subscription = new PersistentSubscription( );
                subscription.Address = row.Address;
                subscription.Operation = row.Operation;
                subscription.Contract = row.Contract;
                return subscription;
            };

            return ConvertToArray( _subscribers, converter );
        }

        /// <summary>
        /// Convert an XML data table into an array of structures
        /// </summary>
        /// <param name="subscribers"></param>
        /// <returns></returns>
        public static PersistentSubscription[] Convert( PersistentSubscribers.PersistentSubscribersXMLDataTable _subscribers )
        {
            Converter<PersistentSubscribers.PersistentSubscribersXMLRow, PersistentSubscription> converter;
            converter = delegate( PersistentSubscribers.PersistentSubscribersXMLRow row )
            {
                PersistentSubscription subscription = new PersistentSubscription( );
                subscription.Address = row.Address;
                subscription.Operation = row.Operation;
                subscription.Contract = row.Contract;
                return subscription;
            };

            return ConvertToArray( _subscribers, converter );
        }

        /// <summary>
        /// Convert a data table into an array of structures
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_subscribers"></param>
        /// <returns></returns>
        //public static PersistentSubscription[] Convert<T,R>( T _subscribers ) where R : DataRow
        //{
        //    Converter<T, PersistentSubscription> converter;
        //    converter = delegate( T row )
        //    {
        //        PersistentSubscription subscription = new PersistentSubscription( );
        //        subscription.Address = row.Address;
        //        subscription.EventOperation = row.Operation;
        //        subscription.EventsContract = row.Contract;
        //        return subscription;
        //    };

        //    return ConvertToArray( _subscribers, converter );
        //}

        /// <summary>
        /// Convert the given data table into an array
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="_dataTable"></param>
        /// <param name="_converter"></param>
        /// <returns></returns>
        public static T[] ConvertToArray<R,T>(DataTable _dataTable, Converter<R,T> _converter ) where R : DataRow
        {
            //If we don't have any data, return an empty array
            if( 0 == _dataTable.Rows.Count )
            {
                return new T[] { };
            }

            //Create a new list then iterate through the generated collection and add each element to the list
            List<T> list = new List<T>( );
            IEnumerable<T> collection = GenericConvert( _dataTable.Rows, _converter );
            using( IEnumerator<T> iterator = collection.GetEnumerator( ) )
            {
                while( iterator.MoveNext( ) )
                {
                    list.Add( iterator.Current );
                }
            }

            return list.ToArray( );
        }

        /// <summary>
        /// Iterate through the given collection and convert each element
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="_collection"></param>
        /// <param name="_converter"></param>
        /// <returns></returns>
        public static IEnumerable<U> GenericConvert<T, U>( IEnumerable _collection, Converter<T, U> _converter )
        {
            if( null == _collection )
            {
                //TODO: Do something here if we have a null collection
            }

            foreach( object item in _collection )
            {
                yield return _converter( (T)item );
            }
        }
    }
}
