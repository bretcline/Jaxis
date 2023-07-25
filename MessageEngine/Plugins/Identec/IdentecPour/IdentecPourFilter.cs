using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using IDENTEC.Tags;
using Jaxis.BeverageManagement.Plugin;
using Jaxis.Interfaces;
using Jaxis.Engine.Base.Device;
using Jaxis.Interfaces.Tags;
using Jaxis.MessageLibrary;
using Jaxis.Util.Log4Net;

namespace Jaxis.Readers.Identec
{
    /*
    <DeviceConfig>
      <AssemblyName>Identec.dll</AssemblyName>
      <AssemblyType>Jaxis.Readers.Identec.IdentecPourFilter</AssemblyType>
       <AssemblyVersion>1.0</AssemblyVersion>
       <ID>321</ID>
         <Name>Identec Detach Filter</Name>
         <Type>DataProducerConsumer</Type>
         <State>Started</State>
         <ConsumerMessageType>TagMessage</ConsumerMessageType>
         <ProducerMessageType>RawData</ProducerMessageType>
         <Options>
            <!-- ReadWindow -->
            <string>1.5</string>
         </Options>
     </DeviceConfig>
    */

    public class IdentecPourFilter : BaseProducerDevice, IProducer, IConsumer
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
            rc.Name = "Identec Pour Filter";
            rc.Type = DeviceType.DataProducerConsumer;
            rc.State = DeviceState.Stopped;
            rc.ProducerMessageType = 8;
            rc.ConsumerMessageType = 512;
            return rc;
        }

        protected System.Threading.Thread m_Worker = null;
        protected double m_Timeout = 0;
        protected TimeSpan m_ReadWindow;
        protected Dictionary<string, BaseBevManDevice.TimedElement<IdentecPour>> m_TagList = new Dictionary<string, BaseBevManDevice.TimedElement<IdentecPour>>();

        protected readonly string LOG_TYPE;
                
        public IdentecPourFilter( )
            : this(GetDefaultDeviceConfig())
        {
        }


        public IdentecPourFilter(IDeviceConfig _Config)
            : base( _Config )
        {
            try
            {
                LOG_TYPE = this.GetType().Name;
                State = Config.State;
            }
            catch( Exception exp )
            {
                Log.WriteException(LOG_TYPE, "IdentecPourFilter:: IdentecPourFilter", exp);
            }
        }

        override public void Start( )
        {
            try
            {
                State = DeviceState.Started;
                Config.State = DeviceState.Started;
                m_Stop = false;
                m_Worker = new System.Threading.Thread(CleanupTags);
                m_Worker.Start();
            }
            catch( Exception exp )
            {
                Log.WriteException(LOG_TYPE, "IdentecPourFilter:: Start", exp);
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
                Log.WriteException(LOG_TYPE, "IdentecPourFilter:: Stop", exp);
            }
            finally
            {
                m_Stop = true;
            }
        }

        protected void CleanupTags()
        {
            var cleanupTime = new TimeSpan(0, 0, 2);
            while (!m_Stop)
            {
                try
                {
                    Thread.Sleep(500);
                    lock (m_TagList)
                    {
                        var tags = m_TagList.Values.ToList();
                        foreach (var tag in tags)
                        {
                            if (tag.TimeDiff > cleanupTime )
                            {
                                m_TagList.Remove(tag.Element.TagID);
                            }
                        }
                    }
                }
                catch (Exception err)
                {
                    Log.WriteException(LOG_TYPE, "IdentecDetachPlugin:: Stop", err);
                }
            }
        }

        public override string Consume( IMessage _message )
        {
            string rc = null;

            try
            {
                if (_message is IdentecPour)
                {
                    var Tag = _message as IdentecPour;

                    //WriteRawPour(Tag);

                    lock (m_TagList)
                    {
                        if (m_TagList.ContainsKey(Tag.TagID))
                        {
                            if (m_TagList[Tag.TagID].Element.PourCount != Tag.PourCount)
                            {
                                m_TagList[Tag.TagID] = new BaseBevManDevice.TimedElement<IdentecPour>( Tag );
                                ProduceMessage(Tag);
                            }
                            else
                            {
                                Log.Debug(LOG_TYPE, string.Format("IdentecPourFilter::Consume - Tag {0} Duplicate Pour", Tag.TagID ));
                            }
                        }
                        else
                        {
                            m_TagList[Tag.TagID] = new BaseBevManDevice.TimedElement<IdentecPour>(Tag);
                            ProduceMessage(Tag);
                        }
                    }
                }
                else
                {
                    ProduceMessage(_message);
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( LOG_TYPE, "DataConsumer::Consume", exp );
            }
            return rc;
        }

        //private void WriteRawPour(IdentecPour _tag)
        //{

        //}
    }
}