Public Class NanoReportParamList
    Inherits System.Web.UI.UserControl


    Public Property Report As DevExpress.XtraReports.UI.XtraReport


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'ThisSession.SelectedReportName = "VehicleReport"

        If String.IsNullOrEmpty(FMS.Business.ThisSession.SelectedReportName) Then Exit Sub

        ' System.Threading.Thread.Sleep(1000)

        Report = ReportContent.GetReportFromName(FMS.Business.ThisSession.SelectedReportName)


        For Each p In Report.Parameters

            Dim x As NanoReportParam = Page.LoadControl("~\Controls\NanoReportParam.ascx")

            x.ReportParameter = p
            x.ID = Guid.NewGuid.ToString.Replace("-", "")

            mainDIV.Controls.Add(x)

        Next

        FMS.Business.ThisSession.SelectedReportName = String.Empty

    End Sub

End Class