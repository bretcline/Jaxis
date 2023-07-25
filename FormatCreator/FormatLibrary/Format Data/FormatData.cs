using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LFI.RFID.Format
{
    public class FormatData
    {
        public string FormatID { get; set; }
        public DataRow HeaderRow { get; set; }
        public List<DataRow> DataRows { get; set; }
    }
}
