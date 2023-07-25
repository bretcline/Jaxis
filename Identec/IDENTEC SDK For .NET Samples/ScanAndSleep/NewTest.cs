using System;
using IDENTEC.Readers;
using IDENTEC.Tags;

		

namespace ScanAndSleep
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
					// Set the transmission power to be high so that we detect tags at a medium distance
					myReader.TxPowerIQ = 0;
					// Scan for up to 32 tags; no blink during scan
					TagCollection tags = myReader.ScanForIQTags(32, false);
					// Sort the tags numerically
					tags.Sort();
					Console.WriteLine(tags.Count + " tags detected during scan:");

					// Bump up the transmission power a bit in case the tag is moving away from the reader
					myReader.TxPowerIQ = 3;
					foreach (iQTag t in tags)
					{
						try
						{
							if (myReader.SleepTag(t, 30))
							{
								Console.WriteLine("Tag " + t.Number + " now sleeping for 30 seconds");
							}
							else
							{
								Console.WriteLine("Could not sleep tag " + t.Number + ". Reason: " + myReader.StatusMessage);
							}
						}
						catch (iCard3CommunicationsException ex)
						{
							Console.WriteLine("An error occured communicating with the iCard: " + ex.Message);
						}
						catch (Exception ex)
						{
							Console.WriteLine("An error occured: " + ex.Message);
						}						
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
