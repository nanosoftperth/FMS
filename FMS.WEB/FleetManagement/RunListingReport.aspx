<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RunListingReport.aspx.vb" Inherits="FMS.WEB.RunListingReport" %>

<%@ Register Assembly="DevExpress.XtraCharts.v15.1.Web, Version=15.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dxchartsui" %>
<%@ Register Assembly="DevExpress.XtraCharts.v15.1, Version=15.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.XtraReports.v15.1.Web, Version=15.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <script src="../Content/javascript/jquery-1.10.2.min.js" ></script>
         <script type="text/javascript">
             function AdjustWindowHeightAndWidth() {
                 var windowHeight = $(window).height() - $(".headerTop").height() - 20;
                 $('#frmContentRunListing').css({
                     "height": windowHeight
                 });
                 $('#frmContentRunListingByNumber').css({
                     "height": windowHeight
                 });
             }

             $(window).resize(function () {
                 AdjustWindowHeightAndWidth();
             })

             function ShowRunListingReport() {
                AdjustWindowHeightAndWidth();
                RunListingLoadingPanel.Show();
                $("#frmContentRunListing").attr("src", "ReportContentPage.aspx?Report=RunListingReport");
             }
             function ShowRunListingByNumberReport() {
                 AdjustWindowHeightAndWidth();
                 RunListingByNumberLoadingPanel.Show();
                 $("#frmContentRunListingByNumber").attr("src", "ReportContentPage.aspx?Report=RunListingByRunNumberReport");
             }
             $(function () {
                 $('#frmContentRunListing').load(function () {
                     $(this).show();
                     RunListingLoadingPanel.Hide();
                 });
                 $('#frmContentRunListingByNumber').load(function () {
                     $(this).show();
                     RunListingByNumberLoadingPanel.Hide();
                 });
             })
        </script>
</head>
<body onload="ShowRunListingReport();ShowRunListingByNumberReport();">
    <form id="form1" runat="server">
        <div>
            <dx:ASPxPageControl ID="RunListingsPageControl" runat="server" ClientInstanceName="RunListingsPageControl" >
                <TabPages>
                    <dx:TabPage Name="RunListing" Text="Run Listing">
                        <ContentCollection>
                            <dx:ContentControl runat="server">
                                <iframe id="frmContentRunListing" src="" style="height:88.5vh; width:190vh; border: none; overflow-y: visible;" class="row"></iframe>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Name="RunListingByNumber" Text="Run Listing By Number">
                        <ContentCollection>
                            <dx:ContentControl runat="server">
                                <iframe id="frmContentRunListingByNumber" src="" style="height:88.5vh; width:190vh; border: none; overflow-y: visible;" class="row"></iframe>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
            </dx:ASPxPageControl>
        </div>
        <div>
            <dx:ASPxLoadingPanel ID="RunListingLoadingPanel" runat="server" ClientInstanceName="RunListingLoadingPanel"
                Modal="True">
            </dx:ASPxLoadingPanel>
        </div>
        <div>
            <dx:ASPxLoadingPanel ID="RunListingByNumberLoadingPanel" runat="server" ClientInstanceName="RunListingByNumberLoadingPanel"
                Modal="True">
            </dx:ASPxLoadingPanel>
        </div>
    </form>
</body>
</html>