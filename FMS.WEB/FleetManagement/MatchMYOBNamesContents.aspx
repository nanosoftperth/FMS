<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MatchMYOBNamesContents.aspx.vb" Inherits="FMS.WEB.MatchMYOBNamesContents" %>

<%@ Register Assembly="DevExpress.XtraReports.v17.2.Web, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dx:ASPxDocumentViewer ID="docvwer" runat="server" Width="100%" Height="410px" Theme="SoftOrange"
            ClientInstanceName="mainReport" SettingsDocumentMap-EnableAnimation="true" SettingsSplitter-SidePanePosition="Left"
            StylesSplitter-SidePaneWidth="230px">
            <SettingsReportViewer UseIFrame="false"/>
        </dx:ASPxDocumentViewer>
    </div>
    <div>
        <br />
        <dx:ASPxButton ID="btnCloseMYOBMatchList" runat="server" Text="Close">
            <ClientSideEvents Click="function(s,e) { parent.popup.Hide(); }" />
        </dx:ASPxButton>
    </div>
    </form>
</body>
</html>
