namespace IDENTEC.Readers
{
    using System;

    public class iCardProductionInformation
    {
        internal int nProductionNumber;
        internal int nWeek;
        internal int nYear;

        public int ProductionNumber
        {
            get
            {
                return this.nProductionNumber;
            }
        }

        public int Week
        {
            get
            {
                return this.nWeek;
            }
        }

        public int Year
        {
            get
            {
                return this.nYear;
            }
        }
    }
}

