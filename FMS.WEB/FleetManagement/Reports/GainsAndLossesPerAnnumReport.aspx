<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="GainsAndLossesPerAnnumReport.aspx.vb" Inherits="FMS.WEB.GainsAndLossesPerAnnumReport" %>

<%@ Register Assembly="DevExpress.XtraCharts.v15.1.Web, Version=15.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dxchartsui" %>
<%@ Register Assembly="DevExpress.XtraCharts.v15.1, Version=15.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.XtraReports.v15.1.Web, Version=15.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <script src="../../Content/javascript/jquery-1.10.2.min.js" ></script>
         <script type="text/javascript">
             function AdjustWindowHeightAndWidth() {
                 var windowHeight = $(window).height() - $(".headerTop").height() - 20;
                 $('#frmContentGainsAndLossesPAReport').css({
                     "height": windowHeight
                 });
                 $('#frmContentGainsAndLossesSummaryReport').css({
                     "height": windowHeight
                 });
             }

             $(window).resize(function () {
                 AdjustWindowHeightAndWidth();
             })

             function ShowPAReport() {
                 AdjustWindowHeightAndWidth();
                 GainsAndLossessPALoadingPanel.Show();
                 $("#frmContentGainsAndLossesPAReport").attr("src", "../ReportContentPage.aspx?Report=GainsAndLossesPerAnnumReport");
             }

             function ShowSummaryReport() {
                 AdjustWindowHeightAndWidth();
                 GainsAndLossessSummaryLoadingPanel.Show();
                 $("#frmContentGainsAndLossesSummaryReport").attr("src", "../ReportContentPage.aspx?Report=GainsAndLossesPerAnnumSummaryReport");
             }

             $(function () {
                 $('#frmContentGainsAndLossesPAReport').load(function () {
                     $(this).show();
                     GainsAndLossessPALoadingPanel.Hide();
                 });
                 $('#frmContentGainsAndLossesSummaryReport').load(function () {
                     $(this).show();
                     GainsAndLossessSummaryLoadingPanel.Hide();
                 });
             })
        </script>
</head>
<body onload="ShowPAReport();ShowSummaryReport();">
    <form id="form1" runat="server">
        <div>
            <dx:ASPxPageControl ID="GainsAndLossesPaSummaryPageControl" runat="server" ClientInstanceName="GainsAndLossesPaSummaryPageControl" >
                <TabPages>
                    <dx:TabPage Name="GainsAndLossesPA" Text="PA Value">
                        <ContentCollection>
                            <dx:ContentControl runat="server">
                                <iframe id="frmContentGainsAndLossesPAReport" src="" style="height:88.5vh; width:190vh; border: none; overflow-y: visible;" class="row"></iframe>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Name="GainsAndLossesPASummary" Text="PA Summary">
                        <ContentCollection>
                            <dx:ContentControl runat="server">
                                <iframe id="frmContentGainsAndLossesSummaryReport" src="" style="height:88.5vh; width:190vh; border: none; overflow-y: visible;" class="row"></iframe>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
            </dx:ASPxPageControl>
        </div>
        <div>
            <dx:ASPxLoadingPanel ID="GainsAndLossessPALoadingPanel" runat="server" ClientInstanceName="GainsAndLossessPALoadingPanel"
                Modal="True">
            </dx:ASPxLoadingPanel>
        </div>
        <div>
            <dx:ASPxLoadingPanel ID="GainsAndLossessSummaryLoadingPanel" runat="server" ClientInstanceName="GainsAndLossessSummaryLoadingPanel"
                Modal="True">
            </dx:ASPxLoadingPanel>
        </div>
    </form>
</body>
</html>
