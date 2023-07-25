using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Xml.Linq;
using Jaxis.Util.Log4Net;
using System.Threading;

namespace ValidationEngine.Readers
{
    public abstract class BaseReader
    {
        protected IConfigData m_ConfigData = null;

        public BaseReader( IConfigData _Config )
        {
            m_ConfigData = _Config;
        }

        protected bool m_Running = false;

        abstract public void Start( );
        abstract public void Stop( );
        abstract protected void ActivityMonitor( );
    }
}
