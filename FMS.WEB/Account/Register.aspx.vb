
Public Class Register
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ' if the application does not allow for self-registration, then redirect to a page stating this.
        Dim allowedToSelfRegister = FMS.Business.DataObjects.Setting.GetAllowSelfRegistration(FMS.Business.ThisSession.ApplicationName)

        If Not allowedToSelfRegister Then _
            Me.RedirectAsNoAccess(String.Format("{0} does not allow self-registration, please contact an administrator to create a user account." _
                                                                                                            , FMS.Business.ThisSession.ApplicationName))

    End Sub

    Private Shared synclock_FMSDataContextNew As New Object

    Friend Shared ReadOnly Property FMSDataContextNew As FMS.Business.LINQtoSQLClassesDataContext
        Get

            'If _FMDDataContext IsNot Nothing Then _FMDDataContext.Dispose()
            ' _FMDDataContext = New FMS.Business.LINQtoSQLClassesDataContext

            SyncLock synclock_FMSDataContextNew
                Return New FMS.Business.LINQtoSQLClassesDataContext
            End SyncLock

        End Get
    End Property

    Protected Sub btnCreateUser_Click(sender As Object, e As EventArgs) Handles btnCreateUser.Click
        Try
            'BY RYAN - CHECKS FIRST IF THE EMAIL ACCOUNT IS ALREADY REGISTERED
            Dim o As FMS.Business.aspnet_User = FMS.Business.DataObjects.User.CheckExistingUser(tbEmail.Text)
            lblEmail.Text = ""
            Try
                If o.aspnet_Membership.Email.Length > 1 And IsNothing(0) = False Then
                    lblEmail.Text = "This email account already exists"
                    Exit Sub
                End If
            Catch
                'Nothing to do, just continue statement below
            End Try

            Dim user As MembershipUser = Membership.CreateUser(tbUserName.Text, tbPassword.Text, tbEmail.Text)

            ' we need to assign the new user to the "generl" role (by default)
            FMS.Business.DataObjects.User.AssignUSerToGeneralRole(user.UserName, user.ProviderUserKey, FMS.Business.ThisSession.ApplicationID)

            Response.Redirect(If(Request.QueryString("ReturnUrl"), "~/Account/RegisterSuccess.aspx"))
        Catch exc As MembershipCreateUserException
            If exc.StatusCode = MembershipCreateStatus.DuplicateEmail OrElse exc.StatusCode = MembershipCreateStatus.InvalidEmail Then
                tbEmail.ErrorText = exc.Message
                tbEmail.IsValid = False
            ElseIf exc.StatusCode = MembershipCreateStatus.InvalidPassword Then
                tbPassword.ErrorText = exc.Message
                tbPassword.IsValid = False
            Else
                tbUserName.ErrorText = exc.Message
                tbUserName.IsValid = False
            End If
        End Try
    End Sub
End Class