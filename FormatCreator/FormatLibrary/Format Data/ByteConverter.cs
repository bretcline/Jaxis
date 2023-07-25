using System;
using System.Collections.Generic;

namespace LFI.RFID.Format
{
    public class ByteConverter
    {
        private int currentEncodingVersion = 1;

        #region Initialization

        public ByteConverter()
        {
            converters = new Dictionary<DataType, IDataTypeConverter>();
            converters.Add(DataType.Text, new TextConverter(false));
            converters.Add(DataType.TextUnicode, new TextConverter(true));
            converters.Add(DataType.DateOnly, new DateTimeConverter(true, false));
            converters.Add(DataType.TimeOnly, new DateTimeConverter(false, true));
            converters.Add(DataType.DateTime, new DateTimeConverter(true, true));
            converters.Add(DataType.PickList, new TextConverter(false));
            converters.Add(DataType.PickListUnicode, new TextConverter(true));
            converters.Add(DataType.PickListKeyValue, new Int16Converter());
            converters.Add(DataType.Bool, new BoolConverter());
            converters.Add(DataType.Double, new DoubleConverter());
            converters.Add(DataType.Float, new FloatConverter());
            converters.Add(DataType.Int16, new Int16Converter());
            converters.Add(DataType.Int32, new Int32Converter());
            converters.Add(DataType.Guid, new GuidConverter());
        }
        private Dictionary<DataType, IDataTypeConverter> converters = null;
        
        #endregion

        #region Convert to bytes

        public byte[] ToByteArray(FormatDef formatDef, TagData data, int maxBytes)
        {
            List<Byte> byteArray = new List<byte>();

            // Stuff in the current binary formatting version
            AddToByteArray(converters[DataType.Int16], currentEncodingVersion.ToString(), ref byteArray);

            // Stuff in format id
            AddToByteArray(converters[DataType.Guid], formatDef.ID.ToString(), ref byteArray);

            // Stuff in header
            AddRowToByteArray(formatDef.HeaderRowDef, ref byteArray, data.HeaderRow);

            // Calculate the maximun number of rows we can write out
            int bytesRemaining = maxBytes - byteArray.Count;
            int numRows = CalculateNumberOfDataRows(bytesRemaining, formatDef, data);
            AddToByteArray(converters[DataType.Int16], numRows.ToString(), ref byteArray);

            for (int rowIndex = 0; rowIndex < numRows; rowIndex++)
                AddRowToByteArray(formatDef.DataRowDef, ref byteArray, data.DataRows[rowIndex]);

            return byteArray.ToArray();
        }

        private int CalculateNumberOfDataRows(int bytesRemaining, FormatDef formatDef, TagData data)
        {
            int numRows = 0;

            foreach (TagDataRow dataRow in data.DataRows)
            {
                int rowSize = CalculateDataRowSize(formatDef.DataRowDef, dataRow);
                if (rowSize > bytesRemaining)
                    break;
                else
                {
                    bytesRemaining -= rowSize;
                    numRows++;
                } 
            }

            if (formatDef.MaxDataRows <= 0)
                return numRows;
            else
                return Math.Min(numRows, formatDef.MaxDataRows);
        }

        private int CalculateDataRowSize(DataRowDef rowDef, TagDataRow row)
        {
            int byteCount = 0;

            foreach (DataElementDef elemDef in rowDef.ElementDefs)
            {
                string value = row.Values[elemDef.Name];
                byteCount += converters[elemDef.DataType].GetSize(value);
            }

            return byteCount;
        }

        private void AddToByteArray(IDataTypeConverter converter, string value, ref List<Byte> byteArray)
        {
            byte[] bytes = converter.ToByteArray(value);
            foreach (byte byteValue in bytes)
                byteArray.Add(byteValue);
        }

        private void AddRowToByteArray(DataRowDef rowDef, ref List<Byte> byteArray, TagDataRow row)
        {
            AddToByteArray(converters[DataType.Bool], row.IsLocked.ToString(), ref byteArray);
            foreach (DataElementDef elemDef in rowDef.ElementDefs)
            {
                string valueAsText = row.Values[elemDef.Name];
                AddToByteArray(converters[elemDef.DataType], valueAsText, ref byteArray);
            }
        }

        #endregion

        #region Convert from bytes

        public Guid GetFormatID( byte[] byteArray )
        {
            int startIndex = 0;

            // Read the binary formatting version
            string bitVersionAsText = converters[DataType.Int16].FromByteArray( byteArray, ref startIndex );
            int bitVersion = int.Parse( bitVersionAsText );

            // Read format ID
            string formatIDAsText = converters[DataType.Guid].FromByteArray( byteArray, ref startIndex );
            return new Guid( formatIDAsText );
        }

        public TagData FromByteArray(FormatDef formatDef, byte[] byteArray)
        {
            int startIndex = 0;
            TagData data = new TagData();

            // Read the binary formatting version
            string bitVersionAsText = converters[DataType.Int16].FromByteArray(byteArray, ref startIndex);
            int bitVersion = int.Parse(bitVersionAsText);
            // TODO: Throw exception if formatting is newer than this version handles
            if (currentEncodingVersion < bitVersion)
                return null;
            else if (currentEncodingVersion > bitVersion)
                return ReadFromOldVersion(bitVersion, formatDef, byteArray, startIndex);            

            // Read format ID
            string formatIDAsText = converters[DataType.Guid].FromByteArray(byteArray, ref startIndex);
            data.FormatID = new Guid(formatIDAsText);

            // Read Header Row
            data.HeaderRow = ReadRowFromByteArray(formatDef.HeaderRowDef, byteArray, ref startIndex);

            // Read the number of data rows
            string numRowsAsText = converters[DataType.Int16].FromByteArray(byteArray, ref startIndex);
            int numRows = int.Parse(numRowsAsText);
            if (numRows == 0)
                data.DataRows = new List<TagDataRow>( );
            else
                data.DataRows = new List<TagDataRow>(numRows);

            for (int rowIndex = 0; rowIndex < numRows; rowIndex++)
            {
                TagDataRow row = ReadRowFromByteArray(formatDef.DataRowDef, byteArray, ref startIndex);
                data.DataRows.Add(row);
            }

            return data;
        }

        private TagDataRow ReadRowFromByteArray(DataRowDef rowDef, byte[] byteArray, ref int startIndex)
        {
            TagDataRow dataRow = new TagDataRow();

            string value = converters[DataType.Bool].FromByteArray(byteArray, ref startIndex);
            dataRow.IsLocked = bool.Parse(value);

            foreach (DataElementDef elemDef in rowDef.ElementDefs)
            {
                value = converters[elemDef.DataType].FromByteArray(byteArray, ref startIndex);
                dataRow.Values.Add(elemDef.Name, value);
            }
            return dataRow;
        }

        #endregion

        private TagData ReadFromOldVersion(int version, FormatDef formatDef, byte[] byteArray, int startIndex)
        {
            // TODO: Fill out as needed when version is rev'd in the future
            return null;
        }
    }
}
