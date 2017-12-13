<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MainLight.master" CodeBehind="ReportPopupTester.aspx.vb" Inherits="FMS.WEB.ReportPopupTester" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <script type="text/javascript">

        function showReportPopup(s, e) {

            showPopupVehicleReport('unknown driver (grants vehicle)', 'C7B797AC-7831-4CD3-8AD7-EFC544E45C05');
            e.processOnServer = false;
            return false;
            // $('#frmContent').attr('src', 'ReportContent.aspx?Report=' + selectedReport);
        }


        function showPopupVehicleReport(vehicleName, vehicleID) {
            var popupHeader = 'Vehicle Report | ' + vehicleName;

            popupReport.ShowAtPos(posX, 130);
            popupReport.SetHeight(newHeight - 140);
            popupReport.SetHeaderText(popupHeader);
            popupReport.SetContentUrl('/ReportContent.aspx?Report=VehicleReport&autoFillParams=true&vehicleid=' + vehicleID);

            var posX = $(window).width() - 850;
            var newHeight = $(window).height();

            popupReport.Show();
        }

    </script>

    <dx:ASPxButton ID="ASPxButton1" runat="server" Text="ASPxButton">
        <ClientSideEvents Click="function(s,e){showReportPopup(s, e);}" />
    </dx:ASPxButton>

    

</asp:Content>
