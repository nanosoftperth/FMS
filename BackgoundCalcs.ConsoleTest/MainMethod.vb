Public Class MainMethod


    Public Shared Sub ExecuteInfinateLoop()

        While True

            Try

                main_method()

                Dim SECONDS_TO_WAIT As Integer = 5

                Dim s As String = "============================================================" & vbNewLine & _
                                  "    LOOP COMPLETE, STARTING NEW LOOP IN {0} SECONDS         " & vbNewLine & _
                                  "============================================================"

                LogMsg(s & vbNewLine, SECONDS_TO_WAIT)

                Threading.Thread.Sleep(TimeSpan.FromSeconds(SECONDS_TO_WAIT))

            Catch ex As Exception

            End Try
        End While

    End Sub

End Class
