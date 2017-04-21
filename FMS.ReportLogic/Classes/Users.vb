Imports FMS.Business
Public Class CacheUsers
    Public Property ID As Int32
    Public Property LineValies As List(Of Users)
    Public Property LogoBinary() As Byte()
End Class
Public Class Users
    Public Property ApplicationID As Guid
    Public Property UserId As Guid
    Public Property UserName As String
    Public Property Email As String
    Public Property Mobile As String
    Public Property TimeZone As DataObjects.TimeZone
    Public Property TimeZoneID As String
    Public Property LastLoggedInDate As Date
    Public Property RoleID As Guid
End Class

