using System;
using System.Windows.Forms;
using LFI.RFID.Format;

namespace MobileInterrogator
{
    public static class ValueEditorFactory
    {
        public static IValueEditor CreateValueEditor(DataType _dataType)
        {
            switch (_dataType)
            {
                case DataType.PickList:
                case DataType.PickListUnicode:
                case DataType.PickListKeyValue: 
                    return new ValueEditorPicklist();
                case DataType.Bool:
                    return new ValueEditorPicklist();
                case DataType.DateOnly:
                case DataType.TimeOnly:
                case DataType.DateTime:
                    return new ValueEditorDate();
                case DataType.Double:
                case DataType.Float:
                case DataType.Int16:
                case DataType.Int32:
                    return new ValueEditorNumber();
                default:
                    return new ValueEditorText();
            }
        }
    }
}
