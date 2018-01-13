<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RunFNCycles.aspx.vb" Inherits="FMS.WEB.RunFNCycles" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Content/grid/bootstrap.css" rel="stylesheet" />
    <link href="../Content/grid/grid.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <dx:ASPxGridView ID="RunFNCyclesGridView" KeyFieldName="FortnightlyCyclesID" DataSourceID="odsRunFNCycles" runat="server" Theme="SoftOrange" AutoGenerateColumns="False">
            <Settings ShowGroupPanel="True" ShowFilterRow="True" ShowTitlePanel="true"></Settings>
            <Templates>
                <TitlePanel>Run F/N Cycles</TitlePanel>
            </Templates>
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
                <dx:GridViewDataTextColumn FieldName="FortnightlyCyclesID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="AID" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="CycleDescription" VisibleIndex="3"></dx:GridViewDataTextColumn>
            </Columns>
        </dx:ASPxGridView>
        <asp:ObjectDataSource ID="odsRunFNCycles" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblRunFortnightlyCycle" DataObjectTypeName="FMS.Business.DataObjects.tblRunFortnightlyCycle" DeleteMethod="Delete" InsertMethod="Create" UpdateMethod="Update"></asp:ObjectDataSource>
    </form>
</body>
</html>
