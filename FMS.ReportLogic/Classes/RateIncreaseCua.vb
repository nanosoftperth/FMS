Public Class CacheRateIncreaseCua
    Public Property LineValues As List(Of RateIncreaseCua)
    Public Property Param1 As String
End Class
Public Class RateIncreaseCua
    Public Property RateIncreasesID As System.Guid
    Public Property Aid As Integer
    Public Property SiteName As String
    Public Property CustomerName As String
    Public Property CSid As System.Nullable(Of Integer)
    Public Property Units As System.Nullable(Of Short)
    Public Property OldServicePrice As System.Nullable(Of Single)
    Public Property NewServicePrice As System.Nullable(Of Single)
    Public Property OldPerAnnumCharge As System.Nullable(Of Single)
    Public Property NewPerAnnumCharge As System.Nullable(Of Single)
    Public Property CustomerID As System.Nullable(Of Integer)
    Public Property SiteID As System.Nullable(Of Integer)
    Public Property Invfreq As System.Nullable(Of Integer)
    Public Property InvStartDate As System.Nullable(Of Date)
    Public Property ApplicationID As System.Nullable(Of System.Guid)
    Public Property ServiceDescription As String
End Class
