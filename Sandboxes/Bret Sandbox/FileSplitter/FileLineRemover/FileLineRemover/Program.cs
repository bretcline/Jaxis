using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileLineRemover
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var reader = new StreamReader(args[0]))
            {
                using (var writer = new StreamWriter(args[0] + ".txt"))
                {
                    var stub = "DataConsumer :: T 045";
                    var line = reader.ReadLine();
                    do
                    {
                        //if (line.Length > 500)
                        if( line.Contains(stub) )
                        {
                            writer.WriteLine(line);
//                            writer.WriteLine(line.Substring(line.IndexOf(stub) + stub.Length - 3));
                        }
                        line = reader.ReadLine();
                    } while (null != line);
                    
                }
            }
        }
    }
}
