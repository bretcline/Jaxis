using System;
using System.Reflection;
using Jaxis.Engine.Base.Device;
using Jaxis.Interfaces;

namespace Jaxis.Engine.Base
{
    public class MessageSniffer : BaseDevice, IDevice, IConsumer
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
            rc.Name = "MessageSniffer";
            rc.Type = DeviceType.DataConsumer;
            rc.State = DeviceState.Stopped;
            rc.ProducerMessageType = 1;
            rc.ConsumerMessageType = 0;
            return rc;
        }

        public Func<IMessage, string> DisplayData;


        public MessageSniffer()
            : this(GetDefaultDeviceConfig())
        {
            Config.ConsumerMessageType = Int64.MaxValue;
        }

        public MessageSniffer( IDeviceConfig _Config )
            : base( _Config )
        {
            Config.ConsumerMessageType = Int64.MaxValue;// MessageType.All;
            Config.Type = DeviceType.DataProducerConsumer;
            State = DeviceState.Stopped;
        }

        public override void Start( )
        {
            State = DeviceState.Started;
        }

        public override void Stop( )
        {
            State = DeviceState.Stopped;
            // may want to tell UI engine device was stopped
        }

        public override string Consume( IMessage _message )
        {
            string rc = null;

            rc = DisplayData( _message ); // Using the IDevice produce event to get data to Form...

            return rc;
        }

        #region IDevice Members

        public string HardwareID { get; set; }

        #endregion IDevice Members
    }
}