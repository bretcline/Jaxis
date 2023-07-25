namespace IDENTEC.Tags
{
    using System;

    public class LoopData
    {
        private LoopPositionInfo m_NewerPosition;
        private int m_NewerRSSI;
        private LoopPositionInfo m_OlderPosition;

        public LoopData()
        {
        }

        public LoopData(LoopData l)
        {
            this.m_NewerPosition = new LoopPositionInfo(l.m_NewerPosition);
            this.m_NewerRSSI = l.m_NewerRSSI;
            this.m_OlderPosition = new LoopPositionInfo(l.m_OlderPosition);
        }

        internal static LoopData CreateLoopData(uint tagID, byte[] byBuff, DateTime dtTagContact)
        {
            LoopData data = null;
            int startIndex = 0;
            if ((byBuff != null) && (byBuff.Length >= 9))
            {
                DateTime now;
                bool flag = false;
                data = new LoopData();
                int loopID = 0;
                loopID = BitConverter.ToUInt16(byBuff, startIndex);
                startIndex += 2;
                int num2 = BitConverter.ToUInt16(byBuff, startIndex);
                bool validTime = num2 != 0xffff;
                try
                {
                    if (flag)
                    {
                        now = dtTagContact.AddSeconds((double) (num2 * -1));
                    }
                    else
                    {
                        now = dtTagContact.AddMilliseconds((double) (num2 * -100));
                    }
                }
                catch (Exception)
                {
                    now = DateTime.Now;
                }
                data.OlderPosition = new LoopPositionInfo(loopID, now, validTime);
                if (byBuff.Length == 10)
                {
                    startIndex++;
                }
                startIndex += 2;
                int num3 = BitConverter.ToUInt16(byBuff, startIndex);
                startIndex += 2;
                num2 = BitConverter.ToUInt16(byBuff, startIndex);
                try
                {
                    if (flag)
                    {
                        now = dtTagContact.AddSeconds((double) (num2 * -1));
                    }
                    else
                    {
                        now = dtTagContact.AddMilliseconds((double) (num2 * -100));
                    }
                }
                catch (Exception)
                {
                    now = DateTime.Now;
                }
                validTime = num2 != 0xffff;
                data.NewerPosition = new LoopPositionInfo(num3, now, validTime);
                startIndex += 2;
                data.NewerRSSI = byBuff[startIndex];
                if (0xff == data.NewerRSSI)
                {
                    data.NewerPosition = new LoopPositionInfo();
                }
            }
            return data;
        }

        public LoopPositionInfo NewerPosition
        {
            get
            {
                if (this.m_NewerPosition == null)
                {
                    this.m_NewerPosition = new LoopPositionInfo();
                }
                return this.m_NewerPosition;
            }
            set
            {
                this.m_NewerPosition = value;
            }
        }

        public int NewerRSSI
        {
            get
            {
                return this.m_NewerRSSI;
            }
            set
            {
                this.m_NewerRSSI = value;
            }
        }

        public LoopPositionInfo OlderPosition
        {
            get
            {
                if (this.m_OlderPosition == null)
                {
                    this.m_OlderPosition = new LoopPositionInfo();
                }
                return this.m_OlderPosition;
            }
            set
            {
                this.m_OlderPosition = value;
            }
        }
    }
}

