using System;
using System.ServiceModel;
using EngineConfigVersionClientDll;
using EngineConfigWCF;
using Jaxis.Engine;
using Jaxis.Util.Log4Net;
using Jaxis.Interfaces;

namespace Jaxis.Engine
{
    public class EngineServiceDll
    {
        ServiceHost m_Host;
        ServiceHost m_EngineConfigHost;

        private System.Threading.Thread m_ServiceTrd = null;
        EngineConfigVersionClient m_Worker = new EngineConfigVersionClient( );

        private static IEngine m_Engine;
        private bool m_Stop;

        public void Stop( )
        {
            try
            {
                if( null != m_Engine )
                {
                    m_Engine.Stop( );
                    StopEngineConfigWCFService( );
                    StopEngineConfigVersionThread( );
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "EngineServiceDll::Stop", exp );
            }
            finally
            {
                m_Stop = true;
            }
        }

        public void Start( )
        {
            try
            {
                // MLF ... would like to put this in engine.cs but that makes it hard to access the engine object from the engine config wcf service
                if ("true" == System.Configuration.ConfigurationManager.AppSettings["EnableWCFConfigService"])
                {
                    System.Threading.Thread.Sleep(5000); // MLF must give PourEngineConfig ClientWCF service time to start before engine pings HostWCF service
                    StartEngineConfigWCFService();
                }
                if ("true" == System.Configuration.ConfigurationManager.AppSettings["EnableWCFConfigVersionService"])
                {
                    System.Threading.Thread.Sleep(5000); // MLF must give PourEngineConfig ClientWCF service time to start before engine pings HostWCF service
                    StartEngineConfigVersionThread();
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "Engine:: Engine", exp );
            }

            try
            {
                m_Stop = false;
                //ClientSimForm Client = new ClientSimForm();
                //Client.Show();
                m_Engine = EngineManager.GetEngine();
                EngineConfigService.SetEngine( m_Engine );
                if( null != m_Engine )
                {
                    System.Threading.Thread.Sleep( 5000 ); // MLF must give PourEngineConfig ClientWCF service time to start before engine pings HostWCF service
                    m_Engine.Start( );
                }
                while( true != m_Stop )
                {
                    try
                    {
                    }
                    catch( Exception exp )
                    {
                        Log.WriteException( "EngineServiceDll::Start-while", exp );
                    }
                    System.Threading.Thread.Sleep( 1500 );
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "EngineServiceDll::Start", exp );
            }
        }

        public void StartEngineConfigWCFService( )
        {
            if( m_EngineConfigHost != null )
            {
                m_EngineConfigHost.Close( );
            }
            try
            {
                m_EngineConfigHost = new ServiceHost( typeof( EngineConfigService ) );
                m_EngineConfigHost.Open( );
            }
            catch( Exception exp )
            {
                Log.WriteException( "Engine:: StartEngineConfigWCFService", exp );
            }
        }

        public void StopEngineConfigWCFService( )
        {
            if( m_EngineConfigHost != null )
            {
                m_EngineConfigHost.Close( );
                m_EngineConfigHost = null;
            }
        }

        public void StartEngineConfigVersionThread( )
        {
            try
            {
                m_ServiceTrd = new System.Threading.Thread( m_Worker.Start );
                m_ServiceTrd.Start( );
            }
            catch( Exception exp )
            {
                Log.WriteException( "Engine:: StartEngineConfigVersionThread", exp );
            }
        }

        public void StopEngineConfigVersionThread( )
        {
            if( null != m_ServiceTrd )
            {
                m_ServiceTrd.Join( );
            }
            if( null != m_Worker )
            {
                m_Worker.Stop( );
            }
        }

        public void RegisterDevice(IDevice _Device)
        {
            if (null != m_Engine)
            {
                m_Engine.RegisterDevice(_Device);
            }
        }

        public void StartDevice( string _DeviceID )
        {
            if (null != m_Engine)
            {
                m_Engine.StartDevice(_DeviceID);
            }
        }
    }
}