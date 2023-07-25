namespace IDENTEC.Tags
{
    using System;

    public abstract class ResponseTag : Tag
    {
        internal byte m_byMode;
        internal int m_nVersion;

        protected ResponseTag()
        {
        }

        protected ResponseTag(ResponseTag t) : base(t)
        {
            this.m_nVersion = t.m_nVersion;
        }

        [CLSCompliant(false)]
        protected ResponseTag(uint id) : base(id)
        {
        }

        internal ResponseTag(uint id, DateTime dt, int signal) : base(id, dt, signal)
        {
        }

        internal virtual int WaitForResponse(bool bWakeUp)
        {
            return 0;
        }

        public virtual int DataCapacity
        {
            get
            {
                return 0;
            }
        }

        public virtual int MinDataWriteAddress
        {
            get
            {
                return 0;
            }
        }

        public int Version
        {
            get
            {
                return this.m_nVersion;
            }
        }

        internal enum TAG_TYPE
        {
            ID_INTERNATIONAL = 1,
            ID_NORTH_AMERICA = 3,
            IQ = 0
        }
    }
}

