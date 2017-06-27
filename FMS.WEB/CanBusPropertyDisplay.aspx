﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CanBusPropertyDisplay.aspx.vb" Inherits="FMS.WEB.CanBusPropertyDisplay" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="refresh" content="5" />
    <title></title>
    <script src="Content/javascript/jquery-1.10.2.min.js"></script>
    <script src="Content/javascript/page.js"></script>
    <style type="text/css">
        .auto-style1 {
            height: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>

        <dx:ASPxGridView ID="grid" Settings-ShowColumnHeaders="false" ClientInstanceName="grid" runat="server" Width="400px"
        AutoGenerateColumns="False" OnDataBinding="grid_DataBinding" Settings-UseFixedTableLayout="true" >
            <SettingsPager Visible="False" PageSize="15" >
            </SettingsPager>
            <Columns>
                <dx:GridViewDataTextColumn FieldName="spn" Visible="false" VisibleIndex="0" SortOrder="Ascending" Width="20">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="label" VisibleIndex="1" Width="97">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="description" VisibleIndex="1" Width="140">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="dtTime" VisibleIndex="1" Width="105">
                </dx:GridViewDataTextColumn>
            </Columns>
        </dx:ASPxGridView>

             
    </div>
    </form>
</body>
</html>
