<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MainLight.master" CodeBehind="axPreviousSuppliers.aspx.vb" Inherits="FMS.WEB.axPreviousSuppliers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<style>
    .container {
        width: 910px;
    }
</style>
     <dx:ASPxGridView ID="PreviousSupplierGridView" KeyFieldName="PreviousSupplierID" DataSourceID="odsPreviousSupplier" runat="server">
        <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
        <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
        <Settings ShowPreview="true" />
        <SettingsPager PageSize="10" />
        <SettingsEditing Mode="PopupEditForm"/>
        <SettingsPopup>
            <EditForm  Modal="true" 
                VerticalAlign="WindowCenter" 
                HorizontalAlign="WindowCenter" Width="500px"/>                
        </SettingsPopup>
        <Columns>
            <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn FieldName="PreviousSupplierID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="AID" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="PreviousSupplier" VisibleIndex="3"></dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>
    <asp:ObjectDataSource ID="odsPreviousSupplier" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblPreviousSuppliers" DataObjectTypeName="FMS.Business.DataObjects.tblPreviousSuppliers" DeleteMethod="Delete" InsertMethod="Create" UpdateMethod="Update"></asp:ObjectDataSource>
</asp:Content>
