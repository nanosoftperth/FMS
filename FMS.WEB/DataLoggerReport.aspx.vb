Imports FMS.Business

Public Class DataLoggerReport
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CType(Me.Master, FMS.WEB.MainLightMaster).HeaderText = "AS7502 Compliance Report"
    End Sub
End Class