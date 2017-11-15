Public Class CacheCustomerByCustZone
    Public Property LineValues As List(Of CustomerByCustZone)
End Class
Public Class CustomerByCustZone
    Public Property ZoneDescription As String
    Public Property Cid As Integer
    Public Property CustomerName As String
    Public Property AddressLine1 As String
    Public Property AddressLine2 As String
    Public Property StateCode As String
    Public Property Suburb As String
    Public Property PostCode As String
    Public Property CustomerContactName As String
    Public Property CustomerPhone As String
End Class
