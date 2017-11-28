Public Class CacheRunValues
    Public Property Param As String
    Public Property LineValues As List(Of RunValues)
End Class
Public Class RunValues
    Public Property SiteName As String
    Public Property Add As String
    Public Property AddLastLine As String
    Public Property ServiceDescription As String
    Public Property ServiceUnits As System.Nullable(Of Single)
    Public Property PerAnnumCharge As System.Nullable(Of Single)
    Public Property ServiceRun As System.Nullable(Of Short)
    Public Property RunDescription As String
    Public Property SiteCeaseDate As System.Nullable(Of Date)
End Class
