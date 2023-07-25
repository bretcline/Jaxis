using System;
using System.Collections.Generic;
using System.Reflection;
using Jaxis.Interfaces;
using Jaxis.Util.Log4Net;
using System.Threading.Tasks;

namespace Jaxis.Engine.Base.Device
{
    public abstract class BaseDevice : IDisposable, IDevice
    {
        protected object m_Locker = new object();

        private Task m_Monitor;

        protected IDeviceConfig m_DeviceConfig;

        public IDeviceConfig Config { get { return m_DeviceConfig; } }

        public List<IFilter> Filters { get; protected set; }

        protected DateTime m_LastMessage = DateTime.Now;

        protected bool m_Stop;

        protected DeviceState m_InitialState;
        public DeviceState InitalState { get { return m_InitialState; } }



        public static string GetPath()
        {
            string rc = string.Empty;
            {
                rc = System.Reflection.Assembly.GetExecutingAssembly().Location;
                //m_Path = GetType().Assembly.Location;
                if (rc.Contains("\\"))
                {
                    rc = rc.Remove(rc.LastIndexOf('\\'));
                }
            }
            Log.Debug(string.Format("Assembly Path - {0}", rc));
            return rc;
        }


        //protected static DeviceConfig GetBaseDeviceConfig()
        //{
        //    var rc = new DeviceConfig();
        //    Assembly asm = Assembly.GetExecutingAssembly();
        //    rc.AssemblyType = MethodBase.GetCurrentMethod().DeclaringType.FullName;
        //    rc.AssemblyName = asm.ManifestModule.Name;
        //    //rc.AssemblyVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(rc.AssemblyName).FileVersion;
        //    rc.ID = Guid.NewGuid().ToString();
        //    return rc;
        //}

        public BaseDevice( IDeviceConfig _Config )
        {
            DeviceConfig tmp = _Config as DeviceConfig;
            //tmp.AssemblyVersion = this.GetType( ).Assembly.GetName( ).Version.ToString( );
            //tmp.AssemblyName = this.GetType( ).Name;
            //tmp.AssemblyType = this.GetType( ).FullName;

            m_InitialState = tmp.State;

            m_DeviceConfig = tmp;

            Filters = new List<IFilter>( );
        }

        public virtual string Consume( IMessage _message )
        {
            string rc = null;
            return rc;
        }

        public DeviceState State { get; set; }

        public DeviceType Type { get; set; }

        public string HardwareID { get; set; }

        public abstract void Start( );

        public abstract void Stop( );

        //public static DeviceConfig GetDefaultDeviceConfig();

        protected Action m_StatusMonitor = null;
        public Action StatusMonitor
        {
            set
            {
                lock( m_Locker )
                {
                    if (null != value && null == m_Monitor)
                    {
                        try
                        {
                            m_StatusMonitor = value;
                            m_Monitor = Task.Factory.StartNew(m_StatusMonitor, TaskCreationOptions.AttachedToParent);
                            if (m_Monitor.Status != TaskStatus.Running)
                            {
                                m_Monitor.Start();
                            }
                        }
                        catch( Exception err )
                        {
                            Log.Exception( err );
                        }
                    }
                }
            }
        }    

        #region IDisposable Members

        public void Dispose( )
        {
            m_Stop = true;
            m_Monitor.Wait( 250 );
        }

        #endregion IDisposable Members
    }
}