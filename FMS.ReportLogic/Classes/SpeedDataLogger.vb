Public Class CacheSpeedDataLogger
    Public Property LineValues As List(Of SpeedDataLogger)
    Public Property Param1 As String
End Class
Public Class SpeedDataLogger
    Public Property Description As String
    Public Property SpeedDateTime As DateTime
    Public Property Value As Decimal
End Class
