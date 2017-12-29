<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ProduceMYOBFile.aspx.vb" Inherits="FMS.WEB.ProduceMYOBFile" %>

<%@ Register Assembly="DevExpress.XtraReports.v15.1.Web, Version=15.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Produce MYOB Filee</title>
    <script src="../Content/javascript/jquery-1.10.2.min.js" ></script>
    <script type="text/javascript">
        var showPopup = true;
        var iframe;
        var updatenamecheck = false;

        $(document).ready(function () {
            LoadingPanel.Hide();
            popup.SetHeaderText('MYOB Names');
            //function ShowLoading() {
            //    alert("Test");
            //    LoadingPanel.Show();
                
            //}

            //function ShowLoading() {
            //    LoadingPanel.Hide();
                
            //}
        
            //$(function () {
            //    $('#frmContent').load(function () {
            //        $(this).show();
            //        LoadingPanel.Hide();
            //    });
            //})
        })
        function TestThis() {
            alert("This is a test");
        }

        function OnPopupInit(s, e) {
            iframe = popup.GetContentIFrame();

            // the "load" event is fired when the content has been already loaded 
            ASPxClientUtils.AttachEventToElement(iframe, 'load', OnContentLoaded);
        }

        function OnContentLoaded(e) {
            //alert("OnContentLoaded");
            showPopup = false;
            LoadingPanel.Hide();
        }

        function OnPopupShown(s, e) {
            if (showPopup)
                //LoadingPanel.ShowInElement(iframe);
                LoadingPanel.ShowInElement(iframe);
        }

        function OnButtonClick(s, e) {
            //var valUpdateCust = chkUpdateCustName.checked;
            //var Div = document.getElementById("DivMain" + i);
            //var valcheck = $("#start").find('#cbxUpdCustNames').val();
            //var valUpdateCust = $('#cbxUpdCustNames').val();
            //alert(valUpdateCust);
            var MYOBFilename = ctxtMYOBFilename.GetText();
            showPopup = true;
            popup.SetContentUrl("MatchMYOBNamesContents.aspx?UpdateCustNameChecked=" + updatenamecheck + "&MYOBFilename=" + MYOBFilename);
            popup.Show();
        }

        function GetUpdatenameValue(s, e) {
            updatenamecheck = s.GetChecked();
            //alert('test: ' + value);
        }

        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dx:ASPxCheckBox ID="ASPxCheckBox1" runat="server" Text="Keep Form Open"></dx:ASPxCheckBox>
    </div>
    <div id="DivMain">
        <table>
            <tr>
                <td>
                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Invoicing File Name"></dx:ASPxLabel>
                </td>
                <td>
                    <dx:ASPxTextBox ID="txtInvoiceFilename" runat="server" Width="230px"></dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="MYOB Customer File Name"></dx:ASPxLabel>
                </td>
                <td>
                    <dx:ASPxTextBox ID="txtMYOBFilename" runat="server" Width="230px" ClientInstanceName="ctxtMYOBFilename"></dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Invoice Start Number"></dx:ASPxLabel>
                </td>
                <td>
                    <dx:ASPxTextBox ID="txtInvStartNo" runat="server" Width="170px"></dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Invoice Month"></dx:ASPxLabel>
                </td>
                <td>
                    <dx:ASPxComboBox ID="cboMonth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboMonth_SelectedIndexChanged"></dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td>
                    <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="Invoice Start Date"></dx:ASPxLabel>
                </td>
                <td>
                    <dx:ASPxDateEdit ID="dteStart" runat="server"></dx:ASPxDateEdit>
                </td>
            </tr>
            <tr>
                <td>
                    <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="Invoice End Date"></dx:ASPxLabel>
                </td>
                <td>
                    <dx:ASPxDateEdit ID="dteEnd" runat="server"></dx:ASPxDateEdit>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <dx:ASPxButton ID="btnChkMYOBCustNum" runat="server" Text="Check MYOB Customer Number" Width="200px"></dx:ASPxButton>
                    <dx:ASPxPopupControl ID="pupChkMYOB" runat="server" ClientInstanceName="popupControl" 
                        Height="83px" Modal="True" CloseAction="CloseButton" Width="700px" AllowDragging="True" 
                        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowHeader="False">
                        <ContentCollection>
                            <dx:PopupControlContentControl runat="server">
                                <dx:ASPxDocumentViewer ID="ASPxDocumentViewer1"
                                    runat="server"              
                                    Width="98%"
                                    Height="400px"
                                    Theme="SoftOrange"
                                    ClientInstanceName="mainReport"
                                    SettingsDocumentMap-EnableAnimation="true"
                                    SettingsSplitter-SidePanePosition="Left"
                                    StylesSplitter-SidePaneWidth="230px">
                                     <SettingsReportViewer UseIFrame="false" />
                                </dx:ASPxDocumentViewer>
                                <br />
                                <dx:ASPxButton ID="btnCloseChkMYOB" runat="server" Text="Close"></dx:ASPxButton>
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:ASPxPopupControl>
                </td>                
            </tr>
            <tr>
                <td>
                    <dx:ASPxButton ID="btnMatchMYOBNames" runat="server" Text="Match MYOB Names" Width="200px"></dx:ASPxButton>
                    <dx:ASPxPopupControl ID="pupMatchMYOBNames" runat="server" ClientInstanceName="popupControl" 
                        Height="83px" Modal="True" CloseAction="CloseButton" Width="300px" AllowDragging="True" 
                        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowHeader="False">
                        <ContentCollection>
                            <dx:PopupControlContentControl runat="server">
                                <table>
                                    <tr>
                                        <td colspan="2">
                                            <dx:ASPxCheckBox ID="cbxUpdCustNames" runat="server" Text="Update Customer Names"
                                                ClientInstanceName="chkUpdateCustName">
                                                <ClientSideEvents CheckedChanged="function(s, e) { 
                                                            GetUpdatenameValue(s, e); 
                                                        }" />
                                            </dx:ASPxCheckBox>
                                            <br />
                                            <dx:ASPxLabel ID="lblRecAdded" runat="server" Text=""></dx:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxButton ID="btnProcMatch" runat="server" Text="Process" AutoPostBack="false" >
                                                <ClientSideEvents Click="OnButtonClick" />
                                            </dx:ASPxButton>
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="btnCloseMatchMYOBNames" runat="server" Text="Close">                                                
                                            </dx:ASPxButton>
                                            
                                        </td>
                                    </tr>
                                </table>                                
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:ASPxPopupControl>
                    <%-- Start MYOB Filename Page loading --%>
                    <dx:ASPxPopupControl ID="popup" runat="server" AllowDragging="true" AllowResize="true"
                        Height="500px" Width="700px" ClientInstanceName="popup" ContentUrl="javascript:void(0);"
                        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowHeader="False" >
                        <ClientSideEvents Init="OnPopupInit" Shown="OnPopupShown" />
                    </dx:ASPxPopupControl>
                    <%-- End MYOB Filename Page loading --%>
                    <%--<dx:ASPxPopupControl ID="pupMYOBMatchList" runat="server" ClientInstanceName="popupControl" 
                        Height="83px" Modal="True" CloseAction="CloseButton" Width="700px" AllowDragging="True" 
                        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowHeader="False">
                        <ContentCollection>
                            <dx:PopupControlContentControl runat="server">
                                <dx:ASPxDocumentViewer ID="ASPxDocumentViewer3"
                                    runat="server"              
                                    Width="98%"
                                    Height="400px"
                                    Theme="SoftOrange"
                                    ClientInstanceName="mainReport"
                                    SettingsDocumentMap-EnableAnimation="true"
                                    SettingsSplitter-SidePanePosition="Left"
                                    StylesSplitter-SidePaneWidth="230px">
                                     <SettingsReportViewer UseIFrame="false" />
                                </dx:ASPxDocumentViewer>
                                <br />
                                <dx:ASPxButton ID="btnCloseMYOBMatchList" runat="server" Text="Close"></dx:ASPxButton>
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:ASPxPopupControl>--%>
                </td>                
            </tr>
            <tr>
                <td>
                    <dx:ASPxButton ID="btnProcess" runat="server" Text="Process" Width="200px" OnClick="btnProcess_Click"></dx:ASPxButton>
                    <dx:ASPxPopupControl ID="puDialogBox" runat="server" ClientInstanceName="popupControl" 
                        Height="83px" Modal="True" CloseAction="CloseButton" Width="200px" AllowDragging="True" 
                        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowHeader="False">
                        <ContentCollection>
                            <dx:PopupControlContentControl runat="server">
                                <table>
                                    <tr>
                                        <td colspan="2">
                                            <dx:ASPxLabel ID="lblBoxText" runat="server" Text="Place your text here"></dx:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxButton ID="btnYes" runat="server" Text="Yes" OnClick="btnYes_Click"></dx:ASPxButton>
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="btnNo" runat="server" Text="No" OnClick="btnNo_Click"></dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:ASPxPopupControl>
                </td>                
            </tr>
            <tr>
                <td>
                    <dx:ASPxButton ID="btnPrevInvSumRep" runat="server" Text="Previous Invoice Summary Report" Width="200px"></dx:ASPxButton>
                    <dx:ASPxPopupControl ID="pupPrevInvSumRep" runat="server" ClientInstanceName="popupControl" 
                        Height="83px" Modal="True" CloseAction="CloseButton" Width="700px" AllowDragging="True" 
                        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowHeader="False">
                        <ContentCollection>
                            <dx:PopupControlContentControl runat="server">
                                <dx:ASPxDocumentViewer ID="ASPxDocumentViewer2"
                                    runat="server"              
                                    Width="98%"
                                    Height="400px"
                                    Theme="SoftOrange"
                                    ClientInstanceName="mainReport"
                                    SettingsDocumentMap-EnableAnimation="true"
                                    SettingsSplitter-SidePanePosition="Left"
                                    StylesSplitter-SidePaneWidth="230px">
                                     <SettingsReportViewer UseIFrame="false" />
                                </dx:ASPxDocumentViewer>
                                <br />
                                <dx:ASPxButton ID="btnClosePrevInvSumRep" runat="server" Text="Close"></dx:ASPxButton>
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:ASPxPopupControl>
                </td>                
            </tr>
        </table>
        <dx:ASPxLoadingPanel ID="idLoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
            Modal="True">
        </dx:ASPxLoadingPanel>
        <script type="text/javascript">
            function Reportcontent_ControlsInitialised() {

                window.onload = window.onresize = function () {

                    var window_height = window.innerHeight;
                    mainReport.SetHeight(window_height - 20);
                }
            }
        </script>

        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <ClientSideEvents ControlsInitialized="Reportcontent_ControlsInitialised" />
        </dx:ASPxGlobalEvents>
    </div>
    <div>
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
            Modal="True">
        </dx:ASPxLoadingPanel>
    </div>
    </form>
</body>
</html>
