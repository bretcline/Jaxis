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
' This example shows how to connect to an iCard and contact an IQ tag 
' without scanning first and then displays the tag's state information.
#End Region


Imports System
Imports IDENTEC.Readers
Imports IDENTEC.Tags
Module Module1

    Sub Main()
        Console.WriteLine("Type the number of an i-Q tag to ping and then press ENTER:")
        Dim myTag As iQTag = New iQTag
        myTag.Label = Console.ReadLine()
        Dim myReader As iCard3 = New iCard3
        Try
            If myReader.Connect() Then
                If myReader.PingTag(myTag) Then
                    Console.WriteLine("Tag info: ")
                    Console.WriteLine("ID: " + myTag.Number.ToString())
                    Console.WriteLine("Model: " + myTag.ModelType.ToString())
                    If (iQTag.LoggerInstalledState.Available = myTag.LoggerInstalled) Then
                        Console.WriteLine("Logging State: " + myTag.Logging.ToString())
                    End If
                    Console.WriteLine("Range State: " + myTag.Range.ToString())
                    If myTag.ReportsBatteryPercentConsumed Then
                        Console.WriteLine(myTag.BatteryPercentConsumed.ToString() + "% of battery consumed")
                    End If
                End If
            Else
                Console.WriteLine("Error reading card: " + myReader.DeviceStatus)
            End If

        Catch ex As Exception
            Console.WriteLine("An exception was thrown : " + ex.ToString())
        End Try
        Console.WriteLine("Press Enter to continue...")
        Console.ReadLine()
    End Sub

End Module
