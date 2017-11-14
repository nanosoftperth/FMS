Public Class CacheContractRenewal
    Public Property Param As String
    Public Property Param2 As String
    Public Property Param3 As String
    Public Property LineValues As List(Of ContractRenewal)
End Class
Public Class ContractRenewal
    Public Property Customer As System.Nullable(Of Short)
    Public Property AreaDescription As String
    Public Property SiteContractExpiry As System.Nullable(Of Date)
    Public Property CustomerName As String
    Public Property SiteName As String
    Public Property SiteStartDate As System.Nullable(Of Date)
    Public Property ContractPeriodDesc As String
    Public Property SiteContactPhone As String
    Public Property CustomerContactName As String
    Public Property CustomerPhone As String
    Public Property ServiceUnits As System.Nullable(Of Double)
    Public Property PerAnnumCharge As System.Nullable(Of Double)
End Class
