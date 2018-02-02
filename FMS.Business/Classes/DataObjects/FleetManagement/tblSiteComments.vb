Namespace DataObjects
    Public Class tblSiteComments
#Region "Properties / enums"
        Public Property CommentsID As System.Guid
        Public Property Aid As Integer
        Public Property Cid As System.Nullable(Of Short)
        Public Property CommentDate As System.Nullable(Of Date)
        Public Property Comments As String
#End Region
#Region "CRUD"
        Public Shared Sub Create(SiteComments As DataObjects.tblSiteComments)
            With New LINQtoSQLClassesDataContext
                Dim objSiteComment As New FMS.Business.tblSiteComment
                With objSiteComment
                    .CommentsID = Guid.NewGuid
                    .Cid = SiteComments.Cid
                    .CommentDate = SiteComments.CommentDate
                    .Comments = SiteComments.Comments
                End With
                .tblSiteComments.InsertOnSubmit(objSiteComment)
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub Update(SiteComments As DataObjects.tblSiteComments)
            With New LINQtoSQLClassesDataContext
                Dim objSiteComment As FMS.Business.tblSiteComment = (From c In .tblSiteComments
                                                                     Where c.CommentsID.Equals(SiteComments.CommentsID)).SingleOrDefault
                With objSiteComment
                    .Cid = SiteComments.Cid
                    .CommentDate = SiteComments.CommentDate
                    .Comments = SiteComments.Comments
                End With
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub Delete(SiteComments As DataObjects.tblSiteComments)
            With New LINQtoSQLClassesDataContext
                Dim objSiteComment As FMS.Business.tblSiteComment = (From c In .tblSiteComments
                                                                     Where c.CommentsID.Equals(SiteComments.CommentsID)).SingleOrDefault
                .tblSiteComments.DeleteOnSubmit(objSiteComment)
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblSiteComments)
            Try
                Dim objSiteComments As New List(Of DataObjects.tblSiteComments)
                With New LINQtoSQLClassesDataContext
                    objSiteComments = (From c In .tblSiteComments
                                       Order By c.Comments
                                       Select New DataObjects.tblSiteComments(c)).ToList
                    .Dispose()
                End With
                Return objSiteComments
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Shared Function GetAllByCID(cid As Short) As List(Of DataObjects.tblSiteComments)
            Try
                Dim objSiteComments As New List(Of DataObjects.tblSiteComments)
                With New LINQtoSQLClassesDataContext
                    objSiteComments = (From c In .tblSiteComments
                                       Where c.Cid.Equals(cid)
                                       Order By c.Comments
                                       Select New DataObjects.tblSiteComments(c)).ToList
                End With
                Return objSiteComments
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objSiteComment As FMS.Business.tblSiteComment)
            With objSiteComment
                Me.CommentsID = .CommentsID
                Me.Aid = .Aid
                Me.Cid = .Cid
                Me.CommentDate = .CommentDate
                Me.Comments = .Comments
            End With
        End Sub
#End Region
    End Class
End Namespace
