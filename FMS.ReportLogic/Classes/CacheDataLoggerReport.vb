Public Class CacheDataLoggerReport
    Public Property LineValues As List(Of ReportFields)
    Public Property Param1 As String
    Public Property Param2 As String
    Public Property Param3 As String
    Public Property Param4 As String
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
End Class