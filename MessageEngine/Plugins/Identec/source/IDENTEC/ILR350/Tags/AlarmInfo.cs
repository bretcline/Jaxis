namespace IDENTEC.ILR350.Tags
{
    using System;

    public class AlarmInfo
    {
        private AlarmFlag m_ActiveAlarm;
        private AlarmFlag m_TriggeredAlarm;

        protected AlarmInfo()
        {
        }

        internal AlarmInfo(AlarmFlag ActiveAlarm, AlarmFlag TriggeredAlarm)
        {
            this.m_ActiveAlarm = ActiveAlarm;
            this.m_TriggeredAlarm = TriggeredAlarm;
        }

        public override string ToString()
        {
            return string.Format("Active {0}: Triggered: {1}", this.ActiveAlarm, this.TriggeredAlarm);
        }

        public AlarmFlag ActiveAlarm
        {
            get
            {
                return this.m_ActiveAlarm;
            }
        }

        public AlarmFlag TriggeredAlarm
        {
            get
            {
                return this.m_TriggeredAlarm;
            }
        }
    }
}

