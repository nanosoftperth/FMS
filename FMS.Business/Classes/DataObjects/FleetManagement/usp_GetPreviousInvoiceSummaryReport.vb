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
            Dim appID = ThisSession.ApplicationID

            Dim objInv = (From i In SingletonAccess.FMSDataContextContignous.usp_GetPreviousInvoiceSummaryReport
                          Where i.ApplicationId = appID
                          Select New DataObjects.usp_GetPreviousInvoiceSummaryReport(i)).ToList()
            Return objInv
        End Function

#End Region


    End Class

End Namespace

