using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using BeverageMonitor.Entities;

namespace PriceImport
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var reader = new StreamReader("HyattHouston.csv"))
            {
                using (var writer = new StreamWriter("MissingItems.text"))
                {
                    using (var data = new BeverageMonitorEntities())
                    {
                        var upcs = data.UPCs.Where( u => u.StandardPrice == null ).ToList();
                        var line = reader.ReadLine();
                        while (null != line)
                        {
                            var items = line.Split(',');
                            if (items.Count() == 3)
                            {
                                items[0] += items[1];
                                items[1] = items[2];
                            }
                            if (1 < items.Count())
                            {
                                var name = items[0].Replace("Liqueur", "").Replace("1L", "");
                                var milliter = data.SizeTypes.Where(s => s.Abbreviation == "ML").FirstOrDefault();
                                var ounce = data.SizeTypes.Where(s => s.Abbreviation == "OZ").FirstOrDefault();
                                var sizeTypeID = milliter.SizeTypeID;
                                var size = 0.0;

                                var matches = Regex.Matches(name, " ([0-9]+)(ml|oz|l)", RegexOptions.IgnoreCase);
                                if (0 < matches.Count)
                                {
                                    size = Double.Parse(matches[0].Groups[1].Value);
                                    switch (matches[0].Groups[2].Value)
                                    {
                                        case "ml":
                                        {
                                            sizeTypeID = milliter.SizeTypeID;
                                            break;
                                        }
                                        case "l":
                                        {
                                            sizeTypeID = milliter.SizeTypeID;
                                            size = size*1000;
                                            break;
                                        }
                                        case "oz":
                                        {
                                            sizeTypeID = ounce.SizeTypeID;
                                            break;
                                        }
                                    }
                                }

                                var upc = GetUPC( upcs, 0, name.ToLower(), sizeTypeID, size);
                                if (null != upc && 1 == upc.Count)
                                {
                                    foreach (var upc1 in upc)
                                    {
                                        Console.WriteLine(String.Format("{0} - {1}", name, upc1.Name));
                                        upc1.UnitPrice = decimal.Parse(items[1].Replace("$", ""));
                                       
                                    }
                                }
                                else
                                {
                                    upc = GetUPC(upcs, 4, name.ToLower(), sizeTypeID, size);
                                    if (null != upc && 1 == upc.Count)
                                    {
                                        Console.WriteLine( String.Format("4 - {0} - {1}", name, upc[0].Name));
                                        upc[0].UnitPrice = decimal.Parse(items[1].Replace("$", ""));
                                    }
                                    else
                                    {
                                        if (upc.Count > 1)
                                        {
                                            foreach (var upc1 in upc)
                                            {
                                                Console.WriteLine(String.Format("8 - {0} - {1}", name, upc1.Name));
                                                var key = Console.ReadKey();
                                                if (key.Key == ConsoleKey.Y)
                                                {
                                                    upc1.UnitPrice = decimal.Parse(items[1].Replace("$", ""));
                                                    Console.WriteLine();
                                                }
                                                else
                                                {
                                                    Console.WriteLine(" --------- " + line);
                                                }
                                            }
                                        }
                                        upc = GetUPC(upcs, 8, name.ToLower(), sizeTypeID, size);
                                        if (null != upc)
                                        {
                                            foreach (var upc1 in upc)
                                            {
                                                Console.WriteLine(String.Format("8 - {0} - {1}", name, upc1.Name));
                                                var key = Console.ReadKey();
                                                if (key.Key == ConsoleKey.Y)
                                                {
                                                    upc1.UnitPrice = decimal.Parse(items[1].Replace("$", ""));
                                                }
                                                else
                                                {
                                                    Console.WriteLine(" --------- " + line);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine(" --------- " + line);
                                            writer.WriteLine(line);
                                        }
                                    }
                                }
                            }
                            line = reader.ReadLine();
                        }
                        data.SaveChanges();
                    }
                }
            }
        }

        private static List<UPC> GetUPC( List<UPC> upcs, int matchIndex, string name, Guid sizeTypeID, double size )
        {
            List<UPC> rc = null;

            if( 0.0 == size)
                rc = upcs.Where(u => matchIndex >= Compute(u.Name.Replace("1000 mL", "").Replace("750 mL", ""), name) && u.SizeTypeID == sizeTypeID).ToList();
            else
                rc = upcs.Where(u => matchIndex >= Compute(u.Name.Replace("1000 mL", "").Replace("750 mL", ""), name) && u.SizeTypeID == sizeTypeID && u.Size == size).ToList();

            return rc;
        }


        public static int Compute(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            var d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            // Step 2
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Step 3
            for (int i = 1; i <= n; i++)
            {
                //Step 4
                for (int j = 1; j <= m; j++)
                {
                    // Step 5
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return d[n, m];
        }
    }
}
