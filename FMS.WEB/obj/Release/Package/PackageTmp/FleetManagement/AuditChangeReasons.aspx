<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AuditChangeReasons.aspx.vb" Inherits="FMS.WEB.AuditChangeReasons" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Content/grid/bootstrap.css" rel="stylesheet" />
    <link href="../Content/grid/grid.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <dx:ASPxGridView ID="AuditChangeReasonGridView" KeyFieldName="RevenueChangeReasonID" DataSourceID="odsAuditChangeReason" runat="server" Theme="SoftOrange" AutoGenerateColumns="False">
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
                <dx:GridViewDataTextColumn FieldName="RevenueChangeReasonID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Rid" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="RevenueChangeReason" VisibleIndex="3"></dx:GridViewDataTextColumn>
            </Columns>
        </dx:ASPxGridView>
        <asp:ObjectDataSource ID="odsAuditChangeReason" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblRevenueChangeReason" DataObjectTypeName="FMS.Business.DataObjects.tblRevenueChangeReason" DeleteMethod="Delete" InsertMethod="Create" UpdateMethod="Update"></asp:ObjectDataSource>
    </form>
</body>
</html>
