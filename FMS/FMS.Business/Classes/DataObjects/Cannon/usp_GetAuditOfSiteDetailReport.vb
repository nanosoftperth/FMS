Namespace DataObjects
    Public Class usp_GetAuditOfSiteDetailReport
#Region "Properties / enums"
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
#End Region
#Region "Get methods"
        Public Shared Function GetAuditOfSiteDetailReportt(sDate As Date, eDate As Date) As List(Of DataObjects.usp_GetAuditOfSiteDetailReport)
            Dim objAuditOfSiteDetail = (From c In SingletonAccess.FMSDataContextContignous.usp_GetAuditOfSiteDetailReport(sDate, eDate)
                            Select New DataObjects.usp_GetAuditOfSiteDetailReport(c)).ToList
            Return objAuditOfSiteDetail
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objAuditOfSiteDetail As FMS.Business.usp_GetAuditOfSiteDetailReportResult)
            With objAuditOfSiteDetail
                Me.FieldType = .FieldType
                Me.Customer = .Customer
                Me.Site = .Site
                Me.OldContractCeasedate = .OldContractCeasedate
                Me.NewContractCeasedate = .NewContractCeasedate
                Me.OldInvoiceCommencing = .OldInvoiceCommencing
                Me.NewInvoiceCommencing = .NewInvoiceCommencing
                Me.OldInvoicingFrequency = .OldInvoicingFrequency
                Me.NewInvoicingFrequency = .NewInvoicingFrequency
                Me.OldContractStartDate = .OldContractStartDate
                Me.NewContractStartDate = .NewContractStartDate
                Me.ChangeDate = .ChangeDate
            End With
        End Sub
#End Region
    End Class
End Namespace


