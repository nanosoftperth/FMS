<%@ Page Language="VB" AutoEventWireup="true" MasterPageFile="~/Light.master" CodeBehind="Register.aspx.vb" Inherits="FMS.WEB.Register" %>

<asp:Content ID="ClientArea" ContentPlaceHolderID="MainContent" runat="server">

    <dx:ASPxPanel ID="ASPxPanel1" DefaultButton="btnCreateUser" runat="server">

        <PanelCollection>
            <dx:PanelContent runat="server" >

                <table style="width: 100%;">
                    <tr>
                        <td>
                            <div class="accountHeader">
                                <h2>Create a New Account</h2>
                                <p>Use the form below to create a new account.</p>
                                <p>Passwords are required to be a minimum of <%= Membership.MinRequiredPasswordLength %> characters in length.</p>
                            </div>
                            <dx:ASPxLabel ID="lblUserName" Theme="SoftOrange" runat="server" AssociatedControlID="tbUserName" Text="User Name:" />
                            <div class="form-field">
                                <dx:ASPxTextBox Theme="SoftOrange" ID="tbUserName" runat="server" Width="200px">
                                    <ValidationSettings ValidationGroup="RegisterUserValidationGroup">
                                        <RequiredField ErrorText="User Name is required." IsRequired="true" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </div>
                            <dx:ASPxLabel ID="lblEmail" Theme="SoftOrange" runat="server" AssociatedControlID="tbEmail" Text="E-mail:" />
                            <div class="form-field">
                                <dx:ASPxTextBox Theme="SoftOrange" ID="tbEmail" runat="server" Width="200px">
                                    <ValidationSettings ValidationGroup="RegisterUserValidationGroup">
                                        <RequiredField ErrorText="E-mail is required." IsRequired="true" />
                                        <RegularExpression ErrorText="Email validation failed" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </div>
                            <dx:ASPxLabel ID="lblPassword" Theme="SoftOrange" runat="server" AssociatedControlID="tbPassword" Text="Password:" />
                            <div class="form-field">
                                <dx:ASPxTextBox Theme="SoftOrange" ID="tbPassword" ClientInstanceName="Password" Password="true" runat="server"
                                    Width="200px">
                                    <ValidationSettings ValidationGroup="RegisterUserValidationGroup">
                                        <RequiredField ErrorText="Password is required." IsRequired="true" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </div>
                            <dx:ASPxLabel Theme="SoftOrange" ID="lblConfirmPassword" runat="server" AssociatedControlID="tbConfirmPassword"
                                Text="Confirm password:" />
                            <div class="form-field">
                                <dx:ASPxTextBox Theme="SoftOrange" ID="tbConfirmPassword" Password="true" runat="server" Width="200px">
                                    <ValidationSettings ValidationGroup="RegisterUserValidationGroup">
                                        <RequiredField ErrorText="Confirm Password is required." IsRequired="true" />
                                    </ValidationSettings>
                                    <ClientSideEvents Validation="function(s, e) {
                var originalPasswd = Password.GetText();
                var currentPasswd = s.GetText();
                e.isValid = (originalPasswd  == currentPasswd );
                e.errorText = 'The Password and Confirmation Password must match.';
            }" />
                                </dx:ASPxTextBox>
                            </div>
                            <dx:ASPxButton Theme="SoftOrange" ID="btnCreateUser" runat="server" Text="Create User" ValidationGroup="RegisterUserValidationGroup"
                                OnClick="btnCreateUser_Click">
                            </dx:ASPxButton>
                        </td>
                        <td valign="bottom"> 
                            <img style="float: right;" src="../Content/Images/rightimage.png" /> 
                        </td> 
                    </tr>
                </table>

            </dx:PanelContent>
        </PanelCollection>


    </dx:ASPxPanel>




</asp:Content>
