using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;

namespace AnonymousData
{
    class Program
    {
        static void Main(string[] args)
        {
            //http://stackoverflow.com/questions/4024754/creating-an-anonymous-type-dynamically


            var data = new List<ExpandoObject>();

            for (int i = 0; i < 100; ++i)
            {
                dynamic row = new ExpandoObject();

                var dictionary = (IDictionary<string, object>)row;
                for (int j = 0; j < 7; ++j)
                {
                    dictionary.Add(string.Format("Col{0}", j), string.Format("Data {0}{1}", i, j));
                }
                data.Add(row);
            }

            foreach (dynamic result in data)
            {
                Console.WriteLine(result.Col1);
                Console.WriteLine(result);
            }

            dynamic test = new ExpandoObject();

            test.MyName = "Bret";
            test.YourName = "Kelly";

            Console.WriteLine( test.MyName );


            // Bind to grid
            // rename column headers

        }
    }
}
