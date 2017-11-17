Public Class CacheSitesWithNoContracts
    Public Property LineValues As List(Of SitesWithNoContracts)
End Class
Public Class SitesWithNoContracts
    Public Property CustomerName As String
    Public Property SiteName As String
    Public Property SitePeriod As System.Nullable(Of Integer)
    Public Property SiteStartDate As System.Nullable(Of Date)
    Public Property ContractPeriodDesc As String
    Public Property SiteCeaseDate As System.Nullable(Of Date)
End Class
