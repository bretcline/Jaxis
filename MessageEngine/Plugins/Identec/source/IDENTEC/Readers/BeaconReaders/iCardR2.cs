namespace IDENTEC.Readers.BeaconReaders
{
    using IDENTEC;
    using IDENTEC.Readers;
    using IDENTEC.Tags;
    using IDENTEC.Tags.BeaconTags;
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading;

    public class iCardR2 : iCard
    {
        private AutoResetEvent m_arePumpTags = new AutoResetEvent(false);
        private bool m_bLongRangeEnabled;
        private bool m_bRunning;
        private ISolProtocolFramer m_comm = new ISolProtocolFramer2();
        private TagCollection m_tags = new TagCollection();
        private Thread m_ThreadListen;
        private Thread m_ThreadPumpTags;
        private const int rec_time = 100;

        public event iCardR2ErrorEventHandler ErrorOccurred;

        public event TagBeaconEventHandler TagBeacon;

        public bool Connect()
        {
            return this.Connect(1);
        }

        public override bool Connect(int port)
        {
            if (!this.m_comm.ConnectPCMCIA(port))
            {
                return false;
            }
            Thread.Sleep(20);
            this.ReadVersion();
            this.ReadProductionInformation();
            if (this.Information.IndexOf("IB2") < 0)
            {
                this.Disconnect();
                throw new InvalidOperationException("The device is not an i-CARD R2");
            }
            return true;
        }

        public bool ConnectRS232(int port)
        {
            if (this.m_comm.ConnectSerialPort(port))
            {
                Thread.Sleep(20);
                this.ReadVersion();
                this.ReadProductionInformation();
                return true;
            }
            return false;
        }

        public override bool Disconnect()
        {
            this.StopListening();
            return this.m_comm.Disconnect();
        }

        private void ListenForTagsThread()
        {
            while (this.m_bRunning)
            {
                Thread.Sleep(100);
                iB2Tag item = null;
                try
                {
                    item = this.TagTelegramResponse();
                }
                catch (CommPortException exception)
                {
                    this.m_bRunning = false;
                    if (this.ErrorOccurred != null)
                    {
                        iCardR2ErrorEventArgs e = new iCardR2ErrorEventArgs {
                            ex = exception
                        };
                        this.ErrorOccurred(this, e);
                    }
                }
                catch (iCardCommunicationsException exception2)
                {
                    this.m_bRunning = false;
                    if (this.ErrorOccurred != null)
                    {
                        iCardR2ErrorEventArgs args2 = new iCardR2ErrorEventArgs {
                            ex = exception2
                        };
                        this.ErrorOccurred(this, args2);
                    }
                }
                catch (CRCException exception3)
                {
                    this.m_bRunning = false;
                    if (this.ErrorOccurred != null)
                    {
                        iCardR2ErrorEventArgs args3 = new iCardR2ErrorEventArgs {
                            ex = exception3
                        };
                        this.ErrorOccurred(this, args3);
                    }
                }
                if ((item != null) && (this.TagBeacon != null))
                {
                    lock (this.m_tags.SyncRoot)
                    {
                        this.m_tags.Add(item);
                    }
                    this.m_arePumpTags.Set();
                }
            }
        }

        private void PumpTagsThread()
        {
            do
            {
                while (!this.m_arePumpTags.WaitOne() || (this.TagBeacon == null))
                {
                }
                while (true)
                {
                    iB2Tag tag = null;
                    lock (this.m_tags.SyncRoot)
                    {
                        if (this.m_tags.Count == 0)
                        {
                            break;
                        }
                        tag = this.m_tags[this.m_tags.Count - 1] as iB2Tag;
                        this.m_tags.RemoveAt(this.m_tags.Count - 1);
                    }
                    TagBeaconEventArgs e = new TagBeaconEventArgs {
                        m_tag = tag
                    };
                    this.TagBeacon(this, e);
                }
            }
            while (this.m_bRunning);
        }

        private bool ReadConfig(ushort wStartAddress, byte[] byBuffer, ushort wLength)
        {
            byte[] msg = new byte[320];
            msg[0] = this.m_comm.m_byBusAddress;
            msg[1] = 0x34;
            msg[2] = (byte) (wStartAddress % 0x100);
            msg[3] = (byte) (wStartAddress / 0x100);
            msg[4] = (byte) wLength;
            this.m_comm.SendMessage(msg, 5);
            int length = this.m_comm.RecvMsg(msg, 300, 5);
            if (length <= 0)
            {
                return false;
            }
            if (length < 3)
            {
                return false;
            }
            if (msg[2] != 0)
            {
                return false;
            }
            length -= 3;
            Array.Copy(msg, 3, byBuffer, 0, length);
            return true;
        }

        private void ReadMaxRFOutputAndRegion()
        {
            byte[] byBuffer = new byte[8];
            this.ReadConfig(0x30, byBuffer, 3);
            switch (byBuffer[0])
            {
                case 0:
                    base.m_Freq = Frequency.European;
                    base.m_WorkingRegion = Reader.CompatibleRegion.EuropeanOnly;
                    return;

                case 1:
                    base.m_Freq = Frequency.NorthAmerican;
                    base.m_WorkingRegion = Reader.CompatibleRegion.NorthAmericanOnly;
                    return;
            }
            base.m_Freq = Frequency.NorthAmerican;
            base.m_WorkingRegion = Reader.CompatibleRegion.All;
        }

        internal void ReadProductionInformation()
        {
            byte[] byBuffer = new byte[10];
            if (this.ReadConfig(2, byBuffer, (ushort) byBuffer.Length))
            {
                string str = new string(Encoding.ASCII.GetString(byBuffer, 0, 10).ToCharArray());
                iCardProductionInformation information = new iCardProductionInformation();
                try
                {
                    information.nYear = 0x7d0 + int.Parse(str.Substring(0, 2));
                    information.nWeek = int.Parse(str.Substring(2, 2));
                    information.nProductionNumber = int.Parse(str.Substring(6, 4));
                    base.m_prod = information;
                    base.m_strSerialNumber = str;
                }
                catch (FormatException)
                {
                }
            }
        }

        internal void ReadRegionInfo()
        {
        }

        internal void ReadVersion()
        {
            byte[] msg = new byte[0x180];
            msg[0] = this.m_comm.m_byBusAddress;
            msg[1] = 0x33;
            this.m_comm.SendMessage(msg, 2);
            msg[0] = 0;
            msg[1] = 0;
            int num = this.m_comm.RecvMsg(msg, 0x3e8, 0);
            if (((num > 0) && ((this.m_comm.m_byBusAddress == 0) || (msg[0] == this.m_comm.m_byBusAddress))) && ((msg[1] == 0xb3) && (num >= 4)))
            {
                base.m_strInformation = msg[2].ToString() + "." + msg[3].ToString() + " " + Encoding.UTF8.GetString(msg, 4, 20);
            }
        }

        private void SendStartListeningMessageToCard(bool LongRangeEnabled)
        {
            byte[] msg = new byte[0x40];
            msg[0] = this.m_comm.m_byBusAddress;
            msg[1] = 0x3e;
            if (LongRangeEnabled)
            {
                msg[2] = 20;
            }
            else
            {
                msg[2] = 0x10;
            }
            this.m_comm.SendMessage(msg, 3);
        }

        public void StartListening(bool LongRangeEnabled)
        {
            this.m_bLongRangeEnabled = LongRangeEnabled;
            this.SendStartListeningMessageToCard(LongRangeEnabled);
            Thread.Sleep(50);
            this.StopListening();
            this.m_bRunning = true;
            this.m_ThreadPumpTags = new Thread(new ThreadStart(this.PumpTagsThread));
            this.m_ThreadPumpTags.Start();
            this.m_ThreadListen = new Thread(new ThreadStart(this.ListenForTagsThread));
            this.m_ThreadListen.Start();
        }

        public void StopListening()
        {
            if (this.m_ThreadListen != null)
            {
                this.m_bRunning = false;
                this.m_arePumpTags.Set();
                Thread.Sleep(300);
            }
        }

        private iB2Tag TagTelegramResponse()
        {
            byte[] msg = new byte[0x100];
            try
            {
                this.m_comm.RecvMsg(msg, 100, 0);
            }
            catch (iCardTimeoutException)
            {
                return null;
            }
            catch (ReaderTimeoutException)
            {
                return null;
            }
            if (0xb0 != msg[1])
            {
                return null;
            }
            uint id = BitConverter.ToUInt32(msg, 5);
            if (id == 0)
            {
                return null;
            }
            sbyte signal = (sbyte) msg[0x13];
            iB2Tag tag = new iB2Tag(id, DateTime.Now, signal) {
                m_nLowByteAgeCount = msg[5],
                m_nHighByteAgeCount = msg[4],
                m_byData = new byte[9]
            };
            Array.Copy(msg, 9, tag.m_byData, 0, 9);
            tag.CreateLoopData();
            return tag;
        }

        public override bool Connected
        {
            get
            {
                if (this.m_comm == null)
                {
                    return false;
                }
                return this.m_comm.IsOpen;
            }
        }

        public delegate void iCardR2ErrorEventHandler(object sender, iCardR2ErrorEventArgs e);

        public delegate void TagBeaconEventHandler(object sender, TagBeaconEventArgs e);
    }
}

