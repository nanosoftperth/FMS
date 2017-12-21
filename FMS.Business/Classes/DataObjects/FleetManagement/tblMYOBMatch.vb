Namespace DataObjects
    Public Class tblMYOBMatch
#Region "Properties / enums"
        Public Property Aid As String
        Public Property MYOBId As String
        Public Property CustomerName As String
        Public Property ImportedCustomerName As String

#End Region

#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objMTYOB As FMS.Business.tblMYOBMatch)
            With objMTYOB
                Me.Aid = .Aid
                Me.MYOBId = .MYOBId
                Me.CustomerName = .CustomerName
                Me.ImportedCustomerName = .ImportedCustomerName

            End With
        End Sub
#End Region

#Region "CRUD"
        Public Shared Sub Create(myob As DataObjects.tblMYOBMatch)
            Dim oMYOB As New FMS.Business.tblMYOBMatch
            With oMYOB
                .Aid = myob.Aid
                .MYOBId = myob.MYOBId
                .CustomerName = myob.CustomerName
                .ImportedCustomerName = myob.ImportedCustomerName
            End With
            SingletonAccess.FMSDataContextContignous.tblMYOBMatches.InsertOnSubmit(oMYOB)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(myob As DataObjects.tblMYOBMatch)
            Dim oMYOB As FMS.Business.tblMYOBMatch = (From m In SingletonAccess.FMSDataContextContignous.tblMYOBMatches
                                                        Where m.Aid.Equals(myob.Aid)).SingleOrDefault
            With oMYOB
                .Aid = myob.Aid
                .MYOBId = myob.MYOBId
                .CustomerName = myob.CustomerName
                .ImportedCustomerName = myob.ImportedCustomerName
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(myob As DataObjects.tblMYOBMatch)
            Dim id As Integer = myob.Aid
            Dim oMYOB As FMS.Business.tblMYOBMatch = (From m In SingletonAccess.FMSDataContextContignous.tblMYOBMatches
                                                        Where m.Aid = id).SingleOrDefault
            SingletonAccess.FMSDataContextContignous.tblMYOBMatches.DeleteOnSubmit(oMYOB)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region

#Region "Extended CRUD"
        Public Shared Sub DeleteAll()
            Dim oMYOB As IEnumerable(Of FMS.Business.tblMYOBMatch) = (From m In SingletonAccess.FMSDataContextContignous.tblMYOBMatches()
                                            Select New DataObjects.tblMYOBMatch(m)).ToList()

            SingletonAccess.FMSDataContextContignous.tblMYOBMatches.DeleteAllOnSubmit(oMYOB)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region

#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblMYOBMatch)
            Dim oMYOB = (From m In SingletonAccess.FMSDataContextContignous.tblMYOBMatches
                            Order By m.CustomerName
                            Select New DataObjects.tblMYOBMatch(m)).ToList
            Return oMYOB
        End Function

#End Region

    End Class

End Namespace


