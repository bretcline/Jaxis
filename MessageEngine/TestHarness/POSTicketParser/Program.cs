using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Jaxis.Interfaces;
using Jaxis.MessageLibrary.POS;
using Jaxis.Readers.POS.Parsers;

namespace POSTicketParser
{
    class Program
    {
        protected IParser m_Parser = null;

        static void Main(string[] args)
        {
            //var p = new Program();
            //p.ProcessData("ParserConfig.xml", "TicketData.txt");

            POSFileReader reader = new POSFileReader();
            reader.Start();
        }

        public void ProcessData( string _configFile, string _dataFile )
        {
            m_Parser = new Generic(_configFile, true);
            using (var reader = new StreamReader(_dataFile))
            {
                string data = reader.ReadToEnd();

                var tickets = data.Split(new string[] { "================================" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var ticket in tickets)
                {
                    ITicket T = m_Parser.ParseData(ticket);

                    Console.WriteLine(T.ToString());
                }
            }
        }
    }
}