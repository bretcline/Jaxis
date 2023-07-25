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
using System.Net.Sockets;
using System.Collections; 

//IDENTEC SOLUTIONS specific:
using IDENTEC;
using IDENTEC.Readers;
using IDENTEC.Tags;

//IDENTEC SOLUTIONS Beacon technology specific:
using IDENTEC.Readers.BeaconReaders;
using IDENTEC.Tags.BeaconTags;

namespace i_PORT_R2
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class i_PORT_R2_Example
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{

            ArrayList readers = new ArrayList();
			/* 
			 Notes: 			 
				This example is designed to be used with a serial server over TCP such as a "Digi One SP" from Digi (www.digi.com).
				Please contact IDENTEC SOLUTIONS Inc. for a quick setup guide on using a Digi device with the i-PORT R2.
				
				Alternatively you could use a USB to RS-422 convertor that provides a com port (this would require a code change below)
			*/

            

			try
			{
				Console.WriteLine("----- IDENTEC SOLUTIONS Inc. i-PORT R2 Sample -----\n\n\n");
				Console.WriteLine("Please input the IP Address of serial server to connect to then press ENTER:");
				
				string strIP = Console.ReadLine();

				Console.WriteLine("Connecting to " + strIP + " ...");				
				//Note: the default port for the Digi One SP is 2101
                TCPSocketStream myStream = new TCPSocketStream(strIP, 2101);
                myStream.Open();
				Console.WriteLine("Connected!");				
				
				//TODO: set the socket options to match your network 
                myStream.ReadTimeout = new TimeSpan(0, 0, 10);
                myStream.WriteTimeout = new TimeSpan(0, 0, 10);

                iBusAdapter myBus = new iBusAdapter(myStream);
                

				Console.WriteLine("Enumerating readers. Please wait...");				
				//Now start enumerating the readers connected to the serial server (on the daisy chain chain)
				//We specify up to a 5 second wait for the first i-PORT R2 to respond (if there are no readers then it will take 5 seconds)
                IBusDevice[] devs = myBus.EnumerateBusModules();
                if (devs.Length == 0)
				{
					Console.WriteLine("There were no readers found on the serial server");
				}
				else
				{
					Console.WriteLine("There were {0} readers found on the serial server\n\n", devs.Length);
					Console.WriteLine("Reader\tSerial #\tInput Power\t\tPowered On");
					for (int i = 0; i < devs.Length; i++)
					{
                        iPortR2 r2 = devs[i] as iPortR2;
                        if (r2 != null)
                        {
                            readers.Add(r2);
                            //Tell the reader to clear the tags out of its internal list when we ask for the list of tags:
                            r2.SetTagListBehavior(TagListBehavior.RemoveTagsWhenReported);
                            //Turn on the receive amplifier to increase read range, 
                            //with it on we can "hear" tags at down to -90 dBm, with it off only at -60

                            r2.EnableHighRfSensitivity(true);
                            int nmV = r2.GetPowerSupplyVoltage();
                            Console.WriteLine("{0}\t{1}\t{2}mV\t\t{3}",
                                r2.Address, r2.SerialNumber, nmV, r2.BootDateTime.ToString());
                        }
					}

					Console.WriteLine("\n\n");
					Console.WriteLine("Press ENTER to start querying for tag lists\n\n");
					Console.ReadLine();

					while (true)
					{
	
						foreach (iPortR2 r2 in readers)
						{
							//Get the tag extended info (first time and last time seen)
							
							Console.WriteLine("\nQuerying reader {0} for its list of tags...", r2.Address);
							TagCollection tags = r2.GetTags(true);
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
						}

						Console.Write("View tags seen on each reader again (Y/N)? Y: ");
						string strReponse = Console.ReadLine();
						if (strReponse.ToLower() == "n")
							break;
					}
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
