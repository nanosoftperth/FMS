Public Class CacheCustomerContactDetails
    Public Property LineValues As List(Of CustomerContactDetails)
End Class
Public Class CustomerContactDetails
    Public Property CustomerName As String
    Public Property AddressLine1 As String
    Public Property AddressLine2 As String
    Public Property AddressLine3 As String
    Public Property StateDesc As String
    Public Property Suburb As String
    Public Property PostCode As String
    Public Property CustomerContactName As String
    Public Property CustomerPhone As String
    Public Property CustomerMobile As String
    Public Property CustomerFax As String
    Public Property CustomerComments As String
    Public Property CustomerAgentName As String
    Public Property CustomerRating As System.Nullable(Of Short)
    Public Property CustomerRatingDesc As String
End Class
