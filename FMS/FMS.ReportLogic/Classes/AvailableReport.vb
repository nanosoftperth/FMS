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


        repts.Add(New AvailableReport With {.ActualReportNameToDisplay = "VehicleListReport" _
                                           , .ReportDescription = "list of alll vehicles" _
                                           , .VisibleReportName = "Vehicle List report"})

        repts.Add(New AvailableReport With {.ActualReportNameToDisplay = "DriversListReport" _
                                           , .ReportDescription = "list of all drivers" _
                                           , .VisibleReportName = "Driver List Report"})
         
        repts.Add(New AvailableReport With {.ActualReportNameToDisplay = "UsersListReport" _
                                         , .ReportDescription = "list of all Users" _
                                         , .VisibleReportName = "Users List Report"})

        Return repts

    End Function
End Class
