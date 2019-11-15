<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main.master" CodeBehind="ExcelView.aspx.vb" Inherits="FMS.WEB.ExcelView" %>
<%@ Register assembly="DevExpress.Web.ASPxSpreadsheet.v18.1, Version=18.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxSpreadsheet" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentLeft" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <script type="text/javascript">

        window.onload = window.onresize = function () {
            var left = document.getElementById('ASPxSpreadsheet1');
            var window_height = window.innerHeight;
            //console.log(left.offsetHeight);
            //console.log(window_height);
            if (left.offsetHeight < window_height) {
                left.style.height = window_height - 130 + "px";

            } else { }
        } 

    </script>

    <dx:ASPxSpreadsheet ID="ASPxSpreadsheet1" Width="100%" ClientIDMode="Static" ClientInstanceName="aspxSpreadsheetClientID" runat="server" EnableTheming="True" Theme="Mulberry">
        <SettingsDocumentSelector>
            <EditingSettings AllowCopy="True" AllowCreate="True" AllowDelete="True" AllowMove="True" />
        </SettingsDocumentSelector>
    </dx:ASPxSpreadsheet>
</asp:Content>
