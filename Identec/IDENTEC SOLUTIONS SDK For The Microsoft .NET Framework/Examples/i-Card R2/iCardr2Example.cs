
#region >>>>> COPYRIGHT AND COMPANY INFORMATION <<<<<
/* 
 *  
 * Copyright (c) 2005 by Identec Solutions.
 *
 * This Copyright notice may not be removed or modified without prior written consent of Identec Solutions.
 * Identec Solutions, reserves the right to modify this software without notice.
 * 
 *
 * IDENTEC Solutions, Inc.
 *
 * (250) 860-6567; Fax: (250) 860-6541
 * 1860 Dayton Street  Suite 102
 * Kelowna, British Columbia, Canada V1Y 7W6
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
 This example shows how to connect to an i-Card R2 and respond to events.
 * 
 * Note that this device is no longer on our product list.
 */
#endregion

using System;
using IDENTEC.Readers;
using IDENTEC.Tags;
using IDENTEC.Readers.BeaconReaders;
using IDENTEC.Tags.BeaconTags;
using System.Threading;

namespace i_Card_R2
{
	class R2Example
	{
		public R2Example()
		{
			m_R2 = new iCardR2();
			m_R2.TagBeacon +=new IDENTEC.Readers.BeaconReaders.iCardR2.TagBeaconEventHandler(m_R2_TagBeacon);
			m_R2.ErrorOccurred +=new IDENTEC.Readers.BeaconReaders.iCardR2.iCardR2ErrorEventHandler(m_R2_ErrorOccurred);
		}
		private iCardR2 m_R2;
		[STAThread]
		static void Main(string[] args)
		{
			R2Example r2Example = new R2Example();
			try
			{
            
				r2Example.SetupCard();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message + "\r\n\r\n" + ex.StackTrace);
			}
			Console.WriteLine("To end program at any time, press Enter...\r\n\r\n\r\n");
			Console.ReadLine();   
			r2Example.m_R2.Disconnect();
		}

		///Gets the reader up and running
		private void SetupCard()
		{
			m_R2.Connect();
			m_R2.StartListening(true);
		}

		///Responds to the iCardR2.TagBeaconEventHandler event to print out the serial number and the  date/time of contact
		private void m_R2_TagBeacon(object sender, TagBeaconEventArgs e)
		{
			Console.WriteLine("Tag " + e.tag.Label + " seen at " + e.tag.ContactTime);
		}

		///Responds to the iCardR2.iCardR2ErrorEventHandler and prints the error message to the screen
		private void m_R2_ErrorOccurred(object sender, iCardR2ErrorEventArgs e)
		{
			Console.WriteLine(e.ex.Message + " " + e.ex.StackTrace);
		}
	}
}
