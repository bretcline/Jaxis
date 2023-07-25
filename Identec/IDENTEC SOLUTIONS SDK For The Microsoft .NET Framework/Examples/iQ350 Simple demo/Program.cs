using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;

using IDENTEC;
using IDENTEC.ILRGen3;
using IDENTEC.ILRGen3.Readers;
using IDENTEC.ILRGen3.Tags;

// basic demonstration to demonstrate how to use the Gen3 APIs
// needs to add try catch for all codes

namespace Simple_Gen3_Test
{
    class Program
    {

        static void Main(string[] args)
        {

            Application app = new Application();
            try
            {
                app.Run();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception during execution :" + ex.Message);
            }
        }

    }

    class Application
    {
        IDENTEC.ILRGen3.Readers.Gen3Reader readerToUse = null;
        IDENTEC.ILRGen3.Tags.Gen3TagCollection TagList = null;

        public void Run()
        {
            ///enumerate the reader and keep only the first ILRGen3 reader
            IDENTEC.DataStream stream = null;
            bool UseTCP = false;
            if (UseTCP)
                stream = new TCPSocketStream("192.168.2.246", 2101);
            else
            {
                stream = new SerialPortStream("COM2");
                /// the following code could be used to find an iCard CF 350
                ///int Port = IDENTEC.Readers.CFReaderSearch.FindReaderComPort();
                ///stream = new SerialPortStream(Port);
            }
            try
            {
                stream.Open();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to open interfac :" + ex.Message);
                return;
            }
             Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            // this is the main process to find out the readers connected to stream
            this.FindReader(stream);

            this.ConfigureReader(readerToUse);

            this.ConfigureOptionnalReaderParameter(readerToUse);

            this.ConfigureReaderListManagement(readerToUse);

            do
            {
                // demo how to find a tag which is not beaconing
                this.FindTag(123099);

                // demo on how to scan for tags
                TagList = readerToUse.ScanForTags(10, true, new TimeSpan(0,0,0,0,100));
                TagList = readerToUse.ScanForTags(20, true);
                TagList = readerToUse.ScanForTags(20, false);
                Console.WriteLine("Scanned " + TagList.Count.ToString());

                if (TagList.Count != 0)
                    this.ConfigureTagBeaconInfo(TagList[0]);
                /// This is how to read data from tag
                foreach (IDENTEC.ILRGen3.Tags.Gen3Tag tag in TagList)
                {
                    this.TagReadWrite(tag);
                }

                /// This is how to read temperature log info
                foreach (IDENTEC.ILRGen3.Tags.Gen3Tag tag in TagList)
                {
                    IDENTEC.ILRGen3.Tags.iQ350Logger TempTag = tag as IDENTEC.ILRGen3.Tags.iQ350Logger;

                    if (TempTag != null)
                    {
                        this.ReadLogInfo(TempTag);
                        this.ReadTemperatureExtremes(TempTag);
                    }
                }

                /// This is how to read temperature log
                foreach (IDENTEC.ILRGen3.Tags.Gen3Tag tag in TagList)
                {
                    if (tag is IDENTEC.ILRGen3.Tags.ILoggerInterface)
                        this.TagReadTemperatureLog((ILoggerInterface)tag);
                }

                /// This is how to start/stop the logger
                foreach (IDENTEC.ILRGen3.Tags.Gen3Tag tag in TagList)
                {
                    if (tag is IDENTEC.ILRGen3.Tags.ILoggerInterface)
                    {
                        this.TagStartStopLogger((ILoggerInterface)tag, true);
                    }
                }


                // let the reader some time to receive some beacon message
                Thread.Sleep(1000);

                /// now query the reader for the list of beacon tag messages
                TagList = readerToUse.GetBeaconTags();

                Console.WriteLine("Beacon message  " + TagList.Count.ToString());

                /// now process the beacon messages
                foreach (IDENTEC.ILRGen3.Tags.Gen3Tag tag in TagList)
                {
                    this.ProcessTagBeaconMessage(tag);

                }


                Thread.Sleep(5000);
                 if (Console.KeyAvailable)
                    break;
            } while (true);

            stream.Close();
        }

