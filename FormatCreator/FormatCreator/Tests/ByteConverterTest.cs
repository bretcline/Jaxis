using System;
using System.Collections.Generic;

namespace LFI.RFID.Format
{
    public class ByteConverterTest
    {
        public bool Test1()
        {
            FormatDef formatDef = CreateFormatDef();
            TagData tagData = CreateTagData();
            ByteConverter converter = new ByteConverter();

            Byte[] bytes = converter.ToByteArray(formatDef, tagData, 100000);
            TagData tagData2 = converter.FromByteArray(formatDef, bytes);

            if (tagData.FormatID != tagData2.FormatID)
                return false;

            if (tagData.HeaderRow.IsLocked != tagData2.HeaderRow.IsLocked)
                return false;

            foreach (string key in tagData.HeaderRow.Values.Keys)
            {
                if (string.Compare(tagData.HeaderRow.Values[key], tagData2.HeaderRow.Values[key]) != 0)
                    return false;
            }

            for (int rowIndex = 0; rowIndex < tagData.DataRows.Count; rowIndex++)
            {
                TagDataRow row1 = tagData.DataRows[rowIndex];
                TagDataRow row2 = tagData2.DataRows[rowIndex];

                if (row1.IsLocked != row2.IsLocked)
                    return false;

                foreach (string key in row1.Values.Keys)
                {
                    if (string.Compare(row1.Values[key], row2.Values[key]) != 0)
                        return false;
                }
            }

            return true;
        }

        private FormatDef CreateFormatDef()
        {
            FormatDef formatDef = new FormatDef();
            formatDef.ID = new Guid("00000000-0000-0000-0000-000000000001");
            formatDef.Name = "Test1";
            formatDef.Description = "Test Description";
            formatDef.MaxDataRows = 5;
            
            formatDef.HeaderRowDef.ElementDefs.Add(new DataElementDef("WellID", DataType.TextUnicode, true, string.Empty));
            formatDef.HeaderRowDef.ElementDefs.Add(new DataElementDef("OpCo", DataType.Text, true, string.Empty));

            formatDef.DataRowDef.ElementDefs.Add(new DataElementDef("InspectionDate", DataType.DateTime, true, string.Empty));
            formatDef.DataRowDef.ElementDefs.Add(new DataElementDef("Inspector", DataType.TextUnicode, true, string.Empty));
            formatDef.DataRowDef.ElementDefs.Add(new DataElementDef("QualityIndicator", DataType.PickListUnicode, true, "Good|Bad|Ugly"));
            formatDef.DataRowDef.ElementDefs.Add(new DataElementDef("HP", DataType.Int16, true, string.Empty));
            formatDef.DataRowDef.ElementDefs.Add(new DataElementDef("RPM", DataType.Int32, true, string.Empty));
            formatDef.DataRowDef.ElementDefs.Add(new DataElementDef("Running", DataType.Bool, true, string.Empty));
            formatDef.DataRowDef.ElementDefs.Add(new DataElementDef("FluidLevel", DataType.Double, true, string.Empty));
            formatDef.DataRowDef.ElementDefs.Add(new DataElementDef("Pressure", DataType.Float, true, string.Empty));

            return formatDef;
        }

        private TagData CreateTagData()
        {
            TagData data = new TagData();
            data.FormatID = new Guid("00000000-0000-0000-0000-000000000001");
            data.HeaderRow = new TagDataRow();
            data.HeaderRow.IsLocked = true;
            data.HeaderRow.Values.Add("WellID", "Well-A14");
            data.HeaderRow.Values.Add("OpCo", "OXY-NA");

            TagDataRow dataRow1 = new TagDataRow();
            dataRow1.IsLocked = true;
            dataRow1.Values.Add("InspectionDate", "1/1/2009 2:00:00 PM");
            dataRow1.Values.Add("Inspector", "Bob");
            dataRow1.Values.Add("QualityIndicator", "Good");
            dataRow1.Values.Add("HP", "300");
            dataRow1.Values.Add("RPM", "1200");
            dataRow1.Values.Add("Running", "True");
            dataRow1.Values.Add("FluidLevel", "4.325");
            dataRow1.Values.Add("Pressure", "6.25");
            data.DataRows.Add(dataRow1);

            TagDataRow dataRow2 = new TagDataRow();
            dataRow2.IsLocked = false;
            dataRow2.Values.Add("InspectionDate", "9/3/2009 4:30:00 PM");
            dataRow2.Values.Add("Inspector", "Fred");
            dataRow2.Values.Add("QualityIndicator", "Bad");
            dataRow2.Values.Add("HP", "400");
            dataRow2.Values.Add("RPM", "1000");
            dataRow2.Values.Add("Running", "False");
            dataRow2.Values.Add("FluidLevel", "-2.3");
            dataRow2.Values.Add("Pressure", "5.25");
            data.DataRows.Add(dataRow2);

            return data;
        }
    }
}
