Namespace DataObjects
    Public Class tblCustomerAgent
#Region "Properties / enums"
        Public Property CustomerAgentID As System.Guid
        Public Property AID As Integer
        Public Property CustomerAgentName As String
#End Region
#Region "CRUD"
        Public Shared Sub Create(CustomerAgent As DataObjects.tblCustomerAgent)
            Dim objCustomerAgent As New FMS.Business.tblCustomerAgent
            With objCustomerAgent
                .CustomerAgentID = Guid.NewGuid
                .AID = GetLastIDUsed() + 1
                .CustomerAgentName = CustomerAgent.CustomerAgentName
            End With
            SingletonAccess.FMSDataContextContignous.tblCustomerAgents.InsertOnSubmit(objCustomerAgent)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(CustomerAgent As DataObjects.tblCustomerAgent)
            Dim objCustomerAgent As FMS.Business.tblCustomerAgent = (From c In SingletonAccess.FMSDataContextContignous.tblCustomerAgents
                                                           Where c.CustomerAgentID.Equals(CustomerAgent.CustomerAgentID)).SingleOrDefault
            With objCustomerAgent
                .CustomerAgentName = CustomerAgent.CustomerAgentName
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(CustomerAgent As DataObjects.tblCustomerAgent)
            Dim objCustomerAgent As FMS.Business.tblCustomerAgent = (From c In SingletonAccess.FMSDataContextContignous.tblCustomerAgents
                                                         Where c.CustomerAgentID.Equals(CustomerAgent.CustomerAgentID)).SingleOrDefault
            SingletonAccess.FMSDataContextContignous.tblCustomerAgents.DeleteOnSubmit(objCustomerAgent)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region
#Region "Get methods"
        Private Shared Function GetLastIDUsed() As Integer
            Dim objCustomerAgents = SingletonAccess.FMSDataContextContignous.tblCustomerAgents.Count
                               
            Return objCustomerAgents
        End Function
        Public Shared Function GetAll() As List(Of DataObjects.tblCustomerAgent)
            Dim objCustomerAgent = (From c In SingletonAccess.FMSDataContextContignous.tblCustomerAgents
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
                Me.CustomerAgentName = .CustomerAgentName
            End With
        End Sub
#End Region
    End Class
End Namespace

