Namespace DataObjects
    Public Class usp_GetStandardAuditReport
#Region "Properties / enums"
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
#End Region
#Region "Get methods"
        Public Shared Function GetStandardAuditReport(sDate As Date, eDate As Date) As List(Of DataObjects.usp_GetStandardAuditReport)
            Try
                Dim objStandardAudit As New List(Of DataObjects.usp_GetStandardAuditReport)
                With New LINQtoSQLClassesDataContext
                    objStandardAudit = (From c In .usp_GetStandardAuditReport(sDate, eDate, ThisSession.ApplicationID)
                                        Select New DataObjects.usp_GetStandardAuditReport(c)).ToList
                    .Dispose()
                End With
                Return objStandardAudit
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objStandardAudit As FMS.Business.usp_GetStandardAuditReportResult)
            With objStandardAudit
                Me.Aid = .Aid
                Me.CSid = .CSid
                Me.Cid = .Cid
                Me.Customer = .Customer
                Me.Site = .Site
                Me.OldServiceUnits = .OldServiceUnits
                Me.OldServicePrice = .OldServicePrice
                Me.OldPerAnnumCharge = .OldPerAnnumCharge
                Me.NewServiceUnits = .NewServiceUnits
                Me.NewServicePrice = .NewServicePrice
                Me.NewPerAnnumCharge = .NewPerAnnumCharge
                Me.ChangeReasonCode = .ChangeReasonCode
                Me.User = .User
                Me.ChangeDate = .ChangeDate
                Me.ChangeTime = .ChangeTime
                Me.EffectiveDate = .EffectiveDate
                Me.OldContractCeasedate = .OldContractCeasedate
                Me.NewContractCeasedate = .NewContractCeasedate
                Me.OldInvoiceCommencing = .OldInvoiceCommencing
                Me.NewInvoiceCommencing = .NewInvoiceCommencing
                Me.OldInvoicingFrequency = .OldInvoicingFrequency
                Me.NewInvoicingFrequency = .NewInvoicingFrequency
                Me.OldContractStartDate = .OldContractStartDate
                Me.NewContractStartDate = .NewContractStartDate
                Me.FieldType = .FieldType
                Me.OldService = .OldService
                Me.RevenueChangeReason = .RevenueChangeReason
                Me.ServiceDescription = .ServiceDescription
                Me.ServiceCode = .ServiceCode
                Me.EffectiveDate1 = .EffectiveDate1
                Me.InvoiceCommencing = .InvoiceCommencing
                Me.Frequency = .Frequency
                Me.SiteCeaseDate = .SiteCeaseDate
                Me.FieldType1 = .FieldType1
                Me.CustomerName = .CustomerName
                Me.InvoiceMonth1 = .InvoiceMonth1
                Me.InvoiceMonth2 = .InvoiceMonth2
                Me.InvoiceMonth3 = .InvoiceMonth3
                Me.InvoiceMonth4 = .InvoiceMonth4
                Me.PurchaseOrderNumber = .PurchaseOrderNumber
                Me.SiteCeaseDate1 = .SiteCeaseDate1
                Me.OldService1 = .OldService1
                Me.CustormerSiteCeaseDate = .CustormerSiteCeaseDate
            End With
        End Sub
#End Region
    End Class
End Namespace


