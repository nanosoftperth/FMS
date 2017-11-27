Public Class CacheAuditContract
    Public Property Param1 As String
    Public Property Param2 As String
    Public Property LineValues As List(Of AuditContract)
End Class
Public Class AuditContract
    Public Property FieldType As String
    Public Property ChangeDate As System.Nullable(Of Date)
    Public Property Customer As String
    Public Property OldContractCeasedate As System.Nullable(Of Date)
    Public Property NewContractCeasedate As System.Nullable(Of Date)
End Class
