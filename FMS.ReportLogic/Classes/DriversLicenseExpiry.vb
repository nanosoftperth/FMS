Public Class CacheDriversLicenseExpiry
    Public Property Param As String
    Public Property LineValues As List(Of DriversLicenseExpiry)
End Class
Public Class DriversLicenseExpiry
    Public Property DriverID As System.Guid
    Public Property Did As Integer
    Public Property DriverName As String
    Public Property DriversLicenseNo As String
    Public Property DriversLicenseExpiryDate As System.Nullable(Of Date)
    Public Property Inactive As Boolean
    Public Property Renewal As String
End Class
