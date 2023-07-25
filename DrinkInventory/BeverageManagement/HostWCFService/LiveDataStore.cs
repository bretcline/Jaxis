using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Jaxis.Inventory.Data;
using Jaxis.Util.Log4Net;

namespace HostWCFService
{
    public class LiveDataStore
    {
        private static object m_Locker = new object();
        BlockingCollection<object> m_DataUpdaterCollection = new BlockingCollection<object>( );
        private static List<Task> m_TaskList = new List<Task>( );
        private Thread m_Processor = null;

        private static LiveDataStore m_LiveData = null;

        Dictionary<Type, IBindingList> m_DataStore = new Dictionary<Type, IBindingList>();
        private bool m_Running;

        public delegate bool DataAdded(object data);
        public event DataAdded AddData;

        protected LiveDataStore( )
        {
            Log.Debug("LiveDataStore::LiveDataStore( )");
            m_Running = true;

            m_TaskList.Add( Task.Factory.StartNew(ProcessData) );

            //m_Processor = new Thread( new ThreadStart( ProcessData ) );
            //m_Processor.Start();
        }

        private void ProcessData()
        {
            Log.Debug( "LiveDataStore::ProcessData( ) - Start Processing Data" );
            while( m_Running == true )
            {
                try
                {
                    object Value;

                    m_DataUpdaterCollection.TryTake(out Value, 1000);
                    if (null != Value && null != AddData)
                    {
                        //Log.Debug( "LiveDataStore::ProcessData( )" );
                        AddData(Value);
                    }
                    if( 50 < m_DataUpdaterCollection.Count && m_TaskList.Count < 10 )
                    {
                        lock (m_Locker)
                        {
                            m_TaskList.Add(Task.Factory.StartNew(ProcessData));
                        }
                    }
                }
                catch (Exception err)
                {
                    Log.WriteException( "LiveDataStore::ProcessData", err);
                }
            }
            Log.Debug( "LiveDataStore::ProcessData( ) - Stop Processing Data" );
        }

        public static void Stop( )
        {
            if( null != m_LiveData )
            {
                m_LiveData.m_Running = false;
                lock (m_Locker)
                {
                    foreach (var task in m_TaskList)
                    {
                        task.Wait(500);
                    }
                }
            }
        }

        public static LiveDataStore Get()
        {
            if (null == m_LiveData)
            {
                lock( m_Locker )
                {
                    m_LiveData = new LiveDataStore();
                }
            }
            return m_LiveData;
        }

        //public IBindingList Pull<T>()
        //{
        //    IBindingList rc = null;
        //    Type type = typeof( T );
        //    if( !m_DataStore.ContainsKey( typeof( T ) ) )
        //    {
        //        m_DataStore.Add( typeof( T ), new BindingList<T>( ) );
        //    }
        //    rc = m_DataStore[type];
        //    return rc;
        //}

        public void Push<T>( T data )
        {
            lock( m_DataStore )
            {
                //Type type = typeof (T);
                //if (!m_DataStore.ContainsKey(typeof (T)))
                //{
                //    m_DataStore.Add(typeof (T), new BindingList<T>());
                //}
                //m_DataStore[type].Add(data);
                //Log.Debug( string.Format( "{0} - {1}", typeof( T ).Name, m_DataStore[type].Count ) );

                m_DataUpdaterCollection.Add( data );
            }
        }
    }
}
