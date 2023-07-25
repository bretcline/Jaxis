using System;
using System.IO;

namespace LOC
{
    class Program
    {
        static void Main(string[] _args)
        {
            try
            {
                var count = 0;
                for (var i = 1; i < _args.Length; i++ )
                    count += Count(_args[0], _args[i]);
                Console.WriteLine(count);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Whoops! First arg is the path, followed by one or more search pattern args.");
                Console.WriteLine(ex);
            }
        }

        private static int Count(string _directory, string _pattern)
        {
            var count = 0;
            var files = Directory.GetFiles(_directory, _pattern, SearchOption.AllDirectories);
            
            foreach (var file in files)
            {
                using (var reader = new StreamReader(file))
                {
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        if (!string.IsNullOrEmpty(line.Trim()))
                            count++;
                        line = reader.ReadLine();
                    }
                }
            }

            return count;
        }
    }
}
