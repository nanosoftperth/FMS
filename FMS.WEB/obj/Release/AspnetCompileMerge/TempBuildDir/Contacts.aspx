<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MainLight.master" CodeBehind="Contacts.aspx.vb" Inherits="FMS.WEB.Contacts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ObjectDataSource ID="odsContacts" runat="server" DataObjectTypeName="FMS.Business.DataObjects.Contact" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetAllForApplication" TypeName="FMS.Business.DataObjects.Contact" UpdateMethod="Update">
        <SelectParameters>
            <asp:SessionParameter DbType="Guid" Name="appidd" SessionField="ApplicationID" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <dx:ASPxGridView ID="dgvConteact" KeyFieldName="ContactID" runat="server" AutoGenerateColumns="False" DataSourceID="odsContacts">
        <SettingsSearchPanel Visible="True" />
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
