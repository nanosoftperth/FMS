Public Class axReportContentPage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Request.QueryString("param") Is Nothing Then
            FMS.Business.ThisSession.ParameterValues = Request.QueryString("param")
        End If

        Dim reportname As String = Request.QueryString("Report")
        Me.ASPxDocumentViewer1.Report = GetReportFromName(reportname)

    End Sub
    Public Shared Function GetReportFromName(reportName As String) As DevExpress.XtraReports.UI.XtraReport

        Select Case reportName
            Case "IndustryListReport"
                Return New FMS.ReportLogic.IndustryListReport()
            Case "ServiceListReport"
                Return New FMS.ReportLogic.ServiceListReport()
            Case "DriversLicenseExpiryReport"
                Return New FMS.ReportLogic.DriversLicenseExpiryReport()
            Case "ContractRenewalReport"
                Return New FMS.ReportLogic.ContractRenewalReport()
            Case "ServiceSummaryReport"
                Return New FMS.ReportLogic.ServiceSummaryReport()
            Case "LengthOfServiceReport"
                Return New FMS.ReportLogic.LengthOfServicesReport()
            Case "CustomerByCustZoneReport"
                Return New FMS.ReportLogic.CustomerByCustZoneReport()
            Case "CustomerContactDetailsReport"
                Return New FMS.ReportLogic.CustomerContactDetailsReport()
            Case "SitesWithNoContractsReport"
                Return New FMS.ReportLogic.SitesWithNoContractsReport()
            Case "InvoiceBasicCheckReport"
                Return New FMS.ReportLogic.InvoiceBasicCheckReport()
            Case "MYOBCustomerInvoiceReport"
                Return New FMS.ReportLogic.MYOBCustomerInvoiceReport()
            Case "GainsAndLossesReport"
                Return New FMS.ReportLogic.GainsAndLossesReport()
            Case "GainsAndLossesSummaryReport"
                Return New FMS.ReportLogic.GainsAndLossesSummaryReport()
            Case "GainsAndLossesPerAnnumReport"
                Return New FMS.ReportLogic.GainsAndLossesReportPerAnnumReport()
            Case "GainsAndLossesPerAnnumSummaryReport"
                Return New FMS.ReportLogic.GainsAndLossesReportPerAnnumSummaryReport()
            Case "StandardAuditReport"
                Return New FMS.ReportLogic.StandardAuditReport()
            Case "AuditContractReport"
                Return New FMS.ReportLogic.AuditContractReport()
            Case "AuditOfSiteDetailChangesReport"
                Return New FMS.ReportLogic.AuditOfSiteDetailReport()
            Case "SiteBySiteZoneReport"
                Return New FMS.ReportLogic.SitesBySiteZoneReport()
            Case "RunValuesReport"
                Return New FMS.ReportLogic.RunValuesReport()
            Case Else
                Return Nothing
        End Select
    End Function

End Class