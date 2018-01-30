Namespace DataObjects
    Public Class FleetRun
#Region "Properties / enums"
        Public Property RunID As System.Guid
        Public Property RunName As String
        Public Property KeyNumber As String
#End Region

#Region "CRUD"
        Public Shared Sub Create(Run As DataObjects.FleetRun)
            Dim fleetRun As New FMS.Business.FleetRun
            With fleetRun
                .RunID = Guid.NewGuid
                .RunName = Run.RunName
                .KeyNumber = Run.KeyNumber
            End With
            SingletonAccess.FMSDataContextContignous.FleetRuns.InsertOnSubmit(fleetRun)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(Run As DataObjects.FleetRun)
            Dim fleetRun As FMS.Business.FleetRun = (From i In SingletonAccess.FMSDataContextContignous.FleetRuns
                                                        Where i.RunID.Equals(Run.RunID)).SingleOrDefault

            With fleetRun
                .RunID = Run.RunID
                .RunName = Run.RunName
                .KeyNumber = Run.KeyNumber
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(Run As DataObjects.FleetRun)
            Dim RunId As System.Guid = Run.RunID
            Dim fleetRun As FMS.Business.FleetRun = (From i In SingletonAccess.FMSDataContextContignous.FleetRuns
                                                        Where i.RunID = RunId).SingleOrDefault
            SingletonAccess.FMSDataContextContignous.FleetRuns.DeleteOnSubmit(fleetRun)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            FMS.Business.DataObjects.FleetDocument.DeleteByRunID(RunId)
        End Sub
#End Region

#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.FleetRun)
            Dim fleetRuns = (From i In SingletonAccess.FMSDataContextContignous.FleetRuns
                             Order By i.RunName
                             Select New DataObjects.FleetRun(i)).ToList()
            Return fleetRuns
        End Function
#End Region

#Region "Constructors"
        Public Sub New()

        End Sub

        Public Sub New(objFleetRun As FMS.Business.FleetRun)
            With objFleetRun
                Me.RunID = .RunID
                Me.RunName = .RunName
                Me.KeyNumber = .KeyNumber
            End With
        End Sub
#End Region
    End Class
End Namespace

