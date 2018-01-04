<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="IndustryGroups.aspx.vb" Inherits="FMS.WEB.IndustryGroups" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Content/grid/bootstrap.css" rel="stylesheet" />
    <link href="../Content/grid/grid.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <dx:ASPxGridView ID="IndustryGroupsGridView" KeyFieldName="IndustryID" DataSourceID="odsIndustryGroups" runat="server" 
            Theme="SoftOrange" AutoGenerateColumns="False"
            OnRowInserting="IndustryGroupsGridView_RowInserting"
            OnRowUpdating="IndustryGroupsGridView_RowUpdating"
            OnRowDeleting="IndustryGroupsGridView_RowDeleting">
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
                <dx:GridViewDataTextColumn FieldName="IndustryID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="AID" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="IndustryDescription" VisibleIndex="3"></dx:GridViewDataTextColumn>
            </Columns>
        </dx:ASPxGridView>
        <asp:ObjectDataSource ID="odsIndustryGroups" runat="server" DataObjectTypeName="FMS.Business.DataObjects.tblIndustryGroups" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetAllByApplicationID" TypeName="FMS.Business.DataObjects.tblIndustryGroups" UpdateMethod="Update">
            <SelectParameters>
                <asp:SessionParameter SessionField="ApplicationID" DbType="Guid" Name="appID"></asp:SessionParameter>
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
