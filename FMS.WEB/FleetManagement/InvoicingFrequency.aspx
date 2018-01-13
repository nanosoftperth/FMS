<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="InvoicingFrequency.aspx.vb" Inherits="FMS.WEB.InvoicingFrequency" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Content/grid/bootstrap.css" rel="stylesheet" />
    <link href="../Content/grid/grid.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <dx:ASPxGridView ID="InvoicingFrequencyGridView" KeyFieldName="InvoiceFrequencyID" DataSourceID="odsInvoicingFrequency" Theme="SoftOrange" runat="server" AutoGenerateColumns="False">
            <Settings ShowGroupPanel="True" ShowFilterRow="True" ShowTitlePanel="true"></Settings>
            <Templates>
                <TitlePanel>Invoicing Frequency</TitlePanel>
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
                <dx:GridViewDataTextColumn FieldName="InvoiceFrequencyID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="IId" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="InvoiceId" VisibleIndex="3" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Frequency" VisibleIndex="4"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="NoOfWeeks" VisibleIndex="5"></dx:GridViewDataTextColumn>
            </Columns>
        </dx:ASPxGridView>
        <asp:ObjectDataSource ID="odsInvoicingFrequency" runat="server" DataObjectTypeName="FMS.Business.DataObjects.tblInvoicingFrequency" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblInvoicingFrequency" UpdateMethod="Update"></asp:ObjectDataSource>
    </form>
</body>
</html>

