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

Imports System
Imports IDENTEC.Readers
Imports IDENTEC.Tags
Module Module1

    Sub Main()
        Dim myReader As iCard3 = New iCard3
        Dim myTag As iD2Tag = New iD2Tag
        Dim Input As String
        Console.WriteLine("i-CARD3 Connect and Ping i-D2 tag")
        Console.WriteLine("Type the number of an i-D2 tag to ping and then press ENTER:")
        Input = Console.ReadLine()
        If Input.Length > 0 Then
            myTag.Label = Input
            Try
                If myReader.Connect() Then
                    If Not myReader.PingTag(myTag) Then
                        Console.WriteLine("Failed to contact tag. Reason: " + myReader.DeviceStatus)
                    Else
                        Console.WriteLine("Tag info: ")
                        Console.WriteLine("ID: " + myTag.Label)
                        'above gives the dotted notation that matches the label on the tag. below would just give a plain number
                        'Console.WriteLine("ID: " + myTag.Number.ToString())

                        Console.WriteLine("Region: " + myTag.Region.ToString())
                        Console.WriteLine("Version: " + myTag.Version.ToString())
                        Console.WriteLine("Battery State: " + myTag.Battery.ToString())
                        Console.WriteLine("Maximum Data Storage: " + myTag.DataCapacity.ToString())
                    End If
                Else
                    Console.WriteLine("Error reading card: " + myReader.DeviceStatus)
                End If

            Catch ex As PartialTagCommunicationsException
                'here's where you could do retries with different power settings as communications was poor or non-existent
                Console.WriteLine(ex.Message)

            Catch ex As Exception
                Console.WriteLine("An exception was thrown : " + ex.ToString())

            Catch ex As FormatException
                Console.WriteLine()

            End Try
            Console.WriteLine("Press Enter to continue...")
            Console.ReadLine()
        End If  ' tag number was entered
    End Sub

End Module
