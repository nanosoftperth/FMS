Public Class AvailableReport

    Public Property VisibleReportName As String
    Public Property ReportDescription As String
    Public Property ActualReportNameToDisplay As String

    Public ReadOnly Property DataforJavascript As String
        Get
            Return String.Format("{0}|{1}", ActualReportNameToDisplay, ReportDescription)
        End Get
    End Property

    Public Sub New()

    End Sub

    Public Shared Function GetAllReports() As List(Of AvailableReport)


        Dim repts As New List(Of AvailableReport)


        repts.Add(New AvailableReport With {.ActualReportNameToDisplay = "VehicleReport" _
                                           , .ReportDescription = "All journeys and stops for a specific vehicle" _
                                           , .VisibleReportName = "Vehicle report"})

        repts.Add(New AvailableReport With {.ActualReportNameToDisplay = "DriverOperatingHoursReport" _
                                           , .ReportDescription = "Driver Operating Hours to Report" _
                                           , .VisibleReportName = "Driver Operating Hours to Report"})


        repts.Add(New AvailableReport With {.ActualReportNameToDisplay = "ReportGeoFence_byDriver" _
                                   , .ReportDescription = "A driver and ALL the geo-fences they have gone through i the time period" _
                                   , .VisibleReportName = "Geo-fence report (by driver)"})


        repts.Add(New AvailableReport With {.ActualReportNameToDisplay = "VehicleListReport" _
                                           , .ReportDescription = "list of alll vehicles" _
                                           , .VisibleReportName = "Vehicle List report"})

        repts.Add(New AvailableReport With {.ActualReportNameToDisplay = "DriversListReport" _
                                           , .ReportDescription = "list of all drivers" _
                                           , .VisibleReportName = "Driver List Report"})
         
        repts.Add(New AvailableReport With {.ActualReportNameToDisplay = "UsersListReport" _
                                         , .ReportDescription = "list of all Users" _
                                         , .VisibleReportName = "Users List Report"})

        repts.Add(New AvailableReport With {.ActualReportNameToDisplay = "ContactListReport" _
                                   , .ReportDescription = "list of all contact" _
                                   , .VisibleReportName = "Contact List Report"})

        repts.Add(New AvailableReport With {.ActualReportNameToDisplay = "RoleListReport" _
                                , .ReportDescription = "list of all defined roles" _
                                , .VisibleReportName = "Defined Roles List Report"})

        repts.Add(New AvailableReport With {.ActualReportNameToDisplay = "FeatureAccessListReport" _
                             , .ReportDescription = "Feature access list" _
                             , .VisibleReportName = "Feature access List Report"})

        repts.Add(New AvailableReport With {.ActualReportNameToDisplay = "BusinessLocationListReport" _
                          , .ReportDescription = " Business location list" _
                          , .VisibleReportName = "Business location List Report"})


        repts.Add(New AvailableReport With {.ActualReportNameToDisplay = "VehicletoDriversListReport" _
                      , .ReportDescription = "Assign Vehicles to drivers list" _
                      , .VisibleReportName = "Assign Vehicles to drivers Report"})

        Return repts

    End Function
End Class
