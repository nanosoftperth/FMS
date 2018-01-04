<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SalesPerson.aspx.vb" Inherits="FMS.WEB.SalesPerson" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Content/grid/bootstrap.css" rel="stylesheet" />
    <link href="../Content/grid/grid.css" rel="stylesheet" />
    <style>
    .dxeMemoEditAreaSys{
        border-width:1px !Important;
    }
</style>
</head>
<body>
    <form id="form1" runat="server">
        <dx:ASPxGridView ID="SalesPersonGridView" KeyFieldName="SalesPersonID" DataSourceID="odsSalesPerson" runat="server" Theme="SoftOrange" AutoGenerateColumns="False">
            <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
            <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
            <Settings ShowPreview="true" />
            <SettingsPager PageSize="10" />
            <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1"/>
            <SettingsPopup>
                <EditForm  Modal="true" 
                    VerticalAlign="WindowCenter" 
                    HorizontalAlign="WindowCenter" width="400px" />
            </SettingsPopup>
            <Columns>
                <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="SalesPersonID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Aid" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="SalesPerson" VisibleIndex="3"></dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn FieldName="SalesPersonStartDate" VisibleIndex="4"></dx:GridViewDataDateColumn>
                <dx:GridViewDataMemoColumn FieldName="SalesPersonComments" VisibleIndex="5" PropertiesMemoEdit-Height="100">
                </dx:GridViewDataMemoColumn>
            </Columns>
        </dx:ASPxGridView>
        <asp:ObjectDataSource ID="odsSalesPerson" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblSalesPersons" DataObjectTypeName="FMS.Business.DataObjects.tblSalesPersons" DeleteMethod="Delete" InsertMethod="Create" UpdateMethod="Update"></asp:ObjectDataSource>
    </form>
</body>
</html>

