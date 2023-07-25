using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POSDataImport
{
    class Program
    {
        static void Main(string[] args)
        {
            var importer = new POSDataImporter(@"C:\temp\BevMet\Mandalay Bay\7.27");
            importer.Import();
        }
    }
}
