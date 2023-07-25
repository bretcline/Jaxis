using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using JaxisPubSubManager.PersistentSubscribersTableAdapters;

namespace JaxisPubSubManager
{
    public class SubscriptionManager<T> where T : class
    {
        static Dictionary<string, List<T>> m_TransientStore;

        static SubscriptionManager()
        {
            m_TransientStore = new Dictionary<string, List<T>>();
            string[] methods = GetOperations( );

            Action<string> insert = delegate(string methodName)
            {
                m_TransientStore.Add( methodName, new List<T>() );
            };

            Array.ForEach(methods, insert);
        }

        #region Transient Subscription Methods
        static string[] GetOperations()
        {
            MethodInfo[] methods = typeof(T).GetMethods(BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.Instance);
            List<string> operations = new List<string>(methods.Length);

            Action<MethodInfo> add = delegate(MethodInfo method)
            {
                Debug.Assert(!operations.Contains(method.Name));
                operations.Add( method.Name );
            };

            Array.ForEach( methods, add );
            return operations.ToArray( );
        }

        /// <summary>
        /// Transient Method.
        /// Retrieve list of all subscribers to the provided operation.
        /// </summary>
        /// <param name="_eventOperation"></param>
        /// <returns></returns>
        internal static T[] GetSubscriberList(string _eventOperation)
        {
            lock (typeof(SubscriptionManager<T>))
            {
                Debug.Assert( m_TransientStore.ContainsKey( _eventOperation ) );
                if (m_TransientStore.ContainsKey(_eventOperation))
                {
                    List<T> list = m_TransientStore[_eventOperation];
                    return list.ToArray( );
                }

                return new T[] { };
            }
        }

        /// <summary>
        /// Transient Method.
        /// Add a subscriber to the list.
        /// </summary>
        /// <param name="_subscriber"></param>
        /// <param name="_eventOperation"></param>
        static void AddSubscriber(T _subscriber, string _eventOperation)
        {
            lock (typeof(SubscriptionManager<T>))
            {
                List<T> list = m_TransientStore[_eventOperation];
                if (list.Contains(_subscriber))
                {
                    return;
                }

                list.Add( _subscriber );
            }
        }

        /// <summary>
        /// Transient method.
        /// Remove subscriber from the list.
        /// </summary>
        /// <param name="_subscriber"></param>
        /// <param name="_eventOperation"></param>
        static void RemoveSubscriber(T _subscriber, string _eventOperation)
        {
            lock (typeof(SubscriptionManager<T>))
            {
                List<T> list = m_TransientStore[_eventOperation];
                list.Remove( _subscriber );
            }
        }

        /// <summary>
        /// Transient Subscription Method
        /// </summary>
        /// <param name="_eventOperation"></param>
        public void Subscribe(string _eventOperation)
        {
            lock (typeof(SubscriptionManager<T>))
            {
                T subscriber = OperationContext.Current.GetCallbackChannel<T>();
                 if (String.IsNullOrEmpty(_eventOperation) == false)
                {
                    AddSubscriber( subscriber, _eventOperation );
                }
                else
                {
                    string[] methods = GetOperations( );
                    Action<string> addTransient = delegate(string methodName)
                    {
                        AddSubscriber( subscriber, methodName );
                    };

                    Array.ForEach( methods, addTransient );
                }
            }
        }

        /// <summary>
        /// Persistent Subscription Method
        /// </summary>
        /// <param name="_address"></param>
        /// <param name="_eventsContract"></param>
        /// <param name="_eventOperation"></param>
        /// //[OperationBehavior( TransactionScopeRequired = true )]
        public void Subscribe( string _address, string _eventsContract, string _eventOperation )
        {
            VerifyAddress( _address );

            if( String.IsNullOrEmpty( _eventOperation ) == false )
            {
                AddPersistent( _address, _eventsContract, _eventOperation );
            }
            else
            {
                string[] methods = GetOperations( );
                Action<string> addPersistent = delegate( string methodName )
                {
                    AddPersistent( _address, _eventsContract, methodName );
                };

                Array.ForEach( methods, addPersistent );
            }
        }

