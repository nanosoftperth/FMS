Namespace DataObjects
    Public Class Cannon_Run
#Region "Properties / enums"
        Public Property RunID As System.Guid
        Public Property RunName As String        
#End Region

#Region "CRUD"
        Public Shared Sub Create(Run As DataObjects.Cannon_Run)
            Dim cannonRun As New FMS.Business.Cannon_Run
            With cannonRun
                .RunID = Guid.NewGuid
                .RunName = Run.RunName
            End With
            SingletonAccess.FMSDataContextContignous.Cannon_Runs.InsertOnSubmit(cannonRun)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(Run As DataObjects.Cannon_Run)
            Dim cannonRun As FMS.Business.Cannon_Run = (From i In SingletonAccess.FMSDataContextContignous.Cannon_Runs
                                                        Where i.RunID.Equals(Run.RunID)).SingleOrDefault

            With cannonRun
                .RunID = Run.RunID
                .RunName = Run.RunName
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(Run As DataObjects.Cannon_Run)
            Dim RunId As System.Guid = Run.RunID
            Dim cannonRun As FMS.Business.Cannon_Run = (From i In SingletonAccess.FMSDataContextContignous.Cannon_Runs
                                                        Where i.RunID = RunId).SingleOrDefault
            SingletonAccess.FMSDataContextContignous.Cannon_Runs.DeleteOnSubmit(cannonRun)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region

#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.Cannon_Run)
            Dim cannonRuns = (From i In SingletonAccess.FMSDataContextContignous.Cannon_Runs
                             Select New DataObjects.Cannon_Run(i)).ToList()
            Return cannonRuns
        End Function
#End Region

#Region "Constructors"
        Public Sub New()

        End Sub

        Public Sub New(objCannonRun As FMS.Business.Cannon_Run)
            With objCannonRun
                Me.RunID = .RunID
                Me.RunName = .RunName                
            End With
        End Sub
#End Region
    End Class
End Namespace

