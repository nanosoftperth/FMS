Namespace DataObjects
    Public Class tblRuns
#Region "Properties / enums"
        Public Property RunID As System.Guid
        Public Property Rid As Integer
        Public Property RunNUmber As System.Nullable(Of Integer)
        Public Property RunDescription As String
        Public Property RunDriver As System.Nullable(Of Short)
        Public Property MondayRun As Boolean
        Public Property TuesdayRun As Boolean
        Public Property WednesdayRun As Boolean
        Public Property ThursdayRun As Boolean
        Public Property FridayRun As Boolean
        Public Property SaturdayRun As Boolean
        Public Property SundayRun As Boolean
        Public Property InactiveRun As Boolean
#End Region
#Region "CRUD"
        Public Shared Sub Create(Run As DataObjects.tblRuns)
            Dim objRun As New FMS.Business.tblRun
            With objRun
                .RunID = Guid.NewGuid
                .RunNUmber = Run.RunNUmber
                .RunDescription = Run.RunDescription
                .RunDriver = Run.RunDriver
                .MondayRun = Run.MondayRun
                .TuesdayRun = Run.TuesdayRun
                .WednesdayRun = Run.WednesdayRun
                .ThursdayRun = Run.ThursdayRun
                .FridayRun = Run.FridayRun
                .SaturdayRun = Run.SaturdayRun
                .SundayRun = Run.SundayRun
                .InactiveRun = Run.InactiveRun
            End With
            SingletonAccess.FMSDataContextContignous.tblRuns.InsertOnSubmit(objRun)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(Run As DataObjects.tblRuns)
            Dim objRun As FMS.Business.tblRun = (From c In SingletonAccess.FMSDataContextContignous.tblRuns
                                                           Where c.Rid.Equals(Run.Rid)).SingleOrDefault
            With objRun
                .RunNUmber = Run.RunNUmber
                .RunDescription = Run.RunDescription
                .RunDriver = Run.RunDriver
                .MondayRun = Run.MondayRun
                .TuesdayRun = Run.TuesdayRun
                .WednesdayRun = Run.WednesdayRun
                .ThursdayRun = Run.ThursdayRun
                .FridayRun = Run.FridayRun
                .SaturdayRun = Run.SaturdayRun
                .SundayRun = Run.SundayRun
                .InactiveRun = Run.InactiveRun
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(Run As DataObjects.tblRuns)
            Dim objRun As FMS.Business.tblRun = (From c In SingletonAccess.FMSDataContextContignous.tblRuns
                                                         Where c.Rid.Equals(Run.Rid)).SingleOrDefault
            SingletonAccess.FMSDataContextContignous.tblRuns.DeleteOnSubmit(objRun)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblRuns)
            Dim objRun = (From c In SingletonAccess.FMSDataContextContignous.tblRuns
                            Order By c.RunDescription
                            Select New DataObjects.tblRuns(c)).ToList
            Return objRun
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objRun As FMS.Business.tblRun)
            With objRun
                Me.RunID = .RunID
                Me.Rid = .Rid
                Me.RunNUmber = .RunNUmber
                Me.RunDescription = .RunDescription
                Me.RunDriver = .RunDriver
                Me.MondayRun = .MondayRun
                Me.TuesdayRun = .TuesdayRun
                Me.WednesdayRun = .WednesdayRun
                Me.ThursdayRun = .ThursdayRun
                Me.FridayRun = .FridayRun
                Me.SaturdayRun = .SaturdayRun
                Me.SundayRun = .SundayRun
                Me.InactiveRun = .InactiveRun
            End With
        End Sub
#End Region
    End Class
End Namespace

