Namespace DataObjects
    Public Class usp_GetMYOBCustomerInvoiceReport
#Region "Properties / enums"
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
#End Region
#Region "Get methods"
        Public Shared Function GetMYOBCustomerInvoiceReport(CustomerName As String) As List(Of DataObjects.usp_GetMYOBCustomerInvoiceReport)
            Try
                Dim objZones As New List(Of DataObjects.usp_GetMYOBCustomerInvoiceReport)
                With New LINQtoSQLClassesDataContext
                    objZones = (From c In .usp_GetMYOBCustomerInvoiceReport(CustomerName, ThisSession.ApplicationID)
                                Select New DataObjects.usp_GetMYOBCustomerInvoiceReport(c)).ToList
                    .Dispose()
                End With
                Return objZones
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objMYOB As FMS.Business.usp_GetMYOBCustomerInvoiceReportResult)
            With objMYOB
                Me.CustomerName = .CustomerName
                Me.CustomerNumber = .CustomerNumber
                Me.InvoiceNumber = .InvoiceNumber
                Me.InvoiceDate = .InvoiceDate
                Me.CustomerPurchaseOrderNumber = .CustomerPurchaseOrderNumber
                Me.Quantity = .Quantity
                Me.ProductCode = .ProductCode
                Me.ProductDescription = .ProductDescription
                Me.InvoiceAmountExGST = .InvoiceAmountExGST
                Me.InvoiceAmountIncGST = .InvoiceAmountIncGST
                Me.GSTAmount = .GSTAmount
                Me.SiteName = .SiteName
            End With
        End Sub
#End Region
    End Class
End Namespace


