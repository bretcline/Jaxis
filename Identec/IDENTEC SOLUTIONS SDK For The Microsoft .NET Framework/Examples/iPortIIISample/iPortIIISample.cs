
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
 This example shows how to connect to an iPORT 3, set power settings, and scan on individual antennas.
 */
#endregion

using System;
using IDENTEC;
using IDENTEC.Readers;
using IDENTEC.Tags;

namespace iPortIIISample
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Class1
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			/* Note: Your iPORT III must be set up for use with this SDK
			 On the "General" page scroll to the bottom and ensure that the custom application is switched off
			 On the "Configuration" page ensure that the iPort Type is set to "host" under the "iPort General" section
			*/

			try
			{
				iPort3 myReader = new iPort3();

				//If you want to have a receive timeout, you can set it as such
				//myReader.tcpClient.ReceiveTimeout = 10000;
				

				Console.WriteLine("Please input the IP Address of the i-PORT and then press ENTER:");
				string strIP = Console.ReadLine();

				Console.WriteLine("Connecting to " + strIP + " ...");				
				myReader.Connect(strIP, 7070);
				Console.WriteLine("Connected!");				
				Console.WriteLine("Version: " + myReader.Version);
				
				//Set max power on each antenna; and disable antenna:
				//Note that antenna 5 is the W antenna:
				for (int i = 1; i <= 5; i++)
				{					
					myReader.SetTxPowerForIQTags(i, 6);
					myReader.EnableAntenna(i, false); //disable the antenna for now:
					//TODO: possibly change the receive settings and threshold etc. For now the default values will do
				}


				//iterate through the 4 normal antennas:
				for (int i = 1; i <= 4; i++)
				{
					Console.WriteLine("Scanning on antenna " + i + "...");
					myReader.ActiveAntenna = i;
					myReader.EnableAntenna(i, true);
					TagCollection tags = myReader.ScanForIQTags(128, false);
					tags.Sort();

					Console.WriteLine(tags.Count.ToString() + " tags detected on antenna " + i);

					foreach (Tag tag in tags)
					{
						Console.WriteLine(tag.Label + "\t" + " @ " + tag.GetSignalStrength(i) + "dBm");
					}
					Console.WriteLine("");

					//disable the antenna
					myReader.EnableAntenna(i, false); 
				}
				//Now scan on all antenna simultaneously; enabling the wakeup antenna as well:
				myReader.EnableAllAntennas(true);

				//Set active antenna to all:
				myReader.ActiveAntenna = 0;
				Console.WriteLine("Scanning on all antennas simultaneously...");

				int nStart = Environment.TickCount;
				TagCollection myTags  = myReader.ScanForIQTags(512, false);
				int nEnd = Environment.TickCount;
				TimeSpan ts = new TimeSpan(0,0,0,0, nEnd - nStart);
				myTags.Sort();
				Console.WriteLine(myTags.Count.ToString() + " tags detected while scanning all antenna simultaneously in a time of " 
					+ ts.ToString());				
				Console.WriteLine("Tag #\t\tAnt1\tAnt2\tAnt3\tAnt4");
				foreach (Tag tag in myTags)
				{					
					Console.WriteLine(tag.Label + "\t" 
						+ (tag.GetSignalStrength(1) == -128 ? "--" : tag.GetSignalStrength(1).ToString())  + "\t" 
						+ (tag.GetSignalStrength(2) == -128 ? "--" : tag.GetSignalStrength(2).ToString())  + "\t" 
						+ (tag.GetSignalStrength(3) == -128 ? "--" : tag.GetSignalStrength(3).ToString())  + "\t" 
						+ (tag.GetSignalStrength(4) == -128 ? "--" : tag.GetSignalStrength(4).ToString()));
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("An exception was thrown:\r\n" + ex.Message);
			}

			Console.WriteLine("Press ENTER to continue");
			Console.ReadLine();
		}
	}
}
