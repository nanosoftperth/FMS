Imports System.Data.SqlClient
Imports System
Imports System.Configuration

Namespace DataObjects
    Public Class tblMYOBInvoicing

#Region "Properties / enums"
        Public Property ApplicationId As Guid
        Public Property Aid As System.Nullable(Of Integer)
        Public Property CustomerNumber As String
        Public Property CustomerName As String
        Public Property InvoiceNumber As String
        Public Property InvoiceDate As System.Nullable(Of Date)
        Public Property CustomerPurchaseOrderNumber As String
        Public Property Quantity As System.Nullable(Of Double)
        Public Property ProductCode As String
        Public Property ProductDescription As String
        Public Property AnnualPriceExGST As System.Nullable(Of Double)
        Public Property AnnualPriceIncGST As System.Nullable(Of Double)
        Public Property Discount As String
        Public Property InvoiceAmountExGST As System.Nullable(Of Double)
        Public Property InvoiceAmountIncGST As System.Nullable(Of Double)
        Public Property Job As String
        Public Property JournalMemo As String
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

#Region "CRUD"
        Public Shared Sub CreateAll(myob As List(Of DataObjects.tblMYOBInvoicing))
            Dim appID = ThisSession.ApplicationID

            For Each rMYOB In myob
                Dim oMYOB As New FMS.Business.tblMYOBInvoicing
                With oMYOB
                    .ApplicationId = appID
                    .CustomerNumber = rMYOB.CustomerNumber
                    .CustomerName = rMYOB.CustomerName
                    .InvoiceNumber = rMYOB.InvoiceNumber
                    .InvoiceDate = rMYOB.InvoiceDate
                    .CustomerPurchaseOrderNumber = rMYOB.CustomerPurchaseOrderNumber
                    .Quantity = rMYOB.Quantity
                    .ProductCode = rMYOB.ProductCode
                    .ProductDescription = rMYOB.ProductDescription
                    .AnnualPriceExGST = rMYOB.AnnualPriceExGST
                    .AnnualPriceIncGST = rMYOB.AnnualPriceIncGST
                    .Job = rMYOB.Job
                    .JournalMemo = rMYOB.JournalMemo
                    .TaxCode = rMYOB.TaxCode
                    .GSTAmount = rMYOB.GSTAmount
                    .Category = rMYOB.Category
                    .SiteName = rMYOB.SiteName
                End With
                SingletonAccess.FMSDataContextContignous.tblMYOBInvoicings.InsertOnSubmit(oMYOB)
            Next

            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region

#Region "Extended CRUD"
        Public Shared Sub DeleteAll()
            '--- Use this technique to faster delete the records from tblMYOBMatch
            Dim strConnection As String = System.Configuration.ConfigurationManager.ConnectionStrings("ApplicationServices").ConnectionString
            Dim connection As SqlConnection
            Dim adapter As SqlDataAdapter = New SqlDataAdapter()
            Dim sql As String = Nothing
            connection = New SqlConnection(strConnection)
            sql = "delete from tblMYOBInvoicing"

            connection.Open()
            adapter.DeleteCommand = connection.CreateCommand()
            adapter.DeleteCommand.CommandText = sql
            adapter.DeleteCommand.ExecuteNonQuery()

        End Sub
#End Region

#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblMYOBInvoicing)
            Dim objMYOB = (From m In SingletonAccess.FMSDataContextContignous.tblMYOBInvoicings
                               Select New DataObjects.tblMYOBInvoicing(m)).ToList

            Return objMYOB
        End Function

        Public Shared Function GetAllOrderByInvoiceNumber() As List(Of DataObjects.tblMYOBInvoicing)
            Dim objMYOB = (From m In SingletonAccess.FMSDataContextContignous.tblMYOBInvoicings
                            Order By m.InvoiceNumber
                               Select New DataObjects.tblMYOBInvoicing(m)).ToList

            Return objMYOB
        End Function

#End Region
    End Class

End Namespace


