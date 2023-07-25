using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jaxis.Utilities.Database;

namespace MicrosDatabaseConnect
{
    class Program
    {
        static void Main(string[] args)
        {
            var sql = new SqlTool(DBTypes.ODBC);

            sql.Open("Dsn=sqlMRSBRMICROS01; Uid=custom; Pwd=custom;");

            var reader = sql.ExecuteReader("SELECT * FROM sysobjects");
            var value = reader.Read();
        }
    }
}
