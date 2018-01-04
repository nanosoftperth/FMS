<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CustomerAgents.aspx.vb" Inherits="FMS.WEB.CustomerAgents" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Content/grid/bootstrap.css" rel="stylesheet" />
    <link href="../Content/grid/grid.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <dx:ASPxGridView ID="CustomerAgentsGridView" KeyFieldName="CustomerAgentID" DataSourceID="odsCustomerAgents" 
            Theme="SoftOrange" runat="server" AutoGenerateColumns="False"
            OnRowInserting="CustomerAgentsGridView_RowInserting"
            OnRowUpdating="CustomerAgentsGridView_RowUpdating"
            OnRowDeleting="CustomerAgentsGridView_RowDeleting">
            <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
            <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
            <Settings ShowPreview="true" />
            <SettingsPager PageSize="10" />
            <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />
            <SettingsPopup>
                <EditForm Modal="true"
                    VerticalAlign="WindowCenter"
                    HorizontalAlign="WindowCenter" Width="300px" />
            </SettingsPopup>
            <Columns>
                <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="CustomerAgentID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="AID" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="CustomerAgentName" VisibleIndex="3"></dx:GridViewDataTextColumn>
            </Columns>
        </dx:ASPxGridView>
        <asp:ObjectDataSource ID="odsCustomerAgents" runat="server" SelectMethod="GetAllByApplicationID" TypeName="FMS.Business.DataObjects.tblCustomerAgent" DataObjectTypeName="FMS.Business.DataObjects.tblCustomerAgent" DeleteMethod="Delete" InsertMethod="Create" UpdateMethod="Update">
            <SelectParameters>
                <asp:SessionParameter SessionField="ApplicationID" DbType="Guid" Name="appID"></asp:SessionParameter>
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
