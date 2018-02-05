Namespace DataObjects
    Public Class tblDriverComments

#Region "Properties / enums"
        Public Property ApplicationId As Guid
        Public Property Aid As Integer
        Public Property Did As System.Nullable(Of Integer)
        Public Property CommentDate As System.Nullable(Of Date)
        Public Property CommentReason As System.Nullable(Of Integer)
        Public Property Comments As String

#End Region

#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objTbl As FMS.Business.tblDriverComment)
            With objTbl
                Me.ApplicationId = .ApplicationId
                Me.Aid = .Aid
                Me.Did = .Did
                Me.CommentDate = .CommentDate
                Me.CommentReason = .CommentReason
                Me.Comments = .Comments

            End With
        End Sub
#End Region

#Region "CRUD"
        Public Shared Sub Create(dvr As DataObjects.tblDriverComments)
            With New LINQtoSQLClassesDataContext
                Dim appID = ThisSession.ApplicationID
                Dim obj As New FMS.Business.tblDriverComment

                With obj
                    .ApplicationId = appID
                    .Did = dvr.Did
                    .CommentDate = dvr.CommentDate
                    .CommentReason = dvr.CommentReason
                    .Comments = dvr.Comments
                End With

                .tblDriverComments.InsertOnSubmit(obj)
                .SubmitChanges()
                .Dispose()

            End With

        End Sub
        Public Shared Sub Update(dvr As DataObjects.tblDriverComments)
            With New LINQtoSQLClassesDataContext
                Dim appID = ThisSession.ApplicationID
                Dim obj As FMS.Business.tblDriverComment = (From d In .tblDriverComments
                                                            Where d.Aid.Equals(dvr.Aid) And d.ApplicationId = appID).SingleOrDefault
                With obj
                    .CommentDate = dvr.CommentDate
                    .CommentReason = dvr.CommentReason
                    .Comments = dvr.Comments

                End With

                .SubmitChanges()
                .Dispose()

            End With

        End Sub
        Public Shared Sub Delete(dvr As DataObjects.tblDriverComments)
            With New LINQtoSQLClassesDataContext
                Dim appID = ThisSession.ApplicationID
                Dim obj As FMS.Business.tblDriverComment = (From d In .tblDriverComments
                                                            Where d.Aid.Equals(dvr.Aid) And d.ApplicationId.Equals(appID)).SingleOrDefault
                .tblDriverComments.DeleteOnSubmit(obj)
                .SubmitChanges()
                .Dispose()

            End With

        End Sub
#End Region

#Region "Get methods"
        Public Shared Function GetAllPerApplication() As List(Of DataObjects.tblDriverComments)

            Try
                Dim appID = ThisSession.ApplicationID
                Dim obj As New List(Of DataObjects.tblDriverComments)
                With New LINQtoSQLClassesDataContext
                    obj = (From d In .tblDriverComments
                           Where d.ApplicationId = appID
                           Order By d.CommentDate
                           Select New DataObjects.tblDriverComments(d)).ToList
                End With
                Return obj

            Catch ex As Exception
                Throw ex
            End Try

        End Function
#End Region

    End Class

End Namespace


