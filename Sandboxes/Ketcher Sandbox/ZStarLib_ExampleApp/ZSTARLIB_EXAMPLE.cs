using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Freescale.ZStarLib;       // 1. step: Add Freescale.ZStarLib to using
using System.IO;


namespace ZStarLib_ExampleApp
{
    public partial class ZSTARLIB_EXAMPLE : Form
    {
        List<DataRead> m_Reads = new List<DataRead>();
        bool m_Start = false;

        private ZStar3 theZStar = new ZStar3();     // 2. step: Allocate new variable as a ZStar Class (Common functions and all events)
        private ZStarSensor theSensor = null;       // 3. step: Allocate new variable as a ZStarSensor Class (Sensor things) this can be allocated as a array up to 16 
        
        public ZSTARLIB_EXAMPLE()
        {
            theSensor = theZStar.GetSensor(0);      // 4. step: Assign right sensor that you want to use (or all for array of 16 elements)
            InitializeComponent();

            // Event when new burst data was received
            theZStar.OnBurstDataReceived += new ZStar3.OnBurstDataReceivedHandler(theZStar_OnBurstDataReceived);
            //KDC -- Added new handler for burst data received.
            //theZStar.OnBurstDataReceived += new ZStar3.OnBurstDataReceivedHandler(theZStar_OnBurstDataReceivedProcess);
            // Event that occur when list of Active sensor was changed
            theZStar.OnActiveSensorsChanged += new ZStar3.OnActiveSensorsChangedHandler(theZStar_OnActiveSensorsChanged);
            // Event that occur if ZStarLib lost Connection on Comport
            theZStar.OnConnectionLost += new ZStar3.OnConnectionLostHandler(theZStar_OnConnectionLost);
        }


        // List Of Active sensor was changed
        void theZStar_OnActiveSensorsChanged(object sender, byte sensor)
        {
            // Is first sensor present???
            if ((theZStar.ActiveSensorsMask & 0x0001) == 0x0001)
            {
                // Enable  Burst Data Receive for this sensor 
                theSensor.BurstDataReceiveEnable = true;
                // Enable global Burst mode 
                theZStar.BurstModeEnabled = true;
                // Switch status label to Connected
                lb_Status.Text = "Connected";
            }
            else // Sensor index 0 is not connected            
            {
                // Disable  Burst Data Receive for this sensor
                theSensor.BurstDataReceiveEnable = false;
                // Disable global Burst mode
                theZStar.BurstModeEnabled = false;
                // Switch status label to DisConnected
                lb_Status.Text = "Diconnected";
            }
        }

