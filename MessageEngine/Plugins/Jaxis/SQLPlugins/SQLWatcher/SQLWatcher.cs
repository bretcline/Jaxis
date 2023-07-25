using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using Jaxis.Engine.Base.Device;
using Jaxis.Interfaces;
using Jaxis.Util.Log4Net;
using Jaxis.Utilities.Database;

namespace SQLWatcher
{
    /*
    <DeviceConfig>
        <AssemblyName>PureRFID.dll</AssemblyName>
        <AssemblyType>Jaxis.Readers.PureRFID.PureRFReader</AssemblyType>
        <AssemblyVersion>1.0</AssemblyVersion>
        <ID>123</ID>
        <Name>PureRFID Device Prod</Name>
        <Type>DataProducer</Type>
        <State>Started</State>
        <ConsumerMessageType>None</ConsumerMessageType>
        <ProducerMessageType>RawData</ProducerMessageType>
        <Options>
            <string>1</string> <!-- Sleep time -->
            <string>COM15</string> <!-- Com or IP -->
            <string>1</string> <!-- Device ID's (comma separated list of int's) -->
        </Options>
    </DeviceConfig>
    */

    public class SQLWatcher : AlertableProducerDevice, IProducer
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
            rc.Name = "SQL Watcher";
            rc.Type = DeviceType.DataProducer;
            rc.State = DeviceState.Stopped;
            rc.ProducerMessageType = 8;
            rc.ConsumerMessageType = 0;

            DeviceConfigOption Option = new DeviceConfigOption();
            Option.Name = "Connection String";
            Option.Value = "server=.;Integrated Security=true;database=BeverageMonitor;";
            rc.Options.Add(Option);

            Option = new DeviceConfigOption();
            Option.Name = "Database Table";
            Option.Value = "[dbo].[WellDailyProductionOFM]";
            rc.Options.Add(Option);

            Option = new DeviceConfigOption();
            Option.Name = "Column Name";
            Option.Value = "Oil";
            rc.Options.Add(Option);

            Option = new DeviceConfigOption();
            Option.Name = "Data Type";
            Option.Value = "Float";
            rc.Options.Add(Option);

            Option = new DeviceConfigOption();
            Option.Name = "Min Value";
            Option.Value = "0";
            rc.Options.Add(Option);

            Option = new DeviceConfigOption();
            Option.Name = "Max Value";
            Option.Value = "5000";
            rc.Options.Add(Option);

            Option = new DeviceConfigOption();
            Option.Name = "Query Interval (Seconds)";
            Option.Value = "1";
            rc.Options.Add(Option);

            Option = new DeviceConfigOption();
            Option.Name = "Retention Cycle";
            Option.Value = "5";
            rc.Options.Add(Option);
            return rc;
        }

        protected System.Threading.Thread m_Worker = null;

        public SQLWatcher()
            : this(GetDefaultDeviceConfig())
        {
        }

        public SQLWatcher(IDeviceConfig _Config)
            : base(_Config)
        {
        }

        public override void Stop()
        {
            Log.Wrap<int>("SQLWatcher::Stop", LogType.Debug, true, () =>
            {
                m_Stop = true;
                return 1;
            });
        }

        public override void Start()
        {
            Log.Wrap<int>("SQLWatcher::Start", LogType.Debug, true, () =>
            {
                m_Worker = new System.Threading.Thread(StartThread);
                m_Worker.Start();

                m_Stop = false;
                State = DeviceState.Started;
                Config.State = DeviceState.Started;

                return 1;
            });
        }

        protected void StartThread()
        {
            var retentionCycle = m_DeviceConfig.GetRetentionCycle();
            var retentionQueue = new Dictionary<string, int>();

            var timeout = m_DeviceConfig.GetQueryInterval()*1000;

            using (var conn = new SqlTool(m_DeviceConfig.GetConnectionString()))
            {
                var sql = BuildSqlStatement();
                var counter = 0;
                while (m_Stop == false)
                {
                    using (var reader = conn.ExecuteReader(sql))
                    {
                        while (reader.Read())
                        {
                            var value = reader.GetValue(0).ToString();

                            if (retentionQueue.ContainsKey(value))
                            {
                                retentionQueue[value]++;
                            }
                            else
                            {
                                var messageTypes = GetMessage(value);
                                var message = new SQLWatcherAlert { AlertMessage = messageTypes.Item2, AlertType = messageTypes.Item1 };
                                ProduceMessage(message);
                                retentionQueue[value] = 0;
                            }
                        }
                    }
                    var expired = retentionQueue.Where(v => v.Value > retentionCycle).ToList();
                    foreach (var keyValuePair in expired)
                    {
                        retentionQueue.Remove(keyValuePair.Key);
                    }
                    Thread.Sleep(timeout);
                }
            }
        }

        private Tuple<AlertTypes, string> GetMessage(string value)
        {
            var message = string.Empty;
            var messageType = AlertTypes.SqlHighValue;

            switch (m_DeviceConfig.GetDataType().ToUpper())
            {
                case "STRING":
                {
                    message = string.Format("The value {0} is not between {1} and {2}.", value,
                        m_DeviceConfig.GetMinValue(), m_DeviceConfig.GetMaxValue());
                    break;
                }
                case "INT":
                {
                    var val = Convert.ToInt32( value );
                    var min = Convert.ToInt32(m_DeviceConfig.GetMinValue());
                    var max = Convert.ToInt32(m_DeviceConfig.GetMaxValue());
                    if (val < min)
                    {
                        messageType = AlertTypes.SqlLowValue;
                        message = string.Format("The value {0} falls below {1}", value, min);
                    }
                    else if (val > max)
                    {
                        messageType = AlertTypes.SqlHighValue;
                        message = string.Format("The value {0} is above {1}", value, max);
                    }
                    break;
                }
                case "FLOAT":
                {
                    var val = Convert.ToDouble(value);
                    var min = Convert.ToDouble(m_DeviceConfig.GetMinValue());
                    var max = Convert.ToDouble(m_DeviceConfig.GetMaxValue());
                    if (val < min)
                    {
                        messageType = AlertTypes.SqlLowValue;
                        message = string.Format("The value {0} falls below {1}", value, min);
                    }
                    else if (val > max)
                    {
                        messageType = AlertTypes.SqlHighValue;
                        message = string.Format("The value {0} is above {1}", value, max);
                    }
                    break;
                }
            }

            return new Tuple<AlertTypes, string>( messageType, message);
        }

        private string BuildSqlStatement()
        {
            var sql = "SELECT {0} FROM {1} WHERE {0} < {2} OR {0} > {3}";

            switch (m_DeviceConfig.GetDataType().ToUpper())
            {
                case "STRING":
                {
                    sql = "SELECT {0} FROM {1} WHERE {0} < '{2}' OR {0} > '{3}'";
                    break;
                }
            }

            return string.Format(sql, m_DeviceConfig.GetColumnName(),
                m_DeviceConfig.GetTableName(), m_DeviceConfig.GetMinValue(), m_DeviceConfig.GetMaxValue());
        }

    }
}
