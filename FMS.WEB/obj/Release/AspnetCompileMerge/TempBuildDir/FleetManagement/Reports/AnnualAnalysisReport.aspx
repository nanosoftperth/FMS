<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AnnualAnalysisReport.aspx.vb" Inherits="FMS.WEB.AnnualAnalysisReport" %>

<%@ Register Assembly="DevExpress.XtraReports.v17.2.Web, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../Content/javascript/jquery-1.10.2.min.js" ></script>
    <script type="text/javascript">
        function btnProcessReport_Click() {
            LoadingPanel.Show();
            $("#frmContent").attr("src", "../ReportContentPage.aspx?Report=AnnualAnalysis");
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
