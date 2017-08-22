Public Class CanBusDisplayProperty
    Inherits System.Web.UI.Page

    Public ReadOnly Property DeviceID
        Get
            Return Request.QueryString("deviceid")
        End Get
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

End Class