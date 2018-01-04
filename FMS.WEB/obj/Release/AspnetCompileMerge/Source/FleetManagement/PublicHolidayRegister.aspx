<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PublicHolidayRegister.aspx.vb" Inherits="FMS.WEB.PublicHolidayRegister" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Public Holiday Register</title>
    <link href="../Content/grid/bootstrap.css" rel="stylesheet" />
    <link href="../Content/grid/grid.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Public Holiday Register" Font-Bold="True" Font-Size="Large"></dx:ASPxLabel>            
        </div>
        <br />
        <dx:ASPxGridView ID="gvPublicHolidayRegister" ClientInstanceName="gvPublicHolidayRegister" 
            KeyFieldName="Aid" DataSourceID="odsHolidays" runat="server" 
            EnableTheming="True" 
            SettingsBehavior-ConfirmDelete="true"
            SettingsText-ConfirmDelete="Are you sure you wish to delete the holiday?"
            Theme="SoftOrange" AutoGenerateColumns="False">
            <Columns>
                <dx:GridViewCommandColumn ShowEditButton="True" 
                    ShowNewButtonInHeader="True" ShowDeleteButton="True"
                    VisibleIndex="0" ></dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="Aid" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn FieldName="PublicHolidayDate" VisibleIndex="2" Visible="true"></dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn FieldName="PublicHolidayDescription" VisibleIndex="3"></dx:GridViewDataTextColumn>
            </Columns>
        </dx:ASPxGridView>
    <%--<dx:ASPxGridView ID="gvPublicHolidayRegister" KeyFieldName="AID" DataSourceID="odsHolidays" runat="server" 
            Theme="SoftOrange" AutoGenerateColumns="False">
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
                <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" 
                    ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="AID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="PublicHolidayDate" VisibleIndex="2" Visible="true"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="PublicHolidayDescription" VisibleIndex="3"></dx:GridViewDataTextColumn>
            </Columns>
        </dx:ASPxGridView>--%>
        <asp:ObjectDataSource ID="odsHolidays" runat="server" SelectMethod="GetAll" 
            TypeName="FMS.Business.DataObjects.tblPublicHolidayRegister" 
            DataObjectTypeName="FMS.Business.DataObjects.tblPublicHolidayRegister" 
            DeleteMethod="Delete" InsertMethod="Create" UpdateMethod="Update"></asp:ObjectDataSource>
    </form>
</body>
</html>
