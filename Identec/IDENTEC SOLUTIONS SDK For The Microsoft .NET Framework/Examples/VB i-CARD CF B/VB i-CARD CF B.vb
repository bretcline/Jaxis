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
' * The implied warranties of non-infringement, merchantability and fitness for a particular purpose are expressly disclaimed.

' ****************************************************************************************************************/
#End Region


#Region ">>>>> Readme <<<<<"
'This example shows how to connect to an chain of i-PORT R2 unites and query for tags
#End Region


Imports System
Imports IDENTEC.Readers
Imports IDENTEC.Tags
Imports IDENTEC.Readers.BeaconReaders
Imports IDENTEC.Tags.BeaconTags


Module Module1

    Sub Main()

        'Notes:
        'You will need to obtain the i-CARD CF .inf file to enable the i-CARD CF com port connection on your PC.
        'This is usually performed by running the i-CARD setup installation package

        Try

            Console.WriteLine("----- IDENTEC SOLUTIONS Inc. i-CARD CF 'B' Sample -----" + vbLf + vbLf + vbLf)
            Dim port As Integer = CFReaderSearch.FindReaderComPort()
            If (port <= 0) Then
                Console.WriteLine("The CF Card could not be found.")
                Exit Sub
            End If

            Console.Write("Connecting to Com{0}: ...", port)
            Dim reader As iCardCFB
            reader.Connect(port)
            Console.WriteLine("Connected!")

            'Tell the reader to clear the tags out of its internal list when we ask for the list of tags:
            reader.SetTagListBehavior(TagListBehavior.RemoveTagsWhenReported)
            ' Turn on the receive amplifier to increase read range, 
            ' with it on we can "hear" tags at down to -90 dBm, with it off only at -60
            reader.EnableHighRfSensitivity(True)
            Dim nmV As Integer = reader.GetPowerSupplyVoltage()
            Console.WriteLine(vbLf + "{0, -10}" + vbTab + "{1,-10}" + vbTab + "{2, -20}", "Serial #", "Input Power", "Powered On")
            Console.WriteLine("{0, -10}" + vbTab + "t{1,-10}mV" + vbTab + "{2, -20}", reader.SerialNumber, nmV, reader.BootDateTime.ToString())

            Dim bContinue As Boolean = True

            While bContinue
                ' Get the tag extended info (first time and last time seen)							
                Console.WriteLine(vbLf + "Querying reader for its list of tags...")
                Dim tags As TagCollection = reader.GetTags(True)
                If (tags.Count = 0) Then
                    Console.WriteLine(vbLf + "No tags to report")
                Else
                    tags.Sort()
                    Console.WriteLine(vbLf + "{0} tags to report:" + vbLf, tags.Count)
                    Console.WriteLine(vbLf + "{0,-15} {1,-20} {2,-20} {3,-10} {4,-10}", "Tag", "First Detected", "Last Detected", "Max RSSI", "Last RSSI")
                    Console.WriteLine("----------------------------------------------------------------------------")
                    For Each t As iB2Tag In tags
                        Console.WriteLine("{0,-15} {1,-20:g} {2,-20:g} {3,-10} {4,-10}", t.Label, t.FirstSeen, t.ContactTime, t.MaxSignal, t.Signal)
                    Next
                End If

                Console.WriteLine(vbLf + vbLf)
                Console.Write("View tags seen on each reader again (Y/N)? Y: ")
                Dim strReponse As String = Console.ReadLine()
                If (strReponse.ToLower() = "n") Then
                    bContinue = False
                End If

            End While

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

        Console.WriteLine(vbLf + vbLf)
        Console.WriteLine("Press ENTER to start querying for tag lists" + vbLf + vbLf)
        Console.ReadLine()


    End Sub

End Module
