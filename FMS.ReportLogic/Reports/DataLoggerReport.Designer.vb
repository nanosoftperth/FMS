﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class DataLoggerReport
    Inherits DevExpress.XtraReports.UI.XtraReport

    'XtraReport overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Designer
    'It can be modified using the Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim XyDiagram1 As DevExpress.XtraCharts.XYDiagram = New DevExpress.XtraCharts.XYDiagram()
        Dim SplineSeriesView1 As DevExpress.XtraCharts.SplineSeriesView = New DevExpress.XtraCharts.SplineSeriesView()
        Dim Parameter1 As DevExpress.DataAccess.ObjectBinding.Parameter = New DevExpress.DataAccess.ObjectBinding.Parameter()
        Dim Parameter2 As DevExpress.DataAccess.ObjectBinding.Parameter = New DevExpress.DataAccess.ObjectBinding.Parameter()
        Dim Parameter3 As DevExpress.DataAccess.ObjectBinding.Parameter = New DevExpress.DataAccess.ObjectBinding.Parameter()
        Dim Parameter4 As DevExpress.DataAccess.ObjectBinding.Parameter = New DevExpress.DataAccess.ObjectBinding.Parameter()
        Dim XyDiagram2 As DevExpress.XtraCharts.XYDiagram = New DevExpress.XtraCharts.XYDiagram()
        Dim SecondaryAxisX1 As DevExpress.XtraCharts.SecondaryAxisX = New DevExpress.XtraCharts.SecondaryAxisX()
        Dim KeyColorColorizer1 As DevExpress.XtraCharts.KeyColorColorizer = New DevExpress.XtraCharts.KeyColorColorizer()
        Dim StackedBarSeriesView1 As DevExpress.XtraCharts.StackedBarSeriesView = New DevExpress.XtraCharts.StackedBarSeriesView()
        Dim Parameter5 As DevExpress.DataAccess.ObjectBinding.Parameter = New DevExpress.DataAccess.ObjectBinding.Parameter()
        Dim Parameter6 As DevExpress.DataAccess.ObjectBinding.Parameter = New DevExpress.DataAccess.ObjectBinding.Parameter()
        Dim Parameter7 As DevExpress.DataAccess.ObjectBinding.Parameter = New DevExpress.DataAccess.ObjectBinding.Parameter()
        Dim Parameter8 As DevExpress.DataAccess.ObjectBinding.Parameter = New DevExpress.DataAccess.ObjectBinding.Parameter()
        Dim DynamicListLookUpSettings1 As DevExpress.XtraReports.Parameters.DynamicListLookUpSettings = New DevExpress.XtraReports.Parameters.DynamicListLookUpSettings()
        Dim DynamicListLookUpSettings2 As DevExpress.XtraReports.Parameters.DynamicListLookUpSettings = New DevExpress.XtraReports.Parameters.DynamicListLookUpSettings()
        Dim DynamicListLookUpSettings3 As DevExpress.XtraReports.Parameters.DynamicListLookUpSettings = New DevExpress.XtraReports.Parameters.DynamicListLookUpSettings()
        Me.ObjectDataSource4 = New DevExpress.DataAccess.ObjectBinding.ObjectDataSource(Me.components)
        Me.ObjectDataSource3 = New DevExpress.DataAccess.ObjectBinding.ObjectDataSource(Me.components)
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.ReportHeaderBand1 = New DevExpress.XtraReports.UI.ReportHeaderBand()
        Me.XrPictureBox1 = New DevExpress.XtraReports.UI.XRPictureBox()
        Me.XrChart2 = New DevExpress.XtraReports.UI.XRChart()
        Me.ObjectDataSource2 = New DevExpress.DataAccess.ObjectBinding.ObjectDataSource(Me.components)
        Me.XrChart1 = New DevExpress.XtraReports.UI.XRChart()
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.DetailReportBand1 = New DevExpress.XtraReports.UI.DetailReportBand()
        Me.GroupHeaderBand1 = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.DetailBand1 = New DevExpress.XtraReports.UI.DetailBand()
        Me.DetailReport = New DevExpress.XtraReports.UI.DetailReportBand()
        Me.Detail1 = New DevExpress.XtraReports.UI.DetailBand()
        Me.DetailReport1 = New DevExpress.XtraReports.UI.DetailReportBand()
        Me.Detail2 = New DevExpress.XtraReports.UI.DetailBand()
        Me.DetailReport2 = New DevExpress.XtraReports.UI.DetailReportBand()
        Me.Detail3 = New DevExpress.XtraReports.UI.DetailBand()
        Me.ObjectDataSource1 = New DevExpress.DataAccess.ObjectBinding.ObjectDataSource(Me.components)
        Me.Title = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.DetailCaption1 = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.DetailData1 = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.DetailCaption3 = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.DetailData3 = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.DetailData3_Odd = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.DetailCaptionBackground3 = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.PageInfo = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.DeviceID = New DevExpress.XtraReports.Parameters.Parameter()
        Me.StartTime = New DevExpress.XtraReports.Parameters.Parameter()
        Me.ChartDate = New DevExpress.XtraReports.Parameters.Parameter()
        Me.EndTime = New DevExpress.XtraReports.Parameters.Parameter()
        CType(Me.ObjectDataSource4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ObjectDataSource3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrChart2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(XyDiagram1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(SplineSeriesView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ObjectDataSource2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrChart1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(XyDiagram2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(SecondaryAxisX1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(StackedBarSeriesView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ObjectDataSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'ObjectDataSource4
        '
        Me.ObjectDataSource4.DataMember = "GetDevicesByApplication"
        Me.ObjectDataSource4.DataSource = GetType(FMS.ReportLogic.ReportDataHandler)
        Me.ObjectDataSource4.Name = "ObjectDataSource4"
        '
        'ObjectDataSource3
        '
        Me.ObjectDataSource3.DataMember = "GetChartTimeList"
        Me.ObjectDataSource3.DataSource = GetType(FMS.ReportLogic.ReportDataHandler)
        Me.ObjectDataSource3.Name = "ObjectDataSource3"
        '
        'Detail
        '
        Me.Detail.HeightF = 0!
        Me.Detail.KeepTogether = True
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'TopMargin
        '
        Me.TopMargin.HeightF = 100.0!
        Me.TopMargin.Name = "TopMargin"
        Me.TopMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'BottomMargin
        '
        Me.BottomMargin.HeightF = 100.0!
        Me.BottomMargin.Name = "BottomMargin"
        Me.BottomMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'ReportHeaderBand1
        '
        Me.ReportHeaderBand1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrPictureBox1, Me.XrChart2, Me.XrChart1, Me.XrLabel1})
        Me.ReportHeaderBand1.HeightF = 861.2917!
        Me.ReportHeaderBand1.Name = "ReportHeaderBand1"
        '
        'XrPictureBox1
        '
        Me.XrPictureBox1.LocationFloat = New DevExpress.Utils.PointFloat(5.999979!, 612.9167!)
        Me.XrPictureBox1.Name = "XrPictureBox1"
        Me.XrPictureBox1.SizeF = New System.Drawing.SizeF(644.0!, 248.3749!)
        '
        'XrChart2
        '
        Me.XrChart2.BorderColor = System.Drawing.Color.Black
        Me.XrChart2.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrChart2.DataSource = Me.ObjectDataSource2
        XyDiagram1.AxisX.DateTimeScaleOptions.MeasureUnit = DevExpress.XtraCharts.DateTimeMeasureUnit.Minute
        XyDiagram1.AxisX.VisibleInPanesSerializable = "-1"
        XyDiagram1.AxisY.Title.Text = "Speed"
        XyDiagram1.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.[True]
        XyDiagram1.AxisY.VisibleInPanesSerializable = "-1"
        XyDiagram1.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.[False]
        XyDiagram1.DefaultPane.EnableAxisXZooming = DevExpress.Utils.DefaultBoolean.[False]
        XyDiagram1.DefaultPane.EnableAxisYScrolling = DevExpress.Utils.DefaultBoolean.[False]
        XyDiagram1.DefaultPane.EnableAxisYZooming = DevExpress.Utils.DefaultBoolean.[False]
        Me.XrChart2.Diagram = XyDiagram1
        Me.XrChart2.Legend.Name = "Default Legend"
        Me.XrChart2.Legend.Visibility = DevExpress.Utils.DefaultBoolean.[False]
        Me.XrChart2.LocationFloat = New DevExpress.Utils.PointFloat(5.999994!, 32.0!)
        Me.XrChart2.Name = "XrChart2"
        Me.XrChart2.SeriesDataMember = "LineValues.Description"
        Me.XrChart2.SeriesSerializable = New DevExpress.XtraCharts.Series(-1) {}
        Me.XrChart2.SeriesTemplate.ArgumentDataMember = "LineValues.SpeedDateTime"
        Me.XrChart2.SeriesTemplate.ValueDataMembersSerializable = "LineValues.Value"
        Me.XrChart2.SeriesTemplate.View = SplineSeriesView1
        Me.XrChart2.SizeF = New System.Drawing.SizeF(644.0!, 189.25!)
        '
        'ObjectDataSource2
        '
        Me.ObjectDataSource2.DataMember = "GetSpeedDataLoggerReport"
        Me.ObjectDataSource2.DataSource = GetType(FMS.ReportLogic.ReportDataHandler)
        Me.ObjectDataSource2.Name = "ObjectDataSource2"
        Parameter1.Name = "deviceID"
        Parameter1.Type = GetType(DevExpress.DataAccess.Expression)
        Parameter1.Value = New DevExpress.DataAccess.Expression("[Parameters.DeviceID]", GetType(String))
        Parameter2.Name = "chartDate"
        Parameter2.Type = GetType(DevExpress.DataAccess.Expression)
        Parameter2.Value = New DevExpress.DataAccess.Expression("[Parameters.ChartDate]", GetType(Date))
        Parameter3.Name = "startTime"
        Parameter3.Type = GetType(DevExpress.DataAccess.Expression)
        Parameter3.Value = New DevExpress.DataAccess.Expression("[Parameters.StartTime]", GetType(String))
        Parameter4.Name = "endTime"
        Parameter4.Type = GetType(DevExpress.DataAccess.Expression)
        Parameter4.Value = New DevExpress.DataAccess.Expression("[Parameters.EndTime]", GetType(String))
        Me.ObjectDataSource2.Parameters.AddRange(New DevExpress.DataAccess.ObjectBinding.Parameter() {Parameter1, Parameter2, Parameter3, Parameter4})
        '
        'XrChart1
        '
        Me.XrChart1.BorderColor = System.Drawing.Color.Black
        Me.XrChart1.Borders = DevExpress.XtraPrinting.BorderSide.None
        XyDiagram2.AxisX.VisibleInPanesSerializable = "-1"
        XyDiagram2.AxisY.VisibleInPanesSerializable = "-1"
        XyDiagram2.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.[False]
        XyDiagram2.DefaultPane.EnableAxisXZooming = DevExpress.Utils.DefaultBoolean.[False]
        XyDiagram2.DefaultPane.EnableAxisYScrolling = DevExpress.Utils.DefaultBoolean.[False]
        XyDiagram2.DefaultPane.EnableAxisYZooming = DevExpress.Utils.DefaultBoolean.[False]
        XyDiagram2.Rotated = True
        SecondaryAxisX1.AxisID = 0
        SecondaryAxisX1.Name = "Secondary AxisX 1"
        SecondaryAxisX1.Reverse = True
        SecondaryAxisX1.VisibleInPanesSerializable = "-1"
        XyDiagram2.SecondaryAxesX.AddRange(New DevExpress.XtraCharts.SecondaryAxisX() {SecondaryAxisX1})
        Me.XrChart1.Diagram = XyDiagram2
        Me.XrChart1.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.[True]
        Me.XrChart1.Legend.Direction = DevExpress.XtraCharts.LegendDirection.BottomToTop
        Me.XrChart1.Legend.Name = "Default Legend"
        Me.XrChart1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.[False]
        Me.XrChart1.LocationFloat = New DevExpress.Utils.PointFloat(5.999994!, 221.25!)
        Me.XrChart1.Name = "XrChart1"
        Me.XrChart1.PaletteRepository.Add("Palette 1", New DevExpress.XtraCharts.Palette("Palette 1", DevExpress.XtraCharts.PaletteScaleMode.Repeat, New DevExpress.XtraCharts.PaletteEntry() {New DevExpress.XtraCharts.PaletteEntry(System.Drawing.Color.Red, System.Drawing.Color.Red), New DevExpress.XtraCharts.PaletteEntry(System.Drawing.Color.Green, System.Drawing.Color.Green)}))
        Me.XrChart1.SeriesDataMember = "LineValues.Direction"
        Me.XrChart1.SeriesSerializable = New DevExpress.XtraCharts.Series(-1) {}
        Me.XrChart1.SeriesTemplate.ArgumentDataMember = "LineValues.Description"
        Me.XrChart1.SeriesTemplate.ColorDataMember = "LineValues.Direction"
        KeyColorColorizer1.PaletteName = "Palette 1"
        Me.XrChart1.SeriesTemplate.Colorizer = KeyColorColorizer1
        Me.XrChart1.SeriesTemplate.SeriesPointsSortingKey = DevExpress.XtraCharts.SeriesPointKey.Value_1
        Me.XrChart1.SeriesTemplate.ValueDataMembersSerializable = "LineValues.Value"
        Me.XrChart1.SeriesTemplate.View = StackedBarSeriesView1
        Me.XrChart1.SizeF = New System.Drawing.SizeF(644.0!, 391.6667!)
        '
        'XrLabel1
        '
        Me.XrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(6.0!, 6.0!)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel1.SizeF = New System.Drawing.SizeF(638.0!, 26.0!)
        Me.XrLabel1.StyleName = "Title"
        Me.XrLabel1.Text = "Report Title"
        '
        'DetailReportBand1
        '
        Me.DetailReportBand1.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.GroupHeaderBand1, Me.DetailBand1, Me.DetailReport, Me.DetailReport1, Me.DetailReport2})
        Me.DetailReportBand1.DataMember = "LineValues"
        Me.DetailReportBand1.DataSource = Me.ObjectDataSource1
        Me.DetailReportBand1.Level = 0
        Me.DetailReportBand1.Name = "DetailReportBand1"
        '
        'GroupHeaderBand1
        '
        Me.GroupHeaderBand1.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail
        Me.GroupHeaderBand1.HeightF = 0!
        Me.GroupHeaderBand1.Name = "GroupHeaderBand1"
        '
        'DetailBand1
        '
        Me.DetailBand1.HeightF = 0!
        Me.DetailBand1.Name = "DetailBand1"
        '
        'DetailReport
        '
        Me.DetailReport.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail1})
        Me.DetailReport.DataMember = "LineValues"
        Me.DetailReport.DataSource = Me.ObjectDataSource2
        Me.DetailReport.Level = 0
        Me.DetailReport.Name = "DetailReport"
        '
        'Detail1
        '
        Me.Detail1.HeightF = 0!
        Me.Detail1.Name = "Detail1"
        '
        'DetailReport1
        '
        Me.DetailReport1.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail2})
        Me.DetailReport1.DataSource = Me.ObjectDataSource3
        Me.DetailReport1.Level = 1
        Me.DetailReport1.Name = "DetailReport1"
        '
        'Detail2
        '
        Me.Detail2.HeightF = 0!
        Me.Detail2.Name = "Detail2"
        '
        'DetailReport2
        '
        Me.DetailReport2.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail3})
        Me.DetailReport2.DataSource = Me.ObjectDataSource4
        Me.DetailReport2.Level = 2
        Me.DetailReport2.Name = "DetailReport2"
        '
        'Detail3
        '
        Me.Detail3.HeightF = 100.0!
        Me.Detail3.Name = "Detail3"
        '
        'ObjectDataSource1
        '
        Me.ObjectDataSource1.DataMember = "GetDataLoggerReport"
        Me.ObjectDataSource1.DataSource = GetType(FMS.ReportLogic.ReportDataHandler)
        Me.ObjectDataSource1.Name = "ObjectDataSource1"
        Parameter5.Name = "deviceID"
        Parameter5.Type = GetType(DevExpress.DataAccess.Expression)
        Parameter5.Value = New DevExpress.DataAccess.Expression("[Parameters.DeviceID]", GetType(String))
        Parameter6.Name = "chartDate"
        Parameter6.Type = GetType(DevExpress.DataAccess.Expression)
        Parameter6.Value = New DevExpress.DataAccess.Expression("[Parameters.ChartDate]", GetType(Date))
        Parameter7.Name = "startTime"
        Parameter7.Type = GetType(DevExpress.DataAccess.Expression)
        Parameter7.Value = New DevExpress.DataAccess.Expression("[Parameters.StartTime]", GetType(String))
        Parameter8.Name = "endTime"
        Parameter8.Type = GetType(DevExpress.DataAccess.Expression)
        Parameter8.Value = New DevExpress.DataAccess.Expression("[Parameters.EndTime]", GetType(String))
        Me.ObjectDataSource1.Parameters.AddRange(New DevExpress.DataAccess.ObjectBinding.Parameter() {Parameter5, Parameter6, Parameter7, Parameter8})
        '
        'Title
        '
        Me.Title.BackColor = System.Drawing.Color.Transparent
        Me.Title.BorderColor = System.Drawing.Color.Black
        Me.Title.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.Title.BorderWidth = 1.0!
        Me.Title.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.Title.ForeColor = System.Drawing.Color.FromArgb(CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.Title.Name = "Title"
        '
        'DetailCaption1
        '
        Me.DetailCaption1.BackColor = System.Drawing.Color.FromArgb(CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.DetailCaption1.BorderColor = System.Drawing.Color.White
        Me.DetailCaption1.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.DetailCaption1.BorderWidth = 2.0!
        Me.DetailCaption1.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.DetailCaption1.ForeColor = System.Drawing.Color.White
        Me.DetailCaption1.Name = "DetailCaption1"
        Me.DetailCaption1.Padding = New DevExpress.XtraPrinting.PaddingInfo(6, 6, 0, 0, 100.0!)
        Me.DetailCaption1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'DetailData1
        '
        Me.DetailData1.BackColor = System.Drawing.Color.Transparent
        Me.DetailData1.BorderColor = System.Drawing.Color.Transparent
        Me.DetailData1.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.DetailData1.BorderWidth = 2.0!
        Me.DetailData1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.DetailData1.ForeColor = System.Drawing.Color.Black
        Me.DetailData1.Name = "DetailData1"
        Me.DetailData1.Padding = New DevExpress.XtraPrinting.PaddingInfo(6, 6, 0, 0, 100.0!)
        Me.DetailData1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'DetailCaption3
        '
        Me.DetailCaption3.BackColor = System.Drawing.Color.Transparent
        Me.DetailCaption3.BorderColor = System.Drawing.Color.Transparent
        Me.DetailCaption3.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.DetailCaption3.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.DetailCaption3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.DetailCaption3.Name = "DetailCaption3"
        Me.DetailCaption3.Padding = New DevExpress.XtraPrinting.PaddingInfo(6, 6, 0, 0, 100.0!)
        Me.DetailCaption3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'DetailData3
        '
        Me.DetailData3.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.DetailData3.ForeColor = System.Drawing.Color.Black
        Me.DetailData3.Name = "DetailData3"
        Me.DetailData3.Padding = New DevExpress.XtraPrinting.PaddingInfo(6, 6, 0, 0, 100.0!)
        Me.DetailData3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'DetailData3_Odd
        '
        Me.DetailData3_Odd.BackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(231, Byte), Integer))
        Me.DetailData3_Odd.BorderColor = System.Drawing.Color.Transparent
        Me.DetailData3_Odd.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.DetailData3_Odd.BorderWidth = 1.0!
        Me.DetailData3_Odd.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.DetailData3_Odd.ForeColor = System.Drawing.Color.Black
        Me.DetailData3_Odd.Name = "DetailData3_Odd"
        Me.DetailData3_Odd.Padding = New DevExpress.XtraPrinting.PaddingInfo(6, 6, 0, 0, 100.0!)
        Me.DetailData3_Odd.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'DetailCaptionBackground3
        '
        Me.DetailCaptionBackground3.BackColor = System.Drawing.Color.Transparent
        Me.DetailCaptionBackground3.BorderColor = System.Drawing.Color.FromArgb(CType(CType(206, Byte), Integer), CType(CType(206, Byte), Integer), CType(CType(206, Byte), Integer))
        Me.DetailCaptionBackground3.Borders = DevExpress.XtraPrinting.BorderSide.Top
        Me.DetailCaptionBackground3.BorderWidth = 2.0!
        Me.DetailCaptionBackground3.Name = "DetailCaptionBackground3"
        '
        'PageInfo
        '
        Me.PageInfo.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.PageInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.PageInfo.Name = "PageInfo"
        Me.PageInfo.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        '
        'DeviceID
        '
        Me.DeviceID.Description = "Device ID"
        DynamicListLookUpSettings1.DataAdapter = Nothing
        DynamicListLookUpSettings1.DataMember = Nothing
        DynamicListLookUpSettings1.DataSource = Me.ObjectDataSource4
        DynamicListLookUpSettings1.DisplayMember = "DeviceValue"
        DynamicListLookUpSettings1.ValueMember = "DeviceValue"
        Me.DeviceID.LookUpSettings = DynamicListLookUpSettings1
        Me.DeviceID.Name = "DeviceID"
        '
        'StartTime
        '
        Me.StartTime.Description = "Start Time"
        DynamicListLookUpSettings2.DataAdapter = Nothing
        DynamicListLookUpSettings2.DataMember = Nothing
        DynamicListLookUpSettings2.DataSource = Me.ObjectDataSource3
        DynamicListLookUpSettings2.DisplayMember = "TimeValue"
        DynamicListLookUpSettings2.ValueMember = "TimeValue"
        Me.StartTime.LookUpSettings = DynamicListLookUpSettings2
        Me.StartTime.Name = "StartTime"
        '
        'ChartDate
        '
        Me.ChartDate.Description = "Chart Date"
        Me.ChartDate.Name = "ChartDate"
        Me.ChartDate.Type = GetType(Date)
        Me.ChartDate.ValueInfo = "2018-06-06"
        '
        'EndTime
        '
        Me.EndTime.Description = "End Time"
        DynamicListLookUpSettings3.DataAdapter = Nothing
        DynamicListLookUpSettings3.DataMember = Nothing
        DynamicListLookUpSettings3.DataSource = Me.ObjectDataSource3
        DynamicListLookUpSettings3.DisplayMember = "TimeValue"
        DynamicListLookUpSettings3.ValueMember = "TimeValue"
        Me.EndTime.LookUpSettings = DynamicListLookUpSettings3
        Me.EndTime.Name = "EndTime"
        '
        'DataLoggerReport
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.ReportHeaderBand1, Me.DetailReportBand1})
        Me.ComponentStorage.AddRange(New System.ComponentModel.IComponent() {Me.ObjectDataSource1, Me.ObjectDataSource2, Me.ObjectDataSource3, Me.ObjectDataSource4})
        Me.DataSource = Me.ObjectDataSource1
        Me.Parameters.AddRange(New DevExpress.XtraReports.Parameters.Parameter() {Me.DeviceID, Me.ChartDate, Me.StartTime, Me.EndTime})
        Me.StyleSheet.AddRange(New DevExpress.XtraReports.UI.XRControlStyle() {Me.Title, Me.DetailCaption1, Me.DetailData1, Me.DetailCaption3, Me.DetailData3, Me.DetailData3_Odd, Me.DetailCaptionBackground3, Me.PageInfo})
        Me.Version = "17.2"
        CType(Me.ObjectDataSource4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ObjectDataSource3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(XyDiagram1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(SplineSeriesView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrChart2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ObjectDataSource2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(SecondaryAxisX1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(XyDiagram2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(StackedBarSeriesView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrChart1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ObjectDataSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents ObjectDataSource1 As DevExpress.DataAccess.ObjectBinding.ObjectDataSource
    Friend WithEvents ReportHeaderBand1 As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents DetailReportBand1 As DevExpress.XtraReports.UI.DetailReportBand
    Friend WithEvents GroupHeaderBand1 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents DetailBand1 As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents Title As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents DetailCaption1 As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents DetailData1 As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents DetailCaption3 As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents DetailData3 As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents DetailData3_Odd As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents DetailCaptionBackground3 As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents PageInfo As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents XrChart1 As DevExpress.XtraReports.UI.XRChart
    Friend WithEvents ObjectDataSource2 As DevExpress.DataAccess.ObjectBinding.ObjectDataSource
    Friend WithEvents DetailReport As DevExpress.XtraReports.UI.DetailReportBand
    Friend WithEvents Detail1 As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents XrChart2 As DevExpress.XtraReports.UI.XRChart
    Friend WithEvents XrPictureBox1 As DevExpress.XtraReports.UI.XRPictureBox
    Friend WithEvents DetailReport1 As DevExpress.XtraReports.UI.DetailReportBand
    Friend WithEvents Detail2 As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents ObjectDataSource3 As DevExpress.DataAccess.ObjectBinding.ObjectDataSource
    Friend WithEvents DeviceID As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents StartTime As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents ChartDate As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents DetailReport2 As DevExpress.XtraReports.UI.DetailReportBand
    Friend WithEvents Detail3 As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents ObjectDataSource4 As DevExpress.DataAccess.ObjectBinding.ObjectDataSource
    Friend WithEvents EndTime As DevExpress.XtraReports.Parameters.Parameter
End Class
