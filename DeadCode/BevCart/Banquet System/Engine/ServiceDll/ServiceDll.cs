using System;
using System.ServiceModel;
using Jaxis.Util.Log4Net;
using BevWCFDB;


namespace BevServiceDll
{
    public class ServiceDll
    {
        ServiceHost host;

        public void Start()
        {
            if (host != null)
            {
                host.Close();
            }
            try
            {
                host = new ServiceHost(typeof(WCFDB));
                host.Open();
            }
            catch (Exception exp)
            {
                Log.WriteException("ServiceDll.Start()", exp);
            }
        }

        public void Stop()
        {
            if (host != null)
            {
                host.Close();
                host = null;
            }
        }
    }
}
