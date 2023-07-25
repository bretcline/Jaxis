
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
 * This sample demonstrates how to display information from an iCard.
 */
#endregion

using System;
using IDENTEC.Readers;
using IDENTEC.Tags;


namespace iCard_Info
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
					Console.WriteLine("i-Card Information: " + myReader.Information);
					Console.WriteLine("Serial Number: " + myReader.SerialNumber);
					Console.WriteLine(@"Production Info (Year/Week/Batch): " + myReader.ProductionInformation.Year + "//" 
						+ myReader.ProductionInformation.Week + "//" + myReader.ProductionInformation.ProductionNumber);
					Console.WriteLine("Region: " + myReader.Region.ToString());
					Console.WriteLine("Current Transmission Power for i-Q tag communications: " + myReader.TxPowerIQ + "dBm");
					Console.WriteLine("Current Transmission Power for i-D2 tag communications: " + myReader.TxPowerID + "dBm");
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