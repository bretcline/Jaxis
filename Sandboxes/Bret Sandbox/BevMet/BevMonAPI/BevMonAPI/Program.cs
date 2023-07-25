using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BevMonAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Program();
            p.GetData();
        }

        public void GetData( )
        {
            var api = new BevManAPI.BeverageManagementAPIClient();
            var data = api.GetPourInformation(DateTime.Now.AddDays(-5), DateTime.Now).OrderBy( p => p.PourTime).ToList();

            foreach (var pourInformation in data)
            {
                Console.WriteLine(String.Format("{0} {1} {2} {3}", pourInformation.TagNumber, pourInformation.UPCName, pourInformation.PourAmount, pourInformation.PourTime));
            }
        }
    }
}
