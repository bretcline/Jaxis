using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Serialization;
using Jaxis.Utilities.Database;

namespace Jaxis.Database.Scripts
{
    public class SQLCommand
    {
        public int DBVersion { get; set; }
        public int Order { get; set; }
        public int Type { get; set; }
        public string DatabaseName { get; set; }
        public string DBCommand { get; set; }
    }

    class ScriptInstaller
    {
        public string DatabaseName = "BeverageMonitor";

        static void Main( string[] args )
        {
            var databaseName = "BeverageMonitor";
            if( args.Length > 3 )
            {
                databaseName = args[3];
            }
            Console.WriteLine( string.Format( "{0} {1} {2} {3}", args[0], args[1], args[2], databaseName) );
            new ScriptInstaller(args[0], args[1], args[2], databaseName);

            //File.Delete(args[1]);
        }

        public static T DeserializeObject<T>(String _XmlizedString) where T : class
        {
            try
            {
                var xs = new XmlSerializer(typeof(T));
                var encoding = new UTF8Encoding();
                var byteArray = encoding.GetBytes(_XmlizedString);
                using (var memoryStream = new MemoryStream(byteArray))
                {
                    return (T)xs.Deserialize(memoryStream);
                }
            }
            catch (Exception exp)
            {
                return null;
            }
        }

        public static void SerializeObject<T>(StreamWriter _writer, T _data) where T : class
        {
            try
            {
                var xs = new XmlSerializer(typeof(T));
                var encoding = new UTF8Encoding();
                xs.Serialize(_writer, _data);
            }
            catch (Exception exp)
            {
            }
        }

        public ScriptInstaller( string _connectionString, string _files, string _reportScripts, string _databaseName )
        {
            DatabaseName = _databaseName;
//            BuildQueryFile(_connectionString, _files, _replace, _with);
            UpdateDB(_connectionString, _files, _reportScripts);
        }
        
        public void UpdateDB( string _connectionString, string _files, string _reportScripts )
        {
            var commands = new List<SQLCommand>();
            int count = 0;
            string commandtext;
            string name = string.Empty;
            using( StreamReader reader = new StreamReader(_files))
            {
                commandtext = reader.ReadToEnd();
            }

            commands = DeserializeObject<List<SQLCommand>>(commandtext);
            bool dbExists = true;
            using (var conn = new SqlTool(ConnectionString(_connectionString, "master")))
            {
                if (!conn.ExecuteScalar("SELECT name FROM sys.databases WHERE name = N'BeverageMonitor'", ref name))
                {
                    count = 0;
                    dbExists = false;
                }
            }
            if (dbExists)
            {
                using (var conn = new SqlTool(ConnectionString(_connectionString, DatabaseName)))
                {
                    if (conn.ExecuteScalar("SELECT COUNT(1) FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DatabaseVersion]') AND type in (N'U')",
                                    ref count))
                    {
                        if (0 == count)
                        {
                            count = 1;
                        }
                        else
                        {
                            conn.ExecuteScalar(string.Format("use {0}; SELECT DatabaseVersion FROM DatabaseVersion", DatabaseName),
                                ref count);
                        }
                    }
                }
            }
            ProcessCommands(_connectionString, count, commands);

            RunReportScripts( _connectionString, _reportScripts);
        }

