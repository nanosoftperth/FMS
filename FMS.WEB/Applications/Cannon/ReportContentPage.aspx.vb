Public Class ReportContentPage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        FMS.Business.ThisSession.ParameterValues = Request.QueryString("param")
        Dim reportname As String = Request.QueryString("Report")
        Me.ASPxDocumentViewer1.Report = GetReportFromName(reportname)

    End Sub
    Public Shared Function GetReportFromName(reportName As String) As DevExpress.XtraReports.UI.XtraReport

        Select Case reportName
            Case "IndustryListReport"
                Return New FMS.ReportLogic.IndustryListReport()
            Case Else
                Return Nothing
        End Select
    End Function

End Class