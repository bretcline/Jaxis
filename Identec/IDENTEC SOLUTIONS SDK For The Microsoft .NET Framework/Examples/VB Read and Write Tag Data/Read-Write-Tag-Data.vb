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
'This example shows how to use an iCard to connect, scan, and read/write user information on tags.
#End Region


Imports System
Imports System.Collections
Imports IDENTEC.Readers
Imports IDENTEC.Tags
Module Module1

    Sub Main()
        Dim myReader As New iCard3
        'Since we are doing a lot of communications, set the card retries high
        myReader.Retries = 4
        'Set the transmission power to be high
        myReader.TxPowerIQ = 6
        Try
            If myReader.Connect() Then
                'Scan for up to 32 tags; no blink during scan
                Dim tags As TagCollection = myReader.ScanForIQTags(32, False)
                'Sort the tags numerically
                tags.Sort()
                Dim phrases As New ArrayList
                phrases.Add("You say yes")
                phrases.Add("I say no")
                phrases.Add("You say stop")
                phrases.Add("And I say go go go")
                phrases.Add("You say goodbye")
                phrases.Add("And I say hello")
                phrases.Add("Hello hello")
                Dim rand As New Random
                Dim successfulTags As New TagCollection
                For Each t As iQTag In tags
                    If phrases.Count <> 0 Then
                        Try
                            Dim index As Integer = rand.Next(0, phrases.Count - 1)
                            Dim writeResult As TagWriteDataResult = myReader.WriteTagDataString(t, 200, phrases.Item(index))
                            If (writeResult.Success) Then
                                'remove the phrase so that each tag has something unique to say
                                phrases.RemoveAt(index)
                                successfulTags.Add(t)
                            End If
                        Catch ex As iCardCommunicationsException
                            Console.WriteLine("An error occured communicating with the iCard: " + ex.Message)
                        Catch ex As Exception
                            Console.WriteLine("An error occured: " + ex.Message)
                        End Try
                    End If
                Next

                For Each t As iQTag In successfulTags
                    Dim readResult As TagReadStringResult = myReader.ReadTagDataString(t, 200)
                    If readResult.Success Then
                        Console.WriteLine(t.ToString() + ": " + readResult.Text)
                    End If
                Next
            End If
        Catch ex As Exception
            Console.WriteLine(ex)
        End Try
        Console.WriteLine("Press ENTER to continue...")
        Console.ReadLine()
    End Sub

End Module
