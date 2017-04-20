<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReceiverMain.aspx.cs" Inherits="FMS.Datalistener.CalAmp.Web.ReceiverMain" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="Button1" runat="server" Text="Get1" OnClick="Get1_Click" />
        <asp:Button ID="Button2" runat="server" Text="Get2" OnClick="Get2_Click" />
        <asp:Button ID="Button3" runat="server" Text="Get3" OnClick="Get3_Click" />
        <asp:Button ID="Button4" runat="server" Text="Get4" OnClick="Get4_Click" />
        <asp:Button ID="Button5" runat="server" Text="Post1" OnClick="Post1_Click" />
    </div>
    </form>
</body>
</html>
