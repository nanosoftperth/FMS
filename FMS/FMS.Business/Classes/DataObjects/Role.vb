
Namespace DataObjects
    Public Class Role

#Region "Properties"

        Public Property ApplicationID As Guid
        Public Property Name As String
        Public Property RoleID As Guid
        ' Public Property Users As List(Of DataObjects.User)
        Public Property Description As String

#End Region

#Region "CRUD"
        Public Shared Function insert(r As Role) As Guid

            Dim applicationName As String = (From a In SingletonAccess.FMSDataContextContignous.aspnet_Applications Where a.ApplicationId = r.ApplicationID).Single.ApplicationName
            SingletonAccess.FMSDataContextContignous.aspnet_Roles_CreateRole(applicationName, r.Name, r.Description)

            Dim app As DataObjects.Application = DataObjects.Application.GetFromApplicationName(applicationName)

            Return DataObjects.Role.GetAllRolesforApplication(app.ApplicationID).Where(Function(x) x.Name = r.Name).Single.RoleID


        End Function


        Public Shared Sub delete(r As Role)

            'find if the role is used anywhere 
            Dim thisr As aspnet_Role = (From i In SingletonAccess.FMSDataContextContignous.aspnet_Roles
                                     Where i.RoleId = r.RoleID).SingleOrDefault

            'if the role doesw not exist, then we willdo nothing (cannot delete it)
            If thisr Is Nothing Then Exit Sub

            'if the role is used , then throw an exception.Hopefully DevExpress will show this niceley
            If thisr.aspnet_UsersInRoles.Count > 0 Then _
                Throw New Exception(String.Format("Cannot delete role ""{0}"" as there are {1} users already using this role." & _
                                    "{2}Please remove users from this role before tryingto delete again" _
                                     , r.Name, thisr.aspnet_UsersInRoles.Count, vbNewLine))

            'if the role is not used , then delete it!
            SingletonAccess.FMSDataContextContignous.aspnet_Roles.DeleteOnSubmit(thisr)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub

        Public Shared Sub Update(r As Role)

            Dim thisRole As aspnet_Role = (From x In SingletonAccess.FMSDataContextContignous.aspnet_Roles
                                            Where x.RoleId = r.RoleID).SingleOrDefault

            If thisRole Is Nothing Then _
                Throw New Exception(String.Format("The role ""{0}"" does not exist, so cannot be updated", r.Name))

            With thisRole
                .RoleName = r.Name
                .LoweredRoleName = r.Name.ToLower
                .Description = r.Description
            End With

            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub
#End Region

#Region "Constructors"

        Public Sub New()

        End Sub

        Public Sub New(r As aspnet_Role)

            With r
                Me.Name = .RoleName
                Me.RoleID = .RoleId
                Me.ApplicationID = .ApplicationId
                Me.Description = .Description
            End With

        End Sub
#End Region

#Region "Get and Sets"
        Public Shared Function GetAllRolesforApplication(appID As Guid) As List(Of Role)

            Dim l As List(Of Role) = (From x In SingletonAccess.FMSDataContextNew.aspnet_Roles
                                        Where x.ApplicationId = appID
                                        Select New DataObjects.Role(x)).ToList
            Return l

        End Function

        ''' <summary>
        ''' bitwise operation from object "FeatureListConstants"
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetFeaturesBitwise() As Integer

            Dim features As List(Of DataObjects.Feature) = _
                                (From x In SingletonAccess.FMSDataContextNew.ApplicationFeatureRoles
                                  Where x.RoleID = Me.RoleID
                                  Select New Feature(x.Feature)).ToList

            Dim bitwiseint As Integer

            For Each f As DataObjects.Feature In features
                bitwiseint = (bitwiseint Or f.BitWiseID)
            Next

            Return bitwiseint

        End Function

        Public Shared Function GetUsersForRole(rlID As Guid) As List(Of DataObjects.User)

            'find if the role is used anywhere 
            Dim thisr As aspnet_Role = (From i In SingletonAccess.FMSDataContextNew.aspnet_Roles
                                     Where i.RoleId = rlID).Single

            Return (From i In thisr.aspnet_UsersInRoles _
                                Select New User(i.aspnet_User)).ToList

        End Function

#End Region

    End Class

End Namespace

