using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LFI.Sync.Shared
{
    public class TableMap
    {
        public static string OrderBy(string columName)
        {
            return string.Format("ORDER BY {0}", columName);
        }

        public static string OrderBy(string[] columNames)
        {
            StringBuilder orderBy = new StringBuilder();
            orderBy.AppendFormat("ORDER BY {0}", columNames[0]);

            bool skip = false;
            foreach (string columnName in columNames)
            {
                if (!skip)
                {
                    skip = true;
                    continue;
                }

                orderBy.AppendFormat(", {0}", columnName);
            }

            return orderBy.ToString();
        }

        #region Nested type: SQL

        public class SQL
        {
            public const string AND = " AND ";
            public const string OR = " OR ";
            public const string ORDERBY = " ORDER BY ";
            public const string WHERE = " WHERE ";
        }

        #endregion
    }
}