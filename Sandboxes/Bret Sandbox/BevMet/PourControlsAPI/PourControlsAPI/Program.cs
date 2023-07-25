using System;
using System.Collections.Generic;
using System.Text;
using PourControlsAPI.BevMonAPI;

namespace PourControlsAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            BevMonAPI.BeverageManagementAPI test = new BeverageManagementAPI();
            PourInformation[] data = test.GetPourInformation(DateTime.Now.AddDays(-1), true, DateTime.Now, true);
            for (int i = 0; i < data.Length; ++i)
            {
                Console.WriteLine(String.Format("{0} {1} {2} {3}", data[i].TagNumber, data[i].UPCName, data[i].PourAmount, data[i].PourTime));
            }
        }
    }
}
