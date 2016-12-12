Public Class ChangePasswordRequest
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnSendEmail_Click(sender As Object, e As EventArgs)
        Try

            If Membership.ValidateUser(ThisSession.User.UserName, tbPassword.Text) Then
                'to avoid creating unecessary number of tokens if user clicks on forgot password many times, just use the first created with 'isFPUsable = True'
                Dim TokenID As Guid = FMS.Business.DataObjects.AuthenticationToken.GetExistingTokenIdForUser(ThisSession.UserID)

                If TokenID = Nothing Then 'create new token
                    Dim t As New FMS.Business.DataObjects.AuthenticationToken

                    With t
                        .ApplicationID = ThisSession.ApplicationID
                        .ExpiryDate = Now.AddDays(1)
                        .StartDate = Now
                        .TokenID = Guid.NewGuid
                        .UserID = ThisSession.UserID
                        .TokenType = "CP"
                        .isUsedForChangePassword = True
                    End With

                    TokenID = FMS.Business.DataObjects.AuthenticationToken.Create(t)

                End If

                Dim urlStrBase = "http://{0}.nanosoft.com.au/Account/ChangePassword.aspx?token={1}"

                Dim URL = String.Format(urlStrBase, ThisSession.ApplicationName, TokenID.ToString)

                'Send Email
                Business.BackgroundCalculations.EmailHelper.SendEmailChangePasswordRequest(ThisSession.User.Email, ThisSession.ApplicationName, URL)
                Dim h = ThisSession.User.Email.Split("@")
                lblEmail.Text = h(0)(0) & "**************@" & h(1)
                ASPxPanel1.Visible = False
                ASPxPanel2.Visible = True
            Else

                tbPassword.ErrorText = "Invalid user"
                tbPassword.IsValid = False
            End If

        Catch ex As Exception

            tbPassword.ErrorText = ex.Message
            tbPassword.IsValid = False
        End Try
    End Sub
End Class