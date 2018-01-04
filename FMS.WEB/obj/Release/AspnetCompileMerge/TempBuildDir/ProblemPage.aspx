<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MainLight.master" CodeBehind="ProblemPage.aspx.vb" Inherits="FMS.WEB.ProblemPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <style type="text/css">
        .centered {
            position: fixed;
            top: 50%;
            left: 50%;
            /* bring your own prefixes */
            transform: translate(-50%, -50%);
        }
    </style>

    <div class="centered">

        <dx:ASPxLabel ID="lblProblemMessage" runat="server" Text=""></dx:ASPxLabel>
    </div>

</asp:Content>
