using System;
using System.Xml.Serialization;

namespace LFI.RFID.Format
{
    public class FormatDef
    {
        public FormatDef()
        {
            ID = Guid.NewGuid();
            Name = "New Format";
            Description = string.Empty;
            MaxDataRows = 1;
            HeaderRowDef = new DataRowDef();
            DataRowDef = new DataRowDef();
        }

        public FormatDef(FormatDef src)
        {
            ID = Guid.NewGuid();
            Name = src.Name + " (copy)";
            Description = src.Description;
            MaxDataRows = src.MaxDataRows;
            HeaderRowDef = new DataRowDef(src.HeaderRowDef);
            DataRowDef = new DataRowDef(src.DataRowDef);
        }

        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }       
        public DataRowDef HeaderRowDef { get; set; }
        public DataRowDef DataRowDef { get; set; }
        public int MaxDataRows { get; set; }

        public void AcceptChanges()
        {
            HeaderRowDef.AcceptChanges();
            DataRowDef.AcceptChanges();
        }

        public void RevertChanges()
        {
            HeaderRowDef.RevertChanges();
            DataRowDef.RevertChanges();
        }
    }
}
