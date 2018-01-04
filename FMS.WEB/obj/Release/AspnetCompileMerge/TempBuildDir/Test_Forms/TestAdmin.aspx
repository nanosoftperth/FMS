<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TestAdmin.aspx.vb" Inherits="FMS.WEB.TestAdmin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="ID"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtID" runat="server"></asp:TextBox>
                </td>              
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Feature Name"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFeaturename" runat="server"></asp:TextBox>
                </td>              
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Description"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDescription" runat="server"></asp:TextBox>
                </td>              
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnInsert" runat="server" Text="Insert" />
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" />
                </td>
                          
            </tr>
            
        </table>
        <table>
            <tr>
                <td>
                    <asp:GridView ID="gvList" runat="server"></asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
