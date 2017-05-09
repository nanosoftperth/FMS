Public Class ReportContent
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
 
        Session.Remove("CachedVehicleReports")

        Dim reportname As String = Request.QueryString("Report")
        Me.ASPxDocumentViewer1.Report = GetReportFromName(reportname)

        '((ReportToolbarButton)this.ReportToolbar1.Items[2]).Enabled = false;



        'BY RYAN: change column labels 
        If reportname = "DriverOperatingHoursReport" Then
            With CType(ASPxDocumentViewer1.Report, FMS.ReportLogic.DriverOperatingHoursReport)
                .ArrivalCell.Text = FMS.Business.ThisSession.ApplicationName + " H.Q. Arrival"
                .DepartureCell.Text = FMS.Business.ThisSession.ApplicationName + " H.Q. Departure"
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

                With CType(ASPxDocumentViewer1.Report, FMS.ReportLogic.VehicleReport)
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

                With CType(ASPxDocumentViewer1.Report, FMS.ReportLogic.DriverOperatingHoursReport)
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
                Return New FMS.ReportLogic.VehicleReport
            Case "DriverOperatingHoursReport"
                Return New FMS.ReportLogic.DriverOperatingHoursReport
            Case "ReportGeoFence_byDriver", "ReportGeoFence_byDriver.vb"
                Return New FMS.ReportLogic.ReportGeoFence_byDriver
            Case "VehicleListReport"
                Return New FMS.ReportLogic.VehicleListReport
            Case "DriversListReport"
                Return New FMS.ReportLogic.DriversListReport
            Case "UsersListReport"
                Return New FMS.ReportLogic.UsersListReport
            Case "ContactListReport"
                Return New FMS.ReportLogic.ContactsListReport
            Case "RoleListReport"
                Return New FMS.ReportLogic.RoleListReport
            Case "FeatureAccessListReport"
                Return New FMS.ReportLogic.FeatureAccessListReport
            Case "BusinessLocationListReport"
                Return New FMS.ReportLogic.BusinessLocationListReport
            Case "VehicletoDriversListReport"
                Return New FMS.ReportLogic.ApplicationVehicleDriver
            Case Else
                Return Nothing
        End Select  
    End Function

End Class