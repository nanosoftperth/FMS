Namespace DataObjects
    Public Class tblRuns
#Region "Properties / enums"
        Public Property RunID As System.Guid
        Public Property ApplicationID As System.Nullable(Of System.Guid)
        Public Property Rid As Integer
        Public Property RunNUmber As System.Nullable(Of Integer)
        Public Property RunDescription As String
        Public Property RunDriver As System.Nullable(Of Integer)
        Public Property MondayRun As Boolean
        Public Property TuesdayRun As Boolean
        Public Property WednesdayRun As Boolean
        Public Property ThursdayRun As Boolean
        Public Property FridayRun As Boolean
        Public Property SaturdayRun As Boolean
        Public Property SundayRun As Boolean
        Public Property InactiveRun As Boolean
        Public Property Notes As String
#End Region
#Region "CRUD"
        Public Shared Sub Create(Run As DataObjects.tblRuns)
            With New LINQtoSQLClassesDataContext

                Dim objRun As New FMS.Business.tblRun
                With objRun
                    .RunID = Guid.NewGuid
                    .ApplicationID = ThisSession.ApplicationID
                    .Rid = tblProjectID.RunIDCreateOrUpdate(ThisSession.ApplicationID)
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
                    .Notes = Run.Notes
                End With

                .tblRuns.InsertOnSubmit(objRun)
                .SubmitChanges()
                .Dispose()

            End With

        End Sub
        Public Shared Sub Update(Run As DataObjects.tblRuns)
            With New LINQtoSQLClassesDataContext

                Dim objRun As FMS.Business.tblRun = (From c In .tblRuns
                                                     Where c.Rid.Equals(Run.Rid) And c.ApplicationID.Equals(ThisSession.ApplicationID)).SingleOrDefault
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
                    .Notes = Run.Notes
                End With

                .SubmitChanges()
                .Dispose()

            End With

        End Sub
        Public Shared Sub Delete(Run As DataObjects.tblRuns)
            With New LINQtoSQLClassesDataContext
                Dim objRun As FMS.Business.tblRun = (From c In .tblRuns
                                                     Where c.Rid.Equals(Run.Rid) And c.ApplicationID.Equals(ThisSession.ApplicationID)).SingleOrDefault
                .tblRuns.DeleteOnSubmit(objRun)
                .SubmitChanges()
                .Dispose()
            End With

        End Sub
#End Region

#Region "Extended CRUD"
        Public Shared Sub ChangeRun(FromDriver As Integer, ToDriver As Integer)
            With New LINQtoSQLClassesDataContext
                .usp_UpdateRunsBasedOnRunDriver(FromDriver, ToDriver)
                .Dispose()
            End With
        End Sub
        Public Shared Sub ChangeRun(RunDriver As Integer, RunNumber As Integer, Run As DataObjects.tblRuns)
            With New LINQtoSQLClassesDataContext
                Dim objRun As FMS.Business.tblRun = (From c In .tblRuns
                                                     Where c.RunDriver.Equals(RunDriver) And c.ApplicationID.Equals(ThisSession.ApplicationID) _
                                                         And c.RunNUmber.Equals(RunNumber)).SingleOrDefault
                With objRun
                    .RunNUmber = Run.RunNUmber
                    .RunDescription = Run.RunDescription

                End With

                .SubmitChanges()
                .Dispose()

            End With

        End Sub

        Public Shared Sub DeleteRun(Run As DataObjects.tblRuns)
            With New LINQtoSQLClassesDataContext
                Dim objRun As FMS.Business.tblRun = (From c In .tblRuns
                                                     Where c.RunDriver.Equals(Run.RunDriver) And c.ApplicationID.Equals(ThisSession.ApplicationID) _
                                                         And c.RunNUmber.Equals(Run.RunNUmber)).SingleOrDefault
                .tblRuns.DeleteOnSubmit(objRun)
                .SubmitChanges()
                .Dispose()

            End With

        End Sub
        Public Shared Sub DeleteRunByRid(Run As DataObjects.tblRuns)
            With New LINQtoSQLClassesDataContext
                Dim objRun As FMS.Business.tblRun = (From c In .tblRuns
                                                     Where c.Rid.Equals(Run.Rid) And c.ApplicationID.Equals(ThisSession.ApplicationID)).SingleOrDefault
                .tblRuns.DeleteOnSubmit(objRun)
                .SubmitChanges()
                .Dispose()

            End With

        End Sub

