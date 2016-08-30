Imports DevExpress.XtraCharts.Web
Imports DevExpress.XtraCharts
Imports FMS.Business.DataObjects.FeatureListConstants

Public Class ReportGenerator
    Inherits System.Web.UI.Page

    Public Shared ReadOnly Property WebVersion As String
        Get
            Return My.Settings.version
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        UserAuthorisationCheck(FeatureListAccess.Report_Generator)

        If Not IsPostBack Then
            deTimeFrom.Value = New Date(Now.Year, Now.Month, Now.Day)
            deTimeTo.Value = New Date(Now.Year, Now.Month, Now.Day).AddDays(1).AddSeconds(-1)

            Me.chartSpeed.DataSource = Nothing
            Me.chartSpeed.DataSourceID = Nothing

        End If

        CType(Me.Master, FMS.WEB.MainLightMaster).HeaderText = "Report Generation"

    End Sub

    Protected Sub ASPxButton1_Click(sender As Object, e As EventArgs) Handles ASPxButton1.Click

        Dim vehicleid As Guid = New Guid(CStr(Me.ASPxComboBox1.Value))
        Dim startdate As Date = CDate(Me.deTimeFrom.Value)
        Dim enddate As Date = CDate(Me.deTimeTo.Value)

        Dim objs As FMS.Business.ReportGeneration.VehicleSpeedRetObj = _
                    FMS.Business.ReportGeneration.ReportGenerator.GetVehicleSpeedAndDistance_ForGraph(vehicleid, startdate, enddate)


        Dim series1 As Series = chartSpeed.Series(1)
        series1.Points.Clear()


        For Each o As FMS.Business.ReportGeneration.TimeSeriesFloat In objs.SpeedVals
            series1.Points.Add(New SeriesPoint(o.DateVal, New Double() {o.Val}))
        Next


        CType(chartSpeed.Diagram, DevExpress.XtraCharts.XYDiagram).AxisX.DateTimeScaleOptions.ScaleMode = ScaleMode.Continuous

        series1.Name = "speed"
        series1.ArgumentScaleType = ScaleType.DateTime
        series1.ValueScaleType = ScaleType.Numerical


        Dim series2 As Series = chartSpeed.Series(0)
        series2.Points.Clear()
        series2.Name = "distance"

        For Each o As FMS.Business.ReportGeneration.TimeSeriesFloat In objs.DistanceVals
            series2.Points.Add(New SeriesPoint(o.DateVal, New Double() {o.Val}))
        Next

        series2.ArgumentScaleType = ScaleType.DateTime
        series2.ValueScaleType = ScaleType.Numerical

    End Sub

End Class