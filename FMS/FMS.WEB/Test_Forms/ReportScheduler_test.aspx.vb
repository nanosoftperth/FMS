Public Class ReportScheduler_test
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


    End Sub

    Private Sub callbackREportEdit_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles callbackREportEdit.Callback

        Dim param As String = e.Parameter

        'NanoReportParamList = New NanoReportParamList With {.Report = ReportContent.GetReportFromName(param)}
        'NanoReportParamList.Report = ReportContent.GetReportFromName(param)



    End Sub


End Class