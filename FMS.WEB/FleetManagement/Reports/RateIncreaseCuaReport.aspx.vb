Public Class RateIncreaseCuaReport
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Request.QueryString("param") Is Nothing Then
            FMS.Business.ThisSession.ParameterValues = Request.QueryString("param")
        End If
    End Sub

End Class