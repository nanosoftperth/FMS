Namespace DataObjects
    Public Class CUST

#Region "Properties / enums"
        Public Property ID As System.Nullable(Of Integer)
        Public Property CardID As String
        Public Property CustomerName As String

#End Region

#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objCust As FMS.Business.CUST)
            With objCust
                Me.ID = .ID
                Me.CardID = .CardID
                Me.CustomerName = .CustomerName

            End With
        End Sub
#End Region

#Region "CRUD"
        Public Shared Sub Create(cust As DataObjects.CUST)
            Dim oCust As New FMS.Business.CUST
            With oCust
                .ID = cust.ID
                .CardID = cust.CardID
                .CustomerName = cust.CustomerName
            End With
            SingletonAccess.FMSDataContextContignous.CUSTs.InsertOnSubmit(oCust)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(cust As DataObjects.CUST)
            Dim oCust As FMS.Business.CUST = (From c In SingletonAccess.FMSDataContextContignous.CUSTs
                                                        Where c.ID.Equals(cust.ID)).SingleOrDefault
            With oCust
                .ID = cust.ID
                .CardID = cust.CardID
                .CustomerName = cust.CustomerName
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(cust As DataObjects.CUST)
            Dim id As Integer = cust.ID
            Dim oCust As FMS.Business.CUST = (From c In SingletonAccess.FMSDataContextContignous.CUSTs
                                                        Where c.ID = id).SingleOrDefault
            SingletonAccess.FMSDataContextContignous.CUSTs.DeleteOnSubmit(oCust)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region

#Region "Extended CRUD"
        Public Shared Sub DeleteAll()
            Dim oCust As IEnumerable(Of FMS.Business.CUST) = (From c In SingletonAccess.FMSDataContextContignous.CUSTs()
                                            Select New DataObjects.CUST(c)).ToList()

            SingletonAccess.FMSDataContextContignous.CUSTs.DeleteAllOnSubmit(oCust)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region

#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.CUST)
            Dim oCust = (From c In SingletonAccess.FMSDataContextContignous.CUSTs
                            Order By c.CustomerName
                            Select New DataObjects.CUST(c)).ToList
            Return oCust
        End Function

        Public Shared Function GetAllExistInTableCustomer() As List(Of DataObjects.CUST)

            Dim oCust = (From c In SingletonAccess.FMSDataContextContignous.CUSTs
                            Where (From cs In SingletonAccess.FMSDataContextContignous.tblCustomers
                               Select cs.MYOBCustomerNumber).Contains(c.CardID)
                            Select New DataObjects.CUST(c)).ToList()

            Return oCust
        End Function
        
#End Region

    End Class

End Namespace
