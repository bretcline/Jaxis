namespace PureRF
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class PortsEnumerator
    {
        public static bool EnumeratePorts(out SortedList<string, string> PortsMap)
        {
            int numPorts = 0;
            PortsMap = new SortedList<string, string>();
            string[] Ports = System.IO.Ports.SerialPort.GetPortNames( );
            numPorts = Ports.Length;
            foreach( string P in Ports )
            {
                PortsMap.Add( P, P );
            }
            return true;
        }

        [DllImport("PortsEnumHelper.dll")]
        private static extern int EnumPortsHelper_Free(int numPorts, IntPtr arrPortNames, IntPtr arrPortDescriptions);
        [DllImport("PortsEnumHelper.dll")]
        private static extern int EnumPortsHelper_GetPorts(out IntPtr arrPortNames, out IntPtr arrPortDescriptions);
    }
}

