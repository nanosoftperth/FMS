Public Class CacheMYOBCustomerInvoice
    Public Property Param As String
    Public Property LineValues As List(Of MYOBCustomerInvoice)
End Class
Public Class MYOBCustomerInvoice
    Public Property CustomerName As String
    Public Property CustomerNumber As String
    Public Property InvoiceNumber As String
    Public Property InvoiceDate As System.Nullable(Of Date)
    Public Property CustomerPurchaseOrderNumber As String
    Public Property Quantity As System.Nullable(Of Short)
    Public Property ProductCode As String
    Public Property ProductDescription As String
    Public Property InvoiceAmountExGST As System.Nullable(Of Single)
    Public Property InvoiceAmountIncGST As System.Nullable(Of Single)
    Public Property GSTAmount As System.Nullable(Of Single)
    Public Property SiteName As String
End Class
