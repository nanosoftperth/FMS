Public Class CacheDataLoggerReport
    Public Property LineValues As List(Of ReportFields)
    Public Property Param1 As String
    Public Property Param2 As String
    Public Property Param3 As String
    Public Property Param4 As String

    Public Property LogoBinary() As Byte()

    Public Property VehicleDisplayName As String

    Public Property GeneratedDateTimeString As String

End Class
Public Class ReportFields
    Public Property Description As String
    Public Property Direction As String
    Public Property Value As Integer
End Class
Public Class TimeClass
    Public Property TimeValue As String
End Class
Public Class DeviceChart
    Public Property DeviceValue As String
    ''' <summary>
    ''' WE are only going ot use the current vehicle settings , not bother with historic (what vehicle was associated with what device) logic as there is no point, this is a report for rio-tinto only now prettu much,
    ''' </summary>
    Public Property VehicleName As String
End Class
Public Class ZoomClass
    Public Property ZoomValue As String
End Class