<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ServiceFrequency.aspx.vb" Inherits="FMS.WEB.ServiceFrequency" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Content/grid/bootstrap.css" rel="stylesheet" />
    <link href="../Content/grid/grid.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <dx:ASPxGridView ID="ServiceFrequencyGridView" KeyFieldName="FrequencyID" DataSourceID="odsServiceFrequency" runat="server" Theme="SoftOrange" AutoGenerateColumns="False">
            <Settings ShowGroupPanel="True" ShowFilterRow="True" ShowTitlePanel="true"></Settings>
            <Templates>
                <TitlePanel>Service Frequency</TitlePanel>
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
                <dx:GridViewDataTextColumn FieldName="FrequencyID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="FID" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="FrequencyDescription" VisibleIndex="3"></dx:GridViewDataTextColumn>
                <dx:GridViewDataSpinEditColumn FieldName="Factor" VisibleIndex="4" PropertiesSpinEdit-Height="20"></dx:GridViewDataSpinEditColumn>
                <dx:GridViewDataCheckColumn FieldName="Periodical" VisibleIndex="5"></dx:GridViewDataCheckColumn>
                <dx:GridViewDataTextColumn FieldName="Notes" VisibleIndex="6"></dx:GridViewDataTextColumn>
            </Columns>
        </dx:ASPxGridView>
        <asp:ObjectDataSource ID="odsServiceFrequency" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblServiceFrequency" DataObjectTypeName="FMS.Business.DataObjects.tblServiceFrequency" DeleteMethod="Delete" InsertMethod="Create" UpdateMethod="Update"></asp:ObjectDataSource>
    </form>
</body>
</html>

