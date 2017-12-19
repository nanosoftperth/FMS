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
        <%--<dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
            Modal="True">
        </dx:ASPxLoadingPanel>--%>
    </div>
    </form>
</body>
</html>
