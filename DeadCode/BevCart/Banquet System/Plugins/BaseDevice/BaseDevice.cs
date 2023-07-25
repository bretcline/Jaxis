using System;
using JaxisInterfaces;

namespace JaxisEngine.Base
{
    public abstract class BaseDevice :  IDisposable
    {
        protected IDeviceConfig m_DeviceConfig;
        public IDeviceConfig Config { get { return m_DeviceConfig; } }
        protected bool m_Stop;

        public BaseDevice( IDeviceConfig _Config )
        {
            m_DeviceConfig = _Config;
        }

        public virtual string Consume(IMessage _Message)
        {            
            string rc = null;
            return rc;
        }

        public DeviceState State { get; set; }

        public abstract void Start( );
        public abstract void Stop( );

        #region IDisposable Members

        public void Dispose( )
        {
            m_Stop = true;
        }

        #endregion
    }
}
