<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ReportContent.aspx.vb" Inherits="FMS.WEB.ReportContent" %>

<%@ Register Assembly="DevExpress.XtraReports.v15.1.Web, Version=15.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report Viewer</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <dx:ASPxDocumentViewer ID="ASPxDocumentViewer1"
                runat="server"
                ReportTypeName="FMS.ReportLogic.VehicleReport"
                Width="98%"
                Theme="SoftOrange"
                ClientInstanceName="mainReport"
                SettingsDocumentMap-EnableAnimation="true"
                SettingsSplitter-SidePanePosition="Left"
                StylesSplitter-SidePaneWidth="230px">
            </dx:ASPxDocumentViewer>
        </div> 

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


    </form>
</body>
</html>
