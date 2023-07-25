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
/* This example shows how to connect to an iCard and contact an IQ tag for temperature logging purposes.
 * The tag is specified from the command line arguments and works in two modes:
 *	1. If the tag is not logging it is set into a logging mode
 *	2. If the tag is is logging then the temperature log is read
 * 
 * Please note:
 *	- You can read the temperature log off of a tag with or without stopping the log first.
 *	- Starting the logger on a tag that is already logigng will clear the existing log and start a new one
 * 
 * To set the tag ID in the command line arguments in Visual Studio .NET, see the project "properties" 
 * under project menu (configuration properties, debugging, command line arguments).
 * */
#endregion

using System;
using IDENTEC.Readers;
using IDENTEC.Tags;
using IDENTEC.Tags.Logging;

namespace Temperature_Logging
{

	class Class1
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			{				
				try
				{
					Console.WriteLine("Type the number of an i-Q tag to contact for temperature information and then press ENTER:");
					iQTag myTag = new iQTag();
					myTag.Label = Console.ReadLine(); 		
					iCard3 myReader = new iCard3();
					if (myReader.Connect())
					{
						//Display some information about the tag before reading the log:						
						if (!myReader.PingTag(myTag))
						{
							Console.WriteLine("Failed to contact tag. Reason: " + myReader.DeviceStatus);
						}
						else
						{
							Console.WriteLine("Tag info: ");
							Console.WriteLine("ID: " + myTag.Label.ToString());
							Console.WriteLine("Model: " +  myTag.ModelType.ToString());
							Console.WriteLine("Range State: " + myTag.Range);
							//Force the tag to get the temperature now; this has no effect on the temperature log or the logging state
							try
							{
								IDENTEC.Tags.Logging.TemperatureLogSample t = myReader.ReadTagCurrentTemperature(myTag);
								Console.WriteLine("Current temperature in degrees Celsius: " + t.DegreesCelsius.ToString());
							}
							catch (PartialTagCommunicationsException partial)
							{
								//We couldn't complete the data transfer :(
								Console.WriteLine(partial.Message);
							}
							catch(TagHasNoLoggerException noLogger)
							{
								//Oops, we should choose the correct type of tag!
								Console.WriteLine("This demo is only applicable to i-Q Temperature tags. Exception details: " + noLogger.Message);
								throw;
							}

							if (myTag.Logging == iQTag.LoggingState.Off)
							{
								//start logging at 1 minute intervals
								TimeSpan ts = new TimeSpan(0,0,1,0,0);
								Console.WriteLine("Starting the log. Please wait a moment......");
								myReader.StartTagLogging(myTag, ts);
							}
							else
								if (myTag.Logging == iQTag.LoggingState.On)
							{			
		
								Console.WriteLine("Reading the log. Please wait a moment......");
								//Now read the temperature log
								TemperatureLogData tLog = myReader.ReadTagTemperatureLog(myTag);
								Console.WriteLine("Log contains: " + tLog.SampleCount + " samples recording every " 
									+ tLog.LoggingInterval.Minutes + " minutes.");							
								Console.WriteLine("Log started at: " + tLog.Start.ToString());							
								Console.WriteLine("Log ended at: " + tLog.End.ToString());					
					
								Console.Write("Would you like to see the samples (Y/N)? ");
								string strInput = Console.ReadLine();
								char [] Yes = new char[] {'Y', 'y'};
								if (strInput.IndexOfAny(Yes) != -1)
								{
									foreach (TemperatureLogSample sample in tLog.Samples)
									{
										Console.WriteLine(sample.DegreesCelsius.ToString("00.0") 
											+ "°C @ " + sample.SampleTime.ToString());
									}
								}
							}
						}
					}
					else
					{
						Console.WriteLine("Error reading card: " + myReader.DeviceStatus);
					}
				}
				catch (PartialTagCommunicationsException ex)
				{
					//TODO: Possible retries with different power settings as communications was poor or non-existent
					Console.WriteLine(ex.Message);
				}
				catch (TagHasNoLoggerException ex)
				{
					//TODO: Display a friendly reminder that the tag isn't capable of logging
					Console.WriteLine(ex.Message);
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
