namespace IDENTEC.PositionMarker
{
    using System;
    using System.Collections;

    public class SynchSlots
    {
        private BitArray _baSlots;
        private int _uiSlots;

        public SynchSlots(BitArray slots)
        {
            this._baSlots = slots;
        }

        public SynchSlots(int slots)
        {
            this._uiSlots = slots;
        }

        public BitArray ba
        {
            get
            {
                int num = 0;
                for (num = 0; num < 12; num++)
                {
                    this._baSlots[num] = ((this._uiSlots >> num) & 1) == 1;
                }
                return this._baSlots;
            }
            set
            {
                int num = 0;
                if (value.Length != 12)
                {
                    throw new Exception("Size of ByteArray for Slots not equal to 12");
                }
                this._baSlots = value;
                this._uiSlots = 0;
                for (num = 0; num < value.Length; num++)
                {
                    if (this._baSlots[num])
                    {
                        this._uiSlots |= ((int) 1) << num;
                    }
                }
            }
        }

        public int ui
        {
            get
            {
                return this._uiSlots;
            }
            set
            {
                if (value > 0xfff)
                {
                    throw new Exception("uint value for Slots higher than 0xFFF");
                }
                this._uiSlots = value;
            }
        }
    }
}

