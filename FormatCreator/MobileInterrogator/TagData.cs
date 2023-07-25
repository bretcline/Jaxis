using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace MobileInterrogator
{
    public enum ItemDataType : int
    {
        Text = 0,
        Number = 1,
        Date = 2,
        PickList = 3
    }

    public class DataRow
    {
        public string Name {get; protected set;}
        public ItemDataType DataType {get; protected set;}
        public bool Required{get; protected set;}
        public string Constraints {get; protected set;}
        public string Value {get;set;}

        public DataRow( string _Name, ItemDataType _DataType, bool _Required, string _Constraints, string _Value )
        {
            Name = _Name;
            DataType = _DataType;
            Required = _Required;
            Constraints = _Constraints;
            Value = _Value;
        }
    }


    public class TagData
    {
        public DataRow Header = null;
        public List<DataRow> Data = new List<DataRow>( );
    }
}
