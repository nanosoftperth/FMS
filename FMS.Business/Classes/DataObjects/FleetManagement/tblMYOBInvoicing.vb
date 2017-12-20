Namespace DataObjects
    Public Class tblMYOBInvoicing
#Region "Properties / enums"
        Public Property Aid As System.Nullable(Of Integer)
        Public Property CustomerNumber As String
        Public Property CustomerName As String
        Public Property InvoiceNumber As String
        Public Property InvoiceDate As System.Nullable(Of Date)
        Public Property CustomerPurchaseOrderNumber As String
        Public Property Quantity As System.Nullable(Of Integer)
        Public Property ProductCode As String
        Public Property ProductDescription As String
        Public Property AnnualPriceExGST As System.Nullable(Of Double)
        Public Property AnnualPriceIncGST As System.Nullable(Of Double)
        Public Property Discount As String
        Public Property InvoiceAmountExGST As System.Nullable(Of Double)
        Public Property InvoiceAmountIncGST As System.Nullable(Of Double)
        Public Property Job As String
        Public Property TaxCode As String
        Public Property GSTAmount As System.Nullable(Of Double)
        Public Property Category As String
        Public Property SiteName As String

#End Region

#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objTbl As FMS.Business.tblMYOBInvoicing)
            
            With objTbl
                Me.Aid = .Aid
                Me.CustomerNumber = .CustomerNumber
                Me.CustomerName = .CustomerName
                Me.InvoiceNumber = .InvoiceNumber
                Me.InvoiceDate = .InvoiceDate
                Me.CustomerPurchaseOrderNumber = .CustomerPurchaseOrderNumber
                Me.Quantity = .Quantity
                Me.ProductCode = .ProductCode
                Me.ProductDescription = .ProductDescription
                Me.AnnualPriceExGST = .AnnualPriceExGST
                Me.AnnualPriceIncGST = .AnnualPriceIncGST
                Me.Discount = .Discount
                Me.InvoiceAmountExGST = .InvoiceAmountExGST
                Me.InvoiceAmountIncGST = .InvoiceAmountIncGST
                Me.Job = .Job
                Me.TaxCode = .TaxCode
                Me.GSTAmount = .GSTAmount
                Me.Category = .Category
                Me.SiteName = .SiteName
                
            End With
        End Sub
#End Region

#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblMYOBInvoicing)
            Dim objMYOB = (From m In SingletonAccess.FMSDataContextContignous.tblMYOBInvoicings
                               Select New DataObjects.tblMYOBInvoicing(m)).ToList

            Return objMYOB
        End Function

#End Region
    End Class

End Namespace


