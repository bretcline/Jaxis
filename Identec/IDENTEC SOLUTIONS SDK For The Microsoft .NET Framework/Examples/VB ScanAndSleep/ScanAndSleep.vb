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
'* This sample demonstrates how to scan for tags and put them to sleep.
'* Once a tag is in a sleep state it will not respond to any interrogations until it wakes up
'* after the specified timeout.
'* 
'* Note that this sample continues to scan for new tags until it can no longer
'* put any tags to sleep. Therefore it is a simple "max detection" routine.
#End Region


Imports System
Imports System.Collections
Imports IDENTEC.Readers
Imports IDENTEC.Tags
Module Module1

    Sub Main()
        Dim myReader As New iCard3
        Dim nTotalTagsSleeping As Integer

        Try
            If myReader.Connect() Then
                Dim nTemp As Integer
                While (True)
                    nTemp = ScanAndSleep(myReader)
                    If (nTemp = 0) Then
                        Exit While
                    Else
                        nTotalTagsSleeping += nTemp
                    End If
                End While

            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

        Console.WriteLine(nTotalTagsSleeping.ToString() + " tags now sleeping")
        Console.WriteLine("Press ENTER to continue...")
        Console.ReadLine()
    End Sub

    Function ScanAndSleep(ByVal myReader As iCard3) As Integer

        Dim nTagsSleeping As Integer
        ' Set the transmission power to be high so that we detect tags at a medium distance
        myReader.TxPowerIQ = 2
        ' Scan for up to 32 tags; no blink during scan
        Dim tags As TagCollection = myReader.ScanForIQTags(32, False)
        tags.Sort()
        Console.WriteLine(tags.Count.ToString() + " tags detected during scan:")
        ' Bump up the transmission power a bit in case the tag is moving away from the reader
        myReader.TxPowerIQ = 3
        For Each t As Tag In tags
            Try
                If myReader.SleepTag(t, 30) Then
                    nTagsSleeping += 1
                    Console.WriteLine("Tag " + t.Label + " now sleeping for 30 seconds")
                Else
                    Console.WriteLine("Could not sleep tag " + t.Label + ". Reason: " + myReader.DeviceStatus.ToString())
                End If

            Catch ex As iCardCommunicationsException
                Console.WriteLine("An error occured communicating with the iCard: " + ex.Message)
            Catch ex As Exception
                Console.WriteLine("An error occured: " + ex.Message)
            End Try
        Next
        ScanAndSleep = nTagsSleeping

    End Function




End Module
