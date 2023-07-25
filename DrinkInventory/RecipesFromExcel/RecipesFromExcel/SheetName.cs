using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RecipesFromExcel
{
    public class SheetName
    {
        public string Sheet
        {
            get
            {
                return RawName == null ? null : Regex.Replace( RawName, @"^[']?(.*?)[$]?[']?$", "$1" );
            }
        }
        public string RawName { get; set; }
        public SheetName( string _sheet )
        {
            RawName = _sheet;
        }
    }
}
