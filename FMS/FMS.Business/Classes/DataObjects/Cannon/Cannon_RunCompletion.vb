Namespace DataObjects
    Public Class Cannon_RunCompletion
#Region "Properties / enums"
        Public Property RunCompletionID As System.Guid
        Public Property RunID As System.Guid
        Public Property DriverID As System.Nullable(Of System.Guid)
        Public Property DID As System.Nullable(Of Integer)
        Public Property RunDate As System.Nullable(Of Date)
        Public Property Notes As String
#End Region

#Region "CRUD"
        Public Shared Sub Create(RunCompletion As DataObjects.Cannon_RunCompletion)
            Dim cannonRunCompletion As New FMS.Business.Cannon_RunCompletion
            With cannonRunCompletion
                .RunCompletionID = Guid.NewGuid
                .RunID = RunCompletion.RunID
                .DriverID = RunCompletion.DriverID
                .DID = RunCompletion.DID
                .RunDate = RunCompletion.RunDate
                .Notes = RunCompletion.Notes
            End With
            SingletonAccess.FMSDataContextContignous.Cannon_RunCompletions.InsertOnSubmit(cannonRunCompletion)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(RunCompletion As DataObjects.Cannon_RunCompletion)
            Dim cannonRunCompletion As FMS.Business.Cannon_RunCompletion = (From i In SingletonAccess.FMSDataContextContignous.Cannon_RunCompletions
                                                                            Where i.RunCompletionID = RunCompletion.RunCompletionID).SingleOrDefault
            With cannonRunCompletion
                .RunCompletionID = RunCompletion.RunCompletionID
                .RunID = RunCompletion.RunID
                .DriverID = RunCompletion.DriverID
                .DID = RunCompletion.DID
                .RunDate = RunCompletion.RunDate
                .Notes = RunCompletion.Notes
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(RunCompletion As DataObjects.Cannon_RunCompletion)
            Dim RunCompletionId As System.Guid = RunCompletion.RunCompletionID
            Dim cannonRunCompletion As FMS.Business.Cannon_RunCompletion = (From i In SingletonAccess.FMSDataContextContignous.Cannon_RunCompletions
                                                        Where i.RunCompletionID = RunCompletionId).SingleOrDefault
            SingletonAccess.FMSDataContextContignous.Cannon_RunCompletions.DeleteOnSubmit(cannonRunCompletion)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.Cannon_RunCompletion)
            Dim cannonRunCompletion = (From i In SingletonAccess.FMSDataContextContignous.Cannon_RunCompletions
                             Select New DataObjects.Cannon_RunCompletion(i)).ToList()
            Return cannonRunCompletion
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objCannonRunCompletion As FMS.Business.Cannon_RunCompletion)
            With objCannonRunCompletion
                Me.RunCompletionID = .RunCompletionID
                Me.RunID = .RunID
                Me.DriverID = .DriverID
                Me.DID = .DID
                Me.RunDate = .RunDate
                Me.Notes = .Notes
            End With
        End Sub
#End Region
    End Class
End Namespace
