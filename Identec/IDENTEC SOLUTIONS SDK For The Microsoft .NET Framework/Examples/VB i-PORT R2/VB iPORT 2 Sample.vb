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
Imports System.Net.Sockets



Module iPORTR2Sample

    Sub Main()

        'Notes:
        '				This example is designed to be used with a serial server over TCP such as a "Digi One SP" from Digi (www.digi.com).
        '				Please contact IDENTEC SOLUTIONS Inc. for a quick setup guide on using a Digi device with the i-PORT R2.
        '				Alternatively you could use a USB to RS-422 convertor that provides a com port (this would require a code change below)

        Try
            Console.WriteLine("----- IDENTEC SOLUTIONS Inc. i-PORT R2 Sample -----" + vbLf + vbLf + vbLf)
            Console.WriteLine("Please input the IP Address of serial server to connect to then press ENTER:")

            Dim strIP As String = Console.ReadLine()
            Console.WriteLine("Connecting to " + strIP + " ...")
            'Note: the default port for the Digi One SP is 2101
            Dim r2SerialServer As New IDENTEC.TCPSocketStream(strIP, 2101)
            r2SerialServer.Open()

            Console.WriteLine("Connected!")

            'TODO: set the socket options to match your network 
            '(for example if you are on a cellular connection with a digi device you'll want to increase these timeouts
            'Here we are setting the timeout to be reasonable for the device on a fast LAN:				
            r2SerialServer.ReadTimeout = New TimeSpan(0, 0, 10)
            r2SerialServer.WriteTimeout = New TimeSpan(0, 0, 10)

            '.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 10000)
            'r2SerialServer.Socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 10000)

            Dim BusAdapter As New IDENTEC.iBusAdapter(r2SerialServer)

            Console.WriteLine("Enumerating readers. Please wait...")
            ' Now start enumerating the readers connected to the serial server (on the daisy chain chain)
            ' We specify up to a 5 second wait for the first i-PORT R2 to respond (if there are no readers then it will take 5 seconds)
            Dim readers As IDENTEC.IBusDevice() = BusAdapter.EnumerateBusModules()
            If (0 = readers.Length) Then
                Console.WriteLine("There were no readers found on the serial server")
            Else
                Console.WriteLine("There were {0} readers found on the serial server" + vbLf + vbLf + vbLf, readers.Length)
                Console.WriteLine("Reader" + vbTab + "Serial #" + vbTab + "Input Power" + vbTab + vbTab + "Powered On")
                For Each r2 As iPortR2 In readers
                    'Tell the reader to clear the tags out of its internal list when we ask for the list of tags:
                    r2.SetTagListBehavior(TagListBehavior.RemoveTagsWhenReported)
                    'Turn on the receive amplifier to increase read range, 
                    ' with it on we can "hear" tags at down to -90 dBm, with it off only at -60
                    r2.EnableHighRfSensitivity(True)
                    Dim nmV As Integer = r2.GetPowerSupplyVoltage()
                    Console.WriteLine("{0}" + vbTab + "{1}" + vbTab + "{2}mV" + vbTab + vbTab + "{3}", r2.Address, r2.SerialNumber, nmV, r2.BootDateTime.ToString())
                Next
            End If


            Console.WriteLine(vbLf + vbLf)
            Console.WriteLine("Press ENTER to start querying for tag lists" + vbLf + vbLf)
            Console.ReadLine()

            Dim CanContinue As Boolean = True

            While CanContinue

                For Each r2 As iPortR2 In readers
                    'Get the tag extended info (first time and last time seen)
                    Console.WriteLine(vbLf + "Querying reader {0} for its list of tags...", r2.Address)
                    Dim tags As TagCollection = r2.GetTags(True)
                    If 0 = tags.Count Then
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
                Next
                Console.WriteLine(vbLf + vbLf)
                Console.Write("View tags seen on each reader again (Y/N)? Y: ")
                Dim strReponse As String = Console.ReadLine()
                If (strReponse.ToLower() = "n") Then
                    CanContinue = False
                End If
            End While


        Catch ex As Exception

            Console.WriteLine(ex.Message)
        End Try
        Console.WriteLine("Press ENTER to end the program")
        Console.ReadLine()

    End Sub

End Module
