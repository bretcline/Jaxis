using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Freescale.ZStarLib;

namespace ZStarDevice
{
    public class ZStar
    {
        private ZStar3 m_ZStar0 = new ZStar3();
        private ZStar3 m_ZStar1 = new ZStar3();

        private ZStarSensor m_Sensor0 = null;
        private ZStarSensor m_Sensor1 = null;
        List<DataRead> m_Reads = new List<DataRead>();
        List<RawDataRead> m_RawReads = new List<RawDataRead>();

        public bool m_UpperSensor = false;
        public bool m_LowerSensor = false;


        public ZStar()
        {
            // Event when new burst data was received
            m_ZStar0.OnBurstDataReceived += new ZStar3.OnBurstDataReceivedHandler(ZStar_OnBurstDataReceived0);
            //KDC -- Added new handler for burst data received.
            //theZStar.OnBurstDataReceived += new ZStar3.OnBurstDataReceivedHandler(ZStar_OnBurstDataReceivedProcess);
            // Event that occur when list of Active sensor was changed
            m_ZStar0.OnActiveSensorsChanged += new ZStar3.OnActiveSensorsChangedHandler(ZStar_OnActiveSensorsChanged0);
            // Event that occur if ZStarLib lost Connection on Comport
            m_ZStar0.OnConnectionLost += new ZStar3.OnConnectionLostHandler(ZStar_OnConnectionLost0);

            // Event when new burst data was received
            m_ZStar1.OnBurstDataReceived += new ZStar3.OnBurstDataReceivedHandler(ZStar_OnBurstDataReceived1);
            //KDC -- Added new handler for burst data received.
            //theZStar.OnBurstDataReceived += new ZStar3.OnBurstDataReceivedHandler(ZStar_OnBurstDataReceivedProcess);
            // Event that occur when list of Active sensor was changed
            m_ZStar1.OnActiveSensorsChanged += new ZStar3.OnActiveSensorsChangedHandler(ZStar_OnActiveSensorsChanged1);
            // Event that occur if ZStarLib lost Connection on Comport
            m_ZStar1.OnConnectionLost += new ZStar3.OnConnectionLostHandler(ZStar_OnConnectionLost1);
        }

        public void Save()
        {
        }

        public void Load()
        {
        }

        // New Burst data was received
        void ZStar_OnBurstDataReceived0(object sender, byte sensor)
        {
            // Are these data from our sensor 0
            if (sensor == 0)
            {
                var SenNum = 0;
                lock (m_Reads)
                {
                    while (m_Sensor0.GetBurstData()) // Get all pending data in Burst Data FIFO Buffer
                    {
                        // Send Fake 0,0,0 read for upper is only one sensor is online
                        if (null == m_Sensor1)
                        {
                            SenNum = 1;
                            m_Reads.Add(new DataRead(0, m_Sensor0.BurstTime, 0, 0, 90));
                        }
                        m_Reads.Add(new DataRead(SenNum, m_Sensor0.BurstTime,m_Sensor0.TiltXFiltered, m_Sensor0.TiltYFiltered, m_Sensor0.TiltZFiltered));
                        m_RawReads.Add(new RawDataRead(m_Sensor0.BurstTime, m_Sensor0.AbsoluteG,
                               m_Sensor0.RealX, m_Sensor0.RealY, m_Sensor0.RealZ,
                               m_Sensor0.RawX, m_Sensor0.RawY, m_Sensor0.RawZ,
                               m_Sensor0.TiltX, m_Sensor0.TiltY, m_Sensor0.TiltZ));

                    }
                }
            }
        }

        // List Of Active sensor was changed
        void ZStar_OnActiveSensorsChanged0(object sender, byte sensor)
        {
            // Is first sensor present???
            if ((m_ZStar0.ActiveSensorsMask & 0x0001) == 0x0001)
            {
                // Enable  Burst Data Receive for this sensor 
                m_Sensor0.BurstDataReceiveEnable = true;
                // Enable global Burst mode 
                m_ZStar0.BurstModeEnabled = true;
                
                // Switch status label to Connected
                //lb_Status.Text = "Connected";
            }
            else // Sensor index 0 is not connected            
            {
                // Disable  Burst Data Receive for this sensor
                m_Sensor0.BurstDataReceiveEnable = false;
                // Disable global Burst mode
                m_ZStar0.BurstModeEnabled = false;
                
                // Switch status label to DisConnected
                //lb_Status.Text = "Diconnected";
            }
        }

