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
            Dim objCustomerAgent As New FMS.Business.tblCustomerAgent
            With objCustomerAgent
                .CustomerAgentID = Guid.NewGuid
                .AID = tblProjectID.CustomerAgentIDCreateOrUpdate(CustomerAgent.ApplicationID)
                .ApplicationID = CustomerAgent.ApplicationID
                .CustomerAgentName = CustomerAgent.CustomerAgentName
            End With
            SingletonAccess.FMSDataContextContignous.tblCustomerAgents.InsertOnSubmit(objCustomerAgent)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(CustomerAgent As DataObjects.tblCustomerAgent)
            Dim objCustomerAgent As FMS.Business.tblCustomerAgent = (From c In SingletonAccess.FMSDataContextContignous.tblCustomerAgents
                                                           Where c.CustomerAgentID.Equals(CustomerAgent.CustomerAgentID) And c.ApplicationID.Equals(CustomerAgent.ApplicationID)).SingleOrDefault
            With objCustomerAgent
                .CustomerAgentName = CustomerAgent.CustomerAgentName
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(CustomerAgent As DataObjects.tblCustomerAgent)
            Dim objCustomerAgent As FMS.Business.tblCustomerAgent = (From c In SingletonAccess.FMSDataContextContignous.tblCustomerAgents
                                                         Where c.CustomerAgentID.Equals(CustomerAgent.CustomerAgentID) And c.ApplicationID.Equals(CustomerAgent.ApplicationID)).SingleOrDefault
            SingletonAccess.FMSDataContextContignous.tblCustomerAgents.DeleteOnSubmit(objCustomerAgent)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblCustomerAgent)
            Dim objCustomerAgent = (From c In SingletonAccess.FMSDataContextContignous.tblCustomerAgents
                            Order By c.CustomerAgentName
                                          Select New DataObjects.tblCustomerAgent(c)).ToList
            Return objCustomerAgent
        End Function
        Public Shared Function GetAllByApplicationID(appID As System.Guid) As List(Of DataObjects.tblCustomerAgent)
            Dim objCustomerAgent = (From c In SingletonAccess.FMSDataContextContignous.tblCustomerAgents
                                    Where c.ApplicationID.Equals(appID)
                                    Order By c.CustomerAgentName
                                          Select New DataObjects.tblCustomerAgent(c)).ToList
            Return objCustomerAgent
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

