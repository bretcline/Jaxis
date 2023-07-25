using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
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
      <AssemblyType>Jaxis.Readers.Identec.IdentecDetachPlugin</AssemblyType>
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

    public class IdentecDetachPlugin : BaseProducerDevice, IProducer, IConsumer
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
            rc.Type = DeviceType.DataProducerConsumer;
            rc.State = DeviceState.Stopped;
            rc.ProducerMessageType = 0;
            rc.ConsumerMessageType = 1;
            DeviceConfigOption Option1 = new DeviceConfigOption();
            Option1.Name = "ReadWindow";
            Option1.Value = "1.5";
            rc.Options.Add(Option1);
            return rc;
        }

        protected System.Threading.Thread m_Worker = null;
        protected double m_Timeout = 0;
        protected TimeSpan m_ReadWindow;
        protected Dictionary<string, PhaseMessage> m_TagList = new Dictionary<string, PhaseMessage>( );
        protected Dictionary<string, ITag> m_WatchList = new Dictionary<string, ITag>( );

        protected readonly string LOG_TYPE;

        public IdentecDetachPlugin( )
            : this(GetDefaultDeviceConfig())
        {
        }

        public IdentecDetachPlugin( IDeviceConfig _Config )
            : base( _Config )
        {
            try
            {
                LOG_TYPE = this.GetType().Name;
                State = Config.State;

                m_Timeout = Convert.ToDouble(_Config.Options[0].Value, CultureInfo.InvariantCulture);

                int Seconds = (int)m_Timeout;
                int Thousands = (int)( ( m_Timeout % 1 ) * 1000 );
                m_ReadWindow = new TimeSpan( 0, 0, 0, Seconds, Thousands );

                Log.Debug( LOG_TYPE, string.Format( "IdentecDetachPlugin::Timeout = {0}", m_Timeout ) );
            }
            catch( Exception exp )
            {
                Log.WriteException( LOG_TYPE, "IdentecDetachPlugin:: IdentecDetachPlugin", exp );
            }
        }

        override public void Start( )
        {
            try
            {
                State = DeviceState.Started;
                Config.State = DeviceState.Started;
                m_Stop = false;
                m_Worker = new System.Threading.Thread( ProcessMessages );
                m_Worker.Start( );
            }
            catch( Exception exp )
            {
                Log.WriteException( LOG_TYPE, "IdentecDetachPlugin:: Start", exp );
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
                Log.WriteException( LOG_TYPE, "IdentecDetachPlugin:: Stop", exp );
            }
            finally
            {
                m_Stop = true;
            }
        }

        protected void ProcessMessages( )
        {
            while( !m_Stop )
            {
                try
                {
                    Thread.Sleep( 1000 );
                    lock( m_TagList )
                    {
                        List<PhaseMessage> tagList = m_TagList.Values.ToList( );
                        foreach( var phaseMessage in tagList )
                        {
                            DateTime TimeDiff = DateTime.Now.Subtract(m_ReadWindow);
                            if( phaseMessage.ReadTime < TimeDiff )
                            {
                                Log.Debug( LOG_TYPE, string.Format( "DetachPlugin - Tag List - TagID {0} TimeDiff {1}", phaseMessage.TagID, TimeDiff ) );

                                m_TagList.Remove( phaseMessage.TagID );
                                ProduceMessage( phaseMessage );
                            }
                        }
                    }
                    lock( m_WatchList )
                    {
                        List<ITag> tagList = m_WatchList.Values.ToList( );
                        foreach( var phaseMessage in tagList )
                        {
                            Log.Debug( LOG_TYPE, string.Format( "WATCH LIST TagID: {0} from WATCH LIST", phaseMessage.TagID ) );

                            DateTime TimeDiff = DateTime.Now.Subtract( m_ReadWindow );
                            if( phaseMessage.ReadTime < TimeDiff )
                            {
                                Log.Debug( LOG_TYPE, string.Format( "ProcessMessages REMOVE TagID: {0} from WATCH LIST", phaseMessage.TagID ) );
                                m_WatchList.Remove( phaseMessage.TagID );
                            }
                        }

                    }
                }
                catch (Exception err )
                {
                    Log.WriteException( LOG_TYPE, "IdentecDetachPlugin:: Stop", err );
                }
            }
        }

        public override string Consume( IMessage _message )
        {
            string rc = null;

            try
            {
                PhaseMessage Tag = _message as PhaseMessage;
                if( null != Tag )
                {
                    lock( m_TagList )
                    {
                        if (TagPhaseType.Disconnect == Tag.EventType)
                        {
                            Log.Debug( LOG_TYPE, string.Format( "DetachPlugin - TagID {0} Detached", Tag.TagID ) );
                            m_TagList[Tag.TagID] = Tag;
                            lock( m_WatchList )
                            {
                                Log.Debug( LOG_TYPE, string.Format( "ADD TagID: {0} from WATCH LIST", Tag.TagID ) );
                                m_WatchList[Tag.TagID] = Tag;
                            }
                        }
                        else if (TagPhaseType.Connect == Tag.EventType)
                        {
                            Log.Debug( LOG_TYPE, string.Format( "DetachPlugin - TagID {0} Attached", Tag.TagID ) );
                            if( m_TagList.ContainsKey( Tag.TagID ) )
                            {
                                Log.Debug( LOG_TYPE, string.Format( "DetachPlugin - TagID {0} Bobbled Att/Det", Tag.TagID ) );
                                m_TagList.Remove( Tag.TagID );
                            }
                            else
                            {
                                ProduceMessage( _message );
                            }
                        }
                        else if( TagPhaseType.BadBottleAttach == Tag.EventType )
                        {
                            if( !m_WatchList.ContainsKey( Tag.TagID ) )
                            {
                                ProduceMessage( _message );
                            }
                        }
                        else
                        {
                            ProduceMessage( _message );
                        }
                    }
                }
                else
                {
                    lock( m_WatchList )
                    {
                        TagAlertMessage TagAlert = _message as TagAlertMessage;

                        if( null != TagAlert )
                        {
                            Log.Debug( LOG_TYPE, string.Format( "TagID: {0} AlertType: {1} InList {2}", TagAlert.TagID, TagAlert.AlertType, m_WatchList.ContainsKey( TagAlert.TagID ) ) );
                            if( AlertTypes.BadBottleAttach == TagAlert.AlertType && !m_WatchList.ContainsKey( TagAlert.TagID ) )
                            {
                                ProduceMessage( _message );
                            }
                            else if( TagAlert.AlertType == AlertTypes.BadBottleAttach )
                            {
                                //Log.Debug( LOG_TYPE,string.Format( "Consume REMOVE TagID: {0} from WATCH LIST", TagAlert.TagID ) );
                                //m_WatchList.Remove( TagAlert.TagID );
                            }
                        }
                        else
                        {
                            ProduceMessage( _message );
                        }
                    }
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( LOG_TYPE, "DataConsumer::Consume", exp );
            }
            return rc;
        }
    }
}