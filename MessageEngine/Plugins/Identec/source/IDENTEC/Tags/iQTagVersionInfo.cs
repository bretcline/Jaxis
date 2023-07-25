namespace IDENTEC.Tags
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct iQTagVersionInfo
    {
        public int PCBVersion;
        public int AssemblingVersion;
        public int SoftwareMajorVersion;
        public int SoftwareMinorVersion;
    }
}