        public Application()
        {
        }
        
        /// <summary>
        /// Will enumerates the reader and select the first Gen3 reader
        /// </summary>
        /// <param name="stream">The astream to use</param>
        public void FindReader(IDENTEC.DataStream stream)
        {
            iBusAdapter myBus = new iBusAdapter(stream);
            IDENTEC.IBusDevice[] devices = myBus.EnumerateBusModules();
            /// for demo we use only the first ILRGen3 reader
            foreach (IDENTEC.IBusDevice dev in devices)
            {
                readerToUse = dev as IDENTEC.ILRGen3.Readers.Gen3Reader;
                if (readerToUse != null)
                    break;
            }
            if (readerToUse != null)
                Console.WriteLine(String.Format("Reader : {0} {1}",readerToUse.SerialNumber,readerToUse.Information)); 
        }

        /// <summary>
        /// The method changes the reader critical parameters
        /// If 1 of the parameters is not correct we may not be able to communicate with the tag
        /// </summary>
        /// <param name="reader"></param>
        public void ConfigureReader(IDENTEC.ILRGen3.Readers.Gen3Reader reader)
        {
            /// make sure that we use default parameters
            /// If we do not reset to factory default we have to make sure to set all parameters 
            /// of the reader that may have impact on the application
            /// WARNING resetting to factory default will erase all tags in the reader internal list
            reader.ResetToFactoryDefault();

            /// now we set the parameters we want to use
            /// the tag default beacon baudrate is 115200 so use 115200
            /// WARNING: the beacon baudrate is not the communication baudrate see reader.RFBaudRate
            reader.SetRFBeaconBaudrate(RFBaudRate.RF_115200);

            reader.SetFrequency(Frequency.European);

            /// The wake up duration is duration of the wake up signal, the reader will send to wake up a tag
            /// 
            /// the wake up duration is on most tags 2 seconds but on special tags it could be different
            /// so changing the wake up duration must be done only if you know you have a special tag
            /// otherwise this value is defined by default to 0.
            /// a wake up duration of 0 means the SDK will automatically select the correct wakeUpDuration
            /// reader.WakeUpDuration = new TimeSpan(0,0,2);


        }


        /// <summary>
        /// This shows how to set optionnal parameters
        /// changing those parameters will only have some impact on the performance
        /// </summary>
        /// <param name="reader"></param>
        public void ConfigureOptionnalReaderParameter(IDENTEC.ILRGen3.Readers.Gen3Reader reader)
        {
            /// the TX power must be set based on the antenna used and the expected range
            reader.TXPower = 5;

            /// the following parameters are optionnal, and should be changed only for fine tuning of the system
            /// we recommend to use the lowest baudrate, 
            /// increasing the baudrate will reduce the range and communication reliability.
            /// Using a higher range is recommended only when trying to read/write a lot of data 
            /// like for example the temperature log
            /// CAUTION: changing the communication baudrate must be done only when the tag is sleeping and never in the middle of a communication
            reader.RFBaudRate = RFBaudRate.RF_115200;            
        }

        /// <summary>
        /// this shows how to configure the reader internal bacon list
        /// </summary>
        /// <param name="reader"></param>
        public void ConfigureReaderListManagement(IDENTEC.ILRGen3.Readers.Gen3Reader reader)
        {
            /// Beacon list management configuration
            /// all the following parameters define the management of the beacon tag list on the reader
            /// 
            /// This is the maximum number of beaconed bytes saved in the list
            /// A tag can send a maximum of 50 bytes.
            /// WARNING calling this methodt will erase all tags in the reader internal list
            reader.SetDataLen(50);

            /// The following parameter defines what the reader will do when a tag has been reported:
            /// remove the tag in the list or leave it
            reader.SetTagListBehavior(IDENTEC.Readers.BeaconReaders.TagListBehavior.RemoveTagsWhenReported);
            /// Defines when the tag is removed from the list
            /// If a tag has been reported and not seen for the amount of time specified in the method
            /// Then after this time the tag is removed from the reader list
            /// 0 --> tag will never be removed from the list, it will be removed only when the list is full
            reader.SetTagListInhibitTime(new TimeSpan(0, 0, 60));
            /// Specify if we want to report multiple time a tag.
            /// If a tag has been reported and is still detected by the reader (beacon message)
            /// it will be reported again but only after the rereport interval time has elapsed
            /// since last time it was reported
            /// Setting a rereport interval of 10 minutes means the tag will only be reported 
            /// every 10 minutes if it is in the reader field.
            /// 0 --> the tag will never be reported unless it is removed form the list and added again
            reader.SetTagReReportingInterval(new TimeSpan(0, 0, 60));

            /// This is the beacon message RSSI limit the reader will use to process the message. 
            /// By setting an RSSI level of -60, the reader will discard all beacon message with an RSSI level below -60
            /// minimum rssi level the readeer can detect is approx -100 so setting a value to -128 mean the reader will not discard any beacon message.
            reader.SetTagSignalFilterLevel(-128);
        }

