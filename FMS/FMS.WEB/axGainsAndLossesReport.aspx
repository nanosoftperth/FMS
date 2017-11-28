<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="axGainsAndLossesReport.aspx.vb" Inherits="FMS.WEB.axGainsAndLossesReport" %>

<%@ Register Assembly="DevExpress.XtraCharts.v15.1.Web, Version=15.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dxchartsui" %>
<%@ Register Assembly="DevExpress.XtraCharts.v15.1, Version=15.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.XtraReports.v15.1.Web, Version=15.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <script src="Content/javascript/jquery-1.10.2.min.js" ></script>
         <script type="text/javascript">
             function btnProcessReport_Click() {
                 var jScriptDataStr = dtFrom.GetText() + ':' + dtTo.GetText();

                 var paramValue = encodeURIComponent(jScriptDataStr);
                 LoadingPanel.Show();
                 $("#frmContent").attr("src", "axReportContentPage.aspx?Report=GainsAndLossesReport&param=" + paramValue);
             }

             function btnProcessSummaryReport_Click() {
                 var jScriptDataStr = dtFrom.GetText() + ':' + dtTo.GetText();

                 var paramValue = encodeURIComponent(jScriptDataStr);
                 LoadingPanel.Show();
                 $("#frmContent").attr("src", "axReportContentPage.aspx?Report=GainsAndLossesSummaryReport&param=" + paramValue);
             }

             $(function () {
                 $('#frmContent').load(function () {
                     $(this).show();
                     LoadingPanel.Hide();    
                 });
             })
        </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td><dx:ASPxLabel ID="lblDtFrom" runat="server" Text="From Date:"></dx:ASPxLabel></td>
                    <td>&nbsp;</td>
                    <td>
                        <dx:ASPxDateEdit ID="dtFrom" ClientInstanceName="dtFrom" runat="server"></dx:ASPxDateEdit>
                    </td>
                </tr>
                <tr>
                    <td><dx:ASPxLabel ID="lblDtTo" runat="server" Text="To Date:"></dx:ASPxLabel></td>
                    <td>&nbsp;</td>
                    <td>
                        <dx:ASPxDateEdit ID="dtTo" ClientInstanceName="dtTo" runat="server"></dx:ASPxDateEdit>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="padding: 4px 0 0 40px;text-align:right">
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="ASPxButton1" AutoPostBack="false" runat="server" Text="Units" width="90px">
                                        <ClientSideEvents Click="function(s, e) {
	                                        btnProcessReport_Click(s,e);
                                        }"></ClientSideEvents>
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="ASPxButton2" AutoPostBack="false" runat="server" Text="Units Summary">
                                        <ClientSideEvents Click="function(s, e) {
	                                        btnProcessSummaryReport_Click(s,e);
                                        }"></ClientSideEvents>
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <iframe id="frmContent" src="" style="height:82vh; width:190vh; border: none; overflow-y: visible;" class="row"></iframe>
        </div>
        <div>
            <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
                Modal="True">
            </dx:ASPxLoadingPanel>
        </div>
    </form>
</body>
</html>
