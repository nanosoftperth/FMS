Namespace DataObjects
    Public Class usp_GetPreviousInvoiceSummaryReport
#Region "Properties / enums"
        Public Property ApplicationId As System.Guid
        Public Property CustomerName As String
        Public Property SumOfAnnualPriceExGST As System.Nullable(Of Double)
        Public Property SumOfInvoiceAmountExGST As System.Nullable(Of Double)
        Public Property SumOfGSTAmount As System.Nullable(Of Double)
        Public Property SumOfInvoiceAmountIncGST As System.Nullable(Of Double)

#End Region

#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objTbl As FMS.Business.usp_GetPreviousInvoiceSummaryReportResult)

            With objTbl
                Me.ApplicationId = .ApplicationId
                Me.CustomerName = .CustomerName
                Me.SumOfAnnualPriceExGST = .SumOfAnnualPriceExGST
                Me.SumOfInvoiceAmountExGST = .SumOfInvoiceAmountExGST
                Me.SumOfInvoiceAmountIncGST = .SumOfInvoiceAmountIncGST
                Me.SumOfGSTAmount = .SumOfGSTAmount

            End With
        End Sub
#End Region

#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.usp_GetPreviousInvoiceSummaryReport)
            Try
                Dim appID = ThisSession.ApplicationID
                Dim objInv As New List(Of DataObjects.usp_GetPreviousInvoiceSummaryReport)

                With New LINQtoSQLClassesDataContext
                    objInv = (From i In .usp_GetPreviousInvoiceSummaryReport
                              Where i.ApplicationId = appID
                              Select New DataObjects.usp_GetPreviousInvoiceSummaryReport(i)).ToList()
                    .Dispose()

                End With

                Return objInv
            Catch ex As Exception
                Throw ex
            End Try

        End Function

#End Region


    End Class

End Namespace

