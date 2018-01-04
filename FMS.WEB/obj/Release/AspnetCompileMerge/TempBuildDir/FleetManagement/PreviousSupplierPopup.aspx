<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PreviousSupplierPopup.aspx.vb" Inherits="FMS.WEB.PreviousSupplierPopup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .container {
            width: 210px;
        }
        .centerBlock {
          display: table;
          margin: auto;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="centerBlock">
            <dx:ASPxGridView ID="PreviousSupplierGridView" KeyFieldName="PreviousSupplierID" DataSourceID="odsPreviousSupplier" runat="server">
                <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                <Settings ShowPreview="true" />
                <SettingsPager PageSize="10" />
                <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1"/>
                <SettingsPopup>
                    <EditForm  Modal="true" 
                        VerticalAlign="WindowCenter" 
                        HorizontalAlign="WindowCenter" Width="400px"/>                
                </SettingsPopup>
                <Columns>
                    <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn FieldName="PreviousSupplierID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="AID" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="PreviousSupplier" VisibleIndex="3"></dx:GridViewDataTextColumn>
                </Columns>
            </dx:ASPxGridView>
            <asp:ObjectDataSource ID="odsPreviousSupplier" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblPreviousSuppliers" DataObjectTypeName="FMS.Business.DataObjects.tblPreviousSuppliers" DeleteMethod="Delete" InsertMethod="Create" UpdateMethod="Update"></asp:ObjectDataSource>    
        </div>
    </div>
    </form>
</body>
</html>
