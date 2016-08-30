Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Function abc(username As String, password As String)
        Return True
    End Function

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click

        If Membership.ValidateUser(tbUserName.Text, tbPassword.Text) Then

            FormsAuthentication.SetAuthCookie(tbUserName.Text, cbRememberMe.Checked)

            If String.IsNullOrEmpty(Request.QueryString("ReturnUrl")) Then
                Response.Redirect("~/Home.aspx")
            Else
                Response.Redirect(Request.QueryString("ReturnUrl"))
            End If

        Else
            tbUserName.ErrorText = "Invalid user"
            tbUserName.IsValid = False
        End If
    End Sub
End Class