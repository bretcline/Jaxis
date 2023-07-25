using System;
using System.Collections.Generic;
using LFI.RFID.Editor;

namespace LFI.RFID.Format
{
    public class TagDataConverterTest
    {
        public bool Test1()
        {
            TagData tagData = CreateTagData();
            string xml = TagDataConverter.TagDataToString(tagData);
            TagData tagData2 = TagDataConverter.TagDataFromString(xml);

            if (tagData.FormatID != tagData2.FormatID)
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

                foreach (string key in row1.Values.Keys)
                {
                    if (string.Compare(row1.Values[key], row2.Values[key]) != 0)
                        return false;
                }
            }

            return true;
        }

        private TagData CreateTagData()
        {
            TagData data = new TagData();
            data.FormatID = new Guid("00000000-0000-0000-0000-000000000001");
            data.HeaderRow = new TagDataRow();
            data.HeaderRow.Values.Add("WellID", "Well-A14");
            data.HeaderRow.Values.Add("OpCo", "OXY-NA");

            TagDataRow dataRow1 = new TagDataRow();
            dataRow1.Values.Add("InspectionDate", "1/1/2009 14:00");
            dataRow1.Values.Add("Inspector", "Bob");
            dataRow1.Values.Add("QualityIndicator", "Good");
            dataRow1.Values.Add("HP", "300");
            dataRow1.Values.Add("RPM", "1200");
            dataRow1.Values.Add("Running", "true");
            dataRow1.Values.Add("FluidLevel", "4.325");
            dataRow1.Values.Add("Pressure", "6.25");
            data.DataRows.Add(dataRow1);

            TagDataRow dataRow2 = new TagDataRow();
            dataRow2.Values.Add("InspectionDate", "9/3/2009 16:30");
            dataRow2.Values.Add("Inspector", "Fred");
            dataRow2.Values.Add("QualityIndicator", "Bad");
            dataRow2.Values.Add("HP", "400");
            dataRow2.Values.Add("RPM", "1000");
            dataRow2.Values.Add("Running", "false");
            dataRow2.Values.Add("FluidLevel", "-2.3");
            dataRow2.Values.Add("Pressure", "5.25");
            data.DataRows.Add(dataRow2);

            return data;
        }
    }
}
