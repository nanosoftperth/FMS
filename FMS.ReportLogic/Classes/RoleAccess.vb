Public Class CacheRole
    Public Property ID As Int32
    Public Property LineValies As List(Of Role)
    Public Property LogoBinary() As Byte()
End Class
Public Class Role
    Public Property ApplicationFeatureRoleID As Guid
    Public Property ApplicationID As Guid
    Public Property FeatureID As Guid
    Public Property RoleID As Guid
    Public Property Feature As String
    Public Property Role As String
    Public Property FeatureName As String
    Public Property RoleName As String
End Class