Public Class CacheServiceSummary
    Public Property LineValues As List(Of ServiceSummary)
End Class
Public Class ServiceSummary
    Public Property FrequencyDescription As String
    Public Property ServiceDescription As String
    Public Property ServiceCode As String
    Public Property ServiceUnits As System.Nullable(Of Double)
    Public Property SiteCeaseDate As System.Nullable(Of Date)
End Class
