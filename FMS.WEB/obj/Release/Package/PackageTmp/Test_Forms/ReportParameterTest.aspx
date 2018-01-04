<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ReportParameterTest.aspx.vb" Inherits="FMS.WEB.ReportParameterTest" %>

<%@ Register Src="~/Controls/NanoReportParamList.ascx" TagPrefix="uc1" TagName="NanoReportParamList" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        <script src="../Content/javascript/jquery-3.1.0.min.js"></script>

        <div id="sourceDIV" runat="server">
            
            <uc1:NanoReportParamList runat="server" id="NanoReportParamList" />

        </div>
    </form>
</body>
</html>
