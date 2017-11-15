﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="axLengthOfServiceReport.aspx.vb" Inherits="FMS.WEB.axLengthOfServiceReport" %>

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
                 if (txtGreaterThanYearsService.GetText() != '') {
                     var jScriptDataStr = "GreaterThan:" + txtGreaterThanYearsService.GetText();

                     var paramValue = jScriptDataStr;
                     $("#frmContent").attr("src", "axReportContentPage.aspx?Report=LengthOfServiceReport&param=" + paramValue);
                 }
             }
        </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td style="padding: 4px;">
                        <dx:ASPxLabel ID="lblGreaterThanYearsService" runat="server" Text="Greater than Years Service:"></dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtGreaterThanYearsService" ClientInstanceName="txtGreaterThanYearsService" runat="server" Width="170px"></dx:ASPxTextBox>
                    </td>
                    <td style="padding: 4px;">
                        <dx:ASPxButton ID="ASPxButton1" AutoPostBack="false" runat="server" Text="Process">
                            <ClientSideEvents Click="function(s, e) {
	                            btnProcessReport_Click(s,e);
                            }"></ClientSideEvents>
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
            <iframe id="frmContent" src="" onload="this.width=screen.width+10;this.height=(screen.height-100);" style="border: none; overflow-y: visible;" class="row"></iframe>
        </div>
    </form>
</body>
</html>