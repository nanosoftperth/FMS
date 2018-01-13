<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FuelLevy.aspx.vb" Inherits="FMS.WEB.FuelLevy" %>
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
        <dx:ASPxGridView ID="FuelLevyGridView" KeyFieldName="FuelLevyID" DataSourceID="odsFuelLevy" runat="server" Theme="SoftOrange" AutoGenerateColumns="False">
            <Settings ShowGroupPanel="True" ShowFilterRow="True" ShowTitlePanel="true"></Settings>
            <Templates>
                <TitlePanel>Fuel Levy</TitlePanel>
            </Templates>
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
                <dx:GridViewDataTextColumn FieldName="FuelLevyID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Aid" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Code" VisibleIndex="3"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Description" VisibleIndex="4"></dx:GridViewDataTextColumn>
                <%--<dx:GridViewDataTextColumn FieldName="Percentage" VisibleIndex="5"></dx:GridViewDataTextColumn>--%>
                <dx:GridViewDataSpinEditColumn FieldName="Percentage" VisibleIndex="5" PropertiesSpinEdit-Height="20"></dx:GridViewDataSpinEditColumn>
                <dx:GridViewDataTextColumn FieldName="MYOBInvoiceCode" VisibleIndex="6"></dx:GridViewDataTextColumn>
            </Columns>
        </dx:ASPxGridView>
        <asp:ObjectDataSource ID="odsFuelLevy" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblFuelLevy" DataObjectTypeName="FMS.Business.DataObjects.tblFuelLevy" DeleteMethod="Delete" InsertMethod="Create" UpdateMethod="Update"></asp:ObjectDataSource>
    </form>
</body>
</html>

