namespace IDENTEC.Readers
{
    using IDENTEC;
    using System;

    public abstract class iCard : Reader, IComparable
    {
        protected DeviceCode m_deviceCode;
        internal int m_nMajorVersion;
        internal int m_nMinorVersion;
        internal iCardProductionInformation m_prod;
        internal string m_strInformation = "N/A";
        internal string m_strSerialNumber = "N/A";

        protected iCard()
        {
        }

        public int CompareTo(object obj)
        {
            iCard card = obj as iCard;
            return this.m_strSerialNumber.CompareTo(card.m_strSerialNumber);
        }

        public abstract bool Connect(int port);
        internal void RegionCompatibilityCheck(Frequency f)
        {
            switch (f)
            {
                case Frequency.European:
                    if (Reader.CompatibleRegion.NorthAmericanOnly == base.m_WorkingRegion)
                    {
                        throw new RegionException("This reader is set to work only in North American (916.5MHz) frequency regions");
                    }
                    break;

                case Frequency.NorthAmerican:
                    if (base.m_WorkingRegion == Reader.CompatibleRegion.EuropeanOnly)
                    {
                        throw new RegionException("This reader is set to work only in European (868MHz) frequency regions");
                    }
                    break;

                default:
                    return;
            }
        }

        public virtual void TestCommunications()
        {
        }

        public virtual DeviceCode DeviceStatus
        {
            get
            {
                return this.m_deviceCode;
            }
        }

        public virtual string Information
        {
            get
            {
                return this.m_strInformation;
            }
        }

        public iCardProductionInformation ProductionInformation
        {
            get
            {
                return this.m_prod;
            }
        }

        public virtual Frequency Region
        {
            get
            {
                return base.m_Freq;
            }
            set
            {
                this.RegionCompatibilityCheck(value);
                base.m_Freq = value;
            }
        }

        public virtual string SerialNumber
        {
            get
            {
                return this.m_strSerialNumber;
            }
        }

        public enum DeviceCode
        {
            InvalidParameter = -322,
            NoAcknowledgementFromTag = -320,
            OK = 0,
            TagNoRead = -324,
            TagNoSessionSetup = -323,
            TagNoWrite = -327,
            TagPartialRead = -321,
            TagPartialWrite = -326,
            TagReadNoAcknowledge = -329,
            TagSignalTooWeak = -325,
            TagWriteNoAcknowledge = -328
        }
    }
}

