﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="axContractCeaseReasonsPopup.aspx.vb" Inherits="FMS.WEB.axContractCeaseReasonsPopup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/grid/bootstrap.css" rel="stylesheet" />
    <link href="Content/grid/grid.css" rel="stylesheet" />
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
        <div class="centerBlock">
            <dx:ASPxGridView ID="ContractCeaseReasonsGridView" KeyFieldName="CeaseReasonID" DataSourceID="odsContractCeaseReasons" runat="server" AutoGenerateColumns="False">
                <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                <Settings ShowPreview="true" />
                <SettingsPager PageSize="10" />
                <SettingsEditing Mode="PopupEditForm"/>
                <SettingsPopup>
                    <EditForm  Modal="true" 
                        VerticalAlign="WindowCenter" 
                        HorizontalAlign="WindowCenter"/>                
                </SettingsPopup>
                <Columns>
                    <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn FieldName="CeaseReasonID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="AID" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="CeaseReasonDescription" VisibleIndex="3"></dx:GridViewDataTextColumn>
                </Columns>
            </dx:ASPxGridView>
            <asp:ObjectDataSource ID="odsContractCeaseReasons" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblContractCeaseReasons" DataObjectTypeName="FMS.Business.DataObjects.tblContractCeaseReasons" DeleteMethod="Delete" InsertMethod="Create" UpdateMethod="Update"></asp:ObjectDataSource>
        </div>
    </form>
</body>
</html>