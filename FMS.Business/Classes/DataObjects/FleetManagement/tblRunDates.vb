Namespace DataObjects
    Public Class tblRunDates
#Region "Properties / enums"
        Public Property DRid As Integer
        Public Property ApplicationID As System.Nullable(Of System.Guid)
        Public Property Rid As Integer
        Public Property DateOfRun As Date

#End Region

#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objRun As FMS.Business.tblRunDate)
            With objRun
                Me.DRid = .DRid
                Me.ApplicationID = .ApplicationID
                Me.Rid = .Rid
                Me.DateOfRun = .DateOfRun

            End With
        End Sub
#End Region

#Region "CRUD"
        Public Shared Sub Create(Run As DataObjects.tblRunDates)
            Dim objRun As New FMS.Business.tblRunDate
            With objRun
                .ApplicationID = ThisSession.ApplicationID
                .Rid = Run.Rid
                .DateOfRun = Run.DateOfRun

            End With
            SingletonAccess.FMSDataContextContignous.tblRunDates.InsertOnSubmit(objRun)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(Run As DataObjects.tblRunDates)
            Dim objRun As FMS.Business.tblRunDate = (From r In SingletonAccess.FMSDataContextContignous.tblRunDates
                                                     Where r.Rid.Equals(Run.Rid) And r.ApplicationID.Equals(ThisSession.ApplicationID)).SingleOrDefault
            With objRun
                .Rid = Run.Rid
                .DateOfRun = Run.DateOfRun

            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(Run As DataObjects.tblRunDates)
            Dim objRun As FMS.Business.tblRunDate = (From r In SingletonAccess.FMSDataContextContignous.tblRunDates
                                                     Where r.DRid.Equals(Run.DRid) And r.ApplicationID.Equals(ThisSession.ApplicationID)).SingleOrDefault
            SingletonAccess.FMSDataContextContignous.tblRunDates.DeleteOnSubmit(objRun)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region

    End Class

End Namespace


