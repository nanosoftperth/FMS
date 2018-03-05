<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MainLight.master" CodeBehind="Contacts.aspx.vb" Inherits="FMS.WEB.Contacts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Content/javascript/jquery-1.10.2.min.js"></script>
    <script>
        function sendMsg(message) {

            $('#aui-flag-container').hide();

            $('#msg').text(message);

            $('#aui-flag-container').toggle('slow'
                , function () {
                    $(this).delay(2500).toggle('slow');
                });
        }



        //Cesar: Use for Delete Dialog Box
        var visibleIndex;
        function OnCustomButtonClick(s, e) {
            visibleIndex = e.visibleIndex;
            popupDelete.SetHeaderText("Delete Item");
            popupDelete.Show();
        }
        function OnClickYes(s, e) {
            dgvConteact.DeleteRow(visibleIndex);
            popupDelete.Hide();
        }
        function OnClickNo(s, e) {
            popupDelete.Hide();
        }

    </script>

    <link href="Content/Jira.css" rel="stylesheet" />
    <div id="aui-flag-container" style="display: none;">
        <div class="aui-flag" aria-hidden="false">
            <div class="aui-message aui-message-success success closeable shadowed aui-will-close">
                <div id="msg">This is some example text</div>
                <span class="aui-icon icon-close" role="button" tabindex="0"></span>
            </div>
        </div>
    </div>
    <asp:ObjectDataSource ID="odsContacts" runat="server" DataObjectTypeName="FMS.Business.DataObjects.Contact" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetAllForApplication" TypeName="FMS.Business.DataObjects.Contact" UpdateMethod="Update">
        <SelectParameters>
            <asp:SessionParameter DbType="Guid" Name="appidd" SessionField="ApplicationID" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <dx:ASPxGridView ID="dgvConteact" ClientInstanceName="dgvConteact" KeyFieldName="ContactID" 
        runat="server" AutoGenerateColumns="False" DataSourceID="odsContacts" Theme="SoftOrange">
        <SettingsSearchPanel Visible="True" />
        <ClientSideEvents CustomButtonClick="OnCustomButtonClick" /> 
        <Templates>
            <EditForm>
                <dx:ASPxGridViewTemplateReplacement runat="server" ID="tr" ReplacementType="EditFormEditors"></dx:ASPxGridViewTemplateReplacement>
                <div style="text-align: right">
                    <dx:ASPxHyperLink Style="text-decoration: underline" ID="lnkUpdate" runat="server" Text="Update" Theme="SoftOrange" NavigateUrl="javascript:void(0);">
                        <ClientSideEvents Click="function (s, e) { 
                                            dgvConteact.UpdateEdit();
                                             sendMsg('User Record Saved!');
                                            }" />
                    </dx:ASPxHyperLink>
                    <dx:ASPxGridViewTemplateReplacement ID="TemplateReplacementCancel" ReplacementType="EditFormCancelButton"
                        runat="server"></dx:ASPxGridViewTemplateReplacement>
                </div>
            </EditForm>
        </Templates>
        <Columns>
            <dx:GridViewCommandColumn ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="0">
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="deleteButton" Text="Delete" />
                </CustomButtons>
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn FieldName="ApplicationID" Visible="False" VisibleIndex="1">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Forname" VisibleIndex="3">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Surname" VisibleIndex="4">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="EmailAddress" VisibleIndex="6">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="MobileNumber" VisibleIndex="7">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="CompanyName" VisibleIndex="5">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="ContactID" Visible="False" VisibleIndex="2">
            </dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>
    <dx:ASPxPopupControl ID="DeleteDialog" runat="server" Text="Are you sure you want to delete this?" 
            ClientInstanceName="popupDelete" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
            <ContentCollection>
                <dx:PopupControlContentControl>
                    <br />
                    <dx:ASPxButton ID="yesButton" runat="server" Text="Yes" AutoPostBack="false">
                        <ClientSideEvents Click="OnClickYes" />
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="noButton" runat="server" Text="No" AutoPostBack="false">
                        <ClientSideEvents Click="OnClickNo" />
                    </dx:ASPxButton>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
</asp:Content>
