using System;
using System.Configuration;
using System.IO;
using System.Linq;
using Jaxis.Utilities.Database;

namespace Migrate
{
    public static class Migrator
    {
        public static void Migrate(string _scriptPath)
        {
            var connection = ConfigurationManager.ConnectionStrings["DrinkReporting"].ConnectionString;

            using (var sql = new SqlTool(connection))
            {
                var version = 0;
                sql.ExecuteScalar("SELECT ISNULL(MAX([Version]), 0) AS [Version] FROM [Schema]", ref version);

                var files = Directory.GetFiles(_scriptPath, "*.*.sql");
                var fileNames = from fileName in files
                                 let parts = Path.GetFileName(fileName).Split('.')
                                 where parts.Length > 0
                                 let sequenceNumber = int.Parse(parts[0])
                                 where sequenceNumber > version
                                 orderby sequenceNumber
                                 select fileName;

                foreach (var fileName in fileNames)
                {
                    Console.WriteLine("Running migration {0}", Path.GetFileName(fileName));

                    var parts = Path.GetFileName(fileName).Split('.');
                    var sequenceNumber = int.Parse(parts[0]);

                    try
                    {
                        var allText = File.ReadAllText(fileName);

                        var commands = allText.Split(new[] {"GO\r\n", "go\r\n"}, Int32.MaxValue, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var command in commands.Where(_c => !string.IsNullOrWhiteSpace(_c)))
                        {
                            sql.Execute(command);
                        }
                        
                        var updateVersionCommand = string.Format("INSERT [Schema] (Version) VALUES ({0})", sequenceNumber);
                        sql.Execute(updateVersionCommand);
                    }
                    catch (Exception ex)
                    {
                        var message = string.Format("Migration failed!  File name = '{0}'", Path.GetFileName(fileName));
                        throw new Exception(message, ex);
                    }
                }
            }
        }
    }
}
