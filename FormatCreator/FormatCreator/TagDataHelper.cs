using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using LFI.RFID.Format;
using System.Data;

namespace LFI.RFID.Editor
{
    internal class TagDataHelper
    {
        static public DataSet CreateDataSet(FormatDef _formatDef, TagData _tagData)
        {
            // Create the data set
            DataSet dataSet = new DataSet("TagData");
            DataTable headerTable = CreateDataTable(HeaderTableName, _formatDef.HeaderRowDef);
            DataTable dataTable = CreateDataTable(DataRowTableName, _formatDef.DataRowDef);
            dataSet.Tables.Add(headerTable);
            dataSet.Tables.Add(dataTable);

            // Populate the data
            AddDataRow(headerTable, _formatDef.HeaderRowDef, _tagData.HeaderRow, false);
            foreach (TagDataRow dataRow in _tagData.DataRows)
                AddDataRow(dataTable, _formatDef.DataRowDef, dataRow, false);

            return dataSet;
        }

        static private DataTable CreateDataTable(string _tableName, DataRowDef _rowDef)
        {
            DataTable table = new DataTable(_tableName);

            table.Columns.Add(LockedColumnName, typeof(bool));
            table.Columns[LockedColumnName].DefaultValue = false;

            foreach (DataElementDef elemDef in _rowDef.ElementDefs)
            {
                Type columnType = GetSystemTypeFromFormatDataType(elemDef.DataType);
                DataColumn column = new DataColumn(elemDef.Name, columnType);
                column.AllowDBNull = (elemDef.Required == false);
                table.Columns.Add(column);
            }

            return table;
        }

        static public TagData ExtractFromDataSet(DataSet _dataSet, FormatDef _formatDef)
        {
            TagData tagData = new TagData();
            tagData.FormatID = _formatDef.ID;

            DataTable headerTable = _dataSet.Tables[HeaderTableName];            
            List<TagDataRow> dataRows = ExtractDataRow(headerTable, _formatDef.HeaderRowDef, 1);
            tagData.HeaderRow = dataRows[0];

            DataTable dataTable = _dataSet.Tables[DataRowTableName];            
            tagData.DataRows = ExtractDataRow(dataTable, _formatDef.DataRowDef, _formatDef.MaxDataRows);
            return tagData;
        }

        static public Dictionary<string, Dictionary<string, string>> CreatePickLists(DataRowDef _rowDef)
        {
            Dictionary<string, Dictionary<string, string>> pickLists = new Dictionary<string, Dictionary<string, string>>();

            foreach (DataElementDef elemDef in _rowDef.ElementDefs)
            {                
                if ((elemDef.DataType == DataType.PickList) || (elemDef.DataType == DataType.PickListUnicode))
                {
                    Dictionary<string, string> pickList = new Dictionary<string, string>();
                    string[] values = elemDef.Constraints.Split('|');
                    foreach (string value in values)
                        pickList.Add(value, value);
                    
                    pickLists.Add(elemDef.Name, pickList);
                }
                else if (elemDef.DataType == DataType.PickListKeyValue)
                {
                    Dictionary<string, string> pickList = new Dictionary<string, string>();
                    string[] values = elemDef.Constraints.Split('|');
                    for (int i = 0; i < values.Length; i += 2)
                        pickList.Add(values[i], values[i + 1]);

                    pickLists.Add(elemDef.Name, pickList);
                }
            }

            return pickLists;
        }

        static public string HeaderTableName = "header";
        static public string DataRowTableName = "data";
        static private string LockedColumnName = "LockOnDevice";

        private static bool IsDateType(DataType dataType)
        {
            if (dataType == DataType.DateOnly || dataType == DataType.DateTime || dataType == DataType.TimeOnly)
                return true;
            return false;
        }