        /// <summary>
        /// Persistent Unsubscribe Method
        /// </summary>
        /// <param name="_address"></param>
        /// <param name="_eventsContract"></param>
        /// <param name="_eventOperation"></param>
        //[OperationBehavior( TransactionScopeRequired = true )]
        public void Unsubscribe( string _address, string _eventsContract, string _eventOperation )
        {
            VerifyAddress( _address );

            if( String.IsNullOrEmpty( _eventOperation ) == false )
            {
                RemovePersistent( _address, _eventsContract, _eventOperation );
            }
            else
            {
                string[] methods = GetOperations( );
                Action<string> removePersistent = delegate( string methodName )
                {
                    RemovePersistent( _address, _eventsContract, methodName );
                };
                Array.ForEach( methods, removePersistent );
            }
        }

        /// <summary>
        /// Transient Unsubscribe Method
        /// </summary>
        /// <param name="_eventOperation"></param>
        public void Unsubscribe( string _eventOperation )
        {
            lock (typeof(SubscriptionManager<T>))
            {
                T subscriber = OperationContext.Current.GetCallbackChannel<T>();
                if (String.IsNullOrEmpty(_eventOperation) == false)
                {
                    RemoveSubscriber( subscriber, _eventOperation );
                }
                else
                {
                    string[] methods = GetOperations( );
                    Action<string> removeTransient = delegate(string methodName)
                    {
                        RemoveSubscriber( subscriber, methodName );
                    };

                    Array.ForEach( methods, removeTransient );
                }
            }
        }
        #endregion

        #region Persistent Subscription Methods
        
        /// <summary>
        /// Verify that the address provided is of valid format
        /// </summary>
        /// <param name="_address"></param>
        /// <returns></returns>
        static bool VerifyAddress( string _address )
        {
            bool bRetCode = false;

            if (_address.StartsWith("http:") || _address.StartsWith("https:"))
            {
                bRetCode = true;
            }
            else if (_address.StartsWith("net.tcp:"))
            {
                bRetCode = true;
            }
            else if (_address.StartsWith("net.pipe:"))
            {
                bRetCode = true;
            }
            else if (_address.StartsWith("net.msmq:"))
            {
                bRetCode = true;
            }

            return (bRetCode);
        }

        /// <summary>
        /// Determine binding based on prefix of address and create an appropriate binding object 
        /// </summary>
        /// <param name="_address"></param>
        /// <returns></returns>
        static Binding GetBindingFromAddress( string _address )
        {
            if (_address.StartsWith("http:") || _address.StartsWith("https:"))
            {
                WSHttpBinding binding = new WSHttpBinding(SecurityMode.Message, true);
                binding.ReliableSession.Enabled = true;
                binding.TransactionFlow = true;
                return binding;
            }

            if (_address.StartsWith("net.tcp:"))
            {
                NetTcpBinding binding = new NetTcpBinding(SecurityMode.Message, true);
                binding.ReliableSession.Enabled = true;
                binding.TransactionFlow = true;
                return binding;
            }

            if (_address.StartsWith("net.pipe:"))
            {
                NetNamedPipeBinding binding = new NetNamedPipeBinding();
                binding.TransactionFlow = true;
                return binding;
            }

            if (_address.StartsWith("net.msmq:"))
            {
                NetMsmqBinding binding = new NetMsmqBinding();
                binding.Security.Mode = NetMsmqSecurityMode.None;
                return binding;
            }

            Debug.Assert( false, "Unsupported protocol specified" );
            return null;
        }

        /// <summary>
        /// Determine if subscriber exists
        /// </summary>
        /// <param name="_address"></param>
        /// <param name="_eventsContract"></param>
        /// <param name="_eventOperation"></param>
        /// <returns></returns>
        static bool ContainsPersistent( string _address, string _eventsContract, string _eventOperation )
        {
            string[] addresses = GetSubscribersToContractEventOperation( _eventsContract, _eventOperation );
            Predicate<string> exists = delegate( string addressToMatch )
            {
                return addressToMatch == _address;
            };
            return Array.Exists( addresses, exists );
        }

