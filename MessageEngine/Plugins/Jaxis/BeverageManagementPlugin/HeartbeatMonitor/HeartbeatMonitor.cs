using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Jaxis.Engine.Base.Device;
using Jaxis.Interfaces;
using Jaxis.Inventory.Data;
using Jaxis.MessageLibrary;
using Jaxis.Util.Log4Net;
using ITag = Jaxis.Inventory.Data.ITag;

namespace Jaxis.BeverageManagement.Plugin
{
    class HeartbeatMonitor : BaseBevManDevice
    {
        /*
            <DeviceConfig>
                <AssemblyName>Jaxis.BeverageManagement.Plugin.dll</AssemblyName>
                <AssemblyType>Jaxis.BeverageManagement.Plugin.HeartbeatMonitor</AssemblyType>
                <AssemblyVersion>1.0</AssemblyVersion>
                <ID>101</ID>
                <Name>Jaxis Heartbeat Monitor</Name>
                <Type>DataConsumer</Type>
                <State>Started</State>
                <ProducerMessageType>0</ProducerMessageType>
                <ConsumerMessageType>1</ConsumerMessageType>
                <Options>
                    <string>http://localhost:8223/HostWCFService/PourEngineService/</string>
                    <string>10</string> <!-- Missing Bottle Interval -->
                    <string></string> <!-- Connection String -->
                </Options>
            </DeviceConfig>

        */

        [Obfuscation(Exclude = true)]
        public static DeviceConfig GetDefaultDeviceConfig()
        {
            var rc = new DeviceConfig();
            var asm = Assembly.GetExecutingAssembly();
            rc.AssemblyName = asm.ManifestModule.Name;
            rc.AssemblyType = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName;
            rc.AssemblyVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(GetPath() + "\\" + rc.AssemblyName).FileVersion;
            rc.ID = Guid.NewGuid().ToString();
            rc.Name = "Jaxis Heartbeat Monitor";
            rc.Type = DeviceType.DataConsumer;
            rc.State = DeviceState.Stopped;
            rc.ProducerMessageType = 0;
            rc.ConsumerMessageType = 1;
            var option1 = new DeviceConfigOption
                              {Name = "WCFPath", Value = "http://localhost:8223/HostWCFService/PourEngineService/"};
            rc.Options.Add(option1);
            var option2 = new DeviceConfigOption {Name = "MissingBottleInterval", Value = "10"};
            rc.Options.Add(option2);
            var option3 = new DeviceConfigOption {Name = "ConnectionString", Value = ""};
            rc.Options.Add(option3);
            return rc;
        }

        private readonly Dictionary<string, PhaseMessage> m_PhaseMessages = new Dictionary<string, PhaseMessage>();
        private new readonly Dictionary<string, ITag> m_Alerts = new Dictionary<string, ITag>();
        private Thread m_Monitor;
        private readonly object m_PhaseLock = new object();
                
        public HeartbeatMonitor( )
            : this(GetDefaultDeviceConfig())
        {
        }


        public HeartbeatMonitor( IDeviceConfig _config )
            : base( _config )
        {
        }

        public string ConnectionString { get { return m_ConnectionString; } }

        public override void Start( )
        {
            try
            {
                m_StandardNozzle = BLManagerFactory.Get().ManageStandardNozzles().Get(Guid.Empty);
                LoadAllTags();

                State = DeviceState.Started;
                Config.State = DeviceState.Started;

                m_Monitor = new Thread( MonitorHeartbeats );
                m_Monitor.Start();
            }
            catch( Exception exp )
            {
                Log.WriteException( "DataConsumer::Start", exp );
            }
        }

        public override void Stop( )
        {
            m_Stop = true;
            State = DeviceState.Stopped;
            Config.State = DeviceState.Stopped;
        }

        public override string Consume( IMessage _message )
        {
            try
            { 
                if( _message is PhaseMessage )
                {
                    var msg = _message as PhaseMessage;
                    lock (m_PhaseLock)
                    {
                        m_PhaseMessages[msg.TagID] = msg;
                        if (m_Alerts.ContainsKey(msg.TagID))
                        {
                            m_Alerts.Remove(msg.TagID);
                        }
                    }
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "DataConsumer :: DataConsumer::Consume", exp );
            }
            return null;
        }

        protected void MonitorHeartbeats( )
        {
            var interval = DateTime.Now.AddMinutes(Config.GetMissingBottleInterval()*-1);

            while( false == m_Stop )
            {
                lock (m_PhaseLock)
                {
                    var tags = m_PhaseMessages.Values.Where( T => T.ReadTime < interval );

                    foreach (var phaseMessage in tags)
                    {
                        try
                        {
                            if (!m_Alerts.ContainsKey(phaseMessage.TagID))
                            {
                                string upcName = "Unknown";
                                var device = GetDevice(phaseMessage.DeviceID);
                                var tag = GetTagForHeartbeat(phaseMessage.TagID, device.Name);
                                var upc = GetUpcByTagNumber(tag);
                                if (null != upc)
                                {
                                    upcName = upc.Name;
                                }
                                m_Alerts[tag.TagNumber] = tag;

                                var alertMan = DataManagerFactory.Get().Manage<ITagAlert>();
                                var alert = alertMan.Create();
                                alert.TagID = tag.TagID;
                                alert.LocationID = tag.LocationID;
                                alert.AlertType = (int)AlertTypes.TagNotReporting;
                                alert.AlertTime = DateTime.Now;
                                alert.DeviceID = device.DeviceID;

                                alert.Message = string.Format("Tag {0} on {2} has not been seen since {1}",
                                                              tag.TagNumber,
                                                              phaseMessage.ReadTime, upcName);
                                alertMan.Save(alert);
                                ProduceMessage(alert);
                            }
                        }
                        catch (Exception err )
                        {
                            Log.WriteException( "MonitorHeartbeats", err);
                        }
                    }
                }
                Thread.Sleep(1000);
            }
        }
    }
}