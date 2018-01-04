<%@ Page Language="vb" AutoEventWireup="true" MasterPageFile="~/Light.master" CodeBehind="ForgotPassword.aspx.vb" Inherits="FMS.WEB.ForgotPassword" %>


<asp:Content ID="ClientArea" ContentPlaceHolderID="MainContent" runat="server">


    <dx:ASPxPanel ID="ASPxPanel1" Visible="true"  DefaultButton="btnLogin" runat="server">

        <PanelCollection>
            <dx:PanelContent ID="panelContent1" runat="server">

                <table style="width: 100%;">
                    <tr>
                        <td>
                            <div class="accountHeader">
                                <h2>Forgot Password</h2>
                                <p>Please use this form to recover you password.</p>
                            </div>
                            
                            <dx:ASPxLabel ID="lblUserName" Theme="SoftOrange" runat="server" AssociatedControlID="tbUserName" Text="User Name:" />
                            <div class="form-field">
                                <dx:ASPxTextBox ID="tbUserName" ClientInstanceName="tbUserName" Theme="SoftOrange" runat="server" Width="200px">
                                    <ValidationSettings ValidationGroup="ForgotPasswordValidationGroup">
                                    </ValidationSettings>
                                    <%-- <ClientSideEvents Validation="function(s, e) {
                                        var un = tbEmail.GetText();
                                        if(!un){
                                            var em = tbUserName.GetText();
                                            if(!em) {   
                                                e.isValid = false;
                                                e.errorText = 'Username is required.';
                                            }
                                        }
                                    }" GotFocus="function(s, e) {
                                         tbEmail.SetText('');
                                         }" />--%>
                                </dx:ASPxTextBox>
                            </div>
                            <div class="form-field">
                                <dx:ASPxLabel ID="ASPxLabel1" Theme="SoftOrange" runat="server" Text="- or -" />
                             </div>   
                            <dx:ASPxLabel ID="lblUserEmail" Theme="SoftOrange" runat="server" AssociatedControlID="tbEmail" Text="Email:" />
                            <div class="form-field">
                                <dx:ASPxTextBox Theme="SoftOrange" ID="tbEmail" ClientInstanceName="tbEmail" runat="server" Width="200px">
                                    <ValidationSettings ValidationGroup="ForgotPasswordValidationGroup" >
                                    </ValidationSettings>
                                  <%--  <ClientSideEvents Validation="function(s, e) {
                                        var un = tbUserName.GetText();
                                        if(!un){
                                            var em = tbEmail.GetText();
                                            if(!em) {   
                                                e.isValid = false;
                                                e.errorText = 'E-mail is required.';
                                            }
                                            else
                                            {
                                                var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
                                                if(!emailReg.test( em)){
                                                    e.isValid = false;
                                                    e.errorText = 'Email validation failed';
                                                }
                                            }
                                        }
                                    }" GotFocus="function(s, e) {
                                         tbUserName.SetText('');
                                         }" />--%>
                                </dx:ASPxTextBox>
                            </div>
                            <dx:ASPxButton ID="btnSendEmail" Theme="SoftOrange" runat="server" Text="Send Email" ValidationGroup="ForgotPasswordValidationGroup"
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
    <h2>Forgot Password</h2>
    <p>An Email has been sent to  <dx:ASPxLabel ID="lblEmail"  Theme="SoftOrange" runat="server" Text=""></dx:ASPxLabel>. Go 
        to <a href="Login.aspx">login page</a>.</p>
</div>
            </dx:PanelContent>
        </PanelCollection>

    </dx:ASPxPanel>

</asp:Content>
