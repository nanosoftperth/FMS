Namespace DataObjects
    Public Class tblContractCeaseReasons
#Region "Properties / enums"
        Public Property CeaseReasonID As System.Guid
        Public Property ApplicationID As System.Nullable(Of System.Guid)
        Public Property Aid As Integer
        Public Property CeaseReasonDescription As String
        Public Property CeaseReasonSortOrder As System.Nullable(Of Integer)
#End Region
#Region "CRUD"
        Public Shared Sub Create(ContractCeaseReason As DataObjects.tblContractCeaseReasons)
            With New LINQtoSQLClassesDataContext
                Dim objContractCeaseReason As New FMS.Business.tblContractCeaseReason
                Dim appId = ThisSession.ApplicationID
                With objContractCeaseReason
                    .CeaseReasonID = Guid.NewGuid
                    .ApplicationID = appId
                    .Aid = tblProjectID.CeaseReasonIDCreateOrUpdate(appId)
                    .CeaseReasonDescription = ContractCeaseReason.CeaseReasonDescription
                End With
                .tblContractCeaseReasons.InsertOnSubmit(objContractCeaseReason)
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub Update(ContractCeaseReason As DataObjects.tblContractCeaseReasons)
            With New LINQtoSQLClassesDataContext
                Dim objContractCeaseReason As FMS.Business.tblContractCeaseReason = (From c In .tblContractCeaseReasons
                                                                                     Where c.CeaseReasonID.Equals(ContractCeaseReason.CeaseReasonID) And c.ApplicationID.Equals(ThisSession.ApplicationID)).SingleOrDefault
                With objContractCeaseReason
                    .CeaseReasonDescription = ContractCeaseReason.CeaseReasonDescription
                End With
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub Delete(ContractCeaseReason As DataObjects.tblContractCeaseReasons)
            With New LINQtoSQLClassesDataContext
                Dim obContractCeaseReason As FMS.Business.tblContractCeaseReason = (From c In .tblContractCeaseReasons
                                                                                    Where c.CeaseReasonID.Equals(ContractCeaseReason.CeaseReasonID) And c.ApplicationID.Equals(ThisSession.ApplicationID)).SingleOrDefault
                .tblContractCeaseReasons.DeleteOnSubmit(obContractCeaseReason)
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblContractCeaseReasons)
            Try
                Dim objContractCeaseReason As New List(Of DataObjects.tblContractCeaseReasons)
                With New LINQtoSQLClassesDataContext
                    objContractCeaseReason = (From c In .tblContractCeaseReasons
                                              Where c.ApplicationID.Equals(ThisSession.ApplicationID)
                                              Order By c.CeaseReasonDescription
                                              Select New DataObjects.tblContractCeaseReasons(c)).ToList
                    .Dispose()
                End With
                Return objContractCeaseReason
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Shared Function GetContractCeaseReasonSortOrder(ContractCeaseReasoneID As Integer) As DataObjects.tblContractCeaseReasons
            Try
                Dim objContractCeaseReason As New DataObjects.tblContractCeaseReasons
                With New LINQtoSQLClassesDataContext
                    objContractCeaseReason = (From c In .usp_GetContractCeaseReasons(ThisSession.ApplicationID)
                                              Where c.Aid.Equals(ContractCeaseReasoneID)
                                              Order By c.CeaseReasonDescription
                                              Select New DataObjects.tblContractCeaseReasons() With {.CeaseReasonID = c.CeaseReasonID,
                                                                                       .Aid = c.Aid, .CeaseReasonDescription = c.CeaseReasonDescription,
                                                                                       .CeaseReasonSortOrder = c.SortOrder}).SingleOrDefault
                    .Dispose()
                End With
                Return objContractCeaseReason
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objContractCeaseReason As FMS.Business.tblContractCeaseReason)
            With objContractCeaseReason
                Me.CeaseReasonID = .CeaseReasonID
                Me.ApplicationID = .ApplicationID
                Me.Aid = .Aid
                Me.CeaseReasonDescription = .CeaseReasonDescription
                Me.CeaseReasonSortOrder = 0
            End With
        End Sub
#End Region
    End Class
End Namespace

