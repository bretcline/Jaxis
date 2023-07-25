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
/* This example shows how to connect to an iCard and contact an i-D2 tag 
 * without scanning first and then displays the tag's state information.*/
#endregion

using System;
using IDENTEC.Readers;
using IDENTEC.Tags;

namespace iCardConnectAndPingi_D2Tag
{
	/// <summary>
	/// Test class for connecting to an i-CARD3 and pinging an i-D2 tag.
	/// </summary>
	class Class1
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
							
			try
			{					
				iCard3 myReader = new iCard3();
				if (myReader.Connect())
				{
					Console.WriteLine("Type the number of an i-D2 tag to ping and then press ENTER:");
                    iD2Tag tag = new iD2Tag();
					tag.Label = Console.ReadLine();
					if (myReader.PingTag(tag))
					{
						Console.WriteLine("Tag info: ");
						Console.WriteLine("ID: " + tag.Number.ToString());
						Console.WriteLine("Region: " + tag.Region.ToString());
						Console.WriteLine("Signal Strength: " + tag.Signal + "dBm");
						Console.WriteLine("Version: " + tag.Version.ToString());
						Console.WriteLine("Battery State: " + tag.Battery.ToString());
						Console.WriteLine("Maximum Data Storage: " + tag.DataCapacity + " bytes");
					}
					else
					{
						Console.WriteLine("Could not ping the specified tag. Card device status: " + myReader.DeviceStatus.ToString());
					}
				}
				else
				{
					Console.WriteLine("Error reading card: " + myReader.DeviceStatus);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("An exception was thrown : " + ex);
			}
			
			Console.WriteLine("Press Enter to continue...");
			Console.ReadLine();                
		}
	}
}
