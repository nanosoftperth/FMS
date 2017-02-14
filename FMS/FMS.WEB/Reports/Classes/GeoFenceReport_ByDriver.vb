
Imports FMS.Business

Public Class ClientSide_GeoFenceReport_ByDriver


    Public Property LogoBinary() As Byte()
    Public Property ReportLines As List(Of FMS.Business.ReportGeneration.GeoFenceReport_Simple)
    Public Property DriverName As String
    Public Property TimeFrom As Date
    Public Property TimeTo As Date
    Public Property VehicleCount As Integer
    Public Property TotalGeoFenceEntries As Integer

    Public Property TotalStopTime As TimeSpan
    Public Property AverageStopTime As TimeSpan
    Public Property MaximumStopTime As TimeSpan

    Public ReadOnly Property Formatted_TotalStopTime As String
        Get
            Return timespanFormatCust(TotalStopTime)
        End Get
    End Property

    Public ReadOnly Property Formatted_AverageStopTime As String
        Get
            Return timespanFormatCust(AverageStopTime)
        End Get
    End Property

    Public ReadOnly Property Formatted_MaximumStopTime As String
        Get
            Return timespanFormatCust(MaximumStopTime)
        End Get
    End Property



    Private Const timeformatstr As String = "hh:mm:ss"


    Public Sub New()

    End Sub

    Friend Sub CalculateSummaryValues(startdate As Date, enddate As Date, driverid As String)

        Dim driver As DataObjects.ApplicationDriver = DataObjects.ApplicationDriver.GetDriverFromID(New Guid(driverid))

        DriverName = driver.NameFormatted
        TimeFrom = startdate
        TimeTo = enddate
        TotalGeoFenceEntries = ReportLines.Count
        VehicleCount = (From x In ReportLines Select x.Vehicle_Name).Distinct.Count

        'Do the summary calcs

        If TotalGeoFenceEntries > 0 Then

            Dim totalSeconds As Integer = ReportLines.Sum(Function(y) y.TimeTakes.TotalSeconds)
            Dim averageSeconds As Integer = totalSeconds / TotalGeoFenceEntries
            Dim maxSeconds As Integer = ReportLines.Max(Function(y) y.TimeTakes.TotalSeconds)

            AverageStopTime = TimeSpan.FromSeconds(averageSeconds)
            MaximumStopTime = TimeSpan.FromSeconds(maxSeconds)
            TotalStopTime = TimeSpan.FromSeconds(totalSeconds)

        End If



    End Sub


    Private Function timespanFormatCust(t As TimeSpan) As String

        Return String.Format("{0:00}:{1:00}:{2:00}", t.TotalHours, t.Minutes, t.Seconds)

    End Function

End Class
