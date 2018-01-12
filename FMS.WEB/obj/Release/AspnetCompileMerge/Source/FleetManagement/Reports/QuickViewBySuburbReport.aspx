﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="QuickViewBySuburbReport.aspx.vb" Inherits="FMS.WEB.QuickViewBySuburbReport" %>

<%@ Register Assembly="DevExpress.XtraReports.v15.1.Web, Version=15.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>QuickViewBySuburbReport</title>
    <script src="../../Content/javascript/jquery-1.10.2.min.js" ></script>
         <script type="text/javascript">
             function btnProcessReport_Click() {
                 LoadingPanel.Show();
                 $("#frmContent").attr("src", "../ReportContentPage.aspx?Report=QuickViewBySuburbReport");
             }

             $(function () {
                 $('#frmContent').load(function () {
                     $(this).show();
                     LoadingPanel.Hide();
                 });
             })
        </script>
</head>
<body onload="btnProcessReport_Click()">
    <form id="form1" runat="server">
    <div>
        <iframe id="frmContent" src="" style="height:96vh; width:190vh; border: none; overflow-y: visible;" class="row"></iframe>
    </div>
    <div>
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
            Modal="True">
        </dx:ASPxLoadingPanel>
    </div>
    </form>
</body>
</html>