        // ZStarLib lost connection with ZStar hardware
        void theZStar_OnConnectionLost(object sender)
        {
            // Perform Close port button
            bt_ClosePort.PerformClick();
            // Show Error Message
            MessageBox.Show("ComPort connection lost!", "ZStarLib connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
     
        // New Burst data was received
        void theZStar_OnBurstDataReceived(object sender, byte sensor)
        {
            // Are these data from our sensor 0
            if (sensor == 0)
            {
                while (theSensor.GetBurstData()) // Get all pending data in Burst Data FIFO Buffer
                {
                    DataRead Read = new DataRead(theSensor.TiltY, theSensor.BurstTime);

                    // Update labels with values of actual acceleration for all axes
                    lb_AxisY.Text = String.Format("{0:F2}", Read.Angle);
                    lblTimeDisplay.Text = theSensor.BurstTimeText;

                    if (m_Start == true)
                    {
                        if (60 <= Read.Angle)
                        {
                            m_Reads.Add(Read);
                        }
                    }
                }
            }
        }

        // On load form
        private void ZSTARLIB_EXAMPLE_Load(object sender, EventArgs e)
        {
            // Refresh List of ComPorts
            bt_RefreshComPort.PerformClick();
        }

        // Refresh ComPorts Button
        private void bt_RefreshComPort_Click(object sender, EventArgs e)
        {
            // Get all ComPorts in PC
            ComPortInfo[] ports = ZStar3.GetComPorts();

            // Clear ListBox of Comports
            cb_ComPortList.Items.Clear();

            // walk through all Comports
            foreach (ComPortInfo p in ports)
            {
                // Add each to Comport list
                int ix = cb_ComPortList.Items.Add(p);
                
                // Check name and look for ZSTAR device
                if (p.FriendlyName.Contains("ZSTAR"))
                {
                    // remember last index of ZSTAR port
                    cb_ComPortList.SelectedIndex = ix;                    
                }
            }

            // If ZSTAR ComPort wasn't found 
            if (cb_ComPortList.SelectedIndex == -1)
            {
                // Select first in List
                cb_ComPortList.SelectedIndex = 0;
            }

        }

        // Open Port Button Action
        private void bt_OpenPort_Click(object sender, EventArgs e)
        {
            // Check ZStarLib ComPort status and selected item in List box
            if (!theZStar.IsPortOpen && cb_ComPortList.SelectedItem != null)
            {
                // If ComPort exist and ZStarLib has closed port
                // Try to open selected port
                if (!theZStar.OpenPort(((ComPortInfo)cb_ComPortList.SelectedItem).PortNum))
                {
                    // If Failed, show Error message 
                    MessageBox.Show("OpenPort failed!", "ZStarLib connection", MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                    // And go out
                    return;
                }

                // Check USB_Stick type (has to be known!!)
                if (theZStar.ZStarUsbStickType == ZStar3.UsbStickType.Unknown)
                {
                    // Unknown USB Stick type
                    // Close opened port
                    theZStar.ClosePort();
                    // And show Error Message
                    MessageBox.Show("This is not ZStar3 Device!!!");
                    return;
                }
                
                //KDC -- See if there is a sensor and if so, connect to it.
                if (theSensor.IsActive == false)
                {
                    // Enable  Burst Data Receive for this sensor
                    theSensor.BurstDataReceiveEnable = true;
                    // Enable global Burst mode
                    theZStar.BurstModeEnabled = true;
                    // Switch status label to Connected
                    lb_Status.Text = "Connected";
                }
   
                // Disable / Enable all depends objects

                // SetUp Enable of all controls on form
                cb_ComPortList.Enabled = false;
                bt_RefreshComPort.Enabled = false;
                bt_OpenPort.Enabled = false;
                bt_ClosePort.Enabled = true;

                // Disable for all sensor burst data
                //KDC -- I commented out the line below this.
                //theZStar.BurstDataReceiveEnableMask = 0x0000;
                
                // Keep new sensors(without power switch) sensor awake up when ZStarLib runs
                theZStar.SleepDisabled = true;
            }
        }

        // Close port button action
        private void bt_ClosePort_Click(object sender, EventArgs e)
        {
            // Switch of sleepDisabled Sensor capatibilities
            theZStar.SleepDisabled = false;
            // Switch of Burst mode of ZStar 
            theZStar.BurstDataReceiveEnableMask = 0x0000;
            theZStar.BurstModeEnabled = false;

            // Close Port
            theZStar.ClosePort();

            // SetUp Form
            lb_Status.Text = "Diconnected";
            cb_ComPortList.Enabled = true;
            bt_RefreshComPort.Enabled = true;
            bt_OpenPort.Enabled = true;
            bt_ClosePort.Enabled = false;
        }

        // Form closed event
        private void ZSTARLIB_EXAMPLE_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Perform Close Port button
            bt_ClosePort.PerformClick();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            m_Start = false;
            StreamWriter Writer = new StreamWriter(String.Format("SensorReaderFile_{0}.txt", DateTime.Now.Ticks), false);
            foreach (DataRead DR in m_Reads)
            {
                Writer.WriteLine(DR);
            }

            Writer.Close();
            m_Reads.Clear();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            m_Start = true;
        }

    }

    public class DataRead
    {
        protected double m_Angle = 0.0;
        public double Angle { get{ return m_Angle;}  set { m_Angle = 90.0 - value; } }
        public double Time { get; set; }

        public DataRead(double _Angle, double _Time)
        {
            Angle = _Angle;
            Time = _Time;
        }

        public override string ToString()
        {
            return string.Format( "{0}, {1}", Angle, Time );
        }

    }
}