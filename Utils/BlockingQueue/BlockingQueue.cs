using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Jaxis.Threading
{

    public class BlockingQueue<t>
    {
        #region Fields

        protected Queue<t> m_Queue = null;

        private AutoResetEvent ArrivalEvent = new AutoResetEvent( false );

        #endregion Fields

        #region Constructors

        public BlockingQueue( int _Size )
        {
            m_Queue = new Queue<t>( _Size );
        }

        #endregion Constructors

        #region Properties

        public int Count
        {
            get
            {
                int rc = 0;
                lock( m_Queue )
                {
                    rc = m_Queue.Count;
                }
                return rc;
            }
        }

        #endregion Properties

        #region Methods

        public void Clear( )
        {
            lock( m_Queue )
            {
                m_Queue.Clear( );
            }
        }

        public t Dequeue( int _Timeout )
        {
            object rc = null;
            if( 0 < Count || true == ArrivalEvent.WaitOne( _Timeout, false ) )
            {
                if( 0 < Count )
                {
                    lock( m_Queue )
                    {
                        rc = m_Queue.Dequeue( );
                    }
                }
            }
            return (t)rc;
        }

        public t Dequeue( )
        {
            return Dequeue( -1 );
        }

        public void Enqueue( t _Item )
        {
            lock( m_Queue )
            {
                m_Queue.Enqueue( _Item );
                ArrivalEvent.Set( );
            }
        }

        #endregion Methods
    }
}
