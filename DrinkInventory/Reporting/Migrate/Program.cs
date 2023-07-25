using System;

namespace Migrate
{
    class Program
    {
        static void Main(string[] _args)
        {
            try
            {
                var path = ".\\";
                if (_args.Length == 1)
                {
                    path = _args[0];
                }
                else if (_args.Length > 1)
                {
                    throw new Exception("Must provide no arguments (current directory) or 1 path argument.");
                }

                Migrator.Migrate(path);
            }
            catch(Exception ex)
            {
                Console.WriteLine("ERROR OCCURRED: {0}", ex);
            }
        }
    }
}
