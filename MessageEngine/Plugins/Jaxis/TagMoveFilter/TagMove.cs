using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Linq;
using Jaxis.Engine.Base.Device;
using Jaxis.Interfaces;
using Jaxis.Interfaces.Tags;
using Jaxis.MessageLibrary;
using Jaxis.Util.Log4Net;

/*
<DeviceConfig>
  <AssemblyName>Jaxis.Engine.Device.dll</AssemblyName>
  <AssemblyType>Jaxis.Engine.Device.TagMove</AssemblyType>
  <AssemblyVersion>1.0</AssemblyVersion>
  <ID>456</ID>
  <Name>Tag Move Device</Name>
  <Type>DataProducerConsumer</Type>
  <State>Started</State>
  <ConsumerMessageType>RawData</ConsumerMessageType>
  <ProducerMessageType>RawData</ProducerMessageType>
  <Options>
    <string>60</string>   <!-- Seconds at new device before sending move alert -->
  </Options>
</DeviceConfig>
*/
namespace Jaxis.Engine.Device
{
    public class TagMove : BaseProducerDevice, IProducer, IConsumer
    {
        [Obfuscation(Exclude = true)]
        public static DeviceConfig GetDefaultDeviceConfig()
        {
            var rc = new DeviceConfig();
            var Asm = System.Reflection.Assembly.GetExecutingAssembly();
            rc.AssemblyName = Asm.ManifestModule.Name;
            rc.AssemblyType = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName;
            rc.AssemblyVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(BaseDevice.GetPath() + "\\" + rc.AssemblyName).FileVersion;
            rc.ID = Guid.NewGuid().ToString();
            rc.Name = "Tag Move Device";
            rc.Type = DeviceType.DataProducerConsumer;
            rc.State = DeviceState.Stopped;
            rc.ProducerMessageType = 16;
            rc.ConsumerMessageType = 8;
            var Option1 = new DeviceConfigOption();
            Option1.Name = "Alert Time";
            Option1.Value = "60";
            rc.Options.Add(Option1);
            return rc;
        }

        protected IFilterConfig m_Config = null;
        protected FilterType m_Type;
        protected int m_Time = 0;
        protected Dictionary<string, List<ITagRead>> m_Tags = new Dictionary<string, List<ITagRead>>( );

        
        public TagMove( )
            : this(GetDefaultDeviceConfig())
        {
        }

        public TagMove( IDeviceConfig _Config )
            : base( _Config )
        {
            //Config.ConsumerMessageType = MessageType.RawData;
            Config.Type = DeviceType.DataProducer;
            Config.State = DeviceState.Stopped;
            State = DeviceState.Stopped;
            if( 1 < _Config.Options.Count )
            {
                DeviceConfigOption Option = new DeviceConfigOption();
                Option.Name = "Interval";
                Option.Value = "60";
                _Config.Options.Add(Option);
            }
            m_Time = Convert.ToInt32( _Config.Options[0].Value );
        }

        override public void Start( )
        {
            LoadOldTagDict( );
            Config.State = DeviceState.Started;
        }

        override public void Stop( )
        {
            Config.State = DeviceState.Stopped;
        }

        public override string Consume( IMessage _message )
        {
            string rc = null;

            try
            {
                ITagRead Msg = _message as ITagRead;
                List<ITagRead> OldMsgs = null;
                if( null != Msg )
                {
                    m_Tags.TryGetValue( Msg.TagID, out OldMsgs );
                    if( null == OldMsgs || 0 == OldMsgs.Count )
                    {
                        // Have not seen tag before
                        SendMoveAlertMsg( Msg, Msg );
                        List<ITagRead> Tags = new List<ITagRead>( );
                        Tags.Add( Msg );
                        m_Tags[Msg.TagID] = Tags;
                    }
                    else
                    {
                        ITagRead CurrentDevice = OldMsgs.Find( M => M.SignalStrength == OldMsgs.Max( Ms => Ms.SignalStrength ) );
                        if( null != CurrentDevice && Msg.DeviceID == CurrentDevice.DeviceID )
                        {
                            // Tag has not moved
                            OldMsgs[OldMsgs.IndexOf( CurrentDevice )] = Msg;
                        }
                        else if( CurrentDevice.SignalStrength != Msg.SignalStrength &&
                                  Msg.ReadTime.Subtract( CurrentDevice.ReadTime ).TotalSeconds > m_Time ) // Has tag "moved" for the defined time period
                        {
                            // Tag has moved
                            SendMoveAlertMsg( Msg, CurrentDevice );
                            ITagRead OldMsg = OldMsgs.Find( M => M.DeviceID == Msg.DeviceID );
                            if( null == OldMsg )
                            {
                                // Have not seen this reader before
                                OldMsgs.Add( Msg );
                                m_Tags[Msg.TagID] = OldMsgs;
                            }
                            else
                            {
                                OldMsgs[OldMsgs.IndexOf( OldMsg )] = Msg;
                            }
                        }
                        // Remove old tag reads
                        if( null != OldMsgs && OldMsgs.Count > 1 )
                        {
                            bool bOldTags = true;
                            while( true == bOldTags )
                            {
                                ITagRead Oldest = OldMsgs.Find( M => M.ReadTime == OldMsgs.Max( Ms => Ms.ReadTime ) );
                                if( OldMsgs.Count > 1 &&
                                    Msg.ReadTime.Subtract( Oldest.ReadTime ).TotalSeconds > m_Time )
                                    OldMsgs.Remove( Oldest );
                                else
                                    bOldTags = false;
                            }
                        }
                    }
                    StoreTagDict( );
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "TagMove::Consume", exp );
            }

            return rc;
        }

        private void LoadOldTagDict( )
        {
            IFormatter formatter = new BinaryFormatter( );
            using( FileStream stream = new FileStream( "TagList.bin", FileMode.Open, FileAccess.Read, FileShare.None ) )
            {
                m_Tags = (Dictionary<string, List<ITagRead>>)formatter.Deserialize( stream );
            }
        }

        private void StoreTagDict( )
        {
            byte[] Data = null;
            using( MemoryStream MSWriter = new MemoryStream( ) )
            {
                BinaryFormatter Writer = new BinaryFormatter( );
                Writer.Serialize( MSWriter, m_Tags );
                Data = MSWriter.ToArray( );
            }

            using( StreamWriter SWriter = new StreamWriter( "TagList.bin" ) )
            {
                BinaryWriter Writer = new BinaryWriter( SWriter.BaseStream );
                Writer.Write( Data );
            }
        }

        private void SendMoveAlertMsg( ITagRead _msg, ITagRead _oldMsg )
        {
            try
            {
                var message = new TagAlertMessage( ) { Driver = Config.Name };
                message.DeviceID = _msg.DeviceID;
                message.ReadTime = _msg.ReadTime;
                message.TagID = _msg.TagID;
                message.Parameters.Add( "OldDeviceID", _oldMsg.DeviceID );
                message.Parameters.Add( "SignalStrength", _msg.SignalStrength );
                //Message.Type = MessageType.RawData;
                message.AlertType = AlertTypes.TagMoved;

                ProduceMessage( message );
            }
            catch( Exception exp )
            {
                Log.WriteException( "TagMove:: SendMoveAlertMsg", exp );
            }
        }
    }
}