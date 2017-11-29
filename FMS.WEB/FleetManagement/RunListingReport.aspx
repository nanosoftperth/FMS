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
             function btnProcessReport_Click() {
                 if (RunListing.GetValue() != null) {
                     LoadingPanel.Show();
                     if (RunListing.GetValue() == '0') {
                         $("#frmContent").attr("src", "ReportContentPage.aspx?Report=RunListingReport");
                     } else {
                         $("#frmContent").attr("src", "ReportContentPage.aspx?Report=RunListingByRunNumberReport");
                     }
                 }
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
                    <td>
                        <dx:ASPxRadioButtonList ID="RunListing" ClientInstanceName="RunListing" AutoPostBack="false" runat="server" ValueType="System.String" >
                            <Items>
                                <dx:ListEditItem Text="Run Listing Report" Value="0" />
                                <dx:ListEditItem Text="Run Listing by Run Number" Value="1" />
                            </Items>
                        </dx:ASPxRadioButtonList>
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
            <iframe id="frmContent" src="" style="height:88.5vh; width:190vh; border: none; overflow-y: visible;" class="row"></iframe>

            <asp:ObjectDataSource ID="odsCustomers" runat="server" SelectMethod="GetAllOrderByCustomerName" TypeName="FMS.Business.DataObjects.tblCustomers"></asp:ObjectDataSource>
        </div>
        <div>
            <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
                Modal="True">
            </dx:ASPxLoadingPanel>
        </div>
    </form>
</body>
</html>