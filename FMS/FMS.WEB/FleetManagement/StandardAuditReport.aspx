<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="StandardAuditReport.aspx.vb" Inherits="FMS.WEB.StandardAuditReport" %>

<%@ Register Assembly="DevExpress.XtraCharts.v15.1.Web, Version=15.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dxchartsui" %>
<%@ Register Assembly="DevExpress.XtraCharts.v15.1, Version=15.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.XtraReports.v15.1.Web, Version=15.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <script src="../Content/javascript/jquery-1.10.2.min.js" ></script>
         <script type="text/javascript">
             function StandardAudit() {
                 LoadingPanel.Show();
                 $("#frmContentStandardAudit").attr("src", "ReportContentPage.aspx?Report=StandardAuditReport");
             }

             function AuditContract() {
                 LoadingPanel.Show();
                 $("#frmContentAuditContract").attr("src", "ReportContentPage.aspx?Report=AuditContractReport");
             }

             function AuditOfSiteDetails() {
                 LoadingPanel.Show();
                 $("#frmContentAuditOfSiteDetails").attr("src", "ReportContentPage.aspx?Report=AuditOfSiteDetailChangesReport");
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
        </script>
</head>
<body onload="StandardAudit();AuditContract();AuditOfSiteDetails();">
    <form id="form1" runat="server">
        <div>
            <dx:ASPxPageControl ID="StandardAuditPageControl" runat="server" ClientInstanceName="StandardAuditPageControl" >
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
