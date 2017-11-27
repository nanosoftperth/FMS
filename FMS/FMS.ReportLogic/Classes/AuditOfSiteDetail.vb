Public Class CacheAuditOfSiteDetail
    Public Property Param1 As String
    Public Property Param2 As String
    Public Property LineValues As List(Of AuditOfSiteDetail)
End Class
Public Class AuditOfSiteDetail
    Public Property FieldType As String
    Public Property Customer As String
    Public Property Site As String
    Public Property OldContractCeasedate As System.Nullable(Of Date)
    Public Property NewContractCeasedate As System.Nullable(Of Date)
    Public Property OldInvoiceCommencing As System.Nullable(Of Date)
    Public Property NewInvoiceCommencing As System.Nullable(Of Date)
    Public Property OldInvoicingFrequency As String
    Public Property NewInvoicingFrequency As String
    Public Property OldContractStartDate As System.Nullable(Of Date)
    Public Property NewContractStartDate As System.Nullable(Of Date)
    Public Property ChangeDate As System.Nullable(Of Date)
End Class
