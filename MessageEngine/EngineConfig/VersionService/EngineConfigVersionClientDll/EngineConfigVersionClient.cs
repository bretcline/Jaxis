using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EngineConfigVersionData;
using Jaxis.Engine.Base.Device;
using Jaxis.Util.Log4Net;

namespace EngineConfigVersionClientDll
{
    public class EngineConfigVersionClient
    {
        private bool m_Stop;

        public void Stop( )
        {
            try
            {
                m_Stop = true;
            }
            catch( Exception exp )
            {
                Log.WriteException( "EngineConfigVersionClient::Stop", exp );
            }
        }

        public void Start( )
        {
            try
            {
                m_Stop = false;
                while( true != m_Stop )
                {
                    try
                    {
                        SendVersionInfo( );
                        int TotalMilliSec = 0;
                        int Days = Convert.ToInt32( System.Configuration.ConfigurationManager.AppSettings["EnableWCFConfigVersionInterval"] );
                        // Spin for "Days"
                        while( true != m_Stop && TotalMilliSec < Days * 24 * 60 * 60 * 1000 )
                        {
                            System.Threading.Thread.Sleep( 1500 );
                            TotalMilliSec += 1500;
                        }
                    }
                    catch( Exception exp )
                    {
                        Log.WriteException( "EngineConfigVersionClient::Start-while", exp );
                    }
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "EngineConfigVersionClient::Start", exp );
            }
        }

        private void SendVersionInfo( )
        {
            try
            {
                DeviceConfigCollection Configs = null;

                string ConfigFile = System.Configuration.ConfigurationManager.AppSettings["ConfigFile"];
                using( StreamReader Reader = new StreamReader( ConfigFile ) )
                {
                    string Data = Reader.ReadToEnd( );
                    Configs = DeviceConfig.DeserializeObject<DeviceConfigCollection>( Data );
                    if( null != Configs )
                    {
                        foreach( DeviceConfig D in Configs.Configs )
                        {
                            EngineVersionData VersionData = new EngineVersionData( );
                            VersionData.AssemblyName = D.AssemblyName;
                            VersionData.AssemblyVersion = D.AssemblyVersion;
                            VersionData.EngineServiceAddress = System.Configuration.ConfigurationManager.AppSettings["EngineServiceAddress"];
                            using( EngineConfigVersionWCFServiceReference.ConfigVersionServiceClient WCFClient = new EngineConfigVersionWCFServiceReference.ConfigVersionServiceClient( ) )
                            {
                                WCFClient.SendCurrentVersion( VersionData );
                            }
                        }
                    }
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "EngineConfigVersionClient.SendVersionInfo()", exp );
            }
        }
    }
}