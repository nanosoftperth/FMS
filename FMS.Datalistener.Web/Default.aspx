﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FMS.Datalistener.Web.DiagsPage" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        <h1>LMU Telegram receipts</h1>

        <div>
            <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" DataSourceID="odsFiles">
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="filename" VisibleIndex="0"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="parsedDate" VisibleIndex="1"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="contentXML" VisibleIndex="2"></dx:GridViewDataTextColumn>
                </Columns>
            </dx:ASPxGridView>
            <asp:ObjectDataSource runat="server" ID="odsFiles" SelectMethod="GetAllTelegramDefs" TypeName="FMS.Datalistener.Web.DataAccess.TelegramReader"></asp:ObjectDataSource>
        </div>
    </form>
</body>
</html>