        // ZStarLib lost connection with ZStar hardware
        void ZStar_OnConnectionLost0(object sender)
        {
            // Perform Close port button
            Close();
        }

        // New Burst data was received
        void ZStar_OnBurstDataReceived1(object sender, byte sensor)
        {
            // Are these data from our sensor 1
            if (sensor == 0)
            {
                lock (m_Reads)
                {
                    while (m_Sensor1.GetBurstData()) // Get all pending data in Burst Data FIFO Buffer
                    {
                        // Send Fake 0,0,0 read if only one sensor is online
                        if (null == m_Sensor0)
                        {
                            m_Reads.Add(new DataRead(0, m_Sensor0.BurstTime, 0, 0, 90));
                        }

                        m_Reads.Add(new DataRead(1, m_Sensor1.BurstTime, m_Sensor1.TiltXFiltered, m_Sensor1.TiltYFiltered, m_Sensor1.TiltZFiltered));
                        m_RawReads.Add(new RawDataRead(m_Sensor1.BurstTime, m_Sensor1.AbsoluteG,
                                                       m_Sensor1.RealX, m_Sensor1.RealY, m_Sensor1.RealZ,
                                                       m_Sensor1.RawX, m_Sensor1.RawY, m_Sensor1.RawZ, 
                                                       m_Sensor1.TiltX, m_Sensor1.TiltY, m_Sensor1.TiltZ));
                    }
                }
            }
        }

        // List Of Active sensor was changed
        void ZStar_OnActiveSensorsChanged1(object sender, byte sensor)
        {
            // Is first sensor present???
            if ((m_ZStar1.ActiveSensorsMask & 0x0001) == 0x0001)
            {
                // Enable  Burst Data Receive for this sensor 
                m_Sensor1.BurstDataReceiveEnable = true;
                // Enable global Burst mode 
                m_ZStar1.BurstModeEnabled = true;

                // Switch status label to Connected
                //lb_Status.Text = "Connected";
            }
            else // Sensor index 0 is not connected            
            {
                // Disable  Burst Data Receive for this sensor
                m_Sensor1.BurstDataReceiveEnable = false;
                // Disable global Burst mode
                m_ZStar1.BurstModeEnabled = false;

                // Switch status label to DisConnected
                //lb_Status.Text = "Diconnected";
            }
        }

        // ZStarLib lost connection with ZStar hardware
        void ZStar_OnConnectionLost1(object sender)
        {
            // Perform Close port button
            Close();
        }

        public List<string> Detect()
        {
            var rc = new List<string>();
            // Get all ComPorts in PC
            ComPortInfo[] ports = ZStar3.GetComPorts();

            if (null != ports && 0 != ports.Length)
            {
                int i = 0;
                while (i != ports.Length)
                {
                    if (ports[i].FriendlyName.Contains("ZSTAR"))
                    {
                        rc.Add(ports[i].PortName);
                    }
                    i++;
                }
            }

            return rc;
        }

        public void Open(int _ZStar, string _Port)
        {
            ZStar3 zStar = m_ZStar0;
            Close(_ZStar);

            if ( 1 == _ZStar)
                zStar = m_ZStar1;

            // Get all ComPorts in PC
            ComPortInfo[] ports = ZStar3.GetComPorts();

            if (null != ports && 0 != ports.Length)
            {
                int i = 0;
                while (i != ports.Length)
                {
                    if (ports[i].PortName.Equals(_Port))
                    {
                        if (zStar.OpenPort(ports[i].PortNum))
                        {
                            // Check USB_Stick type (has to be known!!)
                            if (zStar.ZStarUsbStickType != ZStar3.UsbStickType.Unknown)
                            {
                                //KDC -- See if there is a sensor and if so, connect to it.
                                m_Sensor0 = zStar.GetSensor(0);
                                m_Sensor0.ClearBurstFifo();
                                m_Sensor0.AutoCalibrate();
                                m_Sensor0.AverageFilterSamples = 125;
                                //if (m_Sensor0.IsActive == false)
                                {
                                    // Enable  Burst Data Receive for this sensor
                                    m_Sensor0.BurstDataReceiveEnable = true;
                                    // Enable global Burst mode
                                    zStar.BurstModeEnabled = true;
                                    // Keep new sensors(without power switch) sensor awake up when ZStarLib runs
                                    zStar.SleepDisabled = true;
                                }
                            }
                        }
                    }
                    i++;
                }
            }
        }


