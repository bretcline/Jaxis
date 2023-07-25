namespace IDENTEC.Tags
{
    using System;

    public class LoopPositionInfo
    {
        private static readonly TimeSpan LoopTimeExpires = new TimeSpan(0, 0, 0, 0xffff, 0);
        private bool m_bValidPositionTime;
        private int m_LoopID;
        private DateTime m_PositionTime;

        public LoopPositionInfo()
        {
        }

        public LoopPositionInfo(LoopPositionInfo p)
        {
            this.m_LoopID = p.m_LoopID;
            this.m_PositionTime = p.m_PositionTime;
            this.m_bValidPositionTime = p.m_bValidPositionTime;
        }

        public LoopPositionInfo(int loopID, DateTime dt, bool validTime)
        {
            this.m_LoopID = loopID;
            this.m_bValidPositionTime = validTime;
            if (validTime)
            {
                this.m_PositionTime = dt;
            }
            else
            {
                this.m_PositionTime = DateTime.MinValue;
            }
        }

        public bool IsPositionValid
        {
            get
            {
                return this.m_bValidPositionTime;
            }
        }

        public int LoopID
        {
            get
            {
                return this.m_LoopID;
            }
            set
            {
                this.m_LoopID = value;
            }
        }

        public DateTime PositionTime
        {
            get
            {
                return this.m_PositionTime;
            }
            set
            {
                this.m_PositionTime = value;
            }
        }
    }
}

