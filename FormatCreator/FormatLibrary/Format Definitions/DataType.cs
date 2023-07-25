using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LFI.RFID.Format
{
    public enum DataType : int
    {
        Text = 0,
        TextUnicode = 1,
        
        DateOnly = 2,
        TimeOnly = 3,
        DateTime = 4,
        
        PickList = 5,
        PickListUnicode = 6,
        PickListKeyValue = 7,
       
        Bool = 8,
        
        Double = 9,
        Float = 10,
        Int16 = 11,
        Int32 = 12,

        Guid = 13
    }
}
