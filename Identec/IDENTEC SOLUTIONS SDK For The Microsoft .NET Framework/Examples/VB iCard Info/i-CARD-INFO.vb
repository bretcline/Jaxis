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
'This sample demonstrates how to display information from an iCard.
#End Region


Imports System
Imports IDENTEC.Readers
Imports IDENTEC.Tags

Module Module1

    Sub Main()

        Dim myReader As New iCard3

        Try
            If myReader.Connect() Then
                Console.WriteLine("i-Card Information: " + myReader.Information)
                Console.WriteLine("Serial Number: " + myReader.SerialNumber)
                Console.WriteLine("Production Info (Year/Week/Batch): " + myReader.ProductionInformation.Year.ToString() + "//" + myReader.ProductionInformation.Week.ToString() + "/" + myReader.ProductionInformation.ProductionNumber.ToString())
                Console.WriteLine("Region: " + myReader.Region.ToString())
                Console.WriteLine("Current Transmission Power for i-Q tag communications: " + myReader.TxPowerIQ.ToString() + "dBm")
                Console.WriteLine("Current Transmission Power for i-D2 tag communications: " + myReader.TxPowerID.ToString() + "dBm")
            End If
        Catch ex As Exception
            Console.WriteLine("An exception was thrown : " + ex.ToString())
        End Try
        Console.WriteLine("Press Enter to continue...")
        Console.ReadLine()


    End Sub

End Module
