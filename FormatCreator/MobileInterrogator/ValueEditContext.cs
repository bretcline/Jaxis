using System;
using LFI.RFID.Format;

namespace MobileInterrogator
{
    public class ValueEditState
    {
        public void Init(DataElementDef _dataElementDef, string _initialValue)
        {
            DataElementDef = _dataElementDef;
            InitialValue = _initialValue;
            FinalValue = null;
        }

        public DataElementDef DataElementDef { get; set; }
        public string InitialValue { get; set; }
        public string FinalValue { get; set; }
    }
}