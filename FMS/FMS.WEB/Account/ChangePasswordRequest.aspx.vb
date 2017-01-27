Public Class ChangePasswordRequest
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnSendEmail_Click(sender As Object, e As EventArgs)
        Try

            If Membership.ValidateUser(ThisSession.User.UserName, tbPassword.Text) Then

                'to avoid creating unecessary number of tokens if user clicks on forgot password many times, just use the first created with 'isFPUsable = True'
                FMS.Business.DataObjects.User.SendEmailToUserPasswordRequest(ThisSession.UserID, ThisSession.ApplicationID, ThisSession.ApplicationName, ThisSession.User.Email)

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