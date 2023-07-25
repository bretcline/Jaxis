using System;
using IDENTEC.Readers;
using IDENTEC.Tags;

namespace iCard3Scan
{
	class NewTest
	{
		[STAThread]
		static void Main(string[] args)
		{
			iCard3 myReader = new iCard3();
			try
			{
				if (myReader.Connect())
				{
					// Set the transmission power to be high so that we detect tags at a fair distance
					myReader.TxPowerIQ = 6;
					// Scan for up to 64 tags; allow them to blink when they respond
					TagCollection tags = myReader.ScanForIQTags(64, true);
					// Sort the tags numerically
					tags.Sort();
					Console.WriteLine(tags.Count + " tags detected during scan:");
					foreach (Tag t in tags)
					{
						Console.WriteLine(t.Number);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}            
		}
	}
}
