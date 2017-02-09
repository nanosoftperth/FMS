<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ReportParameterTest.aspx.vb" Inherits="FMS.WEB.ReportParameterTest" %>

<%@ Register Src="~/Controls/NanoReportParam.ascx" TagPrefix="uc1" TagName="NanoReportParam" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        <script src="../Content/javascript/jquery-3.1.0.min.js"></script>

        <div>
            <uc1:NanoReportParam  runat="server" id="nrpTest" />
        </div>
    </form>
</body>
</html>
