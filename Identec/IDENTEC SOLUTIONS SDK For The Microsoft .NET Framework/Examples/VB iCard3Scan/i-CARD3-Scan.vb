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
' This sample demonstrates how to connect to an iCard, set the transmission power and then scan for tags.
' The IDs of the tags detected are then displayed.
#End Region

Imports System
Imports IDENTEC.Readers
Imports IDENTEC.Tags

Module Module1

    Sub Main()

        Dim myReader As New iCard3
        Try
            If myReader.Connect() Then
                ' Set the transmission power to be high so that we detect tags at a fair distance
                myReader.TxPowerIQ = 6
                ' Scan for up to 64 tags; allow them to blink when they respond
                Dim tags As TagCollection = myReader.ScanForIQTags(64, True)
                ' Sort the tags numerically
                tags.Sort()
                Console.WriteLine(tags.Count.ToString() + " tags detected during scan:")
                For Each t As Tag In tags
                    Console.WriteLine(t.Label)
                Next

            End If
        Catch ex As Exception
            Console.WriteLine(ex)
        End Try

        Console.WriteLine("Press ENTER to continue...")
        Console.ReadLine()

    End Sub

End Module
