using System;
using System.ServiceProcess;
using Jaxis.Util.Log4Net;
using EngineConfigVersionDll;

namespace EngineVersionService
{
    public partial class EngineVersionService : ServiceBase
    {
        private System.Threading.Thread m_ServiceTrd = null;
        EngineConfigVersion m_Worker = new EngineConfigVersion();

        public EngineVersionService()
        {
            InitializeComponent();
#if DEBUG
            this.OnStart(null);
#endif
        }

        protected override void OnStart(string[] args)
        {
            Log.Write("OnStart::EngineVersionService is Starting", LogType.Information);

            m_ServiceTrd = new System.Threading.Thread(m_Worker.Start);
            m_ServiceTrd.Start();
        }

        protected override void OnStop()
        {
            Log.Write("OnStop::EngineVersionService is Stopping", LogType.Information);

            m_ServiceTrd.Join();

            if (null != m_Worker)
            {
                m_Worker.Stop();
            }
        }
    }
}
