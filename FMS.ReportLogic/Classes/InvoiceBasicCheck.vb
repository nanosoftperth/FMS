Public Class CacheInvoiceBasicCheck
    Public Property LineValues As List(Of InvoiceBasicCheck)
End Class
Public Class InvoiceBasicCheck
    Public Property CustomerName As String
    Public Property SiteName As String
    Public Property SiteCeaseDate As System.Nullable(Of Date)
    Public Property Frequency As String
    Public Property InvoiceCommencing As System.Nullable(Of Date)
    Public Property MonthDescription As String
End Class
