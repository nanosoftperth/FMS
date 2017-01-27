Imports System.Web.Security
Imports FMS.Business

Namespace DataObjects

    Public Class User

        Public Property ApplicationID As Guid
        Public Property UserId As Guid
        Public Property UserName As String
        Public Property Email As String
        Public Property Mobile As String
        Public Property TimeZone As DataObjects.TimeZone
        Public Property TimeZoneID As String
        Public Property LastLoggedInDate As Date
        Public Property RoleID As Guid
        Public Property SendEmailtoUserWithDefPass As Boolean

        ''' <summary>
        ''' If this has a null value in the DB backend, then this will return an empty GUID
        ''' this is for binding in the devexpress controlls where it will expect a deault option 
        ''' to be in the list with an ID of guid.empty for "use application default".
        ''' </summary>
        Public Property ApplicationLocationID As Guid


        Public Sub UpdatetimeZone(Optional u As aspnet_User = Nothing)

            If u Is Nothing Then u = SingletonAccess.FMSDataContextNew.aspnet_Users.Where(Function(x) x.UserId = Me.UserId).Single

            Dim tz As TimeZone = DataObjects.TimeZone.GetMSftTimeZonesAutoInheritOption.Where(Function(x) x.ID = u.TimeZoneID).SingleOrDefault
            Me.TimeZoneID = If(tz Is Nothing, "ERROR", tz.ID)
            Me.TimeZone = tz

        End Sub

#Region "Constructors"
        Public Sub New(u As aspnet_User)

            With u

                Me.ApplicationID = u.ApplicationId
                Me.UserId = u.UserId
                Me.UserName = u.UserName
                Me.Email = u.aspnet_Membership.Email

                Me.LastLoggedInDate = u.aspnet_Membership.LastLoginDate.timezoneToClient
                Me.Mobile = u.aspnet_Membership.Mobile

                Me.ApplicationLocationID = If(u.ApplicationLocation.HasValue, u.ApplicationLocation.Value, Guid.Empty)

                UpdatetimeZone()

                Dim r As aspnet_UsersInRole = u.aspnet_UsersInRoles.FirstOrDefault

                If r IsNot Nothing Then Me.RoleID = r.RoleId
            End With

        End Sub

        Public Sub New()

        End Sub

#End Region
        Public Shared Function CheckExistingUser(_email As String) As FMS.Business.aspnet_User
            Dim o As FMS.Business.aspnet_User = (From i In SingletonAccess.FMSDataContextNew.aspnet_Users _
                                 Where i.aspnet_Membership.Email = _email).FirstOrDefault


            Return o

        End Function
#Region "CRUD"
        'BY RYAN: BUSINESS LAYER CODE FOR SENDING EMAIL TO USER FOR PASSWORD CHANGE REQUEST
        Public Shared Sub SendEmailToUserPasswordRequest(_UserID As Guid, _ApplicationID As Guid, _ApplicationName As String, _Email As String)
            Dim TokenID As Guid = FMS.Business.DataObjects.AuthenticationToken.GetExistingTokenIdForUser(_UserID)

            If TokenID = Nothing Then 'create new token
                Dim t As New FMS.Business.DataObjects.AuthenticationToken

                With t
                    .ApplicationID = _ApplicationID
                    .ExpiryDate = Now.AddDays(1)
                    .StartDate = Now
                    .TokenID = Guid.NewGuid
                    .UserID = _UserID
                    .TokenType = "CP"
                    .isUsedForChangePassword = True
                End With

                TokenID = FMS.Business.DataObjects.AuthenticationToken.Create(t)

            End If

            Dim urlStrBase = "http://{0}.nanosoft.com.au/Account/ChangePassword.aspx?token={1}"

            Dim URL = String.Format(urlStrBase, _ApplicationName, TokenID.ToString)

            'Send Email
            Business.BackgroundCalculations.EmailHelper.SendEmailChangePasswordRequest(_Email, _ApplicationName, URL)

        End Sub
        Public Shared Sub Insert(u As User)
            'BY RYAN
            'create membership
            Dim agp = Membership.GeneratePassword(9, 0)
            Dim user = Membership.CreateUser(u.UserName, agp, u.Email)
            'update user
            Dim o As aspnet_User = (From i In SingletonAccess.FMSDataContextContignous.aspnet_Users _
                                    Where i.UserId = user.ProviderUserKey).SingleOrDefault
            With u
                o.aspnet_Membership.Mobile = .Mobile
                o.TimeZoneID = .TimeZoneID
                o.ApplicationLocation = u.ApplicationLocationID
                SingletonAccess.FMSDataContextContignous.usp_RemoveAllrolesForUserAndAssignRole(o.UserId, .RoleID)
            End With

            SingletonAccess.FMSDataContextContignous.SubmitChanges()

            'Send Email
            If u.SendEmailtoUserWithDefPass Then
                BackgroundCalculations.EmailHelper.SendEmailUserCreated(u.Email, Application.GetFromAppID(u.ApplicationID).ApplicationName, u.UserName, agp)
            End If

        End Sub

        Public Shared Sub Update(u As User)

            'update membership

            Dim user = Membership.GetUser(u.UserId)
            user.Email = u.Email
            Membership.UpdateUser(user)
            'update user
            Dim o As aspnet_User = (From i In SingletonAccess.FMSDataContextContignous.aspnet_Users _
                                    Where i.UserId = u.UserId).SingleOrDefault
            With u
                o.UserName = .UserName
                o.aspnet_Membership.Mobile = u.Mobile
                o.TimeZoneID = .TimeZoneID

                o.ApplicationLocation = u.ApplicationLocationID

                SingletonAccess.FMSDataContextContignous.usp_RemoveAllrolesForUserAndAssignRole(u.UserId, .RoleID)
            End With

            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub
        Public Shared Sub Delete(u As User)
            'BY RYAN 
            Dim o As aspnet_User = (From i In SingletonAccess.FMSDataContextContignous.aspnet_Users _
                                    Where i.UserId = u.UserId).SingleOrDefault

            Membership.DeleteUser(o.UserName)
        End Sub



#End Region
        Public Shared Sub AssignUSerToGeneralRole(userName As String, userid As Guid, applicationid As Guid)


            Dim role As DataObjects.Role = DataObjects.Role.GetAllRolesforApplication(applicationid).Where(Function(x) x.Name.ToLower = "general").SingleOrDefault()

            If role Is Nothing Then Throw New Exception("There was no ""general"" role to assign the user to")

            Dim u As DataObjects.User = DataObjects.User.GetAllUsersForApplication(applicationid).Where(Function(x) x.UserId = userid).SingleOrDefault()

            If u Is Nothing Then Throw New Exception(String.Format("There was non ""(0}"" user found in the database", userName))


            u.RoleID = role.RoleID

            DataObjects.User.Update(u)

        End Sub

        Public Function GetRoleAccess() As Integer

            Dim roles As List(Of FMS.Business.DataObjects.Role) = _
                                (From x In SingletonAccess.FMSDataContextNew.aspnet_UsersInRoles _
                                  Where x.UserId = Me.UserId
                                  Select New FMS.Business.DataObjects.Role(x.aspnet_Role)).ToList

            Dim retint As Integer = 0

            For Each r As DataObjects.Role In roles
                retint = (retint Or r.GetFeaturesBitwise)
            Next

            Return retint

        End Function

        Public Function GetIfAccessToFeature(FeatureListConstants_Value As Integer) As Boolean

            Dim bitwiseRoles As Integer = GetRoleAccess()

            Return (bitwiseRoles And FeatureListConstants_Value) = FeatureListConstants_Value

        End Function

        Public Shared Function GetAllUsersForApplication(applicationid As Guid) As List(Of DataObjects.User)


            Dim retobj = (From i In SingletonAccess.FMSDataContextNew.aspnet_Users
                                 Where i.ApplicationId = applicationid
                                 Select New User(i)).ToList


            Return retobj

        End Function

    End Class

End Namespace
