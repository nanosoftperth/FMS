Namespace DataObjects
    Public Class tblRunDates
#Region "Properties / enums"
        Public Property DRid As Integer
        Public Property ApplicationID As System.Nullable(Of System.Guid)
        Public Property Rid As Integer
        Public Property DateOfRun As Date
        Public Property Driver As System.Nullable(Of Integer)
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
                Me.Driver = .Driver
            End With
        End Sub
#End Region

#Region "CRUD"
        Public Shared Sub Create(Run As DataObjects.tblRunDates)
            With New LINQtoSQLClassesDataContext
                Dim objRun As New FMS.Business.tblRunDate
                With objRun
                    .ApplicationID = ThisSession.ApplicationID
                    .Rid = Run.Rid
                    .DateOfRun = Run.DateOfRun
                    .Driver = Run.Driver
                End With

                .tblRunDates.InsertOnSubmit(objRun)
                .SubmitChanges()
                .Dispose()
            End With

        End Sub
        Public Shared Sub Update(Run As DataObjects.tblRunDates)
            With New LINQtoSQLClassesDataContext
                Dim objRun As FMS.Business.tblRunDate = (From r In .tblRunDates
                                                         Where r.Rid.Equals(Run.Rid) And r.ApplicationID.Equals(ThisSession.ApplicationID)).SingleOrDefault
                With objRun
                    .Rid = Run.Rid
                    .DateOfRun = Run.DateOfRun
                    .Driver = Run.Driver
                End With

                .SubmitChanges()
                .Dispose()

            End With

        End Sub
        Public Shared Sub UpdateByRunIDAndDrid(Run As DataObjects.tblRunDates)
            With New LINQtoSQLClassesDataContext
                Dim objRun As FMS.Business.tblRunDate = (From r In .tblRunDates
                                                         Where r.Rid.Equals(Run.Rid) And r.DRid.Equals(Run.DRid) And r.ApplicationID.Equals(ThisSession.ApplicationID)).SingleOrDefault
                With objRun
                    .Rid = Run.Rid
                    .DateOfRun = Run.DateOfRun
                    .Driver = Run.Driver
                End With

                .SubmitChanges()
                .Dispose()

            End With

        End Sub
        Public Shared Sub Delete(Run As DataObjects.tblRunDates)
            With New LINQtoSQLClassesDataContext
                Dim objRun As FMS.Business.tblRunDate = (From r In .tblRunDates
                                                         Where r.DRid.Equals(Run.DRid) And r.ApplicationID.Equals(ThisSession.ApplicationID)).SingleOrDefault
                .tblRunDates.DeleteOnSubmit(objRun)
                .SubmitChanges()
                .Dispose()
            End With

        End Sub
#End Region

#Region "Extended CRUD"
        Public Shared Sub DeleteRunDate(Run As DataObjects.tblRunDates)
            With New LINQtoSQLClassesDataContext
                Dim objRun As FMS.Business.tblRunDate = (From r In .tblRunDates
                                                         Where r.Driver.Equals(Run.Driver) And r.ApplicationID.Equals(ThisSession.ApplicationID) _
                                                             And r.DateOfRun.Equals(Run.DateOfRun)).SingleOrDefault
                .tblRunDates.DeleteOnSubmit(objRun)
                .SubmitChanges()
                .Dispose()
            End With

        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetRunDatesByRunID(rid As Integer) As List(Of DataObjects.tblRunDates)
            Try
                Dim objRunDates As New List(Of DataObjects.tblRunDates)

                With New LINQtoSQLClassesDataContext
                    objRunDates = (From c In .tblRunDates
                                   Where c.Rid.Equals(rid)
                                   Select New DataObjects.tblRunDates(c)).ToList
                    .Dispose()
                End With

                Return objRunDates

            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
    End Class

End Namespace


