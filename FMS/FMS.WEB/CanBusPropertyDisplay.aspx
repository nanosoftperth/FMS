<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CanBusPropertyDisplay.aspx.vb" Inherits="FMS.WEB.CanBusPropertyDisplay" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Content/javascript/jquery-1.10.2.min.js"></script>
    <script src="Content/javascript/page.js"></script>
    <style type="text/css">
        .auto-style1 {
            height: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="0px" height="0px">
            <tr>
                <td class="auto-style1"><asp:Label ID="Label1" runat="server" Text="Standard" Height="16px"></asp:Label></td>
                <td class="auto-style1"><asp:TextBox ID="txtStandard" runat="server" Width="80px" Height="10px"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label2" runat="server" Text="PGN" Height="16px"></asp:Label></td>
                <td><asp:TextBox ID="txtPGN" runat="server" Width="80px" Height="10px"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label3" runat="server" Text="SPN" Height="16px"></asp:Label></td>
                <td><asp:TextBox ID="txtSPN" runat="server" Width="80px" Height="10px"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label5" runat="server" Text="Description" Height="16px"></asp:Label></td>
                <td><asp:TextBox ID="txtDescription" runat="server" Width="80px" Height="10px"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label6" runat="server" Text="Resolution" Height="16px"></asp:Label></td>
                <td><asp:TextBox ID="txtResolution" runat="server" Width="80px" Height="10px"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label7" runat="server" Text="Units" Height="16px"></asp:Label></td>
                <td><asp:TextBox ID="txtUnits" runat="server" Width="80px" Height="10px"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label8" runat="server" Text="Offset" Height="16px"></asp:Label></td>
                <td><asp:TextBox ID="txtOffset" runat="server" Width="80px" Height="10px"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label9" runat="server" Text="Pos" Height="16px"></asp:Label></td>
                <td><asp:TextBox ID="txtPos" runat="server" Width="80px" Height="10px"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label10" runat="server" Text="SPN_Length" Height="16px"></asp:Label></td>
                <td><asp:TextBox ID="txtSPN_Length" runat="server" Width="80px" Height="10px"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label11" runat="server" Text="PGN_Length" Height="16px"></asp:Label></td>
                <td><asp:TextBox ID="txtPGN_Length" runat="server" Width="80px" Height="10px"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label12" runat="server" Text="Data_Range" Height="16px"></asp:Label></td>
                <td><asp:TextBox ID="txtData_Range" runat="server" Width="80px" Height="10px"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label13" runat="server" Text="Resolution Multiplier" Height="16px"></asp:Label></td>
                <td><asp:TextBox ID="txtResolution_Multiplier" runat="server" Width="80px" Height="10px"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label14" runat="server" Text="Pos_start" Height="16px"></asp:Label></td>
                <td><asp:TextBox ID="txtPos_start" runat="server" Width="80px" Height="10px"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label15" runat="server" Text="Pos_end" Height="16px"></asp:Label></td>
                <td><asp:TextBox ID="txtPos_end" runat="server" Width="80px" Height="10px"></asp:TextBox></td>
            </tr>
        </table>       
    </div>
    </form>
</body>
</html>
