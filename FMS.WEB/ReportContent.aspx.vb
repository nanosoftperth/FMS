Public Class ReportContent
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim reportname As String = Request.QueryString("Report")
        Me.ASPxDocumentViewer1.Report = GetReportFromName(reportname)

        'BY RYAN: change column labels 
        If reportname = "DriverOperatingHoursReport" Then
            With CType(ASPxDocumentViewer1.Report, DriverOperatingHoursReport)
                .ArrivalCell.Text = ThisSession.ApplicationName + " H.Q. Arrival"
                .DepartureCell.Text = ThisSession.ApplicationName + " H.Q. Departure"
            End With
        End If

        'TODO: what was this for?
        'if this is not going to automatically fill parameters, then exit sub here 
        If String.IsNullOrEmpty(Request.QueryString("autoFillParams")) Then Exit Sub


        'depending on the report, get different settings from the query string 
        Select Case reportname

            Case "VehicleReport"

                Dim vehicleIDStr As String = Request.QueryString("vehicleid")
                'paramaters: startdate,enddate,vehicle
                Dim thisMorning As Date = New Date(Now.Year, Now.Month, Now.Day)
                Dim midnightTonight As Date = thisMorning.AddDays(1)

                Dim vehicleName As String = FMS.Business.DataObjects.ApplicationVehicle.GetForID(New Guid(vehicleIDStr)).Name

                ASPxDocumentViewer1.ToolbarMode = DevExpress.XtraReports.Web.DocumentViewer.DocumentViewerToolbarMode.Ribbon

                With CType(ASPxDocumentViewer1.Report, VehicleReport)
                    .parameter1.Value = thisMorning
                    .parameter2.Value = midnightTonight
                    .Parameter3.Value = vehicleName

                    '.parameter1.Visible = False
                    '.parameter2.Visible = False
                    '.Parameter3.Visible = False

                    '.ObjectDataSource1.DataSource = ReportDataHandler.GetVehicleReportValues(thisMorning, midnightTonight, vehicleName)
                    '.RequestParameters = False
                    '.CreateDocument()
                End With

                ASPxDocumentViewer1.SettingsSplitter.ParametersPanelCollapsed = True


            Case "DriverOperatingHoursReport"

                Dim vehicleIDStr As String = Request.QueryString("vehicleid")
                'paramaters: startdate,enddate,vehicle
                Dim thisMorning As Date = New Date(Now.Year, Now.Month, Now.Day)
                Dim midnightTonight As Date = thisMorning.AddDays(1)

                Dim vehicleName As String = FMS.Business.DataObjects.ApplicationVehicle.GetForID(New Guid(vehicleIDStr)).Name

                ASPxDocumentViewer1.ToolbarMode = DevExpress.XtraReports.Web.DocumentViewer.DocumentViewerToolbarMode.Ribbon

                With CType(ASPxDocumentViewer1.Report, DriverOperatingHoursReport)
                    .parameter1.Value = thisMorning
                    .parameter2.Value = midnightTonight
                    .Parameter3.Value = vehicleName

                    '.parameter1.Visible = False
                    '.parameter2.Visible = False
                    '.Parameter3.Visible = False

                    '.ObjectDataSource1.DataSource = ReportDataHandler.GetVehicleReportValues(thisMorning, midnightTonight, vehicleName)
                    '.RequestParameters = False
                    '.CreateDocument()
                End With

                ASPxDocumentViewer1.SettingsSplitter.ParametersPanelCollapsed = True

        End Select

    End Sub


    Public Shared Function GetReportFromName(reportName As String) As DevExpress.XtraReports.UI.XtraReport

        Select Case reportName
            Case "VehicleReport"
                Return New VehicleReport
            Case "DriverOperatingHoursReport"
                Return New DriverOperatingHoursReport
            Case "ReportGeoFence_byDriver", "ReportGeoFence_byDriver.vb"
                Return New ReportGeoFence_byDriver
            Case Else
                Return Nothing
        End Select


    End Function

End Class