Public Class ChangePassword
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'check token validity here 
        If Not IsPostBack Then
            Dim _tokenId = Request.QueryString("token") 'get token
            If _tokenId IsNot Nothing Then
                Try
                    Dim Token = FMS.Business.DataObjects.AuthenticationToken.GetFPTokenFromID(Guid.Parse(_tokenId))

                    If Token IsNot Nothing Then
                        If Token.TokenType <> "CP" Then Response.Redirect("~/NoAccessPage.aspx") 'not for forgot password
                        If Token.ExpiryDate < Now Then Response.Redirect("~/NoAccessPage.aspx") 'expired

                    Else
                        Response.Redirect("~/NoAccessPage.aspx") 'invalid token
                    End If
                Catch ex As Exception
                    Response.Redirect("~/NoAccessPage.aspx") 'case of error
                End Try
            Else
                Response.Redirect("~/NoAccessPage.aspx") 'has no token
            End If
        End If

    End Sub

    Protected Sub btnChangePassword_Click(sender As Object, e As EventArgs) Handles btnChangePassword.Click
        Try
            Dim Token = FMS.Business.DataObjects.AuthenticationToken.GetFPTokenFromID(Guid.Parse(Request.QueryString("token")))
            Dim currentUser As MembershipUser = Membership.GetUser(Token.UserID)
            If Not currentUser.ChangePassword(currentUser.ResetPassword(), tbPassword.Text) Then 'resetpassword() since password can't be retrieve
                tbPassword.ErrorText = "Password is not valid"
                tbPassword.IsValid = False
            Else
                Token.isCPUsed = False 'make it so that link can only be used once
                FMS.Business.DataObjects.AuthenticationToken.Update(Token)
                Response.Redirect("ChangePasswordSuccess.aspx")
            End If
        Catch ex As Exception
            tbPassword.ErrorText = ex.Message
            tbPassword.IsValid = False
        End Try

    End Sub
End Class