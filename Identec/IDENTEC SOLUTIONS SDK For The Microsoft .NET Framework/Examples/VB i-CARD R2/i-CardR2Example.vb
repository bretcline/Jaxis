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
'This example shows how to connect to an i-Card R2 and respond to events.
#End Region


Imports System
Imports System.Threading
Imports IDENTEC.Readers
Imports IDENTEC.Tags
Imports IDENTEC.Readers.BeaconReaders
Imports IDENTEC.Tags.BeaconTags



Module R2Example

    Dim m_R2 As iCardR2
    Sub Main()
        m_R2 = New iCardR2
        AddHandler m_R2.TagBeacon, AddressOf iCardR2_TagBeaconEvent
        AddHandler m_R2.ErrorOccurred, AddressOf iCardR2_ErrorOccurred

        Try
            m_R2.Connect()
            m_R2.StartListening(True)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

        Console.WriteLine("To end program at any time, press Enter..." + vbCrLf + vbCrLf + vbCrLf)
        Console.ReadLine()
        m_R2.Disconnect()


    End Sub


    Sub iCardR2_TagBeaconEvent(ByVal sender As Object, ByVal e As TagBeaconEventArgs)
        Console.WriteLine("Tag " + e.tag.Label + " seen at " + e.tag.ContactTime.ToString())
    End Sub

    Sub iCardR2_ErrorOccurred(ByVal sender As Object, ByVal e As iCardR2ErrorEventArgs)
        Console.WriteLine(e.ex.Message)
    End Sub

End Module
