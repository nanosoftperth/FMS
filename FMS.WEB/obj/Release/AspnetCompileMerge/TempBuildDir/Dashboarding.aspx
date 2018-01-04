<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main.master" CodeBehind="Dashboarding.aspx.vb" Inherits="FMS.WEB.Dashboarding" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentLeft" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script src="Content/javascript/jquery-3.1.0.min.js"></script>

    <iframe src="http://uniqco-grafana.nanosoft.com.au:3000?theme=light" id="iframeScreenSize" style="border-width: 0px; height: 100vh; width: 100%;">Loading dashobard system now........

    </iframe>

    <script type="text/javascript">

        function cust_setHeight() {

            var height = window.innerHeight;

            //one or the other exists, never both
            var headerHeight1 = $('.headerMenu').height()
            var headerHeight2 = $('#ctl00_ctl00_HeaderPane_EB').height();

            headerHeight = (headerHeight1 == 0) ? headerHeight2 : headerHeight1;

            height = height - headerHeight - 20;

            console.log(headerHeight)

            $('#iframeScreenSize').height(height);

        }

        window.onresize = function (event) {
            cust_setHeight();
        };

        $(document).ready(
                function () { cust_setHeight(); }
            );

    </script>

</asp:Content>
