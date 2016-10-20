Public Class MainMethod


    Public Shared Sub ExecuteInfinateLoop()

        While True

            ' LogMsg("Version 1.0.3 started")

            main_method()

            Dim SECONDS_TO_WAIT As Integer = 5

            Dim s As String = "============================================================" & vbNewLine & _
                              "    LOOP COMPLETE, STARTING NEW LOOP IN {0} SECONDS         " & vbNewLine & _
                              "============================================================"

            LogMsg(s & vbNewLine, SECONDS_TO_WAIT)

            Threading.Thread.Sleep(TimeSpan.FromSeconds(SECONDS_TO_WAIT))

        End While

    End Sub

End Class
