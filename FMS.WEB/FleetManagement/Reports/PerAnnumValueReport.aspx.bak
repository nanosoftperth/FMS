<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PerAnnumValueReport.aspx.vb" Inherits="FMS.WEB.PerAnnumValueReport" %>

<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dxchartsui" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.XtraReports.v17.2.Web, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../Content/javascript/jquery-1.10.2.min.js" ></script>
         <script type="text/javascript">
             //window.onload = function () {
             //    alert('Loaded!');
             //};

             function btnProcessReport_Click() {
                 popupControl.SetHeaderText('Per Annum Value Report');
                 popupControl.Show();

                 LoadingPanel.Show();

                 $("#frmContent").attr("src", "../ReportContentPage.aspx?Report=PerAnnumValue");
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
        <br />
        <dx:ASPxLabel ID="lblMsg" runat="server" Text="Do you want to update Customer Ratings First? It will take a considerable amount of time."></dx:ASPxLabel>
        <br />
        <br />
        <table>
            <tr>
                <td>
                    <dx:ASPxButton ID="btnYes" runat="server" Text="Yes" OnClick="btnYes_Click" >
                        <ClientSideEvents 
                            Click="function(s, e){
                                pnlUpdate.SetText('Updating customer rating. This may take a while. Please wait...');
                                pnlUpdate.Show();  
                                }" 
                            />
                    </dx:ASPxButton>
                    <%--<dx:ASPxButton ID="btnYes" runat="server" Text="Yes">
                        <ClientSideEvents Click="ProcessPerAnnum" />
                    </dx:ASPxButton>--%>
                </td>
                <td>
                    <dx:ASPxButton ID="btnNo" runat="server" Text="No" AutoPostBack="false">
                        <ClientSideEvents Click="btnProcessReport_Click" />
                    </dx:ASPxButton>
                </td>
                
            </tr>
        </table>
        <br />
    <div>
        <dx:ASPxPopupControl ID="pupReport" runat="server" ClientInstanceName="popupControl" 
                        Height="83px" Modal="True" CloseAction="CloseButton" Width="700px" AllowDragging="True" 
                        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowHeader="true">
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <iframe id="frmContent" name="frmContent" src="../ReportContentPage.aspx?Report=PerAnnumValue" style="height:87vh; width:190vh; border: none; overflow-y: visible;" class="row"></iframe>
                </dx:PopupControlContentControl>

            </ContentCollection>
        </dx:ASPxPopupControl>

        
    </div>
    <div>
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
            Modal="True">
        </dx:ASPxLoadingPanel>
    </div>
    <div>
        <dx:ASPxLoadingPanel ID="lpUpdCustRating" runat="server" ClientInstanceName="pnlUpdate"
            Modal="True">
        </dx:ASPxLoadingPanel>
    </div>
    </form>
</body>
</html>
