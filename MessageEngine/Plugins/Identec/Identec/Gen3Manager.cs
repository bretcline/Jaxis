using System;
using System.Collections.Generic;
using System.Text;
using IDENTEC.ILR350.Readers;
using IDENTEC.ILR350.Tags;
using IDENTEC.Tags;
using IDENTEC;

namespace BeverageMetrics.BeaconParser
{
    /// <summary>
    /// Device Manager for Gen3 devices (like ReeferReader....)
    /// </summary>
    public class Gen3Manager
    {
        private DataStream stream = null;

        private List<ILR350Reader> devices = new List<ILR350Reader>();
        private ILR350Reader selectedDevice = null;

        public void DiscoverDevices(string port)
        {
            devices.Clear();

            if (stream != null && stream.IsOpen)
                stream.Close();

            stream = new SerialPortStream(port);

            if (!stream.IsOpen)
                stream.Open();

            iBusAdapter myBus = new iBusAdapter(stream);
            IBusDevice[] busDevices = myBus.EnumerateBusModules();

            foreach (IBusDevice device in busDevices)
            {
                ILR350Reader iPort = device as ILR350Reader;
                if (iPort != null)
                    devices.Add(iPort);
            }

            if (devices.Count > 0)
                SelectedDevice = devices[0];
            else
                SelectedDevice = null;
        }

        public void DiscoverDevicesEth(string IPAdress)
        {
            devices.Clear();

            if (stream != null && stream.IsOpen)
                stream.Close();

            stream = new TCPSocketStream(IPAdress, 2101);

            if (!stream.IsOpen)
                stream.Open();

            iBusAdapter myBus = new iBusAdapter(stream);
            IBusDevice[] busDevices = myBus.EnumerateBusModules();

            foreach (IBusDevice device in busDevices)
            {
                ILR350Reader iPort = device as ILR350Reader;
                if (iPort != null)
                    devices.Add(iPort);
            }

            if (devices.Count > 0)
                SelectedDevice = devices[0];
            else
                SelectedDevice = null;
        }

        public void CloseStream()
        {
            if (stream != null && stream.IsOpen)
                stream.Close();
        }

        public ILR350Reader SelectedDevice
        {
            get
            {
                return selectedDevice;
            }
            set
            {
                if (selectedDevice != value)
                {
                    selectedDevice = value;
                }
            }
        }

        public List<ILR350Reader> Devices
        {
            get
            {
                return devices;
            }
        }

        public  string Status
        {
            get
            {
                string status = string.Empty;

                if (selectedDevice == null)
                    status = "Device is not selected";
                else
                    status = String.Format("Connected to device: {0}", DeviceDescription(selectedDevice));

                return status;
            }
        }

        public string DeviceDescription(ILR350Reader device)
        {
            string description = string.Empty;

            if (device != null)
            {
                string information = device.Information;
                string serialNumber = device.SerialNumber;
                string firmwareVersion = device.FirmwareVersion;

                description = String.Format("{0}, SN: {1}, Version: {2}", information, serialNumber, firmwareVersion);
            }

            return description;

        }
    }
}


