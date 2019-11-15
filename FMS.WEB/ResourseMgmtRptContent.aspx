﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ResourseMgmtRptContent.aspx.vb" Inherits="FMS.WEB.ResourseMgmtRptContent" %>

<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v18.1, Version=18.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.XtraReports.v18.1.Web.WebForms, Version=18.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

 
<%@ Register Assembly="DevExpress.XtraCharts.v18.1, Version=18.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.XtraReports.v18.1.Web.WebForms, Version=18.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>  
        <dx:ASPxDocumentViewer ID="ASPxDocumentViewer1"
                            ReportName="FMS.Web.VehicleReport"
                            runat="server" 
                            Width="98%"
                            Height="500px"
                            Theme="SoftOrange"
                            ClientInstanceName="mainReport"
                            SettingsDocumentMap-EnableAnimation="true"
                            SettingsSplitter-SidePanePosition="Left"
                            StylesSplitter-SidePaneWidth="230px">

        </dx:ASPxDocumentViewer> 

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
    </div>
    </form>
</body>
</html>
