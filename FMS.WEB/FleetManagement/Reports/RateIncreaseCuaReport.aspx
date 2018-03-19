﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RateIncreaseCuaReport.aspx.vb" Inherits="FMS.WEB.RateIncreaseCuaReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <script src="../../Content/javascript/jquery-1.10.2.min.js" ></script>
         <script type="text/javascript">
             function AdjustWindowHeightAndWidth() {
                 var windowHeight = $(window).height() - $(".headerTop").height() - 20;
                 var windowWidth = $(window).width() - $('.nav-side-menu').width() - 20;
                 $('#frmContent').css({
                     "height": windowHeight,
                     "width": windowWidth
                 })
             }

             $(window).resize(function () {
                 AdjustWindowHeightAndWidth();
             })

             function ShowReport() {
                AdjustWindowHeightAndWidth();
                LoadingPanel.Show();
                $("#frmContent").attr("src", "../ReportContentPage.aspx?Report=RateIncreaseCuaReport");
             }

             $(function () {
                 $('#frmContent').load(function () {
                     $(this).show();
                     LoadingPanel.Hide();
                 });
             })
        </script>
</head>
<body onload="ShowReport()">
    <form id="form1" runat="server">
        <div>
            <iframe id="frmContent" src="" style="height:88.5vh; width:190vh; border: none; overflow-y: visible;" class="row"></iframe>
        </div>
        <div>
            <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
                Modal="True">
            </dx:ASPxLoadingPanel>
        </div>
    </form>
</body>
</html>