        /// <summary>
        /// Add a subscriber
        /// </summary>
        /// <param name="_address"></param>
        /// <param name="_eventsContract"></param>
        /// <param name="_eventOperation"></param>
        static void AddPersistent( string _address, string _eventsContract, string _eventOperation )
        {
            if( ContainsPersistent( _address, _eventsContract, _eventOperation ) )
            {
                return;
            }
            
            PersistentSubscribersDBTableAdapter adapter = new PersistentSubscribersDBTableAdapter( );
            adapter.Insert( _address, _eventOperation, _eventsContract );
        }

        /// <summary>
        /// Remove a subscriber
        /// </summary>
        /// <param name="_address"></param>
        /// <param name="_eventsContract"></param>
        /// <param name="_eventOperation"></param>
        static void RemovePersistent( string _address, string _eventsContract, string _eventOperation )
        {
            PersistentSubscribersDBTableAdapter adapter = new PersistentSubscribersDBTableAdapter( );

            PersistentSubscribers.PersistentSubscribersDBDataTable subscribers = adapter.GetSubscribersByAddressContractOperation( _address, _eventsContract, _eventOperation );
            foreach( PersistentSubscribers.PersistentSubscribersDBRow subscriber in subscribers )
            {
                adapter.Delete( subscriber.Address, subscriber.Operation, subscriber.Contract, subscriber.ID );
            }
        }

        /// <summary>
        /// Retrieve an array of subscribers based on the provided operation
        /// </summary>
        /// <param name="_eventOperation"></param>
        /// <returns></returns>
        internal static T[] GetPersistentList( string _eventOperation )
        {
            string[] addresses = GetSubscribersToContractEventOperation( typeof( T ).ToString( ), _eventOperation );

            List<T> subscribers = new List<T>( addresses.Length );

            foreach( string address in addresses )
            {
                Binding binding = GetBindingFromAddress( address );
                T proxy = ChannelFactory<T>.CreateChannel( binding, new EndpointAddress( address ) );
                subscribers.Add( proxy );
            }
            return subscribers.ToArray( );
        }

        /// <summary>
        /// Retrieve subscribers to the contract based on the provided operation
        /// </summary>
        /// <param name="_eventsContract"></param>
        /// <param name="_eventOperation"></param>
        /// <returns></returns>
        static string[] GetSubscribersToContractEventOperation( string _eventsContract, string _eventOperation )
        {
            PersistentSubscribers.PersistentSubscribersDBDataTable subscribers = new PersistentSubscribers.PersistentSubscribersDBDataTable( );
            PersistentSubscribersDBTableAdapter adapter = new PersistentSubscribersDBTableAdapter( );
            subscribers = adapter.GetSubscribersToContractOperation( _eventsContract, _eventOperation );

            Converter<PersistentSubscribers.PersistentSubscribersDBRow, string> extract = delegate( PersistentSubscribers.PersistentSubscribersDBRow row )
            {
                return row.Address;
            };

            return DataConversion.ConvertToArray( subscribers, extract );
        }

        //[OperationBehavior( TransactionScopeRequired = true )]
        public PersistentSubscription[] GetAllSubscribers( )
        {
            PersistentSubscribers.PersistentSubscribersDBDataTable subscribers = new PersistentSubscribers.PersistentSubscribersDBDataTable( );
            PersistentSubscribersDBTableAdapter adapter = new PersistentSubscribersDBTableAdapter( );
            subscribers = adapter.GetAllSubscribers( );
            
            //return DataConversion.Convert<PersistentSubscribers.PersistentSubscribersDBDataTable, PersistentSubscribers.PersistentSubscribersDBRow>( subscribers );
            return DataConversion.Convert( subscribers );
        }
        #endregion
    }
}