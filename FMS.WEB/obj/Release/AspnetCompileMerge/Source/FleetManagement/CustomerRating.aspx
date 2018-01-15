<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CustomerRating.aspx.vb" Inherits="FMS.WEB.CustomerRating" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Content/grid/bootstrap.css" rel="stylesheet" />
    <link href="../Content/grid/grid.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <dx:ASPxGridView ID="CustomerRatingGridView" KeyFieldName="CustomerRatingID" DataSourceID="odsCustomerRating" runat="server" Theme="SoftOrange" AutoGenerateColumns="False">
            <Settings ShowGroupPanel="True" ShowFilterRow="True" ShowTitlePanel="true"></Settings>
            <Templates>
                <TitlePanel>Cust Rating</TitlePanel>
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
                <dx:GridViewDataTextColumn FieldName="CustomerRatingID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="RID" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="CustomerRating" VisibleIndex="3"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="CustomerRatingDesc" VisibleIndex="4"></dx:GridViewDataTextColumn>
                <dx:GridViewDataSpinEditColumn FieldName="FromValue" VisibleIndex="5" PropertiesSpinEdit-Height="20"></dx:GridViewDataSpinEditColumn>
                <dx:GridViewDataSpinEditColumn FieldName="ToValue" VisibleIndex="6" PropertiesSpinEdit-Height="20"></dx:GridViewDataSpinEditColumn>
            </Columns>
        </dx:ASPxGridView>
        <asp:ObjectDataSource ID="odsCustomerRating" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblCustomerRating" DataObjectTypeName="FMS.Business.DataObjects.tblCustomerRating" DeleteMethod="Delete" InsertMethod="Create" UpdateMethod="Update"></asp:ObjectDataSource>
    </form>
</body>
</html>

