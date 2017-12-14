<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="GenerateRunSheets.aspx.vb" Inherits="FMS.WEB.GenerateRunSheets" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <script src="../Content/javascript/jquery-1.10.2.min.js" ></script>
        <script type="text/javascript">
            function btnGenerateRunSheets_Click() {
                //var param = {DateOfRun:'1/1/2001', DayOfRun:'Monday', Month:'January', SqlDate:'1/2/2002',SpecificRun:'runme',PrintCustomerSignature:'Jonathan'}
                var param = {
                    DateOfRun: dtDateOfRun.GetText(),
                    DayOfRun: txtDateOfRun.GetText(),
                    Month: cbMonth.GetText(),
                    SqlDate: dtMonth.GetText(),
                    SpecificRun: cbSpecificRun.GetText(),
                    PrintCustomerSignature: chkPrintSignature.GetText()
                }
                $.ajax({
                    type: 'POST',
                    url: 'GenerateRunSheets.aspx/GenerateRunSheets',
                    dataType: "json",
                    data: JSON.stringify({ 'RunSheets': param }),
                    contentType:"application/json",
                    success: function (data) {
                        if (data.d != null) {
                        }
                    }
                })
                //var jScriptDataStr = cbZones.GetValue() + ":" + cbZones.GetText() + '|' + 'dtFrom:' + dtFrom.GetText() + '|' + 'dtTo:' + dtTo.GetText();

                //var paramValue = encodeURIComponent(jScriptDataStr);
                //LoadingPanel.Show();
                //$("#frmContent").attr("src", "ReportContentPage.aspx?Report=ContractRenewalReport&param=" + paramValue);
            }

            function btnPrintDetail_Click() {

            }
            function btnPrintSummary_Click() {

            }

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
        <table>
            <tr>
                <td>Date of Run</td>
                <td><dx:ASPxDateEdit ID="dtDateOfRun" ClientInstanceName="dtDateOfRun" DisplayFormatString="dddd, dd MMMM yyyy"  runat="server" Width="195px"></dx:ASPxDateEdit></td>
                <td>
                    <dx:ASPxTextBox ID="txtDateOfRun" ClientInstanceName="txtDateOfRun" runat="server" Width="170px"></dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td>Month:</td>
                <td><dx:ASPxComboBox ID="cbMonth" ClientInstanceName="cbMonth" DataSourceID="odsMonth" TextField="MonthDescription" ValueField="MonthNo"  runat="server" ValueType="System.String" Width="195px"></dx:ASPxComboBox></td>
                <td><dx:ASPxDateEdit ID="dtMonth" ClientInstanceName="dtMonth" runat="server"></dx:ASPxDateEdit></td>
            </tr>
            <tr>
                <td>Specific Run:</td>
                <td colspan="2">
                    <dx:ASPxComboBox ID="cbSpecificRun" ClientInstanceName="cbSpecificRun" DataSourceID="odsServiceRun" runat="server" Width="365px">
                        <Columns>
                            <dx:ListBoxColumn FieldName="RunDescription" />
                            <dx:ListBoxColumn FieldName="RunNUmber" />
                        </Columns>
                    </dx:ASPxComboBox></td>
            </tr>
            <tr>
                <td>Print Customer Signature:</td>
                <td>
                    <dx:ASPxCheckBox ID="chkPrintSignature" ClientInstanceName="chkPrintSignature" runat="server"></dx:ASPxCheckBox></td>
            </tr>
            <tr>
                <td colspan="3" style="padding: 4px; text-align:right" >
                    <dx:ASPxButton ID="btnGenerateRunSheets" AutoPostBack="false" runat="server" Text="Generate">
                        <ClientSideEvents Click="function(s, e) {
	                        btnGenerateRunSheets_Click(s,e);
                        }"></ClientSideEvents>
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="btnPrintDetail" AutoPostBack="false" runat="server" Text="Print Detail">
                        <ClientSideEvents Click="function(s, e) {
	                        btnPrintDetail_Click(s,e);
                        }"></ClientSideEvents>
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="printSummary" AutoPostBack="false" runat="server" Text="Print Summary">
                        <ClientSideEvents Click="function(s, e) {
	                        btnPrintSummary_Click(s,e);
                        }"></ClientSideEvents>
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
        <asp:ObjectDataSource ID="odsMonth" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblMonths"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsServiceRun" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblRuns"></asp:ObjectDataSource>
    </div>
    </form>
</body>
</html>
