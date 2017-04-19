Public Class ResourseMgmtRptContent
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load 

        Dim reportname As String = Request.QueryString("Report")
        Me.ASPxDocumentViewer1.Report = GetReportFromName(reportname)

        If String.IsNullOrEmpty(Request.QueryString("autoFillParams")) Then Exit Sub

        ASPxDocumentViewer1.SettingsSplitter.ParametersPanelCollapsed = True
    End Sub
    Public Shared Function GetReportFromName(reportName As String) As DevExpress.XtraReports.UI.XtraReport

        Select Case reportName
            Case "VehicleListReport"
                Return New FMS.ReportLogic.VehicleListReport
            Case "DriversListReport"
                Return New FMS.ReportLogic.DriversListReport
            Case "UsersListReport"
                Return New FMS.ReportLogic.UsersListReport
            Case Else
                Return Nothing
        End Select
    End Function

End Class