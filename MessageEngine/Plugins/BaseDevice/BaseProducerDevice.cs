using System;
using Jaxis.Interfaces;
using Jaxis.MessageLibrary;

namespace Jaxis.Engine.Base.Device
{
    public abstract class BaseProducerDevice : BaseDevice, IProducer
    {
        public BaseProducerDevice( IDeviceConfig _Config )
            : base( _Config )
        {
        }

        #region IProducer Members

        public Func<IMessage, string> Produce { private get; set; }

        public string ProduceMessage( IMessage _Message )
        {
            if( _Message is IAlertMessage )
            {
                MessageManipulator.SetMessageType( _Message as BaseMessage, (ulong)MessageType.AlertMessage );
                
            }
            else if (_Message is BaseMessage)
            {
                MessageManipulator.SetMessageType(_Message as BaseMessage, Config.ProducerMessageType);
                (_Message as BaseMessage).Driver = Config.AssemblyType;
            }
            else if( _Message is IMessageWrapper)
            {
                (_Message as IMessageWrapper).Type = Config.ProducerMessageType;
                (_Message as IMessageWrapper).Driver = Config.AssemblyType;
            }
            //_Message.Type = Config.ProducerMessageType;
            string rc = string.Empty;

            bool Success = true;
            foreach( IFilter F in this.Filters )
            {
                if( false != F.Filter( _Message ) )
                {
                    Success = false;
                    break;
                }
            }
            if( Success == true && null != Produce )
            {
                rc = Produce( _Message );
            }

            return rc;
        }

        #endregion IProducer Members

        #region IDevice Members

        public string HardwareID { get; set; }

        #endregion IDevice Members
    }
}