#End Region

#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblRuns)
            Try
                Dim objRun As New List(Of DataObjects.tblRuns)

                With New LINQtoSQLClassesDataContext
                    objRun = (From c In .tblRuns
                              Where c.ApplicationID.Equals(ThisSession.ApplicationID)
                              Order By c.RunDescription
                              Select New DataObjects.tblRuns(c)).ToList
                    .Dispose()
                End With
                Return objRun

            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Shared Function GetTblRuns() As List(Of DataObjects.tblRuns)
            Try
                Dim objRun As New List(Of DataObjects.tblRuns)
                With New LINQtoSQLClassesDataContext
                    objRun = (From c In .tblRuns
                              Order By c.RunDescription
                              Where c.RunDescription IsNot Nothing And c.ApplicationID.Equals(ThisSession.ApplicationID)
                              Select New DataObjects.tblRuns(c)).ToList
                    .Dispose()
                End With
                Return objRun

            Catch ex As Exception
                Throw ex
            End Try


        End Function
        Public Shared Function GetTblRunByRId(ByVal Rid As Integer) As DataObjects.tblRuns
            Try
                Dim objRun As New DataObjects.tblRuns
                With New LINQtoSQLClassesDataContext
                    objRun = (From c In .tblRuns
                              Order By c.RunDescription
                              Where c.RunDescription IsNot Nothing And c.ApplicationID.Equals(ThisSession.ApplicationID) And c.Rid.Equals(Rid)
                              Select New DataObjects.tblRuns(c)).FirstOrDefault
                    .Dispose()
                End With
                Return objRun

            Catch ex As Exception
                Throw ex
            End Try


        End Function
        Public Shared Function GetTblRunByRunNumberRunDescription(ByVal RunNumber As Integer, ByVal RunDescription As String) As DataObjects.tblRuns
            Try
                Dim objRun As New DataObjects.tblRuns
                With New LINQtoSQLClassesDataContext
                    objRun = (From c In .tblRuns
                              Order By c.RunDescription
                              Where c.RunNUmber.Equals(RunNumber) And c.RunDescription.Equals(RunDescription) And c.ApplicationID.Equals(ThisSession.ApplicationID)
                              Select New DataObjects.tblRuns(c)).FirstOrDefault
                    .Dispose()
                End With
                Return objRun

            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Shared Function GetExistingRunWithRundate(ByVal RunNumber As Integer, ByVal RunDescription As String) As Boolean
            Try
                'Dim objRun As New DataObjects.tblRuns
                'With New LINQtoSQLClassesDataContext
                '    objRun = (From r In .tblRuns
                '              Join rd In .tblRunDates
                '                  On r.Rid Equals rd.Rid
                '              Where r.ApplicationID.Equals(ThisSession.ApplicationID) And r.RunNUmber = RunNumber And r.RunDescription = RunDescription
                '             Select Case New DataObjects.tblRuns(r)
                '    .Dispose()
                'End With

                Dim objRun As New DataObjects.tblRuns
                With New LINQtoSQLClassesDataContext
                    objRun = (From r In .tblRuns
                              Join rd In .tblRunDates
                                  On r.Rid Equals rd.Rid
                              Where r.RunNUmber.Equals(RunNumber) And r.RunDescription.Equals(RunDescription) And r.ApplicationID.Equals(ThisSession.ApplicationID)
                              Select New DataObjects.tblRuns(r)).FirstOrDefault
                    .Dispose()
                End With

                If (objRun IsNot Nothing) Then
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                Throw ex
            End Try

            'Dim petOwnersJoin = From pers In people
            '                    Join pet In pets
            '                    On pet.Owner Equals pers
            '                    Select pers.FirstName, PetName = pet.Name


        End Function


#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objRun As FMS.Business.tblRun)
            With objRun
                Me.RunID = .RunID
                Me.ApplicationID = .ApplicationID
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
                Me.Notes = .Notes
            End With
        End Sub
#End Region
    End Class
End Namespace

