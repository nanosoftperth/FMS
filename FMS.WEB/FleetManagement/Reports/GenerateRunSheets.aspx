<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="GenerateRunSheets.aspx.vb" Inherits="FMS.WEB.GenerateRunSheets" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <script src="../../Content/javascript/jquery-1.10.2.min.js" ></script>
        <script type="text/javascript">
            function AdjustWindowHeightAndWidth() {
                var windowHeight = $(window).height() - $(".headerTop").height() - 75;
                var windowWidth = $(window).width() - $('.nav-side-menu').width() - 75;
                $('#frmContentRunSheetDetail').css({
                    "height": windowHeight,
                    "width": windowWidth
                });
                $('#frmContentRunSheetSummary').css({
                    "height": windowHeight,
                    "width": windowWidth
                });
            }

            $(window).resize(function () {
                AdjustWindowHeightAndWidth();
            })

            function ShowDetailReport() {
                AdjustWindowHeightAndWidth();
                LoadingPanel.Show();
                $("#frmContentRunSheetDetail").attr("src", "../ReportContentPage.aspx?Report=GenerateRunSheetDetailReport");
            }
            function ShowSummaryReport() {
                AdjustWindowHeightAndWidth();
                LoadingPanel.Show();
                $("#frmContentRunSheetSummary").attr("src", "../ReportContentPage.aspx?Report=GenerateRunSheetSummaryReport");
            }

            $(function () {
                $('#frmContentRunSheetDetail').load(function () {
                    $(this).show();
                    LoadingPanel.Hide();
                });
                $('#frmContentRunSheetSummary').load(function () {
                    $(this).show();
                    LoadingPanel.Hide();
                });
            })
            function OnTabClick(s, e) {
                if (e.tab.GetText() == 'Run Sheet Detail') {
                    ShowDetailReport();
                } else {
                    ShowSummaryReport();
                }
            }
        </script>
</head>
<body onload="ShowDetailReport();ShowSummaryReport();">
    <form id="form1" runat="server">
        <div>
            <dx:ASPxPageControl ID="RunSheetsPageControl" runat="server" ClientInstanceName="RunSheetsPageControl">
                <ClientSideEvents TabClick="OnTabClick" />
                <TabPages>
                    <dx:TabPage Name="RunSheetDetail" Text="Run Sheet Detail">
                        <ContentCollection>
                            <dx:ContentControl runat="server">
                                <iframe id="frmContentRunSheetDetail" src="" style="height:88.5vh; width:190vh; border: none; overflow-y: visible;" class="row"></iframe>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Name="RunSheetSummary" Text="Run Sheet Summary">
                        <ContentCollection>
                            <dx:ContentControl runat="server">
                                <iframe id="frmContentRunSheetSummary" src="" style="height:88.5vh; width:190vh; border: none; overflow-y: visible;" class="row"></iframe>
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
