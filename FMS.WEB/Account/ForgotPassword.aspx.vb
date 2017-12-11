Public Class ForgotPassword
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub

    Protected Sub btnSendEmail_Click(sender As Object, e As EventArgs)

        'Dim user = If(String.IsNullOrEmpty(tbUserName.Text.Trim()), Membership.GetUser(Membership.GetUserNameByEmail(tbEmail.Text)), Membership.GetUser(tbUserName.Text))


        'the values we are interested in from the conrols on the web form 
        Dim email_addressas As String = tbEmail.Text.ToLower.Trim
        Dim username As String = tbUserName.Text.ToLower.Trim

        'get list of ALL users from the DB which belong to the application we are logged in to 
        Dim users As List(Of Business.DataObjects.User) = Business.DataObjects.User.GetAllUsersForApplication(FMS.Business.ThisSession.ApplicationID)

        Dim db_user As Business.DataObjects.User

        'get the user from the database (preference getting the user from the defined email address)
        If db_user Is Nothing AndAlso Not String.IsNullOrEmpty(email_addressas) Then db_user = (From x In users Where x.Email.ToLower = email_addressas).SingleOrDefault
        If db_user Is Nothing AndAlso (Not String.IsNullOrEmpty(username)) Then db_user = (From x In users Where x.UserName.ToLower = username).SingleOrDefault

        Dim user = Nothing

        If db_user IsNot Nothing Then user = Membership.GetUser(db_user.UserName)

        If user IsNot Nothing Then
            'to avoid creating unecessary number of tokens if user clicks on forgot password many times, just use the first created with 'isFPUsable = True'
            Dim TokenID As Guid = FMS.Business.DataObjects.AuthenticationToken.GetExistingTokenIdForUser(Guid.Parse(user.ProviderUserKey.ToString))

            If TokenID = Nothing Then 'create new token
                Dim t As New FMS.Business.DataObjects.AuthenticationToken

                With t
                    .ApplicationID = FMS.Business.ThisSession.ApplicationID
                    .ExpiryDate = Now.AddDays(1)
                    .StartDate = Now
                    .TokenID = Guid.NewGuid
                    .UserID = user.ProviderUserKey
                    .TokenType = "CP"
                    .isUsedForChangePassword = True
                End With

                TokenID = FMS.Business.DataObjects.AuthenticationToken.Create(t)

            End If

            Dim urlStrBase = "http://{0}.nanosoft.com.au/Account/ChangePassword.aspx?token={1}"

            Dim URL = String.Format(urlStrBase, FMS.Business.ThisSession.ApplicationName, TokenID.ToString)

            'Send Email
            Business.BackgroundCalculations.EmailHelper.SendEmailChangePasswordRequest(user.Email, FMS.Business.ThisSession.ApplicationName, URL)
            Dim h = user.Email.Split("@")
            lblEmail.Text = h(0)(0) & "**************@" & h(1)
            ASPxPanel1.Visible = False
            ASPxPanel2.Visible = True
        Else
            tbUserName.ErrorText = "Invalid user"
            tbUserName.IsValid = False
        End If
    End Sub
End Class