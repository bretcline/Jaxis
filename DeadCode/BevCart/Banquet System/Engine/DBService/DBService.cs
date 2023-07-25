using System;
using System.ServiceProcess;
using Jaxis.Util.Log4Net;
using BevServiceDll;

namespace DBService
{
    public partial class DBService : ServiceBase
    {
        static bool m_Stop = false;
        private System.Threading.Thread m_WCFServiceTrd = null;
        ServiceDll m_Worker = new ServiceDll();
        
        public DBService()
        {
            InitializeComponent();
#if DEBUG
            OnStart(null);
#endif
        }

        protected override void OnStart(string[] args)
        {
            Log.Write("OnStart::SnapperService is Starting", LogType.Information);

            m_WCFServiceTrd = new System.Threading.Thread(new System.Threading.ThreadStart(StartThread));
            m_Worker.Start();
            m_WCFServiceTrd.Start();
        }

        protected override void OnStop()
        {
            Log.Write("OnStop::SnapperService is Stopping", LogType.Information);

            m_WCFServiceTrd.Join();

            if (null != m_Worker)
            {
                m_Worker.Stop();
            }
        }

        protected static void StartThread()
        {
            while (true != m_Stop)
            {
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
