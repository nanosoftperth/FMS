Public Class FMS_Simulation

    Public Property t As System.Threading.Thread

    Protected Overrides Sub OnStart(ByVal args() As String)
        ' Add code here to start your service. This method should set things
        ' in motion so your service can do its work.

        'System.Diagnostics.Debugger.Launch()

        t = New Threading.Thread(AddressOf FMS.Simulator.Worker.DoWork)

        t.Start()

    End Sub

    Protected Overrides Sub OnStop()
        ' Add code here to perform any tear-down necessary to stop your service.

        t.Abort()

    End Sub

End Class
