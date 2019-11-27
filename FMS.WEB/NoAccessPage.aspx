<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MainLight.master" CodeBehind="NoAccessPage.aspx.vb" Inherits="FMS.WEB.NoAccessPage" %>



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

    <img class="centered" src="Content/Images/account_access_1.jpg" />

    <div id="mainMessageDiv" class="">
            <asp:Literal runat="server" ID="literalMainMessage" ClientIDMode="Predictable"></asp:Literal>
    </div>

    <script type="text/javascript">

        $(document).ready(function () { $('#ctl00_ctl00_MainPane_Content_ASPxRoundPanel1_CRC').css('height', 400); })

    </script>

</asp:Content>
