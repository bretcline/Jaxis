namespace IDENTEC.Readers.BeaconReaders
{
    using IDENTEC.Tags.BeaconTags;
    using System;

    public class TagBeaconEventArgs : EventArgs
    {
        internal iB2Tag m_tag;

        public iB2Tag tag
        {
            get
            {
                return this.m_tag;
            }
        }
    }
}

