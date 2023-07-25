using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Jaxis.Engine.Base.Device;
using Jaxis.Interfaces;
using Jaxis.Readers.POS;
using Jaxis.Readers.POS.Parsers;
using Jaxis.Util.Log4Net;

namespace POSReaderImporter
{
    class POSReaderImport : BaseProducerDevice, IProducer
    {
        [Obfuscation(Exclude = true)]
        public static DeviceConfig GetDefaultDeviceConfig()
        {
            var rc = new DeviceConfig();
            System.Reflection.Assembly Asm = System.Reflection.Assembly.GetExecutingAssembly();
            rc.AssemblyName = Asm.ManifestModule.Name;
            rc.AssemblyType = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName;
            rc.AssemblyVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(BaseDevice.GetPath() + "\\" + rc.AssemblyName).FileVersion;
            rc.ID = Guid.NewGuid().ToString();
            rc.Name = "POS File Reader";
            rc.Type = DeviceType.DataProducer;
            rc.State = DeviceState.Stopped;
            rc.ProducerMessageType = 16;
            rc.ConsumerMessageType = 0;
            var option1 = new DeviceConfigOption {Name = "Filename", Value = @"F:\Journals\septembre\30092015.jnl" };
            rc.Options.Add(option1);
            var option2 = new DeviceConfigOption {Name = "Timeout", Value = "10"};
            rc.Options.Add(option2);
            var option3 = new DeviceConfigOption {Name = "Config", Value = @"C:\Source\Jaxis\trunk\MessageEngine\TestHarness\POSReaderImporter\ParserConfig.xml" };
            rc.Options.Add(option3);
            return rc;
        }

        private string ByteToHex(byte[] comByte)
        {
            var builder = new StringBuilder(comByte.Length * 3);
            foreach (byte data in comByte)
            {
                builder.Append(Convert.ToString(data, 16).PadLeft(2, '0').PadRight(3, ' '));
            }
            return builder.ToString().ToUpper();
        }


        private const int BUFFER_SIZE = 256;
        private readonly string LOG_TYPE = "POSReaderImport";
        private readonly string LOG_HEARTBEAT = "POSReaderHeartbeat";

        protected Thread m_ActivityThread = null;
        protected IParser m_Parser = null;


        public POSReaderImport()
            : this(GetDefaultDeviceConfig())
        {
        }

        public POSReaderImport(IDeviceConfig _config)
            : base(_config)
        {
            LOG_TYPE = this.GetType().Name;
            Log.Info(LOG_TYPE, string.Format("Create {0}", LOG_TYPE));
        }

        public override void Stop()
        {
            Log.Wrap<int>(LOG_TYPE, "POSReaderImport::Stop", LogType.Debug, true, () =>
            {
                m_Stop = true;
                return 1;
            });
        }

        public override void Start()
        {
            Log.Wrap<int>(LOG_TYPE, "POSReaderImport::Start", LogType.Debug, true, () =>
            {
                m_Stop = false;

                string parseConfig = m_DeviceConfig.Options[2].Value;

                bool appendToTicket = m_DeviceConfig.GetAppendToTicket();
                Log.Debug(LOG_TYPE, string.Format("{1} Parser: {0}", parseConfig, appendToTicket));

                m_Parser = new Generic(parseConfig, appendToTicket);

                // Startup thread to check for activity.
                m_ActivityThread = new Thread(ReadFile);
                m_ActivityThread.Start();
                // Receive a message and write it to the console.

                return 1;
            });
        }

        private void ReadFile()
        {
            Log.Wrap<int>(LOG_TYPE, "POSReaderImport::ReadSocket", LogType.Debug, false, () =>
            {
                if (false == m_Stop)
                {
                    var KeysToRemove = new Queue<string>();
                    {
                        var files = Directory.GetFiles(m_DeviceConfig.Options[0].Value, "*.JNL");
                        foreach (var file in files)
                        {
                            var success = true;
                            m_Parser.ClearCache();
                            using (var reader = new StreamReader(file))
                            {
                                Log.Info( file );
                                StringBuilder Builder = new StringBuilder();
                                while (!m_Stop && !reader.EndOfStream)
                                {
                                    Builder.Append(ProcessFile(reader));
                                    {
                                        if (null != m_Parser && Builder.Length > 0)
                                        {
                                            try
                                            {
                                                Log.Debug(LOG_TYPE, Builder.ToString());
                                                var T = m_Parser.ParseData(Builder.ToString());
                                                T.DataSource = Path.GetFileName( file );
                                                if (string.IsNullOrWhiteSpace(T.Establishment))
                                                {
                                                    T.Establishment = m_DeviceConfig.GetBarID();
                                                }
                                                Log.Debug(LOG_TYPE, string.Format("POSDriver::ProduceMessage() {0}{1}", System.Environment.NewLine, T.ToString()));

                                                if (T.CheckNumber == null)
                                                {
                                                    Log.Debug("Null Check Number");
                                                }


                                                //if (T.Items.Count > 5 * T.GuestCount)
                                                //{
                                                //    Console.WriteLine($"Way to many ticket items {T.Items.Count} for {T.GuestCount} on ticket {T.CheckNumber}");
                                                //}

                                                ProduceMessage(T);
                                            }
                                            catch (Exception err)
                                            {
                                                Log.Exception(LOG_TYPE, err);
                                                Log.Info( LOG_TYPE, Builder.ToString());
                                                success = false;
                                            }
                                            Builder.Clear();
                                        }
                                        else if (null == m_Parser)
                                        {
                                            Log.Error("Parser is Invalid!");
                                        }
                                    }
                                }
                            }
                            if (success)
                            {
                                File.Move(file, file + ".processed");
                            }
                        }
                    }
                }
                return 1;
            });
        }

        private string ProcessFile(StreamReader _reader)
        {
            var builder = new StringBuilder();
            try
            {
                //bool ticketComplete = false;
                bool isTicket = false;

                while (!_reader.EndOfStream)
                {
                    var line = _reader.ReadLine();
                    if (line != null)
                    {
                        if( line.StartsWith("Tbl"))
                        {
                            isTicket = true;
                        }
                        else if (line.Contains("================================") && isTicket )
                        {
                            break;
                        }
                        if (isTicket)
                        {
                            builder.Append($"{line}{Environment.NewLine}");
                        }
                    }

                }


                //var line = _reader.ReadLine();
                //while (null != line && false == ticketComplete )
                //{
                //    if (line.StartsWith("Tbl"))
                //    {
                //        isTicket = true;
                //        builder.Clear();
                //        builder.Append(string.Format("{0}{1}", line, Environment.NewLine));                        
                //    }
                //    else if (isTicket)
                //    {
                //        if (line.Contains("================================"))
                //        {
                //            ticketComplete = true;
                //        }
                //        else
                //        {
                //            builder.Append( string.Format("{0}{1}", line, Environment.NewLine));                        
                //            line = _reader.ReadLine();
                //        }
                //    }
                //}
            }
            catch (Exception err)
            {
                Log.Exception(LOG_TYPE, err);
            }
            return builder.ToString();
        }
    }
}
