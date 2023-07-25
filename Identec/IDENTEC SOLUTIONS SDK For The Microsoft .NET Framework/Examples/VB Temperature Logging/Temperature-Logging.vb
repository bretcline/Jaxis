#Region ">>>>> COPYRIGHT AND COMPANY INFORMATION <<<<<"
' *  
' * Copyright (c) 2006 by Identec Solutions.
' *
' * This Copyright notice may not be removed or modified without prior written consent of Identec Solutions.
' * Identec Solutions, reserves the right to modify this software without notice.
' * 
' *
' * IDENTEC Solutions, Inc.
' * www.identecsolutions.com                  
' *
' * IDENTEC SOLUTIONS Inc. grants you a nonexclusive copyright license to use all programming code examples 
' * from which you can generate similar function tailored to your own specific needs.
' *
' * All sample code is provided by IDENTEC SOLUTIONS Inc. for illustrative purposes only. 
' * These examples have not been thoroughly tested under all conditions. IDENTEC SOLUTIONS Inc., therefore, 
' * cannot guarantee or imply reliability, serviceability, or function of these programs.

' * All programs contained herein are provided to you "AS IS" without any warranties of any kind. 
' * The implied warranties of non-infringement, 
' * merchantability and fitness for a particular purpose are expressly disclaimed.

' ****************************************************************************************************************/
#End Region


#Region ">>>>> Readme <<<<<"
' * This example shows how to connect to an iCard and contact an IQ tag for temperature logging purposes.
' * The tag is specified from the command line arguments and works in two modes:
' *	1. If the tag is not logging it is set into a logging mode
' *	2. If the tag is is logging then the temperature log is read
' * 
' * Please note:
' *	- You can read the temperature log off of a tag with or without stopping the log first.
' *	- Starting the logger on a tag that is already logigng will clear the existing log and start a new one
' * 
' * To set the tag ID in the command line arguments in Visual Studio .NET, see the project "properties" 
' * under project menu (configuration properties, debugging, command line arguments).
' * 
#End Region



Imports System
Imports System.Collections
Imports IDENTEC.Readers
Imports IDENTEC.Tags
Imports IDENTEC.Tags.Logging


Module Module1

    Sub Main()
        Console.WriteLine("Type the number of an i-Q tag to contact for temperature information and then press ENTER:")
        Dim myTag As iQTag = New iQTag
        myTag.Label = Console.ReadLine()
        Dim myReader As iCard3 = New iCard3

        Try
            If myReader.Connect() Then
                If Not myReader.PingTag(myTag) Then
                    Console.WriteLine("Failed to contact tag. Reason: " + myReader.DeviceStatus)
                Else
                    Console.WriteLine("Tag info: ")
                    Console.WriteLine("ID: " + myTag.Label)
                    Console.WriteLine("Model: " + myTag.ModelType.ToString())
                    If (iQTag.LoggerInstalledState.Available = myTag.LoggerInstalled) Then
                        Console.WriteLine("Logging State: " + myTag.Logging.ToString())
                    End If
                    Console.WriteLine("Range State: " + myTag.Range.ToString())

                    If myTag.Logging = iQTag.LoggingState.Off Then
                        'start logging at 1 minute intervals
                        Dim ts As New TimeSpan(0, 0, 1, 0, 0)
                        Console.WriteLine("Starting the log. Please wait a moment......")
                        myReader.StartTagLogging(myTag, ts)
                    Else
                        If myTag.Logging = iQTag.LoggingState.On Then
                            Console.WriteLine("Reading the log. Please wait a moment......")
                            'Now read the temperature log
                            Dim tLog As TemperatureLogData = myReader.ReadTagTemperatureLog(myTag)
                            Console.WriteLine("Log contains: " + tLog.SampleCount.ToString() + " samples recording every " + tLog.LoggingInterval.Minutes.ToString() + " minutes.")
                            Console.WriteLine("Log started at: " + tLog.Start.ToString())
                            Console.WriteLine("Log ended at: " + tLog.End.ToString())
                            Console.Write("Would you like to see the samples (Y/N)? ")
                            Dim strInput As String = Console.ReadLine()
                            Dim target As String = "Yy"
                            Dim anyOf As Char() = target.ToCharArray()
                            If (strInput.IndexOfAny(anyOf) <> -1) Then
                                For Each sample As TemperatureLogSample In tLog.Samples
                                    Console.WriteLine(sample.DegreesCelsius.ToString("00.0") + "°C @ " + sample.SampleTime.ToString())
                                Next
                            End If
                        End If
                    End If
                End If
            Else
                Console.WriteLine("Error reading card: " + myReader.DeviceStatus)
            End If

        Catch ex As PartialTagCommunicationsException
            'TODO: Possible retries with different power settings as communications was poor or non-existent
            Console.WriteLine(ex.Message)

        Catch ex As TagHasNoLoggerException
            'TODO: Display a friendly reminder that the tag isn't capable of logging
            Console.WriteLine(ex.Message)

        Catch ex As Exception
            Console.WriteLine("An exception was thrown : " + ex.ToString())
        End Try
        Console.WriteLine("Press Enter to continue...")
        Console.ReadLine()
    End Sub

End Module
