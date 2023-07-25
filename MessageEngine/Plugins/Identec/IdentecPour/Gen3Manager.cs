using System;
using System.Collections.Generic;
using System.Net.Sockets;
using IDENTEC.ILR350.Readers;
using IDENTEC;

namespace Jaxis.Readers.Identec
{
    /// <summary>
    /// Device Manager for Gen3 devices (like ReeferReader....)
    /// </summary>
    public class Gen3Manager
    {
        private DataStream m_Stream = null;

        private List<ILR350Reader> devices = new List<ILR350Reader>();
        private ILR350Reader selectedDevice = null;

        public void DiscoverDevices(string port)
        {
            devices.Clear();

            if (m_Stream != null && m_Stream.IsOpen)
                m_Stream.Close();

            m_Stream = new SerialPortStream(port);

            if (!m_Stream.IsOpen)
                m_Stream.Open();

            iBusAdapter myBus = new iBusAdapter(m_Stream);
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

        public void DiscoverDevicesEth( Socket _stream)
        {

            devices.Clear();

            m_Stream = new TCPSocketStream( _stream );

            if (!m_Stream.IsOpen)
                m_Stream.Open();

            iBusAdapter myBus = new iBusAdapter(m_Stream);
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

            if (m_Stream != null && m_Stream.IsOpen)
                m_Stream.Close();

            m_Stream = new TCPSocketStream(IPAdress, 2101);

            if (!m_Stream.IsOpen)
                m_Stream.Open();

            iBusAdapter myBus = new iBusAdapter(m_Stream);
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
            if (m_Stream != null && m_Stream.IsOpen)
                m_Stream.Close();
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


