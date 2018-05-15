<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="StandardAuditReport.aspx.vb" Inherits="FMS.WEB.StandardAuditReport" %>

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
                 var windowHeight = $(window).height() - $(".headerTop").height() - 75;
                 var windowWidth = $(window).width() - $('.nav-side-menu').width() - 75;
                 $('#frmContentStandardAudit').css({
                     "height": windowHeight,
                     "width": windowWidth
                 });
                 $('#frmContentAuditContract').css({
                     "height": windowHeight,
                     "width": windowWidth
                 });
                 $('#frmContentAuditOfSiteDetails').css({
                     "height": windowHeight,
                     "width": windowWidth
                 });
             }

             $(window).resize(function () {
                 AdjustWindowHeightAndWidth();
             })

             function StandardAudit() {
                 AdjustWindowHeightAndWidth();
                 LoadingPanel.Show();
                 $("#frmContentStandardAudit").attr("src", "../ReportContentPage.aspx?Report=StandardAuditReport");
             }

             function AuditContract() {
                 AdjustWindowHeightAndWidth();
                 LoadingPanel.Show();
                 $("#frmContentAuditContract").attr("src", "../ReportContentPage.aspx?Report=AuditContractReport");
             }

             function AuditOfSiteDetails() {
                 AdjustWindowHeightAndWidth();
                 LoadingPanel.Show();
                 $("#frmContentAuditOfSiteDetails").attr("src", "../ReportContentPage.aspx?Report=AuditOfSiteDetailChangesReport");
             }

             $(function () {
                 $('#frmContentStandardAudit').load(function () {
                     $(this).show();
                     LoadingPanel.Hide();
                 });
                 $('#frmContentAuditContract').load(function () {
                     $(this).show();
                     LoadingPanel.Hide();
                 });
                 $('#frmContentAuditOfSiteDetails').load(function () {
                     $(this).show();
                     LoadingPanel.Hide();
                 });
             })
             function OnTabClick(s, e) {
                 if (e.tab.GetText() == 'Rates or Price') {
                     StandardAudit();
                 } else if (e.tab.GetText() == 'Audit Contract') {
                     AuditContract();
                 } else {
                     AuditOfSiteDetails();
                 }
             }
        </script>
</head>
<body onload="StandardAudit();">
    <form id="form1" runat="server">
        <div>
            <dx:ASPxPageControl ID="StandardAuditPageControl" runat="server" ClientInstanceName="StandardAuditPageControl" >
                <ClientSideEvents TabClick="OnTabClick" />
                <TabPages>
                    <dx:TabPage Name="RatesOrPrice" Text="Rates or Price">
                        <ContentCollection>
                            <dx:ContentControl runat="server">
                                <iframe id="frmContentStandardAudit" src="" style="height:88.5vh; width:190vh; border: none; overflow-y: visible;" class="row"></iframe>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Name="AuditContract" Text="Audit Contract">
                        <ContentCollection>
                            <dx:ContentControl runat="server">
                                <iframe id="frmContentAuditContract" src="" style="height:88.5vh; width:190vh; border: none; overflow-y: visible;" class="row"></iframe>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Name="AuditOfSite" Text="Audit Of Site">
                        <ContentCollection>
                            <dx:ContentControl runat="server">
                                <iframe id="frmContentAuditOfSiteDetails" src="" style="height:88.5vh; width:190vh; border: none; overflow-y: visible;" class="row"></iframe>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
            </dx:ASPxPageControl>
        </div>
        <div>
            <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
                Modal="True">
            </dx:ASPxLoadingPanel>
        </div>
    </form>
</body>
</html>
