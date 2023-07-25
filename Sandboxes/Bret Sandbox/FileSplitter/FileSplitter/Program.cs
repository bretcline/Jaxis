using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileSplitter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (File.Exists(args[0]))
            {
                using (StreamReader reader = new StreamReader(args[0]))
                {
                    long size = reader.BaseStream.Length;
                    long index = 0;
                    int newFileSize = 100000000;
                    long count = (size/newFileSize) + 1;
                    long newSize = size/count;
                    var buffer = new char[newSize + 1];
                    for( int i = 0; i < count; ++i)
                    {
                        using (var writer = new StreamWriter(string.Format("{1}.{0}", args[0], i)))
                        {
                            reader.Read(buffer, 0, (int) newSize);
                            writer.Write( buffer );
                            index += newSize;
                        }
                    }
                }
            }
        }
    }
}
