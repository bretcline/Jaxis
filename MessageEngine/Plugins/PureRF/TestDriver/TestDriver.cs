using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JaxisEngine.Base.Device;
using Jaxis.Interfaces;
using Jaxis.Util.Log4Net;

namespace Jaxis.Plugins.PureRF
{
    public class TestDriver : BaseProducerDevice, IProducer
    {
        public TestDriver( IDeviceConfig _Config )
            : base(_Config)
        {
            Config.ConsumerMessageType = MessageType.RawData;
            Config.Type = DeviceType.DataProducer;
            State = DeviceState.Stopped;
        }

//        public override string Consume(IMessage _Message)
//        {
//            return Log.Wrap<string>("Processor::Consume", LogType.Debug, true, () =>
//            {
//                TimeSpan ReadWindow = new TimeSpan( 0, 0, 30 );
//                string rc = null;

//                try
//                {

//                }
//                catch( Exception err )
//                {
////                    Log.WriteException( string.Format( "On Read: {0}", Message.TagID ), err );
//                }
//                return rc;
//            });
//        }

        override public void Start()
        {
            Log.Wrap<bool>("Processor::Start", LogType.Debug, true, () =>
            {
                State = DeviceState.Started;

                System.Threading.Thread T = new System.Threading.Thread( GenerateMessages );

                return true;
            });
        }

        override public void Stop( )
        {
            Log.Wrap<bool>("Processor::Stop", LogType.Debug, true, () =>
            {
                State = DeviceState.Stopped;
                return true;
            });
        }


        protected void GenerateMessages( )
        {
            Random Rnd = new Random( );
            while( DeviceState.Started == State )
            {
                int SleepTime = Rnd.Next( 5000 );
                System.Threading.Thread.Sleep( SleepTime );
            }
        }

    }

}
