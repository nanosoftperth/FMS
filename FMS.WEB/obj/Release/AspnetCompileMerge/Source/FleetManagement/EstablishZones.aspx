<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="EstablishZones.aspx.vb" Inherits="FMS.WEB.EstablishZones" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Content/grid/bootstrap.css" rel="stylesheet" />
    <link href="../Content/grid/grid.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <dx:ASPxGridView ID="EstablishZonesGridView" KeyFieldName="ZoneID" DataSourceID="odsEstablishZones" 
            Theme="SoftOrange" runat="server" AutoGenerateColumns="False" 
            OnRowInserting="EstablishZonesGridView_RowInserting" 
            OnRowUpdating="EstablishZonesGridView_RowUpdating"
            OnRowDeleting="EstablishZonesGridView_RowDeleting">
            <Settings ShowGroupPanel="True" ShowFilterRow="True" ShowTitlePanel="true"></Settings>
            <Templates>
                <TitlePanel>Zones</TitlePanel>
            </Templates>
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
                <dx:GridViewDataTextColumn FieldName="ZoneID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="AID" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="AreaDescription" Caption="Zone Description" VisibleIndex="3"></dx:GridViewDataTextColumn>
            </Columns>
        </dx:ASPxGridView>
        <asp:ObjectDataSource ID="odsEstablishZones" runat="server" SelectMethod="GetAllByApplicationID" TypeName="FMS.Business.DataObjects.tbZone" DataObjectTypeName="FMS.Business.DataObjects.tbZone" DeleteMethod="Delete" InsertMethod="Create" UpdateMethod="Update">
            <SelectParameters>
                <asp:SessionParameter SessionField="ApplicationID" DbType="Guid" Name="appID"></asp:SessionParameter>
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
