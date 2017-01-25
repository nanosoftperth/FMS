<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MainLight.master" CodeBehind="Contacts.aspx.vb" Inherits="FMS.WEB.Contacts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   <script>
       function sendMsg(message) {

           $('#aui-flag-container').hide();

           $('#msg').text(message);

           $('#aui-flag-container').toggle('slow'
                   , function () {
                       $(this).delay(2500).toggle('slow');
                   });
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
    <dx:ASPxGridView ID="dgvConteact" ClientInstanceName="dgvConteact" KeyFieldName="ContactID" runat="server" AutoGenerateColumns="False" DataSourceID="odsContacts">
        <SettingsSearchPanel Visible="True" />
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
            <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="0">
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
</asp:Content>
