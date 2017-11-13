Public Class CacheServiceList
    Public Property Param As String
    Public Property LineValues As List(Of ServiceList)
End Class
Public Class ServiceList
    Public Property ServicesID As System.Guid
    Public Property Sid As Integer
    Public Property ServiceCode As String
    Public Property ServiceDescription As String
    Public Property CostOfService As System.Nullable(Of Single)
End Class
