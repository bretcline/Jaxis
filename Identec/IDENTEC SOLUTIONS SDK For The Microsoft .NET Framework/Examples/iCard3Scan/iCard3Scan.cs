
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
 * This sample demonstrates how to connect to an iCard, set the transmission power and then scan for tags.
 * The IDs of the tags detected are then displayed.
 */
#endregion
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
					// If your i-Q tags are too close to the reader then you may not detect them at this power level
					// Try no closer than 3m
					myReader.TxPowerIQ = 6;
					// Scan for up to 64 tags; allow them to blink when they respond
					TagCollection tags = myReader.ScanForIQTags(64, true);
					// Sort the tags numerically
					tags.Sort();
					Console.WriteLine(tags.Count + " tags detected during scan:");
					foreach (Tag t in tags)
					{
						Console.WriteLine(t.Label);
					}
					//Note that this is only *one* scan and you may not detect all the tags in the field
					//Typically it can take several scans at different tx powers to detect all tags					
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			} 
           
			Console.WriteLine("Press Enter to continue...");
			Console.ReadLine();   
		}
	}
}
