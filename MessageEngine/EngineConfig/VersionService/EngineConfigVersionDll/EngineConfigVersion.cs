using System;
using System.ServiceModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.Util.Log4Net;
using EngineConfigVersions;
using EngineConfigVersionData;
using EngineConfigVersionWCF;

namespace EngineConfigVersionDll
{
    public class EngineConfigVersion
    {
        ServiceHost m_Host;
        private bool m_Stop;

        public void Stop()
        {
            try
            {
                StopWCFService( );
                m_Stop = true;
            }
            catch( Exception exp )
            {
                Log.WriteException( "EngineConfigVersion::Stop", exp );
            }
            finally
            {
                m_Stop = true;
            }
        }

        public void Start()
        {
            StartWCFService();

            try
            {
                m_Stop = false;
                while (true != m_Stop)
                {
                    try
                    {
                        while (null != EngineVersions.GetNext())
                        {
#warning set client address here
                            using (EngineWCFConfigServiceReference.EngineConfigServiceClient Client = new EngineWCFConfigServiceReference.EngineConfigServiceClient())
                            {
                                //DeviceConfig Config = new DeviceConfig();
                                //Config.AssemblyName = tPlugInAssemblyName.Text;
                                //Config.AssemblyType = tPlugInAssemblyType.Text;
                                //Config.AssemblyVersion = tPlugInAssemblyVersion.Text;
                                //byte[] iv;
                                //byte[] encrypt = Encryption.Encrypt(System.IO.File.ReadAllBytes(tPlugIn.Text), out iv);
                                //foreach (string Dependent in m_DependentNames)
                                //{
                                //    DependentNames.Add(Dependent.Substring(openFileDialog1.FileName.LastIndexOf('\\') + 1));
                                //    Dependents.Add(Encryption.Encrypt(System.IO.File.ReadAllBytes(Dependent), out iv));
                                //}
                                //Client.DownloadDevicePlugin(Config, encrypt, DependentNames, Dependents, iv);
                            }
                        }
                    }
                    catch (Exception exp)
                    {
#warning need to add the failed version back in
                        Log.WriteException("EngineConfigVersion::Start-while", exp);
                    }
                    System.Threading.Thread.Sleep(1500);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException("EngineConfigVersion::Start", exp);
            }
        }

        public void StartWCFService()
        {
            if (m_Host != null)
            {
                m_Host.Close();
            }
            try
            {
                m_Host = new ServiceHost(typeof(ConfigVersionService));
                m_Host.Open();
            }
            catch (Exception exp)
            {
                Log.WriteException("EngineConfigVersion.StartWCFService()", exp);
            }
        }

        public void StopWCFService()
        {
            try
            {
                if (m_Host != null)
                {
                    m_Host.Close();
                    m_Host = null;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException("EngineConfigVersion.StopWCFService()", exp);
            }
        }
    }
}
