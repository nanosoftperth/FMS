Namespace DataObjects
    Public Class tblRevenueChangeAudit
#Region "Properties / enums"
        Public Property ChangeAuditID As System.Guid
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
        Public Property ApplicationID As System.Nullable(Of System.Guid)
#End Region
#Region "CRUD"
        Public Shared Sub Create(RevenueChangeAudit As DataObjects.tblRevenueChangeAudit)
            With New LINQtoSQLClassesDataContext
                Dim AppID = ThisSession.ApplicationID
                Dim objRevenueChangeAudit As New FMS.Business.tblRevenueChangeAudit
                With objRevenueChangeAudit
                    .ChangeAuditID = Guid.NewGuid
                    .Aid = 1
                    .CSid = RevenueChangeAudit.CSid
                    .Cid = RevenueChangeAudit.Cid
                    .Customer = RevenueChangeAudit.Customer
                    .Site = RevenueChangeAudit.Site
                    .OldServiceUnits = RevenueChangeAudit.OldServiceUnits
                    .OldServicePrice = RevenueChangeAudit.OldServicePrice
                    .OldPerAnnumCharge = RevenueChangeAudit.OldPerAnnumCharge
                    .NewServiceUnits = RevenueChangeAudit.NewServiceUnits
                    .NewServicePrice = RevenueChangeAudit.NewServicePrice
                    .NewPerAnnumCharge = RevenueChangeAudit.NewPerAnnumCharge
                    .ChangeReasonCode = RevenueChangeAudit.ChangeReasonCode
                    .User = RevenueChangeAudit.User
                    .ChangeDate = RevenueChangeAudit.ChangeDate
                    .ChangeTime = RevenueChangeAudit.ChangeTime
                    .EffectiveDate = RevenueChangeAudit.EffectiveDate
                    '.OldContractCeasedate = RevenueChangeAudit.OldContractCeasedate
                    '.NewContractCeasedate = RevenueChangeAudit.NewContractCeasedate
                    '.OldInvoiceCommencing = RevenueChangeAudit.OldInvoiceCommencing
                    '.NewInvoiceCommencing = RevenueChangeAudit.NewInvoiceCommencing
                    '.OldInvoicingFrequency = RevenueChangeAudit.OldInvoicingFrequency
                    '.NewInvoicingFrequency = RevenueChangeAudit.NewInvoicingFrequency
                    '.OldContractStartDate = RevenueChangeAudit.OldContractStartDate
                    '.NewContractStartDate = RevenueChangeAudit.NewContractStartDate
                    '.FieldType = RevenueChangeAudit.FieldType
                    .OldService = RevenueChangeAudit.OldService
                    .ApplicationID = AppID
                End With
                .tblRevenueChangeAudits.InsertOnSubmit(objRevenueChangeAudit)
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub Update(RevenueChangeAudit As DataObjects.tblRevenueChangeAudit)
            With New LINQtoSQLClassesDataContext
                Dim objRevenueChangeAudit As FMS.Business.tblRevenueChangeAudit = (From c In .tblRevenueChangeAudits
                                                                                   Where c.ChangeAuditID.Equals(RevenueChangeAudit.ChangeAuditID) And c.ApplicationID.Equals(ThisSession.ApplicationID)).SingleOrDefault
                With objRevenueChangeAudit
                    .CSid = RevenueChangeAudit.CSid
                    .Cid = RevenueChangeAudit.Cid
                    .Customer = RevenueChangeAudit.Customer
                    .Site = RevenueChangeAudit.Site
                    .OldServiceUnits = RevenueChangeAudit.OldServiceUnits
                    .OldServicePrice = RevenueChangeAudit.OldServicePrice
                    .OldPerAnnumCharge = RevenueChangeAudit.OldPerAnnumCharge
                    .NewServiceUnits = RevenueChangeAudit.NewServiceUnits
                    .NewServicePrice = RevenueChangeAudit.NewServicePrice
                    .NewPerAnnumCharge = RevenueChangeAudit.NewPerAnnumCharge
                    .ChangeReasonCode = RevenueChangeAudit.ChangeReasonCode
                    .User = RevenueChangeAudit.User
                    .ChangeDate = RevenueChangeAudit.ChangeDate
                    .ChangeTime = RevenueChangeAudit.ChangeTime
                    .EffectiveDate = RevenueChangeAudit.EffectiveDate
                    .OldService = RevenueChangeAudit.OldService
                End With
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub Delete(RevenueChangeAudit As DataObjects.tblRevenueChangeAudit)
            With New LINQtoSQLClassesDataContext
                Dim objRevenueChangeAudit As FMS.Business.tblRevenueChangeAudit = (From c In .tblRevenueChangeAudits
                                                                                   Where c.ChangeAuditID.Equals(RevenueChangeAudit.ChangeAuditID) And c.ApplicationID.Equals(ThisSession.ApplicationID)).SingleOrDefault
                .tblRevenueChangeAudits.DeleteOnSubmit(objRevenueChangeAudit)
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblRevenueChangeAudit)
            Try
                Dim objRevenueChangeAudit As New List(Of DataObjects.tblRevenueChangeAudit)
                With New LINQtoSQLClassesDataContext
                    objRevenueChangeAudit = (From c In .tblRevenueChangeAudits
                                             Where c.ApplicationID.Equals(ThisSession.ApplicationID)
                                             Order By c.Customer
                                             Select New DataObjects.tblRevenueChangeAudit(c)).ToList
                    .Dispose()
                End With
                Return objRevenueChangeAudit
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objRevenueChangeAudit As FMS.Business.tblRevenueChangeAudit)
            With objRevenueChangeAudit
                Me.ChangeAuditID = .ChangeAuditID
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
                Me.ApplicationID = .ApplicationID
            End With
        End Sub
#End Region
    End Class
End Namespace

