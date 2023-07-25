using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LFI.Sync.Shared
{
    public static class TableMapExtensions
    {
        private static readonly Dictionary<string, string> paramCache = new Dictionary<string, string>( );

        /// <summary>
        /// Converts a string from a table map into a parameter.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns></returns>
        public static string ToParam( this string columnName )
        {
            if( !paramCache.ContainsKey( columnName ) )
                paramCache.Add( columnName, string.Format( "@{0}", columnName ) );

            return paramCache[ columnName ];
        }
    }
}