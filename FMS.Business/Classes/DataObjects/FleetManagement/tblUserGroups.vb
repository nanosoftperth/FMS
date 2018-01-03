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
            Dim obj As New FMS.Business.tblUserGroup
            With obj
                .ApplicationId = grp.ApplicationId
                .GroupId = Guid.NewGuid
                .UserGroup = grp.UserGroup

            End With
            SingletonAccess.FMSDataContextContignous.tblUserGroups.InsertOnSubmit(obj)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(grp As DataObjects.tblUserGroups)
            Dim obj As FMS.Business.tblUserGroup = (From g In SingletonAccess.FMSDataContextContignous.tblUserGroups
                                                    Where g.GroupId.Equals(grp.GroupId) And g.ApplicationId.Equals(grp.ApplicationId)).SingleOrDefault
            With obj
                .UserGroup = grp.UserGroup
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(sec As DataObjects.tblUserSecurity)
            Dim obj As FMS.Business.tblUserSecurity = (From u In SingletonAccess.FMSDataContextContignous.tblUserSecurities
                                                       Where u.usersecID.Equals(sec.usersecID) And u.ApplicationId.Equals(sec.ApplicationId)).SingleOrDefault
            SingletonAccess.FMSDataContextContignous.tblUserSecurities.DeleteOnSubmit(obj)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region

#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblUserGroups)
            Dim obj = (From u In SingletonAccess.FMSDataContextContignous.tblUserGroups
                       Order By u.UserGroup
                       Select New DataObjects.tblUserGroups(u)).ToList

            Return obj
        End Function

        Public Shared Function GetAllPerApplication(AppID As Guid) As List(Of DataObjects.tblUserGroups)
            Dim obj = (From u In SingletonAccess.FMSDataContextContignous.tblUserGroups
                       Where u.ApplicationId = AppID
                       Order By u.UserGroup
                       Select New DataObjects.tblUserGroups(u)).ToList

            Return obj
        End Function

#End Region

    End Class

End Namespace


