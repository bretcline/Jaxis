using System.ServiceProcess;
using System.Threading;
using Jaxis.Engine;
using Jaxis.Util.Log4Net;

namespace Jaxis.Engine
{
    public partial class EngineService : ServiceBase
    {
        private readonly EngineServiceDll m_Worker = new EngineServiceDll();
        private Thread m_ServiceTrd;

        public EngineService()
        {
            InitializeComponent();
#if DEBUG
            this.OnStart( null );
#endif
        }

        protected override void OnStart(string[] args)
        {
            Log.Write("OnStart::EngineService is Starting", LogType.Information);

            m_ServiceTrd = new Thread(m_Worker.Start);
            m_ServiceTrd.Start();
        }

        protected override void OnStop()
        {
            Log.Write("OnStop::EngineService is Stopping", LogType.Information);

            if (null != m_Worker)
            {
                m_Worker.Stop();
            }

            m_ServiceTrd.Join(2500);
        }
    }
}