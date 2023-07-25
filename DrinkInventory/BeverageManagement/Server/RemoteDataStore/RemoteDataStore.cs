using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using HostWCFService;
using Jaxis.Inventory.Data;
using Jaxis.Util.Log4Net;

namespace Jaxis.BeverageManagement.Server
{
    public class RemoteDataStore
    {
        private System.Threading.Thread m_ServiceTrd = null;
        ServiceHost m_Host;

        public void StartWCFService()
        {
            if (m_Host != null)
            {
                m_Host.Close();
            }
            try
            {
                m_Host = new ServiceHost(typeof(PourEngineService));
                m_ServiceTrd = new System.Threading.Thread(m_Host.Open);
                m_ServiceTrd.Start();

                LiveDataStore.Get().AddData += AddNewData;
            }
            catch (Exception exp)
            {
                Log.WriteException("HostSimForm.StartWCFService()", exp);
            }
        }

        private bool AddNewData(object _data)
        {
            return Log.Wrap<bool>("", LogType.Debug, false, () =>
            {
                bool rc = false;
                if (_data is IWCFDataElement)
                {
                    var data = _data as IWCFDataElement;
                    var t = data.CreateDBObject();
                    if (null != t)
                    {
                        t.Save();
                        rc = true;
                    }
                }
                return rc; 
            });
        }
    }
}
