using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JaxisEngine.Base.Device;
using Jaxis.Interfaces;
using Jaxis.AlienRFID.MessageLibrary;
using Jaxis.Util.Log4Net;

namespace Jaxis.MessageProcessor.Alien
{
    public class Processor : BaseProducerDevice, IProducer, IConsumer
    {
        public Processor(IDeviceConfig _Config)
            : base(_Config)
        {
            Config.ConsumerMessageType = MessageType.RawData;
            Config.Type = DeviceType.DataProducer | DeviceType.DataConsumer;
            State = DeviceState.Stopped;
        }

        Dictionary<string, AlienMessages> m_Tags = new Dictionary<string, AlienMessages>( );
        Queue<string> m_KeysToRemove = new Queue<string>( );
        public override string Consume(IMessage _Message)
        {
            return Log.Wrap<string>("Processor::Consume", LogType.Debug, true, () =>
            {
                TimeSpan ReadWindow = new TimeSpan( 0, 0, 30 );
                string rc = null;

                AlienMessages Message = _Message as AlienMessages;

                try
                {
                    DataMessage Msg = new DataMessage( Message );
                    Msg.Type = MessageType.DBData;
                    ProduceMessage( Msg );

                    //string TagID = Message.Tag;

                    //if( !m_Tags.ContainsKey( TagID ) )
                    //{
                    //    m_Tags.Add( Message.Tag, Message );
                    //    m_KeysToRemove.Enqueue( TagID );
                    //    Message.Type = MessageType.DBData;
                    //    ProduceMessage( new DataMessage( Message ) );
                    //    if( 5 < m_KeysToRemove.Count )
                    //    {
                    //        string Remove = m_KeysToRemove.Dequeue( );
                    //        m_Tags.Remove( Remove );
                    //    }
                    //}
                    //else if( ( DateTime.Now - m_Tags[TagID].ReadTime ) > ReadWindow )
                    //{
                    //    m_Tags[Message.Tag] = Message;
                    //    Message.Type = MessageType.DBData;
                    //    ProduceMessage( new DataMessage( Message ) );
                    //}
                }
                catch( Exception err )
                {
                    Log.WriteException( string.Format( "On Read: {0}", Message.Tag ), err );
                }
                return rc;
            });
        }

        override public void Start()
        {
            Log.Wrap<int>("Processor::Start", LogType.Debug, true, () =>
            {
                State = DeviceState.Started;
                return 1;
            });
        }

        override public void Stop( )
        {
            Log.Wrap<int>("Processor::Stop", LogType.Debug, true, () =>
            {
                State = DeviceState.Stopped;
                return 1;
            });
        }
    }


}