        /// <summary>
        /// this method demonstrate how to read or write data to a tag
        /// </summary>
        /// <param name="tag"></param>
        public void TagReadWrite(IDENTEC.ILRGen3.Tags.Gen3Tag tag)
        {
            byte[] data = new byte[5];
            /// we read 5 bytes at address 0
            IDENTEC.Tags.TagReadDataResult result = tag.ReadData(readerToUse, 0, 5, 2);

            /// we copy the 5 bytes read in a new byte array
            if (result.BytesRead >= 5)
                Array.Copy(result.Data, data, 5);
            /// we write it back to teh tag at address 100
            IDENTEC.Tags.TagWriteDataResult resultwrite = tag.WriteData(readerToUse, 100, data, data.Length, 5);

            IDENTEC.ILRGen3.Tags.Info.TagDescription des = tag.description;
            Console.WriteLine("tag : " + tag.SerialLabel + " " + tag.description.Name + " RSSI " + tag.MaxSignal.ToString());
        }

        /// <summary>
        /// This method demonstrate how to read the logger
        /// and access the log sample data
        /// </summary>
        /// <param name="tag"></param>
        public void TagReadTemperatureLog(IDENTEC.ILRGen3.Tags.ILoggerInterface tag)
        {
            if (tag == null)
                return;
            /// by reading the log, you will get the log data and log status
            IDENTEC.Tags.Logging.RawLogData log = null;
            log = tag.ReadLastnLogSamples(readerToUse, 0, 20);

            /// display the temperature data
            IDENTEC.Tags.Logging.TemperatureLogData temLog = log as IDENTEC.Tags.Logging.TemperatureLogData;
            if (temLog != null)
            {
                IDENTEC.Tags.Logging.TemperatureLogSampleCollection samples = temLog.Samples;
                int SamplePerLine = 6;
                foreach (IDENTEC.Tags.Logging.TemperatureLogSample sample in samples)
                {
                    Console.Write(sample.SampleTime.ToString() + " " + sample.DegreesCelsius.ToString("F1") + "  ");
                    if (--SamplePerLine == 0)
                    {
                        SamplePerLine = 6;
                        Console.WriteLine();
                    }
                }
                Console.WriteLine();
                Console.WriteLine("Number of sample read :" + log.SampleCount + " out of " + log.LogInfo.TagSampleCount.ToString() + " stored in tag");
                Console.WriteLine("lowest : " + temLog.LowestTemperatureRecord.ToString() + " highest : " + temLog.HighestTemperatureRecord.ToString());
            }
        }


