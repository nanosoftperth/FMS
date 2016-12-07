<%@ Page Language="VB" AutoEventWireup="true" MasterPageFile="~/Light.master" CodeBehind="Login.aspx.vb" Inherits="FMS.WEB.Login" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" href="Account.css" />


    <dx:ASPxPanel ID="ASPxPanel1" DefaultButton="btnLogin" runat="server">

        <PanelCollection>
            <dx:PanelContent runat="server">

                <table style="width: 100%;">
                    <tr>
                        <td>
                            <div class="accountHeader">
                                <h2>Log In</h2>
                                <p>
                                    Please enter your username and password. 
        <a href="Register.aspx">Register</a> if you don't have an account.
                                </p>
                            </div>
                            <dx:ASPxLabel Theme="SoftOrange" ID="lblUserName" runat="server" AssociatedControlID="tbUserName" Text="User Name:" />
                            <div class="form-field">
                                <dx:ASPxTextBox Theme="SoftOrange" ID="tbUserName" runat="server" Width="200px">
                                    <ValidationSettings ValidationGroup="LoginUserValidationGroup">
                                        <RequiredField ErrorText="User Name is required." IsRequired="true" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </div>
                            <dx:ASPxLabel Theme="SoftOrange" ID="lblPassword" runat="server" AssociatedControlID="tbPassword" Text="Password:" />
                            <div class="form-field">
                                <dx:ASPxTextBox Theme="SoftOrange" ID="tbPassword" runat="server" Password="true" Width="200px">
                                    <ValidationSettings ValidationGroup="LoginUserValidationGroup">
                                        <RequiredField ErrorText="Password is required." IsRequired="true" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </div>
                            <div class="form-field">
                                <asp:CheckBox CssClass="floatRight" ID="cbRememberMe" Text=" remember me?" runat="server" />
                            </div>
                            <div class="form-field">
                                <a href="ForgotPassword.aspx">Forgot Password</a>
                            </div>
                            <dx:ASPxButton ID="btnLogin" Theme="SoftOrange" runat="server" Text="Log In" ValidationGroup="LoginUserValidationGroup"
                                OnClick="btnLogin_Click">
                            </dx:ASPxButton>
                        </td>
                        <td valign="bottom">
                            <img style="float: right;" src="../Content/Images/rightimage.png" /></td>
                    </tr>
                </table>
            </dx:PanelContent>
        </PanelCollection>

    </dx:ASPxPanel>






</asp:Content>
