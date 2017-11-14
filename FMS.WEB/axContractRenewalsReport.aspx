<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="axContractRenewalsReport.aspx.vb" Inherits="FMS.WEB.axContractRenewalsReport" %>

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
                var jScriptDataStr = cbZones.GetValue() + ":" + cbZones.GetText() + '|' + 'dtFrom:' + dtFrom.GetText() + '|' + 'dtTo:' + dtTo.GetText();

                var paramValue = encodeURIComponent(jScriptDataStr);
                $("#frmContent").attr("src", "axReportContentPage.aspx?Report=ContractRenewalReport&param=" + paramValue);
             }
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
                <td style="padding: 4px;">
                    <dx:ASPxLabel ID="lblZones" runat="server" Text="Zone:"></dx:ASPxLabel>
                </td>
                <td>&nbsp;</td>
                <td>
                    <dx:ASPxComboBox ID="cbZones" ClientInstanceName="cbZones" DataSourceID="odsZones" runat="server" ValueType="System.String" TextField="AreaDescription" ValueField="Aid"></dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="padding: 4px; text-align:right" >
                    <dx:ASPxButton ID="ASPxButton1" AutoPostBack="false" runat="server" Text="Process">
                        <ClientSideEvents Click="function(s, e) {
	                        btnProcessReport_Click(s,e);
                        }"></ClientSideEvents>
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
        <iframe id="frmContent" src="" onload="this.width=screen.width+10;this.height=(screen.height-150);" style="border: none; overflow-y: visible;" class="row"></iframe>

        <asp:ObjectDataSource ID="odsZones" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tbZone"></asp:ObjectDataSource>
        </div>
    </form>
</body>
</html>
