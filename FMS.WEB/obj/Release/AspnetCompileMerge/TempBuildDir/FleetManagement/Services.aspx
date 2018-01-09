<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Services.aspx.vb" Inherits="FMS.WEB.Services" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Content/grid/bootstrap.css" rel="stylesheet" />
    <link href="../Content/grid/grid.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <dx:ASPxGridView ID="ServicesGridView" KeyFieldName="ServicesID" DataSourceID="odsServices" 
            runat="server" Theme="SoftOrange" AutoGenerateColumns="False"
            OnRowInserting="ServicesGridView_RowInserting"
            OnRowUpdating="ServicesGridView_RowUpdating"
            OnRowDeleting="ServicesGridView_RowDeleting">
            <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
            <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
            <Settings ShowPreview="true" />
            <SettingsPager PageSize="10" />
            <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1"/>
            <SettingsPopup>
                <EditForm  Modal="true" 
                    VerticalAlign="WindowCenter" 
                    HorizontalAlign="WindowCenter" width="300px" />
            </SettingsPopup>
            <Columns>
                <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="ServicesID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Sid" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="ServiceCode" VisibleIndex="3" PropertiesTextEdit-MaxLength="8"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="ServiceDescription" VisibleIndex="4"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="CostOfService" VisibleIndex="5"></dx:GridViewDataTextColumn>
                <dx:GridViewCommandColumn ButtonType="Button" Caption="Driver Comment Reasons" VisibleIndex="5"></dx:GridViewCommandColumn>
            </Columns>
        </dx:ASPxGridView>
        <asp:ObjectDataSource ID="odsServices" runat="server" SelectMethod="GetAllByApplicationID" TypeName="FMS.Business.DataObjects.tblServices" DataObjectTypeName="FMS.Business.DataObjects.tblServices" DeleteMethod="Delete" InsertMethod="Create" UpdateMethod="Update">
            <SelectParameters>
                <asp:SessionParameter SessionField="ApplicationID" DbType="Guid" Name="appID"></asp:SessionParameter>
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>

