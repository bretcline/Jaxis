using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JaxisInterfaces;
using JaxisEngine;
using JaxisEngine.Base;
using BevClasses;
using BevWCFDB;

namespace BevDBDev
{
    public class DBDev : BaseDevice, IDevice
    {
        public event ProduceHandler Produce;

        public DBDev(IDeviceConfig _Config)
            : base(_Config)
        {
            Config.ConsumerMessageType = MessageType.RawData;
            Config.Type = DeviceType.DataProducerConsumer;
            State = DeviceState.Stopped;
        }

        public override string Consume(IMessage _Message)
        {
            string rc = null;

            WCFDB DB = new WCFDB();
            (_Message as BevMessage).Pour.Bottle = DB.GetBottleForTag(_Message.Tag);
            (_Message as BevMessage).Pour.Bottle.Beverage = DB.GetBeverage((_Message as BevMessage).Pour.Bottle.BeverageID);
            (_Message as BevMessage).Pour.Time = _Message.ReadTime;
            (_Message as BevMessage).Pour.Amount = (_Message as BevMessage).Pour.Duration.Seconds * 0.25; // FL OZ 
            _Message.Type = MessageType.DBData;

            DB.AddUpdatePour((_Message as BevMessage).Pour);

            if (null != Produce)
            {
                Produce(_Message);
            }
            return rc;
        }

        override public void Start()
        {
            State = DeviceState.Started;
        }

        override public void Stop( )
        {
            State = DeviceState.Stopped;
        }
    }
}
