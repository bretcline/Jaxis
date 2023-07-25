using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace RSCService
{
    class Program
    {
        static void Main(string[] args)
        {
            //bool waiting = true;
            //var client = new WebClient();
            //client.Headers["Content-type"] = "application/json";
            //client.Encoding = Encoding.UTF8;

            //client.UploadStringCompleted += (_s, _e) =>
            //{
            //    try
            //    {
            //        Console.WriteLine("");
            //        Console.WriteLine(_e.Result);
            //        waiting = false;
            //    }
            //    catch (Exception ex)
            //    {
            //        var message = "EXCEPTION: " + ex;
            //        Console.WriteLine("");
            //        Console.WriteLine(message);
            //    }
            //};

            //client.UploadStringAsync(new Uri("http://192.206.176.194/KPIBase/KPIService.svc/JSON/GetLeniency"),
            //    "POST", "{\"siteCustomers\":[\"154\",\"149\"]}");

            //while (true == waiting)
            //{
            //    Thread.Sleep(100);
            //    Console.Write(".");
            //}

            using (var file = new StreamWriter("output.txt"))
            {

                var srv = new KPIService.KPIServiceClient();

                var data = srv.GetVendorSpendTop10(new int[] {149});

                var vendor = data.Select(d => d.VendorName).Distinct().ToList();

                foreach (var name in vendor)
                {
                    var vendorData = data.Where(d => d.VendorName == name).Sum(d => d.InvoiceAmount);
                    //Console.WriteLine( string.Format( "{0} - {1}", name, vendorData));
                }

                var catSpend = srv.GetCatalogSpendRS(new int[] {149});

                var months = catSpend.Select(c => c.ReportMonth).Distinct().ToList();

                var catSpendNON = srv.GetCatalogSpendNonRSCRS(new int[] {149});

                var monthsNON = catSpendNON.Select(c => c.ReportMonth).Distinct().ToList();

                vendor = catSpend.Select(d => d.ProductDescription).Distinct().ToList();
                foreach (var name in vendor)
                {
                    var vendorData = catSpend.Where(d => d.ProductDescription == name && d.ReportMonth == 1).Sum(d => d.Spend);
                    file.WriteLine(string.Format("{0} - {1}", name, vendorData));
                }
                file.WriteLine();

                var catsNON = catSpendNON.Select(d => d.ProductDescription).Distinct().ToList();
                foreach (var name in catsNON)
                {
                    var vendorData = catSpendNON.Where(d => d.ProductDescription == name && d.ReportMonth == 1).Sum(d => d.Spend);
                    file.WriteLine(string.Format("{0} - {1}", name, vendorData));
                }




                var monthOne = data.Where(d => d.ReportMonth == 1).ToList();

                foreach (var top10JobsiteSpend in data)
                {
                    Console.WriteLine(top10JobsiteSpend.InvoiceAmount);
                }
            }
        }
    }
}
