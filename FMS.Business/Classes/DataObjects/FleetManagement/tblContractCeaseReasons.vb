Namespace DataObjects
    Public Class tblContractCeaseReasons
#Region "Properties / enums"
        Public Property CeaseReasonID As System.Guid
        Public Property Aid As Integer
        Public Property CeaseReasonDescription As String
        Public Property CeaseReasonSortOrder As System.Nullable(Of Integer)
#End Region
#Region "CRUD"
        Public Shared Sub Create(ContractCeaseReason As DataObjects.tblContractCeaseReasons)
            Dim objContractCeaseReason As New FMS.Business.tblContractCeaseReason
            With objContractCeaseReason
                .CeaseReasonID = Guid.NewGuid
                .CeaseReasonDescription = ContractCeaseReason.CeaseReasonDescription
            End With
            SingletonAccess.FMSDataContextContignous.tblContractCeaseReasons.InsertOnSubmit(objContractCeaseReason)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(ContractCeaseReason As DataObjects.tblContractCeaseReasons)
            Dim objContractCeaseReason As FMS.Business.tblContractCeaseReason = (From c In SingletonAccess.FMSDataContextContignous.tblContractCeaseReasons
                                                           Where c.CeaseReasonID.Equals(ContractCeaseReason.CeaseReasonID)).SingleOrDefault
            With objContractCeaseReason
                .CeaseReasonDescription = ContractCeaseReason.CeaseReasonDescription
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(ContractCeaseReason As DataObjects.tblContractCeaseReasons)
            Dim obContractCeaseReason As FMS.Business.tblContractCeaseReason = (From c In SingletonAccess.FMSDataContextContignous.tblContractCeaseReasons
                                                         Where c.CeaseReasonID.Equals(ContractCeaseReason.CeaseReasonID)).SingleOrDefault
            SingletonAccess.FMSDataContextContignous.tblContractCeaseReasons.DeleteOnSubmit(obContractCeaseReason)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblContractCeaseReasons)
            Dim objContractCeaseReason = (From c In SingletonAccess.FMSDataContextContignous.tblContractCeaseReasons
                                          Order By c.CeaseReasonDescription
                                          Select New DataObjects.tblContractCeaseReasons(c)).ToList
            Return objContractCeaseReason
        End Function
        Public Shared Function GetContractCeaseReasonSortOrder(ContractCeaseReasoneID As Integer) As DataObjects.tblContractCeaseReasons
            Dim objContractCeaseReason = (From c In SingletonAccess.FMSDataContextContignous.usp_GetContractCeaseReasons
                            Where c.Aid.Equals(ContractCeaseReasoneID)
                            Order By c.CeaseReasonDescription
                            Select New DataObjects.tblContractCeaseReasons() With {.CeaseReasonID = c.CeaseReasonID,
                                                                                   .Aid = c.Aid, .CeaseReasonDescription = c.CeaseReasonDescription,
                                                                                   .CeaseReasonSortOrder = c.SortOrder}).SingleOrDefault
            Return objContractCeaseReason
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objContractCeaseReason As FMS.Business.tblContractCeaseReason)
            With objContractCeaseReason
                Me.CeaseReasonID = .CeaseReasonID
                Me.Aid = .Aid
                Me.CeaseReasonDescription = .CeaseReasonDescription
                Me.CeaseReasonSortOrder = 0
            End With
        End Sub
#End Region
    End Class
End Namespace

