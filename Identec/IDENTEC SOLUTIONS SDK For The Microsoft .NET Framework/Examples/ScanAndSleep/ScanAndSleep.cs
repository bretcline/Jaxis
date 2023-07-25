
#region >>>>> COPYRIGHT AND COMPANY INFORMATION <<<<<
/* 
 *  
 * Copyright (c) 2006 by Identec Solutions.
 *
 * This Copyright notice may not be removed or modified without prior written consent of Identec Solutions.
 * Identec Solutions, reserves the right to modify this software without notice.
 * 
 *
 * IDENTEC Solutions, Inc.
 *
 * www.identecsolutions.com                  
 *
 * IDENTEC SOLUTIONS Inc. grants you a nonexclusive copyright license to use all programming code examples 
 * from which you can generate similar function tailored to your own specific needs.
 *
 * All sample code is provided by IDENTEC SOLUTIONS Inc. for illustrative purposes only. 
 * These examples have not been thoroughly tested under all conditions. IDENTEC SOLUTIONS Inc., therefore, 
 * cannot guarantee or imply reliability, serviceability, or function of these programs.

 * All programs contained herein are provided to you "AS IS" without any warranties of any kind. 
 * The implied warranties of non-infringement, 
 * merchantability and fitness for a particular purpose are expressly disclaimed.
 */
#endregion

#region >>>>> ReadMe <<<<<
/*
 * This sample demonstrates how to scan for tags and put them to sleep.
 * Once a tag is in a sleep state it will not respond to any interrogations until it wakes up
 * after the specified timeout.
 * 
 * Note that this sample continues to scan for new tags until it can no longer
 * put any tags to sleep. Therefore it is a simple "max detection" routine.
 * 
 * */
#endregion

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
			int nTotalTagsSleeping = 0;
			iCard3 myReader = new iCard3();
			try
			{
				if (myReader.Connect())
				{
					int nTemp = 0;
					while (true)
					{
						nTemp = ScanAndSleep(myReader);
						if (nTemp == 0)
							break;
						nTotalTagsSleeping += nTemp;
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			} 

		Console.WriteLine(nTotalTagsSleeping + " tags now sleeping");
		Console.WriteLine("Press ENTER to continue...");
        Console.ReadLine();

		}

		/// <summary>
		/// Scans and sleeps the tag.
		/// </summary>
		/// <param name="myReader">The iCard object</param>
		/// <returns>The number of tags successfully put to sleep</returns>
		private static int ScanAndSleep(iCard3 myReader)
		{
			int nTagsSleeping = 0;

			// Set the transmission power to be high so that we detect tags at a medium distance
			myReader.TxPowerIQ = 2;
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
						nTagsSleeping++;
						Console.WriteLine("Tag " + t.Label + " now sleeping for 30 seconds");
					}
					else
					{
						Console.WriteLine("Could not sleep tag " + t.Label + ". Reason: " + myReader.DeviceStatus);
					}
				}
				catch (iCardCommunicationsException ex)
				{
					Console.WriteLine("An error occured communicating with the iCard: " + ex.Message);
				}
				catch (Exception ex)
				{
					Console.WriteLine("An error occured: " + ex.Message);
				}						
			}

			return nTagsSleeping;
		}
	}
}
