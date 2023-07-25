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

#region >>>>> Readme <<<<<
/*
 This example shows how to connect to an i-PORT R2 and report tags that are detected by the reader
 */
#endregion

using System;
using System.Diagnostics;

//IDENTEC SOLUTIONS specific:
using IDENTEC;
using IDENTEC.Readers;
using IDENTEC.Tags;

//IDENTEC SOLUTIONS Beacon technology specific:
using IDENTEC.Readers.BeaconReaders;
using IDENTEC.Tags.BeaconTags;

namespace i_CARD_CF_B
{

	class iCARDCF_B_Example
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			/* 
			 Notes: 			 
			 You will need to obtain the i-CARD CF .inf file to enable the i-CARD CF com port connection on your PC.
			*/

			try
			{
				Console.WriteLine("----- IDENTEC SOLUTIONS Inc. i-CARD CF 'B' Sample -----\n\n");
				
				int port = CFReaderSearch.FindReaderComPort();
				if (port <= 0)
				{
					Console.WriteLine("The CF Card could not be found.");
					return;
				}
				Console.Write("Connecting to Com{0}: ...", port);				
				iCardCFB reader = new iCardCFB();
				reader.Connect(port);
				Console.WriteLine("Connected!");				

				//Tell the reader to clear the tags out of its internal list when we ask for the list of tags:
				reader.SetTagListBehavior(TagListBehavior.RemoveTagsWhenReported);
				//Turn on the receive amplifier to increase read range, 
				//with it on we can "hear" tags at down to -90 dBm, with it off only at -60
						
				reader.EnableHighRfSensitivity(true);
				int nmV = reader.GetPowerSupplyVoltage();
				Console.WriteLine("\n{0, -10}\t{1,-10}\t{2, -20}","Serial #", "Input Power", "Powered On");
				Console.WriteLine("{0, -10}\t{1,-10}mV\t{2, -20}",
					reader.SerialNumber, nmV, reader.BootDateTime.ToString());										

					while (true)
					{
							//Get the tag extended info (first time and last time seen)							
							Console.WriteLine("\nQuerying reader for its list of tags...");
							TagCollection tags = reader.GetTags(true);
							if (tags.Count == 0)
							{
								Console.WriteLine("\nNo tags to report");
							}
							else
							{
								tags.Sort();
								Console.WriteLine("\n{0} tags to report:\n", tags.Count);
								
								Console.WriteLine("\n{0,-15} {1,-20} {2,-20} {3,-10} {4,-10}", 
									"Tag", "First Detected", "Last Detected", "Max RSSI", "Last RSSI");
								Console.WriteLine("----------------------------------------------------------------------------");
								foreach (iB2Tag t in tags)
								{
									Console.WriteLine("{0,-15} {1,-20:g} {2,-20:g} {3,-10} {4,-10}", 
										t.Label, t.FirstSeen, t.ContactTime, t.MaxSignal, t.Signal);
								}
							}
							Console.WriteLine("\r\n");						

						Console.Write("View tags seen on each reader again (Y/N)? Y: ");
						string strReponse = Console.ReadLine();
						if (strReponse.ToLower() == "n")
							break;
					}
				}
			
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			Console.WriteLine("Press ENTER to end the program");
			Console.ReadLine();
		}
	}
}
