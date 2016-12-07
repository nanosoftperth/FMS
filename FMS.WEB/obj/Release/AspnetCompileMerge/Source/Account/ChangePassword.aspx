<%@ Page Language="VB" AutoEventWireup="true" MasterPageFile="~/Light.master" CodeBehind="ChangePassword.aspx.vb" Inherits="FMS.WEB.ChangePassword" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">


    <dx:ASPxPanel ID="ASPxPanel1" DefaultButton="btnChangePassword" runat="server">

        <PanelCollection>
            <dx:PanelContent runat="server">
                <div class="accountHeader">
                    <h2>Change Password</h2>
                    <p>Use the form below to change your password.</p>
                    <p>New passwords are required to be a minimum of <%= Membership.MinRequiredPasswordLength %> characters in length.</p>
                </div>

                <br />
                <dx:ASPxLabel ID="lblPassword" runat="server" Theme="SoftOrange" AssociatedControlID="tbPassword" Text="New Password:" />
                <div class="form-field">
                    <dx:ASPxTextBox ID="tbPassword" Theme="SoftOrange" ClientInstanceName="Password" Password="true" runat="server"
                        Width="200px">
                        <ValidationSettings ValidationGroup="ChangeUserPasswordValidationGroup">
                            <RequiredField ErrorText="Password is required." IsRequired="true" />
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                </div>
                <dx:ASPxLabel ID="lblConfirmPassword" Theme="SoftOrange" runat="server" AssociatedControlID="tbConfirmPassword"
                    Text="Confirm Password:" />
                <div class="form-field">
                    <dx:ASPxTextBox ID="tbConfirmPassword" Theme="SoftOrange" Password="true" runat="server" Width="200px">
                        <ValidationSettings ValidationGroup="ChangeUserPasswordValidationGroup">
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
                <dx:ASPxButton ID="btnChangePassword" runat="server" Theme="SoftOrange" Text="Change Password" ValidationGroup="ChangeUserPasswordValidationGroup"
                    OnClick="btnChangePassword_Click">
                </dx:ASPxButton>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>



</asp:Content>
