<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MYOBCustomers.aspx.vb" Inherits="FMS.WEB.MYOBCustomers" %>

<%@ Register Assembly="DevExpress.XtraReports.v18.1.Web.WebForms, Version=18.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Content/javascript/jquery-1.10.2.min.js" ></script>
    <script type="text/javascript">
        //function btnProcessReport_Click() {
        //    LoadingPanel.Show();
        //    $("#frmContent").attr("src", "ReportContentPage.aspx?Report=SiteListReport");
        //}

        //$(function () {
        //    $('#frmContent').load(function () {
        //        $(this).show();
        //        LoadingPanel.Hide();
        //    });
        //})
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dx:ASPxDocumentViewer ID="ASPxDocumentViewer1"
                runat="server"              
                Width="98%"
                Theme="SoftOrange"
                ClientInstanceName="mainReport"
                SettingsDocumentMap-EnableAnimation="true"
                SettingsSplitter-SidePanePosition="Left"
                StylesSplitter-SidePaneWidth="230px">
                 <SettingsReportViewer UseIFrame="false" />
            </dx:ASPxDocumentViewer>
        </div> 
    </div>
    </form>
</body>
</html>
