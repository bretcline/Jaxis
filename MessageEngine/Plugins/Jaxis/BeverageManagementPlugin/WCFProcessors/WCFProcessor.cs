using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using Jaxis.BeverageManagement.Plugin.PourDataService;
using Jaxis.Interfaces;
using Jaxis.Engine.Base.Device;
using Jaxis.Interfaces.Tags;
using Jaxis.Inventory.Data;
using Jaxis.MessageLibrary;
using Jaxis.Util.Log4Net;

namespace Jaxis.BeverageManagement.Plugin.WCFProcessors
{
    class WCFProcessor : BaseDevice, IConsumer
    {

        [Obfuscation(Exclude = true)]
        public static DeviceConfig GetDefaultDeviceConfig()
        {
            DeviceConfig rc = new DeviceConfig();
            System.Reflection.Assembly Asm = System.Reflection.Assembly.GetExecutingAssembly();
            rc.AssemblyName = Asm.ManifestModule.Name;
            rc.AssemblyType = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName;
            rc.AssemblyVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(BaseDevice.GetPath() + "\\" + rc.AssemblyName).FileVersion;
            rc.ID = Guid.NewGuid().ToString();
            rc.Name = "Identec Detach Filter";
            rc.Type = DeviceType.DataConsumer;
            rc.State = DeviceState.Stopped;
            rc.ProducerMessageType = 0;
            rc.ConsumerMessageType = 64;
            rc.Options.Add(new DeviceConfigOption
            {
                Name = "Config Name",
                Value = "BeverageManager"
            });
            rc.Options.Add(new DeviceConfigOption
            {
                Name = "WCF Path",
                Value = "http://localhost:8223/HostWCFService/PourEngineService/"
            });
            return rc;
        }

        private int m_SkipCount = 0;
        private DateTime m_SkipTime = DateTime.Now;
        private TimeSpan m_SkipInterval = new TimeSpan(0, 0, 0, 30);

        private PourEngineServiceClient m_Client = null;

        protected System.Threading.Thread m_Worker = null;
        protected double m_Timeout = 0;
        protected TimeSpan m_ReadWindow;

        protected readonly string LOG_TYPE;


                
        public WCFProcessor( )
            : this(GetDefaultDeviceConfig())
        {
        }


        public WCFProcessor(IDeviceConfig _config)
            : base( _config )
        {
            try
            {
                LOG_TYPE = this.GetType().Name;
                State = Config.State;
            }
            catch( Exception exp )
            {
                Log.WriteException(LOG_TYPE, "UIProcessor::UIProcessor", exp);
            }
        }

        override public void Start( )
        {
            try
            {
                State = DeviceState.Started;
                Config.State = DeviceState.Started;
                m_Stop = false;

                string configName = Config.Options.Where(i => i.Name == "Config Name").FirstOrDefault().Value;
                string wcfPath = Config.Options.Where(i => i.Name == "WCF Path").FirstOrDefault().Value;

                m_Client = new PourEngineServiceClient( configName, wcfPath );
                m_Client.Open();

            }
            catch( Exception exp )
            {
                Log.WriteException(LOG_TYPE, "UIProcessor:: Start", exp);
            }
        }

        override public void Stop( )
        {
            try
            {
                State = DeviceState.Stopped;
                Config.State = DeviceState.Stopped;
            }
            catch( Exception exp )
            {
                Log.WriteException(LOG_TYPE, "UIProcessor:: Stop", exp);
            }
            finally
            {
                m_Stop = true;
            }
        }

        public override string Consume( IMessage _message )
        {
            string rc = null;
            try
            {
                if (_message is ActivityLog )
                {
                    ProcessWCFCommand(_message as ActivityLog, (_client, _msg) => _client.PushActivityLog(_msg.GetInternalData()));
                }
                else if( _message is TagAlert)
                {
                    ProcessWCFCommand(_message as TagAlert, (_client, _msg) => _client.PushTagAlert(_msg.GetInternalData()));
                }
                else if (_message is TagActivity)
                {
                    ProcessWCFCommand(_message as TagActivity, (_client, _msg) => _client.PushTagActivity(_msg.GetInternalData()));
                }
                else if (_message is Pour)
                {
                    ProcessWCFCommand(_message as Pour, (_client, _msg) => _client.PushPourData(_msg.GetInternalData()));
                }
                else if (_message is CalcPour)
                {
                    ProcessWCFCommand(_message as CalcPour, (_client, _msg) => _client.PushCalcPourData(_msg));
                }
                else if (_message is DeviceAlert)
                {
                    ProcessWCFCommand(_message as DeviceAlert, (_client, _msg ) => _client.PushDeviceAlert(_msg.GetInternalData()));
                }

            }
            catch( Exception exp )
            {
                Log.WriteException( LOG_TYPE, "DataConsumer::Consume", exp );
            }
            return rc;
        }

        /// <summary>
        /// This is to keep the engine from trying to hard to send data to the UI if the UI isnt running.
        /// </summary>
        /// <param name="_process"></param>
        private void ProcessWCFCommand<T>(T _message, Action<PourEngineServiceClient, T> _process)
        {
            try
            {
                m_SkipTime = DateTime.Now;
                {
                    TimeSpan timeout = m_Client.InnerChannel.OperationTimeout;

                    try
                    {
                        m_Client.InnerChannel.OperationTimeout = new TimeSpan(0, 0, 0, 2, 0);
                        m_Client.Ping();
                        m_SkipCount = 0;
                        m_Client.InnerChannel.OperationTimeout = timeout;

                        _process(m_Client, _message);
                    }
                    catch
                    {
                        m_Client.Close();
                        ++m_SkipCount;
//                        Log.Debug(string.Format("ProcessWCFCommand - Failure Count {0}", m_SkipCount));

                        string configName = Config.Options.Where(i => i.Name == "Config Name").FirstOrDefault().Value;
                        string wcfPath = Config.Options.Where(i => i.Name == "WCF Path").FirstOrDefault().Value;

                        m_Client = new PourEngineServiceClient(configName, wcfPath);
                        m_Client.Open();

                    }
                    finally
                    {
                        m_Client.InnerChannel.OperationTimeout = timeout;
                    }
                }

            }
            catch
            {
            }
        }
    }
}