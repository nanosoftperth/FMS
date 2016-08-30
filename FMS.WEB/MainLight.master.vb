Public Class MainLightMaster
    Inherits MainMaster


    Public Property HeaderText As String

    Private Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.ASPxRoundPanel1.HeaderText = HeaderText
    End Sub

End Class