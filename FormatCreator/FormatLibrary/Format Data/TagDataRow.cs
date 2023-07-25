using System;
using System.Collections.Generic;

namespace LFI.RFID.Format
{
    public class TagDataRow
    {
        public bool IsLocked = false;
        public Dictionary<string, string> Values = new Dictionary<string, string>();
    }
}