        /// <summary>
        /// This method shows hwo to configure the tag beacon information
        /// </summary>
        /// <param name="tag">The tag to configure</param>
        public void ConfigureTagBeaconInfo(IDENTEC.ILRGen3.Tags.Gen3Tag tag)
        {
            BeaconInformation info;
            TimeSpan interval = TimeSpan.MinValue;
            byte[] UserData;
            // the following call will read the full reader beacon configuration
            tag.ReadBeaconConfiguration(readerToUse, out info, out interval, out UserData);
            Console.WriteLine("Tag :" + tag.SerialLabel + " beacon interval "
                + interval.ToString() + " beacon data " + info.ToString());

            /// this allows to read only the beacon interval
            interval = tag.ReadBeaconInterval(readerToUse);
            // this method allows to read the user data
            UserData = tag.ReadBeaconUserData(readerToUse);

            /// the following calls will set the beacon configuration
            /// Here the tag will beacon every 20 seconds
            tag.WriteBeaconInterval(readerToUse, new TimeSpan(0, 0, 20));
            byte[] data = System.Text.Encoding.ASCII.GetBytes("Beacon info");
            /// If the tag is set to beacon user data it will beacon "Beacon info"
            /// Caution the tag can beacon only 50 bytes in total so if the tag is also beaconing other information
            /// like marker or temperature, and the user data length is too long
            /// the tag will truncate the user data to the maximum size it can transmit
            tag.WriteBeaconUserData(readerToUse, data, data.Length);
            /// The tag is set to beacon the marker info and the user data
            tag.WriteBeaconConfiguration(readerToUse, BeaconInformation.Marker | BeaconInformation.UserData);
            /// The tag is set to beacon the marker info and the user data and the temperature (if we have a temperature tag)
            tag.WriteBeaconConfiguration(readerToUse, BeaconInformation.Marker | BeaconInformation.UserData | BeaconInformation.Temperature | BeaconInformation.DigitalIO);
            /// the following call will disable the beacon messages
            tag.WriteBeaconConfiguration(readerToUse, BeaconInformation.None);
        }

        /// <summary>
        /// This method shows how to configure the tag event beacon 
        /// </summary>
        /// <param name="tag">The tag to configure</param>
        public void ConfigureTagEventBeaconInfo(IDENTEC.ILRGen3.Tags.Gen3Tag tag)
        {
            BeaconInformation info;
            TimeSpan interval = TimeSpan.MinValue;
            int burstNumber;

            /// first we read teh current information
            tag.ReadEventConfiguration(readerToUse, EventType.Marker, out info, out interval, out burstNumber);
            Console.WriteLine("Tag :" + tag.SerialLabel + " marker event "
                + info.ToString() + " interval  " + interval.ToString() + " burst " + burstNumber);

            /// the following call will request the tag to beacon the user data
            /// 5 times randomly in interval slot of 2 seconds
            /// Note that the marker information will always be sent even if not requested
            tag.WriteEventConfiguration(readerToUse, EventType.Marker, BeaconInformation.UserData, new TimeSpan(0,0,0,2), 5);

        }

        /// <summary>
        /// This method demonstrate how to read the logger state
        /// is the tag logging, when the loggin was started, how many samples have been logged
        /// </summary>
        /// <param name="tag"></param>
        public void ReadLogInfo(IDENTEC.ILRGen3.Tags.iQ350Logger tag)
        {
            /// for information we read the measurement interval
            TimeSpan interval = tag.ReadMeasurementInterval(readerToUse);
            Console.WriteLine("Tag "  + tag.SerialLabel + " measurement interval " + interval.ToString());

            /// reading the log info will allow to check all the tag loggin status
            IDENTEC.Tags.Logging.LogInfoData info = tag.ReadLogInformation(readerToUse);
            if (info != null)
            {
                Console.WriteLine("tag " + tag.SerialLabel + " logging state: " + info.IsLogging.ToString() + " wrapped " + info.Wrapped.ToString());
                Console.WriteLine("Interval :" + info.LoggingInterval.Minutes.ToString() + ":" + info.LoggingInterval.Seconds.ToString());
                Console.WriteLine("Start Time:" + info.LoggerStarted.ToString());
                if (!info.IsLogging)
                    Console.WriteLine("Stop Time :" + info.LoggerStopped.ToString());
            }
       }
       /// <summary>
        /// This method demonstrates how to read the maximum and minimum temperature measured by the tag
        /// </summary>
        /// <param name="tag"></param>
        public void ReadTemperatureExtremes(IDENTEC.ILRGen3.Tags.iQ350Logger tag)
        {
            /// for information we read the measurement interval
            IDENTEC.Tags.Logging.TemperatureExtremes data = tag.ReadTTemperatureExtremes(readerToUse);
            Console.WriteLine("Tag "  + tag.SerialLabel + " max temp " + data.MaximumDegreesCelsius.ToString() +
                 " min temp " + data.MinimumDegreesCelsius.ToString());
            Console.WriteLine("logger started " + data.LogStart.ToString() + " logger end time" + data.LogEnd.ToString());
       }

