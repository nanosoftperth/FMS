<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="axIndustryGroupPopup.aspx.vb" Inherits="FMS.WEB.axIndustryGroupPopup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center">
        <dx:ASPxGridView ID="IndustryGroupsGridView" KeyFieldName="IndustryID" DataSourceID="odsIndustryGroups" runat="server" AutoGenerateColumns="False">
            <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
            <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
            <Settings ShowPreview="true" />
            <SettingsPager PageSize="10" />
            <SettingsEditing Mode="PopupEditForm"/>
            <SettingsPopup>
                <EditForm  Modal="true" 
                    VerticalAlign="WindowCenter" 
                    HorizontalAlign="WindowCenter" Width="450px"/>                
            </SettingsPopup>
            <Columns>
                <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="IndustryID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="AID" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="IndustryDescription" VisibleIndex="3"></dx:GridViewDataTextColumn>
            </Columns>
        </dx:ASPxGridView>
        <asp:ObjectDataSource ID="odsIndustryGroups" runat="server" DataObjectTypeName="FMS.Business.DataObjects.tblIndustryGroups" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblIndustryGroups" UpdateMethod="Update"></asp:ObjectDataSource>
    </div>
    </form>
</body>
</html>
