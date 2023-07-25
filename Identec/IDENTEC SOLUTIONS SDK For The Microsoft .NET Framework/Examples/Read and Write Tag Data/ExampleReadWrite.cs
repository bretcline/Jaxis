
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
 This example shows how to use an iCard to connect, scan, and read/write user information on tags.
 */
#endregion

using System;
using System.Diagnostics;
using System.Collections;
using IDENTEC.Readers;
using IDENTEC.Tags;

namespace Read_and_Write_Tag_Data
{
	class ExampleReadWriteTags
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			iCard3 myReader = new iCard3();
			// Since we are doing a lot of communications, set the card retries high
			myReader.Retries = 4;
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
						catch (iCardCommunicationsException ex)
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
