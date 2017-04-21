Public Class CacheRoles
    Public Property ID As Int32
    Public Property LineValies As List(Of Roles)
    Public Property LogoBinary() As Byte()
End Class
Public Class Roles
    Public Property ApplicationID As Guid
    Public Property Name As String
    Public Property RoleID As Guid
    Public Property Description As String
End Class