        public void Open()
        {

            Close();

            if (!m_ZStar0.IsPortOpen)
            {
                // Get all ComPorts in PC
                ComPortInfo[] ports = ZStar3.GetComPorts();

                if (null != ports && 0 != ports.Length)
                {
                    int i = 0;
                    int S0 = -1;
                    int S1 = -1;
                    while (i != ports.Length)
                    {
                        if (ports[i].FriendlyName.Contains("ZSTAR"))
                        {
                            if (-1 == S0)
                            {
                                S0 = i;
                            }
                            else if (-1 == S1)
                            {
                                S1 = i;
                            }
                        }
                        i++;
                    }

                    if (-1 != S0)
                    {
                        if (m_ZStar0.OpenPort(ports[S0].PortNum))
                        {
                            // Check USB_Stick type (has to be known!!)
                            if (m_ZStar0.ZStarUsbStickType != ZStar3.UsbStickType.Unknown)
                            {
                                //KDC -- See if there is a sensor and if so, connect to it.
                                m_Sensor0 = m_ZStar0.GetSensor(0);
                                m_Sensor0.ClearBurstFifo();
                                m_Sensor0.AutoCalibrate();
                                m_Sensor0.AverageFilterSamples = 125;
                                //if (m_Sensor0.IsActive == false)
                                {
                                    // Enable  Burst Data Receive for this sensor
                                    m_Sensor0.BurstDataReceiveEnable = true;
                                    // Enable global Burst mode
                                    m_ZStar0.BurstModeEnabled = true;
                                    // Keep new sensors(without power switch) sensor awake up when ZStarLib runs
                                    m_ZStar0.SleepDisabled = true;
                                }
                            }
                        }
                        if  (-1 != S1)
                        {
                            if (m_ZStar1.OpenPort(ports[S1].PortNum))
                            {
                                if (m_ZStar1.ZStarUsbStickType != ZStar3.UsbStickType.Unknown)
                                {
                                    //KDC -- See if there is a sensor and if so, connect to it.
                                    m_Sensor1 = m_ZStar1.GetSensor(0);
                                    m_Sensor1.ClearBurstFifo();
                                    m_Sensor1.AutoCalibrate();
                                    m_Sensor1.AverageFilterSamples = 125;
                                    //if (m_Sensor1.IsActive == false)
                                    {
                                        // Enable  Burst Data Receive for this sensor
                                        m_Sensor1.BurstDataReceiveEnable = true;
                                        // Enable global Burst mode
                                        m_ZStar1.BurstModeEnabled = true;
                                        // Keep new sensors(without power switch) sensor awake up when ZStarLib runs
                                        m_ZStar1.SleepDisabled = true;
                                    }
                                }

                            }
                        }
                    }
                }
            }

        }

        // Close port
        public void Close()
        {
            // Switch of sleepDisabled Sensor capatibilities
            m_ZStar0.SleepDisabled = false;
            
            // Switch off Burst mode of ZStar 
            m_ZStar0.BurstDataReceiveEnableMask = 0x0000;
            m_ZStar0.BurstModeEnabled = false;

            // Close Port
            m_ZStar0.ClosePort();

            // Switch of sleepDisabled Sensor capatibilities
            m_ZStar1.SleepDisabled = false;
            
            
            // Switch off Burst mode of ZStar 
            m_ZStar1.BurstDataReceiveEnableMask = 0x0000;
            m_ZStar1.BurstModeEnabled = false;

            // Close Port
            m_ZStar1.ClosePort();

            lock (m_Reads)
            {
                m_Reads.Clear();
                m_RawReads.Clear();
            }
        }