        private void RunReportScripts(string _connectionString, string _reportScripts)
        {
            if (!string.IsNullOrWhiteSpace(_reportScripts))
            {
                string script = File.ReadAllText(_reportScripts);

                // split script on GO command
                IEnumerable<string> commandStrings = Regex.Split(script, "^\\s*GO\\s*$",
                                                                 RegexOptions.Multiline | RegexOptions.IgnoreCase);
                using (var writer = new StreamWriter("SqlErrors.sql", true))
                {
                    using (var conn = new SqlTool(ConnectionString(_connectionString, DatabaseName)))
                    {
                        foreach (string commandString in commandStrings)
                        {
                            if (commandString.Trim() != "")
                            {
                                try
                                {
                                    Console.WriteLine(commandString);
                                    conn.Execute(commandString);
                                }
                                catch (Exception err)
                                {
                                    Console.WriteLine(err.Message);
                                    writer.WriteLine(err.Message);
                                    writer.WriteLine(commandString);
                                    writer.WriteLine(System.Environment.NewLine);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void ProcessCommands(string _connectionString, int _count, List<SQLCommand> _commands)
        {
            try
            {
                using (var writer = new StreamWriter("SqlErrors.sql"))
                {
                    var commands = _commands.Where(c => c.DBVersion >= _count).OrderBy(d => d.Order).ToList();

                    var master = commands.Where(c => c.DatabaseName.Equals("master", StringComparison.CurrentCultureIgnoreCase));
                    var bevMon = commands.Where(c => c.DatabaseName.Equals("BeverageMonitor", StringComparison.CurrentCultureIgnoreCase));

                    using (var conn = new SqlTool(ConnectionString(_connectionString, "master")))
                    {
                        foreach (var sqlCommand in master)
                        {
                            try
                            {
                                Console.WriteLine(sqlCommand.DBCommand);
                                conn.Execute(sqlCommand.DBCommand);
                            }
                            catch (Exception err)
                            {
                                Console.WriteLine(err.Message);
                                writer.WriteLine( err.Message );
                                writer.WriteLine( string.Format( "{0} Ver: {1} Order:{2} Type:{3}", sqlCommand.DatabaseName, sqlCommand.DBVersion, sqlCommand.Order, sqlCommand.Type ) );
                                writer.WriteLine(sqlCommand.DBCommand);
                                writer.WriteLine(System.Environment.NewLine);
                            }
                        }
                    }

                    commands = bevMon.Where(c => c.Type == 0).OrderBy(d => d.Order).ToList();
                    using (var conn = new SqlTool(ConnectionString(_connectionString, DatabaseName)))
                    {
                        foreach (var sqlCommand in commands)
                        {
                            try
                            {
                                Console.WriteLine(sqlCommand.DBCommand);
                                conn.Execute(sqlCommand.DBCommand);
                                conn.Execute(string.Format("UPDATE DatabaseVersion SET DatabaseVersion ={0}",
                                                           ++sqlCommand.DBVersion));
                            }
                            catch (Exception err)
                            {
                                Console.WriteLine( err.Message );

                                writer.WriteLine(err.Message);
                                writer.WriteLine( string.Format( "{0} Ver: {1} Order:{2} Type:{3}", sqlCommand.DatabaseName, sqlCommand.DBVersion, sqlCommand.Order, sqlCommand.Type ) );
                                writer.WriteLine(sqlCommand.DBCommand);
                                writer.WriteLine(System.Environment.NewLine);
                            }
                        }
                    }

                    // Views
                    commands = _commands.Where(c => c.Type == 2).OrderBy(d => d.Order).ToList();
                    using (var conn = new SqlTool(ConnectionString(_connectionString, DatabaseName)))
                    {
                        foreach (var sqlCommand in commands)
                        {
                            try
                            {
                                Console.WriteLine(sqlCommand.DBCommand);
                                conn.Execute(sqlCommand.DBCommand);
                            }
                            catch (Exception err)
                            {
                                Console.WriteLine(err.Message);

                                writer.WriteLine(err.Message);
                                writer.WriteLine( string.Format( "{0} Ver: {1} Order:{2} Type:{3}", sqlCommand.DatabaseName, sqlCommand.DBVersion, sqlCommand.Order, sqlCommand.Type ) );
                                writer.WriteLine(sqlCommand.DBCommand);
                                writer.WriteLine(System.Environment.NewLine);
                            }
                        }
                    }
                    // Stored Procs
                    commands = _commands.Where(c => c.Type == 1).OrderBy(d => d.Order).ToList();
                    using (var conn = new SqlTool(ConnectionString(_connectionString, DatabaseName)))
                    {
                        foreach (var sqlCommand in commands)
                        {
                            try
                            {
                                Console.WriteLine(sqlCommand.DBCommand);
                                conn.Execute(sqlCommand.DBCommand);
                            }
                            catch (Exception err)
                            {
                                Console.WriteLine(err.Message);

                                writer.WriteLine(err.Message);
                                writer.WriteLine( string.Format( "{0} Ver: {1} Order:{2} Type:{3}", sqlCommand.DatabaseName, sqlCommand.DBVersion, sqlCommand.Order, sqlCommand.Type ) );
                                writer.WriteLine(sqlCommand.DBCommand);
                                writer.WriteLine(System.Environment.NewLine);
                            }
                        }
                    }

                    // Other Commands to be run every time
                    commands = _commands.Where(c => c.Type == 3).OrderBy(d => d.Order).ToList();
                    using (var conn = new SqlTool(ConnectionString(_connectionString, DatabaseName)))
                    {
                        foreach (var sqlCommand in commands)
                        {
                            try
                            {
                                Console.WriteLine(sqlCommand.DBCommand);
                                conn.Execute(sqlCommand.DBCommand);
                            }
                            catch (Exception err)
                            {
                                Console.WriteLine(err.Message);

                                writer.WriteLine(err.Message);
                                writer.WriteLine(string.Format("{0} Ver: {1} Order:{2} Type:{3}", sqlCommand.DatabaseName, sqlCommand.DBVersion, sqlCommand.Order, sqlCommand.Type));
                                writer.WriteLine(sqlCommand.DBCommand);
                                writer.WriteLine(System.Environment.NewLine);
                            }
                        }
                    }

                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }

        }

        private string ConnectionString(string _connectionString, string _dbName)
        {
            int start = _connectionString.ToLower().IndexOf("database=");
            int end = _connectionString.IndexOf(';', start + 1);

            _connectionString = _connectionString.Remove(start, end - start);
            _connectionString = _connectionString.Insert(start,
                                                         string.Format("database={0}",
                                                                       _dbName.Replace("[", "").Replace(
                                                                           "]", "")));
            return _connectionString;
        }

        public void BuildQueryFile(string _connectionString, string _files, string _replace, string _with)
        {
            var commands = new List<SQLCommand>();
            int count = 0;

            using (var reader = new StreamReader(@"C:\Source\Jaxis\Builds\Beverage Monitor\DrinkMonitorCreation.sql"))
            {
                string scripts = reader.ReadToEnd();

                scripts = scripts.Replace((char) 0x0d, 'ß').Replace((char) 0x0a, 'ß').Replace("ß",
                                                                                                Environment.NewLine);

                string[] sript = scripts.Split(new string[] {string.Format("{0}GO{0}", System.Environment.NewLine)}
                                                , StringSplitOptions.RemoveEmptyEntries);
                string databaseName = "master";
                foreach (string s in sript)
                {
                    string t = s.Replace(_replace, _with).Replace("\"", "").Trim();
                    try
                    {
                        if (t.ToLower().StartsWith("use"))
                        {
                            databaseName = t.Replace("[", "").Replace("]", "").Substring(4);

                            int start = _connectionString.ToLower().IndexOf("database=");
                            int end = _connectionString.IndexOf(';', start + 1);

                            _connectionString = _connectionString.Remove(start, end - start);
                            _connectionString = _connectionString.Insert(start,
                                                                            string.Format("database={0}",
                                                                                        t.Replace("[", "").Replace(
                                                                                            "]", "").Substring(4)));
                        }
                        else if (!string.IsNullOrWhiteSpace(t))
                        {
                            var command = new SQLCommand{DBVersion = 0, DBCommand = t, Order = count++, Type = 0, DatabaseName = databaseName};
                            if (t.Contains("CREATE VIEW"))
                            {
                                command.Type = 2;
                            }
                            else if (t.Contains("CREATE PROC"))
                            {
                                command.Type = 1;
                            }
                            commands.Add(command);
                            Console.WriteLine(t);
                        }
                    }
                    catch (Exception err)
                    {
                        Console.WriteLine(err.Message);
                        Console.ReadLine();
                    }
                }
            }
            using (var writer = new StreamWriter(_files))
            {
                SerializeObject( writer, commands );   
            }
        }
    }
}
