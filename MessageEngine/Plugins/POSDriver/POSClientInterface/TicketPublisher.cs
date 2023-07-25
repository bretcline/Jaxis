using System;
using System.Linq;
using Jaxis.Interfaces;

namespace Jaxis.Reader.POS
{
    class TicketPublisher
    {
        private static object m_Locker = new object( );
        protected static TicketPublisher m_Publisher = null;

        public Func<IMessage, string> Publish { get; set; }

        protected TicketPublisher( )
        {
        }

        public static TicketPublisher GetPublisher( )
        {
            lock( m_Locker )
            {
                if( null == m_Publisher )
                {
                    m_Publisher = new TicketPublisher( );
                }
            }
            return m_Publisher;
        }
    }
}
