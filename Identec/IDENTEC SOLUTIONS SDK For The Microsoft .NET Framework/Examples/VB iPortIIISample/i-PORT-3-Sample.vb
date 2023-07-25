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
' This example shows how to connect to an iPORT 3, set power settings, and scan on individual antennas.
#End Region


Imports System
Imports System.Threading
Imports IDENTEC.Readers
Imports IDENTEC.Tags

Module iPortIIISample

    Sub Main()
        'Note: Your iPORT III must be set up for use with this SDK
        'On the "General" page scroll to the bottom and ensure that the custom application is switched off
        'On the "Configuration" page ensure that the iPort Type is set to "host" under the "iPort General"

        Try
            Dim myReader As New iPort3
            'If you want to have a receive timeout, you can set it as such
            'myReader.tcpClient.ReceiveTimeout = 10000


            Console.WriteLine("Please input the IP Address of the i-PORT and then press ENTER:")
            Dim strIP As String = Console.ReadLine()
            Console.WriteLine("Connecting to " + strIP + " ...")
            myReader.Connect(strIP, 7070)

            'Set max power on each antenna; and disable antenna:
            'Note that antenna 5 is the W antenna:
            For i As Integer = 1 To 5
                myReader.SetTxPowerForIQTags(i, 6)
                myReader.EnableAntenna(i, False) 'disable the antenna for now:
                'TODO: possibly change the receive settings and threshold etc. For now the default values will do
            Next


            'iterate through the 4 normal antennas:
            For i As Integer = 1 To 4
                Console.WriteLine("Scanning on antenna " + i.ToString() + "...")
                myReader.ActiveAntenna = i
                myReader.EnableAntenna(i, True)
                Dim tags As TagCollection = myReader.ScanForIQTags(128, False)
                tags.Sort()

                Console.WriteLine(tags.Count.ToString() + " tags detected on antenna " + i.ToString())

                For Each t As Tag In tags
                    Console.WriteLine(t.Label + vbTab + " @ " + t.GetSignalStrength(i).ToString() + "dBm")
                Next

                Console.WriteLine("")
                'disable the antenna
                myReader.EnableAntenna(i, False)

            Next

            'Now scan on all antenna simultaneously; enabling the wakeup antenna as well:
            myReader.EnableAllAntennas(True)

            'Set active antenna to all:
            myReader.ActiveAntenna = 0
            Console.WriteLine("Scanning on all antennas simultaneously...")
            Dim nStart As Integer = Environment.TickCount
            Dim myTags As TagCollection = myReader.ScanForIQTags(512, False)
            Dim nEnd As Integer = Environment.TickCount
            Dim ts As TimeSpan = New TimeSpan(0, 0, 0, 0, nEnd - nStart)
            myTags.Sort()
            Console.WriteLine(myTags.Count.ToString() + " tags detected while scanning all antenna simultaneously in a time of " + ts.ToString())
            Console.WriteLine("Tag #" + vbTab + vbTab + "Ant1" + vbTab + "Ant2" + vbTab + "Ant3" + vbTab + "Ant4")
            For Each t As Tag In myTags
                Console.WriteLine(t.Label + vbTab + t.GetSignalStrength(1).ToString() + vbTab + t.GetSignalStrength(2).ToString() + vbTab + t.GetSignalStrength(3).ToString() + vbTab + t.GetSignalStrength(4).ToString())
            Next






        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

        Console.WriteLine("Press ENTER to continue...")
        Console.ReadLine()

    End Sub

End Module