        /// <summary>
        /// this method demonstrate how to start/stop the logger
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="start"></param>
        public void TagStartStopLogger(IDENTEC.ILRGen3.Tags.ILoggerInterface tag, bool start)
        {
            if (tag == null)
                return;
            if (start)
            {
                IDENTEC.ILRGen3.Tags.iQ350Logger LoggerTag = tag as IDENTEC.ILRGen3.Tags.iQ350Logger;
                if (LoggerTag != null)
                {
                    /// we set the measurement interval to the same value as loggin interval
                    /// so each sample is a new value instead of an average of multiple samples
                    LoggerTag.WriteMeasurementInterval(readerToUse, new TimeSpan(0, 10, 0));
                    /// in this call we set the measurement interval to twice the logging interval
                    /// meaning each log sample will be the average of 2 samples
                    LoggerTag.WriteMeasurementInterval(readerToUse, new TimeSpan(0, 5, 0));
                }
                tag.StartLogging(readerToUse, new TimeSpan(0, 10, 0));  // start logging with a 10 minutes interval
            }
            else
                tag.StopLogging(readerToUse);
        }

        /// <summary>
        /// This method demonstrate how to process th ebeacon mesage received
        /// </summary>
        /// <param name="tag"></param>
        public void ProcessTagBeaconMessage(IDENTEC.ILRGen3.Tags.Gen3Tag tag)
        {
            if (tag == null)
                return;
            string tagType;
            if (tag.description != null)
                tagType = tag.description.Name;
            else
                tagType = tag.TypeID.ToString();
            Console.WriteLine(tag.SerialLabel + " " + tagType + " " + tag.BeaconMessageType.ToString() + " Received :" + tag.TimeLastSeen.ToString());
            List<IDENTEC.Utilities.BeaconData.BeaconInfo> infos = IDENTEC.Utilities.BeaconData.BeaconDataConverter.Convert(tag);
            if (infos != null)
                foreach (IDENTEC.Utilities.BeaconData.BeaconInfo info in infos)
            {
                /// check if we have marker information
                IDENTEC.Utilities.BeaconData.LFMarker marker = info as IDENTEC.Utilities.BeaconData.LFMarker;
                if (marker != null)
                {
                    Console.WriteLine("loop ID: " + marker.NewerPosition.LoopID.ToString() + " time :" + marker.NewerPosition.PositionTime.ToString());
                }
                // check if we have temperature
                IDENTEC.Utilities.BeaconData.Temperature temperature = info as IDENTEC.Utilities.BeaconData.Temperature;
                if (temperature != null)
                {
                    Console.WriteLine("pos: " + temperature.Position.ToString() + " temperature :" + temperature.Celsius.ToString("F2"));
                }
                // check if user data has been sent
                IDENTEC.Utilities.BeaconData.UserData userData = info as IDENTEC.Utilities.BeaconData.UserData;
                if (userData != null)
                {
                    Console.WriteLine("User Data: " + userData.ToString());
                }
                // check if we have a DIgital IO info
                // check if user data has been sent
                IDENTEC.Utilities.BeaconData.DigitalIO IO = info as IDENTEC.Utilities.BeaconData.DigitalIO;
                if (IO != null)
                {
                    Console.WriteLine("DIgitalIO: " + IO.ToString());
                }
            } 
        }

        public void FindTag(uint ID)
        {
            IDENTEC.ILRGen3.Tags.Gen3Tag tag = readerToUse.PingTag((int)ID);
            if (tag != null)
            {
                Console.WriteLine("Found tag " + tag.SerialLabel);
                if (tag.description != null)
                    Console.WriteLine("type of tag " + tag.description.Name);
            }
            else
                Console.WriteLine("tag not found");
        }
    }
}
