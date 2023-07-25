using System;
using System.Reflection;
using Jaxis.Engine.Base.Device;
using Jaxis.Interfaces;

namespace TestApp
{
    class UIDataConsumer : BaseDevice, IDevice, IConsumer
    {
        public Func<IMessage, string> DisplayData;

        [Obfuscation(Exclude = true)]
        public static DeviceConfig GetDefaultDeviceConfig()
        {
            DeviceConfig rc = new DeviceConfig();
            System.Reflection.Assembly Asm = System.Reflection.Assembly.GetExecutingAssembly();
            rc.AssemblyName = Asm.ManifestModule.Name;
            rc.AssemblyType = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName;
            rc.AssemblyVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(BaseDevice.GetPath() + "\\" + rc.AssemblyName).FileVersion;
            rc.ID = Guid.NewGuid().ToString();
            rc.Name = "UIDataConsumer";
            rc.Type = DeviceType.DataConsumer;
            rc.State = DeviceState.Stopped;
            rc.ProducerMessageType = 0;
            rc.ConsumerMessageType = ulong.MaxValue; // Consume all types
            return rc;
        }

        public UIDataConsumer( )
            : this(GetDefaultDeviceConfig())
        {
        }

        public UIDataConsumer( IDeviceConfig _Config )
            : base( _Config )
        {
        }

        public override void Start( )
        {
            State = DeviceState.Started;
            Config.State = State;
        }

        public override void Stop( )
        {
            State = DeviceState.Stopped;
            Config.State = State;
            // may want to tell UI engine device was stopped
        }

        public override string Consume( IMessage _message )
        {
            string rc = null;

            if (null != DisplayData)
            {
                rc = DisplayData(_message); // Using the IDevice produce event to get data to Form...
            }

            return rc;
        }

        #region IDevice Members

        public string HardwareID { get; set; }

        #endregion IDevice Members
    }
}