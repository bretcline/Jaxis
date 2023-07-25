namespace IDENTEC
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class iBusModule
    {
        internal long EventMask = 0xffffffffL;

        public static  event OnModuleStatusError EventModuleStatusError;

        protected iBusModule()
        {
        }

        internal void FireEventModuleStatusError(object module, iBusDeviceStatus status)
        {
            if (((status.DWord & this.EventMask) != 0L) && (EventModuleStatusError != null))
            {
                EventModuleStatusError(module, status);
            }
        }

        public delegate void OnModuleStatusError(object module, iBusDeviceStatus status);
    }
}

