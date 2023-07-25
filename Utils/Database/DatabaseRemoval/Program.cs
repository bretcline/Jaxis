using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.Utilities.Database;

namespace DatabaseRemoval
{
    class Program
    {
        static void Main(string[] args)
        {
            string ConnectionString = "Server=.;Database=master;Trusted_Connection=True;";
            using (SqlTool Conn = new SqlTool(ConnectionString))
            {
                string Command =
                    "IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'BevMetMobile') DROP DATABASE [BevMetMobile]";
                Conn.Execute(Command);

                Command =
                    "IF  EXISTS (SELECT * FROM sys.server_principals WHERE name = N'PourMonitor') DROP LOGIN [PourMonitor]";
                Conn.Execute(Command);
            }

        }
    }
}
