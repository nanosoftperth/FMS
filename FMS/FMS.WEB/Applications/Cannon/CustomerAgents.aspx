<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MainLight.master" CodeBehind="CustomerAgents.aspx.vb" Inherits="FMS.WEB.CustomerAgents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <dx:ASPxGridView ID="CustomerAgentsGridView" KeyFieldName="CustomerAgentID" DataSourceID="odsCustomerAgents" runat="server">
        <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
        <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
        <Settings ShowPreview="true" />
        <SettingsPager PageSize="10" />
        <Columns>
            <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn FieldName="CustomerAgentID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="AID" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="CustomerAgentName" VisibleIndex="3"></dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>
    <asp:ObjectDataSource ID="odsCustomerAgents" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblCustomerAgent" DataObjectTypeName="FMS.Business.DataObjects.tblCustomerAgent" DeleteMethod="Delete" InsertMethod="Create" UpdateMethod="Update"></asp:ObjectDataSource>
</asp:Content>
