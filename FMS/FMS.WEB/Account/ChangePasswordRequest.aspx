<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Light.master" CodeBehind="ChangePasswordRequest.aspx.vb" Inherits="FMS.WEB.ChangePasswordRequest" %>

<asp:Content ID="ClientArea" ContentPlaceHolderID="MainContent" runat="server">


    <dx:ASPxPanel ID="ASPxPanel1" Visible="true"  DefaultButton="btnLogin" runat="server">

        <PanelCollection>
            <dx:PanelContent ID="panelContent1" runat="server">

                <table style="width: 100%;">
                    <tr>
                        <td>
                            <div class="accountHeader">
                                <h2>Change Password</h2>
                                <p>Use this form to change you password.</p>
                                <p>Please enter your current password for verification.</p>
                            </div>
                            
                             <dx:ASPxLabel ID="lblPassword" Theme="SoftOrange" runat="server" AssociatedControlID="tbPassword" Text="Password:" />
                            <div class="form-field">
                                <dx:ASPxTextBox Theme="SoftOrange" ID="tbPassword" ClientInstanceName="Password" Password="true" runat="server"
                                    Width="200px">
                                    <ValidationSettings ValidationGroup="CPRValidationGroup">
                                        <RequiredField ErrorText="Password is required." IsRequired="true" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </div>
                            <dx:ASPxLabel Theme="SoftOrange" ID="lblConfirmPassword" runat="server" AssociatedControlID="tbConfirmPassword"
                                Text="Confirm password:" />
                            <div class="form-field">
                                <dx:ASPxTextBox Theme="SoftOrange" ID="tbConfirmPassword" Password="true" runat="server" Width="200px">
                                    <ValidationSettings ValidationGroup="CPRValidationGroup">
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
                            <dx:ASPxButton ID="btnSendEmail" Theme="SoftOrange" runat="server" Text="Confirm" ValidationGroup="CPRValidationGroup"
                                OnClick="btnSendEmail_Click">
                            </dx:ASPxButton>
                        </td>
                        <td valign="bottom">
                            <img style="float: right;" src="../Content/Images/rightimage.png" /></td>
                    </tr>
                </table>
            </dx:PanelContent>
        </PanelCollection>

    </dx:ASPxPanel>
    <dx:ASPxPanel ID="ASPxPanel2" Visible="false" runat="server">

        <PanelCollection>
            <dx:PanelContent ID="panelContent2" runat="server">
                 <div class="accountHeader">
    <h2>Change Password</h2>
    <p>An Email has been sent to  <dx:ASPxLabel ID="lblEmail"  Theme="SoftOrange" runat="server" Text=""></dx:ASPxLabel>. Go 
        to <a href="/UserManagement.aspx">User Management</a>.</p>
</div>
            </dx:PanelContent>
        </PanelCollection>

    </dx:ASPxPanel>

</asp:Content>