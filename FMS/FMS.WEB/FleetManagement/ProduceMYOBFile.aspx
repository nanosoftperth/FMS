<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ProduceMYOBFile.aspx.vb" Inherits="FMS.WEB.ProduceMYOBFile" %>

<%@ Register Assembly="DevExpress.XtraReports.v15.1.Web, Version=15.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Produce MYOB Filee</title>
    <script src="../Content/javascript/jquery-1.10.2.min.js" ></script>
    <script type="text/javascript">
        function btnProcessReport_Click() {
            LoadingPanel.Show();
            $("#frmContent").attr("src", "ReportContentPage.aspx?Report=ProduceMYOBFile");
        }

        $(function () {
            $('#frmContent').load(function () {
                $(this).show();
                LoadingPanel.Hide();
            });
        })
    </script>
</head>
<body onload="btnProcessReport_Click()">
    <form id="form1" runat="server">
    <div>
        <dx:ASPxCheckBox ID="ASPxCheckBox1" runat="server" Text="Keep Form Open"></dx:ASPxCheckBox>
    </div>
    <div>
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
                    <dx:ASPxTextBox ID="txtMYOBFilename" runat="server" Width="230px"></dx:ASPxTextBox>
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
                    <dx:ASPxComboBox ID="cboMonth" runat="server"></dx:ASPxComboBox>
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
                                            <dx:ASPxCheckBox ID="cbxUpdCustNames" runat="server" Text="Update Customer Names"></dx:ASPxCheckBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxButton ID="btnProcMatch" runat="server" Text="Process"></dx:ASPxButton>
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="btnCloseMatchMYOBNames" runat="server" Text="Close"></dx:ASPxButton>
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
                    <dx:ASPxButton ID="btnProcess" runat="server" Text="Process" Width="200px"></dx:ASPxButton>
                </td>                
            </tr>
            <tr>
                <td>
                    <dx:ASPxButton ID="btnPrevInvSumRep" runat="server" Text="Previous Invoice Summary Report" Width="200px"></dx:ASPxButton>
                </td>                
            </tr>
        </table>
        <%--<dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
            Modal="True">
        </dx:ASPxLoadingPanel>--%>
    </div>
    </form>
</body>
</html>
