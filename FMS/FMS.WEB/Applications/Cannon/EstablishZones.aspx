<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MainLight.master" CodeBehind="EstablishZones.aspx.vb" Inherits="FMS.WEB.EstablishZones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <dx:ASPxGridView ID="EstablishZonesGridView" KeyFieldName="ZoneID" DataSourceID="odsEstablishZones" runat="server">
        <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
        <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
        <Settings ShowPreview="true" />
        <SettingsPager PageSize="10" />
        <Columns>
            <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn FieldName="ZoneID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="AID" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="AreaDescription" VisibleIndex="3"></dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>
    <asp:ObjectDataSource ID="odsEstablishZones" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tbZone" DataObjectTypeName="FMS.Business.DataObjects.tbZone" DeleteMethod="Delete" InsertMethod="Create" UpdateMethod="Update"></asp:ObjectDataSource>
</asp:Content>
