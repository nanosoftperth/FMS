Public Class ForgotPassword
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub

    Protected Sub btnSendEmail_Click(sender As Object, e As EventArgs)
        Dim user = Membership.GetUser(tbUserName.Text)

        If user IsNot Nothing Then
            'to avoid creating unecessary number of tokens if user clicks on forgot password many times, just use the first created with 'isFPUsable = True'
            Dim TokenID As Guid = FMS.Business.DataObjects.AuthenticationToken.GetExistingTokenIdForUser(Guid.Parse(user.ProviderUserKey.ToString))

            If TokenID = Nothing Then 'create new token
                Dim t As New FMS.Business.DataObjects.AuthenticationToken

                With t
                    .ApplicationID = ThisSession.ApplicationID
                    .ExpiryDate = Now.AddDays(1)
                    .StartDate = Now
                    .TokenID = Guid.NewGuid
                    .UserID = user.ProviderUserKey
                    .TokenType = "FP"
                    .isFPUsable = True
                End With

                TokenID = FMS.Business.DataObjects.AuthenticationToken.Create(t)

            End If

            Dim urlStrBase = "http://{0}.nanosoft.com.au/Account/ChangePassword.aspx?token={1}"

            Dim URL = String.Format(urlStrBase, ThisSession.ApplicationName, TokenID.ToString)

            'Send Email
            Business.BackgroundCalculations.EmailHelper.SendEmailChangePasswordRequest(user.Email, ThisSession.ApplicationName, URL)
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