<%--<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DataLoggerReport.aspx.vb" Inherits="FMS.WEB.DataLoggerReport" %>--%>
<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MainLight.master" CodeBehind="DataLoggerReport.aspx.vb" Inherits="FMS.WEB.DataLoggerReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?v=3.24&key=AIzaSyA2FG3uZ6Pnj8ANsyVaTwnPOCZe4r6jd0g&libraries=places,visualization"></script>
    <script src="Content/javascript/jquery-1.10.2.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#frmContent').load(function () {
                $(this).show();
                LoadingPanel.Hide();
            });
            LoadingPanel.Show();
            $("#frmContent").attr("src", "../ReportContent.aspx?Report=uniqcopage");
        })
    </script>
    <div>
        <iframe id="frmContent" src="" style="height:100%; width:100%; border: none; overflow-y: visible;" class="row"></iframe>
    </div>
    <div>
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
            Modal="True">
        </dx:ASPxLoadingPanel>
    </div>
</asp:Content>