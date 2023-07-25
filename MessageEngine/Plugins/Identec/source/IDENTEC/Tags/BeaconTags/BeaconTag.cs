namespace IDENTEC.Tags.BeaconTags
{
    using IDENTEC.Tags;
    using System;

    public abstract class BeaconTag : Tag
    {
        public const int ID_EnEXDAT = 0x16d41dbf;
        public const int ID_EnEXTMP = 0x1741fabf;
        public const int ID_EniB2Ls = 0x16a9643f;
        public const int ID_EniQBLs = 0xe5d5e3f;
        public const int ID_StEXDAT = 0x16c4db80;
        public const int ID_StEXTMP = 0x17407420;
        public const int ID_StiB2Ls = 0x16a65700;
        public const int ID_StiQBLs = 0xe4e1c00;
        public const int ID_StSLOTS = 0x16b59940;

        protected BeaconTag()
        {
        }

        protected BeaconTag(BeaconTag t) : base(t)
        {
        }

        [CLSCompliant(false)]
        protected BeaconTag(uint id) : base(id)
        {
        }

        internal BeaconTag(uint id, DateTime dt, int signal) : base(id, dt, signal)
        {
        }
    }
}

