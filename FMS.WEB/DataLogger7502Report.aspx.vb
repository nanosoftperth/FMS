Imports System.Drawing
Imports DevExpress.XtraCharts

Public Class DataLogger7502Report
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        Dim dt1 As Date = #6/18/2017#
        Dim dt2 As Date = #5/30/2017#
        'Dim dt1 As Date = #6/15/2017#
        'Dim dt2 As Date = #6/16/2017#
        WebChartControl1.DataSource = FMS.Business.DataObjects.DataLoggerReport.GetReportDirection("auto19", dt1, dt2)
        WebChartControl1.DataBind()

        'WebChartControl2.DataSource = FMS.Business.DataObjects.DataLoggerReport.Get7502DataLogger("auto19", dt1, dt2)
        'WebChartControl2.DataSource = FMS.Business.DataObjects.DataLoggerReport.GetReportDirection("auto19", dt1, dt2)
        'WebChartControl2.DataBind()

        'WebChartControl2.DataSource = FMS.Business.DataObjects.DataLoggerReport.GetReportDirection("auto19", dt1, dt2)
        'WebChartControl2.DataBind()

        'WebChartControl3.DataSource = FMS.Business.DataObjects.DataLoggerReport.GetReportDirection("auto19", dt1, dt2)
        'WebChartControl3.DataBind()
    End Sub

    Protected Sub WebChartControl3_CustomCallback(ByVal sender As Object, ByVal e As DevExpress.XtraCharts.Web.CustomCallbackEventArgs)
        'If e.Parameter = "MarkerKind" Then
        '    PerformMarkerKindAction()
        'ElseIf e.Parameter = "MarkerSize" Then
        '    PerformMarkerSizeAction()
        'ElseIf e.Parameter = "ShowLabels" Then
        '    PerformShowLabelsAction()
        'ElseIf e.Parameter = "ValueAsPercent" Then
        '    PerformValueAsPercentAction()
        'End If
    End Sub
    Private Sub PerformMarkerKindAction()
        'Dim item As MarkerKindItem = ComboBoxHelper.GetSelectedMarkerKindItem(cbMarkerKind)
        'Dim pointCount As Integer = 0
        'Dim starItem As StarMarkerKindItem = TryCast(item, StarMarkerKindItem)
        'If starItem IsNot Nothing Then
        '    pointCount = starItem.PointCount
        'End If

        'Dim view As PointSeriesView = CType(WebChartControl1.SeriesTemplate.View, PointSeriesView)
        'view.PointMarkerOptions.Kind = item.MarkerKind
        'If pointCount <> 0 Then
        '    view.PointMarkerOptions.StarPointCount = pointCount
        'End If
    End Sub
    Private Sub PerformMarkerSizeAction()
        'Dim size As Integer = Convert.ToInt32(cbMarkerSize.SelectedItem.Text)
        'CType(WebChartControl1.SeriesTemplate.View, PointSeriesView).PointMarkerOptions.Size = size
    End Sub
    Private Sub PerformShowLabelsAction()
        'WebChartControl1.SeriesTemplate.LabelsVisibility = If(cbShowLabels.Checked, DevExpress.Utils.DefaultBoolean.True, DevExpress.Utils.DefaultBoolean.False)
        'WebChartControl1.CrosshairEnabled = If(cbShowLabels.Checked, DevExpress.Utils.DefaultBoolean.False, DevExpress.Utils.DefaultBoolean.True)
    End Sub
    Private Sub PerformValueAsPercentAction()
        'Dim labeltextPattern As String = If(cbValueAsPercent.Checked, "{VP:P0}", "{V:F1}")
        'WebChartControl1.SeriesTemplate.Label.TextPattern = labeltextPattern
    End Sub

    Protected Sub WebChartControl1_CustomDrawSeriesPoint(sender As Object, e As DevExpress.XtraCharts.CustomDrawSeriesPointEventArgs)
        Dim drawOptions = CType(e.SeriesDrawOptions, BarDrawOptions)
        If drawOptions Is Nothing Then
            Return
        End If

        If (e.Series.Points.Owner.Tag.ToString().Split("-")(0).Equals("forward")) Then
            drawOptions.Color = Color.Red
        ElseIf (e.Series.Points.Owner.Tag.ToString().Split("-")(0).Equals("backward")) Then
            drawOptions.Color = Color.Green
        End If
        If (e.Series.Points.Owner.Tag.ToString().Split("-")(0).Equals("Service Brake On")) Then
            drawOptions.Color = Color.Red
        ElseIf (e.Series.Points.Owner.Tag.ToString().Split("-")(0).Equals("Service Brake Off")) Then
            drawOptions.Color = Color.Green
        End If
        If (e.Series.Points.Owner.Tag.ToString().Split("-")(0).Equals("Park Brake On")) Then
            drawOptions.Color = Color.Red
        ElseIf (e.Series.Points.Owner.Tag.ToString().Split("-")(0).Equals("Park Brake Off")) Then
            drawOptions.Color = Color.Green
        End If
        If (e.Series.Points.Owner.Tag.ToString().Split("-")(0).Equals("Horn On")) Then
            drawOptions.Color = Color.Red
        ElseIf (e.Series.Points.Owner.Tag.ToString().Split("-")(0).Equals("Horn Off")) Then
            drawOptions.Color = Color.Green
        End If
        If (e.Series.Points.Owner.Tag.ToString().Split("-")(0).Equals("Beacon is On")) Then
            drawOptions.Color = Color.Red
        ElseIf (e.Series.Points.Owner.Tag.ToString().Split("-")(0).Equals("Beacon is Off")) Then
            drawOptions.Color = Color.Green
        End If
        If (e.Series.Points.Owner.Tag.ToString().Split("-")(0).Equals("Road Rail Engaged On")) Then
            drawOptions.Color = Color.Red
        ElseIf (e.Series.Points.Owner.Tag.ToString().Split("-")(0).Equals("Road Rail Engaged Off")) Then
            drawOptions.Color = Color.Green
        End If

        'Dim legend As Legend = WebChartControl1.Legend

        '' Display the chart control's legend.
        'legend.Visibility = False

        ' Get the fill options for the series point.
        'drawOptions.FillStyle.FillMode = FillMode.Gradient
        'Dim options = CType(drawOptions.FillStyle.Options, RectangleGradientFillOptions)
        'If options Is Nothing Then
        '    Return
        'End If

        '' Get the value at the current series point.
        'Dim val As Double = e.SeriesPoint(0)

        '' If the value is less than 2.5, then fill the bar with green colors.
        'If val < 2.5 Then
        '    options.Color2 = Color.FromArgb(154, 196, 84)
        '    drawOptions.Color = Color.FromArgb(81, 137, 3)
        '    drawOptions.Border.Color = Color.FromArgb(100, 39, 91, 1)
        '    ' ... if the value is less than 5.5, then fill the bar with yellow colors.
        'Else
        '    If val < 5.5 Then
        '        options.Color2 = Color.FromArgb(254, 233, 124)
        '        drawOptions.Color = Color.FromArgb(249, 170, 15)
        '        drawOptions.Border.Color = Color.FromArgb(60, 165, 73, 5)
        '        ' ... if the value is greater, then fill the bar with red colors.
        '    Else
        '        options.Color2 = Color.FromArgb(242, 143, 112)
        '        drawOptions.Color = Color.FromArgb(199, 57, 12)
        '        drawOptions.Border.Color = Color.FromArgb(100, 155, 26, 0)
        '    End If
        'End If
    End Sub
End Class