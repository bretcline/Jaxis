using System;
using System.Diagnostics;
using System.Collections;
using IDENTEC.Readers;
using IDENTEC.Tags;

namespace Read_and_Write_Tag_Data
{
	/// <summary>
	/// Summary description for ExampleReadWriteTags.
	/// </summary>
	class ExampleReadWriteTags
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			iCard3 myReader = new iCard3();
			try
			{
				if (myReader.Connect())
				{
					// Set the transmission power to be high
					myReader.TxPowerIQ = 6;
					// Scan for up to 32 tags; no blink during scan
					TagCollection tags = myReader.ScanForIQTags(32, false);
					// Sort the tags numerically
					tags.Sort();					
					
					ArrayList phrases = new ArrayList();
					phrases.Add("You say yes");
					phrases.Add("I say no");
					phrases.Add("You say stop");
					phrases.Add("And I say go go go");
					phrases.Add("You say goodbye");
					phrases.Add("And I say hello");
					phrases.Add("Hello hello");

					Random rand = new Random();
					TagCollection successfulTags = new TagCollection();

					foreach (iQTag t in tags)
					{
						if (phrases.Count == 0)
							break;
						try
						{
							int index = rand.Next(0, phrases.Count -1);
							TagWriteDataResult writeResult =
								myReader.WriteTagDataString(t, 200, phrases[index] as string);
							if (writeResult.Success)
							{
								//remove the phrase so that each tag has something unique to say
								phrases.RemoveAt(index);
								successfulTags.Add(t);
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

					foreach (iQTag t in successfulTags)
					{
						TagReadStringResult readResult = myReader.ReadTagDataString(t, 200);
						if (readResult.Success)
							Console.WriteLine(t.ToString() + ": " + readResult.Text);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			} 
			Console.WriteLine("Press ENTER to continue...");
			Console.ReadLine();
		}
	}
}