        static public void AddDataRow(DataTable _table, DataRowDef _rowDef, TagDataRow _dataRow, bool insert)
        {
            object[] values = new object[_rowDef.ElementDefs.Count + 1];

            values[0] = _dataRow.IsLocked;

            int colIndex = 1;
            foreach (DataElementDef elemDef in _rowDef.ElementDefs)
            {
                string columnName = elemDef.Name;

                string valueAsText = string.Empty;
                if (_dataRow.Values.ContainsKey(columnName))
                    valueAsText = _dataRow.Values[columnName];

                if (string.IsNullOrEmpty(valueAsText))
                {
                    if (string.IsNullOrEmpty(valueAsText))
                        values[colIndex] = (!elemDef.Required) ? DBNull.Value : GetDefaultForDataType(elemDef.DataType);          
                }
                else
                {
                    values[colIndex] = ConvertValueTextToObject(valueAsText, elemDef.DataType);
                }

                colIndex++;
            }

            //_table.Rows.Add(values);

            DataRow newRow = _table.NewRow();          
            for (int index = 0; index < values.Length; index++)
            {                
                newRow[index] = values[index];
            }
            if (insert)
                _table.Rows.InsertAt(newRow, 0);
            else
                _table.Rows.Add(newRow);
        } 

        static private List<TagDataRow> ExtractDataRow(DataTable _table, DataRowDef _rowDef, int maxRows)
        {
            List<TagDataRow> rows = new List<TagDataRow>();

            for (int rowIndex = 0; rowIndex < _table.Rows.Count; rowIndex++)
            {
                DataRow tableDataRow = _table.Rows[rowIndex];
                TagDataRow tagDataRow = new TagDataRow();

                tagDataRow.IsLocked = (bool)tableDataRow[LockedColumnName];

                foreach (DataElementDef elemDef in _rowDef.ElementDefs)
                {
                    object value = tableDataRow[elemDef.Name];
                    if (value != null)
                        tagDataRow.Values.Add(elemDef.Name, value.ToString());
                }

                rows.Add(tagDataRow);

                if ((maxRows > 0) && (rowIndex >= (maxRows - 1)))
                    break;
            }

            return rows;
        }



        static public DataTable CreateHeaderTable(DataRowDef _rowDef)
        {
            DataTable table = new DataTable(HeaderTableName);

            table.Columns.Add(LockedColumnName, typeof(bool));
            table.Columns[LockedColumnName].DefaultValue = false;

            foreach (DataElementDef elemDef in _rowDef.ElementDefs)
            {
                Type columnType = GetSystemTypeFromFormatDataType(elemDef.DataType);
                DataColumn column = new DataColumn(elemDef.Name, columnType);
                column.AllowDBNull = (elemDef.Required == false);
                table.Columns.Add(column);
            }

            return table;
        }

        static private Type GetSystemTypeFromFormatDataType(DataType _dataType)
        {
            switch (_dataType)
            {
                case DataType.Bool: return typeof(bool);
                case DataType.Float: return typeof(float);
                case DataType.Double: return typeof(double);
                case DataType.Int16: return typeof(short);
                case DataType.Int32: return typeof(int);
                case DataType.DateOnly: return typeof(DateTime);
                case DataType.DateTime: return typeof(DateTime);
                case DataType.TimeOnly: return typeof(DateTime);
                default: return typeof(string);
            }
        }

        static private object ConvertValueTextToObject(string _valueAsText, DataType _dataType)
        {
            switch (_dataType)
            {
                case DataType.Bool: return bool.Parse(_valueAsText);
                case DataType.Float: return float.Parse(_valueAsText);
                case DataType.Double: return double.Parse(_valueAsText);
                case DataType.Int16: return short.Parse(_valueAsText);
                case DataType.Int32: return int.Parse(_valueAsText);
                case DataType.DateOnly: return DateTime.Parse(_valueAsText);
                case DataType.DateTime: return DateTime.Parse(_valueAsText);
                case DataType.TimeOnly: return DateTime.Parse(_valueAsText);
                default: return _valueAsText;
            }
        }

        static private object GetDefaultForDataType(DataType _dataType)
        {
            switch (_dataType)
            {
                case DataType.Bool: return false;
                case DataType.Float: return 0f;
                case DataType.Double: return 0.0;
                case DataType.Int16: return 0;
                case DataType.Int32: return 0;
                case DataType.DateOnly: return DateTime.Today;
                case DataType.DateTime: return DateTime.Now;
                case DataType.TimeOnly: return DateTime.Now;
                default: return string.Empty;
            }
        }
    }
}
