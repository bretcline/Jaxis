using System;
using System.Collections.Generic;

namespace LFI.RFID.Format
{
    public class TagData
    {
        public TagData() 
        {
            TagID = Guid.NewGuid( ).ToByteArray( );
            HeaderRow = new TagDataRow();
            DataRows = new List<TagDataRow>(); 
        }


        public byte[] TagID { get; set; }
        public Guid FormatID { get; set; }
        public TagDataRow HeaderRow { get; set; }
        public List<TagDataRow> DataRows { get; set; }
    }
}
