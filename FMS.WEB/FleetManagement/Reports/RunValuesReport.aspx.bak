<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RunValuesReport.aspx.vb" Inherits="FMS.WEB.RunValuesReport" %>

<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dxchartsui" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.XtraReports.v17.2.Web, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

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

             //$(window).resize(function () {
             //    AdjustWindowHeightAndWidth();
             //})

             function ShowReport() {
                //AdjustWindowHeightAndWidth();
                LoadingPanel.Show();
                $("#frmContent").attr("src", "../ReportContentPage.aspx?Report=RunValuesReport");
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
            <iframe id="frmContent" src="" style="height:96vh; width:190vh; border: none; overflow-y: visible;" class="row"></iframe>
            <asp:ObjectDataSource ID="odsRuns" runat="server" SelectMethod="GetTblRuns" TypeName="FMS.Business.DataObjects.tblRuns"></asp:ObjectDataSource>
        </div>
        <div>
            <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
                Modal="True">
            </dx:ASPxLoadingPanel>
        </div>
    </form>
</body>
</html>
