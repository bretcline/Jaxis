using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Freescale.ZStarLib;
using JaxisInterfaces;

namespace BucketCollectionLib
{
    public class BucketProcessor : IDisposable
    {
        List<DataRead> m_Reads = new List<DataRead>();
        DataRead m_PreviousRead = null;

        //private ZStar3 theZStar = new ZStar3();
        //private ZStarSensor theSensor = null;

        public event ProduceHandler MessageDelegate;

       PourMessage m_PMessage = null;

        public BucketProcessor()
        {
            //theSensor = theZStar.GetSensor(0);

            //theZStar.OnBurstDataReceived += new ZStar3.OnBurstDataReceivedHandler(theZStar_OnBurstDataReceivedProcess);
            //theZStar.OnActiveSensorsChanged += new ZStar3.OnActiveSensorsChangedHandler(theZStar_OnActiveSensorsChanged);
            //theZStar.OnConnectionLost += new ZStar3.OnConnectionLostHandler(theZStar_OnConnectionLost);
        }

        //public void Run()
        //{
            
        //}

        public void GenerateSampleData()
        {
            PourMessage Message = new PourMessage();
            Message.ReadTime = DateTime.Now;
            Dictionary<AngleBucket, double> SampleBucketData = new Dictionary<AngleBucket, double>();
            Random RandomTimeGenerator = new Random();

            foreach(AngleBucket AB in Enum.GetValues(typeof(AngleBucket)))
            {
                SampleBucketData[AB] = RandomTimeGenerator.Next(0, 60001);
            }

            Message.Buckets = SampleBucketData;

            if (null != MessageDelegate)
            {
                MessageDelegate(Message);
            }
        }

//        void theZStar_OnActiveSensorsChanged(object sender, byte sensor)
//        {
//            // Is first sensor present???
//            if ((theZStar.ActiveSensorsMask & 0x0001) == 0x0001)
//            {
//                // Enable  Burst Data Receive for this sensor 
//                theSensor.BurstDataReceiveEnable = true;
//                // Enable global Burst mode 
//                theZStar.BurstModeEnabled = true;
//            }
//            else // Sensor index 0 is not connected            
//            {
//                // Disable  Burst Data Receive for this sensor
//                theSensor.BurstDataReceiveEnable = false;
//                // Disable global Burst mode
//                theZStar.BurstModeEnabled = false;
//            }
//        }

//        void theZStar_OnConnectionLost(object sender)
//        {
//            // Show Error Message
//            //Console.WriteLine("ComPort connection lost! Press any key to continue.");
//            //Console.ReadLine();
//        }

//        void theZStar_OnBurstDataReceivedProcess(object sender, byte sensor)
//        {
//            // Are these data from our sensor 0
//            if (sensor == 0)
//            {

//                while (theSensor.GetBurstData()) // Get all pending data in Burst Data FIFO Buffer
//                {
//                    DataRead Read = new DataRead(theSensor.TiltY, theSensor.BurstTime);
//                    double ElapsedTime = 0;

//                    if (60 <= Read.Angle)
//                    {
//                        if (m_PMessage == null)
//                        {
//                            m_PMessage = new PourMessage();
//                            m_PMessage.ReadTime = DateTime.Now;
//                        }

//                        if (null != m_PreviousRead)
//                        {
//                            ElapsedTime = Read.Time - m_PreviousRead.Time;
//                        }
//                        else
//                        {
//                            ElapsedTime = Read.Time;
//                        }

//                        AngleBucket Key = (AngleBucket)(int)(Read.Angle / 20);
                        
//                        m_PMessage.Buckets[Key] = m_PMessage.Buckets[Key] + ElapsedTime;
//                    }
//                    else
//                    {
//                        if (m_PMessage != null)
//                        {
//                            m_PMessage.PourStop = DateTime.Now;
//                            if (null != MessageDelegate)
//                            {
//                                MessageDelegate( m_PMessage );
//                            }
//                            m_PMessage = null;
//                        }
//                    }

//                    m_PreviousRead = Read;
//                }
//            }
//        }

//        public string OpenComPort()
//        {
//            if (!theZStar.IsPortOpen)
//            {
//                // If ComPort exist and ZStarLib has closed port
//                // Try to open ComPort
//                ComPortInfo[] ports = ZStar3.GetComPorts();
//                ComPortInfo ZStarPort = new ComPortInfo("", "", 0);

//                foreach (ComPortInfo CPI in ports)
//                {
//                    if(CPI.FriendlyName.Contains("ZSTAR"))
//                    {
//                        ZStarPort = CPI;
//                    }
//                }

//                if (!theZStar.OpenPort((ZStarPort).PortNum))
//                {
//                    // If Failed, show Error message 
//                    return "Open ComPort failed!";
//                }

//                // Check USB_Stick type (has to be known!!)
//                if (theZStar.ZStarUsbStickType == ZStar3.UsbStickType.Unknown)
//                {
//                    // Unknown USB Stick type
//                    // Close opened port
//                    theZStar.ClosePort();
//                    // And show Error Message
//                    return "This is not a ZStar3 Device.";
//                }

//#warning KDC -- I do not fully understand if/how this works.
//                //KDC -- I added this code myself to check if the sensor is already on when the program is run.
//                if (theSensor.IsActive == false)
//                {
//                    // Enable  Burst Data Receive for this sensor 
//                    theSensor.BurstDataReceiveEnable = true;
//                    // Enable global Burst mode 
//                    theZStar.BurstModeEnabled = true;
//                }

//                // Disable for all sensor burst data
//                //KDC -- I commented out the line below this.
//                //theZStar.BurstDataReceiveEnableMask = 0x0000;

//                // Keep new sensors(without power switch) sensor awake up when ZStarLib runs
//                theZStar.SleepDisabled = true;
//                return "ComPort opened successfully.";
//            }
//            return "The ComPort is already open.";
//        }

//        public void CloseComPort()
//        {
//            if (theZStar.IsPortOpen)
//            {
//                // Switch of sleepDisabled Sensor capatibilities
//                theZStar.SleepDisabled = false;
//                // Switch of Burst mode of ZStar 
//                theZStar.BurstDataReceiveEnableMask = 0x0000;
//                theZStar.BurstModeEnabled = false;

//                // Close Port
//                theZStar.ClosePort();
//            }
//        }

        public void Dispose()
        {
            //CloseComPort();
        }
    }

     public class DataRead
    {
        protected double m_Angle = 0.0;
        public double Angle { get { return m_Angle; } set { m_Angle = 90.0 - value; } }
        public double Time { get; set; }

        public DataRead(double _Angle, double _Time)
        {
            Angle = _Angle;
            Time = _Time;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}", Angle, Time);
        }

    }


    public enum AngleBucket
    {
        Angle80 = 3,
        Angle100 = 4,
        Angle120 = 5,
        Angle140 = 6,
        Angle160 = 7,
        Angle180 = 8,
    }
}
