Public Class CacheStandardAudit
    Public Property Param1 As String
    Public Property Param2 As String
    Public Property LineValues As List(Of StandardAudit)
End Class
Public Class StandardAudit
    Public Property Aid As Integer
    Public Property CSid As System.Nullable(Of Integer)
    Public Property Cid As System.Nullable(Of Integer)
    Public Property Customer As String
    Public Property Site As String
    Public Property OldServiceUnits As System.Nullable(Of Double)
    Public Property OldServicePrice As System.Nullable(Of Double)
    Public Property OldPerAnnumCharge As System.Nullable(Of Double)
    Public Property NewServiceUnits As System.Nullable(Of Double)
    Public Property NewServicePrice As System.Nullable(Of Double)
    Public Property NewPerAnnumCharge As System.Nullable(Of Double)
    Public Property ChangeReasonCode As System.Nullable(Of Integer)
    Public Property User As String
    Public Property ChangeDate As System.Nullable(Of Date)
    Public Property ChangeTime As System.Nullable(Of Date)
    Public Property EffectiveDate As System.Nullable(Of Date)
    Public Property OldContractCeasedate As System.Nullable(Of Date)
    Public Property NewContractCeasedate As System.Nullable(Of Date)
    Public Property OldInvoiceCommencing As System.Nullable(Of Date)
    Public Property NewInvoiceCommencing As System.Nullable(Of Date)
    Public Property OldInvoicingFrequency As String
    Public Property NewInvoicingFrequency As String
    Public Property OldContractStartDate As System.Nullable(Of Date)
    Public Property NewContractStartDate As System.Nullable(Of Date)
    Public Property FieldType As String
    Public Property OldService As String
    Public Property RevenueChangeReason As String
    Public Property ServiceDescription As String
    Public Property ServiceCode As String
    Public Property EffectiveDate1 As System.Nullable(Of Date)
    Public Property InvoiceCommencing As System.Nullable(Of Date)
    Public Property Frequency As String
    Public Property SiteCeaseDate As System.Nullable(Of Date)
    Public Property FieldType1 As String
    Public Property CustomerName As String
    Public Property InvoiceMonth1 As System.Nullable(Of Integer)
    Public Property InvoiceMonth2 As System.Nullable(Of Integer)
    Public Property InvoiceMonth3 As System.Nullable(Of Integer)
    Public Property InvoiceMonth4 As System.Nullable(Of Integer)
    Public Property PurchaseOrderNumber As String
    Public Property SiteCeaseDate1 As System.Nullable(Of Date)
    Public Property OldService1 As String
    Public Property CustormerSiteCeaseDate As String
End Class
