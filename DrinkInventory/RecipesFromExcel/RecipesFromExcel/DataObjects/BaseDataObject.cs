using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecipesFromExcel
{
    public class BaseDataObject
    {
        public Func<object, string> getString = v => v == DBNull.Value ? String.Empty : v.ToString( );
        public Func<object, Decimal> getDecimal = v => v == DBNull.Value ? 0.0M : Decimal.Parse( v.ToString( ) );
    }
}
