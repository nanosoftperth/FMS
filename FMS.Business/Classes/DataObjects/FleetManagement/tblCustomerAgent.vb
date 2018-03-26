Namespace DataObjects
    Public Class tblCustomerAgent
#Region "Properties / enums"
        Public Property CustomerAgentID As System.Guid
        Public Property AID As Integer
        Public Property ApplicationID As System.Nullable(Of System.Guid)
        Public Property CustomerAgentName As String
#End Region
#Region "CRUD"
        Public Shared Sub Create(CustomerAgent As DataObjects.tblCustomerAgent)
            With New LINQtoSQLClassesDataContext
                Dim objCustomerAgent As New FMS.Business.tblCustomerAgent
                With objCustomerAgent
                    .CustomerAgentID = Guid.NewGuid
                    .AID = tblProjectID.CustomerAgentIDCreateOrUpdate(CustomerAgent.ApplicationID)
                    .ApplicationID = CustomerAgent.ApplicationID
                    .CustomerAgentName = CustomerAgent.CustomerAgentName
                End With
                .tblCustomerAgents.InsertOnSubmit(objCustomerAgent)
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub Update(CustomerAgent As DataObjects.tblCustomerAgent)
            With New LINQtoSQLClassesDataContext
                Dim objCustomerAgent As FMS.Business.tblCustomerAgent = (From c In .tblCustomerAgents
                                                                         Where c.CustomerAgentID.Equals(CustomerAgent.CustomerAgentID) And c.ApplicationID.Equals(CustomerAgent.ApplicationID)).SingleOrDefault
                With objCustomerAgent
                    .CustomerAgentName = CustomerAgent.CustomerAgentName
                End With
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub Delete(CustomerAgent As DataObjects.tblCustomerAgent)
            With New LINQtoSQLClassesDataContext
                Dim objCustomerAgent As FMS.Business.tblCustomerAgent = (From c In .tblCustomerAgents
                                                                         Where c.CustomerAgentID.Equals(CustomerAgent.CustomerAgentID) And c.ApplicationID.Equals(CustomerAgent.ApplicationID)).SingleOrDefault
                .tblCustomerAgents.DeleteOnSubmit(objCustomerAgent)
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblCustomerAgent)
            Try
                Dim objCustomerAgent As New List(Of DataObjects.tblCustomerAgent)
                With New LINQtoSQLClassesDataContext
                    objCustomerAgent = (From c In .tblCustomerAgents
                                        Order By c.CustomerAgentName
                                        Select New DataObjects.tblCustomerAgent(c)).ToList
                    .Dispose()
                End With

                Return objCustomerAgent
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Shared Function GetAllByApplicationID(appID As System.Guid) As List(Of DataObjects.tblCustomerAgent)
            Try
                Dim objCustomerAgent As New List(Of DataObjects.tblCustomerAgent)
                With New LINQtoSQLClassesDataContext
                    objCustomerAgent = (From c In .tblCustomerAgents
                                        Where c.ApplicationID.Equals(appID)
                                        Order By c.CustomerAgentName
                                        Select New DataObjects.tblCustomerAgent(c)).ToList
                    .Dispose()
                End With
                Return objCustomerAgent
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objTblCustomerAgent As FMS.Business.tblCustomerAgent)
            With objTblCustomerAgent
                Me.CustomerAgentID = .CustomerAgentID
                Me.AID = .AID
                Me.ApplicationID = .ApplicationID
                Me.CustomerAgentName = .CustomerAgentName
            End With
        End Sub
#End Region
    End Class
End Namespace

