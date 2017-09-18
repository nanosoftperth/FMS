<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SplunkTest.aspx.vb" Inherits="FMS.WEB.SplunkTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Splunk Test</title>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                     <asp:Label ID="Label1" runat="server" Text="Start Date"></asp:Label>
                </td>            
                <td>
                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                     <asp:Label ID="Label2" runat="server" Text="Vehicle"></asp:Label>
                </td>            
                <td>
                    <asp:TextBox ID="txtVehicle" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>

        <asp:Button runat="server" Text="Send to SPLUNK" OnClick="Unnamed1_Click" />
    </div>
    </form>
</body>
</html>
