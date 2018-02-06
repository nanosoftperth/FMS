Namespace DataObjects

    Public Class tblUserGroups

#Region "Properties / enums"
        Public Property ApplicationId As System.Guid
        Public Property GroupId As System.Guid
        Public Property UserGroup As String
#End Region

#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objTbl As FMS.Business.tblUserGroup)
            With objTbl
                Me.ApplicationId = .ApplicationId
                Me.GroupId = .GroupId
                Me.UserGroup = .UserGroup

            End With
        End Sub
#End Region

#Region "CRUD"
        Public Shared Sub Create(grp As DataObjects.tblUserGroups)
            With New LINQtoSQLClassesDataContext
                Dim appID = ThisSession.ApplicationID
                Dim obj As New FMS.Business.tblUserGroup

                With obj
                    .ApplicationId = grp.ApplicationId
                    .GroupId = Guid.NewGuid
                    .UserGroup = grp.UserGroup

                End With

                .tblUserGroups.InsertOnSubmit(obj)
                .SubmitChanges()
                .Dispose()

            End With

        End Sub
        Public Shared Sub Update(grp As DataObjects.tblUserGroups)
            With New LINQtoSQLClassesDataContext
                Dim appID = ThisSession.ApplicationID
                Dim obj As FMS.Business.tblUserGroup = (From g In .tblUserGroups
                                                        Where g.GroupId.Equals(grp.GroupId) And g.ApplicationId.Equals(grp.ApplicationId)).SingleOrDefault
                With obj
                    .UserGroup = grp.UserGroup
                End With
                .SubmitChanges()
                .Dispose()

            End With

        End Sub
        Public Shared Sub Delete(sec As DataObjects.tblUserSecurity)
            With New LINQtoSQLClassesDataContext
                Dim obj As FMS.Business.tblUserSecurity = (From u In .tblUserSecurities
                                                           Where u.usersecID.Equals(sec.usersecID) And u.ApplicationId.Equals(sec.ApplicationId)).SingleOrDefault
                .tblUserSecurities.DeleteOnSubmit(obj)
                .SubmitChanges()
                .Dispose()
            End With

        End Sub
        Public Shared Sub DeleteGroup(sec As DataObjects.tblUserGroups)
            With New LINQtoSQLClassesDataContext
                Dim obj As FMS.Business.tblUserGroup = (From u In .tblUserGroups
                                                        Where u.GroupId.Equals(sec.GroupId) And u.ApplicationId.Equals(sec.ApplicationId)).SingleOrDefault
                .tblUserGroups.DeleteOnSubmit(obj)
                .SubmitChanges()
                .Dispose()
            End With

        End Sub
#End Region

#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblUserGroups)
            Try
                Dim obj As New List(Of DataObjects.tblUserGroups)

                With New LINQtoSQLClassesDataContext
                    obj = (From u In .tblUserGroups
                           Order By u.UserGroup
                           Select New DataObjects.tblUserGroups(u)).ToList
                    .Dispose()
                End With
                Return obj

            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Shared Function GetAllPerApplication(AppID As Guid) As List(Of DataObjects.tblUserGroups)
            Try
                Dim obj As New List(Of DataObjects.tblUserGroups)

                With New LINQtoSQLClassesDataContext
                    obj = (From u In .tblUserGroups
                           Where u.ApplicationId = AppID
                           Order By u.UserGroup
                           Select New DataObjects.tblUserGroups(u)).ToList
                    .Dispose()
                End With

                Return obj

            Catch ex As Exception
                Throw ex
            End Try

        End Function

#End Region

    End Class

End Namespace


