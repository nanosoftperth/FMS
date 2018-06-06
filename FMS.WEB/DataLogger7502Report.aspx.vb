Imports System.Drawing
Imports DevExpress.XtraCharts

Public Class DataLogger7502Report
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        'Dim dt1 As Date = #6/18/2017#
        'Dim dt2 As Date = #5/30/2017#
        'Dim dt1 As Date = #6/15/2017 10:00:00 AM#
        'Dim dt2 As Date = #6/15/2017 11:00:00 AM#
        Dim dt1 As Date = #6/11/2017 11:00:00 AM#
        Dim dt2 As Date = #6/11/2017 01:00:00 PM#

        Dim x = FMS.Business.DataObjects.DataLoggerReport.GetLatLongLog("auto19", dt1, dt2)


        Me.Session("LatLang") = x
        WebChartControl1.DataSource = FMS.Business.DataObjects.DataLoggerReport.GetReportDirection("auto19", dt1, dt2)
        WebChartControl1.DataBind()

        WebChartControl2.DataSource = FMS.Business.DataObjects.DataLoggerReport.Get7502DataLogger("auto19", dt1, dt2)
        WebChartControl2.DataBind()
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
        If (e.Series.Points.Owner.Tag.ToString().Split("-")(0).Equals("Headlight On")) Then
            drawOptions.Color = Color.Red
        ElseIf (e.Series.Points.Owner.Tag.ToString().Split("-")(0).Equals("Headlight Off")) Then
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