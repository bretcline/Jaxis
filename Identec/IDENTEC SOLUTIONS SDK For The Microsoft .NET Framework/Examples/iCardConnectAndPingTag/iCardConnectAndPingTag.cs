
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
/* This example shows how to connect to an iCard and contact an IQ tag 
 * without scanning first and then displays the tag's state information.*/
#endregion

using System;
using IDENTEC.Readers;
using IDENTEC.Tags;


namespace iCardConnectAndPingTag
{
	class NewTest
	{
		[STAThread]
		static void Main(string[] args)
		{
			{				
				try
				{
					Console.WriteLine("Type the number of the tag to ping and then press ENTER:");
					iQTag tag = new iQTag();
					tag.Label = Console.ReadLine(); 
					iCard3 myReader = new iCard3();
					if (myReader.Connect())
					{						
						if (myReader.PingTag(tag))
						{
							Console.WriteLine("Tag info: ");
							Console.WriteLine("ID: " + tag.Number.ToString());
							Console.WriteLine("Model: " +  tag.ModelType.ToString());
							if (iQTag.LoggerInstalledState.Available == tag.LoggerInstalled)
							{
								Console.WriteLine("Logging State: " + tag.Logging.ToString());
							}
							Console.WriteLine("Range State: " + tag.Range);
							if (tag.ReportsBatteryPercentConsumed)
							{
								Console.WriteLine(tag.BatteryPercentConsumed.ToString() + "% of battery consumed"); 
							}
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
			}
			Console.WriteLine("Press Enter to continue...");
			Console.ReadLine();                
		}
	}
}
