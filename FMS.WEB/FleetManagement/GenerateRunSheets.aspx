<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="GenerateRunSheets.aspx.vb" Inherits="FMS.WEB.GenerateRunSheets" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <script src="../Content/javascript/jquery-1.10.2.min.js" ></script>
        <script type="text/javascript">
            var chkPrint = 'false';
            function btnGenerateRunSheets_Click() {
                LoadingPanel.SetText("");
                LoadingPanel.Show();
                if (dtDateOfRun.GetText() != '' && txtDateOfRun.GetText() != '' && dtMonth.GetText() != '') {
                    var param = {
                        DateOfRun: dtDateOfRun.GetText(),
                        DayOfRun: txtDateOfRun.GetText(),
                        Month: cbMonth.GetText(),
                        SqlDate: dtMonth.GetText(),
                        SpecificRun: cbSpecificRun.GetValue(),
                        PrintCustomerSignature: chkPrint
                    }
                    $.ajax({
                        type: 'POST',
                        url: 'GenerateRunSheets.aspx/GenerateRunSheets',
                        dataType: "json",
                        data: JSON.stringify({ 'RunSheets': param }),
                        contentType:"application/json",
                        success: function (data) {
                            if (data.d != null && data.d == 'Success') {
                                ShowMessageWindow(data.d);
                            } else {
                                ShowMessageWindow(data.d);
                            }
                            LoadingPanel.Hide();
                        }
                    })
                } else {
                    LoadingPanel.Hide();
                    ShowMessageWindow('Please enter a date for the run sheets.');
                }
            }
            function GetDayFromMonth() {
                txtDateOfRun.SetText(GetDayWord(dtDateOfRun.date.getDay()));
                dtMonth.SetText(dtDateOfRun.GetText());
            }

            function GetDayWord(idx) {
                var weekday = new Array(7);
                weekday[0] = "Sunday";
                weekday[1] = "Monday";
                weekday[2] = "Tuesday";
                weekday[3] = "Wednesday";
                weekday[4] = "Thursday";
                weekday[5] = "Friday";
                weekday[6] = "Saturday";
                return weekday[idx];
            }

            function GetCheckBoxValue(s, e) {
                var value = s.GetChecked();
                chkPrint =  "'" + value + "'";
            }

            function btnPrintDetail_Click() {
                if (dtDateOfRun.GetText() != '' && txtDateOfRun.GetText() != '' && dtMonth.GetText() != '') {
                    var jScriptDataStr = dtDateOfRun.GetText() + ":" + txtDateOfRun.GetText() + ":" + chkPrint;

                    var paramValue = jScriptDataStr;
                    LoadingPanel.Show();
                    $("#frmContent").attr("src", "ReportContentPage.aspx?Report=GenerateRunSheetDetailReport&param=" + paramValue);
                } else {
                    ShowMessageWindow('Please enter a date for the run sheets.');
                }
            }
            function btnPrintSummary_Click() {
                if (dtDateOfRun.GetText() != '' && txtDateOfRun.GetText() != '' && dtMonth.GetText() != '') {
                    var jScriptDataStr = dtDateOfRun.GetText() + ":" + txtDateOfRun.GetText();

                    var paramValue = jScriptDataStr;
                    LoadingPanel.Show();

                    $("#frmContent").attr("src", "ReportContentPage.aspx?Report=GenerateRunSheetSummaryReport&param=" + paramValue);
                } else {
                    ShowMessageWindow('Please enter a date for the run sheets.');
                }
            }

            function ShowMessageWindow(message) {
                $('#alertMessage').text(message);
                AlertMessageWindow.Show();
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
                    <td>Date of Run</td>
                    <td><dx:ASPxDateEdit ID="dtDateOfRun" ClientInstanceName="dtDateOfRun" DisplayFormatString="dddd, dd MMMM yyyy"  runat="server" Width="195px">
                            <ClientSideEvents LostFocus="function(s,e){
                                GetDayFromMonth(s,e);
                            }"/>
                        </dx:ASPxDateEdit></td>
                    <td>
                        <dx:ASPxTextBox ID="txtDateOfRun" ClientInstanceName="txtDateOfRun" runat="server" Width="170px" ReadOnly="true"></dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>Month:</td>
                    <td><dx:ASPxComboBox ID="cbMonth" ClientInstanceName="cbMonth" DataSourceID="odsMonth" TextField="MonthDescription" ValueField="MonthNo"  runat="server" ValueType="System.String" Width="195px">
                        
                        </dx:ASPxComboBox></td>
                    <td><dx:ASPxDateEdit ID="dtMonth" ClientInstanceName="dtMonth" runat="server"></dx:ASPxDateEdit></td>
                </tr>
                <tr>
                    <td>Specific Run:</td>
                    <td colspan="2">
                        <dx:ASPxComboBox ID="cbSpecificRun" ClientInstanceName="cbSpecificRun" DataSourceID="odsServiceRun" runat="server" Width="365px" TextField="RunDescription" ValueField="RunNUmber">
                            <Columns>
                                <dx:ListBoxColumn FieldName="RunDescription" />
                                <dx:ListBoxColumn FieldName="RunNUmber" />
                            </Columns>
                        </dx:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td>Print Customer Signature:</td>
                    <td>
                        <dx:ASPxCheckBox ID="chkPrintSignature" ClientInstanceName="chkPrintSignature" runat="server">
                            <ClientSideEvents CheckedChanged="function(s, e) { 
                                GetCheckBoxValue(s, e); 
                            }" />
                        </dx:ASPxCheckBox>
                    </td>
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
            <iframe id="frmContent" src="" style="height:76.5vh; width:190vh; border: none; overflow-y: visible;" class="row"></iframe>
            <asp:ObjectDataSource ID="odsMonth" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblMonths"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="odsServiceRun" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblRuns"></asp:ObjectDataSource>
        </div>
        <div>
            <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
                Modal="True">
            </dx:ASPxLoadingPanel>
        </div>
        <dx:ASPxPopupControl ID="AlertMessageWindow" runat="server" Width="320" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="AlertMessageWindow"
        HeaderText="Message" AllowDragging="True" PopupAnimationType="None" EnableViewState="False" AutoUpdatePosition="true">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="Panel1" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <dx:ASPxFormLayout runat="server" ID="ASPxFormLayout1" Width="100%" Height="100%">
                                <Items>
                                    <dx:LayoutItem Caption="">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <p id="alertMessage" style="font-size:15px">Message</p>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem ShowCaption="False" Paddings-PaddingTop="19">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <div style="padding-left:100px">
                                                    <dx:ASPxButton ID="btOK" runat="server" Text="Ok" Width="80px" AutoPostBack="False" Style="float: left; margin-right: 8px">
                                                        <ClientSideEvents Click="function(s, e) { AlertMessageWindow.Hide(); }" />
                                                    </dx:ASPxButton>
                                                </div>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                </Items>
                            </dx:ASPxFormLayout>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>
    </form>
    <div>
        <dx:ASPxLoadingPanel ID="ASPxLoadingPanel1" runat="server" ClientInstanceName="LoadingPanel" 
            Modal="True">
            <Image Url="../Content/Images/drop.gif"/>
        </dx:ASPxLoadingPanel>
    </div>
</body>
</html>
