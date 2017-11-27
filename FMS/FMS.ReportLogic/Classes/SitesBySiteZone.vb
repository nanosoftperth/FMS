Public Class CacheSitesBySiteZone
    Public Property LineValues As List(Of SitesBySiteZone)
End Class
Public Class SitesBySiteZone
    Public Property Zone As String
    Public Property Cid As Integer
    Public Property SiteName As String
    Public Property Customer As System.Nullable(Of Short)
    Public Property AddressLine1 As String
    Public Property AddressLine2 As String
    Public Property AddressLine3 As String
    Public Property Suburb As String
    Public Property PostCode As System.Nullable(Of Short)
    Public Property PostalAddressLine1 As String
    Public Property PostalAddressLine2 As String
    Public Property PostalSuburb As String
    Public Property PostalPostCode As System.Nullable(Of Short)
End Class
