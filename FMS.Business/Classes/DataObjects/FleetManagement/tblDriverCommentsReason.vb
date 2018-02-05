Namespace DataObjects
    Public Class tblDriverCommentsReason

#Region "Properties / enums"
        Public Property ApplicationId As Guid
        Public Property Aid As Integer
        Public Property CommentDescription As String

#End Region

#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objTbl As FMS.Business.tblDriverCommentsReason)
            With objTbl
                Me.ApplicationId = .ApplicationId
                Me.Aid = .Aid
                Me.CommentDescription = .CommentDescription
            End With
        End Sub
#End Region

#Region "CRUD"
        Public Shared Sub Create(dvr As DataObjects.tblDriverCommentsReason)
            With New LINQtoSQLClassesDataContext
                Dim appID = ThisSession.ApplicationID
                Dim obj As New FMS.Business.tblDriverCommentsReason
                With obj
                    .ApplicationId = appID
                    .CommentDescription = dvr.CommentDescription

                End With

                .tblDriverCommentsReasons.InsertOnSubmit(obj)
                .SubmitChanges()
                .Dispose()

            End With

        End Sub
        Public Shared Sub Update(dvr As DataObjects.tblDriverCommentsReason)
            With New LINQtoSQLClassesDataContext
                Dim appID = ThisSession.ApplicationID
                Dim obj As FMS.Business.tblDriverCommentsReason = (From d In .tblDriverCommentsReasons
                                                                   Where d.Aid.Equals(dvr.Aid) And d.ApplicationId = appID).SingleOrDefault
                With obj
                    .CommentDescription = dvr.CommentDescription
                End With

                .SubmitChanges()
                .Dispose()

            End With

        End Sub
        Public Shared Sub Delete(dvr As DataObjects.tblDriverCommentsReason)
            With New LINQtoSQLClassesDataContext
                Dim appID = ThisSession.ApplicationID
                Dim obj As FMS.Business.tblDriverCommentsReason = (From d In .tblDriverCommentsReasons
                                                                   Where d.Aid.Equals(dvr.Aid) And d.ApplicationId.Equals(appID)).SingleOrDefault

                .tblDriverCommentsReasons.DeleteOnSubmit(obj)
                .SubmitChanges()
                .Dispose()

            End With

        End Sub
#End Region

#Region "Get methods"
        Public Shared Function GetAllPerApplication() As List(Of DataObjects.tblDriverCommentsReason)
            Try
                Dim appID = ThisSession.ApplicationID
                Dim obj As New List(Of DataObjects.tblDriverCommentsReason)
                With New LINQtoSQLClassesDataContext
                    obj = (From d In .tblDriverCommentsReasons
                           Where d.ApplicationId = appID
                           Order By d.CommentDescription
                           Select New DataObjects.tblDriverCommentsReason(d)).ToList
                End With
                Return obj

            Catch ex As Exception
                Throw ex
            End Try

        End Function
#End Region

    End Class

End Namespace


