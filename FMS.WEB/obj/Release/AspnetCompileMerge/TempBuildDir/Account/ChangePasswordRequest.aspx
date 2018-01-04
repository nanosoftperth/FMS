<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Light.master" CodeBehind="ChangePasswordRequest.aspx.vb" Inherits="FMS.WEB.ChangePasswordRequest" %>

<asp:Content ID="ClientArea" ContentPlaceHolderID="MainContent" runat="server">


    <dx:ASPxPanel ID="ASPxPanel1" Visible="true" DefaultButton="btnLogin" runat="server">

        <PanelCollection>
            <dx:PanelContent ID="panelContent1" runat="server">

                <table style="width: 100%;">
                    <tr>
                        <td>
                            <div class="accountHeader">
                                <h2>Change Password</h2>
                                <p>Use this form to change you password.</p>
                                <p>Please enter your <b>current </b>password for verification.</p>

                            </div>

                            <img style="float: left; clear: both; margin-top: 10px; margin-bottom: 10px;" src="../Content/Images/flow_chart_2.png" />


                            <br />

                            <div style="float: left; clear: both;">
                                <dx:ASPxLabel ID="lblPassword" Theme="SoftOrange" runat="server" AssociatedControlID="tbPassword" Text="Password:" />
                                <%--<div class="form-field">--%>
                                <dx:ASPxTextBox Theme="SoftOrange" ID="tbPassword" ClientInstanceName="Password" Password="true" runat="server"
                                    Width="200px">
                                    <%--<ValidationSettings ValidationGroup="CPRValidationGroup">
                                            <RequiredField ErrorText="Password is required." IsRequired="true"  />
                                        </ValidationSettings>--%>
                                </dx:ASPxTextBox>
                                <%--</div>--%>
                            </div>


                            <div style="float: left; margin-left: 10px; margin-top: 14px;">
                                <dx:ASPxButton ID="btnSendEmail" Theme="SoftOrange" runat="server" Text="Confirm" ValidationGroup="CPRValidationGroup"
                                    OnClick="btnSendEmail_Click">
                                </dx:ASPxButton>
                            </div>




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
                    <p>
                        An Email has been sent to 
                        <dx:ASPxLabel ID="lblEmail" Theme="SoftOrange" runat="server" Text=""></dx:ASPxLabel>
                        . Go 
        to <a href="/UserManagement.aspx">User Management</a>.
                    </p>
                </div>
            </dx:PanelContent>
        </PanelCollection>

    </dx:ASPxPanel>

</asp:Content>
