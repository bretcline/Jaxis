using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace POSDataImport
{
    class POSDataImporter
    {
        private string m_FilePath { get; set; }

        public POSDataImporter( string _path )
        {
            m_FilePath = _path;
        }

        public void Import( )
        {
            var files = Directory.GetFiles(m_FilePath);
            using (var writer = new StreamWriter("Data_Insert.sql"))
            {
                foreach (var file in files)
                {
                    var filename = Path.GetFileName(file);
                    if (filename.EndsWith(".txt"))
                    {
                        filename = filename.Replace(".txt", "");
                    }
                    using (var reader = new StreamReader(file))
                    {
                        var line = reader.ReadLine();

                        while (null != line)
                        {
                            //var items = line.Split(',');
                            var sql = string.Format("INSERT INTO {0} select {1}", filename, line.Replace( '"', '\'').Replace("$", ""));
                            writer.WriteLine(sql);

                            line = reader.ReadLine();
                        }

                    }
                }
            }
        }

    }
}