        // Close port
        public void Close(int _ZStar)
        {

            if ( 0 == _ZStar)
            {
                // Switch of sleepDisabled Sensor capatibilities
                m_ZStar0.SleepDisabled = false;
                // Switch off Burst mode of ZStar 
                m_ZStar0.BurstDataReceiveEnableMask = 0x0000;
                m_ZStar0.BurstModeEnabled = false;
                // Close Port
                m_ZStar0.ClosePort();
            }
            
            
            if ( 1 == _ZStar)
            {
                // Switch of sleepDisabled Sensor capatibilities
                m_ZStar1.SleepDisabled = false;
                // Switch off Burst mode of ZStar 
                m_ZStar1.BurstDataReceiveEnableMask = 0x0000;
                m_ZStar1.BurstModeEnabled = false;
                // Close Port
                m_ZStar1.ClosePort();
            }

            lock (m_Reads)
            {
                m_Reads.Clear();
                m_RawReads.Clear();
            }
        }

        public List<DataRead> GetRead()
        {
            var rc = new List<DataRead>();


            lock (m_Reads)
            {
                foreach (DataRead D in m_Reads)
                {
                    rc.Add(D);
                }
                m_Reads.Clear();
            }

            return rc;
        }

        public List<RawDataRead> GetRawRead()
        {
            var rc = new List<RawDataRead>();


            lock (m_Reads)
            {
                foreach (RawDataRead D in m_RawReads)
                {
                    rc.Add(D);
                }
                m_RawReads.Clear();
            }

            return rc;
        }
    }

    public class DataRead
    {
        public int Sensor { get; set; }
        public double Time { get; set; }

        protected double m_AngleX = 0.0;
        protected double m_AngleY = 0.0;
        protected double m_AngleZ = 0.0;

        public double AngleX { get { return m_AngleX; } set { m_AngleX = value; } } // 90.0 - value; } }
        public double AngleY { get { return m_AngleY; } set { m_AngleY = value; } } //90.0 - value; } }
        public double AngleZ { get { return m_AngleZ; } set { m_AngleZ = value - 90; } } //90.0 - value; } }

        public DataRead(int _Sensor, double _Time,
                        double _AngleX, double _AngleY, double _AngleZ )
        {
            Sensor = _Sensor;
            Time = _Time;

            AngleX = _AngleX;
            AngleY = _AngleY;
            AngleZ = _AngleZ;

        }

        public DataRead()
        {
        }
    }

    public class RawDataRead
    {
        public double Time { get; set; }

        protected double m_AbsoluteG = 0.0;
        public double AbsoluteG { get { return m_AbsoluteG; } set { m_AbsoluteG = value; } }
       
        protected double m_RawX = 0.0;
        protected double m_RawY = 0.0;
        protected double m_RawZ = 0.0;
        public double RawX { get { return m_RawX; } set { m_RawX = value; } }
        public double RawY { get { return m_RawY; } set { m_RawY = value; } }
        public double RawZ { get { return m_RawZ; } set { m_RawZ = value; } }

        protected double m_RealX = 0.0;
        protected double m_RealY = 0.0;
        protected double m_RealZ = 0.0;
        public double RealX { get { return m_RealX; } set { m_RealX = value; } }
        public double RealY { get { return m_RealY; } set { m_RealY = value; } }
        public double RealZ { get { return m_RealZ; } set { m_RealZ = value; } }

        protected double m_TiltX = 0.0;
        protected double m_TiltY = 0.0;
        protected double m_TiltZ = 0.0;
        public double TiltX { get { return m_TiltX; } set { m_TiltX = value; } }
        public double TiltY { get { return m_TiltY; } set { m_TiltY = value; } }
        public double TiltZ { get { return m_TiltZ; } set { m_TiltZ = value; } }

        public RawDataRead(double _Time, double _AbsoluteG,
                           double _RealX, double _RealY, double _RealZ,
                           double _RawX, double _RawY, double _RawZ,
                           double _AngleX, double _AngleY, double _AngleZ)
        {
            Time = _Time;
            AbsoluteG = _AbsoluteG;
            
            RealX = _RealX;
            RealY = _RealY;
            RealZ = _RealZ;

            RawX = _RawX;
            RawY = _RawY;
            RawZ = _RawZ;

            TiltX = _AngleX;
            TiltY = _AngleY;
            TiltZ = _AngleZ;
        }

        public RawDataRead()
        {
        }
    }
}
