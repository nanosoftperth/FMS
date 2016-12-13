
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

        repts.Add(New AvailableReport With {.ActualReportNameToDisplay = "ServiceVehicleReport" _
                                           , .ReportDescription = "Testing Phase" _
                                           , .VisibleReportName = "Service Vehicle report"})


        repts.Add(New AvailableReport With {.ActualReportNameToDisplay = "ReportGeoFence_byDriver" _
                                   , .ReportDescription = "A driver and ALL the geo-fences they have gone through i the time period" _
                                   , .VisibleReportName = "Geo-fence report (by driver)"})

        Return repts

    End Function

End Class
