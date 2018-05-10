Public Class DataLogger7502Report
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        Dim dt1 As Date = #5/30/2018#
        Dim dt2 As Date = #5/30/2016#
        FMS.Business.DataObjects.DataLoggerReport.Get7502DataLogger("auto19", dt1, dt2)
    End Sub
End Class